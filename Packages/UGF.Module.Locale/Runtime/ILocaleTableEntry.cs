using System.Collections.Generic;
using UGF.RuntimeTools.Runtime.Tables;

namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTableEntry : ITableEntry
    {
        IEnumerable<ILocaleTableEntryValue> Values { get; }
    }
}
