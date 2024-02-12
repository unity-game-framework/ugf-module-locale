using UGF.EditorTools.Editor.IMGUI;
using UGF.RuntimeTools.Editor.Tables;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleTableCsvAssetImporter), true)]
    internal class LocaleTableCsvAssetImporterEditor : TableAssetImporterEditor
    {
        private SerializedProperty m_propertyClearOnImport;
        private SerializedProperty m_propertyUseLocalesFromProjectSettings;
        private ReorderableListDrawer m_listLocales;
        private ReorderableListSelectionDrawerByElement m_listLocalesSelection;

        public override void OnEnable()
        {
            base.OnEnable();

            m_propertyClearOnImport = serializedObject.FindProperty("m_clearOnImport");
            m_propertyUseLocalesFromProjectSettings = serializedObject.FindProperty("m_useLocalesFromProjectSettings");

            m_listLocales = new ReorderableListDrawer(serializedObject.FindProperty("m_locales"));

            m_listLocalesSelection = new ReorderableListSelectionDrawerByElement(m_listLocales)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listLocales.Enable();
            m_listLocalesSelection.Enable();
        }

        public override void OnDisable()
        {
            base.OnDisable();

            m_listLocales.Disable();
            m_listLocalesSelection.Disable();
        }

        protected override void OnDrawProperties()
        {
            base.OnDrawProperties();

            EditorGUILayout.PropertyField(m_propertyClearOnImport);
            EditorGUILayout.PropertyField(m_propertyUseLocalesFromProjectSettings);

            m_listLocales.DrawGUILayout();
            m_listLocalesSelection.DrawGUILayout();
        }
    }
}
