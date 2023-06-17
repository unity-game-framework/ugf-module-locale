using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [Serializable]
    public class LocaleTableEntry<TValue> : ILocaleTableEntry
    {
        [SerializeField] private GlobalId m_id;
        [SerializeField] private string m_name;
        [SerializeField] private List<LocaleTableEntryValue<TValue>> m_values = new List<LocaleTableEntryValue<TValue>>();

        public GlobalId Id { get { return m_id; } set { m_id = value; } }
        public string Name { get { return m_name; } set { m_name = value; } }
        public List<LocaleTableEntryValue<TValue>> Values { get { return m_values; } }

        IEnumerable<ILocaleTableEntryValue> ILocaleTableEntry.Values { get { return m_values; } }

        public bool TryGet<T>(GlobalId localeId, out T value) where T : class, ILocaleTableEntryValue
        {
            if (TryGet(localeId, out ILocaleTableEntryValue result))
            {
                value = (T)result;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGet(GlobalId localeId, out ILocaleTableEntryValue value)
        {
            for (int i = 0; i < m_values.Count; i++)
            {
                value = m_values[i];

                if (value.LocaleId == localeId)
                {
                    return true;
                }
            }

            value = default;
            return false;
        }
    }
}
