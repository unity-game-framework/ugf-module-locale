using UGF.Module.Descriptions.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleTableDescription<TDescription, TValue> : DescriptionTable<TDescription> where TDescription : ILocaleTableEntryDescription<TValue>
    {
    }
}
