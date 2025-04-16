using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public sealed partial class TranslationEntry : ObservableObject
    {
        #region Members

        [ObservableProperty]
        private string _comment;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _value;

        #endregion Members

        public TranslationEntry(string key, string value, string comment)
        {
            this.Name = key;
            this.Value = value;
            this.Comment = comment;
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            General.Instance.DocumentModified = true;
        }
    }
}