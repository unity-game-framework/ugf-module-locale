using UGF.EditorTools.Editor.IMGUI;
using UGF.RuntimeTools.Editor.Tables;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    public class LocaleTableDrawer : TableDrawer
    {
        private LocaleTableEntryValueListDrawer m_selectedListValues;
        private ReorderableListSelectionDrawerByPathGlobalId m_selectedListValuesSelection;

        public LocaleTableDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnSelect(int index, SerializedProperty propertyEntry)
        {
            base.OnSelect(index, propertyEntry);

            SerializedProperty propertyValues = propertyEntry.FindPropertyRelative("m_values");

            propertyValues.isExpanded = true;

            m_selectedListValues = new LocaleTableEntryValueListDrawer(propertyValues);

            m_selectedListValuesSelection = new ReorderableListSelectionDrawerByPathGlobalId(m_selectedListValues, "m_locale")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_selectedListValues.Enable();
            m_selectedListValuesSelection.Enable();
        }

        protected override void OnDeselect(int index, SerializedProperty propertyEntry)
        {
            base.OnDeselect(index, propertyEntry);

            m_selectedListValues.Disable();
            m_selectedListValuesSelection.Disable();
            m_selectedListValues = null;
            m_selectedListValuesSelection = null;
        }

        protected override void DrawEntryProperties(int index, SerializedProperty propertyEntry)
        {
            m_selectedListValues.DrawGUILayout();
            m_selectedListValuesSelection.DrawGUILayout();
        }
    }
}
