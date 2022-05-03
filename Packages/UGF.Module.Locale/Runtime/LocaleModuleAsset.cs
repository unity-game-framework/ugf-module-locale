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
        [SerializeField] private List<AssetReference<LocaleTableDescriptionAsset>> m_tables = new List<AssetReference<LocaleTableDescriptionAsset>>();
        [AssetGuid(typeof(LocaleTableDescriptionAsset))]
        [SerializeField] private List<string> m_preloadTablesAsync = new List<string>();

        public string DefaultLocale { get { return m_defaultLocale; } set { m_defaultLocale = value; } }
        public bool UnloadEntriesOnUninitialize { get { return m_unloadEntriesOnUninitialize; } set { m_unloadEntriesOnUninitialize = value; } }
        public List<AssetReference<LocaleDescriptionAsset>> Locales { get { return m_locales; } }
        public List<AssetReference<LocaleTableDescriptionAsset>> Tables { get { return m_tables; } }
        public List<string> PreloadTablesAsync { get { return m_preloadTablesAsync; } }

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

                description.Locales.Add(reference.Guid, reference.Asset);
            }

            for (int i = 0; i < m_tables.Count; i++)
            {
                AssetReference<LocaleTableDescriptionAsset> reference = m_tables[i];

                description.Tables.Add(reference.Guid, reference.Asset);
            }

            for (int i = 0; i < m_preloadTablesAsync.Count; i++)
            {
                string id = m_preloadTablesAsync[i];

                if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

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
