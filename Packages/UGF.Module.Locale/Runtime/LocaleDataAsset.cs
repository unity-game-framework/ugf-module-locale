using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleDataAsset : ScriptableObject
    {
        public void Collect(IDictionary<string, object> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            OnCollect(values);
        }

        protected abstract void OnCollect(IDictionary<string, object> values);
    }
}
