using System.Collections.Generic;
using UGF.Description.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleCollectionDescription : DescriptionBase
    {
        public List<string> Entries { get; } = new List<string>();
    }
}
