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

        protected override LocaleEntriesDescription OnBuild()
        {
            var description = new LocaleEntriesDescription();

            for (int i = 0; i < m_entries.Count; i++)
            {
                Entry entry = m_entries[i];
                object value = entry.Value;

                if (string.IsNullOrEmpty(entry.Key)) throw new ArgumentException("Value cannot be null or empty.", nameof(entry.Key));
                if (value == null) throw new ArgumentNullException(nameof(value));

                description.Values.Add(entry.Key, value);
            }

            return description;
        }
    }
}
