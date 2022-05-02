using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleEditorSettingsData), true)]
    internal class LocaleEditorSettingsDataEditor : UnityEditor.Editor
    {
        private ReorderableListDrawer m_listConverters;
        private ReorderableListSelectionDrawerByElement m_listConvertersSelection;

        private void OnEnable()
        {
            m_listConverters = new ReorderableListDrawer(serializedObject.FindProperty("m_converters"));

            m_listConvertersSelection = new ReorderableListSelectionDrawerByElement(m_listConverters)
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listConverters.Enable();
            m_listConvertersSelection.Enable();
        }

        private void OnDisable()
        {
            m_listConverters.Disable();
            m_listConvertersSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                m_listConverters.DrawGUILayout();
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Convert All"))
                {
                    LocaleEditorSettings.ConvertAll();
                }

                EditorGUILayout.Space();
            }
        }
    }
}
