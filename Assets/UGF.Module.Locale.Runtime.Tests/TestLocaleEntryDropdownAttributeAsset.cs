using UGF.EditorTools.Runtime.Ids;
using UGF.Tables.Runtime;
using UnityEngine;

namespace UGF.Module.Locale.Runtime.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestLocaleEntryDropdownAttributeAsset")]
    public class TestLocaleEntryDropdownAttributeAsset : ScriptableObject
    {
        [TableEntryDropdown(typeof(LocaleTableDescriptionAsset))]
        [SerializeField] private Hash128 m_locale1;
        [LocaleEntryDropdown]
        [SerializeField] private Hash128 m_locale2;
        [LocaleEntryDropdown]
        [SerializeField] private Hash128 m_locale3;
        [LocaleEntryDropdown]
        [SerializeField] private Hash128 m_locale4;
        [LocaleEntryDropdown]
        [SerializeField] private Hash128 m_locale5;
        [LocaleEntryDropdown]
        [SerializeField] private Hash128 m_locale6;

        public GlobalId Locale1 { get { return m_locale1; } set { m_locale1 = value; } }
        public GlobalId Locale2 { get { return m_locale2; } set { m_locale2 = value; } }
        public GlobalId Locale3 { get { return m_locale3; } set { m_locale3 = value; } }
        public GlobalId Locale4 { get { return m_locale4; } set { m_locale4 = value; } }
        public GlobalId Locale5 { get { return m_locale5; } set { m_locale5 = value; } }
        public GlobalId Locale6 { get { return m_locale6; } set { m_locale6 = value; } }
    }
}
