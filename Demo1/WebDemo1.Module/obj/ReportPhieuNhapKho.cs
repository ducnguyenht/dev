using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using WebDemo1.Module.BusinessObjects.DBWebDemo;

namespace WebDemo1.Module.Report
{
    public partial class ReportPhieuNhapKho : DevExpress.XtraReports.UI.XtraReport
    {//b1
        public ReportPhieuNhapKho()
        {
            InitializeComponent();
        }

        private void ReportPhieuNhapKho_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DevExpress.XtraReports.Parameters.Parameter param =
                    (DevExpress.XtraReports.Parameters.Parameter)
                    ((DevExpress.XtraReports.UI.XtraReport)sender).Parameters["XafReportParametersObject"];

            if (param != null)
            {
                ReportPhieuNhapKho_Param paramEmployee = (ReportPhieuNhapKho_Param)param.Value;

                Employee targetEmployee = paramEmployee.Employee;

                this.DataSource = new List<Employee>() { targetEmployee };
                this.DataSource = viewDataSource1;
            }
          
            xrLabel15.Text = "Ngày " + DateTime.Now.Day.ToString() + " tháng " + DateTime.Now.Month.ToString() + " năm " + DateTime.Now.Year.ToString();
            //var datasource = new List<DataItem>() { 
            //    new DataItem() { Code = "Item1", Quantity = 10 },
            //    new DataItem() { Code = "Item2", Quantity = 10 },
            //    new DataItem() { Code = "Item3", Quantity = 10 },
            //    new DataItem() { Code = "Item4", Quantity = 10 },
            //    new DataItem() { Code = "Item5", Quantity = 10 }
            //};

            //this.DataSource = datasource;
        }

    }

    public class DataItem
    {
        public string Code { get; set; }
        public decimal Quantity { get; set; }
    }
}
