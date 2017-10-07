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
        private SimpleEditor _output = null;
        public bool EnableAutoScroll { get; set; } = true;
        public bool Enabled { get; internal set; } = true;

        public RichTextBoxLogWriter(SimpleEditor output)
        {
            _output = output;
        }

        public void Write(string value, Color color, bool bold = false, bool force = false)
        {
            if (!Enabled && !force)
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

        public void WriteLine(string value, Color color, bool bold = false, bool force = false)
        {
            Write(value, color, bold, force: force);
            Write("\r\n", force: force);
            Write("\r\n", force: force);
        }

        public void Error(string value, bool bold = false, bool force = false)
        {
            WriteLine(value, Color.Red, bold, force: force);
        }

        public void Info(string value, bool bold = false, bool force = false)
        {
            WriteLine(value, Color.Black, bold, force: force);
        }

        public void Write(string value, bool force = false)
        {
            Write(value, Color.Black, force: force);
        }

        public void WriteLine(bool force = false)
        {
            Write("\r\n", force: force);
        }

        public void WriteLine(string value, bool force = false)
        {
            Write(value, force: force);
            Write("\r\n", force: force);
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
