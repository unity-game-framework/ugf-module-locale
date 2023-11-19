using System.Collections.Generic;
using System.Data;
using System.IO;
using UGF.Csv.Runtime;
using UGF.Module.Locale.Runtime;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    [ScriptedImporter(0, null, new[] { "csv" })]
    public class LocaleTableCsvAssetImporter : ScriptedImporter
    {
        [SerializeField] private bool m_useLocalesFromProjectSettings = true;
        [SerializeField] private List<LocaleDescriptionAsset> m_locales = new List<LocaleDescriptionAsset>();
        [SerializeField] private bool m_tableClearOnImport;
        [SerializeField] private LocaleTable<string> m_table = new LocaleTable<string>();

        public bool UseLocalesFromProjectSettings { get { return m_useLocalesFromProjectSettings; } set { m_useLocalesFromProjectSettings = value; } }
        public List<LocaleDescriptionAsset> Locales { get { return m_locales; } }
        public bool TableClearOnImport { get { return m_tableClearOnImport; } set { m_tableClearOnImport = value; } }
        public LocaleTable<string> Table { get { return m_table; } }

        public override void OnImportAsset(AssetImportContext context)
        {
            string csv = File.ReadAllText(assetPath);
            DataTable data = CsvUtility.FromCsv(csv);

            List<LocaleDescriptionAsset> locales = m_useLocalesFromProjectSettings
                ? LocaleEditorSettings.Settings.GetData().Locales
                : m_locales;

            if (m_tableClearOnImport)
            {
                m_table.Entries.Clear();
            }

            LocaleEditorUtility.UpdateFromDataTable(m_table, data, locales);

            var tableAsset = ObjectFactory.CreateInstance<LocaleTableTextAsset>();

            tableAsset.Table.Entries.AddRange(m_table.Entries);

            context.AddObjectToAsset("main", tableAsset);
            context.SetMainObject(tableAsset);

            EditorUtility.SetDirty(this);
        }
    }
}
