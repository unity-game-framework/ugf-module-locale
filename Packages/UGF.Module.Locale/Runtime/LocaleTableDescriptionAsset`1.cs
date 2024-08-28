using UGF.Module.Descriptions.Runtime;
using UGF.Tables.Runtime;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleTableDescriptionAsset<TTableEntry, TValue, TDescription> : LocaleTableDescriptionAsset
        where TTableEntry : ILocaleTableEntry<TDescription, TValue>
        where TDescription : ILocaleTableEntryDescription<TValue>
    {
        [SerializeField] private Table<TTableEntry> m_table = new Table<TTableEntry>();

        public Table<TTableEntry> Table { get { return m_table; } }

        protected override ITable OnGet()
        {
            return m_table;
        }

        protected override void OnSet(ITable table)
        {
            m_table = (Table<TTableEntry>)table;
        }

        protected override IDescriptionTable OnBuild()
        {
            var description = new LocaleTableDescription<TDescription, TValue>();

            for (int i = 0; i < m_table.Entries.Count; i++)
            {
                TTableEntry entry = m_table.Entries[i];

                TDescription entryDescription = entry.Build();

                description.Add(entry.Id, entryDescription);
            }

            return description;
        }
    }
}
