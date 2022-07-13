using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
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

            IDictionary<GlobalId, LocaleEntriesDescriptionAsset> tableAssets = new Dictionary<GlobalId, LocaleEntriesDescriptionAsset>();
            ILocaleTable table = tableAsset.GetTable();
            IDictionary<GlobalId, IDictionary<GlobalId, object>> tableEntries = LocaleUtility.GetEntries(table);

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

        public static bool TryGetEntryNameFromCache(GlobalId id, out string name)
        {
            return LocaleEntriesCache.TryGetName(id, out name);
        }

        public static bool TryGetEntryNameFromAll(GlobalId id, out string name)
        {
            IReadOnlyList<LocaleTableAsset> tables = FindTableAssetAll();

            name = default;
            return tables.Count > 0 && TryGetEntryName(tables, id, out name);
        }

        public static bool TryGetEntryName(IReadOnlyList<LocaleTableAsset> tables, GlobalId id, out string name)
        {
            if (tables == null) throw new ArgumentNullException(nameof(tables));
            if (tables.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(tables));
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));

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

        public static bool TryGetEntryName(ILocaleTable table, GlobalId id, out string name)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));

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
