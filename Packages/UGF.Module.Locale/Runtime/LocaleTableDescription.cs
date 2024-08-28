using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Descriptions.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleTableDescription<TDescription, TValue> : DescriptionTable<TDescription>, ILocaleTableDescription<TDescription, TValue> where TDescription : ILocaleTableEntryDescription<TValue>
    {
        public bool TryGetValue(LocaleDescription locale, GlobalId entryId, out TValue value)
        {
            value = default;
            return TryGet(entryId, out TDescription entry) && entry.TryGetValue(locale, out value);
        }
    }
}
