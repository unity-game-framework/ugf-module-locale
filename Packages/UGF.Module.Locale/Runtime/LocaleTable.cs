using System;
using UGF.RuntimeTools.Runtime.Tables;

namespace UGF.Module.Locale.Runtime
{
    [Serializable]
    public class LocaleTable<TValue> : Table<LocaleTableEntry<TValue>>
    {
    }
}
