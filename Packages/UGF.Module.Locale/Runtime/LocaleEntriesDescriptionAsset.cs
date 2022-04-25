using System.Collections.Generic;
using UGF.Builder.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleEntriesDescriptionAsset : BuilderAsset<LocaleEntriesDescription>
    {
        protected override LocaleEntriesDescription OnBuild()
        {
            var description = new LocaleEntriesDescription();

            OnCollect(description.Values);

            return description;
        }

        protected abstract void OnCollect(IDictionary<string, object> values);
    }
}
