using FastColoredTextBoxNS;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class AdvancedEditor : IBaseEditor
    {
        private bool enableOptions;
        private bool firstInput = true;

        #region JSON formatter
        
        private readonly Regex jsonKeyRegex = new Regex(@""".+""\s*?\:", SyntaxHighlighter.RegexCompiledOption);
        private readonly Regex jsonNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b", SyntaxHighlighter.RegexCompiledOption);
        private readonly Regex jsonStringRegex = new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'", SyntaxHighlighter.RegexCompiledOption);
        private readonly Regex jsonKeywordRegex = new Regex(@"\b(true|false|null)\b", SyntaxHighlighter.RegexCompiledOption);

        private Style jsonKeyStyle = new TextStyle(Brushes.Blue, null, FontStyle.Bold);
        public readonly Style jsonNumberStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        public readonly Style jsonStringStyle = new TextStyle(Brushes.Black, null, FontStyle.Italic);
        public readonly Style jsonKeywordStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);

        #endregion

        private LanguageSupported language;

        public enum LanguageSupported
        {
            Custom = 0,
            CSharp = 1,
            VB = 2,
            HTML = 3,
            XML = 4,
            SQL = 5,
            PHP = 6,
            JS = 7,
            Lua = 8,
            Json = 9
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEdited { get; set; }

        [DefaultValue(LanguageSupported.Custom)]
        public LanguageSupported Language
        {
            get
            {
                return this.language;
            }
            set
            {
                this.language = value;
                txtContent.ClearStyle(StyleIndex.All);
                txtContent.ClearStylesBuffer();

                if (this.language == LanguageSupported.Json)
                {
                    // txtContent.Language = FastColoredTextBoxNS.Language.Custom;
                    this.txtContent.TextChangedDelayed += this.txtContent_IsJson_TextChangedDelayed;
                    SetJsonStyle();
                }
                else
                {
                    this.txtContent.TextChangedDelayed -= this.txtContent_IsJson_TextChangedDelayed;
                    txtContent.Language = GetLanguageFCTB(value);
                }

                txtContent.OnSyntaxHighlight(new TextChangedEventArgs(txtContent.Range));
            }
        }
        
        [DefaultValue(false)]
        public bool ShowLanguages { get; set; }

        [DefaultValue(true)]
        public bool EnableOptions
        {
            get => enableOptions;
            set
            {
                this.enableOptions = value;
                if (value)
                {
                    var languagesMenu = new ToolStripMenuItem(Resource.languagesMenu);

                    txtContent.ContextMenuStrip = EditorUtils.GetMenuOptions(this);
                    txtContent.ContextMenuStrip.Items.Add(languagesMenu);

                    var langCSharpMenu = new ToolStripMenuItem("C#");
                    var langCustomMenu = new ToolStripMenuItem("Custom");
                    var langHTMLMenu = new ToolStripMenuItem("HTML");
                    var langJSMenu = new ToolStripMenuItem("JavaScript");
                    var langJsonMenu = new ToolStripMenuItem("JSON");
                    var langLuaMenu = new ToolStripMenuItem("Lua");
                    var langPHPMenu = new ToolStripMenuItem("PHP");
                    var langSQLMenu = new ToolStripMenuItem("SQL");
                    var langVBMenu = new ToolStripMenuItem("VB.NET");
                    var langXMLMenu = new ToolStripMenuItem("XML");

                    languagesMenu.DropDownItems.AddRange(new ToolStripMenuItem[]
                    {
                        langCSharpMenu,
                        langCustomMenu,
                        langHTMLMenu,
                        langJSMenu,
                        langJsonMenu,
                        langLuaMenu,
                        langPHPMenu,
                        langSQLMenu,
                        langVBMenu,
                        langXMLMenu,
                    });

                    // languages
                    langCSharpMenu.Click += (a, b) => Language = LanguageSupported.CSharp;
                    langCustomMenu.Click += (a, b) => Language = LanguageSupported.Custom;
                    langHTMLMenu.Click += (a, b) => Language = LanguageSupported.HTML;
                    langJSMenu.Click += (a, b) => Language = LanguageSupported.JS;
                    langJsonMenu.Click += (a, b) => Language = LanguageSupported.Json;
                    langLuaMenu.Click += (a, b) => Language = LanguageSupported.Lua;
                    langPHPMenu.Click += (a, b) => Language = LanguageSupported.PHP;
                    langSQLMenu.Click += (a, b) => Language = LanguageSupported.SQL;
                    langVBMenu.Click += (a, b) => Language = LanguageSupported.VB;
                    langXMLMenu.Click += (a, b) => Language = LanguageSupported.XML;

                    txtContent.ContextMenuStrip.Opened += (o, e) =>
                    {
                        languagesMenu.Visible = ShowLanguages;

                        foreach (ToolStripItem l in languagesMenu.DropDownItems)
                            l.Image = null;

                        switch (language)
                        {
                            case LanguageSupported.CSharp:
                                langCSharpMenu.Image = Properties.Resources.check;
                                break;
                            case LanguageSupported.HTML:
                                langHTMLMenu.Image = Properties.Resources.check;
                                break;
                            case LanguageSupported.JS:
                                langJSMenu.Image = Properties.Resources.check;
                                break;
                            case LanguageSupported.Lua:
                                langLuaMenu.Image = Properties.Resources.check;
                                break;
                            case LanguageSupported.PHP:
                                langPHPMenu.Image = Properties.Resources.check;
                                break;
                            case LanguageSupported.SQL:
                                langSQLMenu.Image = Properties.Resources.check;
                                break;
                            case LanguageSupported.VB:
                                langVBMenu.Image = Properties.Resources.check;
                                break;
                            case LanguageSupported.XML:
                                langXMLMenu.Image = Properties.Resources.check;
                                break;
                            case LanguageSupported.Json:
                                langJsonMenu.Image = Properties.Resources.check;
                                break;
                            default:
                                langCustomMenu.Image = Properties.Resources.check;
                                break;
                        }
                    };
                }
                else
                {
                    txtContent.ContextMenuStrip = null;
                }
            }
        }

        #region RithTextBox Wrapper

        [Localizable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue("")]
        public string TextValue
        {
            get => txtContent.Text;
            set 
            {
                txtContent.Text = value;
                if (firstInput)
                {
                    txtContent.ClearUndo();
                    firstInput = false;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get => txtContent.SelectionStart;
            set => txtContent.SelectionStart = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get => txtContent.SelectionLength;
            set => txtContent.SelectionLength = value;
        }

        [Browsable(false)]
        public int TextLength
        {
            get => txtContent.TextLength;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color SelectionColor
        {
            get => txtContent.SelectionColor;
            set => txtContent.SelectionColor = value;
        }

        [Browsable(false)]
        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get => txtContent.SelectedText;
            set => txtContent.SelectedText = value;
        }

        [DefaultValue(true)]
        public bool AcceptsTab
        {
            get => txtContent.AcceptsTab;
            set => txtContent.AcceptsTab = value;
        }

        [DefaultValue(true)]
        public bool Multiline
        {
            get => txtContent.Multiline;
            set => txtContent.Multiline = value;
        }

        [DefaultValue(true)]
        public bool WordWrap
        {
            get => txtContent.WordWrap;
            set => txtContent.WordWrap = value;
        }

        [DefaultValue(false)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public bool ReadOnly
        {
            get => txtContent.ReadOnly;
            set => txtContent.ReadOnly = value;
        }

        [DefaultValue(BorderStyle.Fixed3D)]
        public new BorderStyle BorderStyle
        {
            get => txtContent.BorderStyle;
            set
            {
                txtContent.BorderStyle = value;
            }
        }

        [DefaultValue(true)]
        public bool EnableSearch { get; set; }

        [DefaultValue(true)]
        public bool EnableFormatters { get; set; }

        #endregion

        public AdvancedEditor()
        {
            InitializeComponent();
            EnableOptions = true;
            EnableSearch = true;
            EnableFormatters = true;
        }

        private void SetJsonStyle()
        {
            txtContent.Range.SetStyle(jsonKeyStyle, jsonKeyRegex);
            txtContent.Range.SetStyle(jsonStringStyle, jsonStringRegex);
            txtContent.Range.SetStyle(jsonNumberStyle, jsonNumberRegex);
            txtContent.Range.SetStyle(jsonKeywordStyle, jsonKeywordRegex);
        }

        private Language GetLanguageFCTB(LanguageSupported language)
        {
            switch (language)
            {
                case LanguageSupported.CSharp:
                    return FastColoredTextBoxNS.Language.CSharp;
                case LanguageSupported.HTML:
                    return FastColoredTextBoxNS.Language.HTML;
                case LanguageSupported.JS:
                    return FastColoredTextBoxNS.Language.JS;
                case LanguageSupported.Lua:
                    return FastColoredTextBoxNS.Language.Lua;
                case LanguageSupported.PHP:
                    return FastColoredTextBoxNS.Language.PHP;
                case LanguageSupported.SQL:
                    return FastColoredTextBoxNS.Language.SQL;
                case LanguageSupported.VB:
                    return FastColoredTextBoxNS.Language.VB;
                case LanguageSupported.XML:
                    return FastColoredTextBoxNS.Language.XML;
                default:
                    return FastColoredTextBoxNS.Language.Custom;
            }
        }

        private void txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (EditorUtils.KeyIsWritable(e.KeyCode))
                IsEdited = true;

            OnKeyDown(e);
        }


        private void txtContent_IsJson_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            SetJsonStyle();
        }

        #region IEditorBase

        public void SelectAll()
        {
            txtContent.SelectAll();
        }

        public void Redo()
        {
            txtContent.Redo();
        }

        public void Undo()
        {
            txtContent.Undo();
        }

        public void Paste()
        {
            txtContent.Paste();
        }

        public void Cut()
        {
            txtContent.Cut();
        }

        public void Copy()
        {
            txtContent.Copy();
        }

        public void ShowReplaceDialog()
        {
            txtContent.ShowReplaceDialog();
        }

        #endregion
    }
}
