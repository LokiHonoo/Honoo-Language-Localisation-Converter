using Honoo.Configuration;
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
        private string _configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");

        protected override void OnExit(ExitEventArgs e)
        {
            using (var manager = new XConfigManager())
            {
                manager.Default.Properties.AddString("WindowWidth", Locator.MainWindowViewModel!.WindowWidth.ToString());
                manager.Default.Properties.AddString("WindowHeight", Locator.MainWindowViewModel!.WindowHeight.ToString());
                manager.Default.Properties.AddString("LanguageFile", Locator.MainWindowViewModel!.LanguageFile);
                manager.Save(_configFile);
            }
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string languageFile = string.Empty;
            double width = 800;
            double height = 580;
            try
            {
                using (var manager = new XConfigManager(_configFile, true))
                {
                    width = manager.Default.Properties.GetValue("WindowWidth", new XString(800.ToString())).GetDoubleValue();
                    height = manager.Default.Properties.GetValue("WindowHeight", new XString(580.ToString())).GetDoubleValue();
                    languageFile = manager.Default.Properties.GetStringValue("LanguageFile", string.Empty);
                }
                if (string.IsNullOrEmpty(languageFile))
                {
                    ViewModels.Localization.Instance.Load(languageFile);
                }
            }
            catch
            {
                ViewModels.Localization.Instance.ResetDefault();
            }
            Size area = SystemParameters.WorkArea.Size;
            var mainWindow = new MainWindow();
            Locator.MainWindowViewModel!.WindowWidth = width;
            Locator.MainWindowViewModel!.WindowHeight = height;
            Locator.MainWindowViewModel!.WindowLeft = (area.Width - width) / 2;
            Locator.MainWindowViewModel!.WindowTop = (area.Height - height) / 2;
            Locator.MainWindowViewModel!.LanguageFile = languageFile;
            mainWindow.Show();
        }
    }
}