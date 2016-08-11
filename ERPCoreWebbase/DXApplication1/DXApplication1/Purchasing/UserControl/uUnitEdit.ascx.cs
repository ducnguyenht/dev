using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxHtmlEditor;
using Utility;
using DevExpress.Web.ASPxEditors;
using NAS.DAL;

namespace WebModule.Purchasing.UserControl
{
    public partial class uUnitEdit : System.Web.UI.UserControl
    {
        private bool validate()
        {
            bool isValid = true;
            return isValid;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            UnitEdittingXDS.Session = session;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void cpUnitEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            ACTION = para[0];
            string id = para.Count<string>() == 2 ? id = para[1] : id = "";
            if (id != "")
                UnitId = Guid.Parse(id);
            Action();
        }

        protected void txtUnitEditCode_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
            string msg = "";
            bool rs = validateDupplicateCode(out msg);
            if (!rs)
            {
                txtUnitEditCode.IsValid = false;
                txtUnitEditCode.ErrorText = msg;
                e.IsValid = false;
                e.ErrorText = msg;
            }
        }

        protected void txtUnitEditName_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
           
        }

        protected void cpUnitEditCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            ACTION = para[0];
            Action();
            //pcUnit.ActiveTabIndex = 0;
        }
       
    }
}