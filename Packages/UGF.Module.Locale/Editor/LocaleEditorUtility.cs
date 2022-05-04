using System;
using System.Collections.Generic;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    public static class LocaleEditorUtility
    {
        public static void UpdateEntries(LocaleTableDescriptionAsset tableDescriptionAsset, LocaleTableAsset tableAsset)
        {
            if (tableDescriptionAsset == null) throw new ArgumentNullException(nameof(tableDescriptionAsset));
            if (tableAsset == null) throw new ArgumentNullException(nameof(tableAsset));

            IDictionary<string, LocaleEntriesDescriptionAsset> tableAssets = new Dictionary<string, LocaleEntriesDescriptionAsset>();
            ILocaleTable table = tableAsset.GetTable();
            IDictionary<string, IDictionary<string, object>> tableEntries = LocaleUtility.GetEntries(table);

            foreach (LocaleTableDescriptionAsset.Entry entry in tableDescriptionAsset.Entries)
            {
                string entriesPath = AssetDatabase.GUIDToAssetPath(entry.Entries);
                var entriesAsset = AssetDatabase.LoadAssetAtPath<LocaleEntriesDescriptionAsset>(entriesPath);

                tableAssets.Add(entry.Locale, entriesAsset);
            }

            foreach ((string localeId, IDictionary<string, object> entries) in tableEntries)
            {
                if (tableAssets.TryGetValue(localeId, out LocaleEntriesDescriptionAsset asset))
                {
                    asset.SetValues(entries);

                    EditorUtility.SetDirty(asset);
                }
            }

            AssetDatabase.SaveAssets();
        }

        public static IReadOnlyList<LocaleTableAsset> FindTableAssetAll()
        {
            var result = new List<LocaleTableAsset>();
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(LocaleTableAsset)}");

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var asset = AssetDatabase.LoadAssetAtPath<LocaleTableAsset>(path);

                result.Add(asset);
            }

            return result;
        }

        public static bool TryGetEntryNameFromAll(string id, out string name)
        {
            IReadOnlyList<LocaleTableAsset> tables = FindTableAssetAll();

            name = default;
            return tables.Count > 0 && TryGetEntryName(tables, id, out name);
        }

        public static bool TryGetEntryName(IReadOnlyList<LocaleTableAsset> tables, string id, out string name)
        {
            if (tables == null) throw new ArgumentNullException(nameof(tables));
            if (tables.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(tables));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            for (int i = 0; i < tables.Count; i++)
            {
                ILocaleTable table = tables[i].GetTable();

                if (TryGetEntryName(table, id, out name))
                {
                    return true;
                }
            }

            name = default;
            return false;
        }

        public static bool TryGetEntryName(ILocaleTable table, string id, out string name)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            foreach (ILocaleTableEntry entry in table.Entries)
            {
                if (entry.Id == id)
                {
                    name = entry.Name;
                    return true;
                }
            }

            name = default;
            return false;
        }
    }
}
