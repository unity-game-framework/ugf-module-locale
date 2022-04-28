using System.Globalization;
using UGF.Builder.Runtime;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Locale/Locale Description", order = 2000)]
    public class LocaleDescriptionAsset : BuilderAsset<LocaleDescription>
    {
        [SerializeField] private string m_displayName;
        [SerializeField] private SystemLanguage m_language = SystemLanguage.Unknown;
        [LocaleCultureNameDropdown]
        [SerializeField] private string m_cultureName;

        public string DisplayName { get { return m_displayName; } set { m_displayName = value; } }
        public SystemLanguage Language { get { return m_language; } set { m_language = value; } }
        public string CultureName { get { return m_cultureName; } set { m_cultureName = value; } }

        protected override LocaleDescription OnBuild()
        {
            var description = new LocaleDescription
            {
                DisplayName = m_displayName,
                SystemLanguage = m_language,
                CultureInfo = CultureInfo.GetCultureInfo(m_cultureName)
            };

            return description;
        }
    }
}
