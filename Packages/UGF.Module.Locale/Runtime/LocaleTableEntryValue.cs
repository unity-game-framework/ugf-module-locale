using System;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [Serializable]
    public class LocaleTableEntryValue<TValue> : ILocaleTableEntryValue
    {
        [SerializeField] private GlobalId m_locale;
        [SerializeField] private TValue m_value;

        public GlobalId LocaleId { get { return m_locale; } set { m_locale = value; } }
        public TValue Value { get { return m_value; } set { m_value = value; } }

        object ILocaleTableEntryValue.Value { get { return m_value; } }
    }
}
