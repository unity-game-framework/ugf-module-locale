using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.Module.Locale.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    [CustomPropertyDrawer(typeof(LocaleEntryDropdownAttribute), true)]
    internal class LocaleEntryDropdownAttributePropertyDrawer : PropertyDrawerTyped<LocaleEntryDropdownAttribute>
    {
        private readonly Func<IEnumerable<DropdownItem<string>>> m_itemsHandler;

        public LocaleEntryDropdownAttributePropertyDrawer() : base(SerializedPropertyType.String)
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
            IList<LocaleTableAsset> tables = LocaleEditorUtility.FindTableAssetAll();

            for (int i = 0; i < tables.Count; i++)
            {
                LocaleTableAsset asset = tables[i];
                ILocaleTable table = asset.GetTable();

                foreach (ILocaleTableEntry entry in table.Entries)
                {
                    items.Add(new DropdownItem<string>(entry.Name, entry.Id)
                    {
                        Path = asset.name
                    });
                }
            }

            return items;
        }
    }
}
