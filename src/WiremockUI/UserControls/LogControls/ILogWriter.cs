using System.Drawing;

namespace WiremockUI
{
    public interface ILogWriter
    {
        void Write(string value);
        void Write(string value, Color color, bool bold = false);

        void WriteLine();
        void WriteLine(string value);
        void WriteLine(string value, Color color, bool bold = false);

        void Error(string value, bool bold = false);
        void Info(string value, bool bold = false);
    }
}