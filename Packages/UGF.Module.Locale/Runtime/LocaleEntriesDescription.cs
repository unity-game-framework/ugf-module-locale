using System.Collections.Generic;
using UGF.Description.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleEntriesDescription : DescriptionBase
    {
        public Dictionary<string, object> Values { get; } = new Dictionary<string, object>();
    }
}
