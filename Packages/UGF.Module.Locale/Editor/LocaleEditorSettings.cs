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

        public static void ConvertAll()
        {
            LocaleEditorSettingsData data = Settings.GetData();

            for (int i = 0; i < data.Converters.Count; i++)
            {
                LocaleConverterAsset converter = data.Converters[i];

                converter.Convert();
            }

            AssetDatabase.SaveAssets();
        }

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<LocaleEditorSettingsData>("Project/Unity Game Framework/Locales", Settings, SettingsScope.Project);
        }
    }
}
