using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [Serializable]
    public class LocaleTable<TValue> : ILocaleTable
    {
        [SerializeField] private List<LocaleTableEntry<TValue>> m_entries = new List<LocaleTableEntry<TValue>>();

        public List<LocaleTableEntry<TValue>> Entries { get { return m_entries; } }

        IEnumerable<ILocaleTableEntry> ILocaleTable.Entries { get { return m_entries; } }

        public void Add(ILocaleTableEntry entry)
        {
            m_entries.Add((LocaleTableEntry<TValue>)entry);
        }

        public bool Remove(ILocaleTableEntry entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));

            return m_entries.Remove((LocaleTableEntry<TValue>)entry);
        }

        public void Clear()
        {
            m_entries.Clear();
        }
    }
}
