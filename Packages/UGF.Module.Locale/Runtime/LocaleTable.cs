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
    }
}
