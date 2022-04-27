using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Locale/Locale Module", order = 2000)]
    public class LocaleModuleAsset : ApplicationModuleAsset<LocaleModule, LocaleModuleDescription>
    {
        [SerializeField] private List<AssetReference<LocaleDescriptionAsset>> m_locales = new List<AssetReference<LocaleDescriptionAsset>>();
        [SerializeField] private List<AssetReference<LocaleEntriesDescriptionAsset>> m_entries = new List<AssetReference<LocaleEntriesDescriptionAsset>>();

        public List<AssetReference<LocaleDescriptionAsset>> Locales { get { return m_locales; } }
        public List<AssetReference<LocaleEntriesDescriptionAsset>> Entries { get { return m_entries; } }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new LocaleModuleDescription();

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

            return description;
        }

        protected override LocaleModule OnBuild(LocaleModuleDescription description, IApplication application)
        {
            return new LocaleModule(description, application);
        }
    }
}
