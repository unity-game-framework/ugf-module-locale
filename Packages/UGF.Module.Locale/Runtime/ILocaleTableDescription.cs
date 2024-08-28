using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Descriptions.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTableDescription<TValue> : IDescriptionTable
    {
        bool TryGetValue(LocaleDescription locale, GlobalId entryId, out TValue value);
    }
}
