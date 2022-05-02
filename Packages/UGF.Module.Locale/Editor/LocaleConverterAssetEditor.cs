using UGF.Module.Locale.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    [CustomEditor(typeof(LocaleConverterAsset), true)]
    internal class LocaleConverterAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DrawConvertControls();
        }

        protected void DrawConvertControls()
        {
            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Convert All"))
                {
                    LocaleEditorSettings.ConvertAll();
                }

                if (GUILayout.Button("Convert"))
                {
                    var asset = (LocaleConverterAsset)target;

                    asset.Convert();

                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
}
