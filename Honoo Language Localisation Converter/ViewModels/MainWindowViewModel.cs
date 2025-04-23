using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Honoo.Configuration;
using HonooLanguageLocalisationConverter.Models;
using HonooUI.WPF;
using HonooUI.WPF.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        #region Members

        private int _counter = 0;

        [ObservableProperty]
        private SectionEntry? _currentSection;

        [ObservableProperty]
        private bool _documentLoaded;

        [ObservableProperty]
        private string? _documentFileName;

        private bool _forceExit;

        [ObservableProperty]
        private bool _hasNewVersion;

        private bool _manualAdd;

        public ICommand AboutCommand { get; }
        public ICommand AddSectionCommand { get; }
        public ICommand AddTranslationCommand { get; }
        public ICommand CreateNewCommand { get; }
        public ICommand ExitCommand { get; } = new RelayCommand(() => { SystemCommands.CloseWindow(Application.Current.MainWindow); });
        public SectionEntry Information { get; } = new SectionEntry("Information");
        public ICommand ItemAddedCommand { get; }
        public ICommand NavigateToWebsiteCommand { get; } = new RelayCommand(() => { Process.Start(new ProcessStartInfo("https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter/") { UseShellExecute = true }); });
        public ICommand OpenCommand { get; }
        public ICommand RemoveExternalLanguageFileCommand { get; }
        public ICommand RemoveSectionCommand { get; }
        public ICommand RemoveTranslationCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveCSharpCodeAsCommand { get; }
        public ICommand SectionMoveCommand { get; }
        public ObservableCollection<SectionEntry> Sections { get; } = [];
        public ICommand SortTranslationCommand { get; }
        public ICommand TestLoadLanguageCommand { get; }
        public ICommand TestSaveLanguageCommand { get; }
        public ICommand WindowClosingCommand { get; }

        #endregion Members

        #region Construction

        public MainWindowViewModel()
        {
            this.WindowClosingCommand = new RelayCommand<CancelEventArgs>(WindowClosingCommandExecute);

            this.CreateNewCommand = new RelayCommand(CreateNewCommandExecute);
            this.OpenCommand = new RelayCommand(OpenCommandExecute);
            this.SaveCommand = new RelayCommand(SaveCommandExecute, () => { return this.DocumentLoaded; });
            this.SaveAsCommand = new RelayCommand(SaveAsCommandExecute, () => { return this.DocumentLoaded; });
            this.SaveCSharpCodeAsCommand = new RelayCommand(SaveCSharpCodeAsCommandExecute, () => { return this.DocumentLoaded; });
            this.TestLoadLanguageCommand = new RelayCommand(TestLoadLanguageCommandExecute);
            this.TestSaveLanguageCommand = new RelayCommand(TestSaveLanguageCommandExecute);
            this.RemoveExternalLanguageFileCommand = new RelayCommand(RemoveExternalLanguageFileCommandExecute);
            this.AboutCommand = new RelayCommand(AboutCommandExecute);
            this.ItemAddedCommand = new RelayCommand<HonooUI.WPF.Controls.TextBox>(ItemAddedCommandExecute);
            this.AddSectionCommand = new RelayCommand(AddSectionCommandExecute);
            this.SectionMoveCommand = new RelayCommand<DragEventArgs>(SectionMoveCommandExecute);
            this.RemoveSectionCommand = new RelayCommand<object>(RemoveSectionCommandExecute);
            this.AddTranslationCommand = new RelayCommand(AddTranslationCommandExecute);
            this.SortTranslationCommand = new RelayCommand(SortTranslationCommandExecute);
            this.RemoveTranslationCommand = new RelayCommand<object>(RemoveTranslationCommandExecute);

            this.Sections.CollectionChanged += OnSectionEntriesChanged;
            this.PropertyChanged += OnPropertyChanged;

            if (DateTime.Now - Settings.Instance.LastUpdate > TimeSpan.FromDays(30))
            {
                var client = new HttpClient();
                client.GetStringAsync("https://raw.githubusercontent.com/LokiHonoo/Honoo-Language-Localisation-Converter/refs/heads/master/Honoo%20Language%20Localisation%20Converter/Honoo%20Language%20Localisation%20Converter.csproj").ContinueWith((t) =>
                {
                    try
                    {
                        if (t.Result != null)
                        {
                            XElement csproj = XElement.Parse(t.Result);
                            var version = csproj.Element("PropertyGroup")!.Element("Version")!.Value;
                            if (version != General.Instance.Version)
                            {
                                this.HasNewVersion = true;
                            }
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        client.Dispose();
                        Settings.Instance.LastUpdate = DateTime.Now;
                    }
                });
            }
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.DocumentLoaded):
                    ((IRelayCommand)this.SaveCommand).NotifyCanExecuteChanged();
                    ((IRelayCommand)this.SaveAsCommand).NotifyCanExecuteChanged();
                    ((IRelayCommand)this.SaveCSharpCodeAsCommand).NotifyCanExecuteChanged();
                    break;

                default: break;
            }
        }

        private void OnSectionEntriesChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            General.Instance.DocumentModified = true;
        }

        #endregion Construction

        private static bool SaveSection(SectionEntry sctionEntry, XConfigManager manager, out string message)
        {
            string sectionName = sctionEntry.Name.Trim();
            if (sectionName.Length == 0 || !VerifyField(sectionName))
            {
                message = string.Format(LanguagePackage.Instance.DialogMessages.SectionNameInvalid, sectionName);
                return false;
            }
            else if (manager.Sections.ContainsName(sectionName))
            {
                message = string.Format(LanguagePackage.Instance.DialogMessages.SectionNameDuplicate, sectionName);
                return false;
            }
            var section = manager.Sections.Add(sectionName);
            if (sctionEntry.TranslationEntries.Count > 0)
            {
                foreach (var item in sctionEntry.TranslationEntries)
                {
                    string translationName = item.Name.Trim();
                    if (translationName.Length == 0 || !VerifyField(translationName))
                    {
                        message = string.Format(LanguagePackage.Instance.DialogMessages.TranslationNameInvalid, sectionName, translationName);
                        return false;
                    }
                    else if (section.Properties.ContainsKey(translationName))
                    {
                        message = string.Format(LanguagePackage.Instance.DialogMessages.TranslationNameDuplicate, sectionName, translationName);
                        return false;
                    }
                    section.Properties.Add(item.Name, new XString(item.Value)).Comment.SetValue(item.Comment, true);
                }
            }
            message = string.Empty;
            return true;
        }

        private static bool VerifyField(string field)
        {
            foreach (char chr in field)
            {
                if (char.IsAscii(chr) && !char.IsAsciiLetterOrDigit(chr) && chr != '_')
                {
                    return false;
                }
            }
            return true;
        }

        private void AboutCommandExecute()
        {
            DialogManager.Default.Show($"Honoo Language Localisation Converter\r\n\r\nVersion {General.Instance.Version}\r\n\r\nCopyright (C) Loki Honoo 2025. All rights reserved.",
                string.Empty,
                DialogButtons.OK,
                DialogDefaultButton.OK,
                DialogCloseButton.Ordinary,
                DialogImage.Information);
        }

        private void AddSectionCommandExecute()
        {
            _manualAdd = true;
            this.Sections.Insert(0, new SectionEntry("Section" + _counter++));
            this.CurrentSection = this.Sections[0];
        }

        private void AddTranslationCommandExecute()
        {
            if (this.CurrentSection != null)
            {
                _manualAdd = true;
                this.CurrentSection.TranslationEntries.Insert(0, new TranslationEntry("Translation" + _counter++, string.Empty, "The Name string using by code member name. Can't be empty and special characters."));
            }
        }

        private void CreateDocument()
        {
            this.Information.TranslationEntries.Clear();
            this.Sections.Clear();
            this.CurrentSection = null;

            this.Information.TranslationEntries.Add(new TranslationEntry("AppName", "Application name", "Application name."));
            this.Information.TranslationEntries.Add(new TranslationEntry("AppVer", "1.x", "Application version."));
            this.Information.TranslationEntries.Add(new TranslationEntry("LangName", "en-US", "Language name as \"en-US\"."));
            this.Information.TranslationEntries.Add(new TranslationEntry("LangVer", "00", "Language file revision version."));
            this.Information.TranslationEntries.Add(new TranslationEntry("Author", "Honoo Language Localisation Converter", "Author name."));
            this.Information.TranslationEntries.Add(new TranslationEntry("Email", string.Empty, "Author email."));
            this.Information.TranslationEntries.Add(new TranslationEntry("Website", "https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter", "Author related url."));
            this.Information.TranslationEntries.Add(new TranslationEntry("Remarks", string.Empty, "Remarks."));

            var section1 = new SectionEntry("Menu");
            section1.TranslationEntries.Add(new TranslationEntry("File", "_File", "Menu button, Top item."));
            section1.TranslationEntries.Add(new TranslationEntry("CreateNew", "_New...", "Menu button, create new document."));
            section1.TranslationEntries.Add(new TranslationEntry("Open", "_Open...", "Menu button, Show dialog for select open file."));
            section1.TranslationEntries.Add(new TranslationEntry("Save", "_Save", "Menu button, Save to lang file."));
            section1.TranslationEntries.Add(new TranslationEntry("SaveAs", "Save _As...", "Menu button, Show dialog for select save file."));
            section1.TranslationEntries.Add(new TranslationEntry("SaveCSharpCodeAs", "Save C# code As...", "Menu button, Show dialog for select save file."));
            section1.TranslationEntries.Add(new TranslationEntry("Exit", "E_xit", "Menu button, Exit app."));
            section1.TranslationEntries.Add(new TranslationEntry("Options", "_Options", "Menu button, Top item."));
            section1.TranslationEntries.Add(new TranslationEntry("Help", "_Help", "Menu button, Top item."));
            section1.TranslationEntries.Add(new TranslationEntry("Website", "_Website", "Menu button, Navigate to project website."));
            section1.TranslationEntries.Add(new TranslationEntry("About", "_About...", "Menu button, Show dialog for app information."));
            this.Sections.Add(section1);
            var section2 = new SectionEntry("Main");
            section2.TranslationEntries.Add(new TranslationEntry("Information", "Information", "Tab title text."));
            section2.TranslationEntries.Add(new TranslationEntry("Sections", "Sections", "Tab title text."));
            section2.TranslationEntries.Add(new TranslationEntry("Sort", "Sort", "Button text."));
            section2.TranslationEntries.Add(new TranslationEntry("SectionEntries", "Section entries", "Title text."));
            section2.TranslationEntries.Add(new TranslationEntry("TranslationEntries", "Translation entries", "Title text."));
            section2.TranslationEntries.Add(new TranslationEntry("HasNewVersion", "New version published", "StatusBar text."));
            this.Sections.Add(section2);
            var section3 = new SectionEntry("DialogMessages");
            section3.TranslationEntries.Add(new TranslationEntry("DocumentExistsCreateNew", "Document loaded already. Create new document?", "Dialog content."));
            section3.TranslationEntries.Add(new TranslationEntry("DocumentExistsLoadNew", "Document loaded already. Load new document?", "Dialog content."));
            section3.TranslationEntries.Add(new TranslationEntry("SectionNameInvalid", "Section \"{0}\" string can't be empty and special characters.", "Dialog content, Set field {0}=Section Name."));
            section3.TranslationEntries.Add(new TranslationEntry("SectionNameDuplicate", "Section \"{0}\" has duplicate name.", "Dialog content, Set field {0}=Section Name."));
            section3.TranslationEntries.Add(new TranslationEntry("TranslationNameInvalid", "Section \"{0}\"'s translation entry \"{1}\" string can't be empty and special characters.", "Dialog content, Set field {0}=Section Name, {1}=Translation Name."));
            section3.TranslationEntries.Add(new TranslationEntry("TranslationNameDuplicate", "Section \"{0}\"'s translation entry \"{1}\" has duplicate name.", "Dialog content, Set field {0}=Section Name, {1}=Translation Name."));
            section3.TranslationEntries.Add(new TranslationEntry("RemoveItem", "Remove \"{0}\" ?", "Dialog content set custom field {0}=Remove Name."));
            section3.TranslationEntries.Add(new TranslationEntry("SaveCodeStandard", "Standard class model, Changed-Notify NOT supported", "Dialog content selection text."));
            section3.TranslationEntries.Add(new TranslationEntry("SaveCodeNotifyBasic", "Changed-Notify interface implemented", "Dialog content selection text. INotifyPropertyChanging, INotifyPropertyChanged implemented."));
            section3.TranslationEntries.Add(new TranslationEntry("SaveCodeCommunityToolkit", "Changed-Notify implemented by CommunityToolkit.Mvvm", "Dialog content selection text. CommunityToolkit.Mvvm code style."));
            section3.TranslationEntries.Add(new TranslationEntry("ExitSaveRemind", "The document modified but has not been saved.\r\n\r\nExit application without save?", "Dialog content."));
            this.Sections.Add(section3);
            var section4 = new SectionEntry("ToastMessages");
            section4.TranslationEntries.Add(new TranslationEntry("LanguageFileSaved", "Language file saved.", "Toast content."));
            this.Sections.Add(section4);
            this.CurrentSection = section1;
            this.DocumentFileName = null;
            this.DocumentLoaded = true;
            General.Instance.DocumentModified = false;
        }

        private void CreateNewCommandExecute()
        {
            if (this.DocumentLoaded)
            {
                DialogManager.Default.Show(LanguagePackage.Instance.DialogMessages.DocumentExistsCreateNew,
                    string.Empty,
                    DialogButtons.OKCancel,
                    DialogDefaultButton.OK,
                    DialogCloseButton.Ordinary,
                    DialogImage.Information,
                    DialogSize.Default,
                    false,
                    DialogLocalization.Default,
                    null,
                    (e) =>
                    {
                        if (e.DialogResult == DialogResult.OK)
                        {
                            CreateDocument();
                        }
                    },
                    null);
            }
            else
            {
                CreateDocument();
            }
        }

        private void ItemAddedCommandExecute(HonooUI.WPF.Controls.TextBox? textBox)
        {
            if (textBox != null && _manualAdd)
            {
                textBox.Focus();
                textBox.SelectAll();
                _manualAdd = false;
            }
        }

        private void OpenCommandExecute()
        {
            var dig = new OpenFileDialog
            {
                Filter = "Language files (*.lang;lng;*.xml)|*.lang;*.lng;*.xml|All files (*.*)|*.*"
            };
            if (dig.ShowDialog() == true)
            {
                if (this.DocumentLoaded)
                {
                    DialogManager.Default.Show(LanguagePackage.Instance.DialogMessages.DocumentExistsLoadNew,
                        string.Empty,
                        DialogButtons.OKCancel,
                        DialogDefaultButton.OK,
                        DialogCloseButton.Ordinary,
                        DialogImage.Information,
                        DialogSize.Default,
                        false,
                        DialogLocalization.Default,
                        null,
                        (e) =>
                        {
                            if (e.DialogResult == DialogResult.OK)
                            {
                                OpenDocument(dig.FileName);
                            }
                        },
                        null);
                }
                else
                {
                    OpenDocument(dig.FileName);
                }
            }
        }

        private void OpenDocument(string fileName)
        {
            try
            {
                this.Information.TranslationEntries.Clear();
                this.Sections.Clear();
                this.CurrentSection = null;
                using (var manager = new XConfigManager(fileName))
                {
                    if (manager.Sections.Count > 0)
                    {
                        foreach (var section in manager.Sections)
                        {
                            if (section.Name == "Information")
                            {
                                foreach (var translation in section.Properties)
                                {
                                    XString value = (XString)translation.Value;
                                    var translationEntry = new TranslationEntry(translation.Key, value.GetStringValue(), value.Comment.HasValue ? value.Comment.GetValue() : string.Empty);
                                    this.Information.TranslationEntries.Add(translationEntry);
                                }
                            }
                            else
                            {
                                var sectionEntry = new SectionEntry(section.Name);
                                foreach (var translation in section.Properties)
                                {
                                    XString value = (XString)translation.Value;
                                    var translationEntry = new TranslationEntry(translation.Key, value.GetStringValue(), value.Comment.HasValue ? value.Comment.GetValue() : string.Empty);
                                    sectionEntry.TranslationEntries.Add(translationEntry);
                                }
                                this.Sections.Add(sectionEntry);
                            }
                        }
                        this.CurrentSection = this.Sections[0];
                    }
                }
                Dictionary<string, TranslationEntry> entries = new()
                    {
                        { "AppName", new TranslationEntry("AppName", "Application name", "Application name.") },
                        { "AppVer", new TranslationEntry("AppVer", "1.x", "Application version.") },
                        { "LangName", new TranslationEntry("LangName", "en-US", "Language name as \"en-US\".") },
                        { "LangVer", new TranslationEntry("LangVer", "00", "Language file revision version.") },
                        { "Author", new TranslationEntry("Author", "Honoo Language Localisation Converter", "Author name.") },
                        { "Email", new TranslationEntry("Email", string.Empty, "Author email.") },
                        { "Website", new TranslationEntry("Website", "https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter", "Author related url.") },
                        { "Remarks", new TranslationEntry("Remarks", string.Empty, "Remarks.") }
                    };
                foreach (var translationEntry in this.Information.TranslationEntries)
                {
                    entries.Remove(translationEntry.Name);
                }
                if (entries.Count > 0)
                {
                    foreach (var entry in entries)
                    {
                        this.Information.TranslationEntries.Add(entry.Value);
                    }
                }
                this.DocumentFileName = fileName;
                this.DocumentLoaded = true;
                General.Instance.DocumentModified = false;
            }
            catch (Exception ex)
            {
                this.Information.TranslationEntries.Clear();
                this.Sections.Clear();
                this.CurrentSection = null;
                this.DocumentFileName = string.Empty;
                this.DocumentLoaded = false;
                General.Instance.DocumentModified = false;
                DialogManager.Default.Show(ex.Message,
                    string.Empty,
                    DialogButtons.OK,
                    DialogDefaultButton.OK,
                    DialogCloseButton.Ordinary,
                    DialogImage.Error);
            }
        }

        private void RemoveExternalLanguageFileCommandExecute()
        {
            LanguagePackage.Instance.ResetDefault();
            Settings.Instance.LanguageFile = string.Empty;
        }

        private void RemoveSectionCommandExecute(object? obj)
        {
            if (obj is SectionEntry section && this.Sections != null)
            {
                DialogManager.Default.Show(string.Format(LanguagePackage.Instance.DialogMessages.RemoveItem, section.Name),
                    string.Empty,
                    DialogButtons.OKCancel,
                    DialogDefaultButton.OK,
                    DialogCloseButton.Ordinary,
                    DialogImage.Information,
                    DialogSize.Default,
                    false,
                    DialogLocalization.Default,
                    null,
                    (e) =>
                    {
                        if (e.DialogResult == DialogResult.OK)
                        {
                            this.Sections.Remove(section);
                            if (section == this.CurrentSection)
                            {
                                this.CurrentSection = null;
                            }
                        }
                    },
                    null);
            }
        }

        private void RemoveTranslationCommandExecute(object? obj)
        {
            if (obj is TranslationEntry translation && this.CurrentSection != null)
            {
                DialogManager.Default.Show(string.Format(LanguagePackage.Instance.DialogMessages.RemoveItem, translation.Name),
                    string.Empty,
                    DialogButtons.OKCancel,
                    DialogDefaultButton.OK,
                    DialogCloseButton.Ordinary,
                    DialogImage.Information,
                    DialogSize.Default,
                    false,
                    DialogLocalization.Default,
                    null,
                    (e) =>
                    {
                        if (e.DialogResult == DialogResult.OK)
                        {
                            this.CurrentSection.TranslationEntries.Remove(translation);
                        }
                    },
                    null);
            }
        }

        private void SaveAsCommandExecute()
        {
            if (this.Sections != null)
            {
                string fileName = string.Empty;
                foreach (var translationEntry in this.Information.TranslationEntries)
                {
                    if (translationEntry.Name == "AppName")
                    {
                        fileName += translationEntry.Value;
                    }
                    else if (translationEntry.Name == "AppVer")
                    {
                        fileName += "_" + translationEntry.Value;
                    }
                    else if (translationEntry.Name == "LangName")
                    {
                        fileName += "_" + translationEntry.Value;
                    }
                }
                fileName = string.IsNullOrWhiteSpace(fileName) ? "Language.lang" : fileName + ".lang";
                var dig = new SaveFileDialog()
                {
                    Filter = "Language files (*.lang)|*.lang|Language files (*.lng)|*.lng|Language files (*.xml)|*.xml",
                    FileName = fileName,
                };
                if (dig.ShowDialog() == true)
                {
                    fileName = dig.FileName;
                    switch (dig.FilterIndex)
                    {
                        case 3:
                            if (Path.GetExtension(dig.FileName) != ".xml")
                            {
                                fileName = Path.ChangeExtension(dig.FileName, ".xml");
                            }
                            break;

                        case 2:
                            if (Path.GetExtension(dig.FileName) != ".lng")
                            {
                                fileName = Path.ChangeExtension(dig.FileName, ".lng");
                            }
                            break;

                        default:
                            if (Path.GetExtension(dig.FileName) != ".lang")
                            {
                                fileName = Path.ChangeExtension(dig.FileName, ".lang");
                            }
                            break;
                    }
                    SaveDocument(fileName);
                }
            }
        }

        private void SaveCommandExecute()
        {
            if (string.IsNullOrEmpty(this.DocumentFileName))
            {
                this.SaveAsCommand.Execute(null);
            }
            else
            {
                SaveDocument(this.DocumentFileName);
            }
        }

        private void SaveCSharpCodeAsCommandExecute()
        {
            if (this.Sections != null)
            {
                var stackPanel = new StackPanel();
                var radioButtonTray = new RadioButtonTray();
                radioButtonTray.Children.Add(new System.Windows.Controls.RadioButton() { HorizontalAlignment = HorizontalAlignment.Left, Content = LanguagePackage.Instance.DialogMessages.SaveCodeStandard });
                radioButtonTray.Children.Add(new System.Windows.Controls.RadioButton() { HorizontalAlignment = HorizontalAlignment.Left, Content = LanguagePackage.Instance.DialogMessages.SaveCodeNotifyBasic + " [.NET Framework 4.0+]", Margin = new Thickness(0, 10, 0, 0) });
                radioButtonTray.Children.Add(new System.Windows.Controls.RadioButton() { HorizontalAlignment = HorizontalAlignment.Left, Content = LanguagePackage.Instance.DialogMessages.SaveCodeNotifyBasic + " [.NET 6.0+]", Margin = new Thickness(0, 10, 0, 0), IsChecked = true });
                radioButtonTray.Children.Add(new System.Windows.Controls.RadioButton() { HorizontalAlignment = HorizontalAlignment.Left, Content = LanguagePackage.Instance.DialogMessages.SaveCodeCommunityToolkit, Margin = new Thickness(0, 10, 0, 0) });
                DialogManager.Default.Show(radioButtonTray,
                    string.Empty,
                    DialogButtons.OKCancel,
                    DialogDefaultButton.OK,
                    DialogCloseButton.Ordinary,
                    DialogImage.Information,
                    DialogSize.Default,
                    false,
                    DialogLocalization.Default,
                    null,
                    (e) =>
                    {
                        if (e.DialogResult == DialogResult.OK)
                        {
                            string fileName = string.Empty;
                            foreach (var translationEntry in this.Information.TranslationEntries)
                            {
                                if (translationEntry.Name == "AppName")
                                {
                                    fileName += translationEntry.Value;
                                }
                                else if (translationEntry.Name == "AppVer")
                                {
                                    fileName += "_" + translationEntry.Value;
                                }
                                else if (translationEntry.Name == "LangName")
                                {
                                    fileName += "_" + translationEntry.Value;
                                }
                            }
                            fileName = string.IsNullOrWhiteSpace(fileName) ? "Language.cs" : fileName + ".cs";
                            var dig = new SaveFileDialog()
                            {
                                Filter = "C# code files (*.cs)|*.cs",
                                FileName = fileName,
                                DefaultExt = ".cs",
                                AddExtension = true
                            };
                            if (dig.ShowDialog() == true)
                            {
                                try
                                {
                                    string code = radioButtonTray.SelectIndex switch
                                    {
                                        3 => CShapCode.CreateCode(3, this.Information, this.Sections),
                                        2 => CShapCode.CreateCode(2, this.Information, this.Sections),
                                        1 => CShapCode.CreateCode(1, this.Information, this.Sections),
                                        _ => CShapCode.CreateCode(0, this.Information, this.Sections),
                                    };
                                    File.WriteAllText(dig.FileName, code, new UTF8Encoding(false));
                                }
                                catch (Exception ex)
                                {
                                    DialogManager.Default.Show(ex.Message,
                                        string.Empty,
                                        DialogButtons.OK,
                                        DialogDefaultButton.OK,
                                        DialogCloseButton.Ordinary,
                                        DialogImage.Error);
                                }
                            }
                        }
                    },
                    null);
            }
        }

        private void SaveDocument(string fileName)
        {
            try
            {
                using var manager = new XConfigManager();
                manager.Default.Properties.AddString("Creator", General.Instance.Creator);
                manager.Default.Properties.AddString("Website", General.Instance.Website);
                manager.Default.Properties.AddString("CreatedTime", DateTime.Now.ToString("R"));
                if (!SaveSection(this.Information, manager, out string message))
                {
                    DialogManager.Default.Show(message,
                                               string.Empty,
                                               DialogButtons.OK,
                                               DialogDefaultButton.OK,
                                               DialogCloseButton.Ordinary,
                                               DialogImage.Error);
                    return;
                }
                if (this.Sections != null)
                {
                    if (this.Sections != null && this.Sections.Count > 0)
                    {
                        foreach (var sctionEntry in this.Sections)
                        {
                            if (!SaveSection(sctionEntry, manager, out message))
                            {
                                DialogManager.Default.Show(message,
                                                           string.Empty,
                                                           DialogButtons.OK,
                                                           DialogDefaultButton.OK,
                                                           DialogCloseButton.Ordinary,
                                                           DialogImage.Error);
                                return;
                            }
                        }
                    }
                }
                manager.Save(fileName);
                this.DocumentFileName = fileName;
                General.Instance.DocumentModified = false;
                ToastManager.Default.Show(LanguagePackage.Instance.ToastMessages.LanguageFileSaved, 2000, ToastOptions.Information, null);
            }
            catch (Exception ex)
            {
                DialogManager.Default.Show(ex.Message, string.Empty, DialogButtons.OK, DialogDefaultButton.OK, DialogCloseButton.Ordinary, DialogImage.Error);
            }
        }

        private void SectionMoveCommandExecute(DragEventArgs? e)
        {
            if (e != null && this.Sections != null)
            {
                var dropped = (DraggableHaft)e.Data.GetData(typeof(DraggableHaft));
                var target = (DraggableHaft)e.Source;
                SectionEntry droppedDataContext = (SectionEntry)dropped.DataContext;
                SectionEntry targetDataContext = (SectionEntry)target.DataContext;
                int sIndex = this.Sections.IndexOf(droppedDataContext);
                int tIndex = this.Sections.IndexOf(targetDataContext);
                if (sIndex != tIndex)
                {
                    this.Sections.RemoveAt(sIndex);
                    this.Sections.Insert(tIndex, droppedDataContext);
                }
                this.CurrentSection = this.Sections[tIndex];
            }
        }

        private void SortTranslationCommandExecute()
        {
            if (this.CurrentSection != null && this.CurrentSection.TranslationEntries.Count > 1)
            {
                List<TranslationEntry> list = [.. this.CurrentSection.TranslationEntries.OrderBy(x => x.Name)];
                for (int i = 0; i < list.Count; i++)
                {
                    this.CurrentSection.TranslationEntries.Move(this.CurrentSection.TranslationEntries.IndexOf(list[i]), i);
                }
            }
        }

        private void TestLoadLanguageCommandExecute()
        {
            var dig = new OpenFileDialog
            {
                Filter = "Language files (*.lang;lng;*.xml)|*.lang;*.lng;*.xml|All files (*.*)|*.*"
            };
            if (dig.ShowDialog() == true)
            {
                try
                {
                    LanguagePackage.GetInformation(dig.FileName);
                    int loaded = LanguagePackage.Instance.Load(dig.FileName);
                    Settings.Instance.LanguageFile = dig.FileName;
                    DialogManager.Default.Show($"Load translation entries {loaded}. Total {LanguagePackage.Instance.Count}.",
                                               string.Empty,
                                               DialogButtons.OK,
                                               DialogDefaultButton.OK,
                                               DialogCloseButton.Ordinary,
                                               DialogImage.Information);
                }
                catch (Exception ex)
                {
                    LanguagePackage.Instance.ResetDefault();
                    DialogManager.Default.Show(ex.Message,
                        string.Empty,
                        DialogButtons.OK,
                        DialogDefaultButton.OK,
                        DialogCloseButton.Ordinary,
                        DialogImage.Error);
                }
            }
        }

        private void TestSaveLanguageCommandExecute()
        {
            var dig = new OpenFolderDialog()
            {
                Multiselect = false,
            };
            if (dig.ShowDialog() == true)
            {
                string fileName1 = Path.Combine(dig.FolderName, "lang_test_save_filename.xml");
                string fileName2 = Path.Combine(dig.FolderName, "lang_test_save_stream.xml");
                string fileName3 = Path.Combine(dig.FolderName, "lang_test_savedefault_filename.xml");
                string fileName4 = Path.Combine(dig.FolderName, "lang_test_savedefault_stream.xml");
                try
                {
                    LanguagePackage.Instance.Save(false, fileName1);
                    LanguagePackage.Instance.Save(false, fileName2);
                    using (var stream = new FileStream(fileName3, FileMode.Create, FileAccess.Write))
                    {
                        LanguagePackage.Instance.Save(true, stream);
                    }
                    using (var stream = new FileStream(fileName4, FileMode.Create, FileAccess.Write))
                    {
                        LanguagePackage.Instance.Save(true, stream);
                    }
                }
                catch (Exception ex)
                {
                    DialogManager.Default.Show(ex.Message,
                        string.Empty,
                        DialogButtons.OK,
                        DialogDefaultButton.OK,
                        DialogCloseButton.Ordinary,
                        DialogImage.Error);
                }
            }
        }

        private void WindowClosingCommandExecute(CancelEventArgs? e)
        {
            if (e != null && General.Instance.DocumentModified && !_forceExit)
            {
                e.Cancel = true;
                DialogManager.Default.Show(LanguagePackage.Instance.DialogMessages.ExitSaveRemind,
                    string.Empty,
                    DialogButtons.YesNo,
                    DialogDefaultButton.None,
                    DialogCloseButton.None,
                    DialogImage.Exclamation,
                    DialogSize.Default,
                    false,
                    DialogLocalization.Default,
                    null,
                    (de) =>
                    {
                        if (de.DialogResult == DialogResult.Yes)
                        {
                            _forceExit = true;
                            SystemCommands.CloseWindow(Application.Current.MainWindow);
                        }
                    },
                    null);
            }
        }
    }
}