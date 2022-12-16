﻿using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleEditorSettingsData), true)]
    internal class LocaleEditorSettingsDataEditor : UnityEditor.Editor
    {
        private ReorderableListKeyAndValueDrawer m_listTables;
        private ReorderableListSelectionDrawer m_listTablesSelectionTable;
        private ReorderableListSelectionDrawer m_listTablesSelectionDescription;

        private void OnEnable()
        {
            m_listTables = new ReorderableListKeyAndValueDrawer(serializedObject.FindProperty("m_tables"), "m_table", "m_description");

            m_listTablesSelectionTable = new ReorderableListSelectionDrawerByPath(m_listTables, "m_table")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listTablesSelectionDescription = new ReorderableListSelectionDrawerByPath(m_listTables, "m_description")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listTables.Enable();
            m_listTablesSelectionTable.Enable();
            m_listTablesSelectionDescription.Enable();
        }

        private void OnDisable()
        {
            m_listTables.Disable();
            m_listTablesSelectionTable.Disable();
            m_listTablesSelectionDescription.Disable();
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

            m_listTablesSelectionTable.DrawGUILayout();
            m_listTablesSelectionDescription.DrawGUILayout();
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
