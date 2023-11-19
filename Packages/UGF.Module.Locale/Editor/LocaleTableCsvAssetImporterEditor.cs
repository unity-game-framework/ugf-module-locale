using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.RuntimeTools.Editor.Tables;
using UnityEditor;
using UnityEditor.AssetImporters;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleTableCsvAssetImporter), true)]
    internal class LocaleTableCsvAssetImporterEditor : ScriptedImporterEditor
    {
        private SerializedProperty m_propertyUseLocalesFromProjectSettings;
        private ReorderableListDrawer m_listLocales;
        private ReorderableListSelectionDrawerByElement m_listLocalesSelection;
        private SerializedProperty m_propertyTableClearOnImport;
        private TableDrawer m_drawerTable;

        public override void OnEnable()
        {
            base.OnEnable();

            m_propertyUseLocalesFromProjectSettings = serializedObject.FindProperty("m_useLocalesFromProjectSettings");

            m_listLocales = new ReorderableListDrawer(serializedObject.FindProperty("m_locales"));

            m_listLocalesSelection = new ReorderableListSelectionDrawerByElement(m_listLocales)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_propertyTableClearOnImport = serializedObject.FindProperty("m_tableClearOnImport");

            m_drawerTable = new LocaleTableDrawer(serializedObject.FindProperty("m_table"));

            m_listLocales.Enable();
            m_listLocalesSelection.Enable();
            m_drawerTable.Enable();
        }

        public override void OnDisable()
        {
            base.OnDisable();

            m_listLocales.Disable();
            m_listLocalesSelection.Disable();
            m_drawerTable.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyUseLocalesFromProjectSettings);

                m_listLocales.DrawGUILayout();

                EditorGUILayout.PropertyField(m_propertyTableClearOnImport);
            }

            m_drawerTable.DrawGUILayout();
            m_listLocalesSelection.DrawGUILayout();

            ApplyRevertGUI();
        }
    }
}
