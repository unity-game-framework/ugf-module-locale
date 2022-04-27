using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Locale/Locale Table", order = 2000)]
    public class LocaleTableAsset : ScriptableObject
    {
        [SerializeField] private List<AssetReference<LocaleDataAsset>> m_data = new List<AssetReference<LocaleDataAsset>>();

        public List<AssetReference<LocaleDataAsset>> Data { get { return m_data; } }
    }
}
