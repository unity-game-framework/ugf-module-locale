using System.Threading.Tasks;
using NUnit.Framework;

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
    }
}
