using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTableEntry
    {
        GlobalId Id { get; }
        string Name { get; }
        IEnumerable<ILocaleTableEntryValue> Values { get; }
    }
}
