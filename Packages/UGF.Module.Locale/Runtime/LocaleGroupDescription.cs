using System.Collections.Generic;
using UGF.Description.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleGroupDescription : DescriptionBase
    {
        public Dictionary<string, List<string>> Entries { get; } = new Dictionary<string, List<string>>();
    }
}
