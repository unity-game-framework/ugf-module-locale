using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleModuleAsset), true)]
    internal class LocaleModuleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyDefaultLocale;
        private SerializedProperty m_propertySelectLocaleBySystemLanguageOnInitialize;
        private AssetIdReferenceListDrawer m_listLocales;
        private ReorderableListSelectionDrawerByPath m_listLocalesSelection;
        private AssetIdReferenceListDrawer m_listTables;
        private ReorderableListSelectionDrawerByPath m_listTablesSelection;
        private ReorderableListDrawer m_listPreloadTablesAsync;
        private ReorderableListSelectionDrawerByElementGlobalId m_listPreloadTablesAsyncSelection;

        private void OnEnable()
        {
            m_propertyDefaultLocale = serializedObject.FindProperty("m_defaultLocale");
            m_propertySelectLocaleBySystemLanguageOnInitialize = serializedObject.FindProperty("m_selectLocaleBySystemLanguageOnInitialize");

            m_listLocales = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_locales"))
            {
                DisplayAsSingleLine = true
            };

            m_listLocalesSelection = new ReorderableListSelectionDrawerByPath(m_listLocales, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listTables = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_tables"))
            {
                DisplayAsSingleLine = true
            };

            m_listTablesSelection = new ReorderableListSelectionDrawerByPath(m_listTables, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listPreloadTablesAsync = new ReorderableListDrawer(serializedObject.FindProperty("m_preloadTablesAsync"))
            {
                DisplayAsSingleLine = true
            };

            m_listPreloadTablesAsyncSelection = new ReorderableListSelectionDrawerByElementGlobalId(m_listPreloadTablesAsync)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listLocales.Enable();
            m_listLocalesSelection.Enable();
            m_listTables.Enable();
            m_listTablesSelection.Enable();
            m_listPreloadTablesAsync.Enable();
            m_listPreloadTablesAsyncSelection.Enable();
        }

        private void OnDisable()
        {
            m_listLocales.Disable();
            m_listLocalesSelection.Disable();
            m_listTables.Disable();
            m_listTablesSelection.Disable();
            m_listPreloadTablesAsync.Disable();
            m_listPreloadTablesAsyncSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyDefaultLocale);
                EditorGUILayout.PropertyField(m_propertySelectLocaleBySystemLanguageOnInitialize);

                m_listLocales.DrawGUILayout();
                m_listTables.DrawGUILayout();
                m_listPreloadTablesAsync.DrawGUILayout();

                m_listLocalesSelection.DrawGUILayout();
                m_listTablesSelection.DrawGUILayout();
                m_listPreloadTablesAsyncSelection.DrawGUILayout();
            }
        }
    }
}
