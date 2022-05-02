using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    internal class LocaleTableAssetEntryValueListDrawer : ReorderableListDrawer
    {
        public LocaleTableAssetEntryValueListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyKey = serializedProperty.FindPropertyRelative("m_locale");
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

            float space = EditorGUIUtility.standardVerticalSpacing;
            float labelWidth = EditorGUIUtility.labelWidth + EditorIMGUIUtility.IndentPerLevel;

            var rectKey = new Rect(position.x, position.y, labelWidth, position.height);
            var rectValue = new Rect(rectKey.xMax + space, position.y, position.width - rectKey.width - space, position.height);

            EditorGUI.PropertyField(position, serializedProperty, GUIContent.none);
            EditorGUI.PropertyField(position, serializedProperty, GUIContent.none);
        }

        protected override float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected override bool OnElementHasVisibleChildren(SerializedProperty serializedProperty)
        {
            return false;
        }
    }
}
