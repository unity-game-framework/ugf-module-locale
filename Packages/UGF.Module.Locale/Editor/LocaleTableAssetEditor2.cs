using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(Runtime.LocaleTableAsset), true)]
    internal class LocaleTableAssetEditor2 : UnityEditor.Editor
    {
        private AssetReferenceListDrawer m_listData;
        private ReorderableListSelectionDrawerByPath m_listDataSelection;

        private void OnEnable()
        {
            m_listData = new AssetReferenceListDrawer(serializedObject.FindProperty("m_data"));

            m_listDataSelection = new ReorderableListSelectionDrawerByPath(m_listData, "m_asset")
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listData.Enable();
            m_listDataSelection.Enable();
        }

        private void OnDisable()
        {
            m_listData.Disable();
            m_listDataSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listData.DrawGUILayout();
                m_listDataSelection.DrawGUILayout();
            }
        }
    }
}
