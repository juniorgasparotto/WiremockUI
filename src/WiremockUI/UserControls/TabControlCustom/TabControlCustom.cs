using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace WiremockUI
{
    public class TabControlCustom : TabControl
    {
        private Image CloseImage;
        public new TabPageCollectionCustom TabPages { get; private set; }

        public TabControlCustom()
        {
            InitializeComponent();
            this.TabPages = new TabPageCollectionCustom(this);
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.DrawItem += OnDrawItemCustom;
            this.MouseClick += OnMouseClickCustom;
            CloseImage = Properties.Resources.ImageClose;
            this.Padding = new Point(20, 5);
        }

        private void OnDrawItemCustom(object sender, DrawItemEventArgs e)
        {
            try
            {
                var tabRect = this.GetTabRect(e.Index);
                tabRect.Inflate(-2, -2);

                var imageRect = new Rectangle(tabRect.Right - CloseImage.Width,
                                         tabRect.Top + (tabRect.Height - CloseImage.Height) / 2,
                                         CloseImage.Width,
                                         CloseImage.Height);

                Font fntTab;
                Brush bshBack;
                Brush bshFore;

                if (e.Index == this.SelectedIndex)
                {
                    fntTab = new Font(e.Font, FontStyle.Bold);
                    bshBack = new SolidBrush(Color.Gainsboro);
                    bshFore = new SolidBrush(Color.Gainsboro);
                    e.Graphics.FillRectangle(bshBack, e.Bounds);
                }

                var sf = new StringFormat(StringFormat.GenericDefault);
                
                e.Graphics.DrawString(this.TabPages[e.Index].Text,
                                      this.Font, Brushes.Black, tabRect, sf);
                e.Graphics.DrawImage(CloseImage, imageRect.Location);
            }
            catch (Exception) { }
        }

        private void OnMouseClickCustom(object sender, MouseEventArgs e)
        {
            for (var i = 0; i < this.TabPages.Count; i++)
            {
                var tabPage = (TabPageCustom)this.TabPages[i];
                var tabRect = this.GetTabRect(i);
                tabRect.Inflate(-2, -2);
                var imageRect = new Rectangle(tabRect.Right - CloseImage.Width,
                                         tabRect.Top + (tabRect.Height - CloseImage.Height) / 2,
                                         CloseImage.Width,
                                         CloseImage.Height);
                if (imageRect.Contains(e.Location))
                {
                    if (tabPage.CanClose == null || tabPage.CanClose())
                    {
                        this.TabPages.RemoveAt(i);
                        SelectLastTab();
                    }
                    break;
                }
            }
        }


        public void SelectLastTab()
        {
            if (this.TabPages.Count > 0)
            { 
                this.SelectedTab = this.TabPages[this.TabPages.Count - 1];
            }
        }

        private static Rectangle GetRTLCoordinates(Rectangle container, Rectangle drawRectangle)
        {
            return new Rectangle(
                container.Width - drawRectangle.Width - drawRectangle.X,
                drawRectangle.Y,
                drawRectangle.Width,
                drawRectangle.Height);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && SelectedTab != null)
            {
                var tab = (TabPageCustom)SelectedTab;
                if (tab.CanClose == null || tab.CanClose())
                {
                    this.TabPages.Remove(tab);
                    SelectLastTab();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void TabControlCustom_MouseClick(object sender, MouseEventArgs e)
        {
            var tabControl = sender as TabControl;
            var tabs = tabControl.TabPages;

            if (e.Button == MouseButtons.Middle)
            {
                if (SelectedTab != null)
                {
                    var tab = tabs.Cast<TabPageCustom>()
                        .Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location))
                        .FirstOrDefault();
                    if (tab != null && tab.CanClose == null || tab.CanClose())
                    {
                        tabs.Remove(tab);
                        SelectLastTab();
                    }
                }
            }
        }

        public class TabPageCollectionCustom : IList<TabPageCustom>
        {
            private TabControlCustom tabControl;
            private TabControl.TabPageCollection tabPages;

            public TabPageCollectionCustom(TabControlCustom tabControl)
            {
                this.tabControl = tabControl;
                this.tabPages = ((TabControl)tabControl).TabPages;
            }

            public TabPageCustom this[int index]
            {
                get => this.tabPages[index] as TabPageCustom;
                set => this.tabPages[index] = value;
            }

            public int Count => this.tabPages.Count;

            public bool IsReadOnly => this.tabPages.IsReadOnly;

            public void Add(TabPageCustom item)
            {
                this.tabPages.Add(item);
            }

            public bool CloseAll()
            {
                while (this.tabControl.SelectedTab != null)
                {
                    var selectedTab = this.tabControl.SelectedTab as TabPageCustom;
                    if (selectedTab.CanClose != null && !selectedTab.CanClose())
                        return false;
                    else
                        Remove(selectedTab);
                }

                return true;
            }

            public void Clear()
            {
                this.tabPages.Clear();
            }

            public bool Contains(TabPageCustom item)
            {
                return this.tabPages.Contains((TabPage)item);
            }

            public void CopyTo(TabPageCustom[] array, int arrayIndex)
            {
                ((IList)this.tabPages).CopyTo(array, arrayIndex);
            }

            public IEnumerator<TabPageCustom> GetEnumerator()
            {
                return tabPages.Cast<TabPageCustom>().GetEnumerator();
            }

            public int IndexOf(TabPageCustom item)
            {
                return tabPages.IndexOf(item);
            }

            public void Insert(int index, TabPageCustom item)
            {
                tabPages.Insert(index, item);
            }

            public bool Remove(TabPageCustom item)
            {
                tabPages.Remove(item);
                return true;
            }

            public void RemoveAt(int index)
            {
                if (tabPages.Count > index)
                    tabPages.RemoveAt(index);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return tabPages.GetEnumerator();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // TabControlCustom
            // 
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabControlCustom_MouseClick);
            this.ResumeLayout(false);

        }

    }
}
