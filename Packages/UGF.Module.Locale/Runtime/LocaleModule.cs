using System;
using System.Globalization;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Logs.Runtime;
using UGF.Module.Descriptions.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModule : ApplicationModuleAsync<LocaleModuleDescription>
    {
        public Provider<GlobalId, LocaleDescription> Locales { get; } = new Provider<GlobalId, LocaleDescription>();
        public Provider<GlobalId, IDescriptionTable> Tables { get; } = new Provider<GlobalId, IDescriptionTable>();
        public GlobalId CurrentLocaleId { get { return HasCurrentLocale ? m_currentLocaleId : throw new ArgumentException("Value not specified."); } }
        public bool HasCurrentLocale { get { return m_currentLocaleId != GlobalId.Empty; } }

        protected ILog Logger { get; }
        protected IDescriptionModule DescriptionModule { get; }

        private GlobalId m_currentLocaleId;

        public LocaleModule(LocaleModuleDescription description, IApplication application) : base(description, application)
        {
            Logger = Log.CreateWithLabel<LocaleModule>();
            DescriptionModule = Application.GetModule<IDescriptionModule>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach ((GlobalId id, LocaleDescription description) in Description.Locales)
            {
                Locales.Add(id, description);
            }

            foreach ((GlobalId id, IDescriptionTable table) in Description.Tables)
            {
                Tables.Add(id, table);
            }

            if (Description.SelectLocaleBySystemLanguageOnInitialize
                && TryGetLocaleBySystemLanguage(UnityEngine.Application.systemLanguage, out GlobalId localeId, out _))
            {
                SetCurrentLocale(localeId);
            }
            else
            {
                SetCurrentLocale(Description.DefaultLocaleId);
            }

            Logger.Debug("Initialized", new
            {
                CurrentLocaleId,
                locales = Description.Locales.Count,
                tables = Description.Tables.Count
            });
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            Logger.Debug("Initialize async", new
            {
                preloadTablesAsync = Description.PreloadTablesAsync.Count
            });

            for (int i = 0; i < Description.PreloadTablesAsync.Count; i++)
            {
                GlobalId id = Description.PreloadTablesAsync[i];

                IDescriptionTable table = await DescriptionModule.LoadTableAsync(id);

                Tables.Add(id, table);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Logger.Debug("Uninitialize", new
            {
                locales = Locales.Entries.Count,
                tables = Tables.Entries.Count
            });

            ClearCurrentLocale();

            Locales.Clear();
            Tables.Clear();
        }

        public LocaleDescription GetCurrentLocale()
        {
            return Locales.Get(CurrentLocaleId);
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

        public T GetValue<T>(GlobalId entryId)
        {
            return GetValue<T>(GetCurrentLocale(), entryId);
        }

        public T GetValue<T>(LocaleDescription locale, GlobalId entryId)
        {
            return TryGetValue(locale, entryId, out T value) ? value : throw new ArgumentException($"Locale value not found by the specified locale and entry id: '{locale.DisplayName}', '{entryId}'.");
        }

        public bool TryGetValue<T>(GlobalId entryId, out T value)
        {
            return TryGetValue(GetCurrentLocale(), entryId, out value);
        }

        public bool TryGetValue<T>(LocaleDescription locale, GlobalId entryId, out T value)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));
            if (!entryId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(entryId));

            foreach ((_, IDescriptionTable descriptionTable) in Tables)
            {
                if (descriptionTable is ILocaleTableDescription<T> table && table.TryGetValue(locale, entryId, out value))
                {
                    return true;
                }
            }

            value = default;
            return false;
        }

        public bool TryGetLocaleBySystemLanguage(SystemLanguage language, out GlobalId id, out LocaleDescription description)
        {
            foreach ((GlobalId localeId, LocaleDescription localeDescription) in Locales.Entries)
            {
                if (localeDescription.SystemLanguage == language)
                {
                    id = localeId;
                    description = localeDescription;
                    return true;
                }
            }

            id = default;
            description = default;
            return false;
        }

        public bool TryGetLocaleByCultureInfo(CultureInfo cultureInfo, out GlobalId id, out LocaleDescription description)
        {
            if (cultureInfo == null) throw new ArgumentNullException(nameof(cultureInfo));

            foreach ((GlobalId localeId, LocaleDescription localeDescription) in Locales.Entries)
            {
                if (localeDescription.CultureInfo.Equals(cultureInfo))
                {
                    id = localeId;
                    description = localeDescription;
                    return true;
                }
            }

            id = default;
            description = default;
            return false;
        }
    }
}
