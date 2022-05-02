using System.Collections.Generic;
using UGF.CustomSettings.Runtime;
using UGF.Module.Locale.Runtime;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    public class LocaleEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private List<LocaleConverterAsset> m_converters = new List<LocaleConverterAsset>();

        public List<LocaleConverterAsset> Converters { get { return m_converters; } }
    }
}
