using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModule : ApplicationModule<LocaleModuleDescription>, IApplicationModuleAsync
    {
        public IProvider<string, LocaleDescription> Locales { get; } = new Provider<string, LocaleDescription>();
        public IProvider<string, LocaleTableDescription> Tables { get; } = new Provider<string, LocaleTableDescription>();
        public IProvider<string, LocaleEntriesDescription> Entries { get; } = new Provider<string, LocaleEntriesDescription>();
        public IProvider<string, HashSet<string>> EntriesByLocale { get; } = new Provider<string, HashSet<string>>();
        public string CurrentLocaleId { get { return HasCurrentLocale ? m_currentLocaleId : throw new ArgumentException("Value not specified."); } }
        public bool HasCurrentLocale { get { return !string.IsNullOrEmpty(m_currentLocaleId); } }

        protected IAssetModule AssetModule { get; }

        private string m_currentLocaleId;

        public LocaleModule(LocaleModuleDescription description, IApplication application) : base(description, application)
        {
            AssetModule = Application.GetModule<IAssetModule>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Tables.Added += OnTableAdded;
            Tables.Removed += OnTableRemoved;

            foreach ((string key, IBuilder<LocaleDescription> value) in Description.Locales)
            {
                Locales.Add(key, value.Build());
            }

            foreach ((string key, IBuilder<LocaleTableDescription> value) in Description.Tables)
            {
                Tables.Add(key, value.Build());
            }

            SetCurrentLocale(Description.DefaultLocaleId);
        }

        public async Task InitializeAsync()
        {
            foreach (string tableId in Description.PreloadTablesAsync)
            {
                await LoadTableAsync(tableId);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            if (Description.UnloadEntriesOnUninitialize)
            {
                while (Entries.Entries.Count > 0)
                {
                    string id = Entries.Entries.Keys.First();

                    UnloadEntries(id);
                }
            }

            Tables.Added -= OnTableAdded;
            Tables.Removed -= OnTableRemoved;

            ClearCurrentLocale();

            Locales.Clear();
            Tables.Clear();
            Entries.Clear();
            EntriesByLocale.Clear();
        }

        public void SetCurrentLocale(string localeId)
        {
            if (string.IsNullOrEmpty(localeId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeId));
            if (!Locales.TryGet(localeId, out _)) throw new ArgumentException($"Locale not found by the specified id: '{localeId}'.");

            m_currentLocaleId = localeId;
        }

        public void ClearCurrentLocale()
        {
            m_currentLocaleId = string.Empty;
        }

        public void LoadTable(string tableId)
        {
            LoadTable(CurrentLocaleId, tableId);
        }

        public void LoadTable(string localeId, string tableId)
        {
            LoadEntries(GetEntriesId(localeId, tableId));
        }

        public Task LoadTableAsync(string tableId)
        {
            return LoadTableAsync(CurrentLocaleId, tableId);
        }

        public Task LoadTableAsync(string localeId, string tableId)
        {
            return LoadEntriesAsync(GetEntriesId(localeId, tableId));
        }

        public bool UnloadTable(string tableId)
        {
            return UnloadTable(CurrentLocaleId, tableId);
        }

        public bool UnloadTable(string localeId, string tableId)
        {
            return UnloadEntries(GetEntriesId(localeId, tableId));
        }

        public Task<bool> UnloadTableAsync(string localeId, string tableId)
        {
            return UnloadEntriesAsync(GetEntriesId(localeId, tableId));
        }

        public void LoadTableAll(string tableId)
        {
            LocaleTableDescription table = Tables.Get(tableId);

            foreach (string entriesId in table.Entries.Values)
            {
                LoadEntries(entriesId);
            }
        }

        public async Task LoadTableAllAsync(string tableId)
        {
            LocaleTableDescription table = Tables.Get(tableId);

            foreach (string entriesId in table.Entries.Values)
            {
                await LoadEntriesAsync(entriesId);
            }
        }

        public void UnloadTableAll(string tableId)
        {
            LocaleTableDescription table = Tables.Get(tableId);

            foreach (string entriesId in table.Entries.Values)
            {
                UnloadEntries(entriesId);
            }
        }

        public async Task UnloadTableAllAsync(string tableId)
        {
            LocaleTableDescription table = Tables.Get(tableId);

            foreach (string entriesId in table.Entries.Values)
            {
                await UnloadEntriesAsync(entriesId);
            }
        }

        public void LoadEntries(string entriesId)
        {
            if (string.IsNullOrEmpty(entriesId)) throw new ArgumentException("Value cannot be null or empty.", nameof(entriesId));
            if (Entries.Entries.ContainsKey(entriesId)) throw new ArgumentException($"Entries already loaded by the specified id: '{entriesId}'.");

            var asset = AssetModule.Load<LocaleEntriesDescriptionAsset>(entriesId);
            LocaleEntriesDescription description = asset.Build();

            Entries.Add(entriesId, description);
        }

        public async Task LoadEntriesAsync(string entriesId)
        {
            if (string.IsNullOrEmpty(entriesId)) throw new ArgumentException("Value cannot be null or empty.", nameof(entriesId));
            if (Entries.Entries.ContainsKey(entriesId)) throw new ArgumentException($"Entries already loaded by the specified id: '{entriesId}'.");

            var asset = await AssetModule.LoadAsync<LocaleEntriesDescriptionAsset>(entriesId);
            LocaleEntriesDescription description = asset.Build();

            Entries.Add(entriesId, description);
        }

        public bool UnloadEntries(string entriesId)
        {
            if (string.IsNullOrEmpty(entriesId)) throw new ArgumentException("Value cannot be null or empty.", nameof(entriesId));

            if (Entries.Entries.ContainsKey(entriesId))
            {
                Entries.Remove(entriesId);

                object asset = AssetModule.Tracker.Get(entriesId).Asset;

                AssetModule.Unload(entriesId, asset);

                return true;
            }

            return false;
        }

        public async Task<bool> UnloadEntriesAsync(string entriesId)
        {
            if (string.IsNullOrEmpty(entriesId)) throw new ArgumentException("Value cannot be null or empty.", nameof(entriesId));

            if (Entries.Entries.ContainsKey(entriesId))
            {
                Entries.Remove(entriesId);

                object asset = AssetModule.Tracker.Get(entriesId).Asset;

                await AssetModule.UnloadAsync(entriesId, asset);

                return true;
            }

            return false;
        }

        public string GetEntriesId(string localeId, string tableId)
        {
            return TryGetEntriesId(localeId, tableId, out string entriesId) ? entriesId : throw new ArgumentException($"Entries id not found by the specified locale and table id: '{tableId}', '{localeId}'.");
        }

        public bool TryGetEntriesId(string localeId, string tableId, out string entriesId)
        {
            if (string.IsNullOrEmpty(localeId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeId));

            LocaleTableDescription table = Tables.Get(tableId);

            return table.Entries.TryGetValue(localeId, out entriesId);
        }

        public T GetEntry<T>(string id)
        {
            return GetEntry<T>(CurrentLocaleId, id);
        }

        public T GetEntry<T>(string localeId, string id)
        {
            return (T)GetEntry(localeId, id);
        }

        public object GetEntry(string id)
        {
            return GetEntry(CurrentLocaleId, id);
        }

        public object GetEntry(string localeId, string id)
        {
            return TryGetEntry(localeId, id, out object value) ? value : throw new ArgumentException($"Entry not found by the specified locale and id: '{localeId}', '{id}'.");
        }

        public bool TryGetEntry<T>(string id, out T value)
        {
            return TryGetEntry(CurrentLocaleId, id, out value);
        }

        public bool TryGetEntry<T>(string localeId, string id, out T value)
        {
            if (TryGetEntry(localeId, id, out object result))
            {
                value = (T)result;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGetEntry(string id, out object value)
        {
            return TryGetEntry(CurrentLocaleId, id, out value);
        }

        public bool TryGetEntry(string localeId, string id, out object value)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            if (EntriesByLocale.TryGet(localeId, out HashSet<string> ids))
            {
                foreach (string entryId in ids)
                {
                    if (Entries.TryGet(entryId, out LocaleEntriesDescription entries) && entries.Values.TryGetValue(id, out value))
                    {
                        return true;
                    }
                }
            }

            value = default;
            return false;
        }

        private void OnTableAdded(IProvider provider, string id, LocaleTableDescription entry)
        {
            foreach ((string key, string value) in entry.Entries)
            {
                if (!EntriesByLocale.TryGet(key, out HashSet<string> ids))
                {
                    ids = new HashSet<string>();

                    EntriesByLocale.Add(key, ids);
                }

                ids.Add(value);
            }
        }

        private void OnTableRemoved(IProvider provider, string id, LocaleTableDescription entry)
        {
            foreach ((string key, string value) in entry.Entries)
            {
                if (EntriesByLocale.TryGet(key, out HashSet<string> ids) && ids.Remove(value))
                {
                    if (ids.Count == 0)
                    {
                        EntriesByLocale.Remove(key);
                    }
                }
            }
        }
    }
}
