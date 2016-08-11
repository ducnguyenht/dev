using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Accounting.UserControl
{
    public partial class uVATReceipt : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {  new{stt = "01",Type = "Ví dụ 1", QS  = "01",MS = "01GTKT3/001", KH = "HT/11P", From = "000001", To = "000050", Date = "26/06/2013", DV = "Chi nhánh 1", Status = "Đang sử dụng"},
                                                new{stt = "02",Type = "Ví dụ 2", QS  = "02",MS = "01GTKT3/001", KH = "HT/11P", From = "000051", To = "000100", Date = "26/06/2013", DV = "Chi nhánh 2", Status = "Đang sử dụng"},
                                                new{stt = "03",Type = "Ví dụ 3", QS  = "03",MS = "01GTKT3/001", KH = "HT/11P", From = "000101", To = "000150", Date = "26/06/2013", DV = "", Status = "Chưa sử dụng"}};
            ASPxGridView1.KeyFieldName = "stt";
            ASPxGridView1.DataBind();
        }
        protected void DetailPerformData(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView detailView = (ASPxGridView)sender;
                detailView.Load += new EventHandler(detailView_Load);
                detailView.DataSource = new[] { new { code="000001", date="27/06/2013", status = "Đã sử dụng"},
                                                new { code="000002", date="27/06/2013", status = "Mất"},
                                                new { code="000003", date="28/06/2013", status = "Hủy"},
                                                new { code="000004", date="29/06/2013", status = "Xóa"},
                                                new { code="000005", date="", status = "Chưa sử dụng"},
                                                new { code="000006", date="", status = "Chưa sử dụng"},};
                detailView.KeyFieldName = "code";
            }
            catch (Exception) { }
        }
        void detailView_Load(object sender, EventArgs e)
        {
            ASPxGridView detailView = (ASPxGridView)sender;
            detailView.DataBind();
        }  
    }
}