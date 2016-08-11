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

namespace ERPCore.Sale.UserControl
{
    public partial class uUnitEdit : System.Web.UI.UserControl
    {
        private bool validate()
        {
            bool isValid = true;

            if (txtUnitEditCode.Text.Trim().Equals(""))
            {
                isValid = false;
            }

            if (txtUnitEditName.Text.Trim().Equals(""))
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

            //if (hUnitEditId.Count > 0)
            //{
            //    ViewProductUnit vpu = uow.GetObjectByKey<ViewProductUnit>(long.Parse(hUnitEditId.Get("id").ToString()));

            //    if (vpu != null)
            //    {
            //        oldCode = vpu.Code;
            //        oldName = vpu.Name;
            //    }
            //}

            //ProductUnit pu = uow.FindObject<ProductUnit>(new BinaryOperator("Code", txtUnitEditCode.Text));

            //if (pu != null && oldCode != txtUnitEditCode.Text)
            //{
            //    txtUnitEditCode.ValidationSettings.ErrorText = "Mã đơn vị tính này đã tồn tại !";
            //    isValid = false;
            //}

            //ProductUnitProperty pup = uow.FindObject<ProductUnitProperty>(new BinaryOperator("Name", txtUnitEditName.Text));

            //if (pup != null && oldName != txtUnitEditName.Text)
            //{
            //    txtUnitEditName.ValidationSettings.ErrorText = "Tên đơn vị tính này đã tồn tại !";
            //    isValid = false;
            //}
         
            return isValid;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
       

        protected void cpUnitEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            String[] p = e.Parameter.Split('|');
            UnitOfWork uow;
            //ViewProductUnit pu;

            ASPxHtmlEditor htmlDescription;

            switch (p[0])
            {
                case "new":
                    cboUnitEditRowStatus.Value = "A";
                    formUnitEdit.HeaderText = "Thông tin đơn vị tính - Thêm mới";

                    htmlDescription = (ASPxHtmlEditor)nbUnitEdit.Items[0].FindControl("htmlDescription");
                    htmlDescription.Html = "";

                    break;
                case "edit":
                    //formUnitEdit.HeaderText = "Thông tin đơn vị tính - " + hUnitEditId.Get("id").ToString();

                    //uow = XpoHelpers.GetNewUnitOfWork();

                    //pu = uow.GetObjectByKey<ViewProductUnit>(long.Parse(hUnitEditId.Get("id").ToString()));

                    //txtUnitEditCode.Text = pu.Code;
                    //txtUnitEditName.Text = pu.Name;
                    //cboUnitEditRowStatus.Value = Convert.ToString(pu.RowStatus);

                    //htmlDescription = (ASPxHtmlEditor)nbUnitEdit.Items[0].FindControl("htmlDescription");
                    //htmlDescription.Html = pu.Description;

                    break;
                //case "save":
                //    if (!validate())
                //    {
                //        return;
                //    }

                    //using (uow = XpoHelpers.GetNewUnitOfWork())
                    //{
                    //    htmlDescription = (ASPxHtmlEditor)nbUnitEdit.Items[0].FindControl("htmlDescription");

                    //    ProductUnitEntity pue = new ProductUnitEntity();
                    //    pue.Code = txtUnitEditCode.Text;
                    //    pue.Name = txtUnitEditName.Text;
                    //    pue.Description = htmlDescription.Html;

                    //    if (hUnitEditId.Count > 0)
                    //    {
                    //        pu = uow.GetObjectByKey<ViewProductUnit>(long.Parse(hUnitEditId.Get("id").ToString()));
                    //        pue.ProductUnitId = pu.ProductUnitId;
                    //        pue.ProductUnitPropertyId = pu.ProductUnitPropertyId;
                    //        pue.RowStatus = char.Parse(cboUnitEditRowStatus.Value.ToString());

                    //        ProductUnitBLO.updateProductUnit(pue);
                    //    }
                    //    else
                    //    {
                    //        pue.RowCreationTimeStamp = DateTime.Now;
                    //        pue.Language = Constant.LANG_DEFAULT;
                    //        pue.RowStatus = Constant.ROWSTATUS_ACTIVE;

                    //        ProductUnitBLO.insertProductUnit(pue);
                    //    }
                //    }
                    
                //    hUnitEditId.Clear();
                //    formUnitEdit.ShowOnPageLoad = false;

                //    cpUnitEdit.JSProperties.Add("cpRefresh", "resfresh");

                    break;
                default:
                    break;
            }

        }

        protected void txtUnitEditCode_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
            e.IsValid = validate();
            if (txtUnitEditCode.ValidationSettings.ErrorText == "")
            {
                e.IsValid = true;
            }
        }

        protected void txtUnitEditName_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
            e.IsValid = validate();
            if (txtUnitEditName.ValidationSettings.ErrorText == "")
            {
                e.IsValid = true;
            }
        }
       
    }
}