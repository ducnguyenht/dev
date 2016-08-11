using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.PurchasingBLO;
using DevExpress.Web.ASPxHtmlEditor;
//using BLL.BO.Purchasing;
using DevExpress.Web.ASPxEditors;

namespace WebModule.Purchasing.UserControl
{
    public partial class uDeviceUnit : System.Web.UI.UserControl
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
                pagDeviceUnitEdit.ActiveTabIndex = 0;
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
        //protected ToolUnitEntity ToolUnitEntity
        //{
        //    get
        //    {
        //        Guid recordId = Guid.Empty;
        //        try
        //        {
        //            recordId = (Guid)Session["ToolUnitId"];
        //            if (recordId == Guid.Empty) return null;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //        ToolUnitEntity toolUnitEntity;
        //        this.toolBOL.getToolUnitByKey(recordId, out toolUnitEntity);
        //        return toolUnitEntity;
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
                    Session["ToolUnitId"] = Guid.Empty;
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

                    //ToolUnitEntity toolEntity;
                    Guid guid = new Guid(args[1]);

                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    Session["ToolUnitId"] = guid;
                    /////2013-09-21 ERP-580 Khoa.Truong INS END

                    //frmlInfoGeneral.DataSource = this.toolBOL.getToolUnitByKey(guid, out toolEntity);
                    frmlInfoGeneral.DataBind();
                    //this.cboManufacturer.SelectedItem.Value = "123";
                    //HtmlEditorDescription.Html = toolEntity.Description;
                    //formDeviceUnitEdit.HeaderText = "Thông tin công cụ dụng cụ - Mã số: " + toolEntity.Code;
                    formDeviceUnitEdit.ShowOnPageLoad = true;

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
                        if (!ASPxEdit.AreEditorsValid(pagDeviceUnitEdit, true))
                        {
                            formDeviceUnitEdit.JSProperties.Add("cpInvalid", true);
                            pagDeviceUnitEdit.ActiveTabIndex = 0;
                            return;
                        }
                        /////2013-09-21 ERP-580 Khoa.Truong INS START

                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            //ToolUnitEntity entity = new ToolUnitEntity();
                            //entity.ToolUnitId = recordId;
                            //entity.Code = txtCode.Text;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //this.toolBOL.updateToolUnit(entity);
                        }
                        else
                        {
                            //Insert mode
                            //ToolUnitEntity entity = new ToolUnitEntity();
                            //entity.Code = txtCode.Text;
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //toolBOL.insertToolUnit(entity);
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
                        formDeviceUnitEdit.JSProperties.Add("cpCallbackArgs",
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
            String toolUnitCode = e.Value.ToString().Trim();
            //New mode
            if ((Guid)Session["ToolUnitId"] == Guid.Empty)
            {
                //if (toolBOL.isToolUnitCodeExist(toolUnitCode))
                //{
                //    e.IsValid = false;
                //    e.ErrorText = String.Format("Mã đơn vị tính công cụ dụng cụ '{0}' đã được sử dụng", toolUnitCode);
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
                //if (!toolUnitCode.Equals(this.ToolUnitEntity.Code))
                //{
                //    if (toolBOL.isToolUnitCodeExist(toolUnitCode))
                //    {
                //        e.IsValid = false;
                //        e.ErrorText = String.Format("Mã đơn vị tính công cụ dụng cụ '{0}' đã được sử dụng", toolUnitCode);
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
        
        /// <summary>
        /// Implements IFormEditBase interface
        /// </summary>
        #region IFormEditBase Implementation
        public event WebModule.Interfaces.FormEditEventHandler Saved;

        public void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmlInfoGeneral);
            pagDeviceUnitEdit.ActiveTabIndex = 0;
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