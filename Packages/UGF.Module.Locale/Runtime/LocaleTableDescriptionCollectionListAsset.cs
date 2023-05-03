using System.Collections.Generic;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Locale/Locale Table Description Collection List", order = 2000)]
    public class LocaleTableDescriptionCollectionListAsset : LocaleTableDescriptionCollectionAsset
    {
        [SerializeField] private List<AssetIdReference<LocaleTableDescriptionAsset>> m_tables = new List<AssetIdReference<LocaleTableDescriptionAsset>>();

        public List<AssetIdReference<LocaleTableDescriptionAsset>> Tables { get { return m_tables; } }

        protected override void OnGetTableDescriptions(IDictionary<GlobalId, LocaleTableDescription> descriptions)
        {
            for (int i = 0; i < m_tables.Count; i++)
            {
                AssetIdReference<LocaleTableDescriptionAsset> reference = m_tables[i];

                descriptions.Add(reference.Guid, reference.Asset.Build());
            }
        }
    }
}
