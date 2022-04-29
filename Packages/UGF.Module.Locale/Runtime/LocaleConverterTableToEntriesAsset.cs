using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Locale/Locale Converter Table to Entries", order = 2000)]
    public class LocaleConverterTableToEntriesAsset : LocaleConverterAsset
    {
        [SerializeField] private LocaleTableAsset m_table;
        [SerializeField] private List<Entry> m_entries = new List<Entry>();

        public LocaleTableAsset Table { get { return m_table; } set { m_table = value; } }
        public List<Entry> Entries { get { return m_entries; } }

        [Serializable]
        public struct Entry
        {
            [AssetGuid(typeof(LocaleDescriptionAsset))]
            [SerializeField] private string m_locale;
            [SerializeField] private LocaleEntriesDescriptionAsset m_entries;

            public string Locale { get { return m_locale; } set { m_locale = value; } }
            public LocaleEntriesDescriptionAsset Entries { get { return m_entries; } set { m_entries = value; } }
        }

        protected override void OnConvert()
        {
            var data = new Dictionary<string, IDictionary<string, object>>();

            m_table.Collect(data);

            for (int i = 0; i < m_entries.Count; i++)
            {
                Entry entry = m_entries[i];

                if (data.TryGetValue(entry.Locale, out IDictionary<string, object> values))
                {
                    entry.Entries.Setup(values);

#if UNITY_EDITOR
                    UnityEditor.EditorUtility.SetDirty(entry.Entries);
#endif
                }
            }
        }
    }
}
