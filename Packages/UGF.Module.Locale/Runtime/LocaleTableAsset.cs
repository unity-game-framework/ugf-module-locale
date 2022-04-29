using System;
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

        public void Collect(IDictionary<string, IDictionary<string, object>> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var values = new Dictionary<string, object>();

            for (int i = 0; i < m_data.Count; i++)
            {
                AssetReference<LocaleDataAsset> reference = m_data[i];

                reference.Asset.Collect(values);

                foreach ((string localeId, object value) in values)
                {
                    if (!data.TryGetValue(localeId, out IDictionary<string, object> entries))
                    {
                        entries = new Dictionary<string, object>();

                        data.Add(localeId, entries);
                    }

                    entries.Add(reference.Guid, value);
                }
            }
        }
    }
}
