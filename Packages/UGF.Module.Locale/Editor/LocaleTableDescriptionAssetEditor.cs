using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleTableDescriptionAsset), true)]
    internal class LocaleTableDescriptionAssetEditor : UnityEditor.Editor
    {
        private ReorderableListKeyAndValueDrawer m_listEntries;
        private ReorderableListSelectionDrawerByPathGlobalId m_listEntriesSelectionLocale;
        private ReorderableListSelectionDrawerByPathGlobalId m_listEntriesSelectionEntries;

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
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listEntries.DrawGUILayout();
                m_listEntriesSelectionLocale.DrawGUILayout();
                m_listEntriesSelectionEntries.DrawGUILayout();
            }
        }
    }
}
