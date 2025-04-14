using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public sealed partial class Settings : ObservableObject
    {
        #region Instance

        public static Settings Instance { get; } = new Settings();

        #endregion Instance

        #region Members

        [ObservableProperty]
        private string? _languageFile;

        [ObservableProperty]
        private double _windowHeight;

        [ObservableProperty]
        private double _windowLeft;

        [ObservableProperty]
        private double _windowTop;

        [ObservableProperty]
        private double _windowWidth;

        public DateTime LastUpdate { get; set; }
        #endregion Members
    }
}