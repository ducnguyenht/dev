using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;

using DevExpress.Web.ASPxEditors;
using WebModule.Interfaces;

using DevExpress.Web.ASPxHtmlEditor;

namespace ERPCore.Purchasing.UserControl
{
    public partial class uMaufacturerGroupEdit : System.Web.UI.UserControl, IFormEditBase
    {

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelpers.GetNewSession();
            dsManufacturerCategory.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pagManufacturerGroupEdit.ActiveTabIndex = 0;
            }
            //manufacturerCategoryBLO = new ManufacturerCategoryBLO();

            frmlManufactureCategory.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected ASPxHtmlEditor HtmlEditorDescription { 
            get { 
                return (ASPxHtmlEditor)navbarDetailInfo.Groups.FindByName("Description").FindControl("htmleditorDescription"); 
            } 
        } 

        //private ManufacturerCategoryBLO manufacturerCategoryBLO;

        //protected ManufacturerCategoryEntity ManufacturerCategoryEntity {
        //    get {
        //        Guid recordId = Guid.Empty;
        //        try 
        //        {
        //            recordId = (Guid)Session["ManufacturerCategoryId"];
        //            if (recordId == Guid.Empty) return null;
        //        }
        //        catch (Exception)
        //        {
        //            return null;                    
        //        }
        //        return manufacturerCategoryBLO.
        //            getManufacturerCategoryEntity(recordId, Utility.CurrentSession.Instance.Lang);
        //    }
        //}

        protected void popManufacturerGroupEdit_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":
                    Session["ManufacturerCategoryId"] = Guid.Empty;
                    frmlManufactureCategory.DataSourceID = null;
                    ClearForm();
                    //popManufacturerGroupEdit.ShowOnPageLoad = true;
                    break;
                case "edit":
                    ClearForm();
                    frmlManufactureCategory.DataSourceID = "dsManufacturerCategory";
                    if (args.Length > 1)
                    {

                        Session["ManufacturerCategoryId"] = Guid.Parse(args[1]);

                        dsManufacturerCategory.CriteriaParameters["ManufacturerCategoryId"].DefaultValue = args[1];
                        dsManufacturerCategory.CriteriaParameters["Language"].DefaultValue = Utility.CurrentSession.Instance.Lang;

                        //ManufacturerCategoryEntity manufacturerCategoryEntity = this.ManufacturerCategoryEntity;
                        //HtmlEditorDescription.Html = manufacturerCategoryEntity.Description;
                        //txtCode.Text = manufacturerCategoryEntity.Code;

                    }

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
                            //ManufacturerCategoryEntity entity = new ManufacturerCategoryEntity();
                            //entity.ManufacturerCategoryId = recordId;
                            //entity.Code = txtCode.Text;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //manufacturerCategoryBLO.Update(entity);
                        }
                        else
                        {
                            //Insert mode
                            //ManufacturerCategoryEntity entity = new ManufacturerCategoryEntity();
                            //entity.Code = txtCode.Text;
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //manufacturerCategoryBLO.Insert(entity);
                        }
                        
                        //popManufacturerGroupEdit.ShowOnPageLoad = false;
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        throw;
                    }
                    finally
                    {
                        OnSaved(new FormEditEventArgs() { isSuccess = isSuccess });
                        popManufacturerGroupEdit.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));
                    }                    
                    break;
                default:
                    break;
            }
        }

        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String manufacturerCategoryCode = e.Value.ToString().Trim();
            //New mode
            if ((Guid)Session["ManufacturerCategoryId"] == Guid.Empty)
            {
                //if (manufacturerCategoryBLO.isCodeExist(manufacturerCategoryCode))
                //{
                //    e.IsValid = false;
                //    e.ErrorText = String.Format("Mã nhóm nhà sản xuất '{0}' đang được sử dụng", manufacturerCategoryCode);
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
                //if (!manufacturerCategoryCode.Equals(this.ManufacturerCategoryEntity.Code))
                //{
                    //if (manufacturerCategoryBLO.isCodeExist(manufacturerCategoryCode))
                    //{
                    //    e.IsValid = false;
                    //    e.ErrorText = String.Format("Mã nhóm nhà sản xuất '{0}' đang được sử dụng", manufacturerCategoryCode);
                    //}
                    //else
                    //{
                    //    e.IsValid = true;
                    //    e.ErrorText = String.Empty;
                    //}
                //}
            }
        }

        /// <summary>
        /// Implements IFormEditBase interface
        /// </summary>
        #region IFormEditBase Implementation
            public event FormEditEventHandler Saved;

            public void ClearForm()
            {
                //clear all fields inside form layout
                ASPxEdit.ClearEditorsInContainer(frmlManufactureCategory);
                pagManufacturerGroupEdit.ActiveTabIndex = 0;
                cbRowStatus.SelectedIndex = 0;
                HtmlEditorDescription.Html = String.Empty;
                txtCode.IsValid = true;
                txtName.IsValid = true;
            }

            public virtual void OnSaved(FormEditEventArgs e)
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