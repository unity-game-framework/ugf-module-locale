using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleEditorSettingsData), true)]
    internal class LocaleEditorSettingsDataEditor : UnityEditor.Editor
    {
        private ReorderableListDrawer m_listLocales;
        private ReorderableListSelectionDrawerByElement m_listLocalesSelection;
        private ReorderableListKeyAndValueDrawer m_listTables;
        private ReorderableListSelectionDrawer m_listTablesSelectionTable;
        private ReorderableListSelectionDrawer m_listTablesSelectionDescription;
        private ReorderableListKeyAndValueDrawer m_listCsv;
        private ReorderableListSelectionDrawer m_listCsvSelection;

        private void OnEnable()
        {
            m_listLocales = new ReorderableListDrawer(serializedObject.FindProperty("m_locales"));

            m_listLocalesSelection = new ReorderableListSelectionDrawerByElement(m_listLocales)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listTables = new ReorderableListKeyAndValueDrawer(serializedObject.FindProperty("m_tables"), "m_table", "m_description");

            m_listTablesSelectionTable = new ReorderableListSelectionDrawerByPath(m_listTables, "m_table")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listTablesSelectionDescription = new ReorderableListSelectionDrawerByPath(m_listTables, "m_description")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listCsv = new ReorderableListKeyAndValueDrawer(serializedObject.FindProperty("m_csv"), "m_table", "m_csv");

            m_listCsvSelection = new ReorderableListSelectionDrawerByPath(m_listCsv, "m_table")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listLocales.Enable();
            m_listLocalesSelection.Enable();
            m_listTables.Enable();
            m_listTablesSelectionTable.Enable();
            m_listTablesSelectionDescription.Enable();
            m_listCsv.Enable();
            m_listCsvSelection.Enable();
        }

        private void OnDisable()
        {
            m_listLocales.Disable();
            m_listLocalesSelection.Disable();
            m_listTables.Disable();
            m_listTablesSelectionTable.Disable();
            m_listTablesSelectionDescription.Disable();
            m_listCsv.Disable();
            m_listCsvSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                m_listLocales.DrawGUILayout();
                m_listTables.DrawGUILayout();
                m_listCsv.DrawGUILayout();
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new EditorGUI.DisabledScope(m_listCsv.List.selectedIndices.Count == 0 || m_listLocales.SerializedProperty.arraySize == 0))
                {
                    if (GUILayout.Button("Import"))
                    {
                        OnImport();
                    }

                    if (GUILayout.Button("Export"))
                    {
                        OnExport();
                    }
                }

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

            if (m_listCsv.SerializedProperty.arraySize > 0 && m_listLocales.SerializedProperty.arraySize == 0)
            {
                EditorGUILayout.HelpBox("Add locales in order to export or import csv files.", MessageType.Info);
            }

            EditorGUILayout.Space();

            m_listLocalesSelection.DrawGUILayout();
            m_listTablesSelectionTable.DrawGUILayout();
            m_listTablesSelectionDescription.DrawGUILayout();
            m_listCsvSelection.DrawGUILayout();
        }

        private void OnImport()
        {
            foreach (int index in m_listCsv.List.selectedIndices)
            {
                LocaleEditorSettings.CsvImport(index);
            }
        }

        private void OnExport()
        {
            foreach (int index in m_listCsv.List.selectedIndices)
            {
                LocaleEditorSettings.CsvExport(index);
            }
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
