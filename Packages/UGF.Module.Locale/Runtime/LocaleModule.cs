using System;
using UGF.Application.Runtime;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModule : ApplicationModule<LocaleModuleDescription>
    {
        public IProvider<string, LocaleEntriesDescription> Entries { get { return m_entries; } }
        public IProvider<string, LocaleCollectionDescription> Locales { get { return m_locales; } }

        private readonly Provider<string, LocaleEntriesDescription> m_entries = new Provider<string, LocaleEntriesDescription>();
        private readonly Provider<string, LocaleCollectionDescription> m_locales = new Provider<string, LocaleCollectionDescription>();

        public LocaleModule(LocaleModuleDescription description, IApplication application) : base(description, application)
        {
        }

        public bool TryGet(string localeId, string key, out object value)
        {
            throw new Exception("");
        }
    }
}
