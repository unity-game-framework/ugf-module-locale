using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleTableAsset<>), true)]
    internal class LocaleTableAssetEditor : UnityEditor.Editor
    {
        private readonly DropdownSelection<DropdownItem<int>> m_selection = new DropdownSelection<DropdownItem<int>>();
        private SerializedProperty m_propertyEntries;
        private int? m_selectedEntryIndex;
        private SerializedProperty m_selectedEntryPropertyId;
        private SerializedProperty m_selectedEntryPropertyName;
        private LocaleTableAssetEntryValueListDrawer m_selectedEntryListValues;
        private Styles m_styles;

        private class Styles
        {
            public GUIContent EntryNoneContent { get; } = new GUIContent("None");
            public GUIContent EntryEmptyContent { get; } = new GUIContent("<Empty>");
            public GUIContent BackButtonContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("back"), "Move to the previous entry.");
            public GUIContent ForwardButtonContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("forward"), "Move to the next entry.");
            public GUIContent AddButtonContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("Toolbar Plus"), "Add new entry.");
            public GUIContent RemoveButtonContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("Toolbar Minus"), "Delete current entry.");
        }

        private void OnEnable()
        {
            m_propertyEntries = serializedObject.FindProperty("m_entries");
        }

        private void OnDisable()
        {
            OnEntryDeselect();
        }

        public override void OnInspectorGUI()
        {
            m_styles ??= new Styles();

            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                OnEntryControlsDraw();

                EditorGUILayout.Space();

                OnEntrySelectedDraw();
            }
        }

        private void OnEntrySelect(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));

            for (int i = 0; i < m_propertyEntries.arraySize; i++)
            {
                SerializedProperty propertyEntry = m_propertyEntries.GetArrayElementAtIndex(i);
                SerializedProperty propertyKey = propertyEntry.FindPropertyRelative("m_key");

                if (propertyKey.stringValue == key)
                {
                    OnEntrySelect(i);
                    break;
                }
            }
        }

        private void OnEntrySelect(int index)
        {
            OnEntryDeselect();

            if (index < m_propertyEntries.arraySize)
            {
                SerializedProperty propertyEntry = m_propertyEntries.GetArrayElementAtIndex(index);

                m_selectedEntryIndex = index;
                m_selectedEntryPropertyName = propertyEntry.FindPropertyRelative("m_name");
                m_selectedEntryListValues = new LocaleTableAssetEntryValueListDrawer(propertyEntry.FindPropertyRelative("m_values"));
                m_selectedEntryListValues.Enable();
            }
        }

        private void OnEntryDeselect()
        {
            if (m_selectedEntryIndex != null)
            {
                m_selectedEntryIndex = null;
                m_selectedEntryPropertyName = null;
                m_selectedEntryListValues.Disable();
                m_selectedEntryListValues = null;
            }
        }

        private void OnEntrySelectedDraw()
        {
            if (m_selectedEntryIndex != null)
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(m_selectedEntryPropertyId);
                }

                EditorGUILayout.PropertyField(m_selectedEntryPropertyName);

                m_selectedEntryListValues.DrawGUILayout();
            }
            else
            {
                EditorGUILayout.HelpBox("Select entry to edit or create new one.", MessageType.Info);
            }
        }

        private void OnEntryControlsDraw()
        {
            EditorGUILayout.LabelField("Entries");

            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                using (new EditorGUI.DisabledScope(m_selectedEntryIndex == null || m_selectedEntryIndex == 0))
                {
                    if (OnDrawToolbarButton(m_styles.BackButtonContent))
                    {
                    }
                }

                GUIContent contentDropdown = m_styles.EntryNoneContent;

                if (m_selectedEntryPropertyName != null)
                {
                    string entryName = m_selectedEntryPropertyName.stringValue;

                    contentDropdown = !string.IsNullOrEmpty(entryName) ? new GUIContent(entryName) : m_styles.EntryEmptyContent;
                }

                Rect rectDropdown = GUILayoutUtility.GetRect(contentDropdown, EditorStyles.toolbarDropDown);

                if (DropdownEditorGUIUtility.Dropdown(rectDropdown, GUIContent.none, contentDropdown, m_selection, OnGetEntryKeyItems, out DropdownItem<int> selected, FocusType.Keyboard, EditorStyles.toolbarDropDown))
                {
                    OnEntrySelect(selected.Value);
                }

                using (new EditorGUI.DisabledScope(m_selectedEntryIndex == null))
                {
                    if (OnDrawToolbarButton(m_styles.RemoveButtonContent))
                    {
                    }
                }

                if (OnDrawToolbarButton(m_styles.AddButtonContent))
                {
                }

                using (new EditorGUI.DisabledScope(m_selectedEntryIndex == null || m_selectedEntryIndex >= m_propertyEntries.arraySize - 1))
                {
                    if (OnDrawToolbarButton(m_styles.ForwardButtonContent))
                    {
                    }
                }
            }
        }

        private IEnumerable<DropdownItem<int>> OnGetEntryKeyItems()
        {
            var items = new List<DropdownItem<int>>();

            for (int i = 0; i < m_propertyEntries.arraySize; i++)
            {
                SerializedProperty propertyEntry = m_propertyEntries.GetArrayElementAtIndex(i);
                SerializedProperty propertyKey = propertyEntry.FindPropertyRelative("m_key");
                string key = propertyKey.stringValue;
                string displayName = !string.IsNullOrEmpty(key) ? key : "<Empty>";

                items.Add(new DropdownItem<int>(displayName, i));
            }

            return items;
        }

        private bool OnDrawToolbarButton(GUIContent content)
        {
            return GUILayout.Button(content, EditorStyles.toolbarButton, GUILayout.Width(50F));
        }
    }
}
