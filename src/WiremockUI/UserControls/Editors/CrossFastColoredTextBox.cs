namespace WiremockUI
{
#if Linux
    using FastColoredTextBoxNS;
    using System;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class CrossSyntaxHighlighter
    {
        public static RegexOptions RegexCompiledOption { get; internal set; }
    }

    public class CrossRange
    {
        internal void SetStyle(Style jsonKeyStyle, Regex jsonKeyRegex)
        {
            
        }
    }

    public class CrossTextChangedEventArgs : EventArgs
    {
        public CrossTextChangedEventArgs(CrossRange changedRange)
        {
        }

        public CrossRange ChangedRange { get; set; }
    }

    public class CrossFastColoredTextBox : SimpleEditor, System.ComponentModel.ISupportInitialize
    {
        public bool IsChanged { get; internal set; }
        public Language Language { get; internal set; }
        public Action<object, TextChangedEventArgs> TextChangedDelayed { get; internal set; }
        public CrossRange Range { get; internal set; } = new CrossRange();
        public char[] AutoCompleteBracketsList { get; internal set; }
        public bool AutoIndentExistingLines { get; internal set; }
        public object BackBrush { get; internal set; }
        public int CharHeight { get; internal set; }
        public int CharWidth { get; internal set; }
        public Color DisabledColor { get; internal set; }
        public string Hotkeys { get; internal set; }
        public bool IsReplaceMode { get; internal set; }
        public Padding Paddings { get; internal set; }
        public ServiceColors ServiceColors { get; internal set; }
        public int Zoom { get; internal set; }

        public new string Text
        {
            get => this.TextValue;
            set => this.TextValue = value;
        }

        public CrossFastColoredTextBox()
        {
            
        }

        internal void ClearStyle(StyleIndex all)
        {
            
        }

        internal void ClearStylesBuffer()
        {
            
        }

        internal void OnSyntaxHighlight(CrossTextChangedEventArgs textChangedEventArgs)
        {
            
        }

        public void BeginInit()
        {
           
        }

        public void EndInit()
        {
            
        }

        internal void ClearUndo()
        {
            
        }
    }
#else
    using FastColoredTextBoxNS;

    public class CrossRange : Range
    {
        public CrossRange(FastColoredTextBox tb) : base(tb)
        {
        }
    }

    public class CrossTextChangedEventArgs : TextChangedEventArgs
    {
        public CrossTextChangedEventArgs(Range changedRange) : base(changedRange)
        {
        }
    }

    public class CrossFastColoredTextBox : FastColoredTextBox
    {

    }

    public class CrossSyntaxHighlighter : SyntaxHighlighter
    {

    }
#endif
}
