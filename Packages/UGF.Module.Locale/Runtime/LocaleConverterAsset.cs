using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    public abstract class LocaleConverterAsset : ScriptableObject
    {
        public void Convert()
        {
            OnConvert();
        }

        protected abstract void OnConvert();
    }
}
