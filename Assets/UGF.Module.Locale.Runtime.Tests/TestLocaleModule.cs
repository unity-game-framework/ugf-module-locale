using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

namespace UGF.Module.Locale.Runtime.Tests
{
    public class TestLocaleModule
    {
        [Test]
        public async Task InitializeAndUninitialize()
        {
            var launcher = new TestApplicationLauncher();

            await launcher.CreateAsync();

            var module = launcher.Application.GetModule<LocaleModule>();

            Assert.NotNull(module);

            launcher.Destroy();
        }

        [Test]
        public async Task Get()
        {
            var launcher = new TestApplicationLauncher();

            await launcher.CreateAsync();

            var module = launcher.Application.GetModule<LocaleModule>();

            Assert.NotNull(module);

            string result1 = module.GetEntry<string>("610ebbd624ee4e439c6542b9f5831243");
            string result2 = module.GetEntry<string>("6601f5ddda034d57b31059fb142bfde5");
            var result3 = module.GetEntry<GameObject>("e8ac7712ff494f0a9fc4bbc5b1b81c60");

            Assert.AreEqual("Good morning!", result1);
            Assert.AreEqual("Goodbye", result2);
            Assert.AreEqual("LocalePrefabEnglish", result3.name);
            Assert.False(module.TryGetEntry("f407829a43364b76bca9d7da83d57822", out _));

            await module.LoadTableAsync("1a51c9decbea6f542989e54a2014bd36");

            string result4 = module.GetEntry<string>("f407829a43364b76bca9d7da83d57822");

            Assert.AreEqual("Resources", result4);

            await module.UnloadTableAsync("1a51c9decbea6f542989e54a2014bd36");

            Assert.False(module.TryGetEntry<string>("f407829a43364b76bca9d7da83d57822", out _));

            await module.LoadTableAsync("47a3846001472074fa3f2d641cb11ae6", "1a51c9decbea6f542989e54a2014bd36");
            await module.LoadTableAsync("97b2faba4064dc4419593e7ce69606c8", "1a51c9decbea6f542989e54a2014bd36");
            await module.LoadTableAsync("47a3846001472074fa3f2d641cb11ae6", "91cbc8881bde7364491096a5970f6599");
            await module.LoadTableAsync("97b2faba4064dc4419593e7ce69606c8", "91cbc8881bde7364491096a5970f6599");
            await module.LoadTableAsync("47a3846001472074fa3f2d641cb11ae6", "01a4c53fe3e3b824f87b9079ebe6204e");
            await module.LoadTableAsync("97b2faba4064dc4419593e7ce69606c8", "01a4c53fe3e3b824f87b9079ebe6204e");

            using (new LocaleScope(module, "47a3846001472074fa3f2d641cb11ae6"))
            {
                Assert.AreEqual("Bonjour!", module.GetEntry<string>("610ebbd624ee4e439c6542b9f5831243"));
                Assert.AreEqual("Au revoir", module.GetEntry<string>("6601f5ddda034d57b31059fb142bfde5"));
                Assert.AreEqual("LocalePrefabFrench", module.GetEntry<GameObject>("e8ac7712ff494f0a9fc4bbc5b1b81c60").name);
                Assert.AreEqual("Ressources", module.GetEntry<string>("f407829a43364b76bca9d7da83d57822"));
            }

            using (new LocaleScope(module, "97b2faba4064dc4419593e7ce69606c8"))
            {
                Assert.AreEqual("早上好！", module.GetEntry<string>("610ebbd624ee4e439c6542b9f5831243"));
                Assert.AreEqual("再见", module.GetEntry<string>("6601f5ddda034d57b31059fb142bfde5"));
                Assert.AreEqual("LocalePrefabChinese", module.GetEntry<GameObject>("e8ac7712ff494f0a9fc4bbc5b1b81c60").name);
                Assert.AreEqual("资源", module.GetEntry<string>("f407829a43364b76bca9d7da83d57822"));
            }

            launcher.Destroy();
        }
    }
}
