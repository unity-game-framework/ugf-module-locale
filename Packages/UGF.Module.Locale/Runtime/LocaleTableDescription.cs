using System.Collections.Generic;
using UGF.Description.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleTableDescription : DescriptionBase
    {
        public Dictionary<string, string> Entries { get; } = new Dictionary<string, string>();
    }
}
