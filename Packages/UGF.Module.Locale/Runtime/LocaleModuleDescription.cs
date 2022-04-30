using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModuleDescription : ApplicationModuleDescription
    {
        public string DefaultLocaleId { get; set; }
        public bool UnloadEntriesOnUninitialize { get; set; }
        public Dictionary<string, LocaleDescription> Locales { get; } = new Dictionary<string, LocaleDescription>();
        public Dictionary<string, LocaleEntriesDescription> Entries { get; } = new Dictionary<string, LocaleEntriesDescription>();
        public Dictionary<string, LocaleGroupDescription> Groups { get; } = new Dictionary<string, LocaleGroupDescription>();
        public List<string> PreloadEntries { get; } = new List<string>();
        public List<string> PreloadGroups { get; } = new List<string>();
    }
}
