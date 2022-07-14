using System;
using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
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
            [AssetId(typeof(LocaleDescriptionAsset))]
            [SerializeField] private GlobalId m_locale;
            [AssetId(typeof(LocaleEntriesDescriptionAsset))]
            [SerializeField] private GlobalId m_entries;

            public GlobalId Locale { get { return m_locale; } set { m_locale = value; } }
            public GlobalId Entries { get { return m_entries; } set { m_entries = value; } }
        }

        protected override LocaleTableDescription OnBuild()
        {
            var description = new LocaleTableDescription();

            for (int i = 0; i < m_entries.Count; i++)
            {
                Entry entry = m_entries[i];

                if (!entry.Locale.IsValid()) throw new ArgumentException("Value should be valid.", nameof(entry.Locale));
                if (!entry.Entries.IsValid()) throw new ArgumentException("Value should be valid.", nameof(entry.Entries));

                description.Entries.Add(entry.Locale, entry.Entries);
            }

            return description;
        }
    }
}
