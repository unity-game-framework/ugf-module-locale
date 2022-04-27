using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModuleDescription : ApplicationModuleDescription
    {
        public Dictionary<string, LocaleDescription> Locales { get; } = new Dictionary<string, LocaleDescription>();
        public Dictionary<string, LocaleEntriesDescription> Entries { get; } = new Dictionary<string, LocaleEntriesDescription>();
        public List<LocaleTableDescription> Tables { get; } = new List<LocaleTableDescription>();
    }
}
