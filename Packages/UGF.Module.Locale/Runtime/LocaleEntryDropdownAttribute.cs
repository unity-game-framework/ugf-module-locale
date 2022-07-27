using System;
using UGF.RuntimeTools.Runtime.Tables;

namespace UGF.Module.Locale.Runtime
{
    [AttributeUsage(AttributeTargets.Field)]
    public class LocaleEntryDropdownAttribute : TableEntryDropdownAttribute
    {
        public LocaleEntryDropdownAttribute() : base(typeof(LocaleTableAsset))
        {
        }
    }
}
