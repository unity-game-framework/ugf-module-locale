using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModule : ApplicationModule<LocaleModuleDescription>
    {
        public IProvider<string, LocaleEntriesDescription> Entries { get; }
        public ICollection<string> Locales { get { return m_locales.Keys; } }

        private readonly Dictionary<string, HashSet<string>> m_locales = new Dictionary<string, HashSet<string>>();

        public LocaleModule(LocaleModuleDescription description, IApplication application) : this(description, application, new Provider<string, LocaleEntriesDescription>())
        {
        }

        public LocaleModule(LocaleModuleDescription description, IApplication application, IProvider<string, LocaleEntriesDescription> entries) : base(description, application)
        {
            Entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach ((string key, LocaleEntriesDescription value) in Description.Entries)
            {
                Entries.Add(key, value);
            }

            for (int i = 0; i < Description.Groups.Count; i++)
            {
                AddEntries(Description.Groups[i]);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Entries.Clear();
            m_locales.Clear();
        }

        public void AddEntries(LocaleGroupDescription description)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));

            foreach ((string key, List<string> value) in description.Entries)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    AddEntries(key, value[i]);
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

            foreach ((string key, List<string> value) in description.Entries)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    if (RemoveEntries(key, value[i]))
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

        public T Get<T>(string localeId, string key)
        {
            return (T)Get(localeId, key);
        }

        public object Get(string localeId, string key)
        {
            return TryGet(localeId, key, out object value) ? value : throw new ArgumentException($"Value not found by the specified locale id and key: '{localeId}', '{key}'.");
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
