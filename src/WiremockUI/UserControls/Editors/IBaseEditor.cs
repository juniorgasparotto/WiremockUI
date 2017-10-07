using System.Windows.Forms;

namespace WiremockUI
{
    public interface IBaseEditor
    {
        string SelectedText { get; set; }
        string TextValue { get; set; }
        bool WordWrap { get; set; }
        ContextMenuStrip ContextMenuStrip { get; set; }
        bool EnableSearch { get; set; }
        bool EnableFormatters { get; set; }

        void SelectAll();
        void Redo();
        void Undo();
        void Paste();
        void Cut();
        void Copy();
        void ShowReplaceDialog();
    }
}
