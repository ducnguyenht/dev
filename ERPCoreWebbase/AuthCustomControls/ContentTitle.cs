using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace AuthCustomControls
{
    [DefaultProperty("Title")]
    [ToolboxData("<{0}:ContentTitle runat=server></{0}:ContentTitle>")]
    public class ContentTitle : WebControl
    {
        public ContentTitle()
        {
            _parentTitle = new Label();
            _parentTitle.Font.Size = new FontUnit("16px");
            _parentTitle.ForeColor = Color.FromArgb(130, 130, 130);
            _parentTitle.Style.Add("display", "block");
            _title = new Label();
            _title.Font.Size = new FontUnit("26px");
            _title.Style.Add("display", "block");
            _title.Style.Add("margin-top", "-2px");
            this.Style.Add("display", "block");
            this.Controls.Add(_parentTitle);
            this.Controls.Add(_title);
            _parentTitle.Style.Add("font-family", "'Segoe UI', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif");
            _title.Style.Add("font-family", "'Segoe UI', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif");
        }

        private Label _parentTitle;
        private Label _title;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("ParentTitle")]
        [Localizable(true)]
        public string ParentTitle
        {
            get
            {
                String s = (String)ViewState["ParentTitle"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                _parentTitle.Text = value;
                ViewState["ParentTitle"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("16px")]
        [Localizable(true)]
        public FontUnit ParentTitleSize
        {
            get
            {
                return _parentTitle.Font.Size;
            }

            set
            {
                _parentTitle.Font.Size = value;
                ViewState["ParentTitleSize"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("26px")]
        [Localizable(true)]
        public FontUnit TitleSize
        {
            get
            {
                return _title.Font.Size;
            }

            set
            {
                _title.Font.Size = value;
                ViewState["TitleSize"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("Title")]
        [Localizable(true)]
        public string Title
        {
            get
            {
                String s = (String)ViewState["Title"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                _title.Text = value;
                ViewState["Title"] = value;
            }
        }

    }
}
