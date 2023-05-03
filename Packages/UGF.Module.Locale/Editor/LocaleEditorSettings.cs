using System;
using UGF.CustomSettings.Editor;
using UGF.Module.Locale.Runtime;
using UnityEditor;

namespace UGF.Module.Locale.Editor
{
    public static class LocaleEditorSettings
    {
        public static CustomSettingsEditorPackage<LocaleEditorSettingsData> Settings { get; } = new CustomSettingsEditorPackage<LocaleEditorSettingsData>
        (
            "UGF.Module.Locale",
            nameof(LocaleEditorSettings)
        );

        public static void UpdateTableAll()
        {
            LocaleEditorSettingsData data = Settings.GetData();

            for (int i = 0; i < data.Tables.Count; i++)
            {
                UpdateTable(i);
            }
        }

        public static void UpdateTable(int index)
        {
            LocaleEditorSettingsData data = Settings.GetData();
            LocaleEditorSettingsData.TableEntry entry = data.Tables[index];

            LocaleEditorUtility.UpdateEntries(entry.Description, entry.Table);
        }

        public static bool TryGetTable(LocaleTableDescriptionAsset description, out LocaleTableAsset table, out int index)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));

            LocaleEditorSettingsData data = Settings.GetData();

            for (int i = 0; i < data.Tables.Count; i++)
            {
                LocaleEditorSettingsData.TableEntry entry = data.Tables[i];

                if (entry.Description == description)
                {
                    table = entry.Table;
                    index = i;
                    return true;
                }
            }

            table = default;
            index = default;
            return false;
        }

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<LocaleEditorSettingsData>("Project/Unity Game Framework/Locales", Settings, SettingsScope.Project);
        }
    }
}
