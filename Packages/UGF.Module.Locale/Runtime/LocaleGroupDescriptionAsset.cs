﻿using System;
using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Locale/Locale Group Description", order = 2000)]
    public class LocaleGroupDescriptionAsset : BuilderAsset<LocaleGroupDescription>
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

        protected override LocaleGroupDescription OnBuild()
        {
            var description = new LocaleGroupDescription();

            for (int i = 0; i < m_entries.Count; i++)
            {
                Entry entry = m_entries[i];

                if (!description.Entries.TryGetValue(entry.Locale, out HashSet<string> collection))
                {
                    collection = new HashSet<string>();

                    description.Entries.Add(entry.Locale, collection);
                }

                collection.Add(entry.Entries);
            }

            return description;
        }
    }
}
