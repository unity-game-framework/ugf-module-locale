using System;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleTableAsset : ScriptableObject
    {
        public ILocaleTable GetTable()
        {
            return OnGetTable();
        }

        public void SetTable(ILocaleTable table)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));

            OnSetTable(table);
        }

        protected abstract ILocaleTable OnGetTable();
        protected abstract void OnSetTable(ILocaleTable table);
    }
}
