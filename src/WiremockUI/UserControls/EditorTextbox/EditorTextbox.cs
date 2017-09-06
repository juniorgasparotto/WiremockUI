using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
                    var viewJsonMenu = new ToolStripMenuItem();
                    var viewXml = new ToolStripMenuItem();

                    menu.Items.AddRange(new ToolStripMenuItem[]
                    {
                    viewJsonMenu,
                    viewXml
                    });

                    // json
                    viewJsonMenu.Text = "Formatar para Json";
                    viewJsonMenu.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(SelectedText))
                            Text = Helper.FormatToJson(Text);
                        //else
                        //    Paste(Helper.FormatToJson(SelectedText));
                    };

                    // xml
                    viewXml.Text = "Formatar para XML";
                    viewXml.Click += (a, b) =>
                    {
                        if (string.IsNullOrEmpty(SelectedText))
                            Text = Helper.FormatToXml(Text);
                        //else
                        //    Paste(Helper.FormatToXml(SelectedText));
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
