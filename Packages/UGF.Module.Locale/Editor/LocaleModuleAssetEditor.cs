using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleModuleAsset), true)]
    internal class LocaleModuleAssetEditor : UnityEditor.Editor
    {
        private AssetReferenceListDrawer m_listLocales;
        private ReorderableListSelectionDrawerByPath m_listLocalesSelection;
        private AssetReferenceListDrawer m_listEntries;
        private ReorderableListSelectionDrawerByPath m_listEntriesSelection;
        private ReorderableListDrawer m_listGroups;
        private ReorderableListSelectionDrawerByElement m_listGroupsSelection;

        private void OnEnable()
        {
            m_listLocales = new AssetReferenceListDrawer(serializedObject.FindProperty("m_locales"));

            m_listLocalesSelection = new ReorderableListSelectionDrawerByPath(m_listLocales, "m_asset")
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listEntries = new AssetReferenceListDrawer(serializedObject.FindProperty("m_entries"));

            m_listEntriesSelection = new ReorderableListSelectionDrawerByPath(m_listEntries, "m_asset")
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listGroups = new ReorderableListDrawer(serializedObject.FindProperty("m_groups"));

            m_listGroupsSelection = new ReorderableListSelectionDrawerByElement(m_listGroups)
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listLocales.Enable();
            m_listLocalesSelection.Enable();
            m_listEntries.Enable();
            m_listEntriesSelection.Enable();
            m_listGroups.Enable();
            m_listGroupsSelection.Enable();
        }

        private void OnDisable()
        {
            m_listLocales.Disable();
            m_listLocalesSelection.Disable();
            m_listEntries.Disable();
            m_listEntriesSelection.Disable();
            m_listGroups.Disable();
            m_listGroupsSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listLocales.DrawGUILayout();
                m_listEntries.DrawGUILayout();
                m_listGroups.DrawGUILayout();

                m_listLocalesSelection.DrawGUILayout();
                m_listEntriesSelection.DrawGUILayout();
                m_listGroupsSelection.DrawGUILayout();
            }
        }
    }
}
