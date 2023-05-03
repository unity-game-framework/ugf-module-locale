using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleTableDescriptionCollectionAsset : ScriptableObject
    {
        public void GetTableDescriptions(IDictionary<GlobalId, LocaleTableDescription> descriptions)
        {
            if (descriptions == null) throw new ArgumentNullException(nameof(descriptions));

            OnGetTableDescriptions(descriptions);
        }

        protected abstract void OnGetTableDescriptions(IDictionary<GlobalId, LocaleTableDescription> descriptions);
    }
}
