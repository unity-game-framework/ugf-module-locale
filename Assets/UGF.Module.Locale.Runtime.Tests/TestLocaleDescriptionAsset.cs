using System.Globalization;
using NUnit.Framework;
using UnityEngine;

namespace UGF.Module.Locale.Runtime.Tests
{
    public class TestLocaleDescriptionAsset
    {
        [Test]
        public void Build()
        {
            var asset = ScriptableObject.CreateInstance<LocaleDescriptionAsset>();
            LocaleDescription description = asset.Build();

            Assert.NotNull(description.CultureInfo);
            Assert.AreEqual(CultureInfo.InvariantCulture, description.CultureInfo);

            Object.DestroyImmediate(asset);
        }
    }
}
