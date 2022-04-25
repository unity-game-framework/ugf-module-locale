using System;
using UGF.Application.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public class LocaleModule : ApplicationModule<LocaleModuleDescription>
    {
        public LocaleModule(LocaleModuleDescription description, IApplication application) : base(description, application)
        {
        }

        public bool TryGet(string key, out object value)
        {
            throw new Exception();
        }
    }
}
