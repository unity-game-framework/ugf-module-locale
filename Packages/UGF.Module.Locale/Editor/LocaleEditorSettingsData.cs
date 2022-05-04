using System;
using System.Collections.Generic;
using UGF.CustomSettings.Runtime;
using UGF.Module.Locale.Runtime;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    public class LocaleEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private List<TableEntry> m_tables = new List<TableEntry>();

        public List<TableEntry> Tables { get { return m_tables; } }

        [Serializable]
        public class TableEntry
        {
            [SerializeField] private LocaleTableAsset m_table;
            [SerializeField] private LocaleTableDescriptionAsset m_description;

            public LocaleTableAsset Table { get { return m_table; } set { m_table = value; } }
            public LocaleTableDescriptionAsset Description { get { return m_description; } set { m_description = value; } }
        }
    }
}
