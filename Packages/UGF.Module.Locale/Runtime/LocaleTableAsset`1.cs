using UGF.RuntimeTools.Runtime.Tables;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleTableAsset<TValue> : LocaleTableAsset
    {
        [SerializeField] private LocaleTable<TValue> m_table = new LocaleTable<TValue>();

        public LocaleTable<TValue> Table { get { return m_table; } }

        protected override ITable OnGet()
        {
            return m_table;
        }

        protected override void OnSet(ITable table)
        {
            m_table = (LocaleTable<TValue>)table;
        }
    }
}
