using System.Collections.Generic;

namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTableEntry
    {
        string Id { get; }
        string Name { get; }
        IEnumerable<ILocaleTableEntryValue> Values { get; }
    }
}
