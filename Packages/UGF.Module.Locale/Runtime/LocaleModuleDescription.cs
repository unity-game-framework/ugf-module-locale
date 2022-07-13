using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModuleDescription : ApplicationModuleDescription
    {
        public GlobalId DefaultLocaleId { get; set; }
        public bool UnloadEntriesOnUninitialize { get; set; }
        public Dictionary<GlobalId, IBuilder<LocaleDescription>> Locales { get; } = new Dictionary<GlobalId, IBuilder<LocaleDescription>>();
        public Dictionary<GlobalId, IBuilder<LocaleTableDescription>> Tables { get; } = new Dictionary<GlobalId, IBuilder<LocaleTableDescription>>();
        public List<GlobalId> PreloadTablesAsync { get; } = new List<GlobalId>();
    }
}
