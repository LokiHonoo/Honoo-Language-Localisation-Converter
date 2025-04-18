using Honoo.Configuration;
using System.ComponentModel;
using System.IO;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    /// <summary>
    /// Language package class. Binding-notify basic class [.NET 6.0+][With Project field <Nullable>enable</Nullable>].
    /// Using single instance <see cref="Instance"/> to visit.
    /// <br />Install nuget package: <see href="https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager"/>.
    /// </summary>
    public sealed class LanguagePackage
    {
        #region Instance

        /// <summary>
        /// Language package instance.
        /// </summary>
        public static LanguagePackage Instance { get; } = new LanguagePackage();

        #endregion Instance

        #region Members

        /// <summary>
        /// DialogMessages section.
        /// </summary>
        public __DialogMessages DialogMessages { get; } = new __DialogMessages();

        /// <summary>
        /// Informartion section.
        /// </summary>
        public __Informartion Informartion { get; } = new __Informartion();

        /// <summary>
        /// Main section.
        /// </summary>
        public __Main Main { get; } = new __Main();

        /// <summary>
        /// Menu section.
        /// </summary>
        public __Menu Menu { get; } = new __Menu();

        /// <summary>
        /// ToastMessages section.
        /// </summary>
        public __ToastMessages ToastMessages { get; } = new __ToastMessages();

        #endregion Members

        /// <summary>
        /// Initialize new instance of LanguagePackage class.
        /// </summary>
        internal LanguagePackage()
        {
        }

        /// <summary>
        /// Load language file.
        /// </summary>
        /// <param name="fileName">Language file name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void Load(string fileName)
        {
            using (var manager = new XConfigManager(fileName, true))
            {
                this.Informartion.LoadInternal(manager);
                this.Menu.LoadInternal(manager);
                this.Main.LoadInternal(manager);
                this.DialogMessages.LoadInternal(manager);
                this.ToastMessages.LoadInternal(manager);
            }
        }

        /// <summary>
        /// Load language stream.
        /// </summary>
        /// <param name="stream">Language stream.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void Load(Stream stream)
        {
            using (var manager = new XConfigManager(stream))
            {
                this.Informartion.LoadInternal(manager);
                this.Menu.LoadInternal(manager);
                this.Main.LoadInternal(manager);
                this.DialogMessages.LoadInternal(manager);
                this.ToastMessages.LoadInternal(manager);
            }
        }

        /// <summary>
        /// Reset all properties to default values.
        /// </summary>
        public void ResetDefault()
        {
            this.Informartion.ResetDefaultInternal();
            this.Menu.ResetDefaultInternal();
            this.Main.ResetDefaultInternal();
            this.DialogMessages.ResetDefaultInternal();
            this.ToastMessages.ResetDefaultInternal();
        }

        /// <summary>
        /// Save to language file.
        /// </summary>
        /// <param name="defaultField">Select current field or default field.</param>
        /// <param name="fileName">Language file name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void Save(bool defaultField, string fileName)
        {
            using (var manager = new XConfigManager())
            {
                this.Informartion.SaveInternal(defaultField, manager);
                this.Menu.SaveInternal(defaultField, manager);
                this.Main.SaveInternal(defaultField, manager);
                this.DialogMessages.SaveInternal(defaultField, manager);
                this.ToastMessages.SaveInternal(defaultField, manager);
                manager.Save(fileName);
            }
        }

        /// <summary>
        /// Save to language stream.
        /// </summary>
        /// <param name="defaultField">Select current field or default field.</param>
        /// <param name="stream">Language stream.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void Save(bool defaultField, Stream stream)
        {
            using (var manager = new XConfigManager())
            {
                this.Informartion.SaveInternal(defaultField, manager);
                this.Menu.SaveInternal(defaultField, manager);
                this.Main.SaveInternal(defaultField, manager);
                this.DialogMessages.SaveInternal(defaultField, manager);
                this.ToastMessages.SaveInternal(defaultField, manager);
                manager.Save(stream);
            }
        }

        /// <summary>
        /// DialogMessages section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __DialogMessages : INotifyPropertyChanging, INotifyPropertyChanged
        {
            #region Events

            /// <summary>
            /// Property changed event handler.
            /// </summary>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Property changing event handler.
            /// </summary>
            public event PropertyChangingEventHandler? PropertyChanging;

            private void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            private void OnPropertyChanging(string name)
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            }

            #endregion Events

            #region Comments

            private const string _documentExistsCreateNew_c = "Dialog content.";
            private const string _documentExistsLoadNew_c = "Dialog content.";
            private const string _exitSaveRemind_c = "Dialog content.";
            private const string _removeItem_c = "Dialog content set custom field {0}=Remove Name.";
            private const string _saveCodeCommunityToolkit_c = "Dialog content. CommunityToolkit.Mvvm code style.";
            private const string _saveCodeNotifyBasic_c = "Dialog content. INotifyPropertyChanging, INotifyPropertyChanged implemented.";
            private const string _saveCodeStandard_c = "Dialog content.";
            private const string _sectionNameDuplicate_c = "Dialog content set field {0}=Section Name.";
            private const string _sectionNameEmpty_c = "Dialog content.";
            private const string _translationNameDuplicate_c = "Dialog content set field {0}=Section Name,{1}=Translation Name.";
            private const string _translationNameEmpty_c = "Dialog content.";

            #endregion Comments

            #region Default

            private const string _documentExistsCreateNew_d = "Document loaded already. Create new document?";
            private const string _documentExistsLoadNew_d = "Document loaded already. Load new document?";
            private const string _exitSaveRemind_d = "The document modified but has not been saved.\r\n\r\nExit application without save?";
            private const string _removeItem_d = "Remove \"{0}\" ?";
            private const string _saveCodeCommunityToolkit_d = "Binding-notify code style for lib - CommunityToolkit.Mvvm";
            private const string _saveCodeNotifyBasic_d = "Binding-notify basic class";
            private const string _saveCodeStandard_d = "Standard class model for all code style";
            private const string _sectionNameDuplicate_d = "Section \"{0}\" has duplicate name.";
            private const string _sectionNameEmpty_d = "Section \"Name\" string can't be empty.";
            private const string _translationNameDuplicate_d = "Section \"{0}\"'s translation entry \"{1}\" has duplicate name.";
            private const string _translationNameEmpty_d = "Section \"{0}\"'s translation entry string can't be empty.";

            #endregion Default

            #region Members

            private string _documentExistsCreateNew = _documentExistsCreateNew_d;
            private string _documentExistsLoadNew = _documentExistsLoadNew_d;
            private string _exitSaveRemind = _exitSaveRemind_d;
            private string _removeItem = _removeItem_d;
            private string _saveCodeCommunityToolkit = _saveCodeCommunityToolkit_d;
            private string _saveCodeNotifyBasic = _saveCodeNotifyBasic_d;
            private string _saveCodeStandard = _saveCodeStandard_d;
            private string _sectionNameDuplicate = _sectionNameDuplicate_d;
            private string _sectionNameEmpty = _sectionNameEmpty_d;
            private string _translationNameDuplicate = _translationNameDuplicate_d;
            private string _translationNameEmpty = _translationNameEmpty_d;

            /// <summary>
            /// Dialog content.
            /// </summary>
            public string DocumentExistsCreateNew
            { get { return _documentExistsCreateNew; } set { OnPropertyChanging(nameof(this.DocumentExistsCreateNew)); _documentExistsCreateNew = value; OnPropertyChanged(nameof(this.DocumentExistsCreateNew)); } }

            /// <summary>
            /// Dialog content.
            /// </summary>
            public string DocumentExistsLoadNew
            { get { return _documentExistsLoadNew; } set { OnPropertyChanging(nameof(this.DocumentExistsLoadNew)); _documentExistsLoadNew = value; OnPropertyChanged(nameof(this.DocumentExistsLoadNew)); } }

            /// <summary>
            /// Dialog content.
            /// </summary>
            public string ExitSaveRemind
            { get { return _exitSaveRemind; } set { OnPropertyChanging(nameof(this.ExitSaveRemind)); _exitSaveRemind = value; OnPropertyChanged(nameof(this.ExitSaveRemind)); } }

            /// <summary>
            /// Dialog content set custom field {0}=Remove Name.
            /// </summary>
            public string RemoveItem
            { get { return _removeItem; } set { OnPropertyChanging(nameof(this.RemoveItem)); _removeItem = value; OnPropertyChanged(nameof(this.RemoveItem)); } }

            /// <summary>
            /// Dialog content. CommunityToolkit.Mvvm code style.
            /// </summary>
            public string SaveCodeCommunityToolkit
            { get { return _saveCodeCommunityToolkit; } set { OnPropertyChanging(nameof(this.SaveCodeCommunityToolkit)); _saveCodeCommunityToolkit = value; OnPropertyChanged(nameof(this.SaveCodeCommunityToolkit)); } }

            /// <summary>
            /// Dialog content. INotifyPropertyChanging, INotifyPropertyChanged implemented.
            /// </summary>
            public string SaveCodeNotifyBasic
            { get { return _saveCodeNotifyBasic; } set { OnPropertyChanging(nameof(this.SaveCodeNotifyBasic)); _saveCodeNotifyBasic = value; OnPropertyChanged(nameof(this.SaveCodeNotifyBasic)); } }

            /// <summary>
            /// Dialog content.
            /// </summary>
            public string SaveCodeStandard
            { get { return _saveCodeStandard; } set { OnPropertyChanging(nameof(this.SaveCodeStandard)); _saveCodeStandard = value; OnPropertyChanged(nameof(this.SaveCodeStandard)); } }

            /// <summary>
            /// Dialog content set field {0}=Section Name.
            /// </summary>
            public string SectionNameDuplicate
            { get { return _sectionNameDuplicate; } set { OnPropertyChanging(nameof(this.SectionNameDuplicate)); _sectionNameDuplicate = value; OnPropertyChanged(nameof(this.SectionNameDuplicate)); } }

            /// <summary>
            /// Dialog content.
            /// </summary>
            public string SectionNameEmpty
            { get { return _sectionNameEmpty; } set { OnPropertyChanging(nameof(this.SectionNameEmpty)); _sectionNameEmpty = value; OnPropertyChanged(nameof(this.SectionNameEmpty)); } }

            /// <summary>
            /// Dialog content set field {0}=Section Name,{1}=Translation Name.
            /// </summary>
            public string TranslationNameDuplicate
            { get { return _translationNameDuplicate; } set { OnPropertyChanging(nameof(this.TranslationNameDuplicate)); _translationNameDuplicate = value; OnPropertyChanged(nameof(this.TranslationNameDuplicate)); } }

            /// <summary>
            /// Dialog content.
            /// </summary>
            public string TranslationNameEmpty
            { get { return _translationNameEmpty; } set { OnPropertyChanging(nameof(this.TranslationNameEmpty)); _translationNameEmpty = value; OnPropertyChanged(nameof(this.TranslationNameEmpty)); } }

            #endregion Members

            internal __DialogMessages()
            {
            }

            internal void LoadInternal(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("DialogMessages", out XSection section))
                {
                    this.DocumentExistsCreateNew = section.Properties.GetStringValue("DocumentExistsCreateNew", _documentExistsCreateNew_d);
                    this.DocumentExistsLoadNew = section.Properties.GetStringValue("DocumentExistsLoadNew", _documentExistsLoadNew_d);
                    this.SectionNameEmpty = section.Properties.GetStringValue("SectionNameEmpty", _sectionNameEmpty_d);
                    this.SectionNameDuplicate = section.Properties.GetStringValue("SectionNameDuplicate", _sectionNameDuplicate_d);
                    this.TranslationNameEmpty = section.Properties.GetStringValue("TranslationNameEmpty", _translationNameEmpty_d);
                    this.TranslationNameDuplicate = section.Properties.GetStringValue("TranslationNameDuplicate", _translationNameDuplicate_d);
                    this.RemoveItem = section.Properties.GetStringValue("RemoveItem", _removeItem_d);
                    this.SaveCodeStandard = section.Properties.GetStringValue("SaveCodeStandard", _saveCodeStandard_d);
                    this.SaveCodeNotifyBasic = section.Properties.GetStringValue("SaveCodeNotifyBasic", _saveCodeNotifyBasic_d);
                    this.SaveCodeCommunityToolkit = section.Properties.GetStringValue("SaveCodeCommunityToolkit", _saveCodeCommunityToolkit_d);
                    this.ExitSaveRemind = section.Properties.GetStringValue("ExitSaveRemind", _exitSaveRemind_d);
                }
            }

            internal void ResetDefaultInternal()
            {
                this.DocumentExistsCreateNew = _documentExistsCreateNew_d;
                this.DocumentExistsLoadNew = _documentExistsLoadNew_d;
                this.SectionNameEmpty = _sectionNameEmpty_d;
                this.SectionNameDuplicate = _sectionNameDuplicate_d;
                this.TranslationNameEmpty = _translationNameEmpty_d;
                this.TranslationNameDuplicate = _translationNameDuplicate_d;
                this.RemoveItem = _removeItem_d;
                this.SaveCodeStandard = _saveCodeStandard_d;
                this.SaveCodeNotifyBasic = _saveCodeNotifyBasic_d;
                this.SaveCodeCommunityToolkit = _saveCodeCommunityToolkit_d;
                this.ExitSaveRemind = _exitSaveRemind_d;
            }

            internal void SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("DialogMessages");
                if (defaultField)
                {
                    section.Properties.AddString("DocumentExistsCreateNew", _documentExistsCreateNew_d).Comment.SetValue(_documentExistsCreateNew_c, true);
                    section.Properties.AddString("DocumentExistsLoadNew", _documentExistsLoadNew_d).Comment.SetValue(_documentExistsLoadNew_c, true);
                    section.Properties.AddString("SectionNameEmpty", _sectionNameEmpty_d).Comment.SetValue(_sectionNameEmpty_c, true);
                    section.Properties.AddString("SectionNameDuplicate", _sectionNameDuplicate_d).Comment.SetValue(_sectionNameDuplicate_c, true);
                    section.Properties.AddString("TranslationNameEmpty", _translationNameEmpty_d).Comment.SetValue(_translationNameEmpty_c, true);
                    section.Properties.AddString("TranslationNameDuplicate", _translationNameDuplicate_d).Comment.SetValue(_translationNameDuplicate_c, true);
                    section.Properties.AddString("RemoveItem", _removeItem_d).Comment.SetValue(_removeItem_c, true);
                    section.Properties.AddString("SaveCodeStandard", _saveCodeStandard_d).Comment.SetValue(_saveCodeStandard_c, true);
                    section.Properties.AddString("SaveCodeNotifyBasic", _saveCodeNotifyBasic_d).Comment.SetValue(_saveCodeNotifyBasic_c, true);
                    section.Properties.AddString("SaveCodeCommunityToolkit", _saveCodeCommunityToolkit_d).Comment.SetValue(_saveCodeCommunityToolkit_c, true);
                    section.Properties.AddString("ExitSaveRemind", _exitSaveRemind_d).Comment.SetValue(_exitSaveRemind_c, true);
                }
                else
                {
                    section.Properties.AddString("DocumentExistsCreateNew", this.DocumentExistsCreateNew).Comment.SetValue(_documentExistsCreateNew_c, true);
                    section.Properties.AddString("DocumentExistsLoadNew", this.DocumentExistsLoadNew).Comment.SetValue(_documentExistsLoadNew_c, true);
                    section.Properties.AddString("SectionNameEmpty", this.SectionNameEmpty).Comment.SetValue(_sectionNameEmpty_c, true);
                    section.Properties.AddString("SectionNameDuplicate", this.SectionNameDuplicate).Comment.SetValue(_sectionNameDuplicate_c, true);
                    section.Properties.AddString("TranslationNameEmpty", this.TranslationNameEmpty).Comment.SetValue(_translationNameEmpty_c, true);
                    section.Properties.AddString("TranslationNameDuplicate", this.TranslationNameDuplicate).Comment.SetValue(_translationNameDuplicate_c, true);
                    section.Properties.AddString("RemoveItem", this.RemoveItem).Comment.SetValue(_removeItem_c, true);
                    section.Properties.AddString("SaveCodeStandard", this.SaveCodeStandard).Comment.SetValue(_saveCodeStandard_c, true);
                    section.Properties.AddString("SaveCodeNotifyBasic", this.SaveCodeNotifyBasic).Comment.SetValue(_saveCodeNotifyBasic_c, true);
                    section.Properties.AddString("SaveCodeCommunityToolkit", this.SaveCodeCommunityToolkit).Comment.SetValue(_saveCodeCommunityToolkit_c, true);
                    section.Properties.AddString("ExitSaveRemind", this.ExitSaveRemind).Comment.SetValue(_exitSaveRemind_c, true);
                }
            }
        }

        /// <summary>
        /// Informartion section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __Informartion : INotifyPropertyChanging, INotifyPropertyChanged
        {
            #region Events

            /// <summary>
            /// Property changed event handler.
            /// </summary>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Property changing event handler.
            /// </summary>
            public event PropertyChangingEventHandler? PropertyChanging;

            private void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            private void OnPropertyChanging(string name)
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            }

            #endregion Events

            #region Default

            private const string _appName_d = "Honoo Language Localisation Converter";
            private const string _appVer_d = "1.x";
            private const string _author_d = "Honoo Language Localisation Converter";
            private const string _langName_d = "en-US";
            private const string _langVer_d = "00";
            private const string _remarks_d = "";
            private const string _url_d = "https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter";

            #endregion Default

            #region Members

            private string _appName = _appName_d;
            private string _appVer = _appVer_d;
            private string _author = _author_d;
            private string _langName = _langName_d;
            private string _langVer = _langVer_d;
            private string _remarks = _remarks_d;
            private string _url = _url_d;

            /// <summary>
            /// Application name.
            /// </summary>
            public string AppName
            { get { return _appName; } set { OnPropertyChanging(nameof(this.AppName)); _appName = value; OnPropertyChanged(nameof(this.AppName)); } }

            /// <summary>
            /// Application version.
            /// </summary>
            public string AppVer
            { get { return _appVer; } set { OnPropertyChanging(nameof(this.AppVer)); _appVer = value; OnPropertyChanged(nameof(this.AppVer)); } }

            /// <summary>
            /// Author name.
            /// </summary>
            public string Author
            { get { return _author; } set { OnPropertyChanging(nameof(this.Author)); _author = value; OnPropertyChanged(nameof(this.Author)); } }

            /// <summary>
            /// Language name as "en-US".
            /// </summary>
            public string LangName
            { get { return _langName; } set { OnPropertyChanging(nameof(this.LangName)); _langName = value; OnPropertyChanged(nameof(this.LangName)); } }

            /// <summary>
            /// Language file version.
            /// </summary>
            public string LangVer
            { get { return _langVer; } set { OnPropertyChanging(nameof(this.LangVer)); _langVer = value; OnPropertyChanged(nameof(this.LangVer)); } }

            /// <summary>
            /// Remarks.
            /// </summary>
            public string Remarks
            { get { return _remarks; } set { OnPropertyChanging(nameof(this.Remarks)); _remarks = value; OnPropertyChanged(nameof(this.Remarks)); } }

            /// <summary>
            /// Author / file related url.
            /// </summary>
            public string Url
            { get { return _url; } set { OnPropertyChanging(nameof(this.Url)); _url = value; OnPropertyChanged(nameof(this.Url)); } }

            #endregion Members

            internal __Informartion()
            {
            }

            internal void LoadInternal(XConfigManager manager)
            {
                this.AppName = manager.Default.Properties.GetStringValue("AppName", _appName_d);
                this.AppVer = manager.Default.Properties.GetStringValue("AppVer", _appVer_d);
                this.LangName = manager.Default.Properties.GetStringValue("LangName", _langName_d);
                this.LangVer = manager.Default.Properties.GetStringValue("LangVer", _langVer_d);
                this.Author = manager.Default.Properties.GetStringValue("Author", _author_d);
                this.Url = manager.Default.Properties.GetStringValue("Url", _url_d);
                this.Remarks = manager.Default.Properties.GetStringValue("Remarks", _remarks_d);
            }

            internal void ResetDefaultInternal()
            {
                this.AppName = _appName_d;
                this.AppVer = _appVer_d;
                this.LangName = _langName_d;
                this.LangVer = _langVer_d;
                this.Author = _author_d;
                this.Url = _url_d;
                this.Remarks = _remarks_d;
            }

            internal void SaveInternal(bool defaultField, XConfigManager manager)
            {
                if (defaultField)
                {
                    manager.Default.Properties.AddString("AppName", _appName_d);
                    manager.Default.Properties.AddString("AppVer", _appVer_d);
                    manager.Default.Properties.AddString("LangName", _langName_d);
                    manager.Default.Properties.AddString("LangVer", _langVer_d);
                    manager.Default.Properties.AddString("Author", _author_d);
                    manager.Default.Properties.AddString("Url", _url_d);
                    manager.Default.Properties.AddString("Remarks", _remarks_d);
                }
                else
                {
                    manager.Default.Properties.AddString("AppName", this.AppName);
                    manager.Default.Properties.AddString("AppVer", this.AppVer);
                    manager.Default.Properties.AddString("LangName", this.LangName);
                    manager.Default.Properties.AddString("LangVer", this.LangVer);
                    manager.Default.Properties.AddString("Author", this.Author);
                    manager.Default.Properties.AddString("Url", this.Url);
                    manager.Default.Properties.AddString("Remarks", this.Remarks);
                }
            }
        }

        /// <summary>
        /// Main section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __Main : INotifyPropertyChanging, INotifyPropertyChanged
        {
            #region Events

            /// <summary>
            /// Property changed event handler.
            /// </summary>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Property changing event handler.
            /// </summary>
            public event PropertyChangingEventHandler? PropertyChanging;

            private void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            private void OnPropertyChanging(string name)
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            }

            #endregion Events

            #region Comments

            private const string _hasNewVersion_c = "StatusBar tip.";
            private const string _informartion_c = "Tab name.";
            private const string _sectionEntries_c = "Title text.";
            private const string _sections_c = "Tab name.";
            private const string _sort_c = "Button text.";
            private const string _translationEntries_c = "Title text.";

            #endregion Comments

            #region Default

            private const string _hasNewVersion_d = "New version published";
            private const string _informartion_d = "Informartion";
            private const string _sectionEntries_d = "Section entries";
            private const string _sections_d = "Sections";
            private const string _sort_d = "Sort";
            private const string _translationEntries_d = "Translation entries";

            #endregion Default

            #region Members

            private string _hasNewVersion = _hasNewVersion_d;
            private string _informartion = _informartion_d;
            private string _sectionEntries = _sectionEntries_d;
            private string _sections = _sections_d;
            private string _sort = _sort_d;
            private string _translationEntries = _translationEntries_d;

            /// <summary>
            /// StatusBar tip.
            /// </summary>
            public string HasNewVersion
            { get { return _hasNewVersion; } set { OnPropertyChanging(nameof(this.HasNewVersion)); _hasNewVersion = value; OnPropertyChanged(nameof(this.HasNewVersion)); } }

            /// <summary>
            /// Tab name.
            /// </summary>
            public string Informartion
            { get { return _informartion; } set { OnPropertyChanging(nameof(this.Informartion)); _informartion = value; OnPropertyChanged(nameof(this.Informartion)); } }

            /// <summary>
            /// Title text.
            /// </summary>
            public string SectionEntries
            { get { return _sectionEntries; } set { OnPropertyChanging(nameof(this.SectionEntries)); _sectionEntries = value; OnPropertyChanged(nameof(this.SectionEntries)); } }

            /// <summary>
            /// Tab name.
            /// </summary>
            public string Sections
            { get { return _sections; } set { OnPropertyChanging(nameof(this.Sections)); _sections = value; OnPropertyChanged(nameof(this.Sections)); } }

            /// <summary>
            /// Button text.
            /// </summary>
            public string Sort
            { get { return _sort; } set { OnPropertyChanging(nameof(this.Sort)); _sort = value; OnPropertyChanged(nameof(this.Sort)); } }

            /// <summary>
            /// Title text.
            /// </summary>
            public string TranslationEntries
            { get { return _translationEntries; } set { OnPropertyChanging(nameof(this.TranslationEntries)); _translationEntries = value; OnPropertyChanged(nameof(this.TranslationEntries)); } }

            #endregion Members

            internal __Main()
            {
            }

            internal void LoadInternal(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("Main", out XSection section))
                {
                    this.Informartion = section.Properties.GetStringValue("Informartion", _informartion_d);
                    this.Sections = section.Properties.GetStringValue("Sections", _sections_d);
                    this.Sort = section.Properties.GetStringValue("Sort", _sort_d);
                    this.SectionEntries = section.Properties.GetStringValue("SectionEntries", _sectionEntries_d);
                    this.TranslationEntries = section.Properties.GetStringValue("TranslationEntries", _translationEntries_d);
                    this.HasNewVersion = section.Properties.GetStringValue("HasNewVersion", _hasNewVersion_d);
                }
            }

            internal void ResetDefaultInternal()
            {
                this.Informartion = _informartion_d;
                this.Sections = _sections_d;
                this.Sort = _sort_d;
                this.SectionEntries = _sectionEntries_d;
                this.TranslationEntries = _translationEntries_d;
                this.HasNewVersion = _hasNewVersion_d;
            }

            internal void SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Main");
                if (defaultField)
                {
                    section.Properties.AddString("Informartion", _informartion_d).Comment.SetValue(_informartion_c, true);
                    section.Properties.AddString("Sections", _sections_d).Comment.SetValue(_sections_c, true);
                    section.Properties.AddString("Sort", _sort_d).Comment.SetValue(_sort_c, true);
                    section.Properties.AddString("SectionEntries", _sectionEntries_d).Comment.SetValue(_sectionEntries_c, true);
                    section.Properties.AddString("TranslationEntries", _translationEntries_d).Comment.SetValue(_translationEntries_c, true);
                    section.Properties.AddString("HasNewVersion", _hasNewVersion_d).Comment.SetValue(_hasNewVersion_c, true);
                }
                else
                {
                    section.Properties.AddString("Informartion", this.Informartion).Comment.SetValue(_informartion_c, true);
                    section.Properties.AddString("Sections", this.Sections).Comment.SetValue(_sections_c, true);
                    section.Properties.AddString("Sort", this.Sort).Comment.SetValue(_sort_c, true);
                    section.Properties.AddString("SectionEntries", this.SectionEntries).Comment.SetValue(_sectionEntries_c, true);
                    section.Properties.AddString("TranslationEntries", this.TranslationEntries).Comment.SetValue(_translationEntries_c, true);
                    section.Properties.AddString("HasNewVersion", this.HasNewVersion).Comment.SetValue(_hasNewVersion_c, true);
                }
            }
        }

        /// <summary>
        /// Menu section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __Menu : INotifyPropertyChanging, INotifyPropertyChanged
        {
            #region Events

            /// <summary>
            /// Property changed event handler.
            /// </summary>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Property changing event handler.
            /// </summary>
            public event PropertyChangingEventHandler? PropertyChanging;

            private void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            private void OnPropertyChanging(string name)
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            }

            #endregion Events

            #region Comments

            private const string _about_c = "Menu button, Show dialog for app information.";
            private const string _createNew_c = "Menu button, create new document.";
            private const string _exit_c = "Exit app.";
            private const string _file_c = "Menu button, Top item.";
            private const string _help_c = "Menu button, Top item.";
            private const string _open_c = "Menu button, Show dialog for select open file.";
            private const string _options_c = "Menu button, Top item.";
            private const string _save_c = "Menu button, Save to lang file.";
            private const string _saveAs_c = "Menu button, Show dialog for select save file.";
            private const string _saveCSharpCodeAs_c = "Menu button, Show dialog for select save file.";
            private const string _website_c = "Menu button, Navigate to project url.";

            #endregion Comments

            #region Default

            private const string _about_d = "_About";
            private const string _createNew_d = "_New...";
            private const string _exit_d = "E_xit";
            private const string _file_d = "_File";
            private const string _help_d = "_Help";
            private const string _open_d = "_Open...";
            private const string _options_d = "_Options";
            private const string _save_d = "_Save";
            private const string _saveAs_d = "Save _As...";
            private const string _saveCSharpCodeAs_d = "Save C# code As...";
            private const string _website_d = "_Website";

            #endregion Default

            #region Members

            private string _about = _about_d;
            private string _createNew = _createNew_d;
            private string _exit = _exit_d;
            private string _file = _file_d;
            private string _help = _help_d;
            private string _open = _open_d;
            private string _options = _options_d;
            private string _save = _save_d;
            private string _saveAs = _saveAs_d;
            private string _saveCSharpCodeAs = _saveCSharpCodeAs_d;
            private string _website = _website_d;

            /// <summary>
            /// Menu button, Show dialog for app information.
            /// </summary>
            public string About
            { get { return _about; } set { OnPropertyChanging(nameof(this.About)); _about = value; OnPropertyChanged(nameof(this.About)); } }

            /// <summary>
            /// Menu button, create new document.
            /// </summary>
            public string CreateNew
            { get { return _createNew; } set { OnPropertyChanging(nameof(this.CreateNew)); _createNew = value; OnPropertyChanged(nameof(this.CreateNew)); } }

            /// <summary>
            /// Exit app.
            /// </summary>
            public string Exit
            { get { return _exit; } set { OnPropertyChanging(nameof(this.Exit)); _exit = value; OnPropertyChanged(nameof(this.Exit)); } }

            /// <summary>
            /// Menu button, Top item.
            /// </summary>
            public string File
            { get { return _file; } set { OnPropertyChanging(nameof(this.File)); _file = value; OnPropertyChanged(nameof(this.File)); } }

            /// <summary>
            /// Menu button, Top item.
            /// </summary>
            public string Help
            { get { return _help; } set { OnPropertyChanging(nameof(this.Help)); _help = value; OnPropertyChanged(nameof(this.Help)); } }

            /// <summary>
            /// Menu button, Show dialog for select open file.
            /// </summary>
            public string Open
            { get { return _open; } set { OnPropertyChanging(nameof(this.Open)); _open = value; OnPropertyChanged(nameof(this.Open)); } }

            /// <summary>
            /// Menu button, Top item.
            /// </summary>
            public string Options
            { get { return _options; } set { OnPropertyChanging(nameof(this.Options)); _options = value; OnPropertyChanged(nameof(this.Options)); } }

            /// <summary>
            /// Menu button, Save to lang file.
            /// </summary>
            public string Save
            { get { return _save; } set { OnPropertyChanging(nameof(this.Save)); _save = value; OnPropertyChanged(nameof(this.Save)); } }

            /// <summary>
            /// Menu button, Show dialog for select save file.
            /// </summary>
            public string SaveAs
            { get { return _saveAs; } set { OnPropertyChanging(nameof(this.SaveAs)); _saveAs = value; OnPropertyChanged(nameof(this.SaveAs)); } }

            /// <summary>
            /// Menu button, Show dialog for select save file.
            /// </summary>
            public string SaveCSharpCodeAs
            { get { return _saveCSharpCodeAs; } set { OnPropertyChanging(nameof(this.SaveCSharpCodeAs)); _saveCSharpCodeAs = value; OnPropertyChanged(nameof(this.SaveCSharpCodeAs)); } }

            /// <summary>
            /// Menu button, Navigate to project url.
            /// </summary>
            public string Website
            { get { return _website; } set { OnPropertyChanging(nameof(this.Website)); _website = value; OnPropertyChanged(nameof(this.Website)); } }

            #endregion Members

            internal __Menu()
            {
            }

            internal void LoadInternal(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("Menu", out XSection section))
                {
                    this.File = section.Properties.GetStringValue("File", _file_d);
                    this.CreateNew = section.Properties.GetStringValue("CreateNew", _createNew_d);
                    this.Open = section.Properties.GetStringValue("Open", _open_d);
                    this.Save = section.Properties.GetStringValue("Save", _save_d);
                    this.SaveAs = section.Properties.GetStringValue("SaveAs", _saveAs_d);
                    this.SaveCSharpCodeAs = section.Properties.GetStringValue("SaveCSharpCodeAs", _saveCSharpCodeAs_d);
                    this.Exit = section.Properties.GetStringValue("Exit", _exit_d);
                    this.Options = section.Properties.GetStringValue("Options", _options_d);
                    this.Help = section.Properties.GetStringValue("Help", _help_d);
                    this.Website = section.Properties.GetStringValue("Website", _website_d);
                    this.About = section.Properties.GetStringValue("About", _about_d);
                }
            }

            internal void ResetDefaultInternal()
            {
                this.File = _file_d;
                this.CreateNew = _createNew_d;
                this.Open = _open_d;
                this.Save = _save_d;
                this.SaveAs = _saveAs_d;
                this.SaveCSharpCodeAs = _saveCSharpCodeAs_d;
                this.Exit = _exit_d;
                this.Options = _options_d;
                this.Help = _help_d;
                this.Website = _website_d;
                this.About = _about_d;
            }

            internal void SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Menu");
                if (defaultField)
                {
                    section.Properties.AddString("File", _file_d).Comment.SetValue(_file_c, true);
                    section.Properties.AddString("CreateNew", _createNew_d).Comment.SetValue(_createNew_c, true);
                    section.Properties.AddString("Open", _open_d).Comment.SetValue(_open_c, true);
                    section.Properties.AddString("Save", _save_d).Comment.SetValue(_save_c, true);
                    section.Properties.AddString("SaveAs", _saveAs_d).Comment.SetValue(_saveAs_c, true);
                    section.Properties.AddString("SaveCSharpCodeAs", _saveCSharpCodeAs_d).Comment.SetValue(_saveCSharpCodeAs_c, true);
                    section.Properties.AddString("Exit", _exit_d).Comment.SetValue(_exit_c, true);
                    section.Properties.AddString("Options", _options_d).Comment.SetValue(_options_c, true);
                    section.Properties.AddString("Help", _help_d).Comment.SetValue(_help_c, true);
                    section.Properties.AddString("Website", _website_d).Comment.SetValue(_website_c, true);
                    section.Properties.AddString("About", _about_d).Comment.SetValue(_about_c, true);
                }
                else
                {
                    section.Properties.AddString("File", this.File).Comment.SetValue(_file_c, true);
                    section.Properties.AddString("CreateNew", this.CreateNew).Comment.SetValue(_createNew_c, true);
                    section.Properties.AddString("Open", this.Open).Comment.SetValue(_open_c, true);
                    section.Properties.AddString("Save", this.Save).Comment.SetValue(_save_c, true);
                    section.Properties.AddString("SaveAs", this.SaveAs).Comment.SetValue(_saveAs_c, true);
                    section.Properties.AddString("SaveCSharpCodeAs", this.SaveCSharpCodeAs).Comment.SetValue(_saveCSharpCodeAs_c, true);
                    section.Properties.AddString("Exit", this.Exit).Comment.SetValue(_exit_c, true);
                    section.Properties.AddString("Options", this.Options).Comment.SetValue(_options_c, true);
                    section.Properties.AddString("Help", this.Help).Comment.SetValue(_help_c, true);
                    section.Properties.AddString("Website", this.Website).Comment.SetValue(_website_c, true);
                    section.Properties.AddString("About", this.About).Comment.SetValue(_about_c, true);
                }
            }
        }

        /// <summary>
        /// ToastMessages section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __ToastMessages : INotifyPropertyChanging, INotifyPropertyChanged
        {
            #region Events

            /// <summary>
            /// Property changed event handler.
            /// </summary>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// Property changing event handler.
            /// </summary>
            public event PropertyChangingEventHandler? PropertyChanging;

            private void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            private void OnPropertyChanging(string name)
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
            }

            #endregion Events

            #region Comments

            private const string _languageFileSaved_c = "Toast content.";

            #endregion Comments

            #region Default

            private const string _languageFileSaved_d = "Language file saved.";

            #endregion Default

            #region Members

            private string _languageFileSaved = _languageFileSaved_d;

            /// <summary>
            /// Toast content.
            /// </summary>
            public string LanguageFileSaved
            { get { return _languageFileSaved; } set { OnPropertyChanging(nameof(this.LanguageFileSaved)); _languageFileSaved = value; OnPropertyChanged(nameof(this.LanguageFileSaved)); } }

            #endregion Members

            internal __ToastMessages()
            {
            }

            internal void LoadInternal(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("ToastMessages", out XSection section))
                {
                    this.LanguageFileSaved = section.Properties.GetStringValue("LanguageFileSaved", _languageFileSaved_d);
                }
            }

            internal void ResetDefaultInternal()
            {
                this.LanguageFileSaved = _languageFileSaved_d;
            }

            internal void SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("ToastMessages");
                if (defaultField)
                {
                    section.Properties.AddString("LanguageFileSaved", _languageFileSaved_d).Comment.SetValue(_languageFileSaved_c, true);
                }
                else
                {
                    section.Properties.AddString("LanguageFileSaved", this.LanguageFileSaved).Comment.SetValue(_languageFileSaved_c, true);
                }
            }
        }
    }
}