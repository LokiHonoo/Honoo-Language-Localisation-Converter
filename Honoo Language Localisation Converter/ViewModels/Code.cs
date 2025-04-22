using System.Collections.Generic;
using System.Text;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    internal static class Code
    {
        internal static string CreateCode(int codeType, SectionEntry information, IList<SectionEntry> sections)
        {
            var builder = new StringBuilder();
            switch (codeType)
            {
                case 3:
                    builder.AppendLine("using CommunityToolkit.Mvvm.ComponentModel;");
                    builder.AppendLine("using Honoo.Configuration;");
                    builder.AppendLine("using System.IO;");
                    break;

                case 2:
                case 1:
                    builder.AppendLine("using Honoo.Configuration;");
                    builder.AppendLine("using System.ComponentModel;");
                    builder.AppendLine("using System.IO;");
                    break;

                default:
                    builder.AppendLine("using Honoo.Configuration;");
                    builder.AppendLine("using System.IO;");
                    break;
            }
            builder.AppendLine();
            builder.AppendLine("namespace HonooLanguageLocalisationConverter.ViewModels");
            builder.AppendLine("{");
            switch (codeType)
            {
                case 3:
                    builder.AppendLine("    /// <summary>");
                    builder.AppendLine($"    /// Language package class. Binding-notify code style for lib - CommunityToolkit.Mvvm.");
                    builder.AppendLine("    /// Using single instance <see cref=\"Instance\"/> to visit.");
                    builder.AppendLine("    /// <br />Install nuget package:");
                    builder.AppendLine("    /// <br /><see href=\"https://www.nuget.org/packages/CommunityToolkit.Mvvm\"/>.");
                    builder.AppendLine("    /// <br /><see href=\"https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager\"/>.");
                    builder.AppendLine("    /// </summary>");
                    break;

                case 2:
                    builder.AppendLine("    /// <summary>");
                    builder.AppendLine($"    /// Language package class. Binding-notify basic class [.NET 6.0+][With Project field <Nullable>enable</Nullable>]");
                    builder.AppendLine("    /// Using single instance <see cref=\"Instance\"/> to visit.");
                    builder.AppendLine("    /// <br />Install nuget package: <see href=\"https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager\"/>.");
                    builder.AppendLine("    /// </summary>");
                    break;

                case 1:
                    builder.AppendLine("    /// <summary>");
                    builder.AppendLine($"    /// Language package class. Binding-notify basic class [.NET Framework 4.0+]");
                    builder.AppendLine("    /// Using single instance <see cref=\"Instance\"/> to visit.");
                    builder.AppendLine("    /// <br />Install nuget package: <see href=\"https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager\"/>.");
                    builder.AppendLine("    /// </summary>");
                    break;

                default:
                    builder.AppendLine("    /// <summary>");
                    builder.AppendLine($"    /// Language package class. Standard class model for all code style.");
                    builder.AppendLine("    /// Using single instance <see cref=\"Instance\"/> to visit.");
                    builder.AppendLine("    /// <br />Install nuget package: <see href=\"https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager\"/>.");
                    builder.AppendLine("    /// </summary>");
                    break;
            }
            switch (codeType)
            {
                case 3:
                    builder.AppendLine("    public sealed partial class LanguagePackage");
                    break;

                case 2:
                case 1:
                default:
                    builder.AppendLine("    public sealed class LanguagePackage");
                    break;
            }
            builder.AppendLine("    {");
            builder.AppendLine("        #region Instance");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Language package instance.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        public static LanguagePackage Instance { get; } = new LanguagePackage();");
            builder.AppendLine();
            builder.AppendLine("        #endregion Instance");
            builder.AppendLine();
            builder.AppendLine("        #region Members");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Language package creator.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine($"        public string Creator {{ get; }} = \"{General.Instance.Creator}\";");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Creator website.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine($"        public string Website {{ get; }} = \"{General.Instance.Website}\";");
            builder.AppendLine();
            builder.AppendLine("        #region Sections");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Information section.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        public __Information Information { get; } = new __Information();");
            foreach (var section in sections)
            {
                string sectionNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        /// {sectionNameU} section.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public __{sectionNameU} {sectionNameU} {{ get; }} = new __{sectionNameU}();");
            }
            builder.AppendLine();
            builder.AppendLine("        #endregion Sections");
            builder.AppendLine();
            builder.AppendLine("        #endregion Members");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Initialize new instance of LanguagePackage class.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        internal LanguagePackage()");
            builder.AppendLine("        {");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Load language file.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"fileName\">Language file name.</param>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE0063:Use simple 'using' statement\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public void Load(string fileName)");
            builder.AppendLine("        {");
            builder.AppendLine("            using (var manager = new XConfigManager(fileName, true))");
            builder.AppendLine("            {");
            builder.AppendLine("                this.Information.LoadInternal(manager);");
            foreach (var section in sections)
            {
                string sectionNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sectionNameU}.LoadInternal(manager);");
            }
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Load language stream.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"stream\">Language stream.</param>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE0063:Use simple 'using' statement\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public void Load(Stream stream)");
            builder.AppendLine("        {");
            builder.AppendLine("            using (var manager = new XConfigManager(stream))");
            builder.AppendLine("            {");
            builder.AppendLine("                this.Information.LoadInternal(manager);");
            foreach (var section in sections)
            {
                string sectionNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sectionNameU}.LoadInternal(manager);");
            }
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Reset all properties to default values.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        public void ResetDefault()");
            builder.AppendLine("        {");
            builder.AppendLine("            this.Information.ResetDefaultInternal();");
            foreach (var section in sections)
            {
                string sectionNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"            this.{sectionNameU}.ResetDefaultInternal();");
            }
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Save to language file.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"defaultFields\">Select current fields or default fields.</param>");
            builder.AppendLine("        /// <param name=\"fileName\">Language file name.</param>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE0063:Use simple 'using' statement\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public void Save(bool defaultFields, string fileName)");
            builder.AppendLine("        {");
            builder.AppendLine("            using (var manager = new XConfigManager())");
            builder.AppendLine("            {");
            builder.AppendLine("                manager.Default.Properties.AddString(\"Creator\", this.Creator);");
            builder.AppendLine("                manager.Default.Properties.AddString(\"Website\", this.Website);");
            builder.AppendLine($"                this.Information.SaveInternal(defaultFields, manager);");
            foreach (var section in sections)
            {
                string sectionNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sectionNameU}.SaveInternal(defaultFields, manager);");
            }
            builder.AppendLine("                manager.Save(fileName);");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Save to language stream.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"defaultFields\">Select current fields or default fields.</param>");
            builder.AppendLine("        /// <param name=\"stream\">Language stream.</param>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE0063:Use simple 'using' statement\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public void Save(bool defaultFields, Stream stream)");
            builder.AppendLine("        {");
            builder.AppendLine("            using (var manager = new XConfigManager())");
            builder.AppendLine("            {");
            builder.AppendLine("                manager.Default.Properties.AddString(\"Creator\", this.Creator);");
            builder.AppendLine("                manager.Default.Properties.AddString(\"Website\", this.Website);");
            builder.AppendLine($"                this.Information.SaveInternal(defaultFields, manager);");
            foreach (var section in sections)
            {
                string sectionNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sectionNameU}.SaveInternal(defaultFields, manager);");
            }
            builder.AppendLine("                manager.Save(stream);");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            switch (codeType)
            {
                case 3: builder.AppendLine(CreateCommunityToolkitSectionCode(information)); break;
                case 2: builder.AppendLine(CreateNotifyBasicSectionCode(information, true)); break;
                case 1: builder.AppendLine(CreateNotifyBasicSectionCode(information, false)); break;
                default: builder.AppendLine(CreateStandardSectionCode(information)); break;
            }
            foreach (var section in sections)
            {
                switch (codeType)
                {
                    case 3: builder.AppendLine(CreateCommunityToolkitSectionCode(section)); break;
                    case 2: builder.AppendLine(CreateNotifyBasicSectionCode(section, true)); break;
                    case 1: builder.AppendLine(CreateNotifyBasicSectionCode(section, false)); break;
                    default: builder.AppendLine(CreateStandardSectionCode(section)); break;
                }
            }
            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }

        private static string CreateCommunityToolkitSectionCode(SectionEntry section)
        {
            var builder = new StringBuilder();
            string sectionNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
            builder.AppendLine($"        #region {sectionNameU}");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine($"        /// {sectionNameU} section.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE1006:Naming Styles\", Justification = \"<Pending>\")]");
            builder.AppendLine($"        public sealed partial class __{sectionNameU} : ObservableObject");
            builder.AppendLine("        {");
            builder.AppendLine("            #region Comments");
            builder.AppendLine();
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"            private const string _{translationNameL}_c = \"{FixString(entry.Comment)}\";");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Comments");
            builder.AppendLine();
            builder.AppendLine("            #region Default");
            builder.AppendLine();
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"            private const string _{translationNameL}_d = \"{FixString(entry.Value)}\";");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Default");
            builder.AppendLine();
            builder.AppendLine("            #region Members");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine();
                builder.AppendLine("            /// <summary>");
                builder.AppendLine($"            /// {entry.Comment}");
                builder.AppendLine("            /// </summary>");
                builder.AppendLine("            [ObservableProperty]");
                builder.AppendLine($"            private string _{translationNameL} = _{translationNameL}_d;");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Members");
            builder.AppendLine();
            builder.AppendLine($"            internal __{sectionNameU}()");
            builder.AppendLine("            {");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void LoadInternal(XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine($"                if (manager.Sections.TryGetValue(\"{section.Name}\", out XSection section))");
            builder.AppendLine("                {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name![0]) + entry.Name![1..];
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"                    this.{translationNameU} = section.Properties.GetStringValue(\"{entry.Name!}\", _{translationNameL}_d);");
            }
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void ResetDefaultInternal()");
            builder.AppendLine("            {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name![0]) + entry.Name![1..];
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"                this.{translationNameU} = _{translationNameL}_d;");
            }
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void SaveInternal(bool defaultFields, XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine($"                XSection section = manager.Sections.Add(\"{section.Name}\");");
            builder.AppendLine("                if (defaultFields)");
            builder.AppendLine("                {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"                    section.Properties.AddString(\"{entry.Name!}\", _{translationNameL}_d).Comment.SetValue(_{translationNameL}_c, true);");
            }
            builder.AppendLine("                }");
            builder.AppendLine("                else");
            builder.AppendLine("                {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name![0]) + entry.Name![1..];
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"                    section.Properties.AddString(\"{entry.Name!}\", this.{translationNameU}).Comment.SetValue(_{translationNameL}_c, true);");
            }
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine($"        #endregion {sectionNameU}");
            return builder.ToString();
        }

        private static string CreateNotifyBasicSectionCode(SectionEntry section, bool nullSign)
        {
            var builder = new StringBuilder();
            string sectionNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
            builder.AppendLine($"        #region {sectionNameU}");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine($"        /// {sectionNameU} section.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE1006:Naming Styles\", Justification = \"<Pending>\")]");
            builder.AppendLine($"        public sealed class __{sectionNameU} : INotifyPropertyChanging, INotifyPropertyChanged");
            builder.AppendLine("        {");
            builder.AppendLine("            #region Events");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Property changed event handler.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine($"            public event PropertyChangedEventHandler{(nullSign ? "?" : string.Empty)} PropertyChanged;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Property changing event handler.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine($"            public event PropertyChangingEventHandler{(nullSign ? "?" : string.Empty)} PropertyChanging;");
            builder.AppendLine();
            builder.AppendLine("            private void OnPropertyChanged(string name)");
            builder.AppendLine("            {");
            builder.AppendLine("                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            private void OnPropertyChanging(string name)");
            builder.AppendLine("            {");
            builder.AppendLine("                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            #endregion Events");
            builder.AppendLine();
            builder.AppendLine("            #region Comments");
            builder.AppendLine();
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"            private const string _{translationNameL}_c = \"{FixString(entry.Comment)}\";");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Comments");
            builder.AppendLine();
            builder.AppendLine("            #region Default");
            builder.AppendLine();
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"            private const string _{translationNameL}_d = \"{FixString(entry.Value)}\";");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Default");
            builder.AppendLine();
            builder.AppendLine("            #region Members");
            builder.AppendLine();
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"            private string _{translationNameL} = _{translationNameL}_d;");
            }
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name![0]) + entry.Name![1..];
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine();
                builder.AppendLine("            /// <summary>");
                builder.AppendLine($"            /// {entry.Comment}");
                builder.AppendLine("            /// </summary>");
                builder.AppendLine($"            public string {translationNameU} {{ get {{ return _{translationNameL}; }} set {{ OnPropertyChanging(nameof(this.{translationNameU})); _{translationNameL} = value; OnPropertyChanged(nameof(this.{translationNameU})); }} }}");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Members");
            builder.AppendLine();
            builder.AppendLine($"            internal __{sectionNameU}()");
            builder.AppendLine("            {");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void LoadInternal(XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine($"                if (manager.Sections.TryGetValue(\"{section.Name}\", out XSection section))");
            builder.AppendLine("                {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name![0]) + entry.Name![1..];
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"                    this.{translationNameU} = section.Properties.GetStringValue(\"{entry.Name!}\", _{translationNameL}_d);");
            }
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void ResetDefaultInternal()");
            builder.AppendLine("            {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name![0]) + entry.Name![1..];
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"                this.{translationNameU} = _{translationNameL}_d;");
            }
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void SaveInternal(bool defaultFields, XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine($"                XSection section = manager.Sections.Add(\"{section.Name}\");");
            builder.AppendLine("                if (defaultFields)");
            builder.AppendLine("                {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"                    section.Properties.AddString(\"{entry.Name!}\", _{translationNameL}_d).Comment.SetValue(_{translationNameL}_c, true);");
            }
            builder.AppendLine("                }");
            builder.AppendLine("                else");
            builder.AppendLine("                {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name![0]) + entry.Name![1..];
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"                    section.Properties.AddString(\"{entry.Name!}\", this.{translationNameU}).Comment.SetValue(_{translationNameL}_c, true);");
            }
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine($"        #endregion {sectionNameU}");
            return builder.ToString();
        }

        private static string CreateStandardSectionCode(SectionEntry section)
        {
            var builder = new StringBuilder();
            string sectionNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
            builder.AppendLine($"        #region {sectionNameU}");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine($"        /// {sectionNameU} section.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE1006:Naming Styles\", Justification = \"<Pending>\")]");
            builder.AppendLine($"        public sealed class __{sectionNameU}");
            builder.AppendLine("        {");
            builder.AppendLine("            #region Comments");
            builder.AppendLine();
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"            private const string _{translationNameL}_c = \"{FixString(entry.Comment)}\";");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Comments");
            builder.AppendLine();
            builder.AppendLine("            #region Default");
            builder.AppendLine();
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"            private const string _{translationNameL}_d = \"{FixString(entry.Value)}\";");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Default");
            builder.AppendLine();
            builder.AppendLine("            #region Members");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name![0]) + entry.Name![1..];
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine();
                builder.AppendLine("            /// <summary>");
                builder.AppendLine($"            /// {entry.Comment}");
                builder.AppendLine("            /// </summary>");
                builder.AppendLine($"            public string {translationNameU} {{ get; set; }} = _{translationNameL}_d;");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Members");
            builder.AppendLine();
            builder.AppendLine($"            internal __{sectionNameU}()");
            builder.AppendLine("            {");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void LoadInternal(XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine($"                if (manager.Sections.TryGetValue(\"{section.Name}\", out XSection section))");
            builder.AppendLine("                {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name![0]) + entry.Name![1..];
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"                    this.{translationNameU} = section.Properties.GetStringValue(\"{entry.Name!}\", _{translationNameL}_d);");
            }
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void ResetDefaultInternal()");
            builder.AppendLine("            {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name![0]) + entry.Name![1..];
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"                this.{translationNameU} = _{translationNameL}_d;");
            }
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void SaveInternal(bool defaultFields, XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine($"                XSection section = manager.Sections.Add(\"{section.Name}\");");
            builder.AppendLine("                if (defaultFields)");
            builder.AppendLine("                {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"                    section.Properties.AddString(\"{entry.Name!}\", _{translationNameL}_d).Comment.SetValue(_{translationNameL}_c, true);");
            }
            builder.AppendLine("                }");
            builder.AppendLine("                else");
            builder.AppendLine("                {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name![0]) + entry.Name![1..];
                string translationNameL = char.ToLowerInvariant(entry.Name![0]) + entry.Name![1..];
                builder.AppendLine($"                    section.Properties.AddString(\"{entry.Name!}\", this.{translationNameU}).Comment.SetValue(_{translationNameL}_c, true);");
            }
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine($"        #endregion {sectionNameU}");
            return builder.ToString();
        }

        private static string FixString(string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.Trim();
                value = value.Replace("\r\n", "\n");
                value = value.Replace("\n", "\\r\\n");
                value = value.Replace("\"", "\\\"");
                return value;
            }
            return string.Empty;
        }
    }
}