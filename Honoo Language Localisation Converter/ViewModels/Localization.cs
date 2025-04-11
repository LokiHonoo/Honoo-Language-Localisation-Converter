using CommunityToolkit.Mvvm.ComponentModel;
using Honoo.Configuration;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    /// <summary>
    /// Language localisation class.
    /// <br />Install nuget package:
    /// <br /><see href="https://www.nuget.org/packages/CommunityToolkit.Mvvm"/>.
    /// <br /><see href="https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager"/>.
    /// </summary>
    public sealed partial class Localization : ObservableObject
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
        public Localization()
        {
        }

        /// <summary>
        /// Load language file.
        /// </summary>
        /// <param name="fileName">Language file name.</param>
        public void Load(string fileName)
        {
            using (var manager = new XConfigManager(fileName, true))
            {
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
            this.Menu.ResetDefault();
            this.Main.ResetDefault();
            this.Messages.ResetDefault();
        }

        /// <summary>
        /// Save language file.
        /// </summary>
        /// <param name="fileName">Language file name.</param>
        public void Save(string fileName)
        {
            using (var manager = new XConfigManager())
            {
                this.Menu.Save(manager);
                this.Main.Save(manager);
                this.Messages.Save(manager);
                manager.Save(fileName);
            }
        }

        /// <summary>
        /// Informartion section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed partial class __Informartion : ObservableObject
        {
            #region Default

            private const string _appName_d = "Application name";
            private const string _appVer_d = "1.0.0";
            private const string _author_d = "HLLC";
            private const string _langName_d = "en-us";
            private const string _langVer_d = "1.0.0";
            private const string _remarks_d = "";
            private const string _url_d = "https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter";

            #endregion Default

            #region Members

            /// <summary>
            /// Application name.
            /// </summary>
            [ObservableProperty]
            private string _appName = _appName_d;

            /// <summary>
            /// Application version.
            /// </summary>
            [ObservableProperty]
            private string _appVer = _appVer_d;

            /// <summary>
            /// Author name.
            /// </summary>
            [ObservableProperty]
            private string _author = _author_d;

            /// <summary>
            /// Language name as "en-US".
            /// </summary>
            [ObservableProperty]
            private string _langName = _langName_d;

            /// <summary>
            /// Language file version.
            /// </summary>
            [ObservableProperty]
            private string _langVer = _langVer_d;

            /// <summary>
            /// Remarks.
            /// </summary>
            [ObservableProperty]
            private string _remarks = _remarks_d;

            /// <summary>
            /// Author / file related url.
            /// </summary>
            [ObservableProperty]
            private string _url = _url_d;

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

            internal void Save(XConfigManager manager)
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

        /// <summary>
        /// Menu section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed partial class __Menu : ObservableObject
        {
            #region Default

            private const string _file_d = "_File";
            private const string _createTemplate_d = "_Create template...";
            private const string _openTemplate_d = "_Open template...";
            private const string _saveAs_d = "Save _As...";
            private const string _saveCsharpCodeAs_d = "Save C# code As...";
            private const string _exit_d = "E_xit";
            private const string _options_d = "_Options";
            private const string _help_d = "_Help";
            private const string _about_d = "_About";

            #endregion Default

            #region Members

            /// <summary>
            /// Menu button, Top item.
            /// </summary>
            [ObservableProperty]
            private string _file = _file_d;

            /// <summary>
            /// Menu button, create new template.
            /// </summary>
            [ObservableProperty]
            private string _createTemplate = _createTemplate_d;

            /// <summary>
            /// Menu button, Show dialog for select open file.
            /// </summary>
            [ObservableProperty]
            private string _openTemplate = _openTemplate_d;

            /// <summary>
            /// Menu button, Show dialog for select save file.
            /// </summary>
            [ObservableProperty]
            private string _saveAs = _saveAs_d;

            /// <summary>
            /// Menu button, Show dialog for select save file.
            /// </summary>
            [ObservableProperty]
            private string _saveCsharpCodeAs = _saveCsharpCodeAs_d;

            /// <summary>
            /// 
            /// </summary>
            [ObservableProperty]
            private string _exit = _exit_d;

            /// <summary>
            /// Menu button, Top item.
            /// </summary>
            [ObservableProperty]
            private string _options = _options_d;

            /// <summary>
            /// Menu button, Top item.
            /// </summary>
            [ObservableProperty]
            private string _help = _help_d;

            /// <summary>
            /// Menu button, Show dialog for information.
            /// </summary>
            [ObservableProperty]
            private string _about = _about_d;

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
                    this.SaveCsharpCodeAs = section.Properties.GetStringValue("SaveCsharpCodeAs", _saveCsharpCodeAs_d);
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
                this.SaveCsharpCodeAs = _saveCsharpCodeAs_d;
                this.Exit = _exit_d;
                this.Options = _options_d;
                this.Help = _help_d;
                this.About = _about_d;
            }

            internal void Save(XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Menu");
                section.Properties.AddString("File", this.File);
                section.Properties.AddString("CreateTemplate", this.CreateTemplate);
                section.Properties.AddString("OpenTemplate", this.OpenTemplate);
                section.Properties.AddString("SaveAs", this.SaveAs);
                section.Properties.AddString("SaveCsharpCodeAs", this.SaveCsharpCodeAs);
                section.Properties.AddString("Exit", this.Exit);
                section.Properties.AddString("Options", this.Options);
                section.Properties.AddString("Help", this.Help);
                section.Properties.AddString("About", this.About);
            }
        }

        /// <summary>
        /// Main section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed partial class __Main : ObservableObject
        {
            #region Default

            private const string _description_d = "Description";
            private const string _sections_d = "Sections";
            private const string _sort_d = "Sort";
            private const string _sectionEntries_d = "Section entries";
            private const string _languageEntries_d = "Language entries";

            #endregion Default

            #region Members

            /// <summary>
            /// Tab name.
            /// </summary>
            [ObservableProperty]
            private string _description = _description_d;

            /// <summary>
            /// Tab name.
            /// </summary>
            [ObservableProperty]
            private string _sections = _sections_d;

            /// <summary>
            /// 
            /// </summary>
            [ObservableProperty]
            private string _sort = _sort_d;

            /// <summary>
            /// 
            /// </summary>
            [ObservableProperty]
            private string _sectionEntries = _sectionEntries_d;

            /// <summary>
            /// 
            /// </summary>
            [ObservableProperty]
            private string _languageEntries = _languageEntries_d;

            #endregion Members

            internal __Main()
            {
            }

            internal void Load(XConfigManager manager)
            {
                if (manager.Sections.TryGetValue("Main", out XSection section))
                {
                    this.Description = section.Properties.GetStringValue("Description", _description_d);
                    this.Sections = section.Properties.GetStringValue("Sections", _sections_d);
                    this.Sort = section.Properties.GetStringValue("Sort", _sort_d);
                    this.SectionEntries = section.Properties.GetStringValue("SectionEntries", _sectionEntries_d);
                    this.LanguageEntries = section.Properties.GetStringValue("LanguageEntries", _languageEntries_d);
                }
            }

            internal void ResetDefault()
            {
                this.Description = _description_d;
                this.Sections = _sections_d;
                this.Sort = _sort_d;
                this.SectionEntries = _sectionEntries_d;
                this.LanguageEntries = _languageEntries_d;
            }

            internal void Save(XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Main");
                section.Properties.AddString("Description", this.Description);
                section.Properties.AddString("Sections", this.Sections);
                section.Properties.AddString("Sort", this.Sort);
                section.Properties.AddString("SectionEntries", this.SectionEntries);
                section.Properties.AddString("LanguageEntries", this.LanguageEntries);
            }
        }

        /// <summary>
        /// Messages section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public sealed partial class __Messages : ObservableObject
        {
            #region Default

            private const string _documentExistsCreateNew_d = "Document loaded already. Create new document?";
            private const string _documentExistsLoadNew_d = "Document loaded already. Load new document?";
            private const string _createSctionTip_d = "Input Section name.\r\nThe string using by code member name.\r\nSpaces and special characters cannot be used.";
            private const string _createLanguageEntryTip_d = "Input language entry name.\r\nThe string using by code member name.\r\nSpaces and special characters cannot be used.";
            private const string _nameExists_d = "Name \"{0}\" exists.";
            private const string _keyExists_d = "Key \"{0}\" exists.";
            private const string _removeItem_d = "Remove \"{0}\" ?";

            #endregion Default

            #region Members

            /// <summary>
            /// Dialog content.
            /// </summary>
            [ObservableProperty]
            private string _documentExistsCreateNew = _documentExistsCreateNew_d;

            /// <summary>
            /// Dialog content.
            /// </summary>
            [ObservableProperty]
            private string _documentExistsLoadNew = _documentExistsLoadNew_d;

            /// <summary>
            /// 
            /// </summary>
            [ObservableProperty]
            private string _createSctionTip = _createSctionTip_d;

            /// <summary>
            /// 
            /// </summary>
            [ObservableProperty]
            private string _createLanguageEntryTip = _createLanguageEntryTip_d;

            /// <summary>
            /// Dialog content set custom field {0}.
            /// </summary>
            [ObservableProperty]
            private string _nameExists = _nameExists_d;

            /// <summary>
            /// Dialog content set custom field {0}.
            /// </summary>
            [ObservableProperty]
            private string _keyExists = _keyExists_d;

            /// <summary>
            /// Dialog content set custom field {0}.
            /// </summary>
            [ObservableProperty]
            private string _removeItem = _removeItem_d;

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
            }

            internal void Save(XConfigManager manager)
            {
                XSection section = manager.Sections.Add("Messages");
                section.Properties.AddString("DocumentExistsCreateNew", this.DocumentExistsCreateNew);
                section.Properties.AddString("DocumentExistsLoadNew", this.DocumentExistsLoadNew);
                section.Properties.AddString("CreateSctionTip", this.CreateSctionTip);
                section.Properties.AddString("CreateLanguageEntryTip", this.CreateLanguageEntryTip);
                section.Properties.AddString("NameExists", this.NameExists);
                section.Properties.AddString("KeyExists", this.KeyExists);
                section.Properties.AddString("RemoveItem", this.RemoveItem);
            }
        }
    }
}
