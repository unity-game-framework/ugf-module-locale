using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleConverter : ScriptableObject
    {
        public void Convert()
        {
            OnConvert();
        }

        protected abstract void OnConvert();
    }
}
