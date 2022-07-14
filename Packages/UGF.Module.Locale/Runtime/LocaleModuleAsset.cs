using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Locale/Locale Module", order = 2000)]
    public class LocaleModuleAsset : ApplicationModuleAsset<LocaleModule, LocaleModuleDescription>
    {
        [AssetId(typeof(LocaleDescriptionAsset))]
        [SerializeField] private GlobalId m_defaultLocale;
        [SerializeField] private bool m_unloadEntriesOnUninitialize = true;
        [SerializeField] private List<AssetIdReference<LocaleDescriptionAsset>> m_locales = new List<AssetIdReference<LocaleDescriptionAsset>>();
        [SerializeField] private List<AssetIdReference<LocaleTableDescriptionAsset>> m_tables = new List<AssetIdReference<LocaleTableDescriptionAsset>>();
        [AssetId(typeof(LocaleTableDescriptionAsset))]
        [SerializeField] private List<GlobalId> m_preloadTablesAsync = new List<GlobalId>();

        public GlobalId DefaultLocale { get { return m_defaultLocale; } set { m_defaultLocale = value; } }
        public bool UnloadEntriesOnUninitialize { get { return m_unloadEntriesOnUninitialize; } set { m_unloadEntriesOnUninitialize = value; } }
        public List<AssetIdReference<LocaleDescriptionAsset>> Locales { get { return m_locales; } }
        public List<AssetIdReference<LocaleTableDescriptionAsset>> Tables { get { return m_tables; } }
        public List<GlobalId> PreloadTablesAsync { get { return m_preloadTablesAsync; } }

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
                AssetIdReference<LocaleDescriptionAsset> reference = m_locales[i];

                description.Locales.Add(reference.Guid, reference.Asset);
            }

            for (int i = 0; i < m_tables.Count; i++)
            {
                AssetIdReference<LocaleTableDescriptionAsset> reference = m_tables[i];

                description.Tables.Add(reference.Guid, reference.Asset);
            }

            for (int i = 0; i < m_preloadTablesAsync.Count; i++)
            {
                GlobalId id = m_preloadTablesAsync[i];

                if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));

                description.PreloadTablesAsync.Add(id);
            }

            return description;
        }

        protected override LocaleModule OnBuild(LocaleModuleDescription description, IApplication application)
        {
            return new LocaleModule(description, application);
        }
    }
}
