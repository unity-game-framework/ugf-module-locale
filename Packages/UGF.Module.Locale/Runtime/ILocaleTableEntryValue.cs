using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTableEntryValue
    {
        GlobalId LocaleId { get; }
        object Value { get; }
    }
}
