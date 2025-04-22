using HonooLanguageLocalisationConverter.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace HonooLanguageLocalisationConverter.Models
{
    internal static class CShapCode
    {
        internal static string CreateCode(int codeType, SectionEntry information, IList<SectionEntry> sections)
        {
            int count = information.TranslationEntries.Count;
            foreach (var section in sections)
            {
                count += section.TranslationEntries.Count;
            }
            var builder = new StringBuilder();
            switch (codeType)
            {
                case 3:
                    builder.AppendLine("using CommunityToolkit.Mvvm.ComponentModel;");
                    builder.AppendLine("using Honoo.Configuration;");
                    builder.AppendLine("using System;");
                    builder.AppendLine("using System.Collections.Generic;");
                    builder.AppendLine("using System.IO;");
                    break;

                case 2:
                case 1:
                    builder.AppendLine("using Honoo.Configuration;");
                    builder.AppendLine("using System;");
                    builder.AppendLine("using System.Collections.Generic;");
                    builder.AppendLine("using System.ComponentModel;");
                    builder.AppendLine("using System.IO;");
                    break;

                default:
                    builder.AppendLine("using Honoo.Configuration;");
                    builder.AppendLine("using System;");
                    builder.AppendLine("using System.Collections.Generic;");
                    builder.AppendLine("using System.IO;");
                    break;
            }
            builder.AppendLine();
            builder.AppendLine("namespace HonooLanguageLocalisationConverter.ViewModels");
            builder.AppendLine("{");
            builder.AppendLine("    /// <summary>");
            switch (codeType)
            {
                case 3: builder.AppendLine("    /// Language package class. Changed-notify implemented by CommunityToolkit.Mvvm."); break;
                case 2: builder.AppendLine("    /// Language package class. Changed-notify interface implemented [.NET 6.0 later With Project setting field <![CDATA[<Nullable>enable</Nullable>]]>]."); break;
                case 1: builder.AppendLine("    /// Language package class. Changed-Notify interface implemented [.NET Framework 4.0 later]."); break;
                default: builder.AppendLine("    /// Language package class. Standard class model. Changed-Notify NOT supported."); break;
            }
            builder.AppendLine("    /// <br />Reference in viewmodel to visit or used single instance <see cref=\"Instance\"/> to visit.");
            builder.AppendLine("    /// <br />e.g.");
            builder.AppendLine("    /// <code>");
            builder.AppendLine("    /// <![CDATA[public LanguagePackage LanguagePackageReference { get; } = LanguagePackage.Instance;]]>");
            builder.AppendLine("    /// <br />");
            builder.AppendLine("    /// <![CDATA[<TextBlock Text=\"{Binding LanguagePackageReference.Main.HasNewVersion}\" />]]>");
            builder.AppendLine("    /// </code>");
            builder.AppendLine("    /// <br />Or:");
            builder.AppendLine("    /// <code>");
            builder.AppendLine("    /// <![CDATA[<TextBlock Text=\"{Binding Main.HasNewVersion, Source={x:Static vm:LanguagePackage.Instance}}\" />]]>");
            builder.AppendLine("    /// </code>");
            builder.AppendLine("    /// <br />Install nuget packages:");
            builder.AppendLine("    /// <br /><see href=\"https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager\"/>.");
            switch (codeType)
            {
                case 3: builder.AppendLine("    /// <br /><see href=\"https://www.nuget.org/packages/CommunityToolkit.Mvvm\"/>."); break;
                default: break;
            }
            builder.AppendLine("    /// </summary>");
            switch (codeType)
            {
                case 3:
                    builder.AppendLine("    public sealed partial class LanguagePackage");
                    break;

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
            builder.AppendLine("        /// Managed translation entries count.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine($"        public int Count {{ get; }}= {count};");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Language package creator.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine($"        public const string CREATOR = \"{General.Instance.Creator}\";");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Creator website.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine($"        public const string WEBSITE = \"{General.Instance.Website}\";");
            builder.AppendLine();
            builder.AppendLine("        #region Sections");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Information section.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        public __Information Information { get; } = new __Information();");
            foreach (var section in sections)
            {
                string sectionNameU = char.ToUpperInvariant(section.Name[0]) + section.Name[1..];
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
            builder.AppendLine("        /// Gets information pairs from language file.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"stream\">Language file name.</param>");
            builder.AppendLine("        /// <returns>Information pairs.</returns>");
            builder.AppendLine("        public static Dictionary<string, string> GetInformation(string fileName)");
            builder.AppendLine("        {");
            builder.AppendLine("            var pairs = new Dictionary<string, string>();");
            builder.AppendLine("            using (var manager = new XConfigManager(fileName))");
            builder.AppendLine("            {");
            builder.AppendLine("                if (manager.Sections.TryGetValue(\"Information\", out XSection section))");
            builder.AppendLine("                {");
            builder.AppendLine("                    foreach (KeyValuePair<string, XProperty> property in section.Properties)");
            builder.AppendLine("                    {");
            builder.AppendLine("                        pairs.Add(property.Key, ((XString)property.Value).GetStringValue());");
            builder.AppendLine("                    }");
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine("            return pairs;");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Gets information pairs from language stream.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"stream\">Language stream.</param>");
            builder.AppendLine("        /// <returns>Information pairs.</returns>");
            builder.AppendLine("        public static Dictionary<string, string> GetInformation(Stream stream)");
            builder.AppendLine("        {");
            builder.AppendLine("            var pairs = new Dictionary<string, string>();");
            builder.AppendLine("            using (var manager = new XConfigManager(stream))");
            builder.AppendLine("            {");
            builder.AppendLine("                if (manager.Sections.TryGetValue(\"Information\", out XSection section))");
            builder.AppendLine("                {");
            builder.AppendLine("                    foreach (KeyValuePair<string, XProperty> property in section.Properties)");
            builder.AppendLine("                    {");
            builder.AppendLine("                        pairs.Add(property.Key, ((XString)property.Value).GetStringValue());");
            builder.AppendLine("                    }");
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine("            return pairs;");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Load language file and return loaded translation entries count.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"fileName\">Language file name.</param>");
            builder.AppendLine("        /// <returns>Loaded translation entries count.</returns>");
            builder.AppendLine("        public int Load(string fileName)");
            builder.AppendLine("        {");
            builder.AppendLine("            return Load(fileName, out _);");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Load language file and return loaded translation entries count.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"fileName\">Language file name.</param>");
            builder.AppendLine("        /// <param name=\"missingNames\">Missing translation entry names when loaded.</param>");
            builder.AppendLine("        /// <returns>Loaded translation entries count.</returns>");
            builder.AppendLine("        public int Load(string fileName, out List<string> missingNames)");
            builder.AppendLine("        {");
            builder.AppendLine("            var missing = new List<string>();");
            builder.AppendLine("            int loaded = 0;");
            builder.AppendLine("            using (var manager = new XConfigManager(fileName))");
            builder.AppendLine("            {");
            builder.AppendLine("                loaded += __Information.__LoadInternal(this, manager, missing);");
            foreach (var section in sections)
            {
                string sectionNameU = char.ToUpperInvariant(section.Name[0]) + section.Name[1..];
                builder.AppendLine($"                loaded += __{sectionNameU}.__LoadInternal(this, manager, missing);");
            }
            builder.AppendLine("            }");
            builder.AppendLine("            missingNames = missing;");
            builder.AppendLine("            return loaded;");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Load language stream and return loaded translation entries count.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"stream\">Language stream.</param>");
            builder.AppendLine("        /// <returns>Loaded translation entries count.</returns>");
            builder.AppendLine("        public int Load(Stream stream)");
            builder.AppendLine("        {");
            builder.AppendLine("            return Load(stream, out _);");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Load language stream and return loaded translation entries count.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"stream\">Language stream.</param>");
            builder.AppendLine("        /// <param name=\"missingNames\">Missing translation entry names when loaded.</param>");
            builder.AppendLine("        /// <returns>Loaded translation entries count.</returns>");
            builder.AppendLine("        public int Load(Stream stream, out List<string> missingNames)");
            builder.AppendLine("        {");
            builder.AppendLine("            var missing = new List<string>();");
            builder.AppendLine("            int loaded = 0;");
            builder.AppendLine("            using (var manager = new XConfigManager(stream))");
            builder.AppendLine("            {");
            builder.AppendLine("                loaded += __Information.__LoadInternal(this, manager, missing);");
            foreach (var section in sections)
            {
                string sectionNameU = char.ToUpperInvariant(section.Name[0]) + section.Name[1..];
                builder.AppendLine($"                loaded += __{sectionNameU}.__LoadInternal(this, manager, missing);");
            }
            builder.AppendLine("            }");
            builder.AppendLine("            missingNames = missing;");
            builder.AppendLine("            return loaded;");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Reset all translation entries to default values.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        public void ResetDefault()");
            builder.AppendLine("        {");
            builder.AppendLine("            __Information.__ResetDefaultInternal(this);");
            foreach (var section in sections)
            {
                string sectionNameU = char.ToUpperInvariant(section.Name[0]) + section.Name[1..];
                builder.AppendLine($"            __{sectionNameU}.__ResetDefaultInternal(this);");
            }
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Save to language file.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"defaultField\">Select current fields or default fields.</param>");
            builder.AppendLine("        /// <param name=\"fileName\">Language file name.</param>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE0063:Use simple 'using' statement\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public void Save(bool defaultField, string fileName)");
            builder.AppendLine("        {");
            builder.AppendLine("            using (var manager = new XConfigManager())");
            builder.AppendLine("            {");
            builder.AppendLine("                manager.Default.Properties.AddString(\"Creator\", CREATOR);");
            builder.AppendLine("                manager.Default.Properties.AddString(\"Website\", WEBSITE);");
            builder.AppendLine("                manager.Default.Properties.AddString(\"CreatedTime\", DateTime.Now.ToString(\"R\"));");
            builder.AppendLine("                __Information.__SaveInternal(this, defaultField, manager);");
            foreach (var section in sections)
            {
                string sectionNameU = char.ToUpperInvariant(section.Name[0]) + section.Name[1..];
                builder.AppendLine($"                __{sectionNameU}.__SaveInternal(this, defaultField, manager);");
            }
            builder.AppendLine("                manager.Save(fileName);");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Save to language stream.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"defaultField\">Select current fields or default fields.</param>");
            builder.AppendLine("        /// <param name=\"stream\">Language stream.</param>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE0063:Use simple 'using' statement\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public void Save(bool defaultField, Stream stream)");
            builder.AppendLine("        {");
            builder.AppendLine("            using (var manager = new XConfigManager())");
            builder.AppendLine("            {");
            builder.AppendLine("                manager.Default.Properties.AddString(\"Creator\", CREATOR);");
            builder.AppendLine("                manager.Default.Properties.AddString(\"Website\", WEBSITE);");
            builder.AppendLine("                manager.Default.Properties.AddString(\"CreatedTime\", DateTime.Now.ToString(\"R\"));");
            builder.AppendLine("                __Information.__SaveInternal(this, defaultField, manager);");
            foreach (var section in sections)
            {
                string sectionNameU = char.ToUpperInvariant(section.Name[0]) + section.Name[1..];
                builder.AppendLine($"                __{sectionNameU}.__SaveInternal(this, defaultField, manager);");
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

        #region Block

        private static string CreateCommunityToolkitSectionCode(SectionEntry section)
        {
            var builder = new StringBuilder();
            string sectionNameU = char.ToUpperInvariant(section.Name[0]) + section.Name[1..];
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
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine($"            private const string _{translationNameL}_c = \"{FixValueString(entry.Comment)}\";");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Comments");
            builder.AppendLine();
            builder.AppendLine("            #region Default");
            builder.AppendLine();
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine($"            private const string _{translationNameL}_d = \"{FixValueString(entry.Value)}\";");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Default");
            builder.AppendLine();
            builder.AppendLine("            #region Members");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine();
                builder.AppendLine("            /// <summary>");
                builder.AppendLine($"            /// {FixSummaryString(entry.Comment)}");
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
            builder.AppendLine(CreateSectionMethods(section));
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine($"        #endregion {sectionNameU}");
            return builder.ToString();
        }

        private static string CreateNotifyBasicSectionCode(SectionEntry section, bool nullSign)
        {
            var builder = new StringBuilder();
            string sectionNameU = char.ToUpperInvariant(section.Name[0]) + section.Name[1..];
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
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine($"            private const string _{translationNameL}_c = \"{FixValueString(entry.Comment)}\";");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Comments");
            builder.AppendLine();
            builder.AppendLine("            #region Default");
            builder.AppendLine();
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine($"            private const string _{translationNameL}_d = \"{FixValueString(entry.Value)}\";");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Default");
            builder.AppendLine();
            builder.AppendLine("            #region Members");
            builder.AppendLine();
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine($"            private string _{translationNameL} = _{translationNameL}_d;");
            }
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name[0]) + entry.Name[1..];
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine();
                builder.AppendLine("            /// <summary>");
                builder.AppendLine($"            /// {FixSummaryString(entry.Comment)}");
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
            builder.AppendLine(CreateSectionMethods(section));
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine($"        #endregion {sectionNameU}");
            return builder.ToString();
        }

        private static string CreateStandardSectionCode(SectionEntry section)
        {
            var builder = new StringBuilder();
            string sectionNameU = char.ToUpperInvariant(section.Name[0]) + section.Name[1..];
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
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine($"            private const string _{translationNameL}_c = \"{FixValueString(entry.Comment)}\";");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Comments");
            builder.AppendLine();
            builder.AppendLine("            #region Default");
            builder.AppendLine();
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine($"            private const string _{translationNameL}_d = \"{FixValueString(entry.Value)}\";");
            }
            builder.AppendLine();
            builder.AppendLine("            #endregion Default");
            builder.AppendLine();
            builder.AppendLine("            #region Members");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name[0]) + entry.Name[1..];
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine();
                builder.AppendLine("            /// <summary>");
                builder.AppendLine($"            /// {FixSummaryString(entry.Comment)}");
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
            builder.AppendLine(CreateSectionMethods(section));
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine($"        #endregion {sectionNameU}");
            return builder.ToString();
        }

        #endregion Block

        private static string CreateSectionMethods(SectionEntry section)
        {
            var builder = new StringBuilder();
            string sectionNameU = char.ToUpperInvariant(section.Name[0]) + section.Name[1..];
            builder.AppendLine("            internal static int __LoadInternal(LanguagePackage instance,  XConfigManager manager, List<string> missing)");
            builder.AppendLine("            {");
            builder.AppendLine($"                return instance.{sectionNameU}.__LoadInternal(manager, missing);");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal static void __ResetDefaultInternal(LanguagePackage instance)");
            builder.AppendLine("            {");
            builder.AppendLine($"                instance.{sectionNameU}.__ResetDefaultInternal();");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal static void __SaveInternal(LanguagePackage instance, bool defaultField, XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine($"                instance.{sectionNameU}.__SaveInternal(defaultField, manager);");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            private static string __GetTranslationEntryInternal(XSection section, string name, string defaultValue, List<string> missing, ref int loaded)");
            builder.AppendLine("            {");
            builder.AppendLine("                if (section.Properties.TryGetStringValue(name, out string value))");
            builder.AppendLine("                {");
            builder.AppendLine("                    loaded++;");
            builder.AppendLine("                    return value;");
            builder.AppendLine("                }");
            builder.AppendLine("                else");
            builder.AppendLine("                {");
            builder.AppendLine("                    missing.Add(name);");
            builder.AppendLine("                    return defaultValue;");
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            private int __LoadInternal(XConfigManager manager, List<string> missing)");
            builder.AppendLine("            {");
            builder.AppendLine($"                if (manager.Sections.TryGetValue(\"{section.Name}\", out XSection section))");
            builder.AppendLine("                {");
            builder.AppendLine("                    int loaded = 0;");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name[0]) + entry.Name[1..];
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine($"                    this.{translationNameU} = __GetTranslationEntryInternal(section, \"{entry.Name}\", _{translationNameL}_d, missing, ref loaded);");
            }
            builder.AppendLine("                    return loaded;");
            builder.AppendLine("                }");
            builder.AppendLine("                else");
            builder.AppendLine("                {");
            foreach (var entry in section.TranslationEntries)
            {
                builder.AppendLine($"                    missing.Add(\"{entry.Name}\");");
            }
            builder.AppendLine("                    return 0;");
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            private void __ResetDefaultInternal()");
            builder.AppendLine("            {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name[0]) + entry.Name[1..];
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine($"                this.{translationNameU} = _{translationNameL}_d;");
            }
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            private void __SaveInternal(bool defaultField, XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine($"                XSection section = manager.Sections.Add(\"{section.Name}\");");
            builder.AppendLine("                if (defaultField)");
            builder.AppendLine("                {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine($"                    section.Properties.AddString(\"{entry.Name}\", _{translationNameL}_d).Comment.SetValue(_{translationNameL}_c, true);");
            }
            builder.AppendLine("                }");
            builder.AppendLine("                else");
            builder.AppendLine("                {");
            foreach (var entry in section.TranslationEntries)
            {
                string translationNameU = char.ToUpperInvariant(entry.Name[0]) + entry.Name[1..];
                string translationNameL = char.ToLowerInvariant(entry.Name[0]) + entry.Name[1..];
                builder.AppendLine($"                    section.Properties.AddString(\"{entry.Name}\", this.{translationNameU}).Comment.SetValue(_{translationNameL}_c, true);");
            }
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            return builder.ToString();
        }

        private static string FixSummaryString(string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.Trim();
                value = value.Replace("\r\n", "\n");
                value = value.Replace("\n", "<br />");
                return value;
            }
            return string.Empty;
        }

        private static string FixValueString(string? value)
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