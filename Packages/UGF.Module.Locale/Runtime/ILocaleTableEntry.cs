using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Tables;

namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTableEntry : ITableEntry
    {
        IEnumerable<ILocaleTableEntryValue> Values { get; }

        bool TryGet<T>(GlobalId localeId, out T value) where T : class, ILocaleTableEntryValue;
        bool TryGet(GlobalId localeId, out ILocaleTableEntryValue value);
    }
}
