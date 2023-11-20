using System;
using System.Collections.Generic;
using UGF.CustomSettings.Editor;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    internal class LocaleTableAssetPostprocessor : AssetPostprocessor
    {
        private static readonly Dictionary<string, LocaleEditorSettingsData.TableEntry> m_entries = new Dictionary<string, LocaleEditorSettingsData.TableEntry>();
        private static readonly HashSet<string> m_paths = new HashSet<string>();

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            CustomSettingsEditorPackage<LocaleEditorSettingsData> settings = LocaleEditorSettings.Settings;

            if (settings.Exists())
            {
                LocaleEditorSettingsData data = settings.GetData();

                if (data.TablesUpdateOnPostprocess)
                {
                    for (int i = 0; i < data.Tables.Count; i++)
                    {
                        LocaleEditorSettingsData.TableEntry entry = data.Tables[i];

                        if (entry.Table != null && entry.Description != null)
                        {
                            string path = AssetDatabase.GetAssetPath(entry.Table);

                            m_entries.Add(path, entry);
                        }
                    }

                    OnAddPaths(m_paths, importedAssets);
                    OnAddPaths(m_paths, movedAssets);

                    foreach (string path in m_paths)
                    {
                        if (m_entries.TryGetValue(path, out LocaleEditorSettingsData.TableEntry entry))
                        {
                            LocaleEditorUtility.UpdateEntries(entry.Description, entry.Table);
                        }
                    }

                    m_entries.Clear();
                    m_paths.Clear();

                    AssetDatabase.SaveAssets();
                }
            }
        }

        private static void OnAddPaths(HashSet<string> paths, string[] pathsToAdd)
        {
            if (paths == null) throw new ArgumentNullException(nameof(paths));
            if (pathsToAdd == null) throw new ArgumentNullException(nameof(pathsToAdd));

            for (int i = 0; i < pathsToAdd.Length; i++)
            {
                paths.Add(pathsToAdd[i]);
            }
        }
    }
}
