using UGF.RuntimeTools.Runtime.Tables;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleTableAsset<TValue> : LocaleTableAsset
    {
        [SerializeField] private Table<LocaleTableEntry<TValue>> m_table = new Table<LocaleTableEntry<TValue>>();

        public Table<LocaleTableEntry<TValue>> Table { get { return m_table; } }

        protected override ITable OnGet()
        {
            return m_table;
        }

        protected override void OnSet(ITable table)
        {
            m_table = (Table<LocaleTableEntry<TValue>>)table;
        }
    }
}
