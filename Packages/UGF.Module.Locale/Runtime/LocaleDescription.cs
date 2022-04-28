using System.Globalization;
using UGF.Description.Runtime;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleDescription : DescriptionBase
    {
        public string DisplayName { get; set; }
        public SystemLanguage SystemLanguage { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}
