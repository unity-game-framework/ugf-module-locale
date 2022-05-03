using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModuleDescription : ApplicationModuleDescription
    {
        public string DefaultLocaleId { get; set; }
        public bool UnloadEntriesOnUninitialize { get; set; }
        public Dictionary<string, IBuilder<LocaleDescription>> Locales { get; } = new Dictionary<string, IBuilder<LocaleDescription>>();
        public Dictionary<string, IBuilder<LocaleTableDescription>> Tables { get; } = new Dictionary<string, IBuilder<LocaleTableDescription>>();
        public List<string> PreloadTablesAsync { get; } = new List<string>();
    }
}
