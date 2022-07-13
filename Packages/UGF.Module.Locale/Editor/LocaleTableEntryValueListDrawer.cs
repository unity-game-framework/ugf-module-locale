using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    internal class LocaleTableEntryValueListDrawer : ReorderableListKeyAndValueDrawer
    {
        public LocaleTableEntryValueListDrawer(SerializedProperty serializedProperty) : base(serializedProperty, "m_locale")
        {
        }

        protected override void OnDrawValue(Rect position, SerializedProperty serializedProperty)
        {
            if (serializedProperty.propertyType == SerializedPropertyType.String)
            {
                position.height = EditorGUIUtility.singleLineHeight * 3F;

                serializedProperty.stringValue = EditorGUI.TextArea(position, serializedProperty.stringValue);
            }
            else
            {
                base.OnDrawValue(position, serializedProperty);
            }
        }

        protected override float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

            return propertyValue.propertyType == SerializedPropertyType.String ? EditorGUIUtility.singleLineHeight * 3F : EditorGUIUtility.singleLineHeight;
        }

        protected override bool OnElementHasVisibleChildren(SerializedProperty serializedProperty)
        {
            return false;
        }
    }
}
