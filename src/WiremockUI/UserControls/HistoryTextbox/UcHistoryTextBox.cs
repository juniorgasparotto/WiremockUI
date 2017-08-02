using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WiremockUI
{
    public class HistoryTextBox : TextBox
    {
        Stack<Func<object>> undoStack = new Stack<Func<object>>();
        Stack<Func<object>> redoStack = new Stack<Func<object>>();

        public HistoryTextBox()
        {
            this.AcceptsTab = true;
            this.KeyDown += HistoryTextBox_KeyDown;
        }

        private void HistoryTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var spaces = "    ";

            if (e.KeyCode == Keys.ControlKey && ModifierKeys == Keys.Control) { }
            else if (e.KeyCode == Keys.Tab && ModifierKeys == Keys.None)
            {
                e.SuppressKeyPress = true;
                var posFinal = this.SelectionStart + spaces.Length;
                this.Text = this.Text.Insert(this.SelectionStart, spaces);
                this.SelectionStart = posFinal;
            }
            else if (e.KeyCode == Keys.Tab && ModifierKeys == Keys.Shift)
            {
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
            else if (e.KeyCode == Keys.Z && ModifierKeys == Keys.Control)
            {
                if (undoStack.Count > 0)
                {
                    StackPush(sender, redoStack);
                    undoStack.Pop()();
                }
            }
            else if (e.KeyCode == Keys.Y && ModifierKeys == Keys.Control)
            {
                if (redoStack.Count > 0)
                {
                    StackPush(sender, undoStack);
                    redoStack.Pop()();
                }
            }
            else
            {
                redoStack.Clear();
                StackPush(sender, undoStack);
            }
        }

        private void StackPush(object sender, Stack<Func<object>> stack)
        {
            TextBox textBox = (TextBox)sender;
            var tBT = GetText(textBox, textBox.Text, textBox.SelectionStart);
            stack.Push(tBT);
        }

        private Func<TextBox> GetText(TextBox textBox, string text, int sel)
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
