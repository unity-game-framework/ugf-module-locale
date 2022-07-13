using System;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Locale.Runtime
{
    public readonly struct LocaleScope : IDisposable
    {
        private readonly LocaleModule m_localeModule;
        private readonly GlobalId m_localeId;

        public LocaleScope(IApplication application, GlobalId localeId) : this(application.GetModule<LocaleModule>(), localeId)
        {
        }

        public LocaleScope(LocaleModule localeModule, GlobalId localeId)
        {
            if (!localeId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(localeId));

            m_localeModule = localeModule ?? throw new ArgumentNullException(nameof(localeModule));
            m_localeId = localeModule.HasCurrentLocale ? localeModule.CurrentLocaleId : GlobalId.Empty;

            localeModule.SetCurrentLocale(localeId);
        }

        public void Dispose()
        {
            if (m_localeId != GlobalId.Empty)
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
