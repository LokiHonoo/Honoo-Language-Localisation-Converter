using CommunityToolkit.Mvvm.ComponentModel;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public sealed partial class General : ObservableObject
    {
        #region Instance

        public static General Instance { get; } = new General();

        #endregion Instance

        [ObservableProperty]
        private bool _documentModified;
    }
}