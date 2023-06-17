using System;
using System.Collections.Generic;
using System.Data;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Locale.Runtime;
using UGF.RuntimeTools.Runtime.Tables;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    public static class LocaleEditorUtility
    {
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

        public static void UpdateFromDataTable(LocaleTableAsset localeTable, DataTable dataTable, IReadOnlyDictionary<string, GlobalId> locales, bool clear = false)
        {
            if (localeTable == null) throw new ArgumentNullException(nameof(localeTable));
            if (dataTable == null) throw new ArgumentNullException(nameof(dataTable));
            if (locales == null) throw new ArgumentNullException(nameof(locales));

            var table = (LocaleTable<string>)localeTable.Get();

            if (clear)
            {
                table.Entries.Clear();
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                string name = (string)row["name"];

                if (!OnTryGetTableEntryByName(table, name, out LocaleTableEntry<string> entry))
                {
                    entry = new LocaleTableEntry<string>
                    {
                        Id = GlobalId.Generate(),
                        Name = name
                    };

                    table.Entries.Add(entry);
                }

                foreach ((string localeName, GlobalId localeId) in locales)
                {
                    string value = (string)row[localeName];

                    if (!OnTryGetTableEntryValueByLocaleId(entry, localeId, out LocaleTableEntryValue<string> entryValue))
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

        public static DataTable GetDataTable(LocaleTableAsset localeTable)
        {
            if (localeTable == null) throw new ArgumentNullException(nameof(localeTable));

            var data = new DataTable();
            ITable table = localeTable.Get();

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
            }

            return data;
        }

        private static bool OnTryGetTableEntryByName<T>(ITable table, string name, out T entry) where T : class, ITableEntry
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            foreach (ITableEntry tableEntry in table.Entries)
            {
                if (tableEntry.Name == name)
                {
                    entry = (T)tableEntry;
                    return true;
                }
            }

            entry = default;
            return false;
        }

        private static bool OnTryGetTableEntryValueByLocaleId(LocaleTableEntry<string> entry, GlobalId localeId, out LocaleTableEntryValue<string> entryValue)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            if (!localeId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(localeId));

            for (int i = 0; i < entry.Values.Count; i++)
            {
                LocaleTableEntryValue<string> value = entry.Values[i];

                if (value.LocaleId == localeId)
                {
                    entryValue = value;
                    return true;
                }
            }

            entryValue = default;
            return false;
        }
    }
}
