using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.PurchasingBLO;
//using BLL.BO.Purchasing;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxEditors;

namespace WebModule.Purchasing.UserControl
{
    public partial class uActiveElement : System.Web.UI.UserControl
    {

        //ActiveElementBLO activeElementBLO = new ActiveElementBLO();
        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pagManufacturerGroupEdit.ActiveTabIndex = 0;
            }
            frmlActiveElement.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            //session.Dispose();
            //this.activeElementBLO.DisposeSession();
        }

        protected ASPxHtmlEditor HtmlEditorDescription
        {
            get
            {
                return (ASPxHtmlEditor)navbarDetailInfo.Groups.FindByName("Description").FindControl("htmleditorDescription");
            }
        }

        /////2013-09-20 ERP-572 Khoa.Truong INS START
        //protected ActiveElementEntity ActiveElementEntity {
        //    get {
        //        Guid recordId = Guid.Empty;
        //        try
        //        {
        //            recordId = (Guid)Session["ActiveElementId"];
        //            if (recordId == Guid.Empty) return null;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //        ActiveElementEntity elementEntity;
        //        this.activeElementBLO.getActiveElementByKey(recordId, out elementEntity);
        //        return elementEntity;
        //    }
        //}
        /////2013-09-20 ERP-572 Khoa.Truong INS END

        protected void popManufacturerGroupEdit_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":
                    frmlActiveElement.DataSourceID = null;
                    this.ClearForm();

                    /////2013-09-20 ERP-572 Khoa.Truong INS START
                    Session["ActiveElementId"] = Guid.Empty;
                    /////2013-09-20 ERP-572 Khoa.Truong INS END

                    //popManufacturerGroupEdit.ShowOnPageLoad = true;
                    break;
                case "edit":
                    /*frmlActiveElement.DataSourceID = "dsManufacturerCategory";
                    if (args.Length > 1)
                    {
                        dsManufacturerCategory.CriteriaParameters["ManufacturerCategoryId"].DefaultValue = args[1];
                    }*/

                    /////2013-09-20 ERP-572 Khoa.Truong INS START
                    this.ClearForm();
                    /////2013-09-20 ERP-572 Khoa.Truong INS END

                   // ActiveElementEntity elementEntity;
                    Guid guid = new Guid(args[1]);

                    /////2013-09-20 ERP-572 Khoa.Truong INS START
                    Session["ActiveElementId"] = guid;
                    /////2013-09-20 ERP-572 Khoa.Truong INS END

                    //frmlActiveElement.DataSource = this.activeElementBLO.getActiveElementByKey(guid, out elementEntity);
                    frmlActiveElement.DataBind();
                    //HtmlEditorDescription.Html = elementEntity.Description;
                    //popManufacturerGroupEdit.HeaderText = "Thông tin hoạt chất - Mã số: " + elementEntity.Code;
                    popManufacturerGroupEdit.ShowOnPageLoad = true;

                    /////2013-09-20 ERP-572 Khoa.Truong INS START
                    //txtCode.Text = elementEntity.Code;
                    /////2013-09-20 ERP-572 Khoa.Truong INS END
                    
                    /*HtmlEditorDescription.Html =
                        manufacturerCategoryBLO.getManufacturerCategoryEntity(Guid.Parse(args[1]),
                                                                       Utility.CurrentSession.Instance.Lang).Description;*/
                    //popManufacturerGroupEdit.ShowOnPageLoad = true;
                    break;
                case "save":
                    bool isSuccess = true;
                    string recordIdStr = null;
                    try
                    {
                        //Check validation
                        if (!ASPxEdit.AreEditorsValid(pagManufacturerGroupEdit, true))
                        {
                            popManufacturerGroupEdit.JSProperties.Add("cpInvalid", true);
                            pagManufacturerGroupEdit.ActiveTabIndex = 0;
                            return;
                        }
                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            //ActiveElementEntity entity = new ActiveElementEntity();
                            //entity.ActiveElementId = recordId;
                            //entity.Code = txtCode.Text;
                            //entity.ActiveComponent = txtComponent.Text;
                            //entity.ActiveFunction = txtFunction.Text;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //this.activeElementBLO.updateActiveElement(entity);
                        }
                        else
                        {
                            //Insert mode
                            //ActiveElementEntity entity = new ActiveElementEntity();
                            //entity.Code = txtCode.Text;
                            //entity.ActiveComponent = txtComponent.Text;
                            //entity.ActiveFunction = txtFunction.Text;
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            ////activeElementBLO.insertActiveElement(entity);
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
                        popManufacturerGroupEdit.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));
                    }
                    break;
                default:
                    break;
            }
        }

        /////2013-09-20 ERP-572 Khoa.Truong INS START
        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String activeElementCode = e.Value.ToString().Trim();
            //New mode
            if ((Guid)Session["ActiveElementId"] == Guid.Empty)
            {
                //if (this.activeElementBLO.isCodeExist(activeElementCode))
                //{
                //    e.IsValid = false;
                //    e.ErrorText = String.Format("Mã hoạt chất '{0}' đang được sử dụng", activeElementCode);
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
                //if (!activeElementCode.Equals(this.ActiveElementEntity.Code))
                //{
                //    if (this.activeElementBLO.isCodeExist(activeElementCode))
                //    {
                //        e.IsValid = false;
                //        e.ErrorText = String.Format("Mã hoạt chất '{0}' đang được sử dụng", activeElementCode);
                //    }
                //    else
                //    {
                //        e.IsValid = true;
                //        e.ErrorText = String.Empty;
                //    }
                //}
            }
        }
        /////2013-09-20 ERP-572 Khoa.Truong INS END

        /// <summary>
        /// Implements IFormEditBase interface
        /// </summary>
        #region IFormEditBase Implementation
        public event WebModule.Interfaces.FormEditEventHandler Saved;

        public void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmlActiveElement);
            pagManufacturerGroupEdit.ActiveTabIndex = 0;
            cbRowStatus.SelectedIndex = 0;
            HtmlEditorDescription.Html = String.Empty;

            /////2013-09-20 ERP-572 Khoa.Truong INS START
            this.txtCode.IsValid = true;
            this.txtName.IsValid = true;
            this.txtComponent.IsValid = true;
            this.txtFunction.IsValid = true;
            /////2013-09-20 ERP-572 Khoa.Truong INS END
        }

        public virtual void OnSaved(WebModule.Interfaces.FormEditEventArgs e)
        {
            if (Saved != null)
            {
                Saved(this, e);
            }
        }
        #endregion

        protected void cpLineManufacturerGroup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }
    }
}