using System.Collections.Generic;

namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTableEntry
    {
        string Id { get; }
        string Name { get; }
        IEnumerable<string> Locales { get; }

        bool TryGetValue(string localeId, out object value);
    }
}
