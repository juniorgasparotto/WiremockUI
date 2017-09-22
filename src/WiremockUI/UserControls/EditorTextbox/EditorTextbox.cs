using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public class EditorTextBox : RichTextBox
    {
        Stack<Func<object>> undoStack = new Stack<Func<object>>();
        Stack<Func<object>> redoStack = new Stack<Func<object>>();
        private bool enableFormatter;

        public bool EnableHistory { get; set; } = true;

        public bool EnableFormatter
        {
            get => enableFormatter;
            set
            {
                this.enableFormatter = value;
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

                    menu.Items.AddRange(new ToolStripMenuItem[]
                    {
                        jsonMenu,
                        xmlMenu
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
                        editJsonValueMenu.Visible = !string.IsNullOrEmpty(SelectedText);
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
                        editXmlValueMenu.Visible = !string.IsNullOrEmpty(SelectedText);
                    };

                    // json
                    formatJsonMenu.Text = Resource.formatJsonMenu;
                    formatJsonMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(SelectedText))
                            Text = Helper.FormatToJson(Text);
                        else
                            SelectedText = Helper.FormatToJson(SelectedText);
                    };

                    // xml
                    formatXmlMenu.Text = Resource.formatXmlMenu;
                    formatXmlMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(SelectedText))
                            Text = Helper.FormatToXml(Text);
                        else
                            SelectedText = Helper.FormatToXml(SelectedText);
                    };

                    // encode json
                    jsonEscapeMenu.Text = Resource.jsonEscapeMenu;
                    jsonEscapeMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(SelectedText))
                            Text = Helper.JsonEscape(Text);
                        else
                            SelectedText = Helper.JsonEscape(SelectedText);
                    };

                    // decode json
                    jsonUnescapeMenu.Text = Resource.jsonUnescapeMenu;
                    jsonUnescapeMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(SelectedText))
                            Text = Helper.JsonUnescape(Text);
                        else
                            SelectedText = Helper.JsonUnescape(SelectedText);
                    };

                    // minify json
                    minifyJsonMenu.Text = Resource.minifyMenu;
                    minifyJsonMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(SelectedText))
                            Text = Helper.JsonMinify(Text);
                        else
                            SelectedText = Helper.JsonMinify(SelectedText);
                    };

                    // edit value json
                    editJsonValueMenu.Text = Resource.editValueMenu;
                    editJsonValueMenu.Click += (a, b) =>
                    {
                        var selectedValue = Helper.GetJsonValue(SelectedText, out var hasQuote, out var hasError);

                        if (!hasError)
                        {
                            var frmEdit = new FormEditValue(selectedValue, result =>
                            {
                                if (hasQuote)
                                    SelectedText = "\"" + Helper.JsonEscape(result) + "\"";
                                else
                                    SelectedText = Helper.JsonEscape(result);
                            });

                            frmEdit.StartPosition = FormStartPosition.CenterParent;
                            frmEdit.ShowDialog();
                        }
                    };

                    // encode xml
                    xmlEscapeMenu.Text = Resource.xmlEscapeMenu;
                    xmlEscapeMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(SelectedText))
                            Text = Helper.XmlEscape(Text);
                        else
                            SelectedText = Helper.XmlEscape(SelectedText);
                    };

                    // decode xml
                    xmlUnescapeMenu.Text = Resource.jsonUnescapeMenu;
                    xmlUnescapeMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(SelectedText))
                            Text = Helper.XmlUnescape(Text);
                        else
                            SelectedText = Helper.XmlUnescape(SelectedText);
                    };

                    // minify xml
                    minifyXmlMenu.Text = Resource.minifyMenu;
                    minifyXmlMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(SelectedText))
                            Text = Helper.XmlMinify(Text);
                        else
                            SelectedText = Helper.XmlMinify(SelectedText);
                    };

                    // edit value json
                    editXmlValueMenu.Text = Resource.editValueMenu;
                    editXmlValueMenu.Click += (a, b) =>
                    {
                        var selectedValue = Helper.GetXmlValue(SelectedText, out var hasTagInSelection, out var hasError);
                        if (!hasError)
                        {
                            var frmEdit = new FormEditValue(selectedValue, result =>
                            {
                                if (hasTagInSelection)
                                    SelectedText = ">" + Helper.XmlEscape(result) + "</";
                                else
                                    SelectedText = Helper.XmlEscape(result);
                            });

                            frmEdit.StartPosition = FormStartPosition.CenterParent;
                            frmEdit.ShowDialog();
                        }
                    };


                    this.ContextMenuStrip = menu;
                }
                else
                {
                    this.ContextMenuStrip = null;
                }
            }
        }

        public EditorTextBox()
        {
            this.AcceptsTab = true;
            this.Multiline = true;
            this.KeyDown += HistoryTextBox_KeyDown;
            this.MaxLength = 0;
            this.DetectUrls = false;
        }

        private void HistoryTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var spaces = "    ";

            if (e.KeyCode == Keys.ControlKey && ModifierKeys == Keys.Control) { }
            else if (e.KeyCode == Keys.A && ModifierKeys == Keys.Control)
            {
                ((EditorTextBox)sender).SelectAll();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Tab && ModifierKeys == Keys.None)
            {
                if (ReadOnly)
                    return;

                e.SuppressKeyPress = true;
                var posFinal = this.SelectionStart + spaces.Length;
                this.Text = this.Text.Insert(this.SelectionStart, spaces);
                this.SelectionStart = posFinal;
            }
            else if (e.KeyCode == Keys.Tab && ModifierKeys == Keys.Shift)
            {
                if (ReadOnly)
                    return;

                e.SuppressKeyPress = true;

                var posStart = this.SelectionStart - spaces.Length;
                var posEnd = this.SelectionStart - 1;

                if (posStart < 0)
                    return;

                var c = 0;
                for (var i = posStart; i <= posEnd; i++)
                {
                    if (this.Text[i] == ' ')
                        c++;
                }

                if (c == spaces.Length)
                {
                    this.Text = this.Text.Remove(posStart, spaces.Length);
                    this.SelectionStart = posStart;
                }
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
    }
}
