using System;
using System.Collections.Generic;
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
        private readonly DropdownSelection<DropdownItem<string>> m_selection = new DropdownSelection<DropdownItem<string>>();
        private readonly Func<IEnumerable<DropdownItem<string>>> m_itemsHandler;
        private Styles m_styles;

        private class Styles
        {
            public GUIContent NoneContent { get; } = new GUIContent("None");
            public GUIContent MissingContent { get; } = new GUIContent("Missing");
            public GUIContent UntitledContent { get; } = new GUIContent("Untitled");
        }

        public LocaleEntryDropdownAttributePropertyDrawer() : base(SerializedPropertyType.String)
        {
            m_itemsHandler = GetItems;
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            m_styles ??= new Styles();

            string value = serializedProperty.stringValue;
            GUIContent content = m_styles.NoneContent;

            if (!string.IsNullOrEmpty(value))
            {
                if (LocaleEditorUtility.TryGetEntryNameFromCache(value, out string name))
                {
                    content = !string.IsNullOrEmpty(name) ? new GUIContent(name) : m_styles.UntitledContent;
                }
                else
                {
                    content = m_styles.MissingContent;
                }
            }

            if (DropdownEditorGUIUtility.Dropdown(position, label, content, m_selection, m_itemsHandler, out DropdownItem<string> selected))
            {
                serializedProperty.stringValue = selected.Value;
            }
        }

        private IEnumerable<DropdownItem<string>> GetItems()
        {
            var items = new List<DropdownItem<string>>();
            IReadOnlyList<LocaleTableAsset> tables = LocaleEditorUtility.FindTableAssetAll();

            LocaleEntriesCache.Update(tables);

            items.Add(new DropdownItem<string>("None", string.Empty)
            {
                Priority = int.MaxValue
            });

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
