using System;
using System.Windows.Forms;

namespace WiremockUI
{
    public class TabMaster
    {
        private FormMaster master;
        private TabControlCustom tabControl;

        public class Tag
        {
            public object InternalTag { get; set; }
            public Form Form { get; set; }
        }

        public TabMaster(FormMaster master)
        {
            this.master = master;
            this.tabControl = master.GetTabControl();
        }

        public TabPageCustom AddTab(Form form, object tag, string text)
        {
            var tabpage = new TabPageCustom { Text = text };
            tabpage.Tag = new Tag { Form = form, InternalTag = tag };

            tabpage.BorderStyle = BorderStyle.Fixed3D;
            tabControl.TabPages.Add(tabpage);
            form.TopLevel = false;
            form.Parent = tabpage;
            form.Show();
            form.Dock = DockStyle.Fill;
            tabControl.SelectedTab = tabpage;
            return tabpage;
        }

        public void CloseTab(TabPageCustom tab)
        {
            tabControl.TabPages.Remove(tab);
            tabControl.SelectLastTab();
        }

        public void CloseTab(object tag)
        {
            var tab = GetTabByInternalTag(tag);
            if (tab != null)
            {
                ((Tag)tab.Tag).Form.Close();
                tabControl.TabPages.Remove(tab);
                tabControl.SelectLastTab();
            }
        }

        public void CloseTab(Form form)
        {
            if (form.Parent is TabPage)
            {
                var tabPage = (TabPage)form.Parent;
                var tab = (TabControl)tabPage.Parent;
                tab.TabPages.Remove(tabPage);
                tabControl.SelectLastTab();
            }

            form.Close();
        }

        public TabPageCustom GetTabByInternalTag(object tag)
        {
            foreach (TabPageCustom t in tabControl.TabPages)
            {
                var internalTag = ((Tag)t.Tag).InternalTag;
                if (internalTag != null && internalTag.Equals(tag))
                    return t;
            }

            return null;
        }

        public TabPageCustom GetTabByText(string text)
        {
            foreach (TabPageCustom t in tabControl.TabPages)
            {
                if (t.Text == text)
                    return t;
            }

            return null;
        }

        public Tag GetTag(TabPageCustom tab)
        {
            return (Tag)tab.Tag;
        }


        public Form GetForm(TabPageCustom tab)
        {
            return ((TabMaster.Tag)tab.Tag).Form;
        }

        internal void SelectTab(TabPageCustom tab)
        {
            this.tabControl.SelectedTab = tab;
        }
    }
}
