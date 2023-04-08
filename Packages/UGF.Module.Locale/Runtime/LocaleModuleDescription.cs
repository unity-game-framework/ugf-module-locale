using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModuleDescription : ApplicationModuleDescription
    {
        public GlobalId DefaultLocaleId { get; }
        public bool UnloadEntriesOnUninitialize { get; }
        public IReadOnlyDictionary<GlobalId, LocaleDescription> Locales { get; }
        public IReadOnlyDictionary<GlobalId, LocaleTableDescription> Tables { get; }
        public IReadOnlyList<GlobalId> PreloadTablesAsync { get; }

        public LocaleModuleDescription(
            Type registerType,
            GlobalId defaultLocaleId,
            bool unloadEntriesOnUninitialize,
            IReadOnlyDictionary<GlobalId, LocaleDescription> locales,
            IReadOnlyDictionary<GlobalId, LocaleTableDescription> tables,
            IReadOnlyList<GlobalId> preloadTablesAsync) : base(registerType)
        {
            if (!defaultLocaleId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(defaultLocaleId));

            DefaultLocaleId = defaultLocaleId;
            UnloadEntriesOnUninitialize = unloadEntriesOnUninitialize;
            Locales = locales ?? throw new ArgumentNullException(nameof(locales));
            Tables = tables ?? throw new ArgumentNullException(nameof(tables));
            PreloadTablesAsync = preloadTablesAsync ?? throw new ArgumentNullException(nameof(preloadTablesAsync));
        }
    }
}
