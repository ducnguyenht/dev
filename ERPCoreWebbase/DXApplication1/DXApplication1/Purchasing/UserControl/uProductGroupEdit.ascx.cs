using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxHtmlEditor;

//using DAL.Purchasing;
//using BLL.PurchasingBLO;
//using BLL.BO.Purchasing;
using Utility;
using DevExpress.Web.ASPxEditors;

namespace ERPCore.Purchasing.UserControl
{
    public partial class uProductGroupEdit : System.Web.UI.UserControl
    {
        //private bool validate()
        //{
            //bool isValid = true;

            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();

            //string oldCode = "";
            //string oldName = "";

            //if (hEditProductGroupId.Count > 0 && hEditProductGroupId.Get("id").ToString() != "")
            //{
            //    ViewBuyingProductCategory vpu = uow.GetObjectByKey<ViewBuyingProductCategory>(long.Parse(hEditProductGroupId.Get("id").ToString()));

            //    if (vpu != null)
            //    {
            //        oldCode = vpu.Code;
            //        oldName = vpu.Name;
            //    }
            //}

            //BuyingProductCategory pu = uow.FindObject<BuyingProductCategory>(new BinaryOperator("Code", txtProductGroupEditCode.Text));

            //if (pu != null && pu.RowStatus != Constant.ROWSTATUS_DELETED && oldCode != txtProductGroupEditCode.Text)
            //{
            //    txtProductGroupEditCode.ValidationSettings.ErrorText = "Mã nhóm hàng này đã tồn tại !";
            //    isValid = false;
            //}

            //return isValid;
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void cpProductGroupEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
                //String[] p = e.Parameter.Split('|');
                //UnitOfWork uow;
                //ViewBuyingProductCategory vbpc;
                //BuyingProductCategoryEntity bpce;

                //ASPxHtmlEditor htmlDescription;

                //switch (p[0])
                //{
                //    case "new":
                //        cboProductGroupEditRowStatus.Value = Constant.ROWSTATUS_ACTIVE.ToString();
                //        formProductGroupEdit.HeaderText = "Thông tin nhóm hàng hóa - Thêm mới";

                //        htmlDescription = (ASPxHtmlEditor)nbProductGroupEdit.Items[0].FindControl("htmlDescription");
                //        htmlDescription.Html = "";

                //        break;
                //    case "edit":
                    

                //        uow = XpoHelpers.GetNewUnitOfWork();

                //        vbpc = uow.GetObjectByKey<ViewBuyingProductCategory>(long.Parse(hEditProductGroupId.Get("id").ToString()));

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

                //            bpce = new BuyingProductCategoryEntity();
                //            bpce.Code = txtProductGroupEditCode.Text;
                //            bpce.Name = txtProductGroupEditName.Text;
                //            bpce.Description = htmlDescription.Html;

                //            if (hEditProductGroupId.Count > 0 && hEditProductGroupId.Get("id").ToString() != "")
                //            {
                //                vbpc = uow.GetObjectByKey<ViewBuyingProductCategory>(long.Parse(hEditProductGroupId.Get("id").ToString()));
                //                bpce.BuyingProductCategoryId = vbpc.BuyingProductCategoryId;
                //                bpce.BuyingProductCategoryPropertyId = vbpc.BuyingProductCategoryPropertyId;
                //                bpce.RowStatus = char.Parse(cboProductGroupEditRowStatus.Value.ToString());

                //                BuyingProductCategoryBLO.updateBuyingProductCategory(bpce);
                //            }
                //            else
                //            {
                //                bpce.RowCreationTimeStamp = DateTime.Now;
                //                bpce.Language = Constant.LANG_DEFAULT;
                //                bpce.RowStatus = Constant.ROWSTATUS_ACTIVE;

                //                BuyingProductCategoryBLO.insertBuyingProductCategory(bpce);
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
            //ASPxTextBox txt = sender as ASPxTextBox;

            //string oldCode = "";
            //string oldName = "";

            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();

            //if (hEditProductGroupId.Count > 0 && hEditProductGroupId.Get("id").ToString() != "")
            //{
            //    ViewBuyingProductCategory vpu = uow.GetObjectByKey<ViewBuyingProductCategory>(long.Parse(hEditProductGroupId.Get("id").ToString()));

            //    if (vpu != null)
            //    {
            //        oldCode = vpu.Code;
            //        oldName = vpu.Name;
            //    }
            //}

            //BuyingProductCategory pu = uow.FindObject<BuyingProductCategory>(new BinaryOperator("Code", txtProductGroupEditCode.Text));

            //if (pu != null && pu.RowStatus != Constant.ROWSTATUS_DELETED && oldCode != txtProductGroupEditCode.Text)
            //{
            //    e.IsValid = false;
            //    e.ErrorText = "Mã nhóm hàng này đã tồn tại !";
            //}
        }

        protected void txtProductGroupEditName_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
           
        }
       

   
    }
}