using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System.Data;
using NAS.DAL.Report;
using NAS.DAL;
using DevExpress.XtraPrintingLinks;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Accounting.Report.UserControls
{
    public partial class B09_DN : System.Web.UI.UserControl
    {
        string m_Sql = "";
        Session m_Session = null;
        SelectedData m_SelectedData;
        XPDataView xpDataView;
        DataTable m_DataTable;
        bool m_Transfered = false;
        string m_SqlDebitLike = "";
        string m_SqlCreditLike = "";
        string[] m_SplitAccount;
        DataRow m_Line;
        GridViewDataColumn m_DataColumn;

        XPCollection<FinancialDescriptionReportT1_Fact> m_ListFinancialDescriptionReportT1_Fact = null;
        FinancialDescriptionReportT1_Fact m_FinancialDescriptionReportT1_Fact = null;

        private void InitB09DNT1Data()
        {
            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "1";
            m_FinancialDescriptionReportT1_Fact.Name = "I- Đặc điểm hoạt động của doanh nghiệp";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "2";
            m_FinancialDescriptionReportT1_Fact.Name = "1- Hình thức sở hữu vốn";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "3";
            m_FinancialDescriptionReportT1_Fact.Name = "2- Lĩnh vực kinh doanh";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "4";
            m_FinancialDescriptionReportT1_Fact.Name = "3- Ngành nghề kinh doanh";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "5";
            m_FinancialDescriptionReportT1_Fact.Name = "4- Đặc điểm hoạt động của doanh nghiệp trong năm tài chính có ảnh hưởng đến BCTC";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "6";
            m_FinancialDescriptionReportT1_Fact.Name = "II- Kỳ kế toán, đơn vị tiền tệ sử dụng trong kế toán";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "7";
            m_FinancialDescriptionReportT1_Fact.Name = "1- Kỳ kế toán năm (bắt đầu từ ngày 01/01 kết thúc vào ngày 31/12).";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "8";
            m_FinancialDescriptionReportT1_Fact.Name = "2. Đơn vị tiền tệ sử dụng trong kế toán";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "9";
            m_FinancialDescriptionReportT1_Fact.Name = "III- Chuẩn mực và chế độ kế toán áp dụng";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "10";
            m_FinancialDescriptionReportT1_Fact.Name = "1- Chế độ kế toán áp dụng";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "11";
            m_FinancialDescriptionReportT1_Fact.Name = "2- Tuyên bố về việc tuân thủ Chuẩn mực kế toán và Chế độ kế toán";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "12";
            m_FinancialDescriptionReportT1_Fact.Name = "3- Hình thức kế toán áp dụng";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "13";
            m_FinancialDescriptionReportT1_Fact.Name = "IV- Các chính sách kế toán áp dụng";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "14";
            m_FinancialDescriptionReportT1_Fact.Name = "1- Nguyên tắc ghi nhận các khoản tiền và các khoản tương đương tiền";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "15";
            m_FinancialDescriptionReportT1_Fact.Name = "- Nguyên tắc ghi nhận các khoản đầu tư vào công ty con, công ty liên kết";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "16";
            m_FinancialDescriptionReportT1_Fact.Name = "- Phương pháp chuyển đổi các đồng tiền khác ra đồng tiền sử dụng trong kế toán";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "17";
            m_FinancialDescriptionReportT1_Fact.Name = "2- Nguyên tắc ghi nhận hàng tồn kho:";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "18";
            m_FinancialDescriptionReportT1_Fact.Name = "- Nguyên tắc ghi nhận hàng tồn kho;";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "19";
            m_FinancialDescriptionReportT1_Fact.Name = "- Phương pháp tính giá trị hàng tồn kho cuối kỳ;";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "20";
            m_FinancialDescriptionReportT1_Fact.Name = "- Phương pháp hạch toán hàng tồn kho;";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "21";
            m_FinancialDescriptionReportT1_Fact.Name = "- Phương pháp lập dự phòng giảm giá hàng tồn kho.";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "22";
            m_FinancialDescriptionReportT1_Fact.Name = "3- Nguyên tắc ghi nhận và khấu hao TSCĐ và bất động sản đầu tư";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "23";
            m_FinancialDescriptionReportT1_Fact.Name = "- Nguyên tắc ghi nhận TSCĐ (hữu hình, vô hình, thuê tài chính);";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "24";
            m_FinancialDescriptionReportT1_Fact.Name = "- Phương pháp khấu hao TSCĐ (hữu hình, vô hình, thuê tài chính).";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "25";
            m_FinancialDescriptionReportT1_Fact.Name = "4- Nguyên tắc ghi nhận và khấu hao bất động sản đầu tư.";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "26";
            m_FinancialDescriptionReportT1_Fact.Name = "- Nguyên tắc ghi nhận bất động sản đầu tư;";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "27";
            m_FinancialDescriptionReportT1_Fact.Name = "- Phương pháp khấu hao bất động sản đầu tư.";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "28";
            m_FinancialDescriptionReportT1_Fact.Name = "5- Nguyên tắc ghi nhận các khoản đầu tư tài chính:";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "29";
            m_FinancialDescriptionReportT1_Fact.Name = "- Các khoản đầu tư vào công ty con, công ty liên kết, vốn góp vào cơ sở kinh doanh đồng kiểm soát";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "30";
            m_FinancialDescriptionReportT1_Fact.Name = "- Các khoản đầu tư ngắn hạn, dài hạn khác;";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "31";
            m_FinancialDescriptionReportT1_Fact.Name = "- Phương pháp lập dự phòng giảm giá đầu tư ngắn hạn, dài hạn";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "32";
            m_FinancialDescriptionReportT1_Fact.Name = "6- Nguyên tắc ghi nhận và vốn hóa các khoản chi phí đi vay:";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "33";
            m_FinancialDescriptionReportT1_Fact.Name = "- Nguyên tắc ghi nhận chi phí đi vay;";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "34";
            m_FinancialDescriptionReportT1_Fact.Name = "- Tỷ lệ vốn hóa được sử dụng để xác định chi phí đi vay được vốn hóa trong kỳ;";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "35";
            m_FinancialDescriptionReportT1_Fact.Name = "7- Nguyên tắc ghi nhận và vốn hóa các khoản chi phí khác:";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "36";
            m_FinancialDescriptionReportT1_Fact.Name = "- Chi phí trả trước;";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "37";
            m_FinancialDescriptionReportT1_Fact.Name = "- Chi phí khác;";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "38";
            m_FinancialDescriptionReportT1_Fact.Name = "- Phương pháp phân bổ chi phí trả trước;";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "39";
            m_FinancialDescriptionReportT1_Fact.Name = "- Phương pháp và thời gian phân bổ lợi thế thương mại;";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "40";
            m_FinancialDescriptionReportT1_Fact.Name = "8- Nguyên tắc ghi nhận chi phí phải trả.";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "41";
            m_FinancialDescriptionReportT1_Fact.Name = "9- Nguyên tắc và phương pháp ghi nhận các khoản dự phòng phải trả.";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "42";
            m_FinancialDescriptionReportT1_Fact.Name = "10- Nguyên tắc ghi nhận vốn chủ sở hữu:";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "43";
            m_FinancialDescriptionReportT1_Fact.Name = "- Nguyên tắc ghi nhận vốn đầu tư của chủ sở hữu, thặng dư vốn cổ phần, vốn khác của chủ sở hữu";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "44";
            m_FinancialDescriptionReportT1_Fact.Name = "- Nguyên tắc ghi nhận chênh lệch đánh giá lại tài sản.";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "45";
            m_FinancialDescriptionReportT1_Fact.Name = "- Nguyên tắc ghi nhận chênh lệch tỷ giá.";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "46";
            m_FinancialDescriptionReportT1_Fact.Name = "- Nguyên tắc ghi nhận lợi nhuận chưa phân phối.";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "47";
            m_FinancialDescriptionReportT1_Fact.Name = "11- Nguyên tắc và phương pháp ghi nhận doanh thu:";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "48";
            m_FinancialDescriptionReportT1_Fact.Name = "- Doanh thu bán hàng";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "49";
            m_FinancialDescriptionReportT1_Fact.Name = "- Doanh thu cung cấp dịch vụ";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "50";
            m_FinancialDescriptionReportT1_Fact.Name = "- Doanh thu hoạt động tài chính";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "51";
            m_FinancialDescriptionReportT1_Fact.Name = "- Doanh thu hợp đồng xây dựng";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "52";
            m_FinancialDescriptionReportT1_Fact.Name = "12- Nguyên tắc và phương pháp ghi nhận chi phí tài chính.";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "53";
            m_FinancialDescriptionReportT1_Fact.Name = "13- Nguyên tắc và phương pháp ghi nhận chi phí thuế thu nhập doanh nghiệp hiện hành, chi phí thuế thu nhập doanh nghiệp hoãn lại";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "54";
            m_FinancialDescriptionReportT1_Fact.Name = "14- Các nghiệp vụ dự phòng rủi ro hối đoái";

            m_FinancialDescriptionReportT1_Fact.Save();

            //
            m_FinancialDescriptionReportT1_Fact = new FinancialDescriptionReportT1_Fact(m_Session);
            m_FinancialDescriptionReportT1_Fact.Code = "55";
            m_FinancialDescriptionReportT1_Fact.Name = "15- Các nguyên tắc và phương pháp kế toán khác";

            m_FinancialDescriptionReportT1_Fact.Save();
        }

        private void load_data()
        {
            m_ListFinancialDescriptionReportT1_Fact = new XPCollection<FinancialDescriptionReportT1_Fact>(m_Session);
            if (m_ListFinancialDescriptionReportT1_Fact.Count == 0)
            {
                InitB09DNT1Data();
            }

            

            m_DataTable = new DataTable();
            m_DataTable.Columns.Add("name");

            GridViewDataColumn m_DataColumn = new GridViewDataColumn();
            m_DataColumn.FieldName = "name";
            m_DataColumn.Width = 500;
            B09DNT1Data.Columns.Add(m_DataColumn);

            m_Sql = "select name from FinancialDescriptionReportT1_Fact";
            m_SelectedData = m_Session.ExecuteQuery(m_Sql);

            foreach (var row in m_SelectedData.ResultSet)
            {
                foreach (var col in row.Rows)
                {
                    m_Line = m_DataTable.NewRow();
                    m_Line["name"] = col.Values[0].ToString();

                    m_DataTable.Rows.Add(m_Line);
                }


            }

            B09DNT1Data.DataSource = m_DataTable;
            B09DNT1Data.KeyFieldName = "name";
            B09DNT1Data.DataBind();

            Report.B09_DN reportB09_DN = new Report.B09_DN();       
            reportB09_DN.PCCB09DNT1Data.PrintableComponent = new PrintableComponentLinkBase { Component = B09DNT1DataGridViewExporter };
            
            B09dnReportViewer.Report = reportB09_DN;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Session = XpoHelper.GetNewSession();
            if (hB09dn.Contains("show"))
            {
                load_data();
            }
        }

    }
}