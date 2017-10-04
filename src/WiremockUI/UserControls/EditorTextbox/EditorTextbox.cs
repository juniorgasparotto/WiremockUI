using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class EditorTextBox
    {
        #region Search
        private int indexOfSearchText;
        private int start;
        #endregion

        private Stack<Func<object>> undoStack = new Stack<Func<object>>();
        private Stack<Func<object>> redoStack = new Stack<Func<object>>();
        private bool enableOptions;

        [DefaultValue(true)]
        public bool EnableSearch { get; set; }

        [DefaultValue(true)]
        public bool EnableFormatters { get; set; }

        [DefaultValue(true)]
        public bool EnableHistory { get; set; }

        [DefaultValue(true)]
        public bool EnableOptions
        {
            get => enableOptions;
            set
            {
                this.enableOptions = value;
                if (value)
                {
                    var menu = new ContextMenuStrip();
                    var jsonMenu = new ToolStripMenuItem(Resource.jsonText);
                    var xmlMenu = new ToolStripMenuItem(Resource.xmlText);

                    var formatJsonMenu = new ToolStripMenuItem();
                    var jsonEscapeMenu = new ToolStripMenuItem();
                    var jsonUnescapeMenu = new ToolStripMenuItem();
                    var minifyJsonMenu = new ToolStripMenuItem();
                    var editJsonValueMenu = new ToolStripMenuItem();

                    var formatXmlMenu = new ToolStripMenuItem();
                    var xmlEscapeMenu = new ToolStripMenuItem();
                    var xmlUnescapeMenu = new ToolStripMenuItem();
                    var minifyXmlMenu = new ToolStripMenuItem();
                    var editXmlValueMenu = new ToolStripMenuItem();
                    var searchMenu = new ToolStripMenuItem();

                    menu.Items.AddRange(new ToolStripMenuItem[]
                    {
                        jsonMenu,
                        xmlMenu,
                        searchMenu
                    });

                    jsonMenu.DropDownItems.AddRange(new ToolStripMenuItem[]
                    {
                        formatJsonMenu,
                        jsonEscapeMenu,
                        jsonUnescapeMenu,
                        minifyJsonMenu,
                        editJsonValueMenu
                    });

                    jsonMenu.DropDownOpening += (o, s) =>
                    {
                        editJsonValueMenu.Visible = !string.IsNullOrEmpty(txtContent.SelectedText);
                    };

                    xmlMenu.DropDownItems.AddRange(new ToolStripMenuItem[]
                    {
                        formatXmlMenu,
                        xmlEscapeMenu,
                        xmlUnescapeMenu,
                        minifyXmlMenu,
                        editXmlValueMenu
                    });

                    xmlMenu.DropDownOpening += (o, s) =>
                    {
                        editXmlValueMenu.Visible = !string.IsNullOrEmpty(txtContent.SelectedText);
                    };

                    // json
                    formatJsonMenu.Text = Resource.formatJsonMenu;
                    formatJsonMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(txtContent.SelectedText))
                            TextValue = Helper.FormatToJson(TextValue);
                        else
                            txtContent.SelectedText = Helper.FormatToJson(txtContent.SelectedText);
                    };

                    // xml
                    formatXmlMenu.Text = Resource.formatXmlMenu;
                    formatXmlMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(txtContent.SelectedText))
                            txtContent.Text = Helper.FormatToXml(TextValue);
                        else
                            txtContent.SelectedText = Helper.FormatToXml(txtContent.SelectedText);
                    };

                    // encode json
                    jsonEscapeMenu.Text = Resource.jsonEscapeMenu;
                    jsonEscapeMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(txtContent.SelectedText))
                            TextValue = Helper.JsonEscape(TextValue);
                        else
                            txtContent.SelectedText = Helper.JsonEscape(txtContent.SelectedText);
                    };

                    // decode json
                    jsonUnescapeMenu.Text = Resource.jsonUnescapeMenu;
                    jsonUnescapeMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(txtContent.SelectedText))
                            txtContent.Text = Helper.JsonUnescape(TextValue);
                        else
                            txtContent.SelectedText = Helper.JsonUnescape(txtContent.SelectedText);
                    };

                    // minify json
                    minifyJsonMenu.Text = Resource.minifyMenu;
                    minifyJsonMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(txtContent.SelectedText))
                            TextValue = Helper.JsonMinify(TextValue);
                        else
                            txtContent.SelectedText = Helper.JsonMinify(txtContent.SelectedText);
                    };

                    // edit value json
                    editJsonValueMenu.Text = Resource.editValueMenu;
                    editJsonValueMenu.Click += (a, b) =>
                    {
                        var selectedValue = Helper.GetJsonValue(txtContent.SelectedText, out var hasQuote, out var hasError);

                        if (!hasError)
                        {
                            var frmEdit = new FormEditValue(selectedValue, result =>
                            {
                                if (hasQuote)
                                    txtContent.SelectedText = "\"" + Helper.JsonEscape(result) + "\"";
                                else
                                    txtContent.SelectedText = Helper.JsonEscape(result);
                            });

                            frmEdit.StartPosition = FormStartPosition.CenterParent;
                            frmEdit.ShowDialog();
                        }
                    };

                    // encode xml
                    xmlEscapeMenu.Text = Resource.xmlEscapeMenu;
                    xmlEscapeMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(txtContent.SelectedText))
                            TextValue = Helper.XmlEscape(TextValue);
                        else
                            txtContent.SelectedText = Helper.XmlEscape(txtContent.SelectedText);
                    };

                    // decode xml
                    xmlUnescapeMenu.Text = Resource.jsonUnescapeMenu;
                    xmlUnescapeMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(txtContent.SelectedText))
                            TextValue = Helper.XmlUnescape(TextValue);
                        else
                            txtContent.SelectedText = Helper.XmlUnescape(txtContent.SelectedText);
                    };

                    // minify xml
                    minifyXmlMenu.Text = Resource.minifyMenu;
                    minifyXmlMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(txtContent.SelectedText))
                            TextValue = Helper.XmlMinify(TextValue);
                        else
                            txtContent.SelectedText = Helper.XmlMinify(txtContent.SelectedText);
                    };

                    // edit value json
                    editXmlValueMenu.Text = Resource.editValueMenu;
                    editXmlValueMenu.Click += (a, b) =>
                    {
                        var selectedValue = Helper.GetXmlValue(txtContent.SelectedText, out var hasTagInSelection, out var hasError);
                        if (!hasError)
                        {
                            var frmEdit = new FormEditValue(selectedValue, result =>
                            {
                                if (hasTagInSelection)
                                    txtContent.SelectedText = ">" + Helper.XmlEscape(result) + "</";
                                else
                                    txtContent.SelectedText = Helper.XmlEscape(result);
                            });

                            frmEdit.StartPosition = FormStartPosition.CenterParent;
                            frmEdit.ShowDialog();
                        }
                    };

                    // search
                    searchMenu.Text = Resource.searchMenu;
                    searchMenu.ShortcutKeys = Keys.Control | Keys.F;
                    searchMenu.Click += (a, b) =>
                    {
                        OpenSearch();
                    };

                    menu.Opened += (o, e) =>
                    {
                        jsonMenu.Visible = EnableFormatters;
                        xmlMenu.Visible = EnableFormatters;
                        searchMenu.Visible = EnableSearch;
                    };

                    txtContent.ContextMenuStrip = menu;
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
            set => txtContent.Text = value;
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

        [DefaultValue(RichTextBoxScrollBars.Both)]
        [Localizable(true)]
        public RichTextBoxScrollBars ScrollBars
        {
            get => txtContent.ScrollBars;
            set => txtContent.ScrollBars = value;
        }

        [DefaultValue(false)]
        public bool ShowSelectionMargin
        {
            get => txtContent.ShowSelectionMargin;
            set => txtContent.ShowSelectionMargin = value;
        }

        [DefaultValue(false)]
        public bool HideSelection
        {
            get => txtContent.HideSelection;
            set => txtContent.HideSelection = value;
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font SelectionFont
        {
            get => txtContent.SelectionFont;
            set => txtContent.SelectionFont = value;
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

        [DefaultValue(0)]
        public int MaxLength
        {
            get => txtContent.MaxLength;
            set => txtContent.MaxLength = value;
        }

        [DefaultValue(false)]
        public bool DetectUrls
        {
            get => txtContent.DetectUrls;
            set => txtContent.DetectUrls = value;
        }

        [DefaultValue(true)]
        [Localizable(true)]
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

        #endregion

        public EditorTextBox()
        {
            InitializeComponent();

            AcceptsTab = true;
            Multiline = true;
            MaxLength = 0;
            DetectUrls = false;
            HideSelection = false;
            EnableHistory = true;
            EnableOptions = true;
            EnableSearch = true;
            EnableFormatters = true;

            this.txtSearchValue.Multiline = false;
            this.btnSearch.Text = Resource.btnSearch;
            this.btnClose.Text = Resource.btnClose;
        }

        private void txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            var spaces = "    ";

            if (e.KeyCode == Keys.ControlKey && ModifierKeys == Keys.Control) { }
            else if (e.KeyCode == Keys.A && ModifierKeys == Keys.Control)
            {
                ((RichTextBox)sender).SelectAll();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Tab && ModifierKeys == Keys.None)
            {
                if (txtContent.ReadOnly)
                    return;

                e.SuppressKeyPress = true;
                var posFinal = txtContent.SelectionStart + spaces.Length;
                txtContent.Text = txtContent.Text.Insert(txtContent.SelectionStart, spaces);
                txtContent.SelectionStart = posFinal;
            }
            else if (e.KeyCode == Keys.Tab && ModifierKeys == Keys.Shift)
            {
                if (txtContent.ReadOnly)
                    return;

                e.SuppressKeyPress = true;

                var posStart = txtContent.SelectionStart - spaces.Length;
                var posEnd = txtContent.SelectionStart - 1;

                if (posStart < 0)
                    return;

                var c = 0;
                for (var i = posStart; i <= posEnd; i++)
                {
                    if (txtContent.Text[i] == ' ')
                        c++;
                }

                if (c == spaces.Length)
                {
                    txtContent.Text = txtContent.Text.Remove(posStart, spaces.Length);
                    txtContent.SelectionStart = posStart;
                }
            }
            else if (EnableSearch && e.Control && e.KeyCode == Keys.F)
            {
                OpenSearch();
            }
            else if (EnableHistory && e.KeyCode == Keys.Z && ModifierKeys == Keys.Control)
            {
                if (undoStack.Count > 0)
                {
                    StackPush(sender, redoStack);
                    undoStack.Pop()();
                }
            }
            else if (EnableHistory && e.KeyCode == Keys.Y && ModifierKeys == Keys.Control)
            {
                if (redoStack.Count > 0)
                {
                    StackPush(sender, undoStack);
                    redoStack.Pop()();
                }
            }
            else if (EnableHistory)
            {
                redoStack.Clear();
                StackPush(sender, undoStack);
            }
        }

        private void StackPush(object sender, Stack<Func<object>> stack)
        {
            var textBox = (RichTextBox)sender;
            var tBT = GetText(textBox, textBox.Text, textBox.SelectionStart);
            stack.Push(tBT);
        }

        private Func<RichTextBox> GetText(RichTextBox textBox, string text, int sel)
        {
            return () =>
            {
                textBox.Text = text;
                textBox.SelectionStart = sel;
                return textBox;
            };
        }


        public void ScrollToCaret()
        {
            txtContent.ScrollToCaret();
        }

        public void Clear()
        {
            txtContent.Clear();
        }

        #region Search

        private void OpenSearch()
        {
            pnlSearch.Visible = !pnlSearch.Visible;
            txtContent.DeselectAll();
            txtSearchValue.Focus();
        }

        private void Search()
        {

            int startindex = 0;

            if (txtSearchValue.Text.Length > 0)
            {
                startindex = FindMyText(txtSearchValue.Text.Trim(), start, txtContent.Text.Length);

                if (startindex == -1 && start >= 0) // Not found string and not searching from beginning
                {
                    // Wrap search
                    // int oldStart = start;
                    start = 0;
                    startindex = FindMyText(txtSearchValue.Text.Trim(), start, txtContent.Text.Length);
                }
            }

            // If string was found in the RichTextBox, highlight it
            if (startindex >= 0)
            {
                int endindex = txtSearchValue.Text.Length;
                txtContent.Select(startindex, endindex);
                start = startindex + endindex;
            }
        }

        public int FindMyText(string txtToSearch, int searchStart, int searchEnd)
        {
            // Unselect the previously searched string
            if (searchStart > 0 && searchEnd > 0 && indexOfSearchText >= 0)
            {
                txtContent.DeselectAll();
            }

            // Set the return value to -1 by default.
            int retVal = -1;

            // A valid starting index should be specified.
            // if indexOfSearchText = -1, the end of search
            if (searchStart >= 0 && indexOfSearchText >= 0)
            {
                // A valid ending index
                if (searchEnd > searchStart || searchEnd == -1)
                {
                    // Find the position of search string in RichTextBox
                    indexOfSearchText = txtContent.Find(txtToSearch, searchStart, searchEnd, RichTextBoxFinds.None);
                    // Determine whether the text was found in richTextBox1.
                    if (indexOfSearchText != -1)
                    {
                        // Return the index to the specified search text.
                        retVal = indexOfSearchText;
                    }
                    else
                    {
                        start = 0;
                        indexOfSearchText = 0;
                    }
                }
            }
            return retVal;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void txtSearchValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
            txtContent.DeselectAll();
        }

        #endregion        
    }
}
