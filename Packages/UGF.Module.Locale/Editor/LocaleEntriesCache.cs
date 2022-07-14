using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [InitializeOnLoad]
    internal static class LocaleEntriesCache
    {
        public static int Count { get { return m_entries.Count; } }

        private static readonly Dictionary<GlobalId, EntryData> m_entries = new Dictionary<GlobalId, EntryData>();

        public struct EntryData
        {
            public string Name { get; }

            public EntryData(string name)
            {
                if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

                Name = name;
            }
        }

        static LocaleEntriesCache()
        {
            Update();
        }

        public static void Update()
        {
            Update(LocaleEditorUtility.FindTableAssetAll());
        }

        public static void Update(IReadOnlyList<LocaleTableAsset> tables)
        {
            if (tables == null) throw new ArgumentNullException(nameof(tables));

            m_entries.Clear();

            for (int i = 0; i < tables.Count; i++)
            {
                LocaleTableAsset tableAsset = tables[i];
                ILocaleTable table = tableAsset.GetTable();

                foreach (ILocaleTableEntry entry in table.Entries)
                {
                    var data = new EntryData(entry.Name);

                    if (entry.Id.IsValid())
                    {
                        m_entries.TryAdd(entry.Id, data);
                    }
                }
            }
        }

        public static void Clear()
        {
            m_entries.Clear();
        }

        public static bool TryGetName(GlobalId id, out string name)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));

            if (m_entries.TryGetValue(id, out EntryData data))
            {
                name = data.Name;
                return true;
            }

            name = default;
            return false;
        }
    }
}
