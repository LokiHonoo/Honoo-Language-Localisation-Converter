using HonooLanguageLocalisationConverter.ViewModels;
using System.Windows;

namespace HonooLanguageLocalisationConverter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            Settings.Instance.Save();
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Settings.Instance.Load();
        }
    }
}