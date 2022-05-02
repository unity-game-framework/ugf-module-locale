using System.Collections.Generic;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    public static class LocaleEditorUtility
    {
        public static IList<LocaleTableAsset> FindTableAssetAll()
        {
            var result = new List<LocaleTableAsset>();
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(LocaleTableAsset)}");

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var asset = AssetDatabase.LoadAssetAtPath<LocaleTableAsset>(path);

                result.Add(asset);
            }

            return result;
        }
    }
}
