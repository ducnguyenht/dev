using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxGridView;

//using DAL.Purchasing;
//using BLL.SalesBLO;
//using BLL.BO.Sale;
using Utility;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxTabControl;


namespace WebModule.GUI.Sales.userControl
{
    public partial class uEditPartnerGrp : System.Web.UI.UserControl
    {
        Session session;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool validate()
        {
            bool isValid = true;

            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();

            //string oldCode = "";
            //string oldName = "";
          
            //if (hPartnerCategoryId.Count > 0 && hPartnerCategoryId.Get("id").ToString() != "")
            //{
            //    ViewPartnerCategory vpu = uow.GetObjectByKey<ViewPartnerCategory>(long.Parse(hPartnerCategoryId.Get("id").ToString()));

            //    if (vpu != null)
            //    {
            //        oldCode = vpu.Code;
            //        oldName = vpu.Name;
            //    }
            //}

            //PartnerCategory p = uow.FindObject<PartnerCategory>(new BinaryOperator("Code", txtPartnerCategoryCode.Text));

            //if (p != null && oldCode != txtPartnerCategoryCode.Text)
            //{                
            //    isValid = false;
            //}

            return isValid;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelpers.GetNewSession();
        }

        protected void txtPartnerCategoryCode_Validation(object sender, ValidationEventArgs e)
        {
            //ASPxTextBox txt = sender as ASPxTextBox;

            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();

            //string oldCode = "";
            //string oldName = "";
          

            //if (hPartnerCategoryId.Count > 0 && hPartnerCategoryId.Get("id").ToString() != "")
            //{
            //    ViewPartnerCategory vpu = uow.GetObjectByKey<ViewPartnerCategory>(long.Parse(hPartnerCategoryId.Get("id").ToString()));

            //    if (vpu != null)
            //    {
            //        oldCode = vpu.Code;
            //        oldName = vpu.Name;
            //    }
            //}


            //PartnerCategory p = uow.FindObject<PartnerCategory>(new BinaryOperator("Code", txt.Text));

            //if (p != null && oldCode != txt.Text)
            //{
            //    e.IsValid = false;
            //    e.ErrorText = "Mã nhóm cộng tác viên này đã tồn tại !";                
            //}
        }

        protected void txtPartnerCategoryName_Validation(object sender, ValidationEventArgs e)
        {
            e.IsValid = validate();
            if (txtPartnerCategoryName.ValidationSettings.ErrorText == "")
            {
                e.IsValid = true;
            }
        }

        protected void cpPartnerCategoryEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //ASPxHtmlEditor htmlDescription;
            //UnitOfWork uow;

            //if (e.Parameter.Equals("undefined"))
            //{
            //    return;
            //}

            //String[] p = e.Parameter.Split('|');

            //switch (p[0])
            //{

            //    case "edit":

            //        uow = XpoHelpers.GetNewUnitOfWork();

            //        ViewPartnerCategory vp = uow.GetObjectByKey<ViewPartnerCategory>(long.Parse(hPartnerCategoryId.Get("id").ToString()));

            //        txtPartnerCategoryCode.Text = vp.Code;
            //        txtPartnerCategoryName.Text = vp.Name;
            //        cboPartnerCategoryRowDevice.Value = Convert.ToString(vp.RowStatus);                    

            //        htmlDescription = (ASPxHtmlEditor)nbPartnerCategory.Items[0].FindControl("htmlDescription");
            //        htmlDescription.Html = vp.Description;

            //        formPartnerCategoryEdit.HeaderText = "Thông tin nhóm cộng tác viên - " + vp.Code;

            //        break;

            //    case "new":

            //        cboPartnerCategoryRowDevice.Value = Convert.ToString(Constant.ROWSTATUS_ACTIVE);   
            //        formPartnerCategoryEdit.HeaderText = "Thông tin nhóm cộng tác viên - Thêm mới";
                   
            //        htmlDescription = (ASPxHtmlEditor)nbPartnerCategory.Items[0].FindControl("htmlDescription");
            //        htmlDescription.Html = "";

            //        break;

            //    case "save":

            //        if (!validate())
            //        {
            //            return;
            //        }
                   

            //        htmlDescription = (ASPxHtmlEditor)nbPartnerCategory.Items[0].FindControl("htmlDescription");

            //        PartnerCategoryEntity pe = new PartnerCategoryEntity();
            //        pe.Code = txtPartnerCategoryCode.Text;
            //        pe.Name = txtPartnerCategoryName.Text;
            //        pe.Description = htmlDescription.Html;

            //        if (hPartnerCategoryId.Count > 0 && hPartnerCategoryId.Get("id").ToString() != "")
            //        {
            //            vp = session.GetObjectByKey<ViewPartnerCategory>(long.Parse(hPartnerCategoryId.Get("id").ToString()));
            //            pe.PartnerCategoryId = vp.PartnerCategoryId;
            //            pe.PartnerCategoryPropertyId = vp.PartnerCategoryPropertyId;
            //            pe.RowCreationTimeStamp = DateTime.Now;
            //            pe.RowStatus = char.Parse(cboPartnerCategoryRowDevice.Value.ToString());
            //            pe.Description = htmlDescription.Html;                        

            //            PartnerCategoryBLO.updatePartnerCategory(pe);

            //        }
            //        else
            //        {
            //            pe.RowCreationTimeStamp = DateTime.Now;
            //            pe.Language = Constant.LANG_DEFAULT;
            //            pe.RowStatus = Constant.ROWSTATUS_ACTIVE;
            //            pe.Description = htmlDescription.Html;

            //            PartnerCategoryBLO.insertPartnerCategory(pe);
            //        }

            //        hPartnerCategoryId.Clear();                    

            //        cpPartnerCategoryEdit.JSProperties.Add("cpRefresh", "resfresh");

            //        break;

            //    case "view":

            //        break;
            //    default:
            //        break;
            //}
        }

        protected void formPartnerCategoryEdit_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();

            //String[] p = e.Parameter.Split('|');

            //switch (p[0])
            //{
            //    case "txtPartnerCategoryCode_Validation":

            //        string oldCode = "";
            //        string oldName = "";

            //        if (hPartnerCategoryId.Count > 0 && hPartnerCategoryId.Get("id").ToString() != "")
            //        {
            //            ViewPartnerCategory vpu = uow.GetObjectByKey<ViewPartnerCategory>(long.Parse(hPartnerCategoryId.Get("id").ToString()));

            //            if (vpu != null)
            //            {
            //                oldCode = vpu.Code;
            //                oldName = vpu.Name;
            //            }
            //        }

            //        PartnerCategory pp = uow.FindObject<PartnerCategory>(new BinaryOperator("Code", txtPartnerCategoryCode.Text));

            //        if (pp != null && oldCode != txtPartnerCategoryCode.Text)
            //        {
            //            txtPartnerCategoryCode.IsValid = false;
            //            txtPartnerCategoryCode.ValidationSettings.ErrorText = "Mã nhóm cộng tác viên này đã tồn tại !";

            //            pcPartnerCategory.JSProperties.Add("cpCheck", "txtVald");
            //        }

            //        break;
            //    default:
            //        break;
            //}
        }
    
     
        protected void cpPartnerCategoryCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();

            //String[] p = e.Parameter.Split('|');

            //switch (p[0])
            //{
            //    case "txtPartnerCategoryCode_Validation":

            //        string oldCode = "";
            //        string oldName = "";

            //        if (hPartnerCategoryId.Count > 0 && hPartnerCategoryId.Get("id").ToString() != "")
            //        {
            //            ViewPartnerCategory vpu = uow.GetObjectByKey<ViewPartnerCategory>(long.Parse(hPartnerCategoryId.Get("id").ToString()));

            //            if (vpu != null)
            //            {
            //                oldCode = vpu.Code;
            //                oldName = vpu.Name;
            //            }
            //        }

            //        PartnerCategory pp = uow.FindObject<PartnerCategory>(new BinaryOperator("Code", txtPartnerCategoryCode.Text));

            //        if (pp != null && oldCode != txtPartnerCategoryCode.Text)
            //        {
            //            txtPartnerCategoryCode.IsValid = false;
            //            txtPartnerCategoryCode.ValidationSettings.ErrorText = "Mã nhóm cộng tác viên này đã tồn tại !";

            //            pcPartnerCategory.JSProperties.Add("cpCheck", "txtVald");
            //        }

            //        break;
            //    default:
            //        break;
            //}
        }

       

       
       

       
      
    }
}