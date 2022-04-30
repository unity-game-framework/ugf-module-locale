using System;
using UGF.Application.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public readonly struct LocaleScope : IDisposable
    {
        private readonly LocaleModule m_localeModule;
        private readonly string m_localeId;

        public LocaleScope(IApplication application, string localeId) : this(application.GetModule<LocaleModule>(), localeId)
        {
        }

        public LocaleScope(LocaleModule localeModule, string localeId)
        {
            if (string.IsNullOrEmpty(localeId)) throw new ArgumentException("Value cannot be null or empty.", nameof(localeId));

            m_localeModule = localeModule ?? throw new ArgumentNullException(nameof(localeModule));
            m_localeId = localeModule.HasCurrentLocale ? localeModule.CurrentLocaleId : string.Empty;

            localeModule.SetCurrentLocale(localeId);
        }

        public void Dispose()
        {
            if (!string.IsNullOrEmpty(m_localeId))
            {
                m_localeModule.SetCurrentLocale(m_localeId);
            }
            else
            {
                m_localeModule.ClearCurrentLocale();
            }
        }
    }
}
