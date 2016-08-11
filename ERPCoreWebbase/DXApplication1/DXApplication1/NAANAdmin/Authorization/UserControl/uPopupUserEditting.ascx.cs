using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL;
using DevExpress.Web.ASPxGridView;
using System.Text.RegularExpressions;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Xpo;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Data.Filtering;

namespace WebModule.NAANAdmin.Authorization.UserControl
{
    public partial class uPopupUserEditting : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //session = XpoHelper.GetNewSession();
            //PersonEdittingXDS.Session = session;
            //LoginEmailAccountXDS.Session = session;
            //DepartmentXDS.Session = session;
            //PersonEdittingXDS.CriteriaParameters["PersonId"].DefaultValue = Guid.Empty.ToString();
            //LoginEmailAccountXDS.CriteriaParameters["PersonId"].DefaultValue = Guid.Empty.ToString();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            PersonEdittingXDS.Session = session;
            LoginEmailAccountXDS.Session = session;
            DepartmentXDS.Session = session;
            PersonEdittingXDS.CriteriaParameters["PersonId"].DefaultValue = Guid.Empty.ToString();
            LoginEmailAccountXDS.CriteriaParameters["PersonId"].DefaultValue = Guid.Empty.ToString();
        } 

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            //session.Dispose();
        }

        protected void cpPersonEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            ACTION = para[0];
            if (ACTION.Equals("Edit") || ACTION.Equals("Delete"))
            {
                string id = para.Count<string>() == 2 ? para[1] : String.Empty;
                if (id != "")
                {
                    PersonId = Guid.Parse(id);
                }
            }

            if (ACTION.Equals("Add"))
            {
                defaultDepartment = string.Empty;
                defaultDepartment = para.Count<string>() == 2 ? para[1] : string.Empty;
            }
            action();
        }

        protected void grdEmailList_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            LoginAccount acc = PersonEdittingXDS.Session.GetObjectByKey<LoginAccount>(e.Keys[grdEmailList.KeyFieldName]);
            if (acc != null)
            {
                acc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                acc.Save();
            }
            e.Cancel = true;
            grdEmailList.CancelEdit();
        }

        protected void grdEmailList_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["PersonId!Key"] = PersonId;
            e.NewValues["RowStatus"] = Utility.Constant.ROWSTATUS_ACTIVE;
        }

        protected void grdEmailList_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            GridViewColumn colEmail = grid.Columns["Email"];
            string email = string.Empty;
            string oldemail = string.Empty;

            if (e.OldValues["Email"] != null)
                oldemail = e.OldValues["Email"].ToString().Trim();

            if (e.NewValues["Email"] != null)
                email = e.NewValues["Email"].ToString().Trim();

            if (email.Equals(string.Empty))
            {
                e.Errors[colEmail] = "Email không được rỗng";
                return;
            }

            Regex rgx = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (!rgx.IsMatch(email))
            {
                e.Errors[colEmail] = "Email không hợp lệ!";
                //e.Errors[colEmail] = String.Format(" '{0}' không hợp lệ", email);
                return;
            }

            if (!e.IsNewRow && email.Equals(oldemail))
                return;

            if (Util.isExistXpoObject<LoginAccount>("Email", email,
                Utility.Constant.ROWSTATUS_INACTIVE,
                Utility.Constant.ROWSTATUS_ACTIVE,
                Utility.Constant.ROWSTATUS_DEFAULT))
            {
                e.Errors[colEmail] = "Email đã tồn tại trong hệ thống!";
                //e.Errors[colEmail] = email + "Email đã tồn tại trong hệ thống";
            }   

        }

        protected void trlDepartment_SelectionChanged(object sender, EventArgs e)
        {
            //updateDepartmentOnPersonTree();
        }

        protected void cpPersonCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string code = txtPersonCode.Text.Trim();
            if (!code.Equals(string.Empty) && isDupplicateCode(code))
            {
                CriteriaOperator criteria = new BinaryOperator("Code", code);
                Person person = session.FindObject<Person>(criteria);
                short rowstatus = person.RowStatus;
                if (rowstatus > -1)
                {
                    txtPersonCode.ErrorText = "Mã người dùng đã được sử dụng";
                    //txtPersonCode.ErrorText = String.Format("'{0}' đã được sử dụng", code);
                    txtPersonCode.IsValid = false;
                    txtPersonCode.Focus();
                }
            }            
        }
    }
}