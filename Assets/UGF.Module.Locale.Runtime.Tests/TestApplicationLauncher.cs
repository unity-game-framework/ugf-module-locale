using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.Locale.Runtime.Tests
{
    public class TestApplicationLauncher
    {
        public string ResourcesPath { get; }
        public ApplicationLauncherComponent Launcher { get { return m_launcher ? m_launcher : throw new ArgumentException("Value not specified."); } }
        public IApplication Application { get { return Launcher.Launcher.Application; } }

        private ApplicationLauncherComponent m_launcher;

        public TestApplicationLauncher(string resourcesPath = "Launcher")
        {
            if (string.IsNullOrEmpty(resourcesPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(resourcesPath));

            ResourcesPath = resourcesPath;
        }

        public async Task CreateAsync()
        {
            m_launcher = Object.Instantiate(Resources.Load<ApplicationLauncherComponent>(ResourcesPath));
            m_launcher.Initialize();

            await m_launcher.LaunchAsync();
        }

        public void Destroy()
        {
            m_launcher.Uninitialize();

            Object.DestroyImmediate(m_launcher.gameObject);

            m_launcher = null;
        }
    }
}
