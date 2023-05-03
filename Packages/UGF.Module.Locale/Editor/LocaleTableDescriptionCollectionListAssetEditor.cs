using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleTableDescriptionCollectionListAsset), true)]
    internal class LocaleTableDescriptionCollectionListAssetEditor : UnityEditor.Editor
    {
        private AssetIdReferenceListDrawer m_listTables;
        private ReorderableListSelectionDrawerByPath m_listTablesSelection;

        private void OnEnable()
        {
            m_listTables = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_tables"));

            m_listTablesSelection = new ReorderableListSelectionDrawerByPath(m_listTables, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
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
                m_listTablesSelection.DrawGUILayout();
            }
        }
    }
}
