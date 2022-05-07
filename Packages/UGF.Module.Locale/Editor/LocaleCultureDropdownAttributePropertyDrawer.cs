using System;
using System.Collections.Generic;
using System.Globalization;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.Module.Locale.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    [CustomPropertyDrawer(typeof(LocaleCultureDropdownAttribute))]
    internal class LocaleCultureDropdownAttributePropertyDrawer : PropertyDrawerTyped<LocaleCultureDropdownAttribute>
    {
        private readonly Func<IEnumerable<DropdownItem<string>>> m_itemsHandler;

        public LocaleCultureDropdownAttributePropertyDrawer() : base(SerializedPropertyType.String)
        {
            m_itemsHandler = GetItems;
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            EditorElementsUtility.TextFieldWithDropdown(position, label, serializedProperty, m_itemsHandler);
        }

        private IEnumerable<DropdownItem<string>> GetItems()
        {
            var items = new List<DropdownItem<string>>();
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            foreach (CultureInfo culture in cultures)
            {
                string name = !string.IsNullOrEmpty(culture.Name)
                    ? $"{culture.Name} ({culture.EnglishName})"
                    : $"Empty ({culture.EnglishName})";

                var item = new DropdownItem<string>(name, culture.Name);

                items.Add(item);
            }

            return items;
        }
    }
}
