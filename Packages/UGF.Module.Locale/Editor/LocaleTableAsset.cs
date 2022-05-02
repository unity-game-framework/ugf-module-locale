using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.Locale.Editor
{
    public abstract class LocaleTableAsset : ScriptableObject
    {
        public IReadOnlyList<(string Id, string Name)> GetEntries()
        {
            return OnGetEntries();
        }

        public IDictionary<string, IDictionary<string, object>> GetData()
        {
            return OnGetData();
        }

        protected abstract IReadOnlyList<(string Id, string Name)> OnGetEntries();
        protected abstract IDictionary<string, IDictionary<string, object>> OnGetData();
    }
}
