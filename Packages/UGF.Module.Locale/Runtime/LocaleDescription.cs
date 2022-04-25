using UGF.Description.Runtime;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleDescription : DescriptionBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public SystemLanguage SystemLanguage { get; set; }
    }
}
