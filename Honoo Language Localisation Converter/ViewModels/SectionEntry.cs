using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public sealed partial class SectionEntry : ObservableObject
    {
        #region Members

        [ObservableProperty]
        public string? _name;

        public ObservableCollection<LanguageEntry> LanguageEntries { get; } = [];

        #endregion Members

        public SectionEntry(string name)
        {
            this.Name = name;
        }
    }
}