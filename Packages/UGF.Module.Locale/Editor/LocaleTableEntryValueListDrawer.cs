using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UGF.Module.Locale.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    internal class LocaleTableEntryValueListDrawer : ReorderableListDrawer
    {
        public LocaleTableEntryValueListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyKey = serializedProperty.FindPropertyRelative("m_key");
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

            float space = EditorGUIUtility.standardVerticalSpacing;
            float labelWidth = EditorGUIUtility.labelWidth + EditorIMGUIUtility.IndentPerLevel;

            var rectKey = new Rect(position.x, position.y, labelWidth, position.height);
            var rectValue = new Rect(rectKey.xMax + space, position.y, position.width - rectKey.width - space, position.height);

            AttributeEditorGUIUtility.DrawAssetGuidField(rectKey, propertyKey, GUIContent.none, typeof(LocaleDescriptionAsset));

            if (propertyValue.propertyType == SerializedPropertyType.String)
            {
                rectValue.height = EditorGUIUtility.singleLineHeight * 3F;

                propertyValue.stringValue = EditorGUI.TextArea(rectValue, propertyValue.stringValue);
            }
            else
            {
                EditorGUI.PropertyField(rectValue, propertyValue, GUIContent.none);
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
