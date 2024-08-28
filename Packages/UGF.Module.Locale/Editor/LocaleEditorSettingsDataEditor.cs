using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleEditorSettingsData), true)]
    internal class LocaleEditorSettingsDataEditor : UnityEditor.Editor
    {
        private ReorderableListDrawer m_listLocales;
        private ReorderableListSelectionDrawerByElement m_listLocalesSelection;

        private void OnEnable()
        {
            m_listLocales = new ReorderableListDrawer(serializedObject.FindProperty("m_locales"));

            m_listLocalesSelection = new ReorderableListSelectionDrawerByElement(m_listLocales)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listLocales.Enable();
            m_listLocalesSelection.Enable();
        }

        private void OnDisable()
        {
            m_listLocales.Disable();
            m_listLocalesSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                m_listLocales.DrawGUILayout();
            }

            m_listLocalesSelection.DrawGUILayout();
        }
    }
}
