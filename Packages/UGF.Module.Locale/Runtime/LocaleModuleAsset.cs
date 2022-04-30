using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Locale/Locale Module", order = 2000)]
    public class LocaleModuleAsset : ApplicationModuleAsset<LocaleModule, LocaleModuleDescription>
    {
        [AssetGuid(typeof(LocaleDescriptionAsset))]
        [SerializeField] private string m_defaultLocale;
        [SerializeField] private bool m_unloadEntriesOnUninitialize = true;
        [SerializeField] private List<AssetReference<LocaleDescriptionAsset>> m_locales = new List<AssetReference<LocaleDescriptionAsset>>();
        [SerializeField] private List<AssetReference<LocaleEntriesDescriptionAsset>> m_entries = new List<AssetReference<LocaleEntriesDescriptionAsset>>();
        [SerializeField] private List<AssetReference<LocaleGroupDescriptionAsset>> m_groups = new List<AssetReference<LocaleGroupDescriptionAsset>>();
        [AssetGuid(typeof(LocaleEntriesDescriptionAsset))]
        [SerializeField] private List<string> m_preloadEntries = new List<string>();
        [AssetGuid(typeof(LocaleGroupDescriptionAsset))]
        [SerializeField] private List<string> m_preloadGroups = new List<string>();

        public string DefaultLocale { get { return m_defaultLocale; } set { m_defaultLocale = value; } }
        public bool UnloadEntriesOnUninitialize { get { return m_unloadEntriesOnUninitialize; } set { m_unloadEntriesOnUninitialize = value; } }
        public List<AssetReference<LocaleDescriptionAsset>> Locales { get { return m_locales; } }
        public List<AssetReference<LocaleEntriesDescriptionAsset>> Entries { get { return m_entries; } }
        public List<AssetReference<LocaleGroupDescriptionAsset>> Groups { get { return m_groups; } }
        public List<string> PreloadEntries { get { return m_preloadEntries; } }
        public List<string> PreloadGroups { get { return m_preloadGroups; } }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new LocaleModuleDescription
            {
                RegisterType = typeof(LocaleModule),
                DefaultLocaleId = m_defaultLocale,
                UnloadEntriesOnUninitialize = m_unloadEntriesOnUninitialize
            };

            for (int i = 0; i < m_locales.Count; i++)
            {
                AssetReference<LocaleDescriptionAsset> reference = m_locales[i];

                description.Locales.Add(reference.Guid, reference.Asset.Build());
            }

            for (int i = 0; i < m_entries.Count; i++)
            {
                AssetReference<LocaleEntriesDescriptionAsset> reference = m_entries[i];

                description.Entries.Add(reference.Guid, reference.Asset.Build());
            }

            for (int i = 0; i < m_groups.Count; i++)
            {
                AssetReference<LocaleGroupDescriptionAsset> reference = m_groups[i];

                description.Groups.Add(reference.Guid, reference.Asset.Build());
            }

            for (int i = 0; i < m_preloadEntries.Count; i++)
            {
                string id = m_preloadEntries[i];

                if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty/", nameof(id));

                description.PreloadEntries.Add(id);
            }

            for (int i = 0; i < m_preloadGroups.Count; i++)
            {
                string id = m_preloadGroups[i];

                if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty/", nameof(id));

                description.PreloadGroups.Add(id);
            }

            return description;
        }

        protected override LocaleModule OnBuild(LocaleModuleDescription description, IApplication application)
        {
            return new LocaleModule(description, application);
        }
    }
}
