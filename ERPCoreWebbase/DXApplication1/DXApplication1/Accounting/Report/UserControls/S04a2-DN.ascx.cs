using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using System.Data;
using NAS.DAL.BI.Actor;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting;
using DevExpress.Web.ASPxGridView;
using DevExpress.XtraPrintingLinks;
using DevExpress.Xpo.DB;
using System.Globalization;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class S04a2_DN : System.Web.UI.UserControl
    {
        #region Page_Init
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();            
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (hS04a2dn.Contains("show"))
            {
                load_data();                
            }
        }

        #region Method Lấy TK đối ứng

        //get list Contra Account with parameter: creditAccount, month, year
        public DataTable DataTable_ContraAccount(int creditAccount, string owner, int month, int year, string currency)
        {
            DataTable ContraAccount = new DataTable();

            XPCollection<OwnerOrgDim> ood = new XPCollection<OwnerOrgDim>(session, CriteriaOperator.Parse("Name = '" + owner + "'"));
            XPCollection<FinancialCreditAccountDim> fcad = new XPCollection<FinancialCreditAccountDim>(session, CriteriaOperator.Parse("Name like '" + creditAccount + "%' "));
            XPCollection<MonthDim> md = new XPCollection<MonthDim>(session, CriteriaOperator.Parse("Name= '" + month + "' "));
            XPCollection<YearDim> yd = new XPCollection<YearDim>(session, CriteriaOperator.Parse("Name= '" + year + "' "));
            XPCollection<FinancialAssetDim> fad = new XPCollection<FinancialAssetDim>(session, CriteriaOperator.Parse("Name = '" + currency + "'"));
            if (fcad.Count > 0 && md.Count > 0 && yd.Count > 0 && ood.Count > 0 && fad.Count > 0)
            {
                foreach (FinancialCreditAccountDim each_fcad in fcad)
                {
                    XPCollection<FinancialDoubleEntry_Fact> fdef_by3Id = new XPCollection<FinancialDoubleEntry_Fact>(session, CriteriaOperator.Parse("FinancialCreditAccountDimId = '" + each_fcad.FinancialCreditAccountDimId + "' AND MonthDimId = '" + md[0].MonthDimId + "' AND YearDimId = '" + yd[0].YearDimId + "' AND FinancialAssetDimId = '" + fad[0].FinancialAssetDimId + "' AND OwnerOrgDimId = '" + ood[0].OwnerOrgDimId + "'"));

                    foreach (FinancialDoubleEntry_Fact each_fdef_by3Id in fdef_by3Id)
                    {
                        XPCollection<FinancialTransactionDim> ftd_byName = new XPCollection<FinancialTransactionDim>(session, CriteriaOperator.Parse("Name = '" + each_fdef_by3Id.FinancialTransactionDimId.Name + "'"));

                        foreach (FinancialTransactionDim ftd in ftd_byName)
                        {
                            if (ftd.FinancialTransactionDimId != each_fdef_by3Id.FinancialTransactionDimId.FinancialTransactionDimId)
                            {
                                XPCollection<FinancialDoubleEntry_Fact> fdef = new XPCollection<FinancialDoubleEntry_Fact>(session, CriteriaOperator.Parse("FinancialTransactionDimId= '" + ftd.FinancialTransactionDimId + "' "));

                                string debit = fdef[0].FinancialDebitAccountDimId.Name;
                                try { ContraAccount.Columns.Add(debit); }
                                catch { continue; }
                            }
                        }
                    }
                }
            }

            return ContraAccount;
        }

        #endregion

        #region Method tạo header(columns) cho gridview(khi có danh sách TK đối ứng) (có **** SPACE ****)
        public void ColumnsForGridview(int creditAccount, string owner, int month, int year, string currency)
        {
            //khai báo space, cân chỉnh report
            // int a = DataTable_ContraAccount(creditAccount, owner, month, year, currency).Columns.Count;
            // String space = new String(' ', 180 - a*5);

            //1.Add Column1:"Số TT"              
            GridViewColumn column1 = new GridViewDataTextColumn { FieldName = "Số TT", Caption = "Số TT" };
            ASPxGridViewS04a2.Columns.Add(column1);

            //2.Add Column2:"Chứng từ" là BandColumn chứa các column:"Số hiệu", "Ngày,tháng"
            GridViewColumn column2 = new GridViewBandColumn("Chứng từ");
            (column2 as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "Số hiệu" });
            (column2 as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "Ngày,tháng" });
            ASPxGridViewS04a2.Columns.Add(column2);

            //3.Add Column3:"Diễn Giải"
            GridViewColumn column3 = new GridViewDataTextColumn { FieldName = "Diễn giải" };
            ASPxGridViewS04a2.Columns.Add(column3);

            //4.Add Column4 là BandColumns("Tài khoản có 112, Nợ các tài khoản...") chứa các Column:"TK đối ứng"
            GridViewColumn column4 = new GridViewBandColumn("Tài Khoản Có " + creditAccount + ", Nợ các tài khoản");
            //lấy ra tên các tài khoản đối ứng tìm được ở trên, cho vào các columns: "nợ các tài khoản..."
            foreach (DataColumn ContraAccount in DataTable_ContraAccount(creditAccount, owner, month, year, currency).Columns)
            {
                (column4 as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = ContraAccount.ColumnName, Caption = ContraAccount.ColumnName });
            }
            //ASPxGridViewS04a2.Columns.Add(column4);
            //5.Add Colum5:"Cộng có TK"
            (column4 as GridViewBandColumn).Columns.Add(new GridViewDataTextColumn { FieldName = "Cộng có TK " + creditAccount + "", Caption = space + "Cộng có TK " + creditAccount });//add column:"Cộng có tài khoản 112"
            ASPxGridViewS04a2.Columns.Add(column4);
        }

        #endregion

        #region Method tạo dòng đầu cho gridview (Có thêm **** SPACE **** )
        public DataTable DataTable_FirstRow(int creditAccount, string owner, int month, int year, string currency)
        {
            //khai báo space, cân chỉnh report
            //int a = DataTable_ContraAccount(creditAccount, owner, month, year, currency).Columns.Count;
            //String space = new String(' ', 180 - a * 5 + 12 );

            //1.------Create new DataTable----------
            DataTable datatable_FR = new DataTable();
            //2.------Add Columns for datatable_FR-------
            datatable_FR.Columns.Add("Số TT");
            datatable_FR.Columns.Add("Số hiệu");
            datatable_FR.Columns.Add("Ngày,tháng");
            datatable_FR.Columns.Add("Diễn giải");
            foreach (DataColumn ContraAccount in DataTable_ContraAccount(creditAccount, owner, month, year, currency).Columns)
            {
                datatable_FR.Columns.Add(ContraAccount.ColumnName);//Add Column:"TK đối ứng"
            }
            datatable_FR.Columns.Add("Cộng có TK " + creditAccount + "");//Add Column:"Cộng có TK"

            //Create data row for datatable_FR
            DataRow datarow_FR = datatable_FR.NewRow();

            datarow_FR["Số TT"] = "A";//tại column :"Số TT" Add giá trị "A"
            datarow_FR["Số hiệu"] = "B";
            datarow_FR["Ngày,tháng"] = "C";
            datarow_FR["Diễn giải"] = "D";

            int i = 1;
            for (int j = 4; j < datatable_FR.Columns.Count - 1; j++)//a = số Columns của TK đối ứng
            {
                datarow_FR[datatable_FR.Columns[j]] = i++;//
            }
            //3.-----add row for datatable_FR-------
            datatable_FR.Rows.Add(datarow_FR);

            #region Thêm **** SPACE **** để cân chỉnh report(first row)
            //add row cho column cuối
            datarow_FR["Cộng có TK " + creditAccount + ""] = space2 + (datatable_FR.Columns.Count - 4);
            #endregion

            return datatable_FR;
        }
        #endregion

        #region Method load_data
        //Khai Báo biến cục bộ(dùng ở nhiều nơi)
        int ContraAccount_Count;
        string space;
        string space2;

        private void load_data()
        {
            int month = Int32.Parse(hS04a2dnMonth.Get("month_id").ToString());
            int year = Int32.Parse(hS04a2dnYear.Get("year_id").ToString());
            string owner = hS04a2dnOwner.Get("owner_id").ToString();

            string sql = "" +
            "select distinct d.Name, d.IssueDate, d.Description, b.Debit, e.Code " +              
            "from DiaryJournal_Fact a, " +
                    "DiaryJournal_Detail b, " +
                    "FinancialAccountDim c, " +
                    "FinancialTransactionDim d, " +
                    "CorrespondFinancialAccountDim e " +
            "where a.DiaryJournal_FactId = b.DiaryJournal_FactId " +
            "and a.FinancialAccountDimId = c.FinancialAccountDimId " +
            "and b.FinancialTransactionDimId = d.FinancialTransactionDimId " +
            "and b.CorrespondFinancialAccountDimId = e.CorrespondFinancialAccountDimId " +
            "and c.Code like '112%' " +
            "and (month(d.IssueDate) = " + month.ToString() + " and year(d.IssueDate) = " + year.ToString() + ") " +
            "and b.Debit > 0 " +
            "and (len(e.Code) > 0 and e.Code != 'NAAN_DEFAULT')" +
            "and (b.FinancialAccountDimId is null) " +
            "and e.Code not in ('1','2','3','4','5','6','7','8') " +
            "order by d.IssueDate ";


            DataTable dataTable = null;
            SelectedData seletectedData = null;

            GridViewDataColumn dataColumn = null;
            GridViewDataTextColumn textColumn = null;
            GridViewBandColumn bandColumn = null;

            //string spaceString = "";

            int widthEndCol = 0;
            int maxMonth = 0; //Int16.Parse(DateTime.Parse(txtToDate.Value.ToString()).ToString("MM"));

            double total = 0;
            double quantity = 0;

            double beginDebit = 0;
            double beginCredit = 0;

            string codeBegin = "";
            int index = 0;
            string dateBegin;

            DataRow line = null;

            string account = "112";

          
            dataTable = new DataTable();

            dataTable.Columns.Add("stt");
            dataTable.Columns.Add("sohieu", typeof(string));
            dataTable.Columns.Add("ngay");
            dataTable.Columns.Add("diengiai", typeof(string));

            dataColumn = new GridViewDataColumn();
            dataColumn.Caption = "Số TT";
            dataColumn.FieldName = "stt";
            ASPxGridViewS04a2.Columns.Add(dataColumn);

            bandColumn = new GridViewBandColumn("Chứng từ");
            ASPxGridViewS04a2.Columns.Add(bandColumn);

            dataColumn = new GridViewDataColumn();
            dataColumn.Caption = "Số hiệu";
            dataColumn.FieldName = "sohieu";
            bandColumn.Columns.Add(dataColumn);

            dataColumn = new GridViewDataColumn();
            dataColumn.Caption = "Ngày";
            dataColumn.FieldName = "ngay";
            bandColumn.Columns.Add(dataColumn);

            dataColumn = new GridViewDataColumn();
            dataColumn.Caption = "Diễn giải";
            dataColumn.FieldName = "diengiai";
            dataColumn.Width = 200;
            ASPxGridViewS04a2.Columns.Add(dataColumn);

            bandColumn = new GridViewBandColumn("Ghi Có tài khoản " + account + ", ghi Nợ các tài khoản");
            ASPxGridViewS04a2.Columns.Add(bandColumn);

            codeBegin = new String(' ', 10);

            try
            {
                seletectedData = session.ExecuteQuery(sql);
            }
            catch
            {
            }

            List<int> listHeader = new List<int>();

            foreach (var row in seletectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    if (!col.Values[4].ToString().Contains("112") && !listHeader.Contains(Int32.Parse(col.Values[4].ToString() == "" ? "0" : col.Values[4].ToString())))
                    {
                        listHeader.Add(Int32.Parse(col.Values[4].ToString()));
                    }  
                }                
            }

            listHeader.Sort();
            for (index = 0; index < listHeader.Count; index++)
            {
                textColumn = new GridViewDataTextColumn();
                textColumn.Caption = listHeader[index].ToString();
                textColumn.FieldName = listHeader[index].ToString();
                textColumn.PropertiesEdit.DisplayFormatString = "#,#";
                bandColumn.Columns.Add(textColumn);

                dataTable.Columns.Add(listHeader[index].ToString(), typeof(double));
            }

            textColumn = new GridViewDataTextColumn();
            textColumn.Caption = "Cộng Nợ TK " + account;
            textColumn.FieldName = "total";
            textColumn.PropertiesEdit.DisplayFormatString = "#,#";
            bandColumn.Columns.Add(textColumn);

            dataTable.Columns.Add("total", typeof(double));

            // data
            index = 0;
            total = 0;

            dateBegin = new String(' ', 10);
            codeBegin = new String(' ', 10);

            line = null;

            foreach (var row in seletectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    if (!codeBegin.Equals(col.Values[0].ToString()))
                    {
                        line = dataTable.NewRow();

                        codeBegin = col.Values[0].ToString();
                        //dateBegin = col.Values[1].ToString();
                        index++;

                        line[0] = index;
                        dataTable.Rows.Add(line);

                        total = 0;
                    }

                    line["sohieu"] = col.Values[0].ToString();
                    line["ngay"] = DateTime.Parse(col.Values[1].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);                    

                    if (col.Values[2] != null)
                    {
                        if (col.Values[2].ToString().Length > 10)
                        {
                            line["diengiai"] = col.Values[2].ToString().Substring(0, 10) + "...";
                        }
                        else
                        {
                            line["diengiai"] = col.Values[2].ToString();
                        }
                    }

                    line[col.Values[4].ToString()] = double.Parse(col.Values[3].ToString());

                    total += double.Parse(col.Values[3].ToString());
                    line["total"] = total;
                }

            }

            // sum total

            widthEndCol = 0;

            line = dataTable.NewRow();
            line["ngay"] = "Cộng";
            dataTable.Rows.Add(line);

            codeBegin = "";

            for (int i = 4; i < dataTable.Columns.Count - 1; i++)
            {
                line[dataTable.Columns[i].ColumnName] = dataTable.Compute("Sum([" + dataTable.Columns[i].ColumnName + "])", string.Empty);
                widthEndCol += line[dataTable.Columns[i].ColumnName].ToString().Length;
            }

            line["total"] = dataTable.Compute("Sum([total])", string.Empty);

            string space = "";
            if (220 - widthEndCol * 5 > 0)
            {
                space = new String(' ', 220 - widthEndCol * 5);
            }
            ASPxGridViewS04a2.Columns["total"].Caption = space + "Cộng có TK " + account;

            ASPxGridViewS04a2.DataSource = dataTable;
            ASPxGridViewS04a2.KeyFieldName = "stt";
            ASPxGridViewS04a2.DataBind();

            Report.S04a2_DN reportS04A2_DN = new Report.S04a2_DN();
            reportS04A2_DN.printableS04a2.PrintableComponent = new PrintableComponentLinkBase() { Component = ASPxGridViewExporterS04a2 };
            reportS04A2_DN.Parameters["datePeriod"].Value = new DateTime(year, month, 1);            

            ReportViewerS04a2.Report = reportS04A2_DN;
        }

     
        #endregion
        
    }
}