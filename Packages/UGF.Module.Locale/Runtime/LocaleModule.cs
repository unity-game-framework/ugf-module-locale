using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModule : ApplicationModule<LocaleModuleDescription>
    {
        public IProvider<string, LocaleEntriesDescription> Entries { get; }
        public ICollection<string> Locales { get { return m_locales.Keys; } }
        public string CurrentLocaleId { get { return HasCurrentLocale ? m_currentLocaleId : throw new ArgumentException("Value not specified."); } }
        public LocaleDescription CurrentLocale { get { return Description.Locales[CurrentLocaleId]; } }
        public bool HasCurrentLocale { get { return !string.IsNullOrEmpty(m_currentLocaleId); } }

        protected IAssetModule AssetModule { get; }

        private readonly Dictionary<string, HashSet<string>> m_locales = new Dictionary<string, HashSet<string>>();
        private readonly Dictionary<string, LocaleEntriesDescriptionAsset> m_assets = new Dictionary<string, LocaleEntriesDescriptionAsset>();
        private string m_currentLocaleId;

        public LocaleModule(LocaleModuleDescription description, IApplication application) : this(description, application, new Provider<string, LocaleEntriesDescription>())
        {
        }

        public LocaleModule(LocaleModuleDescription description, IApplication application, IProvider<string, LocaleEntriesDescription> entries) : base(description, application)
        {
            Entries = entries ?? throw new ArgumentNullException(nameof(entries));

            AssetModule = Application.GetModule<IAssetModule>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            if (!string.IsNullOrEmpty(Description.DefaultLocaleId))
            {
                SetCurrentLocale(Description.DefaultLocaleId);
            }

            foreach ((string key, LocaleEntriesDescription value) in Description.Entries)
            {
                Entries.Add(key, value);
            }

            foreach ((_, LocaleGroupDescription value) in Description.Groups)
            {
                AddEntries(value);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            ClearCurrentLocale();

            Entries.Clear();
            m_locales.Clear();
        }

        public void SetCurrentLocale(string localeId)
        {
            if (string.IsNullOrEmpty(localeId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeId));
            if (!Description.Locales.ContainsKey(localeId)) throw new ArgumentException($"Locale description not found by the specified id: '{localeId}'.");

            m_currentLocaleId = localeId;
        }

        public void ClearCurrentLocale()
        {
            m_currentLocaleId = string.Empty;
        }

        public async Task LoadAsync(string localeId, string localeGroupId)
        {
            if (string.IsNullOrEmpty(localeId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeId));
            if (string.IsNullOrEmpty(localeGroupId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeGroupId));
            if (!Description.Groups.TryGetValue(localeGroupId, out LocaleGroupDescription description)) throw new ArgumentException($"Locale group not found by the specified id: '{localeGroupId}'.");
            if (!description.Entries.TryGetValue(localeId, out HashSet<string> entries)) throw new ArgumentException($"Entries not found by the specified locale id: '{localeId}'.");

            foreach (string id in entries)
            {
                await LoadAsync(id);
            }
        }

        public async Task<LocaleEntriesDescription> LoadAsync(string entriesId)
        {
            if (string.IsNullOrEmpty(entriesId)) throw new ArgumentException("Value cannot be null or empty.", nameof(entriesId));

            var asset = await AssetModule.LoadAsync<LocaleEntriesDescriptionAsset>(entriesId);
            LocaleEntriesDescription description = asset.Build();

            m_assets.Add(entriesId, asset);
            Entries.Add(entriesId, description);

            return description;
        }

        public async Task UnloadAsync(string localeId, string localeGroupId)
        {
            if (string.IsNullOrEmpty(localeId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeId));
            if (string.IsNullOrEmpty(localeGroupId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeGroupId));
            if (!Description.Groups.TryGetValue(localeGroupId, out LocaleGroupDescription description)) throw new ArgumentException($"Locale group not found by the specified id: '{localeGroupId}'.");
            if (!description.Entries.TryGetValue(localeId, out HashSet<string> entries)) throw new ArgumentException($"Entries not found by the specified locale id: '{localeId}'.");

            foreach (string id in entries)
            {
                await UnloadAsync(id);
            }
        }

        public async Task<bool> UnloadAsync(string entriesId)
        {
            if (string.IsNullOrEmpty(entriesId)) throw new ArgumentException("Value cannot be null or empty.", nameof(entriesId));

            if (m_assets.TryGetValue(entriesId, out LocaleEntriesDescriptionAsset asset))
            {
                m_assets.Remove(entriesId);
                Entries.Remove(entriesId);

                await AssetModule.UnloadAsync(entriesId, asset);

                return true;
            }

            return false;
        }

        public void AddEntries(LocaleGroupDescription description)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));

            foreach ((string key, HashSet<string> value) in description.Entries)
            {
                foreach (string id in value)
                {
                    AddEntries(key, id);
                }
            }
        }

        public void AddEntries(string localeId, string entriesId)
        {
            if (string.IsNullOrEmpty(localeId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeId));
            if (string.IsNullOrEmpty(entriesId)) throw new ArgumentException("Value cannot be null or empty.", nameof(entriesId));

            if (!m_locales.TryGetValue(localeId, out HashSet<string> collection))
            {
                collection = new HashSet<string>();

                m_locales.Add(localeId, collection);
            }

            collection.Add(entriesId);
        }

        public bool RemoveEntries(LocaleGroupDescription description)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));

            bool result = false;

            foreach ((string key, HashSet<string> value) in description.Entries)
            {
                foreach (string id in value)
                {
                    if (RemoveEntries(key, id))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public bool RemoveEntries(string localeId, string entriesId)
        {
            if (string.IsNullOrEmpty(localeId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeId));
            if (string.IsNullOrEmpty(entriesId)) throw new ArgumentException("Value cannot be null or empty.", nameof(entriesId));

            if (m_locales.TryGetValue(localeId, out HashSet<string> collection) && collection.Remove(entriesId))
            {
                if (collection.Count == 0)
                {
                    m_locales.Remove(localeId);
                }

                return true;
            }

            return false;
        }

        public ICollection<string> GetEntries(string localeId)
        {
            return TryGetEntries(localeId, out ICollection<string> entries) ? entries : throw new ArgumentException($"Entries not found by the specified locale id: '{localeId}'.");
        }

        public bool TryGetEntries(string localeId, out ICollection<string> entries)
        {
            if (string.IsNullOrEmpty(localeId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeId));

            if (m_locales.TryGetValue(localeId, out HashSet<string> collection))
            {
                entries = collection;
                return true;
            }

            entries = default;
            return false;
        }

        public T Get<T>(string key)
        {
            return Get<T>(CurrentLocaleId, key);
        }

        public T Get<T>(string localeId, string key)
        {
            return (T)Get(localeId, key);
        }

        public object Get(string key)
        {
            return Get(CurrentLocaleId, key);
        }

        public object Get(string localeId, string key)
        {
            return TryGet(localeId, key, out object value) ? value : throw new ArgumentException($"Value not found by the specified locale id and key: '{localeId}', '{key}'.");
        }

        public bool TryGet<T>(string key, out T value)
        {
            return TryGet(CurrentLocaleId, key, out value);
        }

        public bool TryGet<T>(string localeId, string key, out T value)
        {
            if (TryGet(localeId, key, out object result))
            {
                value = (T)result;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGet(string key, out object value)
        {
            return TryGet(CurrentLocaleId, key, out value);
        }

        public bool TryGet(string localeId, string key, out object value)
        {
            if (string.IsNullOrEmpty(localeId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeId));
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));

            if (m_locales.TryGetValue(localeId, out HashSet<string> collection))
            {
                foreach (string id in collection)
                {
                    if (Entries.TryGet(id, out LocaleEntriesDescription entries) && entries.Values.TryGetValue(key, out value))
                    {
                        return true;
                    }
                }
            }

            value = default;
            return false;
        }
    }
}
