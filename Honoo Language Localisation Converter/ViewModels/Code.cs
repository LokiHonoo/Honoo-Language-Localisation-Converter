﻿using System.Collections.Generic;
using System.Text;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    internal static class Code
    {
        internal static string CreateMVVM(string? appName, string? appVer, string? langName, string? langVer, string? author, string? url, string? remarks, IList<SectionEntry> sections)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using CommunityToolkit.Mvvm.ComponentModel;");
            builder.AppendLine("using Honoo.Configuration;");
            builder.AppendLine("using System.IO;");
            builder.AppendLine();
            builder.AppendLine("namespace HonooLanguageLocalisationConverter");
            builder.AppendLine("{");
            builder.AppendLine("    /// <summary>");
            builder.AppendLine($"    /// Language package class. MVVM structure for lib - CommunityToolkit.Mvvm.");
            builder.AppendLine("    /// Using single instance <see cref=\"Instance\"/> to visit.");
            builder.AppendLine("    /// <br />Install nuget package:");
            builder.AppendLine("    /// <br /><see href=\"https://www.nuget.org/packages/CommunityToolkit.Mvvm\"/>.");
            builder.AppendLine("    /// <br /><see href=\"https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager\"/>.");
            builder.AppendLine("    /// </summary>");
            builder.AppendLine("    public sealed partial class LanguagePackage : ObservableObject");
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
            builder.AppendLine("        /// Informartion section.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        public __Informartion Informartion { get; } = new __Informartion();");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        /// {sNameU} section.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public __{sNameU} {sNameU} {{ get; }} = new __{sNameU}();");
            }
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
            builder.AppendLine("                this.Informartion.Load(manager);");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sNameU}.Load(manager);");
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
            builder.AppendLine("                this.Informartion.Load(manager);");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sNameU}.Load(manager);");
            }
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Reset all properties to default values.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        public void ResetDefault()");
            builder.AppendLine("        {");
            builder.AppendLine("            this.Informartion.ResetDefault();");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"            this.{sNameU}.ResetDefault();");
            }
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Save to language file.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"defaultField\">Select current field or default field.</param>");
            builder.AppendLine("        /// <param name=\"fileName\">Language file name.</param>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE0063:Use simple 'using' statement\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public void Save(bool defaultField, string fileName)");
            builder.AppendLine("        {");
            builder.AppendLine("            using (var manager = new XConfigManager())");
            builder.AppendLine("            {");
            builder.AppendLine($"                this.Informartion.Save(defaultField, manager);");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sNameU}.Save(defaultField, manager);");
            }
            builder.AppendLine("                manager.Save(fileName);");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Save to language stream.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"defaultField\">Select current field or default field.</param>");
            builder.AppendLine("        /// <param name=\"stream\">Language stream.</param>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE0063:Use simple 'using' statement\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public void Save(bool defaultField, Stream stream)");
            builder.AppendLine("        {");
            builder.AppendLine("            using (var manager = new XConfigManager())");
            builder.AppendLine("            {");
            builder.AppendLine($"                this.Informartion.Save(defaultField, manager);");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sNameU}.Save(defaultField, manager);");
            }
            builder.AppendLine("                manager.Save(stream);");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Informartion section.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE1006:Naming Styles\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public sealed partial class __Informartion : ObservableObject");
            builder.AppendLine("        {");
            builder.AppendLine("            #region Default");
            builder.AppendLine();
            builder.AppendLine($"            private const string _appName_d = \"{FixString(appName)}\";");
            builder.AppendLine($"            private const string _appVer_d = \"{FixString(appVer)}\";");
            builder.AppendLine($"            private const string _author_d = \"{FixString(author)}\";");
            builder.AppendLine($"            private const string _langName_d = \"{FixString(langName)}\";");
            builder.AppendLine($"            private const string _langVer_d = \"{FixString(langVer)}\";");
            builder.AppendLine($"            private const string _remarks_d = \"{FixString(remarks)}\";");
            builder.AppendLine($"            private const string _url_d = \"{FixString(url)}\";");
            builder.AppendLine();
            builder.AppendLine("            #endregion Default");
            builder.AppendLine();
            builder.AppendLine("            #region Members");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Application name.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            [ObservableProperty]");
            builder.AppendLine("            private string _appName = _appName_d;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Application version.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            [ObservableProperty]");
            builder.AppendLine("            private string _appVer = _appVer_d;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Author name.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            [ObservableProperty]");
            builder.AppendLine("            private string _author = _author_d;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Language name as \"en-US\".");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            [ObservableProperty]");
            builder.AppendLine("            private string _langName = _langName_d;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Language file version.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            [ObservableProperty]");
            builder.AppendLine("            private string _langVer = _langVer_d;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Remarks.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            [ObservableProperty]");
            builder.AppendLine("            private string _remarks = _remarks_d;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Author / file related url.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            [ObservableProperty]");
            builder.AppendLine("            private string _url = _url_d;");
            builder.AppendLine();
            builder.AppendLine("            #endregion Members");
            builder.AppendLine();
            builder.AppendLine("            internal __Informartion()");
            builder.AppendLine("            {");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void Load(XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine("                this.AppName = manager.Default.Properties.GetStringValue(\"AppName\", _appName_d);");
            builder.AppendLine("                this.AppVer = manager.Default.Properties.GetStringValue(\"AppVer\", _appVer_d);");
            builder.AppendLine("                this.LangName = manager.Default.Properties.GetStringValue(\"LangName\", _langName_d);");
            builder.AppendLine("                this.LangVer = manager.Default.Properties.GetStringValue(\"LangVer\", _langVer_d);");
            builder.AppendLine("                this.Author = manager.Default.Properties.GetStringValue(\"Author\", _author_d);");
            builder.AppendLine("                this.Url = manager.Default.Properties.GetStringValue(\"Url\", _url_d);");
            builder.AppendLine("                this.Remarks = manager.Default.Properties.GetStringValue(\"Remarks\", _remarks_d);");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void ResetDefault()");
            builder.AppendLine("            {");
            builder.AppendLine("                this.AppName = _appName_d;");
            builder.AppendLine("                this.AppVer = _appVer_d;");
            builder.AppendLine("                this.LangName = _langName_d;");
            builder.AppendLine("                this.LangVer = _langVer_d;");
            builder.AppendLine("                this.Author = _author_d;");
            builder.AppendLine("                this.Url = _url_d;");
            builder.AppendLine("                this.Remarks = _remarks_d;");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void Save(bool defaultField, XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine("                if (defaultField)");
            builder.AppendLine("                {");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"AppName\", _appName_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"AppVer\", _appVer_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"LangName\", _langName_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"LangVer\", _langVer_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Author\", _author_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Url\", _url_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Remarks\", _remarks_d);");
            builder.AppendLine("                }");
            builder.AppendLine("                else");
            builder.AppendLine("                {");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"AppName\", this.AppName);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"AppVer\", this.AppVer);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"LangName\", this.LangName);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"LangVer\", this.LangVer);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Author\", this.Author);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Url\", this.Url);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Remarks\", this.Remarks);");
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        /// {sNameU} section.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE1006:Naming Styles\", Justification = \"<Pending>\")]");
                builder.AppendLine($"        public sealed partial class __{sNameU} : ObservableObject");
                builder.AppendLine("        {");
                builder.AppendLine("            #region Comments");
                builder.AppendLine();
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"            private const string _{eKeyL}_c = \"{FixString(entry.Comment)}\";");
                }
                builder.AppendLine();
                builder.AppendLine("            #endregion Comments");
                builder.AppendLine();
                builder.AppendLine("            #region Default");
                builder.AppendLine();
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"            private const string _{eKeyL}_d = \"{FixString(entry.Value)}\";");
                }
                builder.AppendLine();
                builder.AppendLine("            #endregion Default");
                builder.AppendLine();
                builder.AppendLine("            #region Members");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine();
                    builder.AppendLine("            /// <summary>");
                    builder.AppendLine($"            /// {entry.Comment}");
                    builder.AppendLine("            /// </summary>");
                    builder.AppendLine("            [ObservableProperty]");
                    builder.AppendLine($"            private string _{eKeyL} = _{eKeyL}_d;");
                }
                builder.AppendLine();
                builder.AppendLine("            #endregion Members");
                builder.AppendLine();
                builder.AppendLine($"            internal __{sNameU}()");
                builder.AppendLine("            {");
                builder.AppendLine("            }");
                builder.AppendLine();
                builder.AppendLine("            internal void Load(XConfigManager manager)");
                builder.AppendLine("            {");
                builder.AppendLine($"                if (manager.Sections.TryGetValue(\"{section.Name}\", out XSection section))");
                builder.AppendLine("                {");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyU = char.ToUpperInvariant(entry.Key![0]) + entry.Key![1..];
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"                    this.{eKeyU} = section.Properties.GetStringValue(\"{entry.Key!}\", _{eKeyL}_d);");
                }
                builder.AppendLine("                }");
                builder.AppendLine("            }");
                builder.AppendLine();
                builder.AppendLine("            internal void ResetDefault()");
                builder.AppendLine("            {");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyU = char.ToUpperInvariant(entry.Key![0]) + entry.Key![1..];
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"                this.{eKeyU} = _{eKeyL}_d;");
                }
                builder.AppendLine("            }");
                builder.AppendLine();
                builder.AppendLine("            internal void Save(bool defaultField, XConfigManager manager)");
                builder.AppendLine("            {");
                builder.AppendLine($"                XSection section = manager.Sections.Add(\"{section.Name}\");");
                builder.AppendLine("                if (defaultField)");
                builder.AppendLine("                {");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"                    section.Properties.AddString(\"{entry.Key!}\", _{eKeyL}_d).Comment.SetValue(_{eKeyL}_c);");
                }
                builder.AppendLine("                }");
                builder.AppendLine("                else");
                builder.AppendLine("                {");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyU = char.ToUpperInvariant(entry.Key![0]) + entry.Key![1..];
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"                    section.Properties.AddString(\"{entry.Key!}\", this.{eKeyU}).Comment.SetValue(_{eKeyL}_c);");
                }
                builder.AppendLine("                }");
                builder.AppendLine("            }");
                builder.AppendLine("        }");
            }
            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }

        internal static string CreateStandard(string? appName, string? appVer, string? langName, string? langVer, string? author, string? url, string? remarks, IList<SectionEntry> sections)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using Honoo.Configuration;");
            builder.AppendLine("using System.IO;");
            builder.AppendLine();
            builder.AppendLine("namespace HonooLanguageLocalisationConverter");
            builder.AppendLine("{");
            builder.AppendLine("    /// <summary>");
            builder.AppendLine($"    /// Language package class. Standard class model for all app type.");
            builder.AppendLine("    /// Using single instance <see cref=\"Instance\"/> to visit.");
            builder.AppendLine("    /// <br />Install nuget package: <see href=\"https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager\"/>.");
            builder.AppendLine("    /// </summary>");
            builder.AppendLine("    public sealed class LanguagePackage");
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
            builder.AppendLine("        /// Informartion section.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        public __Informartion Informartion { get; } = new __Informartion();");

            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        /// {sNameU} section.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public __{sNameU} {sNameU} {{ get; }} = new __{sNameU}();");
            }
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
            builder.AppendLine("                this.Informartion.Load(manager);");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sNameU}.Load(manager);");
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
            builder.AppendLine("                this.Informartion.Load(manager);");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sNameU}.Load(manager);");
            }
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Reset all properties to default values.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        public void ResetDefault()");
            builder.AppendLine("        {");
            builder.AppendLine("            this.Informartion.ResetDefault();");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"            this.{sNameU}.ResetDefault();");
            }
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Save to language file.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"defaultField\">Select current field or default field.</param>");
            builder.AppendLine("        /// <param name=\"fileName\">Language file name.</param>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE0063:Use simple 'using' statement\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public void Save(bool defaultField, string fileName)");
            builder.AppendLine("        {");
            builder.AppendLine("            using (var manager = new XConfigManager())");
            builder.AppendLine("            {");
            builder.AppendLine($"                this.Informartion.Save(defaultField, manager);");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sNameU}.Save(defaultField, manager);");
            }
            builder.AppendLine("                manager.Save(fileName);");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Save to language stream.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"defaultField\">Select current field or default field.</param>");
            builder.AppendLine("        /// <param name=\"stream\">Language stream.</param>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE0063:Use simple 'using' statement\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public void Save(bool defaultField, Stream stream)");
            builder.AppendLine("        {");
            builder.AppendLine("            using (var manager = new XConfigManager())");
            builder.AppendLine("            {");
            builder.AppendLine($"                this.Informartion.Save(defaultField, manager);");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sNameU}.Save(defaultField, manager);");
            }
            builder.AppendLine("                manager.Save(stream);");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Informartion section.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE1006:Naming Styles\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public sealed class __Informartion");
            builder.AppendLine("        {");
            builder.AppendLine("            #region Default");
            builder.AppendLine();
            builder.AppendLine($"            private const string _appName_d = \"{FixString(appName)}\";");
            builder.AppendLine($"            private const string _appVer_d = \"{FixString(appVer)}\";");
            builder.AppendLine($"            private const string _author_d = \"{FixString(author)}\";");
            builder.AppendLine($"            private const string _langName_d = \"{FixString(langName)}\";");
            builder.AppendLine($"            private const string _langVer_d = \"{FixString(langVer)}\";");
            builder.AppendLine($"            private const string _remarks_d = \"{FixString(remarks)}\";");
            builder.AppendLine($"            private const string _url_d = \"{FixString(url)}\";");
            builder.AppendLine();
            builder.AppendLine("            #endregion Default");
            builder.AppendLine();
            builder.AppendLine("            #region Members");
            builder.AppendLine();
            builder.AppendLine("            private string _appName = _appName_d;");
            builder.AppendLine("            private string _appVer = _appVer_d;");
            builder.AppendLine("            private string _author = _author_d;");
            builder.AppendLine("            private string _langName = _langName_d;");
            builder.AppendLine("            private string _langVer = _langVer_d;");
            builder.AppendLine("            private string _remarks = _remarks_d;");
            builder.AppendLine("            private string _url = _url_d;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Application name.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string AppName => _appName;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Application version.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string AppVer => _appVer;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Author name.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string Author => _author;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Language name as \"en-US\".");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string LangName => _langName;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Language file version.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string LangVer => _langVer;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Remarks.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string Remarks => _remarks;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Author / file related url.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string Url => _url;");
            builder.AppendLine();
            builder.AppendLine("            #endregion Members");
            builder.AppendLine();
            builder.AppendLine("            internal __Informartion()");
            builder.AppendLine("            {");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void Load(XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine("                _appName = manager.Default.Properties.GetStringValue(\"AppName\", _appName_d);");
            builder.AppendLine("                _appVer = manager.Default.Properties.GetStringValue(\"AppVer\", _appVer_d);");
            builder.AppendLine("                _langName = manager.Default.Properties.GetStringValue(\"LangName\", _langName_d);");
            builder.AppendLine("                _langVer = manager.Default.Properties.GetStringValue(\"LangVer\", _langVer_d);");
            builder.AppendLine("                _author = manager.Default.Properties.GetStringValue(\"Author\", _author_d);");
            builder.AppendLine("                _url = manager.Default.Properties.GetStringValue(\"Url\", _url_d);");
            builder.AppendLine("                _remarks = manager.Default.Properties.GetStringValue(\"Remarks\", _remarks_d);");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void ResetDefault()");
            builder.AppendLine("            {");
            builder.AppendLine("                _appName = _appName_d;");
            builder.AppendLine("                _appVer = _appVer_d;");
            builder.AppendLine("                _langName = _langName_d;");
            builder.AppendLine("                _langVer = _langVer_d;");
            builder.AppendLine("                _author = _author_d;");
            builder.AppendLine("                _url = _url_d;");
            builder.AppendLine("                _remarks = _remarks_d;");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void Save(bool defaultField, XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine("                if (defaultField)");
            builder.AppendLine("                {");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"AppName\", _appName_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"AppVer\", _appVer_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"LangName\", _langName_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"LangVer\", _langVer_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Author\", _author_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Url\", _url_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Remarks\", _remarks_d);");
            builder.AppendLine("                }");
            builder.AppendLine("                else");
            builder.AppendLine("                {");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"AppName\", _appName);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"AppVer\", _appVer);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"LangName\", _langName);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"LangVer\", _langVer);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Author\", _author);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Url\", _url);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Remarks\", _remarks);");
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        /// {sNameU} section.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE1006:Naming Styles\", Justification = \"<Pending>\")]");
                builder.AppendLine($"        public sealed class __{sNameU}");
                builder.AppendLine("        {");
                builder.AppendLine("            #region Comments");
                builder.AppendLine();
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"            private const string _{eKeyL}_c = \"{FixString(entry.Comment)}\";");
                }
                builder.AppendLine();
                builder.AppendLine("            #endregion Comments");
                builder.AppendLine();
                builder.AppendLine("            #region Default");
                builder.AppendLine();
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"            private const string _{eKeyL}_d = \"{FixString(entry.Value)}\";");
                }
                builder.AppendLine();
                builder.AppendLine("            #endregion Default");
                builder.AppendLine();
                builder.AppendLine("            #region Members");
                builder.AppendLine();
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"            private string _{eKeyL} = _{eKeyL}_d;");
                }
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyU = char.ToUpperInvariant(entry.Key![0]) + entry.Key![1..];
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine();
                    builder.AppendLine("            /// <summary>");
                    builder.AppendLine($"            /// {entry.Comment}");
                    builder.AppendLine("            /// </summary>");
                    builder.AppendLine($"            public string {eKeyU} => _{eKeyL};");
                }
                builder.AppendLine();
                builder.AppendLine("            #endregion Members");
                builder.AppendLine();
                builder.AppendLine($"            internal __{sNameU}()");
                builder.AppendLine("            {");
                builder.AppendLine("            }");
                builder.AppendLine();
                builder.AppendLine("            internal void Load(XConfigManager manager)");
                builder.AppendLine("            {");
                builder.AppendLine($"                if (manager.Sections.TryGetValue(\"{section.Name}\", out XSection section))");
                builder.AppendLine("                {");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"                    _{eKeyL} = section.Properties.GetStringValue(\"{entry.Key!}\", _{eKeyL}_d);");
                }
                builder.AppendLine("                }");
                builder.AppendLine("            }");
                builder.AppendLine();
                builder.AppendLine("            internal void ResetDefault()");
                builder.AppendLine("            {");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"                _{eKeyL} = _{eKeyL}_d;");
                }
                builder.AppendLine("            }");
                builder.AppendLine();
                builder.AppendLine("            internal void Save(bool defaultField, XConfigManager manager)");
                builder.AppendLine("            {");
                builder.AppendLine($"                XSection section = manager.Sections.Add(\"{section.Name}\");");
                builder.AppendLine("                if (defaultField)");
                builder.AppendLine("                {");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"                    section.Properties.AddString(\"{entry.Key!}\", _{eKeyL}_d).Comment.SetValue(_{eKeyL}_c);");
                }
                builder.AppendLine("                }");
                builder.AppendLine("                else");
                builder.AppendLine("                {");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"                    section.Properties.AddString(\"{entry.Key!}\", _{eKeyL}).Comment.SetValue(_{eKeyL}_c);");
                }
                builder.AppendLine("                }");
                builder.AppendLine("            }");
                builder.AppendLine("        }");
            }
            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }

        internal static string CreateWPF(string? appName, string? appVer, string? langName, string? langVer, string? author, string? url, string? remarks, IList<SectionEntry> sections, bool nullSign)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using Honoo.Configuration;");
            builder.AppendLine("using System.ComponentModel;");
            builder.AppendLine("using System.IO;");
            builder.AppendLine();
            builder.AppendLine("namespace HonooLanguageLocalisationConverter");
            builder.AppendLine("{");
            builder.AppendLine("    /// <summary>");
            builder.AppendLine($"    /// Language package class. Using in WPF basic class {(nullSign ? "[.NET 6.0+][With Project field <Nullable>enable</Nullable>]" : "[.NET Framework 4.0+]")}.");
            builder.AppendLine("    /// Using single instance <see cref=\"Instance\"/> to visit.");
            builder.AppendLine("    /// <br />Install nuget package: <see href=\"https://www.nuget.org/packages/Honoo.Configuration.ConfigurationManager\"/>.");
            builder.AppendLine("    /// </summary>");
            builder.AppendLine("    public sealed class LanguagePackage");
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
            builder.AppendLine("        /// Informartion section.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        public __Informartion Informartion { get; } = new __Informartion();");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        /// {sNameU} section.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public __{sNameU} {sNameU} {{ get; }} = new __{sNameU}();");
            }
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
            builder.AppendLine("                this.Informartion.Load(manager);");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sNameU}.Load(manager);");
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
            builder.AppendLine("                this.Informartion.Load(manager);");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sNameU}.Load(manager);");
            }
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Reset all properties to default values.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        public void ResetDefault()");
            builder.AppendLine("        {");
            builder.AppendLine("            this.Informartion.ResetDefault();");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"            this.{sNameU}.ResetDefault();");
            }
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Save to language file.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"defaultField\">Select current field or default field.</param>");
            builder.AppendLine("        /// <param name=\"fileName\">Language file name.</param>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE0063:Use simple 'using' statement\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public void Save(bool defaultField, string fileName)");
            builder.AppendLine("        {");
            builder.AppendLine("            using (var manager = new XConfigManager())");
            builder.AppendLine("            {");
            builder.AppendLine($"                this.Informartion.Save(defaultField, manager);");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sNameU}.Save(defaultField, manager);");
            }
            builder.AppendLine("                manager.Save(fileName);");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Save to language stream.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        /// <param name=\"defaultField\">Select current field or default field.</param>");
            builder.AppendLine("        /// <param name=\"stream\">Language stream.</param>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE0063:Use simple 'using' statement\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public void Save(bool defaultField, Stream stream)");
            builder.AppendLine("        {");
            builder.AppendLine("            using (var manager = new XConfigManager())");
            builder.AppendLine("            {");
            builder.AppendLine($"                this.Informartion.Save(defaultField, manager);");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine($"                this.{sNameU}.Save(defaultField, manager);");
            }
            builder.AppendLine("                manager.Save(stream);");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        /// <summary>");
            builder.AppendLine("        /// Informartion section.");
            builder.AppendLine("        /// </summary>");
            builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE1006:Naming Styles\", Justification = \"<Pending>\")]");
            builder.AppendLine("        public sealed class __Informartion : INotifyPropertyChanging, INotifyPropertyChanged");
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
            builder.AppendLine("            #region Default");
            builder.AppendLine();
            builder.AppendLine($"            private const string _appName_d = \"{FixString(appName)}\";");
            builder.AppendLine($"            private const string _appVer_d = \"{FixString(appVer)}\";");
            builder.AppendLine($"            private const string _author_d = \"{FixString(author)}\";");
            builder.AppendLine($"            private const string _langName_d = \"{FixString(langName)}\";");
            builder.AppendLine($"            private const string _langVer_d = \"{FixString(langVer)}\";");
            builder.AppendLine($"            private const string _remarks_d = \"{FixString(remarks)}\";");
            builder.AppendLine($"            private const string _url_d = \"{FixString(url)}\";");
            builder.AppendLine();
            builder.AppendLine("            #endregion Default");
            builder.AppendLine();
            builder.AppendLine("            #region Members");
            builder.AppendLine();
            builder.AppendLine("            private string _appName = _appName_d;");
            builder.AppendLine("            private string _appVer = _appVer_d;");
            builder.AppendLine("            private string _author = _author_d;");
            builder.AppendLine("            private string _langName = _langName_d;");
            builder.AppendLine("            private string _langVer = _langVer_d;");
            builder.AppendLine("            private string _remarks = _remarks_d;");
            builder.AppendLine("            private string _url = _url_d;");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Application name.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string AppName { get { return _appName; } set { OnPropertyChanging(nameof(this.AppName)); _appName = value; OnPropertyChanged(nameof(this.AppName)); } }");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Application version.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string AppVer { get { return _appVer; } set { OnPropertyChanging(nameof(this.AppVer)); _appVer = value; OnPropertyChanged(nameof(this.AppVer)); } }");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Author name.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string Author { get { return _author; } set { OnPropertyChanging(nameof(this.Author)); _author = value; OnPropertyChanged(nameof(this.Author)); } }");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Language name as \"en-US\".");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string LangName { get { return _langName; } set { OnPropertyChanging(nameof(this.LangName)); _langName = value; OnPropertyChanged(nameof(this.LangName)); } }");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Language file version.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string LangVer { get { return _langVer; } set { OnPropertyChanging(nameof(this.LangVer)); _langVer = value; OnPropertyChanged(nameof(this.LangVer)); } }");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Remarks.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string Remarks { get { return _remarks; } set { OnPropertyChanging(nameof(this.Remarks)); _remarks = value; OnPropertyChanged(nameof(this.Remarks)); } }");
            builder.AppendLine();
            builder.AppendLine("            /// <summary>");
            builder.AppendLine("            /// Author / file related url.");
            builder.AppendLine("            /// </summary>");
            builder.AppendLine("            public string Url { get { return _url; } set { OnPropertyChanging(nameof(this.Url)); _url = value; OnPropertyChanged(nameof(this.Url)); } }");
            builder.AppendLine();
            builder.AppendLine("            #endregion Members");
            builder.AppendLine();
            builder.AppendLine("            internal __Informartion()");
            builder.AppendLine("            {");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void Load(XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine("                this.AppName = manager.Default.Properties.GetStringValue(\"AppName\", _appName_d);");
            builder.AppendLine("                this.AppVer = manager.Default.Properties.GetStringValue(\"AppVer\", _appVer_d);");
            builder.AppendLine("                this.LangName = manager.Default.Properties.GetStringValue(\"LangName\", _langName_d);");
            builder.AppendLine("                this.LangVer = manager.Default.Properties.GetStringValue(\"LangVer\", _langVer_d);");
            builder.AppendLine("                this.Author = manager.Default.Properties.GetStringValue(\"Author\", _author_d);");
            builder.AppendLine("                this.Url = manager.Default.Properties.GetStringValue(\"Url\", _url_d);");
            builder.AppendLine("                this.Remarks = manager.Default.Properties.GetStringValue(\"Remarks\", _remarks_d);");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void ResetDefault()");
            builder.AppendLine("            {");
            builder.AppendLine("                this.AppName = _appName_d;");
            builder.AppendLine("                this.AppVer = _appVer_d;");
            builder.AppendLine("                this.LangName = _langName_d;");
            builder.AppendLine("                this.LangVer = _langVer_d;");
            builder.AppendLine("                this.Author = _author_d;");
            builder.AppendLine("                this.Url = _url_d;");
            builder.AppendLine("                this.Remarks = _remarks_d;");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.AppendLine("            internal void Save(bool defaultField, XConfigManager manager)");
            builder.AppendLine("            {");
            builder.AppendLine("                if (defaultField)");
            builder.AppendLine("                {");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"AppName\", _appName_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"AppVer\", _appVer_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"LangName\", _langName_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"LangVer\", _langVer_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Author\", _author_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Url\", _url_d);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Remarks\", _remarks_d);");
            builder.AppendLine("                }");
            builder.AppendLine("                else");
            builder.AppendLine("                {");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"AppName\", this.AppName);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"AppVer\", this.AppVer);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"LangName\", this.LangName);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"LangVer\", this.LangVer);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Author\", this.Author);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Url\", this.Url);");
            builder.AppendLine("                    manager.Default.Properties.AddString(\"Remarks\", this.Remarks);");
            builder.AppendLine("                }");
            builder.AppendLine("            }");
            builder.AppendLine("        }");
            foreach (var section in sections)
            {
                string sNameU = char.ToUpperInvariant(section.Name![0]) + section.Name![1..];
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        /// {sNameU} section.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine("        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Style\", \"IDE1006:Naming Styles\", Justification = \"<Pending>\")]");
                builder.AppendLine($"        public sealed class __{sNameU} : INotifyPropertyChanging, INotifyPropertyChanged");
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
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"            private const string _{eKeyL}_c = \"{FixString(entry.Comment)}\";");
                }
                builder.AppendLine();
                builder.AppendLine("            #endregion Comments");
                builder.AppendLine();
                builder.AppendLine("            #region Default");
                builder.AppendLine();
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"            private const string _{eKeyL}_d = \"{FixString(entry.Value)}\";");
                }
                builder.AppendLine();
                builder.AppendLine("            #endregion Default");
                builder.AppendLine();
                builder.AppendLine("            #region Members");
                builder.AppendLine();
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"            private string _{eKeyL} = _{eKeyL}_d;");
                }
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyU = char.ToUpperInvariant(entry.Key![0]) + entry.Key![1..];
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine();
                    builder.AppendLine("            /// <summary>");
                    builder.AppendLine($"            /// {entry.Comment}");
                    builder.AppendLine("            /// </summary>");
                    builder.AppendLine($"            public string {eKeyU} {{ get {{ return _{eKeyL}; }} set {{ OnPropertyChanging(nameof(this.{eKeyU})); _{eKeyL} = value; OnPropertyChanged(nameof(this.{eKeyU})); }} }}");
                }
                builder.AppendLine();
                builder.AppendLine("            #endregion Members");
                builder.AppendLine();
                builder.AppendLine($"            internal __{sNameU}()");
                builder.AppendLine("            {");
                builder.AppendLine("            }");
                builder.AppendLine();
                builder.AppendLine("            internal void Load(XConfigManager manager)");
                builder.AppendLine("            {");
                builder.AppendLine($"                if (manager.Sections.TryGetValue(\"{section.Name}\", out XSection section))");
                builder.AppendLine("                {");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyU = char.ToUpperInvariant(entry.Key![0]) + entry.Key![1..];
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"                    this.{eKeyU} = section.Properties.GetStringValue(\"{entry.Key!}\", _{eKeyL}_d);");
                }
                builder.AppendLine("                }");
                builder.AppendLine("            }");
                builder.AppendLine();
                builder.AppendLine("            internal void ResetDefault()");
                builder.AppendLine("            {");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyU = char.ToUpperInvariant(entry.Key![0]) + entry.Key![1..];
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"                this.{eKeyU} = _{eKeyL}_d;");
                }
                builder.AppendLine("            }");
                builder.AppendLine();
                builder.AppendLine("            internal void Save(bool defaultField, XConfigManager manager)");
                builder.AppendLine("            {");
                builder.AppendLine($"                XSection section = manager.Sections.Add(\"{section.Name}\");");
                builder.AppendLine("                if (defaultField)");
                builder.AppendLine("                {");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"                    section.Properties.AddString(\"{entry.Key!}\", _{eKeyL}_d).Comment.SetValue(_{eKeyL}_c);");
                }
                builder.AppendLine("                }");
                builder.AppendLine("                else");
                builder.AppendLine("                {");
                foreach (var entry in section.LanguageEntries)
                {
                    string eKeyU = char.ToUpperInvariant(entry.Key![0]) + entry.Key![1..];
                    string eKeyL = char.ToLowerInvariant(entry.Key![0]) + entry.Key![1..];
                    builder.AppendLine($"                    section.Properties.AddString(\"{entry.Key!}\", this.{eKeyU}).Comment.SetValue(_{eKeyL}_c);");
                }
                builder.AppendLine("                }");
                builder.AppendLine("            }");
                builder.AppendLine("        }");
            }
            builder.AppendLine("    }");
            builder.AppendLine("}");
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