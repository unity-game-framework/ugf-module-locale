using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Logs.Runtime;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModule : ApplicationModuleAsync<LocaleModuleDescription>
    {
        public IProvider<GlobalId, LocaleDescription> Locales { get; } = new Provider<GlobalId, LocaleDescription>();
        public IProvider<GlobalId, LocaleTableDescription> Tables { get; } = new Provider<GlobalId, LocaleTableDescription>();
        public IProvider<GlobalId, LocaleEntriesDescription> Entries { get; } = new Provider<GlobalId, LocaleEntriesDescription>();
        public IProvider<GlobalId, HashSet<GlobalId>> EntriesByLocale { get; } = new Provider<GlobalId, HashSet<GlobalId>>();
        public GlobalId CurrentLocaleId { get { return HasCurrentLocale ? m_currentLocaleId : throw new ArgumentException("Value not specified."); } }
        public bool HasCurrentLocale { get { return m_currentLocaleId != GlobalId.Empty; } }

        protected IAssetModule AssetModule { get; }

        private GlobalId m_currentLocaleId;

        public LocaleModule(LocaleModuleDescription description, IApplication application) : base(description, application)
        {
            AssetModule = Application.GetModule<IAssetModule>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Tables.Added += OnTableAdded;
            Tables.Removed += OnTableRemoved;

            foreach ((GlobalId key, IBuilder<LocaleDescription> value) in Description.Locales)
            {
                Locales.Add(key, value.Build());
            }

            foreach ((GlobalId key, IBuilder<LocaleTableDescription> value) in Description.Tables)
            {
                Tables.Add(key, value.Build());
            }

            SetCurrentLocale(Description.DefaultLocaleId);

            Log.Debug("Locale Module initialized", new
            {
                defaultLocaleId = Description.DefaultLocaleId,
                locales = Description.Locales.Count,
                tables = Description.Tables.Count
            });
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            Log.Debug("Locale Module initialize async", new
            {
                preloadTablesAsync = Description.PreloadTablesAsync.Count
            });

            foreach (GlobalId tableId in Description.PreloadTablesAsync)
            {
                await LoadTableAsync(tableId);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Log.Debug("Locale Module uninitialize", new
            {
                locales = Locales.Entries.Count,
                tables = Tables.Entries.Count,
                entries = Entries.Entries.Count
            });

            if (Description.UnloadEntriesOnUninitialize)
            {
                while (Entries.Entries.Count > 0)
                {
                    GlobalId id = Entries.Entries.Keys.First();

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

        public void SetCurrentLocale(GlobalId localeId)
        {
            if (!localeId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(localeId));
            if (!Locales.TryGet(localeId, out _)) throw new ArgumentException($"Locale not found by the specified id: '{localeId}'.");

            m_currentLocaleId = localeId;
        }

        public void ClearCurrentLocale()
        {
            m_currentLocaleId = GlobalId.Empty;
        }

        public void LoadTable(GlobalId tableId)
        {
            LoadTable(CurrentLocaleId, tableId);
        }

        public void LoadTable(GlobalId localeId, GlobalId tableId)
        {
            LoadEntries(GetEntriesId(localeId, tableId));
        }

        public Task LoadTableAsync(GlobalId tableId)
        {
            return LoadTableAsync(CurrentLocaleId, tableId);
        }

        public Task LoadTableAsync(GlobalId localeId, GlobalId tableId)
        {
            return LoadEntriesAsync(GetEntriesId(localeId, tableId));
        }

        public bool UnloadTable(GlobalId tableId)
        {
            return UnloadTable(CurrentLocaleId, tableId);
        }

        public bool UnloadTable(GlobalId localeId, GlobalId tableId)
        {
            return UnloadEntries(GetEntriesId(localeId, tableId));
        }

        public Task<bool> UnloadTableAsync(GlobalId tableId)
        {
            return UnloadTableAsync(CurrentLocaleId, tableId);
        }

        public Task<bool> UnloadTableAsync(GlobalId localeId, GlobalId tableId)
        {
            return UnloadEntriesAsync(GetEntriesId(localeId, tableId));
        }

        public void LoadTableAll(GlobalId tableId)
        {
            LocaleTableDescription table = Tables.Get(tableId);

            foreach (GlobalId entriesId in table.Entries.Values)
            {
                LoadEntries(entriesId);
            }
        }

        public async Task LoadTableAllAsync(GlobalId tableId)
        {
            LocaleTableDescription table = Tables.Get(tableId);

            foreach (GlobalId entriesId in table.Entries.Values)
            {
                await LoadEntriesAsync(entriesId);
            }
        }

        public void UnloadTableAll(GlobalId tableId)
        {
            LocaleTableDescription table = Tables.Get(tableId);

            foreach (GlobalId entriesId in table.Entries.Values)
            {
                UnloadEntries(entriesId);
            }
        }

        public async Task UnloadTableAllAsync(GlobalId tableId)
        {
            LocaleTableDescription table = Tables.Get(tableId);

            foreach (GlobalId entriesId in table.Entries.Values)
            {
                await UnloadEntriesAsync(entriesId);
            }
        }

        public void LoadEntries(GlobalId entriesId)
        {
            if (!entriesId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(entriesId));
            if (Entries.Entries.ContainsKey(entriesId)) throw new ArgumentException($"Entries already loaded by the specified id: '{entriesId}'.");

            var asset = AssetModule.Load<LocaleEntriesDescriptionAsset>(entriesId);
            LocaleEntriesDescription description = asset.Build();

            Entries.Add(entriesId, description);
        }

        public async Task LoadEntriesAsync(GlobalId entriesId)
        {
            if (!entriesId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(entriesId));
            if (Entries.Entries.ContainsKey(entriesId)) throw new ArgumentException($"Entries already loaded by the specified id: '{entriesId}'.");

            var asset = await AssetModule.LoadAsync<LocaleEntriesDescriptionAsset>(entriesId);
            LocaleEntriesDescription description = asset.Build();

            Entries.Add(entriesId, description);
        }

        public bool UnloadEntries(GlobalId entriesId)
        {
            if (!entriesId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(entriesId));

            if (Entries.Entries.ContainsKey(entriesId))
            {
                Entries.Remove(entriesId);

                object asset = AssetModule.Tracker.Get(entriesId).Asset;

                AssetModule.Unload(entriesId, asset);

                return true;
            }

            return false;
        }

        public async Task<bool> UnloadEntriesAsync(GlobalId entriesId)
        {
            if (!entriesId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(entriesId));

            if (Entries.Entries.ContainsKey(entriesId))
            {
                Entries.Remove(entriesId);

                object asset = AssetModule.Tracker.Get(entriesId).Asset;

                await AssetModule.UnloadAsync(entriesId, asset);

                return true;
            }

            return false;
        }

        public GlobalId GetEntriesId(GlobalId localeId, GlobalId tableId)
        {
            return TryGetEntriesId(localeId, tableId, out GlobalId entriesId) ? entriesId : throw new ArgumentException($"Entries id not found by the specified locale and table id: '{tableId}', '{localeId}'.");
        }

        public bool TryGetEntriesId(GlobalId localeId, GlobalId tableId, out GlobalId entriesId)
        {
            if (!localeId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(localeId));

            LocaleTableDescription table = Tables.Get(tableId);

            return table.Entries.TryGetValue(localeId, out entriesId);
        }

        public T GetEntry<T>(GlobalId id)
        {
            return GetEntry<T>(CurrentLocaleId, id);
        }

        public T GetEntry<T>(GlobalId localeId, GlobalId id)
        {
            return (T)GetEntry(localeId, id);
        }

        public object GetEntry(GlobalId id)
        {
            return GetEntry(CurrentLocaleId, id);
        }

        public object GetEntry(GlobalId localeId, GlobalId id)
        {
            return TryGetEntry(localeId, id, out object value) ? value : throw new ArgumentException($"Entry not found by the specified locale and id: '{localeId}', '{id}'.");
        }

        public bool TryGetEntry<T>(GlobalId id, out T value)
        {
            return TryGetEntry(CurrentLocaleId, id, out value);
        }

        public bool TryGetEntry<T>(GlobalId localeId, GlobalId id, out T value)
        {
            if (TryGetEntry(localeId, id, out object result))
            {
                value = (T)result;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGetEntry(GlobalId id, out object value)
        {
            return TryGetEntry(CurrentLocaleId, id, out value);
        }

        public bool TryGetEntry(GlobalId localeId, GlobalId id, out object value)
        {
            if (!localeId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(localeId));

            if (EntriesByLocale.TryGet(localeId, out HashSet<GlobalId> ids))
            {
                foreach (GlobalId entryId in ids)
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

        private void OnTableAdded(IProvider provider, GlobalId id, LocaleTableDescription entry)
        {
            foreach ((GlobalId key, GlobalId value) in entry.Entries)
            {
                if (!EntriesByLocale.TryGet(key, out HashSet<GlobalId> ids))
                {
                    ids = new HashSet<GlobalId>();

                    EntriesByLocale.Add(key, ids);
                }

                ids.Add(value);
            }
        }

        private void OnTableRemoved(IProvider provider, GlobalId id, LocaleTableDescription entry)
        {
            foreach ((GlobalId key, GlobalId value) in entry.Entries)
            {
                if (EntriesByLocale.TryGet(key, out HashSet<GlobalId> ids) && ids.Remove(value))
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
