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

//using DAL.Purchasing;

//using BLL.SalesBLO;
//using BLL.PurchasingBLO;

//using BLL.BO.Purchasing;
//using BLL.BO.Sale;



namespace ERPCore.Sale.UserControl
{
    public partial class uProductGroupEdit : System.Web.UI.UserControl
    {
        private bool validate()
        {
            bool isValid = true;

            if (txtProductGroupEditCode.Text.Trim().Equals(""))
            {
                isValid = false;
            }

            if (txtProductGroupEditName.Text.Trim().Equals(""))
            {
                isValid = false;
            }

            if (!isValid)
            {
                return false;
            }

            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();

            //string oldCode = "";
            //string oldName = "";

            //if (hEditProductGroupId.Count > 0)
            //{
            //    ViewSalingProductCategory vpu = uow.GetObjectByKey<ViewSalingProductCategory>(long.Parse(hEditProductGroupId.Get("id").ToString()));

            //    if (vpu != null)
            //    {
            //        oldCode = vpu.Code;
            //        oldName = vpu.Name;
            //    }
            //}

            //SalingProductCategory pu = uow.FindObject<SalingProductCategory>(new BinaryOperator("Code", txtProductGroupEditCode.Text));

            //if (pu != null && oldCode != txtProductGroupEditCode.Text)
            //{
            //    txtProductGroupEditCode.ValidationSettings.ErrorText = "Mã nhóm hàng này đã tồn tại !";
            //    isValid = false;
            //}

            //SalingProductCategoryProperty pup = uow.FindObject<SalingProductCategoryProperty>(new BinaryOperator("Name", txtProductGroupEditName.Text));

            //if (pup != null && oldName != txtProductGroupEditName.Text)
            //{
            //    txtProductGroupEditName.ValidationSettings.ErrorText = "Tên nhóm hàng này đã tồn tại !";
            //    isValid = false;
            //}

            return isValid;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void cpProductGroupEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
                String[] p = e.Parameter.Split('|');
                UnitOfWork uow;
                //ViewSalingProductCategory vbpc;
                

                //ASPxHtmlEditor htmlDescription;

                //switch (p[0])
                //{
                //    case "new":
                //        cboProductGroupEditRowStatus.Value = "A";
                //        formProductGroupEdit.HeaderText = "Thông tin nhóm hàng hóa - Thêm mới";

                //        htmlDescription = (ASPxHtmlEditor)nbProductGroupEdit.Items[0].FindControl("htmlDescription");
                //        htmlDescription.Html = "";

                //        break;
                //    case "edit":
                    

                //        uow = XpoHelpers.GetNewUnitOfWork();

                //        vbpc = uow.GetObjectByKey<ViewSalingProductCategory>(long.Parse(hEditProductGroupId.Get("id").ToString()));

                //        txtProductGroupEditCode.Text = vbpc.Code;
                //        txtProductGroupEditName.Text = vbpc.Name;
                //        cboProductGroupEditRowStatus.Value = Convert.ToString(vbpc.RowStatus);

                //        htmlDescription = (ASPxHtmlEditor)nbProductGroupEdit.Items[0].FindControl("htmlDescription");
                //        htmlDescription.Html = vbpc.Description;

                //        formProductGroupEdit.HeaderText = "Thông tin nhóm hàng hóa - " + vbpc.Code;

                //        break;
                //    case "save":
                //        if (!validate())
                //        {
                //            return;
                //        }

                //        using (uow = XpoHelpers.GetNewUnitOfWork())
                //        {
                //            htmlDescription = (ASPxHtmlEditor)nbProductGroupEdit.Items[0].FindControl("htmlDescription");

                //            SalingProductCategoryEntity bpce = new SalingProductCategoryEntity();
                //            bpce.Code = txtProductGroupEditCode.Text;
                //            bpce.Name = txtProductGroupEditName.Text;
                //            bpce.Description = htmlDescription.Html;

                //            if (hEditProductGroupId.Count > 0)
                //            {
                //                vbpc = uow.GetObjectByKey<ViewSalingProductCategory>(long.Parse(hEditProductGroupId.Get("id").ToString()));
                //                bpce.SalingProductCategoryId = vbpc.SalingProductCategoryId;
                //                bpce.SalingProductCategoryPropertyId = vbpc.SalingProductCategoryPropertyId;
                //                bpce.RowStatus = char.Parse(cboProductGroupEditRowStatus.Value.ToString());

                //                SalingProductCategoryBLO.updateSalingProductCategory(bpce);
                //            }
                //            else
                //            {
                //                bpce.RowCreationTimeStamp = DateTime.Now;
                //                bpce.Language = Constant.LANG_DEFAULT;
                //                bpce.RowStatus = Constant.ROWSTATUS_ACTIVE;

                //                SalingProductCategoryBLO.insertSalingProductCategory(bpce);
                //            }
                //        }

                //        hEditProductGroupId.Clear();
                //        formProductGroupEdit.ShowOnPageLoad = false;

                //        cpProductGroupEdit.JSProperties.Add("cpRefresh", "resfresh");

                //        break;
                //    default:
                //        break;
                //}

        }

        protected void txtProductGroupEditCode_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
            e.IsValid = validate();
            if (txtProductGroupEditCode.ValidationSettings.ErrorText == "")
            {
                e.IsValid = true;
            }
        }

        protected void txtProductGroupEditName_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
            e.IsValid = validate();
            if (txtProductGroupEditName.ValidationSettings.ErrorText == "")
            {
                e.IsValid = true;
            }
        }
       

   
    }
}