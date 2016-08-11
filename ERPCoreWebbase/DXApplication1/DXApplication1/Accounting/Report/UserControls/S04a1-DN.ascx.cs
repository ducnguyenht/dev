using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using System.Data;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using NAS.DAL.System.ShareDim;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting;
using DevExpress.Web.ASPxGridView;
using DevExpress.XtraPrintingLinks;
using DevExpress.Web.ASPxHiddenField;
using DevExpress.Xpo.DB;
using System.Globalization;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04a1_DN : System.Web.UI.UserControl
    {
        #region the first

        Session session;
        string sql;
        SelectedData seletectedData;
        string codeBegin;
        GridViewBandColumn bandColumn;
        int maxMonth;
        DataRow line;
        double total;
        int widthEndCol;
        string space;

        string m_Sql = "";
       
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (hS04a1dn.Contains("show"))
            {
                //try 
                //{ 
                load_data();
                //hS04a1dn.Remove("show");
                //}
                //catch { }

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        #endregion

        public void load_data()
        {

            // Begin TUAN sua theo du lieu ERPCORE
            int month = Int32.Parse(hS04a1dnMonth.Get("month_id").ToString());
            int year = Int32.Parse(hS04a1dnYear.Get("year_id").ToString());
            string owner = hS04a1dnOwner.Get("owner_id").ToString();
            string asset = hS04a1dnAsset.Get("asset_id").ToString();
            
            WebModule.Accounting.Report.S04a1_DN ctd = new WebModule.Accounting.Report.S04a1_DN();
            ctd.Label_month.Text = month.ToString();
            ctd.Label_year.Text = year.ToString();
         
            m_Sql = "" +
                "select distinct d.IssueDate, b.Debit, e.Code " +
                "from DiaryJournal_Fact a, " +
                        "DiaryJournal_Detail b, " +
                        "FinancialAccountDim c, " +
                        "FinancialTransactionDim d, " +
                        "CorrespondFinancialAccountDim e " +
                "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
                "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
                "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
                "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
                "and c.Code like '111%' " +
                "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) = " + year.ToString() + ") " +
                "and b.Debit > 0 " +
                "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
                "and (b.FinancialAccountDimId is null) " +
                "and e.Code not in ('1','2','3','4','5','6','7','8') " +
                "order by d.IssueDate ";

            // header
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("stt");
            dataTable.Columns.Add("ngay");

            try
            {
                seletectedData = session.ExecuteQuery(m_Sql);
            }
            catch
            {
            }

            GridViewDataColumn caption = new GridViewDataColumn();
            caption.Caption = "Số TT";
            caption.FieldName = "stt";
            S04a11dnASPxGridView.Columns.Add(caption);

            caption = new GridViewDataColumn();
            caption.Caption = "Ngày";
            caption.FieldName = "ngay";
            S04a11dnASPxGridView.Columns.Add(caption);

            bandColumn = new GridViewBandColumn("Ghi có tài khoản 111, ghi Nợ các tài khoản");

            S04a11dnASPxGridView.Columns.Add(bandColumn);   

            List<int> listHeader = new List<int>();
            

            foreach (var row in seletectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    if (!listHeader.Contains(Int32.Parse(col.Values[2].ToString())))
                    {                        
                        listHeader.Add(Int32.Parse(col.Values[2].ToString()));
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
            caption.Caption = space + "Cộng có TK 111";
            caption.FieldName = "total";
            caption.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(caption);

            dataTable.Columns.Add("total", typeof(double));

            // data
            index = 0;

            double amount = 0;
            String dateBegin = new String(' ', 10);
            String codeBegin = new String(' ', 10);

            foreach (var row in seletectedData.ResultSet)
            {
                
                foreach (var col in row.Rows)
                {
                    if (!dateBegin.Substring(0, 10).Equals(col.Values[0].ToString().Substring(0, 10)))
                    {
                        line = dataTable.NewRow();

                        dateBegin = col.Values[0].ToString();
                        index++;

                        line[0] = index;
                        dataTable.Rows.Add(line);
                       
                        total = 0;
                    }

                    line["ngay"] = DateTime.Parse(col.Values[0].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    
                    amount = double.Parse(col.Values[1].ToString());
                    total += double.Parse(col.Values[1].ToString());

                    line[col.Values[2].ToString()] = (line[col.Values[2].ToString()].ToString().Trim() == "" ? 0 : double.Parse(line[col.Values[2].ToString()].ToString())) + amount;
                    line["total"] = total;

                }

            }

            // sum total

            widthEndCol = 0;

            line = dataTable.NewRow();
            line["ngay"] = "Cộng";
            dataTable.Rows.Add(line);

            codeBegin = "";

            for (int i = 2; i < dataTable.Columns.Count - 1; i++)
            {
                line[dataTable.Columns[i].ColumnName] = dataTable.Compute("Sum([" + dataTable.Columns[i].ColumnName + "])", string.Empty);
                widthEndCol += line[dataTable.Columns[i].ColumnName].ToString().Length;
            }

            line["total"] = dataTable.Compute("Sum([total])", string.Empty);


            if (240 - widthEndCol * 3 > 0)
            {
                space = new String(' ', 240 - widthEndCol * 3);
            }
            S04a11dnASPxGridView.Columns["total"].Caption = space + "Cộng Có TK 111";

            S04a11dnASPxGridView.DataSource = dataTable;
            S04a11dnASPxGridView.KeyFieldName = "stt";
            S04a11dnASPxGridView.DataBind();
          
            ctd.pccData1111.PrintableComponent = new PrintableComponentLinkBase() { Component = S04a1dnASPxGridViewExporter };
            S04a1dnReportViewer.Report = ctd;            
                                
        }
   

        protected void ASPxGridViewExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {         
        }
    }
}