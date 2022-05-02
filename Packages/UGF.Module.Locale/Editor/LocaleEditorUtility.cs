using System;
using System.Collections.Generic;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    public static class LocaleEditorUtility
    {
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
