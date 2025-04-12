using Honoo.Configuration;
using System.ComponentModel;
using System.IO;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    /// <summary>
    /// Language localisation class. Using in WPF basic class [.NET 6.0+][With Project field <Nullable>enable</Nullable>].
    /// Install nuget package: <see href="https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager"/>.
    /// </summary>
    public sealed class Localization
    {
        #region Instance

        /// <summary>
        /// Language localisation instance.
        /// </summary>
        public static Localization Instance { get; } = new Localization();

        #endregion Instance

        #region Members

        /// <summary>
        /// Informartion section.
        /// </summary>
        public __Informartion Informartion { get; } = new __Informartion();

        /// <summary>
        /// Menu section.
        /// </summary>
        public __Menu Menu { get; } = new __Menu();

        /// <summary>
        /// Main section.
        /// </summary>
        public __Main Main { get; } = new __Main();

        /// <summary>
        /// Messages section.
        /// </summary>
        public __Messages Messages { get; } = new __Messages();

        #endregion Members

        /// <summary>
        /// initialize new instance of Localization class.
        /// </summary>
        internal Localization()
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
                this.Informartion.Load(manager);
                this.Menu.Load(manager);
                this.Main.Load(manager);
                this.Messages.Load(manager);
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
                this.Informartion.Load(manager);
                this.Menu.Load(manager);
                this.Main.Load(manager);
                this.Messages.Load(manager);
            }
        }

        /// <summary>
        /// Reset all properties to default values.
        /// </summary>
        public void ResetDefault()
        {
            this.Informartion.ResetDefault();
            this.Menu.ResetDefault();
            this.Main.ResetDefault();
            this.Messages.ResetDefault();
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
                this.Informartion.Save(defaultField, manager);
                this.Menu.Save(defaultField, manager);
                this.Main.Save(defaultField, manager);
                this.Messages.Save(defaultField, manager);
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
                this.Informartion.Save(defaultField, manager);
                this.Menu.Save(defaultField, manager);
                this.Main.Save(defaultField, manager);
                this.Messages.Save(defaultField, manager);
                manager.Save(stream);
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

            private const string _appName_d = "Application name";
            private const string _appVer_d = "1.0.0";
            private const string _author_d = "HLLC";
            private const string _langName_d = "en-US";
            private const string _langVer_d = "1.0.0";
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

            internal void Load(XConfigManager manager)
            {
                this.AppName = manager.Default.Properties.GetStringValue("AppName", _appName_d);
                this.AppVer = manager.Default.Properties.GetStringValue("AppVer", _appVer_d);
                this.LangName = manager.Default.Properties.GetStringValue("LangName", _langName_d);
                this.LangVer = manager.Default.Properties.GetStringValue("LangVer", _langVer_d);
                this.Author = manager.Default.Properties.GetStringValue("Author", _author_d);
                this.Url = manager.Default.Properties.GetStringValue("Url", _url_d);
                this.Remarks = manager.Default.Properties.GetStringValue("Remarks", _remarks_d);
            }

            internal void ResetDefault()
            {
                this.AppName = _appName_d;
                this.AppVer = _appVer_d;
                this.LangName = _langName_d;
                this.LangVer = _langVer_d;
                this.Author = _author_d;
                this.Url = _url_d;
                this.Remarks = _remarks_d;
            }

            internal void Save(bool defaultField, XConfigManager manager)
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

            private const string _file_c = "Menu button, Top item.";
            private const string _createTemplate_c = "Menu button, create new template.";
            private const string _openTemplate_c = "Menu button, Show dialog for select open file.";
            private const string _saveAs_c = "Menu button, Show dialog for select save file.";
            private const string _saveCSharpCodeAs_c = "Menu button, Show dialog for select save file.";
            private const string _exit_c = "Exit app.";
            private const string _options_c = "Menu button, Top item.";
            private const string _help_c = "Menu button, Top item.";
            private const string _about_c = "Menu button, Show dialog for app information.";

            #endregion Comments

            #region Default

            private const string _file_d = "_File";
            private const string _createTemplate_d = "_Create template...";
            private const string _openTemplate_d = "_Open template...";
            private const string _saveAs_d = "Save _As...";
            private const string _saveCSharpCodeAs_d = "Save C# code As...";
            private const string _exit_d = "E_xit";
            private const string _options_d = "_Options";
            private const string _help_d = "_Help";
            private const string _about_d = "_About";

            #endregion Default

            #region Members

            private string _file = _file_d;
            private string _createTemplate = _createTemplate_d;
            private string _openTemplate = _openTemplate_d;
            private string _saveAs = _saveAs_d;
            private string _saveCSharpCodeAs = _saveCSharpCodeAs_d;
            private string _exit = _exit_d;
            private string _options = _options_d;
            private string _help = _help_d;
            private string _about = _about_d;

            /// <summary>
            /// Menu button, Top item.
            /// </summary>
            public string File
            { get { return _file; } set { OnPropertyChanging(nameof(this.File)); _file = value; OnPropertyChanged(nameof(this.File)); } }

            /// <summary>
            /// Menu button, create new template.
            /// </summary>
            public string CreateTemplate
            { get { return _createTemplate; } set { OnPropertyChanging(nameof(this.CreateTemplate)); _createTemplate = value; OnPropertyChanged(nameof(this.CreateTemplate)); } }

            /// <summary>
            /// Menu button, Show dialog for select open file.
            /// </summary>
            public string OpenTemplate
            { get { return _openTemplate; } set { OnPropertyChanging(nameof(this.OpenTemplate)); _openTemplate = value; OnPropertyChanged(nameof(this.OpenTemplate)); } }

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
            /// Exit app.
            /// </summary>
            public string Exit
            { get { return _exit; } set { OnPropertyChanging(nameof(this.Exit)); _exit = value; OnPropertyChanged(nameof(this.Exit)); } }

            /// <summary>
            /// Menu button, Top item.
            /// </summary>
            public string Options
            { get { return _options; } set { OnPropertyChanging(nameof(this.Options)); _options = value; OnPropertyChanged(nameof(this.Options)); } }

            /// <summary>
            /// Menu button, Top item.
            /// </summary>
            public string Help
            { get { return _help; } set { OnPropertyChanging(nameof(this.Help)); _help = value; OnPropertyChanged(nameof(this.Help)); } }

            /// <summary>
            /// Menu button, Show dialog for app information.
            /// </summary>
            public string About
            { get { return _about; } set { OnPropertyChanging(nameof(this.About)); _about = value; OnPropertyChanged(nameof(this.About)); } }

            #endregion Members

            internal __Menu()
            {
            }

            internal void Load(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("Menu", out XSection section))
                {
                    this.File = section.Properties.GetStringValue("File", _file_d);
                    this.CreateTemplate = section.Properties.GetStringValue("CreateTemplate", _createTemplate_d);
                    this.OpenTemplate = section.Properties.GetStringValue("OpenTemplate", _openTemplate_d);
                    this.SaveAs = section.Properties.GetStringValue("SaveAs", _saveAs_d);
                    this.SaveCSharpCodeAs = section.Properties.GetStringValue("SaveCSharpCodeAs", _saveCSharpCodeAs_d);
                    this.Exit = section.Properties.GetStringValue("Exit", _exit_d);
                    this.Options = section.Properties.GetStringValue("Options", _options_d);
                    this.Help = section.Properties.GetStringValue("Help", _help_d);
                    this.About = section.Properties.GetStringValue("About", _about_d);
                }
            }

            internal void ResetDefault()
            {
                this.File = _file_d;
                this.CreateTemplate = _createTemplate_d;
                this.OpenTemplate = _openTemplate_d;
                this.SaveAs = _saveAs_d;
                this.SaveCSharpCodeAs = _saveCSharpCodeAs_d;
                this.Exit = _exit_d;
                this.Options = _options_d;
                this.Help = _help_d;
                this.About = _about_d;
            }

            internal void Save(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Menu");
                if (defaultField)
                {
                    section.Properties.AddString("File", _file_d).Comment.SetValue(_file_c);
                    section.Properties.AddString("CreateTemplate", _createTemplate_d).Comment.SetValue(_createTemplate_c);
                    section.Properties.AddString("OpenTemplate", _openTemplate_d).Comment.SetValue(_openTemplate_c);
                    section.Properties.AddString("SaveAs", _saveAs_d).Comment.SetValue(_saveAs_c);
                    section.Properties.AddString("SaveCSharpCodeAs", _saveCSharpCodeAs_d).Comment.SetValue(_saveCSharpCodeAs_c);
                    section.Properties.AddString("Exit", _exit_d).Comment.SetValue(_exit_c);
                    section.Properties.AddString("Options", _options_d).Comment.SetValue(_options_c);
                    section.Properties.AddString("Help", _help_d).Comment.SetValue(_help_c);
                    section.Properties.AddString("About", _about_d).Comment.SetValue(_about_c);
                }
                else
                {
                    section.Properties.AddString("File", this.File).Comment.SetValue(_file_c);
                    section.Properties.AddString("CreateTemplate", this.CreateTemplate).Comment.SetValue(_createTemplate_c);
                    section.Properties.AddString("OpenTemplate", this.OpenTemplate).Comment.SetValue(_openTemplate_c);
                    section.Properties.AddString("SaveAs", this.SaveAs).Comment.SetValue(_saveAs_c);
                    section.Properties.AddString("SaveCSharpCodeAs", this.SaveCSharpCodeAs).Comment.SetValue(_saveCSharpCodeAs_c);
                    section.Properties.AddString("Exit", this.Exit).Comment.SetValue(_exit_c);
                    section.Properties.AddString("Options", this.Options).Comment.SetValue(_options_c);
                    section.Properties.AddString("Help", this.Help).Comment.SetValue(_help_c);
                    section.Properties.AddString("About", this.About).Comment.SetValue(_about_c);
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

            private const string _informartion_c = "Tab name.";
            private const string _sections_c = "Tab name.";
            private const string _sort_c = "Button text.";
            private const string _sectionEntries_c = "Title text.";
            private const string _languageEntries_c = "Title text.";

            #endregion Comments

            #region Default

            private const string _informartion_d = "Informartion";
            private const string _sections_d = "Sections";
            private const string _sort_d = "Sort";
            private const string _sectionEntries_d = "Section entries";
            private const string _languageEntries_d = "Language entries";

            #endregion Default

            #region Members

            private string _informartion = _informartion_d;
            private string _sections = _sections_d;
            private string _sort = _sort_d;
            private string _sectionEntries = _sectionEntries_d;
            private string _languageEntries = _languageEntries_d;

            /// <summary>
            /// Tab name.
            /// </summary>
            public string Informartion
            { get { return _informartion; } set { OnPropertyChanging(nameof(this.Informartion)); _informartion = value; OnPropertyChanged(nameof(this.Informartion)); } }

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
            public string SectionEntries
            { get { return _sectionEntries; } set { OnPropertyChanging(nameof(this.SectionEntries)); _sectionEntries = value; OnPropertyChanged(nameof(this.SectionEntries)); } }

            /// <summary>
            /// Title text.
            /// </summary>
            public string LanguageEntries
            { get { return _languageEntries; } set { OnPropertyChanging(nameof(this.LanguageEntries)); _languageEntries = value; OnPropertyChanged(nameof(this.LanguageEntries)); } }

            #endregion Members

            internal __Main()
            {
            }

            internal void Load(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("Main", out XSection section))
                {
                    this.Informartion = section.Properties.GetStringValue("Informartion", _informartion_d);
                    this.Sections = section.Properties.GetStringValue("Sections", _sections_d);
                    this.Sort = section.Properties.GetStringValue("Sort", _sort_d);
                    this.SectionEntries = section.Properties.GetStringValue("SectionEntries", _sectionEntries_d);
                    this.LanguageEntries = section.Properties.GetStringValue("LanguageEntries", _languageEntries_d);
                }
            }

            internal void ResetDefault()
            {
                this.Informartion = _informartion_d;
                this.Sections = _sections_d;
                this.Sort = _sort_d;
                this.SectionEntries = _sectionEntries_d;
                this.LanguageEntries = _languageEntries_d;
            }

            internal void Save(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Main");
                if (defaultField)
                {
                    section.Properties.AddString("Informartion", _informartion_d).Comment.SetValue(_informartion_c);
                    section.Properties.AddString("Sections", _sections_d).Comment.SetValue(_sections_c);
                    section.Properties.AddString("Sort", _sort_d).Comment.SetValue(_sort_c);
                    section.Properties.AddString("SectionEntries", _sectionEntries_d).Comment.SetValue(_sectionEntries_c);
                    section.Properties.AddString("LanguageEntries", _languageEntries_d).Comment.SetValue(_languageEntries_c);
                }
                else
                {
                    section.Properties.AddString("Informartion", this.Informartion).Comment.SetValue(_informartion_c);
                    section.Properties.AddString("Sections", this.Sections).Comment.SetValue(_sections_c);
                    section.Properties.AddString("Sort", this.Sort).Comment.SetValue(_sort_c);
                    section.Properties.AddString("SectionEntries", this.SectionEntries).Comment.SetValue(_sectionEntries_c);
                    section.Properties.AddString("LanguageEntries", this.LanguageEntries).Comment.SetValue(_languageEntries_c);
                }
            }
        }

        /// <summary>
        /// Messages section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __Messages : INotifyPropertyChanging, INotifyPropertyChanged
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
            private const string _createSctionTip_c = "Dialog content.";
            private const string _createLanguageEntryTip_c = "Dialog content.";
            private const string _nameExists_c = "Dialog content set custom field {0}.";
            private const string _keyExists_c = "Dialog content set custom field {0}.";
            private const string _removeItem_c = "Dialog content set custom field {0}.";
            private const string _saveCodeStandard_c = "Dialog content.";
            private const string _saveCodeWpf_c = "Dialog content.";
            private const string _saveCodeMvvm_c = "Dialog content.";

            #endregion Comments

            #region Default

            private const string _documentExistsCreateNew_d = "Document loaded already. Create new document?";
            private const string _documentExistsLoadNew_d = "Document loaded already. Load new document?";
            private const string _createSctionTip_d = "Input Section name.\r\nThe string using by code member name.\r\nSpaces and special characters cannot be used.";
            private const string _createLanguageEntryTip_d = "Input language entry name.\r\nThe string using by code member name.\r\nSpaces and special characters cannot be used.";
            private const string _nameExists_d = "Name \"{0}\" exists.";
            private const string _keyExists_d = "Key \"{0}\" exists.";
            private const string _removeItem_d = "Remove \"{0}\" ?";
            private const string _saveCodeStandard_d = "Standard class model for all app type";
            private const string _saveCodeWpf_d = "Using in WPF basic class";
            private const string _saveCodeMvvm_d = "MVVM structure for lib - CommunityToolkit.Mvvm";

            #endregion Default

            #region Members

            private string _documentExistsCreateNew = _documentExistsCreateNew_d;
            private string _documentExistsLoadNew = _documentExistsLoadNew_d;
            private string _createSctionTip = _createSctionTip_d;
            private string _createLanguageEntryTip = _createLanguageEntryTip_d;
            private string _nameExists = _nameExists_d;
            private string _keyExists = _keyExists_d;
            private string _removeItem = _removeItem_d;
            private string _saveCodeStandard = _saveCodeStandard_d;
            private string _saveCodeWpf = _saveCodeWpf_d;
            private string _saveCodeMvvm = _saveCodeMvvm_d;

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
            public string CreateSctionTip
            { get { return _createSctionTip; } set { OnPropertyChanging(nameof(this.CreateSctionTip)); _createSctionTip = value; OnPropertyChanged(nameof(this.CreateSctionTip)); } }

            /// <summary>
            /// Dialog content.
            /// </summary>
            public string CreateLanguageEntryTip
            { get { return _createLanguageEntryTip; } set { OnPropertyChanging(nameof(this.CreateLanguageEntryTip)); _createLanguageEntryTip = value; OnPropertyChanged(nameof(this.CreateLanguageEntryTip)); } }

            /// <summary>
            /// Dialog content set custom field {0}.
            /// </summary>
            public string NameExists
            { get { return _nameExists; } set { OnPropertyChanging(nameof(this.NameExists)); _nameExists = value; OnPropertyChanged(nameof(this.NameExists)); } }

            /// <summary>
            /// Dialog content set custom field {0}.
            /// </summary>
            public string KeyExists
            { get { return _keyExists; } set { OnPropertyChanging(nameof(this.KeyExists)); _keyExists = value; OnPropertyChanged(nameof(this.KeyExists)); } }

            /// <summary>
            /// Dialog content set custom field {0}.
            /// </summary>
            public string RemoveItem
            { get { return _removeItem; } set { OnPropertyChanging(nameof(this.RemoveItem)); _removeItem = value; OnPropertyChanged(nameof(this.RemoveItem)); } }

            /// <summary>
            /// Dialog content.
            /// </summary>
            public string SaveCodeStandard
            { get { return _saveCodeStandard; } set { OnPropertyChanging(nameof(this.SaveCodeStandard)); _saveCodeStandard = value; OnPropertyChanged(nameof(this.SaveCodeStandard)); } }

            /// <summary>
            /// Dialog content.
            /// </summary>
            public string SaveCodeWpf
            { get { return _saveCodeWpf; } set { OnPropertyChanging(nameof(this.SaveCodeWpf)); _saveCodeWpf = value; OnPropertyChanged(nameof(this.SaveCodeWpf)); } }

            /// <summary>
            /// Dialog content.
            /// </summary>
            public string SaveCodeMvvm
            { get { return _saveCodeMvvm; } set { OnPropertyChanging(nameof(this.SaveCodeMvvm)); _saveCodeMvvm = value; OnPropertyChanged(nameof(this.SaveCodeMvvm)); } }

            #endregion Members

            internal __Messages()
            {
            }

            internal void Load(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("Messages", out XSection section))
                {
                    this.DocumentExistsCreateNew = section.Properties.GetStringValue("DocumentExistsCreateNew", _documentExistsCreateNew_d);
                    this.DocumentExistsLoadNew = section.Properties.GetStringValue("DocumentExistsLoadNew", _documentExistsLoadNew_d);
                    this.CreateSctionTip = section.Properties.GetStringValue("CreateSctionTip", _createSctionTip_d);
                    this.CreateLanguageEntryTip = section.Properties.GetStringValue("CreateLanguageEntryTip", _createLanguageEntryTip_d);
                    this.NameExists = section.Properties.GetStringValue("NameExists", _nameExists_d);
                    this.KeyExists = section.Properties.GetStringValue("KeyExists", _keyExists_d);
                    this.RemoveItem = section.Properties.GetStringValue("RemoveItem", _removeItem_d);
                    this.SaveCodeStandard = section.Properties.GetStringValue("SaveCodeStandard", _saveCodeStandard_d);
                    this.SaveCodeWpf = section.Properties.GetStringValue("SaveCodeWpf", _saveCodeWpf_d);
                    this.SaveCodeMvvm = section.Properties.GetStringValue("SaveCodeMvvm", _saveCodeMvvm_d);
                }
            }

            internal void ResetDefault()
            {
                this.DocumentExistsCreateNew = _documentExistsCreateNew_d;
                this.DocumentExistsLoadNew = _documentExistsLoadNew_d;
                this.CreateSctionTip = _createSctionTip_d;
                this.CreateLanguageEntryTip = _createLanguageEntryTip_d;
                this.NameExists = _nameExists_d;
                this.KeyExists = _keyExists_d;
                this.RemoveItem = _removeItem_d;
                this.SaveCodeStandard = _saveCodeStandard_d;
                this.SaveCodeWpf = _saveCodeWpf_d;
                this.SaveCodeMvvm = _saveCodeMvvm_d;
            }

            internal void Save(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Messages");
                if (defaultField)
                {
                    section.Properties.AddString("DocumentExistsCreateNew", _documentExistsCreateNew_d).Comment.SetValue(_documentExistsCreateNew_c);
                    section.Properties.AddString("DocumentExistsLoadNew", _documentExistsLoadNew_d).Comment.SetValue(_documentExistsLoadNew_c);
                    section.Properties.AddString("CreateSctionTip", _createSctionTip_d).Comment.SetValue(_createSctionTip_c);
                    section.Properties.AddString("CreateLanguageEntryTip", _createLanguageEntryTip_d).Comment.SetValue(_createLanguageEntryTip_c);
                    section.Properties.AddString("NameExists", _nameExists_d).Comment.SetValue(_nameExists_c);
                    section.Properties.AddString("KeyExists", _keyExists_d).Comment.SetValue(_keyExists_c);
                    section.Properties.AddString("RemoveItem", _removeItem_d).Comment.SetValue(_removeItem_c);
                    section.Properties.AddString("SaveCodeStandard", _saveCodeStandard_d).Comment.SetValue(_saveCodeStandard_c);
                    section.Properties.AddString("SaveCodeWpf", _saveCodeWpf_d).Comment.SetValue(_saveCodeWpf_c);
                    section.Properties.AddString("SaveCodeMvvm", _saveCodeMvvm_d).Comment.SetValue(_saveCodeMvvm_c);
                }
                else
                {
                    section.Properties.AddString("DocumentExistsCreateNew", this.DocumentExistsCreateNew).Comment.SetValue(_documentExistsCreateNew_c);
                    section.Properties.AddString("DocumentExistsLoadNew", this.DocumentExistsLoadNew).Comment.SetValue(_documentExistsLoadNew_c);
                    section.Properties.AddString("CreateSctionTip", this.CreateSctionTip).Comment.SetValue(_createSctionTip_c);
                    section.Properties.AddString("CreateLanguageEntryTip", this.CreateLanguageEntryTip).Comment.SetValue(_createLanguageEntryTip_c);
                    section.Properties.AddString("NameExists", this.NameExists).Comment.SetValue(_nameExists_c);
                    section.Properties.AddString("KeyExists", this.KeyExists).Comment.SetValue(_keyExists_c);
                    section.Properties.AddString("RemoveItem", this.RemoveItem).Comment.SetValue(_removeItem_c);
                    section.Properties.AddString("SaveCodeStandard", this.SaveCodeStandard).Comment.SetValue(_saveCodeStandard_c);
                    section.Properties.AddString("SaveCodeWpf", this.SaveCodeWpf).Comment.SetValue(_saveCodeWpf_c);
                    section.Properties.AddString("SaveCodeMvvm", this.SaveCodeMvvm).Comment.SetValue(_saveCodeMvvm_c);
                }
            }
        }
    }
}