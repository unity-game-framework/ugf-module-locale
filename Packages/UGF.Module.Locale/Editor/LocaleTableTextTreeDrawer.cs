using UGF.Tables.Editor;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    public class LocaleTableTextTreeDrawer : TableTreeDrawer
    {
        public LocaleTableTextTreeDrawer(SerializedObject serializedObject, TableTreeOptions options) : base(serializedObject, options)
        {
        }

        protected override void OnDrawRowCellValue(Rect position, TableTreeViewItem item, SerializedProperty serializedProperty, TableTreeColumnOptions column)
        {
            if (column.PropertyName != Options.PropertyIdName
                && column.PropertyName != Options.PropertyNameName
                && serializedProperty.propertyType == SerializedPropertyType.String)
            {
                serializedProperty.stringValue = EditorGUI.TextArea(position, serializedProperty.stringValue);
            }
            else
            {
                base.OnDrawRowCellValue(position, item, serializedProperty, column);
            }
        }
    }
}
