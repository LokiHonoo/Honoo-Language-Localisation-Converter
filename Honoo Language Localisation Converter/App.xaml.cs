using Honoo.Configuration;
using HonooLanguageLocalisationConverter.ViewModels;
using System;
using System.IO;
using System.Windows;

namespace HonooLanguageLocalisationConverter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly string _configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");

        protected override void OnExit(ExitEventArgs e)
        {
            using (var manager = new XConfigManager())
            {
                manager.Default.Properties.AddString("WindowWidth", Settings.Instance.WindowWidth.ToString());
                manager.Default.Properties.AddString("WindowHeight", Settings.Instance.WindowHeight.ToString());
                manager.Default.Properties.AddString("LanguageFile", Settings.Instance.LanguageFile);
                manager.Default.Properties.AddString("LastUpdate", Settings.Instance.LastUpdate.ToString("yyyy-MM-dd HH:mm:ss"));
                manager.Save(_configFile);
            }
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string languageFile = string.Empty;
            double width = 1200;
            double height = 580;
            try
            {
                using var manager = new XConfigManager(_configFile, true);
                width = manager.Default.Properties.GetValue("WindowWidth", new XString(1200.ToString())).GetDoubleValue();
                height = manager.Default.Properties.GetValue("WindowHeight", new XString(580.ToString())).GetDoubleValue();
                languageFile = manager.Default.Properties.GetStringValue("LanguageFile", string.Empty);
                Settings.Instance.LastUpdate = manager.Default.Properties.GetValue("LastUpdate", new XString(DateTime.MinValue.ToString("yyyy-MM-dd"))).GetDateTimeValue();
            }
            catch
            {
            }
            if (!string.IsNullOrEmpty(languageFile))
            {
                try
                {
                    LanguagePackage.Instance.Load(languageFile);
                }
                catch
                {
                    LanguagePackage.Instance.ResetDefault();
                }
            }
            Size area = SystemParameters.WorkArea.Size;
            Settings.Instance.WindowWidth = width;
            Settings.Instance.WindowHeight = height;
            Settings.Instance.WindowLeft = (area.Width - width) / 2;
            Settings.Instance.WindowTop = (area.Height - height) / 2;
            Settings.Instance.LanguageFile = languageFile;
        }
    }
}