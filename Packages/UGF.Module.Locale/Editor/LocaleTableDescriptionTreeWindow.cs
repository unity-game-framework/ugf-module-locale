using System;
using System.Reflection;
using UGF.Module.Locale.Runtime;
using UGF.Tables.Editor;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [TableTreeWindow(typeof(LocaleTableDescriptionAsset))]
    internal class LocaleTableDescriptionTreeWindow : TableTreeWindow
    {
        protected override TableTreeDrawer OnCreateDrawer(SerializedObject serializedObject)
        {
            Type type = serializedObject.targetObject.GetType();

            if (type.IsDefined(typeof(LocaleTableTextTreeDrawerAttribute)))
            {
                TableTreeOptions options = CreateOptions(serializedObject);

                options.RowHeight = EditorGUIUtility.singleLineHeight * 4F;

                return new LocaleTableTextTreeDrawer(serializedObject, options);
            }

            return base.OnCreateDrawer(serializedObject);
        }
    }
}
