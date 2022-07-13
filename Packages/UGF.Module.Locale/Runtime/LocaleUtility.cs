using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Locale.Runtime
{
    public static class LocaleUtility
    {
        public static IDictionary<GlobalId, IDictionary<GlobalId, object>> GetEntries(ILocaleTable table)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));

            var result = new Dictionary<GlobalId, IDictionary<GlobalId, object>>();

            foreach (ILocaleTableEntry entry in table.Entries)
            {
                foreach (ILocaleTableEntryValue entryValue in entry.Values)
                {
                    if (!result.TryGetValue(entryValue.LocaleId, out IDictionary<GlobalId, object> entries))
                    {
                        entries = new Dictionary<GlobalId, object>();

                        result.Add(entryValue.LocaleId, entries);
                    }

                    entries.Add(entry.Id, entryValue.Value);
                }
            }

            return result;
        }
    }
}
