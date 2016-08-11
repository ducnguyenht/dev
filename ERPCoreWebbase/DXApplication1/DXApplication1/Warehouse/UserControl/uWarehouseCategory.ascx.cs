using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.PurchasingBLO;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxHtmlEditor;
//using BLL.BO.Purchasing;

namespace WebModule.Warehouse.UserControl
{
    public partial class uWarehouseCategory : System.Web.UI.UserControl
    {
        //WarehouseBLO toolBOL = new WarehouseBLO();
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
                pagWarehouseCategoryEdit.ActiveTabIndex = 0;
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
        //protected WarehouseCategoryEntity WarehouseCategoryEntity
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
        //        WarehouseCategoryEntity buyingToolCategoryEntity;
        //        this.toolBOL.getWarehouseCategoryByKey(recordId, out buyingToolCategoryEntity);
        //        return buyingToolCategoryEntity;
        //    }
        //}
        /////2013-09-20 ERP-580 Khoa.Truong INS END

        protected void popWarehouseCategoryEdit_WindowCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

                    //WarehouseCategoryEntity toolEntity;
                    Guid guid = new Guid(args[1]);

                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    Session["BuyingToolCategoryId"] = guid;
                    /////2013-09-21 ERP-580 Khoa.Truong INS END

                    //frmlInfoGeneral.DataSource = this.toolBOL.getWarehouseCategoryByKey(guid, out toolEntity);
                    //frmlInfoGeneral.DataBind();
                    //this.cboManufacturer.SelectedItem.Value = "123";
                    //HtmlEditorDescription.Html = toolEntity.Description;

                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    //txtCode.Text = toolEntity.Code;
                    /////2013-09-21 ERP-580 Khoa.Truong INS END

                    break;
                case "save":
                    bool isSuccess = true;
                    string recordIdStr = null;
                    try
                    {

                        /////2013-09-21 ERP-580 Khoa.Truong INS START
                        //Check validation
                        if (!ASPxEdit.AreEditorsValid(pagWarehouseCategoryEdit, true))
                        {
                            formWarehouseCategoryEdit.JSProperties.Add("cpInvalid", true);
                            pagWarehouseCategoryEdit.ActiveTabIndex = 0;
                            return;
                        }
                        /////2013-09-21 ERP-580 Khoa.Truong INS START

                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            //WarehouseCategoryEntity entity = new WarehouseCategoryEntity();
                            //entity.WarehouseCategoryId = recordId;
                            //entity.Code = txtCode.Text;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //this.toolBOL.updateWarehouseCategory(entity);
                        }
                        else
                        {
                            //Insert mode
                            //WarehouseCategoryEntity entity = new WarehouseCategoryEntity();
                            //entity.Code = txtCode.Text;
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //toolBOL.insertWarehouseCategory(entity);
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
                        formWarehouseCategoryEdit.JSProperties.Add("cpCallbackArgs",
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
                //if (toolBOL.isWarehouseCategoryCodeExist(buyingToolCategoryCode))
                //{
                //    e.IsValid = false;
                //    e.ErrorText = String.Format("Mã thể loại kho '{0}' đã được sử dụng", buyingToolCategoryCode);
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
                //if (!buyingToolCategoryCode.Equals(this.WarehouseCategoryEntity.Code))
                //{
                //    if (toolBOL.isWarehouseCategoryCodeExist(buyingToolCategoryCode))
                //    {
                //        e.IsValid = false;
                //        e.ErrorText = String.Format("Mã thể loại kho '{0}' đã được sử dụng", buyingToolCategoryCode);
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


        protected void txtCodeWarehouseCategory_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {

        }

        protected void txtNameWarehouseCategory_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {

        }

        protected void cpLineWarehouseCategory_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
            pagWarehouseCategoryEdit.ActiveTabIndex = 0;
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