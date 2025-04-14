using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public sealed partial class TranslationEntry : ObservableObject
    {
        #region Members

        private readonly MainWindowViewModel _mainWindowViewModel;

        [ObservableProperty]
        private string _comment;
        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _value;

        #endregion Members

        public TranslationEntry(string key, string value, string comment, MainWindowViewModel mainWindowViewModel)
        {
            this.Name = key;
            this.Value = value;
            this.Comment = comment;
            _mainWindowViewModel = mainWindowViewModel;
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            _mainWindowViewModel.Modified = true;
        }
    }
}