using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    internal class LocaleGroupDescriptionAssetEntriesListDrawer : ReorderableListDrawer
    {
        public LocaleGroupDescriptionAssetEntriesListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyLocale = serializedProperty.FindPropertyRelative("m_locale");
            SerializedProperty propertyEntries = serializedProperty.FindPropertyRelative("m_entries");

            float space = EditorGUIUtility.standardVerticalSpacing;
            float labelWidth = EditorGUIUtility.labelWidth + EditorIMGUIUtility.IndentPerLevel;

            var rectLocale = new Rect(position.x, position.y, labelWidth, position.height);
            var rectEntries = new Rect(rectLocale.xMax + space, position.y, position.width - rectLocale.width - space, position.height);

            EditorGUI.PropertyField(rectLocale, propertyLocale, GUIContent.none);
            EditorGUI.PropertyField(rectEntries, propertyEntries, GUIContent.none);
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
