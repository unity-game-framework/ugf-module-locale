using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleConverterTableAsset), true)]
    internal class LocaleConverterTableAssetEditor : LocaleConverterAssetEditor
    {
        private SerializedProperty m_propertyTable;
        private LocaleKeyAndValueCollectionDrawer m_listEntries;
        private ReorderableListSelectionDrawerByPath m_listEntriesSelection;

        private void OnEnable()
        {
            m_propertyTable = serializedObject.FindProperty("m_table");
            m_listEntries = new LocaleKeyAndValueCollectionDrawer(serializedObject.FindProperty("m_entries"), "m_locale", "m_entries");

            m_listEntriesSelection = new ReorderableListSelectionDrawerByPath(m_listEntries, "m_entries")
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listEntries.Enable();
            m_listEntriesSelection.Enable();
        }

        private void OnDisable()
        {
            m_listEntries.Disable();
            m_listEntriesSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyTable);

                m_listEntries.DrawGUILayout();
                m_listEntriesSelection.DrawGUILayout();
            }

            DrawConvertControls();
        }
    }
}
