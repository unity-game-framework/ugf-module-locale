using System.Collections.Generic;
using UGF.Description.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleEntriesDescription : DescriptionBase
    {
        public Dictionary<GlobalId, object> Values { get; } = new Dictionary<GlobalId, object>();
    }
}
