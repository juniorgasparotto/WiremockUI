using System.Windows.Forms;
using WiremockUI.Languages;
using System.Linq;

namespace WiremockUI
{
    public static class EditorUtils
    {
        public static ContextMenuStrip GetMenuOptions(IBaseEditor editor)
        {
            var menu = new ContextMenuStrip();
            var jsonMenu = new ToolStripMenuItem(Resource.jsonText);
            var xmlMenu = new ToolStripMenuItem(Resource.xmlText);
            var editMenu = new ToolStripMenuItem(Resource.editMenu);

            var copyMenu = new ToolStripMenuItem();
            var cutMenu = new ToolStripMenuItem();
            var pasteMenu = new ToolStripMenuItem();
            var undoMenu = new ToolStripMenuItem();
            var redoMenu = new ToolStripMenuItem();
            var selectAllMenu = new ToolStripMenuItem();
            var removeMenu = new ToolStripMenuItem();
            var wordWrapMenu = new ToolStripMenuItem();

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

            menu.Items.AddRange(new ToolStripItem[]
            {
                undoMenu,
                redoMenu,
                new ToolStripSeparator(),
                editMenu,
                searchMenu,
                new ToolStripSeparator(),
                jsonMenu,
                xmlMenu,
            });

            editMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                wordWrapMenu,
                new ToolStripSeparator(),
                selectAllMenu,
                copyMenu,
                cutMenu,
                pasteMenu,
                removeMenu,
            });

            jsonMenu.DropDownItems.AddRange(new ToolStripMenuItem[]
            {
                formatJsonMenu,
                jsonEscapeMenu,
                jsonUnescapeMenu,
                minifyJsonMenu,
                editJsonValueMenu
            });

            xmlMenu.DropDownItems.AddRange(new ToolStripMenuItem[]
            {
                formatXmlMenu,
                xmlEscapeMenu,
                xmlUnescapeMenu,
                minifyXmlMenu,
                editXmlValueMenu
            });

            // json
            formatJsonMenu.Text = Resource.formatJsonMenu;
            formatJsonMenu.ShortcutKeys = Keys.Control | Keys.D1;
            formatJsonMenu.Click += (a, b) =>
            {
                if (string.IsNullOrEmpty(editor.SelectedText))
                    editor.TextValue = Helper.FormatToJson(editor.TextValue);
                else
                    editor.SelectedText = Helper.FormatToJson(editor.SelectedText);
            };

            // encode json
            jsonEscapeMenu.Text = Resource.jsonEscapeMenu;
            jsonEscapeMenu.ShortcutKeys = Keys.Control | Keys.D2;
            jsonEscapeMenu.Click += (a, b) =>
            {
                if (string.IsNullOrEmpty(editor.SelectedText))
                    editor.TextValue = Helper.JsonEscape(editor.TextValue);
                else
                    editor.SelectedText = Helper.JsonEscape(editor.SelectedText);
            };

            // decode json
            jsonUnescapeMenu.Text = Resource.jsonUnescapeMenu;
            jsonUnescapeMenu.ShortcutKeys = Keys.Control | Keys.D3;
            jsonUnescapeMenu.Click += (a, b) =>
            {
                if (string.IsNullOrEmpty(editor.SelectedText))
                    editor.TextValue = Helper.JsonUnescape(editor.TextValue);
                else
                    editor.SelectedText = Helper.JsonUnescape(editor.SelectedText);
            };

            // minify json
            minifyJsonMenu.Text = Resource.minifyMenu;
            minifyJsonMenu.ShortcutKeys = Keys.Control | Keys.D4;
            minifyJsonMenu.Click += (a, b) =>
            {
                if (string.IsNullOrEmpty(editor.SelectedText))
                    editor.TextValue = Helper.JsonMinify(editor.TextValue);
                else
                    editor.SelectedText = Helper.JsonMinify(editor.SelectedText);
            };

            // edit value json
            editJsonValueMenu.Text = Resource.editValueMenu;
            editJsonValueMenu.ShortcutKeys = Keys.Control | Keys.D5;
            editJsonValueMenu.Click += (a, b) =>
            {
                var selectedValue = Helper.GetJsonValue(editor.SelectedText, out var hasQuote, out var hasError);

                if (!hasError)
                {
                    var frmEdit = new FormEditValue(selectedValue, result =>
                    {
                        if (hasQuote)
                            editor.SelectedText = "\"" + Helper.JsonEscape(result) + "\"";
                        else
                            editor.SelectedText = Helper.JsonEscape(result);
                    });

                    frmEdit.StartPosition = FormStartPosition.CenterParent;
                    frmEdit.ShowDialog();
                }
            };

            // xml
            formatXmlMenu.Text = Resource.formatXmlMenu;
            formatXmlMenu.ShortcutKeys = Keys.Control | Keys.Shift | Keys.D1;
            formatXmlMenu.Click += (a, b) =>
            {
                if (string.IsNullOrEmpty(editor.SelectedText))
                    editor.TextValue = Helper.FormatToXml(editor.TextValue);
                else
                    editor.SelectedText = Helper.FormatToXml(editor.SelectedText);
            };


            // encode xml
            xmlEscapeMenu.Text = Resource.xmlEscapeMenu;
            xmlEscapeMenu.ShortcutKeys = Keys.Control | Keys.Shift | Keys.D2;
            xmlEscapeMenu.Click += (a, b) =>
            {
                if (string.IsNullOrEmpty(editor.SelectedText))
                    editor.TextValue = Helper.XmlEscape(editor.TextValue);
                else
                    editor.SelectedText = Helper.XmlEscape(editor.SelectedText);
            };

            // decode xml
            xmlUnescapeMenu.Text = Resource.jsonUnescapeMenu;
            xmlUnescapeMenu.ShortcutKeys = Keys.Control | Keys.Shift | Keys.D3;
            xmlUnescapeMenu.Click += (a, b) =>
            {
                if (string.IsNullOrEmpty(editor.SelectedText))
                    editor.TextValue = Helper.XmlUnescape(editor.TextValue);
                else
                    editor.SelectedText = Helper.XmlUnescape(editor.SelectedText);
            };

            // minify xml
            minifyXmlMenu.Text = Resource.minifyMenu;
            minifyXmlMenu.ShortcutKeys = Keys.Control | Keys.Shift | Keys.D4;
            minifyXmlMenu.Click += (a, b) =>
            {
                if (string.IsNullOrEmpty(editor.SelectedText))
                    editor.TextValue = Helper.XmlMinify(editor.TextValue);
                else
                    editor.SelectedText = Helper.XmlMinify(editor.SelectedText);
            };

            // edit value json
            editXmlValueMenu.Text = Resource.editValueMenu;
            editXmlValueMenu.ShortcutKeys = Keys.Control | Keys.Shift | Keys.D5;
            editXmlValueMenu.Click += (a, b) =>
            {
                var selectedValue = Helper.GetXmlValue(editor.SelectedText, out var hasTagInSelection, out var hasError);
                if (!hasError)
                {
                    var frmEdit = new FormEditValue(selectedValue, result =>
                    {
                        if (hasTagInSelection)
                            editor.SelectedText = ">" + Helper.XmlEscape(result) + "</";
                        else
                            editor.SelectedText = Helper.XmlEscape(result);
                    });

                    frmEdit.StartPosition = FormStartPosition.CenterParent;
                    frmEdit.ShowDialog();
                }
            };

            // search
            searchMenu.Text = Resource.searchMenu;
            searchMenu.ShortcutKeyDisplayString = "Ctrl + F";
            searchMenu.Click += (a, b) =>
            {
                editor.ShowReplaceDialog();
            };

            // copy menu
            copyMenu.Text = Resource.copyMenu;
            copyMenu.ShortcutKeyDisplayString = "Ctrl + C";
            copyMenu.Click += (a, b) =>
            {
                editor.Copy();
            };

            // cut menu 
            cutMenu.Text = Resource.cutMenu;
            cutMenu.ShortcutKeyDisplayString = "Ctrl + X";
            cutMenu.Click += (a, b) =>
            {
                editor.Cut();
            };

            // paste menu 
            pasteMenu.Text = Resource.pasteMenu;
            pasteMenu.ShortcutKeyDisplayString = "Ctrl + V";
            pasteMenu.Click += (a, b) =>
            {
                editor.Paste();
            };

            // undo menu 
            undoMenu.Text = Resource.undoMenu;
            undoMenu.ShortcutKeyDisplayString = "Ctrl + Z";
            undoMenu.Click += (a, b) =>
            {
                editor.Undo();
            };

            // retro menu 
            redoMenu.Text = Resource.redoMenu;
            redoMenu.ShortcutKeyDisplayString = "Ctrl + Y";
            redoMenu.Click += (a, b) =>
            {
                editor.Redo();
            };

            // select all menu 
            selectAllMenu.Text = Resource.selectAllMenu;
            selectAllMenu.ShortcutKeyDisplayString = "Ctrl + A";
            selectAllMenu.Click += (a, b) =>
            {
                editor.SelectAll();
            };

            // remove menu 
            removeMenu.Text = Resource.removeMenu;
            removeMenu.ShortcutKeyDisplayString = "Del";
            removeMenu.Click += (a, b) =>
            {
                if (!string.IsNullOrEmpty(editor.SelectedText))
                    editor.SelectedText = "";
            };

            // wordWrap menu 
            wordWrapMenu.Text = Resource.wordWrapMenu;
            wordWrapMenu.Click += (a, b) =>
            {
                editor.WordWrap = !editor.WordWrap;
            };

            menu.Opened += (o, e) =>
            {
                editXmlValueMenu.Visible = !string.IsNullOrEmpty(editor.SelectedText);
                editJsonValueMenu.Visible = !string.IsNullOrEmpty(editor.SelectedText);

                jsonMenu.Enabled = editor.EnableFormatters;
                xmlMenu.Enabled = editor.EnableFormatters;
                searchMenu.Enabled = editor.EnableSearch;

                if (editor.WordWrap)
                    wordWrapMenu.Image = Properties.Resources.check;
                else
                    wordWrapMenu.Image = null;
            };

            return menu;
        }

        public static bool KeyIsWritable(Keys key)
        {
            var keysNotWrite = new Keys[]
            {
                Keys.ControlKey,
                Keys.Alt,
                Keys.ShiftKey,
                Keys.LShiftKey,
                Keys.RShiftKey,
                Keys.Menu,
                Keys.LMenu,
                Keys.RMenu,
                Keys.Apps,
                Keys.CapsLock,
                Keys.Control,
                Keys.VolumeDown,
                Keys.VolumeMute,
                Keys.VolumeUp,
                Keys.XButton1,
                Keys.XButton2,
                Keys.Zoom,
                Keys.Attn,
                Keys.Capital,
                Keys.F1,
                Keys.F2,
                Keys.F3,
                Keys.F4,
                Keys.F5,
                Keys.F6,
                Keys.F7,
                Keys.F8,
                Keys.F9,
                Keys.F10,
                Keys.F11,
                Keys.F12,
                Keys.F13,
                Keys.F14,
                Keys.F15,
                Keys.F16,
                Keys.F17,
                Keys.F18,
                Keys.F19,
                Keys.F20,
                Keys.F21,
                Keys.F22,
                Keys.F23,
                Keys.F24,
                Keys.LWin,
                Keys.RWin,
                Keys.Escape,
                Keys.Down,
                Keys.Up,
            };

            return !keysNotWrite.Contains(key);
        }
    }
}
