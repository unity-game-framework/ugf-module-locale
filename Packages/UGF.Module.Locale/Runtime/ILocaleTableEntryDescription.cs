using System.Collections.Generic;
using UGF.Description.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTableEntryDescription<TValue> : IDescription
    {
        bool TryGetValue(LocaleDescription locale, out TValue value);
        void GetValues(ICollection<TValue> values);
    }
}
