using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.Xpo;
using NAS.DAL.Invoice;
using NAS.DAL.Nomenclature.Item;

namespace ERPCore.Sales.Report
{
    public partial class _01GTKT3_001 : DevExpress.XtraReports.UI.XtraReport
    {
        public _01GTKT3_001()
        {
            InitializeComponent();
        }

        Session session;

        private void _01GTKT3_001_DataSourceDemanded(object sender, EventArgs e)
        {
           
        }

    }
}
