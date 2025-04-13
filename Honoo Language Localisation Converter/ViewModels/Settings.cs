using CommunityToolkit.Mvvm.ComponentModel;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public sealed partial class Settings : ObservableObject
    {
        #region Instance

        public static Settings Instance { get; } = new Settings();

        #endregion Instance

        #region Members

        [ObservableProperty]
        private double _windowHeight;

        [ObservableProperty]
        private double _windowLeft;

        [ObservableProperty]
        private double _windowTop;

        [ObservableProperty]
        private double _windowWidth;

        public string? LanguageFile { get; set; }

        #endregion Members
    }
}