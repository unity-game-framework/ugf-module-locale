using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    public class LocaleTableDrawer : DrawerBase
    {
        public SerializedProperty SerializedProperty { get; }
        public int SelectedIndex { get { return m_selectedIndex ?? throw new ArgumentException("Value not specified."); } }
        public bool SearchById { get; set; }
        public bool ShowIndexes { get; set; }
        public bool UnlockIds { get; set; }

        private readonly DropdownSelection<DropdownItem<int>> m_selection = new DropdownSelection<DropdownItem<int>>();
        private readonly SerializedProperty m_propertyEntries;
        private int? m_selectedIndex;
        private SerializedProperty m_selectedPropertyId;
        private SerializedProperty m_selectedPropertyName;
        private LocaleTableEntryValueListDrawer m_selectedListValues;
        private Styles m_styles;

        private class Styles
        {
            public GUIContent EntryNoneContent { get; } = new GUIContent("None");
            public GUIContent EntryEmptyContent { get; } = new GUIContent("Untitled");
            public GUIContent AddButtonContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("Toolbar Plus"), "Add new entry.");
            public GUIContent RemoveButtonContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("Toolbar Minus"), "Delete current entry.");
            public GUIContent MenuButtonContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("_Menu"));
        }

        public LocaleTableDrawer(SerializedProperty serializedProperty)
        {
            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));

            m_propertyEntries = SerializedProperty.FindPropertyRelative("m_entries");
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            OnEntryDeselect();
        }

        public void DrawGUILayout()
        {
            m_styles ??= new Styles();

            SerializedProperty.isExpanded = EditorGUILayout.Foldout(SerializedProperty.isExpanded, SerializedProperty.displayName, true);

            if (SerializedProperty.isExpanded)
            {
                OnEntryControlsDraw();

                EditorGUILayout.Space();

                OnEntrySelectedDraw();
            }
        }

        private void OnEntrySelect(int index)
        {
            OnEntryDeselect();

            if (index < m_propertyEntries.arraySize)
            {
                SerializedProperty propertyEntry = m_propertyEntries.GetArrayElementAtIndex(index);

                m_selectedIndex = index;
                m_selectedPropertyId = propertyEntry.FindPropertyRelative("m_id");
                m_selectedPropertyName = propertyEntry.FindPropertyRelative("m_name");

                SerializedProperty propertyValues = propertyEntry.FindPropertyRelative("m_values");

                propertyValues.isExpanded = true;

                m_selectedListValues = new LocaleTableEntryValueListDrawer(propertyValues);
                m_selectedListValues.Enable();
            }
        }

        private void OnEntryDeselect()
        {
            if (m_selectedIndex != null)
            {
                m_selectedIndex = null;
                m_selectedPropertyName = null;
                m_selectedListValues.Disable();
                m_selectedListValues = null;
            }
        }

        private void OnEntryRemove(int index)
        {
            OnEntryDeselect();

            m_propertyEntries.DeleteArrayElementAtIndex(index);
        }

        private void OnEntryAdd(int index)
        {
            m_propertyEntries.InsertArrayElementAtIndex(index);

            SerializedProperty propertyEntry = m_propertyEntries.GetArrayElementAtIndex(index);
            SerializedProperty propertyId = propertyEntry.FindPropertyRelative("m_id");
            SerializedProperty propertyName = propertyEntry.FindPropertyRelative("m_name");

            propertyId.stringValue = Guid.NewGuid().ToString("N");

            if (string.IsNullOrEmpty(propertyName.stringValue))
            {
                propertyName.stringValue = "Entry";
            }

            OnEntrySelect(index);
        }

        private void OnEntrySelectedDraw()
        {
            if (m_selectedIndex != null)
            {
                if (ShowIndexes)
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUILayout.IntField("Index", SelectedIndex);
                    }
                }

                using (new EditorGUI.DisabledScope(!UnlockIds))
                {
                    EditorGUILayout.PropertyField(m_selectedPropertyId);
                }

                EditorGUILayout.PropertyField(m_selectedPropertyName);

                m_selectedListValues.DrawGUILayout();
            }
            else
            {
                EditorGUILayout.HelpBox("Select entry to edit or create new one.", MessageType.Info);
            }
        }

        private void OnEntryControlsDraw()
        {
            EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);

            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUIContent contentDropdown = m_styles.EntryNoneContent;

                if (m_selectedPropertyName != null)
                {
                    string entryName = m_selectedPropertyName.stringValue;

                    contentDropdown = !string.IsNullOrEmpty(entryName) ? new GUIContent(entryName) : m_styles.EntryEmptyContent;
                }

                Rect rectDropdown = GUILayoutUtility.GetRect(contentDropdown, EditorStyles.toolbarDropDown);

                if (DropdownEditorGUIUtility.Dropdown(rectDropdown, GUIContent.none, contentDropdown, m_selection, OnGetEntryKeyItems, out DropdownItem<int> selected, FocusType.Keyboard, EditorStyles.toolbarDropDown))
                {
                    OnEntrySelect(selected.Value);
                }

                using (new EditorGUI.DisabledScope(m_selectedIndex == null))
                {
                    if (OnDrawToolbarButton(m_styles.RemoveButtonContent))
                    {
                        OnEntryRemove(SelectedIndex);
                    }
                }

                if (OnDrawToolbarButton(m_styles.AddButtonContent))
                {
                    int index = m_selectedIndex != null
                        ? m_selectedIndex.Value + 1
                        : m_propertyEntries.arraySize;

                    OnEntryAdd(index);
                }

                Rect rectMenu = GUILayoutUtility.GetRect(m_styles.MenuButtonContent, EditorStyles.toolbarButton, GUILayout.Width(25F));

                if (GUI.Button(rectMenu, m_styles.MenuButtonContent, EditorStyles.toolbarButton))
                {
                    OnMenuOpen(rectMenu);
                }
            }
        }

        private void OnMenuOpen(Rect position)
        {
            var menu = new GenericMenu();

            menu.AddItem(new GUIContent("Search by Id"), SearchById, () => SearchById = !SearchById);
            menu.AddItem(new GUIContent("Show Indexes"), ShowIndexes, () => ShowIndexes = !ShowIndexes);
            menu.AddItem(new GUIContent("Unlock Ids"), UnlockIds, () => UnlockIds = !UnlockIds);
            menu.AddSeparator(string.Empty);

            if (m_propertyEntries.arraySize > 0)
            {
                menu.AddItem(new GUIContent("Clear"), false, OnMenuClear);
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Clear"));
            }

            menu.DropDown(position);
        }

        private void OnMenuClear()
        {
            OnEntryDeselect();

            m_propertyEntries.ClearArray();
            m_propertyEntries.serializedObject.ApplyModifiedProperties();
        }

        private IEnumerable<DropdownItem<int>> OnGetEntryKeyItems()
        {
            var items = new List<DropdownItem<int>>();

            for (int i = 0; i < m_propertyEntries.arraySize; i++)
            {
                SerializedProperty propertyEntry = m_propertyEntries.GetArrayElementAtIndex(i);
                string displayName;

                if (SearchById)
                {
                    SerializedProperty propertyId = propertyEntry.FindPropertyRelative("m_id");

                    displayName = propertyId.stringValue;
                }
                else
                {
                    SerializedProperty propertyName = propertyEntry.FindPropertyRelative("m_name");
                    string value = propertyName.stringValue;

                    displayName = !string.IsNullOrEmpty(value) ? value : m_styles.EntryEmptyContent.text;
                }

                if (ShowIndexes)
                {
                    displayName = $"[{i}] {displayName}";
                }

                items.Add(new DropdownItem<int>(displayName, i));
            }

            return items;
        }

        private bool OnDrawToolbarButton(GUIContent content, float width = 50F)
        {
            return GUILayout.Button(content, EditorStyles.toolbarButton, GUILayout.Width(width));
        }
    }
}
