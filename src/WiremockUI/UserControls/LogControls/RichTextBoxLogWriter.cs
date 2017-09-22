using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Drawing;

namespace WiremockUI
{
    public class RichTextBoxLogWriter : ILogWriter
    {
        delegate void SetTextCallback(string text);
        private RichTextBox _output = null;
        public bool EnableAutoScroll { get; set; } = true;
        public bool Enabled { get; internal set; } = true;

        public RichTextBoxLogWriter(RichTextBox output)
        {
            _output = output;
        }

        public void Write(string value, Color color, bool bold = false)
        {
            if (!Enabled)
                return;

            if (_output.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(f => AppendText(f, color, bold));
                _output.Invoke(d, new object[] { value });
            }
            else
            {
                AppendText(value, color, bold);
            }
        }

        public void WriteLine(string value, Color color, bool bold = false)
        {
            Write(value, color, bold);
            Write("\r\n");
            Write("\r\n");
        }

        public void Error(string value, bool bold = false)
        {
            WriteLine(value, Color.Red, bold);
        }

        public void Info(string value, bool bold = false)
        {
            WriteLine(value, Color.Black, bold);
        }

        public void Write(string value)
        {
            Write(value, Color.Black);
        }

        public void WriteLine()
        {
            Write("\r\n");
        }

        public void WriteLine(string value)
        {
            Write(value);
            Write("\r\n");
        }

        private void AppendText(string text, Color color, bool bold)
        {
            var ss = _output.SelectionStart;
            _output.SuspendLayout();
            _output.SelectionStart = _output.TextLength;
            _output.SelectionColor = color;
            _output.SelectionFont = new Font(_output.Font, bold ? FontStyle.Bold : FontStyle.Regular);
            _output.SelectedText = text;
            _output.ResumeLayout();

            if (EnableAutoScroll)
                _output.ScrollToCaret();
            else 
                _output.SelectionStart = ss;
        }
    }
}
