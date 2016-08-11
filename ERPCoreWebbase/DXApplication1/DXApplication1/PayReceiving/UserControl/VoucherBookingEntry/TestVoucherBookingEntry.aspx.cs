using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

namespace WebModule.PayReceiving.UserControl.VoucherBookingEntry
{
    public partial class TestVoucherBookingEntry : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //Nếu request trang lần đầu tiên
            if (!IsPostBack)
            {
                //Sinh ra Id của page và lưu vào biến ViewState
                GenerateViewStateControlId();
                ItemId = Guid.NewGuid();
            }
        }

        private Guid ItemId
        {
            //Bằng việc nối thêm chuỗi ViewStateControlId thì session key sẽ là duy nhất
            get { return (Guid)Session["ItemId_" + ViewStateControlId]; }
            set { Session["ItemId_" + ViewStateControlId] = value; }
        }

        private string ViewStateControlId
        {
            get { return (string)ViewState["ViewStateControlId"]; }
            set { ViewState["ViewStateControlId"] = value; }
        }

        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}