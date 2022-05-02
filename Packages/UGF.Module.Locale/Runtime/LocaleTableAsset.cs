using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleTableAsset : ScriptableObject
    {
        public ILocaleTable GetTable()
        {
            return OnGetTable();
        }

        protected abstract ILocaleTable OnGetTable();
    }
}
