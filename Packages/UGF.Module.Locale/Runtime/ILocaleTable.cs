using System.Collections.Generic;

namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTable
    {
        IEnumerable<ILocaleTableEntry> Entries { get; }

        void Add(ILocaleTableEntry entry);
        bool Remove(ILocaleTableEntry entry);
        void Clear();
    }
}
