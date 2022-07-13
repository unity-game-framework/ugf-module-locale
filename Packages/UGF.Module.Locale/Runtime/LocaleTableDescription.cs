using System.Collections.Generic;
using UGF.Description.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleTableDescription : DescriptionBase
    {
        public Dictionary<GlobalId, GlobalId> Entries { get; } = new Dictionary<GlobalId, GlobalId>();
    }
}
