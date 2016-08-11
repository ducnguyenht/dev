using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.BO.Purchasing;
//using BLL.PurchasingBLO;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxEditors;

namespace WebModule.Purchasing.UserControl
{
    public partial class uBuyingDeviceCategory : System.Web.UI.UserControl
    {
        //ToolBLO toolBOL = new ToolBLO();
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            //session.Dispose();
            //this.toolBOL.DisposeSession();
        }
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                pagDeviceCategoryEdit.ActiveTabIndex = 0;
            }
            frmlInfoGeneral.DataBind();
        }
        protected ASPxHtmlEditor HtmlEditorDescription
        {
            get
            {
                return (ASPxHtmlEditor)navbarDetailInfo.Groups.FindByName("Description").FindControl("htmleditorDescription");
            }
        }


        /////2013-09-21 ERP-580 Khoa.Truong INS START
        //protected BuyingToolCategoryEntity BuyingToolCategoryEntity
        //{
        //    get
        //    {
        //        Guid recordId = Guid.Empty;
        //        try
        //        {
        //            recordId = (Guid)Session["BuyingToolCategoryId"];
        //            if (recordId == Guid.Empty) return null;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //        BuyingToolCategoryEntity buyingToolCategoryEntity;
        //        this.toolBOL.getBuyingToolCategoryByKey(recordId, out buyingToolCategoryEntity);
        //        return buyingToolCategoryEntity;
        //    }
        //}
        /////2013-09-20 ERP-580 Khoa.Truong INS END

        protected void popDeviceCategoryEdit_WindowCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":

                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    Session["BuyingToolCategoryId"] = Guid.Empty;
                    /////2013-09-21 ERP-580 Khoa.Truong INS END

                    frmlInfoGeneral.DataSourceID = null;
                    ClearForm();
                    //popManufacturerGroupEdit.ShowOnPageLoad = true;
                    break;
                case "edit":
                    /*frmlActiveElement.DataSourceID = "dsManufacturerCategory";
                    if (args.Length > 1)
                    {
                        dsManufacturerCategory.CriteriaParameters["ManufacturerCategoryId"].DefaultValue = args[1];
                    }*/

                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    this.ClearForm();
                    /////2013-09-21 ERP-580 Khoa.Truong INS END

                    //BuyingToolCategoryEntity toolEntity;
                    Guid guid = new Guid(args[1]);

                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    Session["BuyingToolCategoryId"] = guid;
                    /////2013-09-21 ERP-580 Khoa.Truong INS END

                    //frmlInfoGeneral.DataSource = this.toolBOL.getBuyingToolCategoryByKey(guid, out toolEntity);
                    frmlInfoGeneral.DataBind();
                    //this.cboManufacturer.SelectedItem.Value = "123";
                    //HtmlEditorDescription.Html = toolEntity.Description;

                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    //txtCode.Text = toolEntity.Code;
                    /////2013-09-21 ERP-580 Khoa.Truong INS END

                    //formDeviceCategoryEdit.HeaderText = "Thông tin công cụ dụng cụ - Mã số: " + toolEntity.Code;
                    //formDeviceCategoryEdit.ShowOnPageLoad = true;
                    break;
                case "save":
                    bool isSuccess = true;
                    string recordIdStr = null;
                    try
                    {

                        /////2013-09-21 ERP-580 Khoa.Truong INS START
                        //Check validation
                        if (!ASPxEdit.AreEditorsValid(pagDeviceCategoryEdit, true))
                        {
                            formDeviceCategoryEdit.JSProperties.Add("cpInvalid", true);
                            pagDeviceCategoryEdit.ActiveTabIndex = 0;
                            return;
                        }
                        /////2013-09-21 ERP-580 Khoa.Truong INS START

                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            //BuyingToolCategoryEntity entity = new BuyingToolCategoryEntity();
                            //entity.BuyingToolCategoryId = recordId;
                            //entity.Code = txtCode.Text;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //this.toolBOL.updateBuyingToolCategory(entity);
                        }
                        else
                        {
                            //Insert mode
                            //BuyingToolCategoryEntity entity = new BuyingToolCategoryEntity();
                            //entity.Code = txtCode.Text;
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //toolBOL.insertBuyingToolCategory(entity);
                        }

                        //popManufacturerGroupEdit.ShowOnPageLoad = false;
                    }
                    catch (Exception)
                    {
                        isSuccess = false;
                        throw;
                    }
                    finally
                    {
                        OnSaved(new WebModule.Interfaces.FormEditEventArgs() { isSuccess = isSuccess });
                        formDeviceCategoryEdit.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));
                    }
                    break;
                default:
                    break;
            }
        }

        /////2013-09-21 ERP-580 Khoa.Truong INS START
        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String buyingToolCategoryCode = e.Value.ToString().Trim();
            //New mode
            if ((Guid)Session["BuyingToolCategoryId"] == Guid.Empty)
            {
                //if (toolBOL.isBuyingToolCategoryCodeExist(buyingToolCategoryCode))
                //{
                //    e.IsValid = false;
                //    e.ErrorText = String.Format("Mã nhóm công cụ dụng cụ '{0}' đã được sử dụng", buyingToolCategoryCode);
                //}
                //else
                //{
                //    e.IsValid = true;
                //    e.ErrorText = String.Empty;
                //}
            }
            //Edit mode
            else
            {
                //Validate if new code not equal old code
                //if (!buyingToolCategoryCode.Equals(this.BuyingToolCategoryEntity.Code))
                //{
                //    if (toolBOL.isBuyingToolCategoryCodeExist(buyingToolCategoryCode))
                //    {
                //        e.IsValid = false;
                //        e.ErrorText = String.Format("Mã nhóm công cụ dụng cụ '{0}' đã được sử dụng", buyingToolCategoryCode);
                //    }
                //    else
                //    {
                //        e.IsValid = true;
                //        e.ErrorText = String.Empty;
                //    }
                //}
            }
        }
        /////2013-09-21 ERP-580 Khoa.Truong INS END


        protected void txtCodeDeviceCategory_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {

        }

        protected void txtNameDeviceCategory_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {

        }

        protected void cpLineDeviceCategory_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }
        /// <summary>
        /// Implements IFormEditBase interface
        /// </summary>
        #region IFormEditBase Implementation
        public event WebModule.Interfaces.FormEditEventHandler Saved;

        public void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmlInfoGeneral);
            pagDeviceCategoryEdit.ActiveTabIndex = 0;
            this.cbRowStatus.SelectedIndex = 0;
            HtmlEditorDescription.Html = String.Empty;

            /////2013-09-21 ERP-580 Khoa.Truong INS START
            txtCode.IsValid = true;
            txtName.IsValid = true;
            /////2013-09-21 ERP-580 Khoa.Truong INS END

        }

        public virtual void OnSaved(WebModule.Interfaces.FormEditEventArgs e)
        {
            if (Saved != null)
            {
                Saved(this, e);
            }
        }
        #endregion
    }
}