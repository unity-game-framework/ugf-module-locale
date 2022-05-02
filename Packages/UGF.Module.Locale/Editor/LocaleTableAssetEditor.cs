using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleTableAsset<>), true)]
    internal class LocaleTableAssetEditor : UnityEditor.Editor
    {
        private LocaleTableDrawer m_tableDrawer;

        private void OnEnable()
        {
            m_tableDrawer = new LocaleTableDrawer(serializedObject.FindProperty("m_table"));
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
