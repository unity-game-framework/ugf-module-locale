using System.Threading.Tasks;
using NUnit.Framework;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
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

            string result1 = module.GetValue<string>(new GlobalId("92d6e8700ddb42ec9e6aab8cb1b49ebe"));
            string result2 = module.GetValue<string>(new GlobalId("1897460040b244c5b2abaaa04e56fc2b"));
            var result3 = module.GetValue<GameObject>(new GlobalId("a3512a8e8d0241a4b2e7c564d6bc7215"));

            Assert.AreEqual("Good morning!", result1);
            Assert.AreEqual("Goodbye", result2);
            Assert.AreEqual("LocalePrefabEnglish", result3.name);
            Assert.False(module.TryGetValue<string>(new GlobalId("6e843cb42ca8420385a74de9f59314f4"), out _));

            // await module.LoadTableAsync(new GlobalId("1a51c9decbea6f542989e54a2014bd36"));

            string result4 = module.GetValue<string>(new GlobalId("6e843cb42ca8420385a74de9f59314f4"));

            Assert.AreEqual("Resources", result4);

            // await module.UnloadTableAsync(new GlobalId("1a51c9decbea6f542989e54a2014bd36"));

            Assert.False(module.TryGetValue<string>(new GlobalId("6e843cb42ca8420385a74de9f59314f4"), out _));

            // await module.LoadTableAsync(new GlobalId("47a3846001472074fa3f2d641cb11ae6"), new GlobalId("1a51c9decbea6f542989e54a2014bd36"));
            // await module.LoadTableAsync(new GlobalId("97b2faba4064dc4419593e7ce69606c8"), new GlobalId("1a51c9decbea6f542989e54a2014bd36"));
            // await module.LoadTableAsync(new GlobalId("47a3846001472074fa3f2d641cb11ae6"), new GlobalId("91cbc8881bde7364491096a5970f6599"));
            // await module.LoadTableAsync(new GlobalId("97b2faba4064dc4419593e7ce69606c8"), new GlobalId("91cbc8881bde7364491096a5970f6599"));
            // await module.LoadTableAsync(new GlobalId("47a3846001472074fa3f2d641cb11ae6"), new GlobalId("01a4c53fe3e3b824f87b9079ebe6204e"));
            // await module.LoadTableAsync(new GlobalId("97b2faba4064dc4419593e7ce69606c8"), new GlobalId("01a4c53fe3e3b824f87b9079ebe6204e"));

            using (new LocaleScope(module, new GlobalId("47a3846001472074fa3f2d641cb11ae6")))
            {
                Assert.AreEqual("Bonjour!", module.GetValue<string>(new GlobalId("92d6e8700ddb42ec9e6aab8cb1b49ebe")));
                Assert.AreEqual("Au revoir", module.GetValue<string>(new GlobalId("1897460040b244c5b2abaaa04e56fc2b")));
                Assert.AreEqual("LocalePrefabFrench", module.GetValue<GameObject>(new GlobalId("a3512a8e8d0241a4b2e7c564d6bc7215")).name);
                Assert.AreEqual("Ressources", module.GetValue<string>(new GlobalId("6e843cb42ca8420385a74de9f59314f4")));
            }

            using (new LocaleScope(module, new GlobalId("97b2faba4064dc4419593e7ce69606c8")))
            {
                Assert.AreEqual("早上好！", module.GetValue<string>(new GlobalId("92d6e8700ddb42ec9e6aab8cb1b49ebe")));
                Assert.AreEqual("再见", module.GetValue<string>(new GlobalId("1897460040b244c5b2abaaa04e56fc2b")));
                Assert.AreEqual("LocalePrefabChinese", module.GetValue<GameObject>(new GlobalId("a3512a8e8d0241a4b2e7c564d6bc7215")).name);
                Assert.AreEqual("资源", module.GetValue<string>(new GlobalId("6e843cb42ca8420385a74de9f59314f4")));
            }

            launcher.Destroy();
        }
    }
}
