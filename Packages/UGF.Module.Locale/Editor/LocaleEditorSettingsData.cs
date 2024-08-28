using System.Collections.Generic;
using UGF.CustomSettings.Runtime;
using UGF.Module.Locale.Runtime;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    public class LocaleEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private List<LocaleDescriptionAsset> m_locales = new List<LocaleDescriptionAsset>();

        public List<LocaleDescriptionAsset> Locales { get { return m_locales; } }
    }
}
