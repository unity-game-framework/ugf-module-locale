﻿using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleGroupDescriptionAsset), true)]
    internal class LocaleGroupDescriptionAssetEditor : UnityEditor.Editor
    {
        private LocaleGroupDescriptionAssetEntriesListDrawer m_listEntries;

        private void OnEnable()
        {
            m_listEntries = new LocaleGroupDescriptionAssetEntriesListDrawer(serializedObject.FindProperty("m_entries"));
            m_listEntries.Enable();
        }

        private void OnDisable()
        {
            m_listEntries.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listEntries.DrawGUILayout();
            }
        }
    }
}
