using UGF.CustomSettings.Editor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGF.Module.Locale.Editor
{
    internal class LocaleTableAssetBuildPreprocessor : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; }

        public void OnPreprocessBuild(BuildReport report)
        {
            CustomSettingsEditorPackage<LocaleEditorSettingsData> settings = LocaleEditorSettings.Settings;

            if (settings.Exists() && settings.GetData().TablesUpdateOnBuild)
            {
                LocaleEditorSettings.UpdateTableAll();
            }
        }
    }
}
