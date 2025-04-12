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
using System.IO;
using System.Linq;
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

        [ObservableProperty]
        private string? _appName;

        [ObservableProperty]
        private string? _appVer;

        [ObservableProperty]
        private string? _author;

        [ObservableProperty]
        private SectionEntry? _currentSection;

        [ObservableProperty]
        private bool _documentLoaded;

        [ObservableProperty]
        private string? _langName;

        [ObservableProperty]
        private string? _langVer;

        [ObservableProperty]
        private Localization _localization = Localization.Instance;

        [ObservableProperty]
        private string? _remarks;

        [ObservableProperty]
        private ObservableCollection<SectionEntry>? _sections;

        [ObservableProperty]
        private string? _url;

        [ObservableProperty]
        private double _windowHeight;

        [ObservableProperty]
        private double _windowLeft;

        [ObservableProperty]
        private double _windowTop;

        [ObservableProperty]
        private double _windowWidth;

        public ICommand AboutCommand { get; } = new RelayCommand(() =>
                {
                    string version = Assembly.GetExecutingAssembly().GetName().Version!.ToString(3);
                    DialogManager.Default.Show($"Honoo Language Localisation Converter\r\n\r\nVersion {version}\r\n\r\nCopyright (C) Loki Honoo 2025. All rights reserved.",
                        string.Empty,
                        DialogButtons.OK,
                        DialogCloseButton.Ordinary,
                        DialogImage.Information);
                });

        public ICommand AddLanguageEntryCommand { get; }
        public ICommand AddSectionEntryCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand ExitCommand { get; } = new RelayCommand(() => { Environment.Exit(0); });
        public string? LanguageFile { get; set; }
        public ICommand LoadLanguageFileCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand RemoveLanguageEntryCommand { get; }
        public ICommand RemoveSectionEntryCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand SaveCSharpCodeAsCommand { get; }
        public ICommand SectionMoveCommand { get; }
        public ICommand SortLanguageEntriesCommand { get; }
        public ICommand TestExportCommand { get; }

        #endregion Members

        public MainWindowViewModel()
        {
            this.CreateCommand = new RelayCommand(Create);
            this.OpenCommand = new RelayCommand(Open);
            this.SaveAsCommand = new RelayCommand(SaveAs, () => { return this.DocumentLoaded; });
            this.SaveCSharpCodeAsCommand = new RelayCommand(SaveCSharpCodeAs, () => { return this.DocumentLoaded; });
            this.LoadLanguageFileCommand = new RelayCommand(LoadLanguageFile);
            this.TestExportCommand = new RelayCommand(TestExport);
            this.AddSectionEntryCommand = new RelayCommand(AddSectionEntry);
            this.SectionMoveCommand = new RelayCommand<DragEventArgs>(SectionMove);
            this.AddLanguageEntryCommand = new RelayCommand(AddLanguageEntry);
            this.SortLanguageEntriesCommand = new RelayCommand(SortLanguageEntries);
            this.RemoveSectionEntryCommand = new RelayCommand<object>(RemoveSectionEntry);
            this.RemoveLanguageEntryCommand = new RelayCommand<object>(RemoveLanguageEntry);
            this.PropertyChanged += OnPropertyChanged;
            Locator.MainWindowViewModel = this;
        }

        private void AddLanguageEntry()
        {
            if (this.CurrentSection != null)
            {
                var stackPanel = new StackPanel();
                var textBlock = new TextBlock
                {
                    Text = this.Localization.Messages.CreateLanguageEntryTip,
                };
                var textBox = new System.Windows.Controls.TextBox
                {
                    Margin = new Thickness(0, 10, 0, 0),
                    Width = 200,
                };
                stackPanel.Children.Add(textBlock);
                stackPanel.Children.Add(textBox);
                DialogManager.Default.Show(stackPanel,
                    string.Empty,
                    DialogButtons.OKCancel,
                    DialogCloseButton.Ordinary,
                    DialogImage.None,
                    false,
                    DialogLocalization.Default,
                    DialogSize.Default,
                    (e) =>
                    {
                        foreach (var item in this.CurrentSection.LanguageEntries)
                        {
                            string key = textBox.Text.Trim();
                            if (item.Key == key)
                            {
                                DialogManager.Default.Show(string.Format(this.Localization.Messages.KeyExists, key),
                                    string.Empty,
                                    DialogButtons.OK,
                                    DialogCloseButton.Ordinary,
                                    DialogImage.Error);
                                return false;
                            }
                        }
                        return true;
                    },
                    (e) =>
                    {
                        if (e.DialogResult == DialogResult.OK)
                        {
                            this.CurrentSection.LanguageEntries.Insert(0, new LanguageEntry(textBox.Text.Trim(), string.Empty, string.Empty));
                        }
                    },
                    null);
            }
        }

        private void AddSectionEntry()
        {
            if (this.Sections != null)
            {
                var stackPanel = new StackPanel();
                var textBlock = new TextBlock
                {
                    Text = this.Localization.Messages.CreateSctionTip,
                };
                var textBox = new System.Windows.Controls.TextBox
                {
                    Margin = new Thickness(0, 10, 0, 0),
                    Width = 200,
                };
                stackPanel.Children.Add(textBlock);
                stackPanel.Children.Add(textBox);
                DialogManager.Default.Show(stackPanel,
                    string.Empty,
                    DialogButtons.OKCancel,
                    DialogCloseButton.Ordinary,
                    DialogImage.None,
                    false,
                    DialogLocalization.Default,
                    DialogSize.Default,
                    (e) =>
                    {
                        foreach (var item in this.Sections)
                        {
                            string name = textBox.Text.Trim();
                            if (item.Name == name)
                            {
                                DialogManager.Default.Show(string.Format(this.Localization.Messages.NameExists, name),
                                    string.Empty,
                                    DialogButtons.OK,
                                    DialogCloseButton.Ordinary,
                                    DialogImage.Error);
                                return false;
                            }
                        }
                        return true;
                    },
                    (e) =>
                    {
                        if (e.DialogResult == DialogResult.OK)
                        {
                            this.Sections.Insert(0, new SectionEntry(textBox.Text.Trim()));
                            this.CurrentSection = this.Sections[0];
                        }
                    },
                    null);
            }
        }

        private void Create()
        {
            if (this.DocumentLoaded)
            {
                DialogManager.Default.Show(this.Localization.Messages.DocumentExistsCreateNew,
                    string.Empty,
                    DialogButtons.OKCancel,
                    DialogCloseButton.Ordinary,
                    DialogImage.Information,
                    false,
                    DialogLocalization.Default,
                    DialogSize.Default,
                    null,
                    (e) =>
                    {
                        if (e.DialogResult == DialogResult.OK)
                        {
                            CreateTemplate();
                        }
                    },
                    null);
            }
            else
            {
                CreateTemplate();
            }
        }

        private void CreateTemplate()
        {
            this.AppName = "Application name";
            this.AppVer = "1.0.0";
            this.LangName = "en-US";
            this.LangVer = "1.0.0";
            this.Author = "HLLC";
            this.Url = "https://github.com/LokiHonoo/Honoo-Language-Localisation-Converter";
            this.Remarks = string.Empty;
            this.Sections = [];
            var section1 = new SectionEntry("Menu");
            section1.LanguageEntries.Add(new LanguageEntry("File", "_File", "Menu button, Top item."));
            section1.LanguageEntries.Add(new LanguageEntry("CreateTemplate", "_Create template...", "Menu button, create new template."));
            section1.LanguageEntries.Add(new LanguageEntry("OpenTemplate", "_Open template...", "Menu button, Show dialog for select open file."));
            section1.LanguageEntries.Add(new LanguageEntry("SaveAs", "Save _As...", "Menu button, Show dialog for select save file."));
            section1.LanguageEntries.Add(new LanguageEntry("SaveCSharpCodeAs", "Save C# code As...", "Menu button, Show dialog for select save file."));
            section1.LanguageEntries.Add(new LanguageEntry("Exit", "E_xit", "Exit app."));
            section1.LanguageEntries.Add(new LanguageEntry("Options", "_Options", "Menu button, Top item."));
            section1.LanguageEntries.Add(new LanguageEntry("Help", "_Help", "Menu button, Top item."));
            section1.LanguageEntries.Add(new LanguageEntry("About", "_About", "Menu button, Show dialog for app information."));
            this.Sections.Add(section1);
            var section2 = new SectionEntry("Main");
            section2.LanguageEntries.Add(new LanguageEntry("Informartion", "Informartion", "Tab name."));
            section2.LanguageEntries.Add(new LanguageEntry("Sections", "Sections", "Tab name."));
            section2.LanguageEntries.Add(new LanguageEntry("Sort", "Sort", "Button text."));
            section2.LanguageEntries.Add(new LanguageEntry("SectionEntries", "Section entries", "Title text."));
            section2.LanguageEntries.Add(new LanguageEntry("LanguageEntries", "Language entries", "Title text."));
            this.Sections.Add(section2);
            var section3 = new SectionEntry("Messages");
            section3.LanguageEntries.Add(new LanguageEntry("DocumentExistsCreateNew", "Document loaded already. Create new document?", "Dialog content."));
            section3.LanguageEntries.Add(new LanguageEntry("DocumentExistsLoadNew", "Document loaded already. Load new document?", "Dialog content."));
            section3.LanguageEntries.Add(new LanguageEntry("CreateSctionTip", "Input Section name.\r\nThe string using by code member name.\r\nSpaces and special characters cannot be used.", "Dialog content."));
            section3.LanguageEntries.Add(new LanguageEntry("CreateLanguageEntryTip", "Input language entry name.\r\nThe string using by code member name.\r\nSpaces and special characters cannot be used.", "Dialog content."));
            section3.LanguageEntries.Add(new LanguageEntry("NameExists", "Name \"{0}\" exists.", "Dialog content set custom field {0}."));
            section3.LanguageEntries.Add(new LanguageEntry("KeyExists", "Key \"{0}\" exists.", "Dialog content set custom field {0}."));
            section3.LanguageEntries.Add(new LanguageEntry("RemoveItem", "Remove \"{0}\" ?", "Dialog content set custom field {0}."));
            section3.LanguageEntries.Add(new LanguageEntry("SaveCodeStandard", "Standard class model for all app type", "Dialog content."));
            section3.LanguageEntries.Add(new LanguageEntry("SaveCodeWpf", "Using in WPF basic class", "Dialog content."));
            section3.LanguageEntries.Add(new LanguageEntry("SaveCodeMvvm", "MVVM structure for lib - CommunityToolkit.Mvvm", "Dialog content."));
            this.Sections.Add(section3);
            this.CurrentSection = section1;
            this.DocumentLoaded = true;
        }

        private void LoadLanguageFile()
        {
            var dig = new OpenFileDialog
            {
                Filter = "Language files (*.lang;lng;*.xml)|*.lang;*.lng;*.xml|All files (*.*)|*.*"
            };
            if (dig.ShowDialog() == true)
            {
                try
                {
                    this.Localization.Load(dig.FileName);
                    this.LanguageFile = dig.FileName;
                }
                catch (Exception ex)
                {
                    this.Localization.ResetDefault();
                    DialogManager.Default.Show(ex.Message,
                        string.Empty,
                        DialogButtons.OK,
                        DialogCloseButton.Ordinary,
                        DialogImage.Error);
                }
            }
        }

        private void LoadTemplate(string fileName)
        {
            using var manager = new XConfigManager(fileName);
            this.AppName = manager.Default.Properties.GetStringValue("AppName", "This field is not available.");
            this.AppVer = manager.Default.Properties.GetStringValue("AppVer", "This field is not available.");
            this.LangName = manager.Default.Properties.GetStringValue("LangName", "This field is not available.");
            this.LangVer = manager.Default.Properties.GetStringValue("LangVer", "This field is not available.");
            this.Author = manager.Default.Properties.GetStringValue("Author", "This field is not available.");
            this.Url = manager.Default.Properties.GetStringValue("Url", "This field is not available.");
            this.Remarks = manager.Default.Properties.GetStringValue("Remarks", "This field is not available.");
            this.Sections = [];
            this.CurrentSection = null;
            if (manager.Sections.Count > 0)
            {
                foreach (var section in manager.Sections)
                {
                    var se = new SectionEntry(section.Name);
                    foreach (var item in section.Properties)
                    {
                        XString value = (XString)item.Value;
                        var entry = new LanguageEntry(item.Key, value.GetStringValue(), value.Comment.GetValue());
                        se.LanguageEntries.Add(entry);
                    }
                    this.Sections.Add(se);
                }
                this.CurrentSection = this.Sections[0];
            }
            this.DocumentLoaded = true;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.DocumentLoaded))
            {
                ((IRelayCommand)this.SaveAsCommand).NotifyCanExecuteChanged();
                ((IRelayCommand)this.SaveCSharpCodeAsCommand).NotifyCanExecuteChanged();
            }
        }

        private void Open()
        {
            var dig = new OpenFileDialog
            {
                Filter = "Language files (*.lang;lng;*.xml)|*.lang;*.lng;*.xml|All files (*.*)|*.*"
            };
            if (dig.ShowDialog() == true)
            {
                if (this.DocumentLoaded)
                {
                    DialogManager.Default.Show(this.Localization.Messages.DocumentExistsLoadNew,
                        string.Empty,
                        DialogButtons.OKCancel,
                        DialogCloseButton.Ordinary,
                        DialogImage.Information,
                        false,
                        DialogLocalization.Default,
                        DialogSize.Default,
                        null,
                        (e) =>
                        {
                            if (e.DialogResult == DialogResult.OK)
                            {
                                try
                                {
                                    LoadTemplate(dig.FileName);
                                }
                                catch (Exception ex)
                                {
                                    CreateTemplate();
                                    DialogManager.Default.Show(ex.Message,
                                        string.Empty,
                                        DialogButtons.OK,
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
                        LoadTemplate(dig.FileName);
                    }
                    catch (Exception ex)
                    {
                        CreateTemplate();
                        DialogManager.Default.Show(ex.Message,
                            string.Empty,
                            DialogButtons.OK,
                            DialogCloseButton.Ordinary,
                            DialogImage.Error);
                    }
                }
            }
        }

        private void RemoveLanguageEntry(object? obj)
        {
            if (obj is LanguageEntry entry && this.CurrentSection != null)
            {
                DialogManager.Default.Show(string.Format(this.Localization.Messages.RemoveItem, entry.Key),
                    string.Empty,
                    DialogButtons.OKCancel,
                    DialogCloseButton.Ordinary,
                    DialogImage.Information,
                    false,
                    DialogLocalization.Default,
                    DialogSize.Default,
                    null,
                    (e) =>
                    {
                        if (e.DialogResult == DialogResult.OK)
                        {
                            this.CurrentSection.LanguageEntries.Remove(entry);
                        }
                    },
                    null);
            }
        }

        private void RemoveSectionEntry(object? obj)
        {
            if (obj is SectionEntry entry && this.Sections != null)
            {
                DialogManager.Default.Show(string.Format(this.Localization.Messages.RemoveItem, entry.Name),
                    string.Empty,
                    DialogButtons.OKCancel,
                    DialogCloseButton.Ordinary,
                    DialogImage.Information,
                    false,
                    DialogLocalization.Default,
                    DialogSize.Default,
                    null,
                    (e) =>
                    {
                        if (e.DialogResult == DialogResult.OK)
                        {
                            this.Sections.Remove(entry);
                            if (entry == this.CurrentSection)
                            {
                                this.CurrentSection = null;
                            }
                        }
                    },
                    null);
            }
        }

        private void SaveAs()
        {
            var dig = new SaveFileDialog()
            {
                Filter = "Language files (*.lang)|*.lang|Language files (*.lng)|*.lng|Language files (*.xml)|*.xml",
                FileName = $"{this.AppName}_{this.AppVer}_{this.LangName}.lang",
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
                        DialogCloseButton.Ordinary,
                        DialogImage.Error);
                }
            }
        }

        private void SaveCSharpCodeAs()
        {
            if (this.Sections != null)
            {
                var stackPanel = new StackPanel();
                var radioButtonTray = new RadioButtonTray();
                radioButtonTray.Children.Add(new RadioButton() { HorizontalAlignment = HorizontalAlignment.Left, Content = this.Localization.Messages.SaveCodeStandard, IsChecked = true });
                radioButtonTray.Children.Add(new RadioButton() { HorizontalAlignment = HorizontalAlignment.Left, Content = this.Localization.Messages.SaveCodeWpf + " [.NET Framework 4.0+]", Margin = new Thickness(0, 10, 0, 0) });
                radioButtonTray.Children.Add(new RadioButton() { HorizontalAlignment = HorizontalAlignment.Left, Content = this.Localization.Messages.SaveCodeWpf + " [.NET 6.0+]", Margin = new Thickness(0, 10, 0, 0) });
                radioButtonTray.Children.Add(new RadioButton() { HorizontalAlignment = HorizontalAlignment.Left, Content = this.Localization.Messages.SaveCodeMvvm, Margin = new Thickness(0, 10, 0, 0) });
                DialogManager.Default.Show(radioButtonTray,
                    string.Empty,
                    DialogButtons.OKCancel,
                    DialogCloseButton.Ordinary,
                    DialogImage.Information,
                    false,
                    DialogLocalization.Default,
                    DialogSize.Default,
                    null,
                    (e) =>
                    {
                        if (e.DialogResult == DialogResult.OK)
                        {
                            var dig = new SaveFileDialog()
                            {
                                Filter = "C# code files (*.cs)|*.cs",
                                FileName = $"{this.AppName}_{this.AppVer}_{this.LangName}.cs",
                                DefaultExt = ".cs",
                                AddExtension = true
                            };
                            if (dig.ShowDialog() == true)
                            {
                                try
                                {
                                    string code = radioButtonTray.SelectIndex switch
                                    {
                                        3 => Code.CreateMVVM(this.AppName, this.AppVer, this.LangName, this.LangVer, this.Author, this.Url, this.Remarks, this.Sections),
                                        2 => Code.CreateWPF(this.AppName, this.AppVer, this.LangName, this.LangVer, this.Author, this.Url, this.Remarks, this.Sections, true),
                                        1 => Code.CreateWPF(this.AppName, this.AppVer, this.LangName, this.LangVer, this.Author, this.Url, this.Remarks, this.Sections, false),
                                        _ => Code.CreateStandard(this.AppName, this.AppVer, this.LangName, this.LangVer, this.Author, this.Url, this.Remarks, this.Sections),
                                    };
                                    File.WriteAllText(dig.FileName, code, new UTF8Encoding(false));
                                }
                                catch (Exception ex)
                                {
                                    DialogManager.Default.Show(ex.Message,
                                        string.Empty,
                                        DialogButtons.OK,
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
            using var manager = new XConfigManager();
            manager.Default.Properties.AddString("AppName", this.AppName);
            manager.Default.Properties.AddString("AppVer", this.AppVer);
            manager.Default.Properties.AddString("LangName", this.LangName);
            manager.Default.Properties.AddString("LangVer", this.LangVer);
            manager.Default.Properties.AddString("Author", this.Author);
            manager.Default.Properties.AddString("Url", this.Url);
            manager.Default.Properties.AddString("Remarks", this.Remarks);
            if (this.Sections != null && this.Sections.Count > 0)
            {
                foreach (var se in this.Sections)
                {
                    var section = manager.Sections.Add(se.Name);
                    if (se.LanguageEntries.Count > 0)
                    {
                        foreach (var item in se.LanguageEntries)
                        {
                            section.Properties.Add(item.Key, new XString(item.Value)).Comment.SetValue(item.Comment, true);
                        }
                    }
                }
            }
            manager.Save(fileName);
        }

        private void SectionMove(DragEventArgs? e)
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

        private void SortLanguageEntries()
        {
            if (this.CurrentSection != null && this.CurrentSection.LanguageEntries.Count > 1)
            {
                List<LanguageEntry> list = [.. this.CurrentSection.LanguageEntries.OrderBy(x => x.Key)];
                for (int i = 0; i < list.Count; i++)
                {
                    this.CurrentSection.LanguageEntries.Move(this.CurrentSection.LanguageEntries.IndexOf(list[i]), i);
                }
            }
        }

        private void TestExport()
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
                    this.Localization.Save(false, fileName1);
                    this.Localization.Save(false, fileName2);
                    using (var stream = new FileStream(fileName3, FileMode.Create, FileAccess.Write))
                    {
                        this.Localization.Save(true, stream);
                    }
                    using (var stream = new FileStream(fileName4, FileMode.Create, FileAccess.Write))
                    {
                        this.Localization.Save(true, stream);
                    }
                }
                catch (Exception ex)
                {
                    DialogManager.Default.Show(ex.Message,
                        string.Empty,
                        DialogButtons.OK,
                        DialogCloseButton.Ordinary,
                        DialogImage.Error);
                }
            }
        }
    }
}