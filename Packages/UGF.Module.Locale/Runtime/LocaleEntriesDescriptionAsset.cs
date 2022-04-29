using System;
using System.Collections.Generic;
using UGF.Builder.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleEntriesDescriptionAsset : BuilderAsset<LocaleEntriesDescription>
    {
        public void Collect(IDictionary<string, object> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            OnCollect(values);
        }

        public void Setup(IDictionary<string, object> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            OnSetup(values);
        }

        protected override LocaleEntriesDescription OnBuild()
        {
            var description = new LocaleEntriesDescription();

            OnCollect(description.Values);

            return description;
        }

        protected abstract void OnCollect(IDictionary<string, object> values);
        protected abstract void OnSetup(IDictionary<string, object> values);
    }
}
