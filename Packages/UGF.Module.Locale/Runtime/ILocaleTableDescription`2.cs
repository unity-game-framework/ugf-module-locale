using UGF.Module.Descriptions.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTableDescription<TDescription, TValue> : IDescriptionTable<TDescription>, ILocaleTableDescription<TValue> where TDescription : ILocaleTableEntryDescription<TValue>
    {
    }
}
