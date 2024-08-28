using UGF.CustomSettings.Editor;
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

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<LocaleEditorSettingsData>("Project/Unity Game Framework/Locales", Settings, SettingsScope.Project);
        }
    }
}
