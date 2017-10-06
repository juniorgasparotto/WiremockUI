using System.Drawing;

namespace WiremockUI
{
    public interface ILogWriter
    {
        void Write(string value, bool force = false);
        void Write(string value, Color color, bool bold = false, bool force = false);

        void WriteLine(bool force = false);
        void WriteLine(string value, bool force = false);
        void WriteLine(string value, Color color, bool bold = false, bool force = false);

        void Error(string value, bool bold = false, bool force = false);
        void Info(string value, bool bold = false, bool force = false);
    }
}