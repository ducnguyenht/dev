using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Web.ASPxGridView;
using System.Data;
using NAS.DAL;
using DevExpress.XtraPrintingLinks;
using System.Globalization;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04a9_DN : System.Web.UI.UserControl
    {
        Session session;
        string sql;
        SelectedData seletectedData;
        string codeBegin = "";
        GridViewBandColumn bandColumn;
        int maxMonth;
        DataRow line;
        double total;
        int widthEndCol;
        string space;

        string m_Sql = "";

        public void load_data()
        {
            int month = Int32.Parse(hS04a9dnMonth.Get("month_id").ToString());
            int year = Int32.Parse(hS04a9dnYear.Get("year_id").ToString());
            string owner = hS04a9dnOwner.Get("owner_id").ToString();
            string asset = hS04a9dnAsset.Get("asset_id").ToString();
            string account = hS04a9dnAccount.Get("account_id").ToString();


            //account = "213";

            m_Sql = "" +
            "select distinct d.Name, d.IssueDate, isnull(d.Description,'') as [description], b.Debit, " +
		            "e.Code as DebitAccount, c.Code as CreditAccount " +
            "from DiaryJournal_Fact a, " +
		            "DiaryJournal_Detail b, " +
		            "FinancialAccountDim c, " +
		            "FinancialTransactionDim d, " +
		            "CorrespondFinancialAccountDim e " +  		
            "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
            "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
            "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
            "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
            "and c.Code like '" + account + "%' " +
            "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) =  " + year.ToString() + " ) " +
            "and b.Debit > 0 " +
            "and (len(e.Code) > 0)" +
            "and (b.FinancialAccountDimId is null) " +
            "and e.Code not in ('1','2','3','4','5','6','7','8') " +
            "order by d.IssueDate ";
               

            // header
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("stt");
            dataTable.Columns.Add("sohieu");
            dataTable.Columns.Add("ngay");            
            dataTable.Columns.Add("diengiai");

            GridViewDataColumn caption = new GridViewDataColumn();
            caption.Caption = "Số TT";
            caption.FieldName = "stt";
            S04a9dnASPxGridView.Columns.Add(caption);

            bandColumn = new GridViewBandColumn("Chứng từ");
            S04a9dnASPxGridView.Columns.Add(bandColumn);

            caption = new GridViewDataTextColumn();
            caption.Caption = "Số hiệu";
            caption.FieldName = "sohieu";
            bandColumn.Columns.Add(caption);

            caption = new GridViewDataTextColumn();
            caption.Caption = "Ngày";
            caption.FieldName = "ngay";
            bandColumn.Columns.Add(caption);


            caption = new GridViewDataColumn();
            caption.Caption = "Diễn giải";
            caption.FieldName = "diengiai";
            S04a9dnASPxGridView.Columns.Add(caption);

            bandColumn = new GridViewBandColumn("Ghi có tài khoản " + account + " Ghi nợ các tài khoản");
            S04a9dnASPxGridView.Columns.Add(bandColumn);


            try
            {
                seletectedData = session.ExecuteQuery(m_Sql);
            }
            catch
            {
            }

            List<int> listHeader = new List<int>();

            foreach (var row in seletectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    if (!listHeader.Contains(Int32.Parse(col.Values[4].ToString())))
                    {
                        listHeader.Add(Int32.Parse(col.Values[4].ToString()));
                    }
                }
            }

            listHeader.Sort();
            int index;

            for (index = 0; index < listHeader.Count; index++)
            {
                GridViewDataTextColumn c = new GridViewDataTextColumn();
                c.Caption = listHeader[index].ToString();
                c.FieldName = listHeader[index].ToString();
                c.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(c);                
                dataTable.Columns.Add(listHeader[index].ToString(), typeof(double));
            }

            caption = new GridViewDataTextColumn();
            String space = new String(' ', 240 - maxMonth * 20);
            caption.Caption = space + "Cộng";
            caption.FieldName = "total";
            caption.PropertiesEdit.DisplayFormatString = "#,#";            
            bandColumn.Columns.Add(caption);

            dataTable.Columns.Add("total", typeof(double));

            // data
            index = 0;

            double amount = 0;
            codeBegin = new String(' ', 10);

            foreach (var row in seletectedData.ResultSet)
            {

                foreach (var col in row.Rows)
                {
                    if (!codeBegin.Equals(col.Values[0].ToString()))
                    {
                        line = dataTable.NewRow();

                        codeBegin = col.Values[0].ToString();
                        index++;

                        line[0] = index;
                        dataTable.Rows.Add(line);

                        total = 0;
                    }


                    line["sohieu"] = col.Values[0].ToString();
                    line["ngay"] = DateTime.Parse(col.Values[1].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    line["diengiai"] = col.Values[2].ToString();

                    amount = double.Parse(col.Values[3].ToString());
                    total += double.Parse(col.Values[3].ToString());

                    line[col.Values[4].ToString()] = amount;
                    line["total"] = total;

                }

            }

            // sum total

            widthEndCol = 0;

            line = dataTable.NewRow();
            line["diengiai"] = "Cộng";
            dataTable.Rows.Add(line);

            codeBegin = "";

            for (int i = 4; i < dataTable.Columns.Count; i++)
            {
                line[dataTable.Columns[i].ColumnName] = dataTable.Compute("Sum([" + dataTable.Columns[i].ColumnName + "])", string.Empty);
                widthEndCol += line[dataTable.Columns[i].ColumnName].ToString().Length;
            }

            //line["total"] = dataTable.Compute("Sum([total])", string.Empty);


            //if (240 - widthEndCol * 3 > 0)
            //{
            //    space = new String(' ', 240 - widthEndCol * 3);
            //}
            S04a9dnASPxGridView.Columns["total"].Caption = space + "Cộng có TK " + account;

            S04a9dnASPxGridView.DataSource = dataTable;
            S04a9dnASPxGridView.KeyFieldName = "stt";
            S04a9dnASPxGridView.DataBind();

            WebModule.Accounting.Report.S04a9_DN ctd = new WebModule.Accounting.Report.S04a9_DN();
            ctd.pccData.PrintableComponent = new PrintableComponentLinkBase() { Component = S04a9dnASPxGridViewExporter };
            ctd.Parameters["accountName"].Value = account;
            ctd.Parameters["datePeriod"].Value = new DateTime(year, month, 1);

            S04a9dnReportViewer.Report = ctd;
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (hS04a9dn.Contains("show"))
            {
                load_data();
            }
        }
    }
}