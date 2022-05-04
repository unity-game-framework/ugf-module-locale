using System.Collections.Generic;

namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTableEntry
    {
        string Id { get; }
        string Name { get; }
        IEnumerable<ILocaleTableEntryValue> Values { get; }

        void Add(ILocaleTableEntryValue value);
        bool Remove(ILocaleTableEntryValue value);
        void Clear();
    }
}
