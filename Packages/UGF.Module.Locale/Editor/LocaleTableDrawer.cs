using UGF.RuntimeTools.Editor.Tables;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    public class LocaleTableDrawer : TableDrawer
    {
        private LocaleTableEntryValueListDrawer m_selectedListValues;

        public LocaleTableDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnSelect(int index, SerializedProperty propertyEntry)
        {
            base.OnSelect(index, propertyEntry);

            SerializedProperty propertyValues = propertyEntry.FindPropertyRelative("m_values");

            propertyValues.isExpanded = true;

            m_selectedListValues = new LocaleTableEntryValueListDrawer(propertyValues);
            m_selectedListValues.Enable();
        }

        protected override void OnDeselect(int index, SerializedProperty propertyEntry)
        {
            base.OnDeselect(index, propertyEntry);

            m_selectedListValues.Disable();
            m_selectedListValues = null;
        }

        protected override void OnDraw(int index, SerializedProperty propertyEntry)
        {
            SerializedProperty propertyId = propertyEntry.FindPropertyRelative("m_id");
            SerializedProperty propertyName = propertyEntry.FindPropertyRelative("m_name");

            if (ShowIndexes)
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.IntField("Index", SelectedIndex);
                }
            }

            using (new EditorGUI.DisabledScope(!UnlockIds))
            {
                EditorGUILayout.PropertyField(propertyId);
            }

            EditorGUILayout.PropertyField(propertyName);

            m_selectedListValues.DrawGUILayout();
        }
    }
}
