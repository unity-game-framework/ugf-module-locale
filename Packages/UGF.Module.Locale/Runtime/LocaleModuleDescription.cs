using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Descriptions.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModuleDescription : ApplicationModuleDescription
    {
        public GlobalId DefaultLocaleId { get; }
        public bool SelectLocaleBySystemLanguageOnInitialize { get; }
        public IReadOnlyDictionary<GlobalId, LocaleDescription> Locales { get; }
        public IReadOnlyDictionary<GlobalId, IDescriptionTable> Tables { get; }
        public IReadOnlyList<GlobalId> PreloadTablesAsync { get; }

        public LocaleModuleDescription(
            GlobalId defaultLocaleId,
            bool selectLocaleBySystemLanguageOnInitialize,
            IReadOnlyDictionary<GlobalId, LocaleDescription> locales,
            IReadOnlyDictionary<GlobalId, IDescriptionTable> tables,
            IReadOnlyList<GlobalId> preloadTablesAsync)
        {
            if (!defaultLocaleId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(defaultLocaleId));

            DefaultLocaleId = defaultLocaleId;
            SelectLocaleBySystemLanguageOnInitialize = selectLocaleBySystemLanguageOnInitialize;
            Locales = locales ?? throw new ArgumentNullException(nameof(locales));
            Tables = tables ?? throw new ArgumentNullException(nameof(tables));
            PreloadTablesAsync = preloadTablesAsync ?? throw new ArgumentNullException(nameof(preloadTablesAsync));
        }
    }
}
