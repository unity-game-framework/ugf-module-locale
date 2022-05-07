using System;
using System.Collections.Generic;
using UGF.Builder.Runtime;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleEntriesDescriptionAsset : BuilderAsset<LocaleEntriesDescription>
    {
        public IDictionary<string, object> GetValues()
        {
            return OnGetValues();
        }

        public void SetValues(IDictionary<string, object> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            OnSetValues(values);
        }

        protected abstract IDictionary<string, object> OnGetValues();
        protected abstract void OnSetValues(IDictionary<string, object> values);
    }
}
