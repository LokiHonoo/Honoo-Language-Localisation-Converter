using CommunityToolkit.Mvvm.ComponentModel;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public sealed class LanguageEntry : ObservableObject
    {
        #region Members

        public string? Comment { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }

        #endregion Members

        public LanguageEntry(string key, string value, string comment)
        {
            this.Key = key;
            this.Value = value;
            this.Comment = comment;
        }
    }
}