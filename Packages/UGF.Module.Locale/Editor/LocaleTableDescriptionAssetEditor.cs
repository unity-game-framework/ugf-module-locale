using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Locale.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleTableDescriptionAsset), true)]
    internal class LocaleTableDescriptionAssetEditor : UnityEditor.Editor
    {
        private ReorderableListKeyAndValueDrawer m_listEntries;
        private ReorderableListSelectionDrawerByPathGlobalId m_listEntriesSelectionLocale;
        private ReorderableListSelectionDrawerByPathGlobalId m_listEntriesSelectionEntries;
        private Styles m_styles;

        private class Styles
        {
            public GUIContent UpdateEnabledContent { get; } = new GUIContent("Update", "Update entries with data from Locale Table registered at Locale project settings.");
            public GUIContent UpdateDisabledContent { get; } = new GUIContent("Update", "Locale Table Description should be registered at Locale project settings in to order update it.");
        }

        private void OnEnable()
        {
            m_listEntries = new ReorderableListKeyAndValueDrawer(serializedObject.FindProperty("m_entries"), "m_locale", "m_entries");

            m_listEntriesSelectionLocale = new ReorderableListSelectionDrawerByPathGlobalId(m_listEntries, "m_locale")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listEntriesSelectionEntries = new ReorderableListSelectionDrawerByPathGlobalId(m_listEntries, "m_entries")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listEntries.Enable();
            m_listEntriesSelectionLocale.Enable();
            m_listEntriesSelectionEntries.Enable();
        }

        private void OnDisable()
        {
            m_listEntries.Disable();
            m_listEntriesSelectionLocale.Disable();
            m_listEntriesSelectionEntries.Disable();
        }

        public override void OnInspectorGUI()
        {
            m_styles ??= new Styles();

            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listEntries.DrawGUILayout();
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new EditorGUI.DisabledScope(!OnCanUpdate()))
                {
                    GUIContent content = OnHasRegisteredAtSettings() ? m_styles.UpdateEnabledContent : m_styles.UpdateDisabledContent;

                    if (GUILayout.Button(content, GUILayout.Width(65F)))
                    {
                        OnUpdate();
                    }
                }
            }

            EditorGUILayout.Space();

            m_listEntriesSelectionLocale.DrawGUILayout();
            m_listEntriesSelectionEntries.DrawGUILayout();
        }

        private bool OnHasRegisteredAtSettings()
        {
            return LocaleEditorSettings.TryGetTable((LocaleTableDescriptionAsset)target, out _, out _);
        }

        private bool OnCanUpdate()
        {
            return m_listEntries.SerializedProperty.arraySize > 0 && OnHasRegisteredAtSettings();
        }

        private void OnUpdate()
        {
            if (LocaleEditorSettings.TryGetTable((LocaleTableDescriptionAsset)target, out _, out int index))
            {
                LocaleEditorSettings.UpdateTable(index);
            }
        }
    }
}
