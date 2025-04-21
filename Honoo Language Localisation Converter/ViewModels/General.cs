using CommunityToolkit.Mvvm.ComponentModel;
using System.Reflection;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public sealed partial class General : ObservableObject
    {
        #region Instance

        public static General Instance { get; } = new General();

        #endregion Instance

        #region Members

        [ObservableProperty]
        private bool _documentModified;

        public string Creator { get; }
        public string Version { get; }
        public string Website { get; }

        #endregion Members

        public General()
        {
            this.Version = Assembly.GetExecutingAssembly().GetName().Version!.ToString(3);
            this.Creator = "Honoo Language Localisation Converter " + this.Version;
            this.Website = "https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter";
        }
    }
}