using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleEditorSettingsData), true)]
    internal class LocaleEditorSettingsDataEditor : UnityEditor.Editor
    {
        private ReorderableListKeyAndValueDrawer m_listTables;
        private ReorderableListSelectionDrawer m_listTablesSelection;

        private void OnEnable()
        {
            m_listTables = new ReorderableListKeyAndValueDrawer(serializedObject.FindProperty("m_tables"), "m_table", "m_description");

            m_listTablesSelection = new ReorderableListSelectionDrawerByPath(m_listTables, "m_description")
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listTables.Enable();
            m_listTablesSelection.Enable();
        }

        private void OnDisable()
        {
            m_listTables.Disable();
            m_listTablesSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listTables.DrawGUILayout();
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new EditorGUI.DisabledScope(m_listTables.List.selectedIndices.Count == 0))
                {
                    if (GUILayout.Button("Update"))
                    {
                        OnUpdateSelected();
                    }
                }

                using (new EditorGUI.DisabledScope(m_listTables.List.count == 0))
                {
                    if (GUILayout.Button("Update All"))
                    {
                        OnUpdateAll();
                    }
                }

                EditorGUILayout.Space();
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            m_listTablesSelection.DrawGUILayout();
        }

        private void OnUpdateSelected()
        {
            foreach (int index in m_listTables.List.selectedIndices)
            {
                LocaleEditorSettings.UpdateTable(index);
            }
        }

        private void OnUpdateAll()
        {
            LocaleEditorSettings.UpdateTableAll();
        }
    }
}
