using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public sealed partial class InformationEntry : ObservableObject
    {
        #region Members

        [ObservableProperty]
        private string _appName = string.Empty;

        [ObservableProperty]
        private string _appVer = string.Empty;

        [ObservableProperty]
        private string _author = string.Empty;

        [ObservableProperty]
        private string _langName = string.Empty;

        [ObservableProperty]
        private string _langVer = string.Empty;

        [ObservableProperty]
        private string _remarks = string.Empty;

        [ObservableProperty]
        private string _url = string.Empty;

        public InformationEntry()
        {
            this.PropertyChanged += OnPropertyChanged;
        }

        #endregion Members

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            General.Instance.DocumentModified = true;
        }
    }
}