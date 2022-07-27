using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    public static class LocaleEditorUtility
    {
        public static void UpdateEntries(LocaleTableDescriptionAsset tableDescriptionAsset, LocaleTableAsset tableAsset)
        {
            if (tableDescriptionAsset == null) throw new ArgumentNullException(nameof(tableDescriptionAsset));
            if (tableAsset == null) throw new ArgumentNullException(nameof(tableAsset));

            IDictionary<GlobalId, LocaleEntriesDescriptionAsset> tableAssets = new Dictionary<GlobalId, LocaleEntriesDescriptionAsset>();
            IDictionary<GlobalId, IDictionary<GlobalId, object>> tableEntries = LocaleUtility.GetEntries(tableAsset.Get());

            for (int i = 0; i < tableDescriptionAsset.Entries.Count; i++)
            {
                LocaleTableDescriptionAsset.Entry entry = tableDescriptionAsset.Entries[i];
                string entriesPath = AssetDatabase.GUIDToAssetPath(entry.Entries.ToString());
                var entriesAsset = AssetDatabase.LoadAssetAtPath<LocaleEntriesDescriptionAsset>(entriesPath);

                tableAssets.Add(entry.Locale, entriesAsset);
            }

            foreach ((GlobalId localeId, IDictionary<GlobalId, object> entries) in tableEntries)
            {
                if (tableAssets.TryGetValue(localeId, out LocaleEntriesDescriptionAsset asset))
                {
                    asset.SetValues(entries);

                    EditorUtility.SetDirty(asset);
                }
            }

            AssetDatabase.SaveAssets();
        }
    }
}
