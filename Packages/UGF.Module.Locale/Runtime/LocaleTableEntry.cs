using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [Serializable]
    public class LocaleTableEntry<TValue> : ILocaleTableEntry
    {
        [SerializeField] private string m_id;
        [SerializeField] private string m_name;
        [SerializeField] private List<LocaleTableEntryValue<TValue>> m_values = new List<LocaleTableEntryValue<TValue>>();

        public string Id { get { return m_id; } set { m_id = value; } }
        public string Name { get { return m_name; } set { m_name = value; } }
        public List<LocaleTableEntryValue<TValue>> Values { get { return m_values; } }

        public IEnumerable<string> Locales
        {
            get
            {
                for (int i = 0; i < m_values.Count; i++)
                {
                    yield return m_values[i].Locale;
                }
            }
        }

        public bool TryGetValue(string localeId, out object value)
        {
            if (string.IsNullOrEmpty(localeId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeId));

            for (int i = 0; i < m_values.Count; i++)
            {
                LocaleTableEntryValue<TValue> entryValue = m_values[i];

                if (entryValue.Locale == localeId)
                {
                    value = entryValue.Value;
                    return true;
                }
            }

            value = default;
            return false;
        }
    }
}
