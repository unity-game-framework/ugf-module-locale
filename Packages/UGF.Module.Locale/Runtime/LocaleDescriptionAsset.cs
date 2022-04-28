using System.Globalization;
using UGF.Builder.Runtime;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Locale/Locale Description", order = 2000)]
    public class LocaleDescriptionAsset : BuilderAsset<LocaleDescription>
    {
        [SerializeField] private string m_code;
        [SerializeField] private string m_name;
        [SerializeField] private SystemLanguage m_language = SystemLanguage.Unknown;
        [SerializeField] private string m_cultureName;

        public string Code { get { return m_code; } set { m_code = value; } }
        public string Name { get { return m_name; } set { m_name = value; } }
        public SystemLanguage Language { get { return m_language; } set { m_language = value; } }
        public string CultureName { get { return m_cultureName; } set { m_cultureName = value; } }

        protected override LocaleDescription OnBuild()
        {
            var description = new LocaleDescription
            {
                Code = m_code,
                Name = m_name,
                SystemLanguage = m_language,
                CultureInfo = string.IsNullOrEmpty(m_cultureName) ? CultureInfo.GetCultureInfo(m_cultureName) : null
            };

            return description;
        }
    }
}
