using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public sealed partial class SectionEntry : ObservableObject
    {
        #region Members

        [ObservableProperty]
        public string _name;

        [ObservableProperty]
        private bool _modified;

        public ObservableCollection<TranslationEntry> TranslationEntries { get; } = [];

        #endregion Members

        public SectionEntry(string name)
        {
            this.Name = name;
            this.TranslationEntries.CollectionChanged += TranslationEntriesChanged;
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            General.Instance.DocumentModified = true;
        }

        private void TranslationEntriesChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            General.Instance.DocumentModified = true;
        }
    }
}