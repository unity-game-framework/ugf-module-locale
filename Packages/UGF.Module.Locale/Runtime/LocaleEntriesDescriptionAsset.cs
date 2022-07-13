using System;
using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleEntriesDescriptionAsset : BuilderAsset<LocaleEntriesDescription>
    {
        public IDictionary<GlobalId, object> GetValues()
        {
            return OnGetValues();
        }

        public void SetValues(IDictionary<GlobalId, object> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            OnSetValues(values);
        }

        protected abstract IDictionary<GlobalId, object> OnGetValues();
        protected abstract void OnSetValues(IDictionary<GlobalId, object> values);
    }
}
