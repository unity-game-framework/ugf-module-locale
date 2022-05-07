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

        private void OnEnable()
        {
            m_listEntries = new ReorderableListKeyAndValueDrawer(serializedObject.FindProperty("m_entries"), "m_locale", "m_entries");
            m_listEntries.Enable();
        }

        private void OnDisable()
        {
            m_listEntries.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listEntries.DrawGUILayout();
            }
        }
    }
}
