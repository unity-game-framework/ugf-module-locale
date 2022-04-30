using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

namespace UGF.Module.Locale.Runtime.Tests
{
    public class TestLocaleModule
    {
        private TestApplicationLauncher m_launcher;

        [SetUp]
        public async Task Setup()
        {
            m_launcher = new TestApplicationLauncher();

            await m_launcher.CreateAsync();
        }

        [TearDown]
        public void Teardown()
        {
            m_launcher.Destroy();
            m_launcher = null;
        }

        [Test]
        public void InitializeAndUninitialize()
        {
            var module = m_launcher.Application.GetModule<LocaleModule>();

            Assert.NotNull(module);
        }

        [Test]
        public void Get()
        {
            var module = m_launcher.Application.GetModule<LocaleModule>();

            Assert.NotNull(module);

            string result1 = module.Get<string>("f9cd8a1f6057ad94fbe269362df9057c");
            string result2 = module.Get<string>("777e3e4561cf2ab45a37ca31462e5240");
            var result3 = module.Get<GameObject>("6bf9ce242ad62fe44bc10f96753bbf6f");

            Assert.AreEqual("Good morning!", result1);
            Assert.AreEqual("Goodbye", result2);
            Assert.AreEqual("LocalePrefabEnglish", result3.name);

            using (new LocaleScope(module, "47a3846001472074fa3f2d641cb11ae6"))
            {
                Assert.AreEqual("Bonjour!", module.Get<string>("f9cd8a1f6057ad94fbe269362df9057c"));
                Assert.AreEqual("Au revoir", module.Get<string>("777e3e4561cf2ab45a37ca31462e5240"));
                Assert.AreEqual("LocalePrefabFrench", module.Get<GameObject>("6bf9ce242ad62fe44bc10f96753bbf6f").name);
            }

            using (new LocaleScope(module, "97b2faba4064dc4419593e7ce69606c8"))
            {
                Assert.AreEqual("早上好！", module.Get<string>("f9cd8a1f6057ad94fbe269362df9057c"));
                Assert.AreEqual("再见", module.Get<string>("777e3e4561cf2ab45a37ca31462e5240"));
                Assert.AreEqual("LocalePrefabChinese", module.Get<GameObject>("6bf9ce242ad62fe44bc10f96753bbf6f").name);
            }
        }
    }
}
