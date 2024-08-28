using UGF.Module.Descriptions.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTableEntry<out TDescription, TValue> : IDescriptionTableEntry<TDescription> where TDescription : ILocaleTableEntryDescription<TValue>
    {
    }
}
