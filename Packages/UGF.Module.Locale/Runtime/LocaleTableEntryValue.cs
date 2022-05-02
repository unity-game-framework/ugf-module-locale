using System;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [Serializable]
    public struct LocaleTableEntryValue<TValue>
    {
        [SerializeField] private string m_key;
        [SerializeField] private TValue m_value;

        public string Key { get { return m_key; } set { m_key = value; } }
        public TValue Value { get { return m_value; } set { m_value = value; } }
    }
}
