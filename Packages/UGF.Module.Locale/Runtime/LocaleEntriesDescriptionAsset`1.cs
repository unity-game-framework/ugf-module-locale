using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleEntriesDescriptionAsset<TValue> : LocaleEntriesDescriptionAsset
    {
        [SerializeField] private List<Entry> m_entries = new List<Entry>();

        public List<Entry> Entries { get { return m_entries; } }

        [Serializable]
        public struct Entry
        {
            [SerializeField] private string m_key;
            [SerializeField] private TValue m_value;

            public string Key { get { return m_key; } set { m_key = value; } }
            public TValue Value { get { return m_value; } set { m_value = value; } }
        }

        protected override void OnCollect(IDictionary<string, object> values)
        {
            for (int i = 0; i < m_entries.Count; i++)
            {
                Entry entry = m_entries[i];

                values.Add(entry.Key, entry.Value);
            }
        }
    }
}
