using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UGF.Module.Locale.Runtime;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    public abstract class LocaleTableAsset<TValue> : LocaleTableAsset
    {
        [SerializeField] private List<Entry> m_entries = new List<Entry>();

        public List<Entry> Entries { get { return m_entries; } }

        [Serializable]
        public class Entry
        {
            [SerializeField] private string m_id;
            [SerializeField] private string m_name;
            [SerializeField] private List<EntryValue> m_values = new List<EntryValue>();

            public string Id { get { return m_id; } set { m_id = value; } }
            public string Name { get { return m_name; } set { m_name = value; } }
            public List<EntryValue> Values { get { return m_values; } }
        }

        [Serializable]
        public class EntryValue
        {
            [AssetGuid(typeof(LocaleDescriptionAsset))]
            [SerializeField] private string m_locale;
            [SerializeField] private TValue m_value;

            public string LocaleId { get { return m_locale; } set { m_locale = value; } }
            public TValue Value { get { return m_value; } set { m_value = value; } }
        }

        protected override IReadOnlyList<(string Id, string Name)> OnGetEntries()
        {
            var result = new List<(string Id, string Name)>();

            for (int i = 0; i < m_entries.Count; i++)
            {
                Entry entry = m_entries[i];

                result.Add((entry.Id, entry.Name));
            }

            return result;
        }

        protected override IDictionary<string, IDictionary<string, object>> OnGetData()
        {
            var result = new Dictionary<string, IDictionary<string, object>>();

            for (int i = 0; i < m_entries.Count; i++)
            {
                Entry entry = m_entries[i];

                for (int j = 0; j < entry.Values.Count; j++)
                {
                    EntryValue value = entry.Values[j];

                    if (!result.TryGetValue(value.LocaleId, out IDictionary<string, object> values))
                    {
                        values = new Dictionary<string, object>();

                        result.Add(value.LocaleId, values);
                    }

                    values.Add(entry.Id, value.Value);
                }
            }

            return result;
        }
    }
}
