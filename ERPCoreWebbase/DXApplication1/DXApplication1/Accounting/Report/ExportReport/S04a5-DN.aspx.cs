using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting;
using DevExpress.Data.Filtering;

namespace WebModule.Accounting.Report.ExportReport
{
    public partial class S04a5_DN : System.Web.UI.Page
    {
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            //load_combobox_month();
            //load_combobox_year();
            //load_combobox_asset();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void load_combobox_month()
        {
            ListEditItem defaultItem = new ListEditItem("Select One", -1);
            xComboBox_month.Items.Insert(0, defaultItem);
            xComboBox_month.SelectedIndex = 0;

            XPView view = new XPView(session, typeof(MonthDim));
            view.Properties.AddRange(new[] { new ViewProperty("name", DevExpress.Xpo.SortDirection.None, "[Name]", false, true) });

            foreach (ViewRecord mini_view in view)
            {
                xComboBox_month.Items.Add(mini_view[0].ToString(), mini_view[0].ToString());
            }

        }

        public void load_combobox_year()
        {
            ListEditItem defaultItem = new ListEditItem("Select One", -1);
            xComboBox_year.Items.Insert(0, defaultItem);
            xComboBox_year.SelectedIndex = 0;

            XPView view = new XPView(session, typeof(YearDim));

            view.Properties.AddRange(new[] { new ViewProperty("name", DevExpress.Xpo.SortDirection.None, "[Name]", false, true) });

            foreach (ViewRecord mini_view in view)
            {
                xComboBox_year.Items.Add(mini_view[0].ToString(), mini_view[0].ToString());
            }
        }

        public void load_combobox_asset()
        {
            ListEditItem defaultItem = new ListEditItem("Select One", -1);
            xComboBox_Currency.Items.Insert(0, defaultItem);
            xComboBox_Currency.SelectedIndex = 0;

            XPView view = new XPView(session, typeof(FinancialAssetDim));
            view.Properties.AddRange(new[] { new ViewProperty("name", DevExpress.Xpo.SortDirection.None, "[Name]", false, true) });
            foreach (ViewRecord mini_view in view)
            {
                xComboBox_Currency.Items.Add(mini_view[0].ToString(), mini_view[0].ToString());
            }
        }
    }
}