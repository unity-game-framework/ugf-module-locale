using System;
using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Locale/Locale Table Description", order = 2000)]
    public class LocaleTableDescriptionAsset : BuilderAsset<LocaleTableDescription>
    {
        [SerializeField] private List<Entry> m_entries = new List<Entry>();

        public List<Entry> Entries { get { return m_entries; } }

        [Serializable]
        public struct Entry
        {
            [AssetGuid(typeof(LocaleDescriptionAsset))]
            [SerializeField] private string m_locale;
            [AssetGuid(typeof(LocaleEntriesDescriptionAsset))]
            [SerializeField] private string m_entries;

            public string Locale { get { return m_locale; } set { m_locale = value; } }
            public string Entries { get { return m_entries; } set { m_entries = value; } }
        }

        protected override LocaleTableDescription OnBuild()
        {
            var description = new LocaleTableDescription();

            for (int i = 0; i < m_entries.Count; i++)
            {
                Entry entry = m_entries[i];

                if (string.IsNullOrEmpty(entry.Locale)) throw new ArgumentException("Value cannot be null or empty.", nameof(entry.Locale));
                if (string.IsNullOrEmpty(entry.Entries)) throw new ArgumentException("Value cannot be null or empty.", nameof(entry.Entries));

                description.Entries.Add(entry.Locale, entry.Entries);
            }

            return description;
        }
    }
}
