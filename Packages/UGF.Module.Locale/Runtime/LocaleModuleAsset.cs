using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Descriptions.Runtime;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Locale/Locale Module", order = 2000)]
    public class LocaleModuleAsset : ApplicationModuleAsset<LocaleModule, LocaleModuleDescription>
    {
        [AssetId(typeof(LocaleDescriptionAsset))]
        [SerializeField] private Hash128 m_defaultLocale;
        [SerializeField] private bool m_selectLocaleBySystemLanguageOnInitialize;
        [SerializeField] private bool m_unloadEntriesOnUninitialize = true;
        [SerializeField] private List<AssetIdReference<LocaleDescriptionAsset>> m_locales = new List<AssetIdReference<LocaleDescriptionAsset>>();
        [SerializeField] private List<AssetIdReference<LocaleTableDescriptionAsset>> m_tables = new List<AssetIdReference<LocaleTableDescriptionAsset>>();
        [AssetId(typeof(LocaleTableDescriptionAsset))]
        [SerializeField] private List<Hash128> m_preloadTablesAsync = new List<Hash128>();

        public GlobalId DefaultLocale { get { return m_defaultLocale; } set { m_defaultLocale = value; } }
        public bool SelectLocaleBySystemLanguageOnInitialize { get { return m_selectLocaleBySystemLanguageOnInitialize; } set { m_selectLocaleBySystemLanguageOnInitialize = value; } }
        public bool UnloadEntriesOnUninitialize { get { return m_unloadEntriesOnUninitialize; } set { m_unloadEntriesOnUninitialize = value; } }
        public List<AssetIdReference<LocaleDescriptionAsset>> Locales { get { return m_locales; } }
        public List<AssetIdReference<LocaleTableDescriptionAsset>> Tables { get { return m_tables; } }
        public List<Hash128> PreloadTablesAsync { get { return m_preloadTablesAsync; } }

        protected override LocaleModuleDescription OnBuildDescription()
        {
            var locales = new Dictionary<GlobalId, LocaleDescription>();
            var tables = new Dictionary<GlobalId, IDescriptionTable>();
            var preloadTableAsync = new List<GlobalId>();

            for (int i = 0; i < m_locales.Count; i++)
            {
                AssetIdReference<LocaleDescriptionAsset> reference = m_locales[i];

                locales.Add(reference.Guid, reference.Asset.Build());
            }

            for (int i = 0; i < m_tables.Count; i++)
            {
                AssetIdReference<LocaleTableDescriptionAsset> reference = m_tables[i];

                tables.Add(reference.Guid, reference.Asset.Build());
            }

            for (int i = 0; i < m_preloadTablesAsync.Count; i++)
            {
                GlobalId id = m_preloadTablesAsync[i];

                if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));

                preloadTableAsync.Add(id);
            }

            return new LocaleModuleDescription(
                m_defaultLocale,
                m_selectLocaleBySystemLanguageOnInitialize,
                locales,
                tables,
                preloadTableAsync
            );
        }

        protected override LocaleModule OnBuild(LocaleModuleDescription description, IApplication application)
        {
            return new LocaleModule(description, application);
        }
    }
}
