using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using UGF.Csv.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Locale.Runtime;
using UGF.RuntimeTools.Runtime.Tables;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    public static class LocaleEditorUtility
    {
        public static void CsvImport(LocaleTableAsset localeTable, string path, IReadOnlyList<LocaleDescriptionAsset> locales)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            string csv = File.ReadAllText(path);
            DataTable data = CsvUtility.FromCsv(csv);

            UpdateFromDataTable(localeTable, data, locales);

            EditorUtility.SetDirty(localeTable);
            AssetDatabase.SaveAssets();
        }

        public static void CsvExport(LocaleTableAsset localeTable, string path, IReadOnlyList<LocaleDescriptionAsset> locales)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            DataTable data = GetDataTable(localeTable, locales);
            string csv = CsvUtility.ToCsv(data);

            File.WriteAllText(path, csv);

            AssetDatabase.SaveAssets();
        }

        public static void UpdateEntries(LocaleTableDescriptionAsset tableDescriptionAsset, LocaleTableAsset tableAsset)
        {
            if (tableDescriptionAsset == null) throw new ArgumentNullException(nameof(tableDescriptionAsset));
            if (tableAsset == null) throw new ArgumentNullException(nameof(tableAsset));

            IDictionary<GlobalId, LocaleEntriesDescriptionAsset> tableAssets = new Dictionary<GlobalId, LocaleEntriesDescriptionAsset>();
            IDictionary<GlobalId, IDictionary<GlobalId, object>> tableEntries = LocaleUtility.GetEntries(tableAsset.Get());

            for (int i = 0; i < tableDescriptionAsset.Entries.Count; i++)
            {
                LocaleTableDescriptionAsset.Entry entry = tableDescriptionAsset.Entries[i];
                string entriesPath = AssetDatabase.GUIDToAssetPath(entry.Entries.ToString());
                var entriesAsset = AssetDatabase.LoadAssetAtPath<LocaleEntriesDescriptionAsset>(entriesPath);

                tableAssets.Add(entry.Locale, entriesAsset);
            }

            foreach ((GlobalId localeId, IDictionary<GlobalId, object> entries) in tableEntries)
            {
                if (tableAssets.TryGetValue(localeId, out LocaleEntriesDescriptionAsset asset))
                {
                    asset.SetValues(entries);

                    EditorUtility.SetDirty(asset);
                }
            }

            AssetDatabase.SaveAssets();
        }

        public static void UpdateFromDataTable(LocaleTableAsset localeTable, DataTable dataTable, IReadOnlyList<LocaleDescriptionAsset> locales, bool clear = false)
        {
            UpdateFromDataTable((LocaleTable<string>)localeTable.Get(), dataTable, locales, clear);
        }

        public static void UpdateFromDataTable(LocaleTable<string> localeTable, DataTable dataTable, IReadOnlyList<LocaleDescriptionAsset> locales, bool clear = false)
        {
            if (localeTable == null) throw new ArgumentNullException(nameof(localeTable));
            if (dataTable == null) throw new ArgumentNullException(nameof(dataTable));
            if (locales == null) throw new ArgumentNullException(nameof(locales));

            if (clear)
            {
                localeTable.Entries.Clear();
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                string name = (string)row["name"];

                if (!localeTable.TryGetByName(name, out LocaleTableEntry<string> entry))
                {
                    entry = new LocaleTableEntry<string>
                    {
                        Id = GlobalId.Generate(),
                        Name = name
                    };

                    localeTable.Entries.Add(entry);
                }

                foreach (LocaleDescriptionAsset locale in locales.OrderBy(x => x.CultureName))
                {
                    string value = (string)row[locale.CultureName];
                    string localePath = AssetDatabase.GetAssetPath(locale);
                    GlobalId localeId = GlobalId.Parse(AssetDatabase.AssetPathToGUID(localePath));

                    if (!entry.TryGet(localeId, out LocaleTableEntryValue<string> entryValue))
                    {
                        entryValue = new LocaleTableEntryValue<string>
                        {
                            LocaleId = localeId
                        };

                        entry.Values.Add(entryValue);
                    }

                    entryValue.Value = value;
                }
            }
        }

        public static DataTable GetDataTable(LocaleTableAsset localeTable, IReadOnlyList<LocaleDescriptionAsset> locales)
        {
            if (localeTable == null) throw new ArgumentNullException(nameof(localeTable));
            if (locales == null) throw new ArgumentNullException(nameof(locales));

            var data = new DataTable();
            ITable table = localeTable.Get();

            data.Columns.Add("name");

            foreach (LocaleDescriptionAsset locale in locales.OrderBy(x => x.CultureName))
            {
                data.Columns.Add(locale.CultureName);
            }

            foreach (ITableEntry tableEntry in table.Entries)
            {
                var entry = (ILocaleTableEntry)tableEntry;
                DataRow row = data.NewRow();

                row["name"] = tableEntry.Name;

                foreach (ILocaleTableEntryValue value in entry.Values)
                {
                    string localePath = AssetDatabase.GUIDToAssetPath(value.LocaleId.ToString());
                    var localeDescription = AssetDatabase.LoadAssetAtPath<LocaleDescriptionAsset>(localePath);

                    row[localeDescription.CultureName] = value.Value;
                }

                data.Rows.Add(row);
            }

            data.DefaultView.Sort = "name";

            return data.DefaultView.ToTable();
        }
    }
}
