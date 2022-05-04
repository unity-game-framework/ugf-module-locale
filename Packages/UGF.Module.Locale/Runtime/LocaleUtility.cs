using System;
using System.Collections.Generic;

namespace UGF.Module.Locale.Runtime
{
    public static class LocaleUtility
    {
        public static IDictionary<string, IDictionary<string, object>> GetEntries(ILocaleTable table)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));

            var result = new Dictionary<string, IDictionary<string, object>>();

            foreach (ILocaleTableEntry entry in table.Entries)
            {
                foreach (ILocaleTableEntryValue entryValue in entry.Values)
                {
                    if (!result.TryGetValue(entryValue.LocaleId, out IDictionary<string, object> entries))
                    {
                        entries = new Dictionary<string, object>();

                        result.Add(entryValue.LocaleId, entries);
                    }

                    entries.Add(entry.Id, entryValue.Value);
                }
            }

            return result;
        }
    }
}
