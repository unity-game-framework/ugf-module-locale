using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    internal class LocaleKeyAndValueTextCollectionDrawer : LocaleKeyAndValueCollectionDrawer
    {
        public LocaleKeyAndValueTextCollectionDrawer(SerializedProperty serializedProperty, string propertyKeyName, string propertyValueName) : base(serializedProperty, propertyKeyName, propertyValueName)
        {
        }

        protected override float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            return EditorGUIUtility.singleLineHeight * 3F;
        }

        protected override void OnDrawValue(Rect position, SerializedProperty serializedProperty)
        {
            position.height = EditorGUIUtility.singleLineHeight * 3F;

            serializedProperty.stringValue = EditorGUI.TextArea(position, serializedProperty.stringValue);
        }
    }
}
