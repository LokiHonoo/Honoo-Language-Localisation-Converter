using CommunityToolkit.Mvvm.ComponentModel;
using Honoo.Configuration;
using HonooUI.WPF;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public sealed partial class Settings : ObservableObject
    {
        #region Instance

        public static Settings Instance { get; } = new Settings();

        #endregion Instance

        #region Members

        private readonly string _configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");

        [ObservableProperty]
        private string? _languageFile;

        [ObservableProperty]
        private double _windowHeight;

        [ObservableProperty]
        private double _windowLeft;

        [ObservableProperty]
        private WindowState? _windowState;

        [ObservableProperty]
        private double _windowTop;

        [ObservableProperty]
        private double _windowWidth;

        public DateTime LastUpdate { get; set; }

        #endregion Members

        #region Construction

        public Settings()
        {
        }

        #endregion Construction

        internal void Load()
        {
            string languageFile = string.Empty;
            double width = 1200;
            double height = 580;
            WindowState state = System.Windows.WindowState.Normal;
            try
            {
                using var manager = new XConfigManager(_configFile, true);
                state = manager.Default.Properties.GetValue("WindowState", new XString(System.Windows.WindowState.Normal.ToString())).GetEnumValue<WindowState>();
                width = manager.Default.Properties.GetValue("WindowWidth", new XString(1200.ToString())).GetDoubleValue();
                height = manager.Default.Properties.GetValue("WindowHeight", new XString(580.ToString())).GetDoubleValue();
                languageFile = manager.Default.Properties.GetStringValue("LanguageFile", string.Empty);
                this.LastUpdate = manager.Default.Properties.GetValue("LastUpdate", new XString(DateTime.MinValue.ToString("yyyy-MM-dd"))).GetDateTimeValue();
            }
            catch
            {
            }
            if (state == System.Windows.WindowState.Minimized)
            {
                state = System.Windows.WindowState.Normal;
                width = 1200;
                height = 580;
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
            this.WindowState = state;
            this.WindowWidth = width;
            this.WindowHeight = height;
            this.WindowLeft = (area.Width - width) / 2;
            this.WindowTop = (area.Height - height) / 2;
            this.LanguageFile = languageFile;
        }

        internal void Save()
        {
            using var manager = new XConfigManager();
            manager.Default.Properties.AddString("WindowState", this.WindowState.ToString());
            manager.Default.Properties.AddString("WindowWidth", this.WindowWidth.ToString());
            manager.Default.Properties.AddString("WindowHeight", this.WindowHeight.ToString());
            manager.Default.Properties.AddString("LanguageFile", this.LanguageFile);
            manager.Default.Properties.AddString("LastUpdate", this.LastUpdate.ToString("yyyy-MM-dd HH:mm:ss"));
            manager.Save(_configFile);
        }
    }
}