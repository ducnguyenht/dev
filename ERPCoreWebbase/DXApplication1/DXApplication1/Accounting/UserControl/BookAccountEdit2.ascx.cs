using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace ERPCore.Accounting.UserControl
{
    public partial class BookAccountEdit2 : System.Web.UI.UserControl
    {
        private string textid;

        public string Textid
        {
            get { return textid; }
            set { textid = value; }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            String[] par = Regex.Split(e.Parameter, "%%");
            formBookAccountEdit2.HeaderText = "Sơ đồ định khoản: "+par[1];
            ASPxFormLayout4_E1.Text = "Tổng tiền phiếu "+par[0].ToLower();                                             
        }
    }
}