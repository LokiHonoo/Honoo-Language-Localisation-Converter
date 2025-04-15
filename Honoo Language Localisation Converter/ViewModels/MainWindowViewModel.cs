using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Honoo.Configuration;
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
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        private string? _fileName;

        private bool _forceExit;

        [ObservableProperty]
        private bool _hasNewVersion;

        private bool _manualAdd;

        [ObservableProperty]
        private string _version = Assembly.GetExecutingAssembly().GetName().Version!.ToString(3);

        public ICommand AboutCommand { get; }
        public ICommand AddSectionCommand { get; }
        public ICommand AddTranslationCommand { get; }
        public ICommand CreateNewCommand { get; }
        public ICommand ExitCommand { get; } = new RelayCommand(() => { SystemCommands.CloseWindow(Application.Current.MainWindow); });
        public InformationEntry Informartion { get; } = new InformationEntry();
        public ICommand ItemAddedCommand { get; }
        public ICommand LoadLanguageFileCommand { get; }
        public ICommand NavigateToWebsiteCommand { get; } = new RelayCommand(() => { Process.Start(new ProcessStartInfo("https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter/") { UseShellExecute = true }); });
        public ICommand OpenCommand { get; }
        public ICommand RemoveSectionCommand { get; }
        public ICommand RemoveTranslationCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveCSharpCodeAsCommand { get; }
        public ICommand SectionMoveCommand { get; }
        public ObservableCollection<SectionEntry> Sections { get; } = [];
        public ICommand SortTranslationCommand { get; }
        public ICommand TestExportCommand { get; }
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
            this.LoadLanguageFileCommand = new RelayCommand(LoadLanguageFileCommandExecute);
            this.TestExportCommand = new RelayCommand(TestExportCommandExecute);
            this.AboutCommand = new RelayCommand(AboutCommandExecute);
            this.ItemAddedCommand = new RelayCommand<HonooUI.WPF.Controls.TextBox>(ItemAddedCommandExecute);
            this.AddSectionCommand = new RelayCommand(AddSectionCommandExecute);
            this.SectionMoveCommand = new RelayCommand<DragEventArgs>(SectionMoveCommandExecute);
            this.RemoveSectionCommand = new RelayCommand<object>(RemoveSectionCommandExecute);
            this.AddTranslationCommand = new RelayCommand(AddTranslationCommandExecute);
            this.SortTranslationCommand = new RelayCommand(SortTranslationCommandExecute);
            this.RemoveTranslationCommand = new RelayCommand<object>(RemoveTranslationCommandExecute);

            this.Sections.CollectionChanged += SectionEntriesChanged;
            this.PropertyChanged += OnPropertyChanged;
            if (DateTime.Now - Settings.Instance.LastUpdate > TimeSpan.FromDays(7))
            {
                var client = new HttpClient();
                client.GetStringAsync("https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter/blob/master/version.published").ContinueWith((t) =>
                {
                    try
                    {
                        if (t.Result != null)
                        {
                            var version = t.Result;
                            if (version != this.Version)
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

        private void SectionEntriesChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            General.Instance.DocumentModified = true;
        }

        #endregion Construction

        private void AboutCommandExecute()
        {
            DialogManager.Default.Show($"Honoo Language Localisation Converter\r\n\r\nVersion {this.Version}\r\n\r\nCopyright (C) Loki Honoo 2025. All rights reserved.",
                string.Empty,
                DialogButtons.OK,
                DialogDefaultButton.OK,
                DialogCloseButton.Ordinary,
                DialogImage.Information);
        }

        private void AddSectionCommandExecute()
        {
            this.Sections.Insert(0, new SectionEntry("Section" + _counter++));
            this.CurrentSection = this.Sections[0];
            _manualAdd = true;
        }

        private void AddTranslationCommandExecute()
        {
            if (this.CurrentSection != null)
            {
                this.CurrentSection.TranslationEntries.Insert(0, new TranslationEntry("Translation" + _counter++, string.Empty, "The \"Name\" string using by code member name. Spaces and special characters cannot be used."));
                _manualAdd = true;
            }
        }

        private void CreateDocument()
        {
            this.Informartion.AppName = "Application name";
            this.Informartion.AppVer = "1.0.0";
            this.Informartion.LangName = "en-US";
            this.Informartion.LangVer = "00";
            this.Informartion.Author = "HLLC";
            this.Informartion.Url = "https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter";
            this.Informartion.Remarks = string.Empty;
            this.Sections.Clear();
            var section1 = new SectionEntry("Menu");
            section1.TranslationEntries.Add(new TranslationEntry("File", "_File", "Menu button, Top item."));
            section1.TranslationEntries.Add(new TranslationEntry("CreateNew", "_New...", "Menu button, create new document."));
            section1.TranslationEntries.Add(new TranslationEntry("Open", "_Open...", "Menu button, Show dialog for select open file."));
            section1.TranslationEntries.Add(new TranslationEntry("Save", "_Save", "Menu button, Save to lang file."));
            section1.TranslationEntries.Add(new TranslationEntry("SaveAs", "Save _As...", "Menu button, Show dialog for select save file."));
            section1.TranslationEntries.Add(new TranslationEntry("SaveCSharpCodeAs", "Save C# code As...", "Menu button, Show dialog for select save file."));
            section1.TranslationEntries.Add(new TranslationEntry("Exit", "E_xit", "Exit app."));
            section1.TranslationEntries.Add(new TranslationEntry("Options", "_Options", "Menu button, Top item."));
            section1.TranslationEntries.Add(new TranslationEntry("Help", "_Help", "Menu button, Top item."));
            section1.TranslationEntries.Add(new TranslationEntry("Website", "_Website", "Menu button, Navigate to project url."));
            section1.TranslationEntries.Add(new TranslationEntry("About", "_About", "Menu button, Show dialog for app information."));
            this.Sections.Add(section1);
            var section2 = new SectionEntry("Main");
            section2.TranslationEntries.Add(new TranslationEntry("Informartion", "Informartion", "Tab name."));
            section2.TranslationEntries.Add(new TranslationEntry("Sections", "Sections", "Tab name."));
            section2.TranslationEntries.Add(new TranslationEntry("Sort", "Sort", "Button text."));
            section2.TranslationEntries.Add(new TranslationEntry("SectionEntries", "Section entries", "Title text."));
            section2.TranslationEntries.Add(new TranslationEntry("TranslationEntries", "Translation entries", "Title text."));
            section2.TranslationEntries.Add(new TranslationEntry("HasNewVersion", "New version published", "StatusBar tip."));
            this.Sections.Add(section2);
            var section3 = new SectionEntry("DialogMessages");
            section3.TranslationEntries.Add(new TranslationEntry("DocumentExistsCreateNew", "Document loaded already. Create new document?", "Dialog content."));
            section3.TranslationEntries.Add(new TranslationEntry("DocumentExistsLoadNew", "Document loaded already. Load new document?", "Dialog content."));
            section3.TranslationEntries.Add(new TranslationEntry("SectionNameEmpty", "Section \"Name\" string can't be empty.", "Dialog content."));
            section3.TranslationEntries.Add(new TranslationEntry("SectionNameDuplicate", "Section \"{0}\" has duplicate name.", "Dialog content set field {0}=Section Name."));
            section3.TranslationEntries.Add(new TranslationEntry("TranslationNameEmpty", "Section \"{0}\"'s translation entry string can't be empty.", "Dialog content."));
            section3.TranslationEntries.Add(new TranslationEntry("TranslationNameDuplicate", "Section \"{0}\"'s translation entry \"{1}\" has duplicate name.", "Dialog content set field {0}=Section Name,{1}=Translation Name."));
            section3.TranslationEntries.Add(new TranslationEntry("RemoveItem", "Remove \"{0}\" ?", "Dialog content set custom field {0}=Remove Name."));
            section3.TranslationEntries.Add(new TranslationEntry("SaveCodeStandard", "Standard class model for all code style", "Dialog content."));
            section3.TranslationEntries.Add(new TranslationEntry("SaveCodeNotifyBasic", "Binding-notify basic class", "Dialog content. INotifyPropertyChanging, INotifyPropertyChanged implemented."));
            section3.TranslationEntries.Add(new TranslationEntry("SaveCodeCommunityToolkit", "Binding-notify code style for lib - CommunityToolkit.Mvvm", "Dialog content. CommunityToolkit.Mvvm code style."));
            section3.TranslationEntries.Add(new TranslationEntry("ExitSaveRemind", "The document modified but has not been saved.\r\n\r\nExit application without save?", "Dialog content."));
            this.Sections.Add(section3);
            var section4 = new SectionEntry("ToastMessages");
            section4.TranslationEntries.Add(new TranslationEntry("LanguageFileSaved", "Language file saved.", "Toast content."));
            this.Sections.Add(section4);
            this.CurrentSection = section1;
            this.FileName = null;
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

        private void LoadLanguageFileCommandExecute()
        {
            var dig = new OpenFileDialog
            {
                Filter = "Language files (*.lang;lng;*.xml)|*.lang;*.lng;*.xml|All files (*.*)|*.*"
            };
            if (dig.ShowDialog() == true)
            {
                try
                {
                    LanguagePackage.Instance.Load(dig.FileName);
                    Settings.Instance.LanguageFile = dig.FileName;
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
                                try
                                {
                                    OpenDocument(dig.FileName);
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
                        },
                        null);
                }
                else
                {
                    try
                    {
                        OpenDocument(dig.FileName);
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
        }

        private void OpenDocument(string fileName)
        {
            using var manager = new XConfigManager(fileName);
            this.Informartion.AppName = manager.Default.Properties.GetStringValue("AppName", "This field is not available.");
            this.Informartion.AppVer = manager.Default.Properties.GetStringValue("AppVer", "This field is not available.");
            this.Informartion.LangName = manager.Default.Properties.GetStringValue("LangName", "This field is not available.");
            this.Informartion.LangVer = manager.Default.Properties.GetStringValue("LangVer", "This field is not available.");
            this.Informartion.Author = manager.Default.Properties.GetStringValue("Author", "This field is not available.");
            this.Informartion.Url = manager.Default.Properties.GetStringValue("Url", "This field is not available.");
            this.Informartion.Remarks = manager.Default.Properties.GetStringValue("Remarks", "This field is not available.");
            this.Sections.Clear();
            this.CurrentSection = null;
            if (manager.Sections.Count > 0)
            {
                foreach (var section in manager.Sections)
                {
                    var sectionEntry = new SectionEntry(section.Name);
                    foreach (var translation in section.Properties)
                    {
                        XString value = (XString)translation.Value;
                        var translationEntry = new TranslationEntry(translation.Key, value.GetStringValue(), value.Comment.GetValue());
                        sectionEntry.TranslationEntries.Add(translationEntry);
                    }
                    this.Sections.Add(sectionEntry);
                }
                this.CurrentSection = this.Sections[0];
            }
            this.FileName = fileName;
            this.DocumentLoaded = true;
            General.Instance.DocumentModified = false;
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
                var dig = new SaveFileDialog()
                {
                    Filter = "Language files (*.lang)|*.lang|Language files (*.lng)|*.lng|Language files (*.xml)|*.xml",
                    FileName = $"{this.Informartion.AppName}_{this.Informartion.AppVer}_{this.Informartion.LangName}.lang",
                };
                if (dig.ShowDialog() == true)
                {
                    string fileName = dig.FileName;
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
                    try
                    {
                        SaveDocument(fileName);
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
        }

        private void SaveCommandExecute()
        {
            if (string.IsNullOrEmpty(this.FileName))
            {
                this.SaveAsCommand.Execute(null);
            }
            else
            {
                try
                {
                    SaveDocument(this.FileName);
                    ToastManager.Default.Show(LanguagePackage.Instance.ToastMessages.LanguageFileSaved, 2000, ToastOptions.Information, null);
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

        private void SaveCSharpCodeAsCommandExecute()
        {
            if (this.Sections != null)
            {
                var stackPanel = new StackPanel();
                var radioButtonTray = new RadioButtonTray();
                radioButtonTray.Children.Add(new RadioButton() { HorizontalAlignment = HorizontalAlignment.Left, Content = LanguagePackage.Instance.DialogMessages.SaveCodeStandard });
                radioButtonTray.Children.Add(new RadioButton() { HorizontalAlignment = HorizontalAlignment.Left, Content = LanguagePackage.Instance.DialogMessages.SaveCodeNotifyBasic + " [.NET Framework 4.0+]", Margin = new Thickness(0, 10, 0, 0) });
                radioButtonTray.Children.Add(new RadioButton() { HorizontalAlignment = HorizontalAlignment.Left, Content = LanguagePackage.Instance.DialogMessages.SaveCodeNotifyBasic + " [.NET 6.0+]", Margin = new Thickness(0, 10, 0, 0), IsChecked = true });
                radioButtonTray.Children.Add(new RadioButton() { HorizontalAlignment = HorizontalAlignment.Left, Content = LanguagePackage.Instance.DialogMessages.SaveCodeCommunityToolkit, Margin = new Thickness(0, 10, 0, 0) });
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
                            var dig = new SaveFileDialog()
                            {
                                Filter = "C# code files (*.cs)|*.cs",
                                FileName = $"{this.Informartion.AppName}_{this.Informartion.AppVer}_{this.Informartion.LangName}.cs",
                                DefaultExt = ".cs",
                                AddExtension = true
                            };
                            if (dig.ShowDialog() == true)
                            {
                                try
                                {
                                    string code = radioButtonTray.SelectIndex switch
                                    {
                                        3 => Code.CreateCommunityToolkit(this.Informartion, this.Sections),
                                        2 => Code.CreateNotifyBasic(this.Informartion, this.Sections, true),
                                        1 => Code.CreateNotifyBasic(this.Informartion, this.Sections, false),
                                        _ => Code.CreateStandard(this.Informartion, this.Sections),
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
            if (this.Sections != null)
            {
                using var manager = new XConfigManager();
                manager.Default.Properties.AddString("AppName", this.Informartion.AppName);
                manager.Default.Properties.AddString("AppVer", this.Informartion.AppVer);
                manager.Default.Properties.AddString("LangName", this.Informartion.LangName);
                manager.Default.Properties.AddString("LangVer", this.Informartion.LangVer);
                manager.Default.Properties.AddString("Author", this.Informartion.Author);
                manager.Default.Properties.AddString("Url", this.Informartion.Url);
                manager.Default.Properties.AddString("Remarks", this.Informartion.Remarks);
                if (this.Sections != null && this.Sections.Count > 0)
                {
                    foreach (var se in this.Sections)
                    {
                        string sectionName = se.Name.Trim();
                        if (sectionName.Length == 0)
                        {
                            DialogManager.Default.Show(LanguagePackage.Instance.DialogMessages.SectionNameEmpty,
                                string.Empty,
                                DialogButtons.OK,
                                DialogDefaultButton.OK,
                                DialogCloseButton.Ordinary,
                                DialogImage.Error);
                            return;
                        }
                        else if (manager.Sections.ContainsName(sectionName))
                        {
                            DialogManager.Default.Show(string.Format(LanguagePackage.Instance.DialogMessages.SectionNameDuplicate, sectionName),
                                string.Empty,
                                DialogButtons.OK,
                                DialogDefaultButton.OK,
                                DialogCloseButton.Ordinary,
                                DialogImage.Error);
                            return;
                        }
                        var section = manager.Sections.Add(sectionName);
                        if (se.TranslationEntries.Count > 0)
                        {
                            foreach (var item in se.TranslationEntries)
                            {
                                string translationName = item.Name.Trim();
                                if (translationName.Length == 0)
                                {
                                    DialogManager.Default.Show(string.Format(LanguagePackage.Instance.DialogMessages.TranslationNameEmpty, translationName),
                                        string.Empty,
                                        DialogButtons.OK,
                                        DialogDefaultButton.OK,
                                        DialogCloseButton.Ordinary,
                                        DialogImage.Error);
                                    return;
                                }
                                else if (section.Properties.ContainsKey(translationName))
                                {
                                    DialogManager.Default.Show(string.Format(LanguagePackage.Instance.DialogMessages.TranslationNameDuplicate, translationName),
                                        string.Empty,
                                        DialogButtons.OK,
                                        DialogDefaultButton.OK,
                                        DialogCloseButton.Ordinary,
                                        DialogImage.Error);
                                    return;
                                }
                                section.Properties.Add(item.Name, new XString(item.Value)).Comment.SetValue(item.Comment, true);
                            }
                        }
                    }
                }
                manager.Save(fileName);
                this.FileName = fileName;
                General.Instance.DocumentModified = false;
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

        private void TestExportCommandExecute()
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