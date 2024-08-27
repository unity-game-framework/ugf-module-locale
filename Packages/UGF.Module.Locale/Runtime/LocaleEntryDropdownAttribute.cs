using System;
using UGF.Tables.Runtime;

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
