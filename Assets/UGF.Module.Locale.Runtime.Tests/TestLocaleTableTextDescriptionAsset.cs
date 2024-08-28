using System;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Locale.Runtime.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestLocaleTableTextDescriptionAsset")]
    public class TestLocaleTableTextDescriptionAsset : LocaleTableDescriptionAsset<TestLocaleTableTextEntry, string, TestLocaleTableTextEntryDescription>
    {
    }

    public readonly struct TestLocaleTableTextEntryDescription : ILocaleTableEntryDescription<string>
    {
        public string English { get; }
        public string French { get; }
        public string Chinese { get; }

        public TestLocaleTableTextEntryDescription(
            string english,
            string french,
            string chinese)
        {
            if (string.IsNullOrEmpty(english)) throw new ArgumentException("Value cannot be null or empty.", nameof(english));
            if (string.IsNullOrEmpty(french)) throw new ArgumentException("Value cannot be null or empty.", nameof(french));
            if (string.IsNullOrEmpty(chinese)) throw new ArgumentException("Value cannot be null or empty.", nameof(chinese));

            English = english;
            French = french;
            Chinese = chinese;
        }

        public bool TryGetValue(LocaleDescription locale, out string value)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            value = locale.SystemLanguage switch
            {
                SystemLanguage.Chinese => Chinese,
                SystemLanguage.English => English,
                SystemLanguage.French => French,
                _ => string.Empty
            };

            return !string.IsNullOrEmpty(value);
        }
    }

    [Serializable]
    public struct TestLocaleTableTextEntry : ILocaleTableEntry<TestLocaleTableTextEntryDescription, string>
    {
        [SerializeField] private Hash128 m_id;
        [SerializeField] private string m_name;
        [TextArea(5, 5)]
        [SerializeField] private string m_english;
        [TextArea(5, 5)]
        [SerializeField] private string m_french;
        [TextArea(5, 5)]
        [SerializeField] private string m_chinese;

        public GlobalId Id { get { return m_id; } set { m_id = value; } }
        public string Name { get { return m_name; } set { m_name = value; } }
        public string English { get { return m_english; } set { m_english = value; } }
        public string French { get { return m_french; } set { m_french = value; } }
        public string Chinese { get { return m_chinese; } set { m_chinese = value; } }

        public TestLocaleTableTextEntryDescription Build()
        {
            return new TestLocaleTableTextEntryDescription(
                m_english,
                m_french,
                m_chinese
            );
        }
    }
}
