using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Locale.Runtime;
using UGF.Tables.Editor;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleTableDescriptionAsset<,,>), true)]
    internal class LocaleTableDescriptionAssetEditor : UnityEditor.Editor
    {
        private TableDrawer m_tableDrawer;

        private void OnEnable()
        {
            m_tableDrawer = new TableDrawer(serializedObject.FindProperty("m_table"));
            m_tableDrawer.Enable();
        }

        private void OnDisable()
        {
            m_tableDrawer.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_tableDrawer.DrawGUILayout();
            }
        }
    }
}
