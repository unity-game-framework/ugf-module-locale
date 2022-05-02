namespace UGF.Module.Locale.Runtime
{
    public interface ILocaleTableEntry
    {
        string Id { get; }
        string Name { get; }

        bool TryGetValue(string key, out object value);
    }
}
