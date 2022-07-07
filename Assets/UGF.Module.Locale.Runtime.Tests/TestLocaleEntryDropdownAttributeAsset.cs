﻿using UnityEngine;

namespace UGF.Module.Locale.Runtime.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestLocaleEntryDropdownAttributeAsset")]
    public class TestLocaleEntryDropdownAttributeAsset : ScriptableObject
    {
        [LocaleEntryDropdown]
        [SerializeField] private string m_locale1;
        [LocaleEntryDropdown]
        [SerializeField] private string m_locale2;
        [LocaleEntryDropdown]
        [SerializeField] private string m_locale3;
        [LocaleEntryDropdown]
        [SerializeField] private string m_locale4;
        [LocaleEntryDropdown]
        [SerializeField] private string m_locale5;
        [LocaleEntryDropdown]
        [SerializeField] private string m_locale6;

        public string Locale1 { get { return m_locale1; } set { m_locale1 = value; } }
        public string Locale2 { get { return m_locale2; } set { m_locale2 = value; } }
        public string Locale3 { get { return m_locale3; } set { m_locale3 = value; } }
        public string Locale4 { get { return m_locale4; } set { m_locale4 = value; } }
        public string Locale5 { get { return m_locale5; } set { m_locale5 = value; } }
        public string Locale6 { get { return m_locale6; } set { m_locale6 = value; } }
    }
}
