using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleDataAsset<TValue> : LocaleDataAsset
    {
        [SerializeField] private List<Entry> m_entries = new List<Entry>();

        public List<Entry> Entries { get { return m_entries; } }

        [Serializable]
        public struct Entry
        {
            [AssetGuid(typeof(LocaleDescriptionAsset))]
            [SerializeField] private string m_locale;
            [SerializeField] private TValue m_value;

            public string Locale { get { return m_locale; } set { m_locale = value; } }
            public TValue Value { get { return m_value; } set { m_value = value; } }
        }

        protected override void OnCollect(IDictionary<string, object> values)
        {
            for (int i = 0; i < m_entries.Count; i++)
            {
                Entry entry = m_entries[i];

                values.Add(entry.Locale, entry.Value);
            }
        }
    }
}
