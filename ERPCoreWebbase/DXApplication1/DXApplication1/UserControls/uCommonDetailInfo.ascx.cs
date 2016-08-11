using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using NAS.DAL;

namespace WebModule.UserControls
{
    public partial class uCommonDetailInfo : System.Web.UI.UserControl
    {
        public string DetailContent{
            get {
                if (Session["DetailContent"] != null)
                    return Session["DetailContent"].ToString();
                return string.Empty;
            }
            set { 
                Session["DetailContent"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DetailContent = string.Empty;
        }

        protected void popupCommonDetailInfo_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            DetailContent = e.Parameter.ToString();
            if (DetailContent.Equals(string.Empty))
                DetailContent = string.Format("<div {0}>{1}</div>", "style=\"font-size:13px\"", "Chưa nhập thông tin chi tiết");
            popupCommonDetailInfo.ShowOnPageLoad = true;
        }

    }
}