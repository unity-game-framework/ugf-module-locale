using System;
using System.Collections.Generic;
using UGF.CustomSettings.Runtime;
using UGF.Module.Locale.Runtime;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    public class LocaleEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private List<LocaleDescriptionAsset> m_locales = new List<LocaleDescriptionAsset>();
        [SerializeField] private List<TableEntry> m_tables = new List<TableEntry>();
        [SerializeField] private List<CsvEntry> m_csv = new List<CsvEntry>();

        public List<LocaleDescriptionAsset> Locales { get { return m_locales; } }
        public List<TableEntry> Tables { get { return m_tables; } }
        public List<CsvEntry> Csv { get { return m_csv; } }

        [Serializable]
        public class TableEntry
        {
            [SerializeField] private LocaleTableAsset m_table;
            [SerializeField] private LocaleTableDescriptionAsset m_description;

            public LocaleTableAsset Table { get { return m_table; } set { m_table = value; } }
            public LocaleTableDescriptionAsset Description { get { return m_description; } set { m_description = value; } }
        }
        
        [Serializable]
        public class CsvEntry
        {
            [SerializeField] private LocaleTableTextAsset m_table;
            [SerializeField] private TextAsset m_csv;

            public LocaleTableTextAsset Table { get { return m_table; } set { m_table = value; } }
            public TextAsset Csv { get { return m_csv; } set { m_csv = value; } }
        }
    }
}
