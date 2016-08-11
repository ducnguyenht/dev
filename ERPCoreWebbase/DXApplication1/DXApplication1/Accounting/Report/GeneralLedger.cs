using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace WebModule.Accounting.Report
{
    public partial class GeneralLedger : DevExpress.XtraReports.UI.XtraReport
    {
        public GeneralLedger()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
