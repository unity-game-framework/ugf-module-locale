using System.Collections.Generic;
using System.Data;
using System.IO;
using UGF.Csv.Runtime;
using UGF.Module.Locale.Runtime;
using UGF.RuntimeTools.Editor.Tables;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    [ScriptedImporter(0, null, new[] { "csv" })]
    public class LocaleTableCsvAssetImporter : TableAssetImporter<LocaleTableAsset>
    {
        [SerializeField] private bool m_clearOnImport;
        [SerializeField] private bool m_useLocalesFromProjectSettings = true;
        [SerializeField] private List<LocaleDescriptionAsset> m_locales = new List<LocaleDescriptionAsset>();

        public bool ClearOnImport { get { return m_clearOnImport; } set { m_clearOnImport = value; } }
        public bool UseLocalesFromProjectSettings { get { return m_useLocalesFromProjectSettings; } set { m_useLocalesFromProjectSettings = value; } }
        public List<LocaleDescriptionAsset> Locales { get { return m_locales; } }

        public override void OnImportAsset(AssetImportContext context)
        {
            string csv = File.ReadAllText(assetPath);
            var asset = new TextAsset(csv);

            context.AddObjectToAsset("main", asset);
            context.SetMainObject(asset);

            EditorUtility.SetDirty(this);
        }

        protected override void OnImport()
        {
            string csv = File.ReadAllText(assetPath);
            DataTable data = CsvUtility.FromCsv(csv);

            List<LocaleDescriptionAsset> locales = m_useLocalesFromProjectSettings
                ? LocaleEditorSettings.Settings.GetData().Locales
                : m_locales;

            LocaleEditorUtility.UpdateFromDataTable(Table, data, locales, m_clearOnImport);
        }

        protected override void OnExport()
        {
            List<LocaleDescriptionAsset> locales = m_useLocalesFromProjectSettings
                ? LocaleEditorSettings.Settings.GetData().Locales
                : m_locales;

            DataTable data = LocaleEditorUtility.GetDataTable(Table, locales);
            string csv = CsvUtility.ToCsv(data);

            File.WriteAllText(assetPath, csv);
        }
    }
}
