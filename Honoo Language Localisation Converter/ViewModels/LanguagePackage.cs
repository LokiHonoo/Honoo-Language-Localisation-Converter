using Honoo.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    /// <summary>
    /// Language package class. Changed-notify interface implemented [.NET 6.0 later With Project setting field <![CDATA[<Nullable>enable</Nullable>]]>].
    /// <br />Reference in viewmodel to visit or used single instance <see cref="Instance"/> to visit.
    /// <br />e.g.
    /// <code>
    /// <![CDATA[public LanguagePackage LanguagePackageReference { get; } = LanguagePackage.Instance;]]>
    /// <br />
    /// <![CDATA[<TextBlock Text="{Binding LanguagePackageReference.Main.HasNewVersion}" />]]>
    /// </code>
    /// <br />Or:
    /// <code>
    /// <![CDATA[<TextBlock Text="{Binding Main.HasNewVersion, Source={x:Static vm:LanguagePackage.Instance}}" />]]>
    /// </code>
    /// <br />Install nuget packages:
    /// <br /><see href="https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager"/>.
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
        /// Language package creator.
        /// </summary>
        public const string CREATOR = "Honoo Language Localisation Converter 1.0.5";

        /// <summary>
        /// Creator website.
        /// </summary>
        public const string WEBSITE = "https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter";

        /// <summary>
        /// Managed translation entries count.
        /// </summary>
        public int Count { get; } = 37;

        #region Sections

        /// <summary>
        /// DialogMessages section.
        /// </summary>
        public __DialogMessages DialogMessages { get; } = new __DialogMessages();

        /// <summary>
        /// Information section.
        /// </summary>
        public __Information Information { get; } = new __Information();

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

        #endregion Sections

        #endregion Members

        /// <summary>
        /// Initialize new instance of LanguagePackage class.
        /// </summary>
        internal LanguagePackage()
        {
        }

        /// <summary>
        /// Gets information pairs from language file.
        /// </summary>
        /// <param name="stream">Language file name.</param>
        /// <returns>Information pairs.</returns>
        public static Dictionary<string, string> GetInformation(string fileName)
        {
            var pairs = new Dictionary<string, string>();
            using (var manager = new XConfigManager(fileName))
            {
                if (manager.Sections.TryGetValue("Information", out XSection section))
                {
                    foreach (KeyValuePair<string, XProperty> property in section.Properties)
                    {
                        pairs.Add(property.Key, ((XString)property.Value).GetStringValue());
                    }
                }
            }
            return pairs;
        }

        /// <summary>
        /// Gets information pairs from language stream.
        /// </summary>
        /// <param name="stream">Language stream.</param>
        /// <returns>Information pairs.</returns>
        public static Dictionary<string, string> GetInformation(Stream stream)
        {
            var pairs = new Dictionary<string, string>();
            using (var manager = new XConfigManager(stream))
            {
                if (manager.Sections.TryGetValue("Information", out XSection section))
                {
                    foreach (KeyValuePair<string, XProperty> property in section.Properties)
                    {
                        pairs.Add(property.Key, ((XString)property.Value).GetStringValue());
                    }
                }
            }
            return pairs;
        }

        /// <summary>
        /// Load language file and return loaded translation entries count.
        /// </summary>
        /// <param name="fileName">Language file name.</param>
        /// <returns>Loaded translation entries count.</returns>
        public int Load(string fileName)
        {
            return Load(fileName, out _);
        }

        /// <summary>
        /// Load language file and return loaded translation entries count.
        /// </summary>
        /// <param name="fileName">Language file name.</param>
        /// <param name="missingNames">Missing translation entry names when loaded.</param>
        /// <returns>Loaded translation entries count.</returns>
        public int Load(string fileName, out List<string> missingNames)
        {
            var missing = new List<string>();
            int loaded = 0;
            using (var manager = new XConfigManager(fileName))
            {
                loaded += __Information.__LoadInternal(this, manager, missing);
                loaded += __Menu.__LoadInternal(this, manager, missing);
                loaded += __Main.__LoadInternal(this, manager, missing);
                loaded += __DialogMessages.__LoadInternal(this, manager, missing);
                loaded += __ToastMessages.__LoadInternal(this, manager, missing);
            }
            missingNames = missing;
            return loaded;
        }

        /// <summary>
        /// Load language stream and return loaded translation entries count.
        /// </summary>
        /// <param name="stream">Language stream.</param>
        /// <returns>Loaded translation entries count.</returns>
        public int Load(Stream stream)
        {
            return Load(stream, out _);
        }

        /// <summary>
        /// Load language stream and return loaded translation entries count.
        /// </summary>
        /// <param name="stream">Language stream.</param>
        /// <param name="missingNames">Missing translation entry names when loaded.</param>
        /// <returns>Loaded translation entries count.</returns>
        public int Load(Stream stream, out List<string> missingNames)
        {
            var missing = new List<string>();
            int loaded = 0;
            using (var manager = new XConfigManager(stream))
            {
                loaded += __Information.__LoadInternal(this, manager, missing);
                loaded += __Menu.__LoadInternal(this, manager, missing);
                loaded += __Main.__LoadInternal(this, manager, missing);
                loaded += __DialogMessages.__LoadInternal(this, manager, missing);
                loaded += __ToastMessages.__LoadInternal(this, manager, missing);
            }
            missingNames = missing;
            return loaded;
        }

        /// <summary>
        /// Reset all translation entries to default values.
        /// </summary>
        public void ResetDefault()
        {
            __Information.__ResetDefaultInternal(this);
            __Menu.__ResetDefaultInternal(this);
            __Main.__ResetDefaultInternal(this);
            __DialogMessages.__ResetDefaultInternal(this);
            __ToastMessages.__ResetDefaultInternal(this);
        }

        /// <summary>
        /// Save to language file.
        /// </summary>
        /// <param name="defaultField">Select current fields or default fields.</param>
        /// <param name="fileName">Language file name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void Save(bool defaultField, string fileName)
        {
            using (var manager = new XConfigManager())
            {
                manager.Default.Properties.AddString("Creator", CREATOR);
                manager.Default.Properties.AddString("Website", WEBSITE);
                manager.Default.Properties.AddString("CreatedTime", DateTime.Now.ToString("R"));
                __Information.__SaveInternal(this, defaultField, manager);
                __Menu.__SaveInternal(this, defaultField, manager);
                __Main.__SaveInternal(this, defaultField, manager);
                __DialogMessages.__SaveInternal(this, defaultField, manager);
                __ToastMessages.__SaveInternal(this, defaultField, manager);
                manager.Save(fileName);
            }
        }

        /// <summary>
        /// Save to language stream.
        /// </summary>
        /// <param name="defaultField">Select current fields or default fields.</param>
        /// <param name="stream">Language stream.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>")]
        public void Save(bool defaultField, Stream stream)
        {
            using (var manager = new XConfigManager())
            {
                manager.Default.Properties.AddString("Creator", CREATOR);
                manager.Default.Properties.AddString("Website", WEBSITE);
                manager.Default.Properties.AddString("CreatedTime", DateTime.Now.ToString("R"));
                __Information.__SaveInternal(this, defaultField, manager);
                __Menu.__SaveInternal(this, defaultField, manager);
                __Main.__SaveInternal(this, defaultField, manager);
                __DialogMessages.__SaveInternal(this, defaultField, manager);
                __ToastMessages.__SaveInternal(this, defaultField, manager);
                manager.Save(stream);
            }
        }

        #region Information

        /// <summary>
        /// Information section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed class __Information : INotifyPropertyChanging, INotifyPropertyChanged
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

            private const string _appName_c = "Application name.";
            private const string _appVer_c = "Application version.";
            private const string _author_c = "Author name.";
            private const string _email_c = "Author email.";
            private const string _langName_c = "Language name as \"en-US\".";
            private const string _langVer_c = "Language file revision version.";
            private const string _remarks_c = "Remarks.";
            private const string _website_c = "Author related url.";

            #endregion Comments

            #region Default

            private const string _appName_d = "Application name";
            private const string _appVer_d = "1.x";
            private const string _author_d = "Honoo Language Localisation Converter";
            private const string _email_d = "";
            private const string _langName_d = "en-US";
            private const string _langVer_d = "00";
            private const string _remarks_d = "";
            private const string _website_d = "https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter";

            #endregion Default

            #region Members

            private string _appName = _appName_d;
            private string _appVer = _appVer_d;
            private string _author = _author_d;
            private string _email = _email_d;
            private string _langName = _langName_d;
            private string _langVer = _langVer_d;
            private string _remarks = _remarks_d;
            private string _website = _website_d;

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
            /// Author email.
            /// </summary>
            public string Email
            { get { return _email; } set { OnPropertyChanging(nameof(this.Email)); _email = value; OnPropertyChanged(nameof(this.Email)); } }

            /// <summary>
            /// Language name as "en-US".
            /// </summary>
            public string LangName
            { get { return _langName; } set { OnPropertyChanging(nameof(this.LangName)); _langName = value; OnPropertyChanged(nameof(this.LangName)); } }

            /// <summary>
            /// Language file revision version.
            /// </summary>
            public string LangVer
            { get { return _langVer; } set { OnPropertyChanging(nameof(this.LangVer)); _langVer = value; OnPropertyChanged(nameof(this.LangVer)); } }

            /// <summary>
            /// Remarks.
            /// </summary>
            public string Remarks
            { get { return _remarks; } set { OnPropertyChanging(nameof(this.Remarks)); _remarks = value; OnPropertyChanged(nameof(this.Remarks)); } }

            /// <summary>
            /// Author related url.
            /// </summary>
            public string Website
            { get { return _website; } set { OnPropertyChanging(nameof(this.Website)); _website = value; OnPropertyChanged(nameof(this.Website)); } }

            #endregion Members

            internal __Information()
            {
            }

            internal static int __LoadInternal(LanguagePackage instance, XConfigManager manager, List<string> missing)
            {
                return instance.Information.__LoadInternal(manager, missing);
            }

            internal static void __ResetDefaultInternal(LanguagePackage instance)
            {
                instance.Information.__ResetDefaultInternal();
            }

            internal static void __SaveInternal(LanguagePackage instance, bool defaultField, XConfigManager manager)
            {
                instance.Information.__SaveInternal(defaultField, manager);
            }

            private static string __GetTranslationEntryInternal(XSection section, string name, string defaultValue, List<string> missing, ref int loaded)
            {
                if (section.Properties.TryGetStringValue(name, out string value))
                {
                    loaded++;
                    return value;
                }
                else
                {
                    missing.Add(name);
                    return defaultValue;
                }
            }

            private int __LoadInternal(XConfigManager manager, List<string> missing)
            {
                if (manager.Sections.TryGetValue("Information", out XSection section))
                {
                    int loaded = 0;
                    this.AppName = __GetTranslationEntryInternal(section, "AppName", _appName_d, missing, ref loaded);
                    this.AppVer = __GetTranslationEntryInternal(section, "AppVer", _appVer_d, missing, ref loaded);
                    this.LangName = __GetTranslationEntryInternal(section, "LangName", _langName_d, missing, ref loaded);
                    this.LangVer = __GetTranslationEntryInternal(section, "LangVer", _langVer_d, missing, ref loaded);
                    this.Author = __GetTranslationEntryInternal(section, "Author", _author_d, missing, ref loaded);
                    this.Email = __GetTranslationEntryInternal(section, "Email", _email_d, missing, ref loaded);
                    this.Website = __GetTranslationEntryInternal(section, "Website", _website_d, missing, ref loaded);
                    this.Remarks = __GetTranslationEntryInternal(section, "Remarks", _remarks_d, missing, ref loaded);
                    return loaded;
                }
                else
                {
                    missing.Add("AppName");
                    missing.Add("AppVer");
                    missing.Add("LangName");
                    missing.Add("LangVer");
                    missing.Add("Author");
                    missing.Add("Email");
                    missing.Add("Website");
                    missing.Add("Remarks");
                    return 0;
                }
            }

            private void __ResetDefaultInternal()
            {
                this.AppName = _appName_d;
                this.AppVer = _appVer_d;
                this.LangName = _langName_d;
                this.LangVer = _langVer_d;
                this.Author = _author_d;
                this.Email = _email_d;
                this.Website = _website_d;
                this.Remarks = _remarks_d;
            }

            private void __SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Information");
                if (defaultField)
                {
                    section.Properties.AddString("AppName", _appName_d).Comment.SetValue(_appName_c, true);
                    section.Properties.AddString("AppVer", _appVer_d).Comment.SetValue(_appVer_c, true);
                    section.Properties.AddString("LangName", _langName_d).Comment.SetValue(_langName_c, true);
                    section.Properties.AddString("LangVer", _langVer_d).Comment.SetValue(_langVer_c, true);
                    section.Properties.AddString("Author", _author_d).Comment.SetValue(_author_c, true);
                    section.Properties.AddString("Email", _email_d).Comment.SetValue(_email_c, true);
                    section.Properties.AddString("Website", _website_d).Comment.SetValue(_website_c, true);
                    section.Properties.AddString("Remarks", _remarks_d).Comment.SetValue(_remarks_c, true);
                }
                else
                {
                    section.Properties.AddString("AppName", this.AppName).Comment.SetValue(_appName_c, true);
                    section.Properties.AddString("AppVer", this.AppVer).Comment.SetValue(_appVer_c, true);
                    section.Properties.AddString("LangName", this.LangName).Comment.SetValue(_langName_c, true);
                    section.Properties.AddString("LangVer", this.LangVer).Comment.SetValue(_langVer_c, true);
                    section.Properties.AddString("Author", this.Author).Comment.SetValue(_author_c, true);
                    section.Properties.AddString("Email", this.Email).Comment.SetValue(_email_c, true);
                    section.Properties.AddString("Website", this.Website).Comment.SetValue(_website_c, true);
                    section.Properties.AddString("Remarks", this.Remarks).Comment.SetValue(_remarks_c, true);
                }
            }
        }

        #endregion Information

        #region Menu

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
            private const string _exit_c = "Menu button, Exit app.";
            private const string _file_c = "Menu button, Top item.";
            private const string _help_c = "Menu button, Top item.";
            private const string _open_c = "Menu button, Show dialog for select open file.";
            private const string _options_c = "Menu button, Top item.";
            private const string _save_c = "Menu button, Save to lang file.";
            private const string _saveAs_c = "Menu button, Show dialog for select save file.";
            private const string _saveCSharpCodeAs_c = "Menu button, Show dialog for select save file.";
            private const string _website_c = "Menu button, Navigate to project website.";

            #endregion Comments

            #region Default

            private const string _about_d = "_About...";
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
            /// Menu button, Exit app.
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
            /// Menu button, Navigate to project website.
            /// </summary>
            public string Website
            { get { return _website; } set { OnPropertyChanging(nameof(this.Website)); _website = value; OnPropertyChanged(nameof(this.Website)); } }

            #endregion Members

            internal __Menu()
            {
            }

            internal static int __LoadInternal(LanguagePackage instance, XConfigManager manager, List<string> missing)
            {
                return instance.Menu.__LoadInternal(manager, missing);
            }

            internal static void __ResetDefaultInternal(LanguagePackage instance)
            {
                instance.Menu.__ResetDefaultInternal();
            }

            internal static void __SaveInternal(LanguagePackage instance, bool defaultField, XConfigManager manager)
            {
                instance.Menu.__SaveInternal(defaultField, manager);
            }

            private static string __GetTranslationEntryInternal(XSection section, string name, string defaultValue, List<string> missing, ref int loaded)
            {
                if (section.Properties.TryGetStringValue(name, out string value))
                {
                    loaded++;
                    return value;
                }
                else
                {
                    missing.Add(name);
                    return defaultValue;
                }
            }

            private int __LoadInternal(XConfigManager manager, List<string> missing)
            {
                if (manager.Sections.TryGetValue("Menu", out XSection section))
                {
                    int loaded = 0;
                    this.File = __GetTranslationEntryInternal(section, "File", _file_d, missing, ref loaded);
                    this.CreateNew = __GetTranslationEntryInternal(section, "CreateNew", _createNew_d, missing, ref loaded);
                    this.Open = __GetTranslationEntryInternal(section, "Open", _open_d, missing, ref loaded);
                    this.Save = __GetTranslationEntryInternal(section, "Save", _save_d, missing, ref loaded);
                    this.SaveAs = __GetTranslationEntryInternal(section, "SaveAs", _saveAs_d, missing, ref loaded);
                    this.SaveCSharpCodeAs = __GetTranslationEntryInternal(section, "SaveCSharpCodeAs", _saveCSharpCodeAs_d, missing, ref loaded);
                    this.Exit = __GetTranslationEntryInternal(section, "Exit", _exit_d, missing, ref loaded);
                    this.Options = __GetTranslationEntryInternal(section, "Options", _options_d, missing, ref loaded);
                    this.Help = __GetTranslationEntryInternal(section, "Help", _help_d, missing, ref loaded);
                    this.Website = __GetTranslationEntryInternal(section, "Website", _website_d, missing, ref loaded);
                    this.About = __GetTranslationEntryInternal(section, "About", _about_d, missing, ref loaded);
                    return loaded;
                }
                else
                {
                    missing.Add("File");
                    missing.Add("CreateNew");
                    missing.Add("Open");
                    missing.Add("Save");
                    missing.Add("SaveAs");
                    missing.Add("SaveCSharpCodeAs");
                    missing.Add("Exit");
                    missing.Add("Options");
                    missing.Add("Help");
                    missing.Add("Website");
                    missing.Add("About");
                    return 0;
                }
            }

            private void __ResetDefaultInternal()
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

            private void __SaveInternal(bool defaultField, XConfigManager manager)
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

        #endregion Menu

        #region Main

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

            private const string _hasNewVersion_c = "StatusBar text.";
            private const string _information_c = "Tab title text.";
            private const string _sectionEntries_c = "Title text.";
            private const string _sections_c = "Tab title text.";
            private const string _sort_c = "Button text.";
            private const string _translationEntries_c = "Title text.";

            #endregion Comments

            #region Default

            private const string _hasNewVersion_d = "New version published";
            private const string _information_d = "Information";
            private const string _sectionEntries_d = "Section entries";
            private const string _sections_d = "Sections";
            private const string _sort_d = "Sort";
            private const string _translationEntries_d = "Translation entries";

            #endregion Default

            #region Members

            private string _hasNewVersion = _hasNewVersion_d;
            private string _information = _information_d;
            private string _sectionEntries = _sectionEntries_d;
            private string _sections = _sections_d;
            private string _sort = _sort_d;
            private string _translationEntries = _translationEntries_d;

            /// <summary>
            /// StatusBar text.
            /// </summary>
            public string HasNewVersion
            { get { return _hasNewVersion; } set { OnPropertyChanging(nameof(this.HasNewVersion)); _hasNewVersion = value; OnPropertyChanged(nameof(this.HasNewVersion)); } }

            /// <summary>
            /// Tab title text.
            /// </summary>
            public string Information
            { get { return _information; } set { OnPropertyChanging(nameof(this.Information)); _information = value; OnPropertyChanged(nameof(this.Information)); } }

            /// <summary>
            /// Title text.
            /// </summary>
            public string SectionEntries
            { get { return _sectionEntries; } set { OnPropertyChanging(nameof(this.SectionEntries)); _sectionEntries = value; OnPropertyChanged(nameof(this.SectionEntries)); } }

            /// <summary>
            /// Tab title text.
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

            internal static int __LoadInternal(LanguagePackage instance, XConfigManager manager, List<string> missing)
            {
                return instance.Main.__LoadInternal(manager, missing);
            }

            internal static void __ResetDefaultInternal(LanguagePackage instance)
            {
                instance.Main.__ResetDefaultInternal();
            }

            internal static void __SaveInternal(LanguagePackage instance, bool defaultField, XConfigManager manager)
            {
                instance.Main.__SaveInternal(defaultField, manager);
            }

            private static string __GetTranslationEntryInternal(XSection section, string name, string defaultValue, List<string> missing, ref int loaded)
            {
                if (section.Properties.TryGetStringValue(name, out string value))
                {
                    loaded++;
                    return value;
                }
                else
                {
                    missing.Add(name);
                    return defaultValue;
                }
            }

            private int __LoadInternal(XConfigManager manager, List<string> missing)
            {
                if (manager.Sections.TryGetValue("Main", out XSection section))
                {
                    int loaded = 0;
                    this.Information = __GetTranslationEntryInternal(section, "Information", _information_d, missing, ref loaded);
                    this.Sections = __GetTranslationEntryInternal(section, "Sections", _sections_d, missing, ref loaded);
                    this.Sort = __GetTranslationEntryInternal(section, "Sort", _sort_d, missing, ref loaded);
                    this.SectionEntries = __GetTranslationEntryInternal(section, "SectionEntries", _sectionEntries_d, missing, ref loaded);
                    this.TranslationEntries = __GetTranslationEntryInternal(section, "TranslationEntries", _translationEntries_d, missing, ref loaded);
                    this.HasNewVersion = __GetTranslationEntryInternal(section, "HasNewVersion", _hasNewVersion_d, missing, ref loaded);
                    return loaded;
                }
                else
                {
                    missing.Add("Information");
                    missing.Add("Sections");
                    missing.Add("Sort");
                    missing.Add("SectionEntries");
                    missing.Add("TranslationEntries");
                    missing.Add("HasNewVersion");
                    return 0;
                }
            }

            private void __ResetDefaultInternal()
            {
                this.Information = _information_d;
                this.Sections = _sections_d;
                this.Sort = _sort_d;
                this.SectionEntries = _sectionEntries_d;
                this.TranslationEntries = _translationEntries_d;
                this.HasNewVersion = _hasNewVersion_d;
            }

            private void __SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Main");
                if (defaultField)
                {
                    section.Properties.AddString("Information", _information_d).Comment.SetValue(_information_c, true);
                    section.Properties.AddString("Sections", _sections_d).Comment.SetValue(_sections_c, true);
                    section.Properties.AddString("Sort", _sort_d).Comment.SetValue(_sort_c, true);
                    section.Properties.AddString("SectionEntries", _sectionEntries_d).Comment.SetValue(_sectionEntries_c, true);
                    section.Properties.AddString("TranslationEntries", _translationEntries_d).Comment.SetValue(_translationEntries_c, true);
                    section.Properties.AddString("HasNewVersion", _hasNewVersion_d).Comment.SetValue(_hasNewVersion_c, true);
                }
                else
                {
                    section.Properties.AddString("Information", this.Information).Comment.SetValue(_information_c, true);
                    section.Properties.AddString("Sections", this.Sections).Comment.SetValue(_sections_c, true);
                    section.Properties.AddString("Sort", this.Sort).Comment.SetValue(_sort_c, true);
                    section.Properties.AddString("SectionEntries", this.SectionEntries).Comment.SetValue(_sectionEntries_c, true);
                    section.Properties.AddString("TranslationEntries", this.TranslationEntries).Comment.SetValue(_translationEntries_c, true);
                    section.Properties.AddString("HasNewVersion", this.HasNewVersion).Comment.SetValue(_hasNewVersion_c, true);
                }
            }
        }

        #endregion Main

        #region DialogMessages

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
            private const string _saveCodeCommunityToolkit_c = "Dialog content selection text. CommunityToolkit.Mvvm code style.";
            private const string _saveCodeNotifyBasic_c = "Dialog content selection text. INotifyPropertyChanging, INotifyPropertyChanged implemented.";
            private const string _saveCodeStandard_c = "Dialog content selection text.";
            private const string _sectionNameDuplicate_c = "Dialog content, Set field {0}=Section Name.";
            private const string _sectionNameInvalid_c = "Dialog content, Set field {0}=Section Name.";
            private const string _translationNameDuplicate_c = "Dialog content, Set field {0}=Section Name, {1}=Translation Name.";
            private const string _translationNameInvalid_c = "Dialog content, Set field {0}=Section Name, {1}=Translation Name.";

            #endregion Comments

            #region Default

            private const string _documentExistsCreateNew_d = "Document loaded already. Create new document?";
            private const string _documentExistsLoadNew_d = "Document loaded already. Load new document?";
            private const string _exitSaveRemind_d = "The document modified but has not been saved.\r\n\r\nExit application without save?";
            private const string _removeItem_d = "Remove \"{0}\" ?";
            private const string _saveCodeCommunityToolkit_d = "Changed-Notify implemented by CommunityToolkit.Mvvm";
            private const string _saveCodeNotifyBasic_d = "Changed-Notify interface implemented";
            private const string _saveCodeStandard_d = "Standard class model, Changed-Notify NOT supported";
            private const string _sectionNameDuplicate_d = "Section \"{0}\" has duplicate name.";
            private const string _sectionNameInvalid_d = "Section \"{0}\" string can't be empty and special characters.";
            private const string _translationNameDuplicate_d = "Section \"{0}\"'s translation entry \"{1}\" has duplicate name.";
            private const string _translationNameInvalid_d = "Section \"{0}\"'s translation entry \"{1}\" string can't be empty and special characters.";

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
            private string _sectionNameInvalid = _sectionNameInvalid_d;
            private string _translationNameDuplicate = _translationNameDuplicate_d;
            private string _translationNameInvalid = _translationNameInvalid_d;

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
            /// Dialog content selection text. CommunityToolkit.Mvvm code style.
            /// </summary>
            public string SaveCodeCommunityToolkit
            { get { return _saveCodeCommunityToolkit; } set { OnPropertyChanging(nameof(this.SaveCodeCommunityToolkit)); _saveCodeCommunityToolkit = value; OnPropertyChanged(nameof(this.SaveCodeCommunityToolkit)); } }

            /// <summary>
            /// Dialog content selection text. INotifyPropertyChanging, INotifyPropertyChanged implemented.
            /// </summary>
            public string SaveCodeNotifyBasic
            { get { return _saveCodeNotifyBasic; } set { OnPropertyChanging(nameof(this.SaveCodeNotifyBasic)); _saveCodeNotifyBasic = value; OnPropertyChanged(nameof(this.SaveCodeNotifyBasic)); } }

            /// <summary>
            /// Dialog content selection text.
            /// </summary>
            public string SaveCodeStandard
            { get { return _saveCodeStandard; } set { OnPropertyChanging(nameof(this.SaveCodeStandard)); _saveCodeStandard = value; OnPropertyChanged(nameof(this.SaveCodeStandard)); } }

            /// <summary>
            /// Dialog content, Set field {0}=Section Name.
            /// </summary>
            public string SectionNameDuplicate
            { get { return _sectionNameDuplicate; } set { OnPropertyChanging(nameof(this.SectionNameDuplicate)); _sectionNameDuplicate = value; OnPropertyChanged(nameof(this.SectionNameDuplicate)); } }

            /// <summary>
            /// Dialog content, Set field {0}=Section Name.
            /// </summary>
            public string SectionNameInvalid
            { get { return _sectionNameInvalid; } set { OnPropertyChanging(nameof(this.SectionNameInvalid)); _sectionNameInvalid = value; OnPropertyChanged(nameof(this.SectionNameInvalid)); } }

            /// <summary>
            /// Dialog content, Set field {0}=Section Name, {1}=Translation Name.
            /// </summary>
            public string TranslationNameDuplicate
            { get { return _translationNameDuplicate; } set { OnPropertyChanging(nameof(this.TranslationNameDuplicate)); _translationNameDuplicate = value; OnPropertyChanged(nameof(this.TranslationNameDuplicate)); } }

            /// <summary>
            /// Dialog content, Set field {0}=Section Name, {1}=Translation Name.
            /// </summary>
            public string TranslationNameInvalid
            { get { return _translationNameInvalid; } set { OnPropertyChanging(nameof(this.TranslationNameInvalid)); _translationNameInvalid = value; OnPropertyChanged(nameof(this.TranslationNameInvalid)); } }

            #endregion Members

            internal __DialogMessages()
            {
            }

            internal static int __LoadInternal(LanguagePackage instance, XConfigManager manager, List<string> missing)
            {
                return instance.DialogMessages.__LoadInternal(manager, missing);
            }

            internal static void __ResetDefaultInternal(LanguagePackage instance)
            {
                instance.DialogMessages.__ResetDefaultInternal();
            }

            internal static void __SaveInternal(LanguagePackage instance, bool defaultField, XConfigManager manager)
            {
                instance.DialogMessages.__SaveInternal(defaultField, manager);
            }

            private static string __GetTranslationEntryInternal(XSection section, string name, string defaultValue, List<string> missing, ref int loaded)
            {
                if (section.Properties.TryGetStringValue(name, out string value))
                {
                    loaded++;
                    return value;
                }
                else
                {
                    missing.Add(name);
                    return defaultValue;
                }
            }

            private int __LoadInternal(XConfigManager manager, List<string> missing)
            {
                if (manager.Sections.TryGetValue("DialogMessages", out XSection section))
                {
                    int loaded = 0;
                    this.DocumentExistsCreateNew = __GetTranslationEntryInternal(section, "DocumentExistsCreateNew", _documentExistsCreateNew_d, missing, ref loaded);
                    this.DocumentExistsLoadNew = __GetTranslationEntryInternal(section, "DocumentExistsLoadNew", _documentExistsLoadNew_d, missing, ref loaded);
                    this.SectionNameInvalid = __GetTranslationEntryInternal(section, "SectionNameInvalid", _sectionNameInvalid_d, missing, ref loaded);
                    this.SectionNameDuplicate = __GetTranslationEntryInternal(section, "SectionNameDuplicate", _sectionNameDuplicate_d, missing, ref loaded);
                    this.TranslationNameInvalid = __GetTranslationEntryInternal(section, "TranslationNameInvalid", _translationNameInvalid_d, missing, ref loaded);
                    this.TranslationNameDuplicate = __GetTranslationEntryInternal(section, "TranslationNameDuplicate", _translationNameDuplicate_d, missing, ref loaded);
                    this.RemoveItem = __GetTranslationEntryInternal(section, "RemoveItem", _removeItem_d, missing, ref loaded);
                    this.SaveCodeStandard = __GetTranslationEntryInternal(section, "SaveCodeStandard", _saveCodeStandard_d, missing, ref loaded);
                    this.SaveCodeNotifyBasic = __GetTranslationEntryInternal(section, "SaveCodeNotifyBasic", _saveCodeNotifyBasic_d, missing, ref loaded);
                    this.SaveCodeCommunityToolkit = __GetTranslationEntryInternal(section, "SaveCodeCommunityToolkit", _saveCodeCommunityToolkit_d, missing, ref loaded);
                    this.ExitSaveRemind = __GetTranslationEntryInternal(section, "ExitSaveRemind", _exitSaveRemind_d, missing, ref loaded);
                    return loaded;
                }
                else
                {
                    missing.Add("DocumentExistsCreateNew");
                    missing.Add("DocumentExistsLoadNew");
                    missing.Add("SectionNameInvalid");
                    missing.Add("SectionNameDuplicate");
                    missing.Add("TranslationNameInvalid");
                    missing.Add("TranslationNameDuplicate");
                    missing.Add("RemoveItem");
                    missing.Add("SaveCodeStandard");
                    missing.Add("SaveCodeNotifyBasic");
                    missing.Add("SaveCodeCommunityToolkit");
                    missing.Add("ExitSaveRemind");
                    return 0;
                }
            }

            private void __ResetDefaultInternal()
            {
                this.DocumentExistsCreateNew = _documentExistsCreateNew_d;
                this.DocumentExistsLoadNew = _documentExistsLoadNew_d;
                this.SectionNameInvalid = _sectionNameInvalid_d;
                this.SectionNameDuplicate = _sectionNameDuplicate_d;
                this.TranslationNameInvalid = _translationNameInvalid_d;
                this.TranslationNameDuplicate = _translationNameDuplicate_d;
                this.RemoveItem = _removeItem_d;
                this.SaveCodeStandard = _saveCodeStandard_d;
                this.SaveCodeNotifyBasic = _saveCodeNotifyBasic_d;
                this.SaveCodeCommunityToolkit = _saveCodeCommunityToolkit_d;
                this.ExitSaveRemind = _exitSaveRemind_d;
            }

            private void __SaveInternal(bool defaultField, XConfigManager manager)
            {
                XSection section = manager.Sections.Add("DialogMessages");
                if (defaultField)
                {
                    section.Properties.AddString("DocumentExistsCreateNew", _documentExistsCreateNew_d).Comment.SetValue(_documentExistsCreateNew_c, true);
                    section.Properties.AddString("DocumentExistsLoadNew", _documentExistsLoadNew_d).Comment.SetValue(_documentExistsLoadNew_c, true);
                    section.Properties.AddString("SectionNameInvalid", _sectionNameInvalid_d).Comment.SetValue(_sectionNameInvalid_c, true);
                    section.Properties.AddString("SectionNameDuplicate", _sectionNameDuplicate_d).Comment.SetValue(_sectionNameDuplicate_c, true);
                    section.Properties.AddString("TranslationNameInvalid", _translationNameInvalid_d).Comment.SetValue(_translationNameInvalid_c, true);
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
                    section.Properties.AddString("SectionNameInvalid", this.SectionNameInvalid).Comment.SetValue(_sectionNameInvalid_c, true);
                    section.Properties.AddString("SectionNameDuplicate", this.SectionNameDuplicate).Comment.SetValue(_sectionNameDuplicate_c, true);
                    section.Properties.AddString("TranslationNameInvalid", this.TranslationNameInvalid).Comment.SetValue(_translationNameInvalid_c, true);
                    section.Properties.AddString("TranslationNameDuplicate", this.TranslationNameDuplicate).Comment.SetValue(_translationNameDuplicate_c, true);
                    section.Properties.AddString("RemoveItem", this.RemoveItem).Comment.SetValue(_removeItem_c, true);
                    section.Properties.AddString("SaveCodeStandard", this.SaveCodeStandard).Comment.SetValue(_saveCodeStandard_c, true);
                    section.Properties.AddString("SaveCodeNotifyBasic", this.SaveCodeNotifyBasic).Comment.SetValue(_saveCodeNotifyBasic_c, true);
                    section.Properties.AddString("SaveCodeCommunityToolkit", this.SaveCodeCommunityToolkit).Comment.SetValue(_saveCodeCommunityToolkit_c, true);
                    section.Properties.AddString("ExitSaveRemind", this.ExitSaveRemind).Comment.SetValue(_exitSaveRemind_c, true);
                }
            }
        }

        #endregion DialogMessages

        #region ToastMessages

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

            internal static int __LoadInternal(LanguagePackage instance, XConfigManager manager, List<string> missing)
            {
                return instance.ToastMessages.__LoadInternal(manager, missing);
            }

            internal static void __ResetDefaultInternal(LanguagePackage instance)
            {
                instance.ToastMessages.__ResetDefaultInternal();
            }

            internal static void __SaveInternal(LanguagePackage instance, bool defaultField, XConfigManager manager)
            {
                instance.ToastMessages.__SaveInternal(defaultField, manager);
            }

            private static string __GetTranslationEntryInternal(XSection section, string name, string defaultValue, List<string> missing, ref int loaded)
            {
                if (section.Properties.TryGetStringValue(name, out string value))
                {
                    loaded++;
                    return value;
                }
                else
                {
                    missing.Add(name);
                    return defaultValue;
                }
            }

            private int __LoadInternal(XConfigManager manager, List<string> missing)
            {
                if (manager.Sections.TryGetValue("ToastMessages", out XSection section))
                {
                    int loaded = 0;
                    this.LanguageFileSaved = __GetTranslationEntryInternal(section, "LanguageFileSaved", _languageFileSaved_d, missing, ref loaded);
                    return loaded;
                }
                else
                {
                    missing.Add("LanguageFileSaved");
                    return 0;
                }
            }

            private void __ResetDefaultInternal()
            {
                this.LanguageFileSaved = _languageFileSaved_d;
            }

            private void __SaveInternal(bool defaultField, XConfigManager manager)
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

        #endregion ToastMessages
    }
}