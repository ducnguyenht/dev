using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Accounting
{
    public partial class VATReceiptForPrinting : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_VATRECEIPTFORPRINTING_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            grid_dmorder.DataSource = new[] { new {stt = "01", id = "PDH00001", idkh = "KH00001", tenkh = "Nguyen Van A", ngaymua = "01/07/2013", vatreceiptid="000000001", PrintStatus="Đã in"},
                                            new {stt = "02", id = "PDH00002", idkh = "KH00002", tenkh = "Nguyen Van B", ngaymua = "05/07/2013", vatreceiptid="000000002", PrintStatus="Đã in"},
                                            new {stt = "03", id = "PDH00003", idkh = "KH00003", tenkh = "Nguyen Van C", ngaymua = "07/07/2013", vatreceiptid="000000003", PrintStatus="Đang xử lý"}};
            grid_dmorder.KeyFieldName = "id";
            grid_dmorder.DataBind();
        }
    }
}