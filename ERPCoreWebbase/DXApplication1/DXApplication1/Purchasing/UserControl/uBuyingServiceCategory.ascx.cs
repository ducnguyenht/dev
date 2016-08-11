using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxEditors;
//using BLL.BO.Purchasing;
//using BLL.PurchasingBLO;
using DevExpress.Xpo;
//using DAL.Purchasing;

namespace DXApplication1.Purchasing.UserControl
{
    public partial class uBuyingServiceCategory : System.Web.UI.UserControl
    {

        //private BuyingServiceCategoryBLO buyingServiceCategoryBLO;

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelpers.GetNewSession();
            dsBuyingServiceCategoryProperty.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //buyingServiceCategoryBLO = new BuyingServiceCategoryBLO();
            frmBuyingServiceCategory.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        /////2013-09-25 ERP-607 Khoa.Truong INS START
        protected ASPxHtmlEditor HtmlEditorDescription
        {
            get
            {
                return (ASPxHtmlEditor)navbarDetail.Groups.FindByName("Description").FindControl("htmleditorDescription");
            }
        }
        /////2013-09-25 ERP-607 Khoa.Truong INS END

        /////2013-09-25 ERP-607 Khoa.Truong INS START
        //protected BuyingServiceCategoryEntity BuyingServiceCategoryEntity
        //{
        //    get
        //    {
        //        Guid recordId = Guid.Empty;
        //        try
        //        {
        //            recordId = (Guid)Session["BuyingServiceCategoryId"];
        //            if (recordId == Guid.Empty) return null;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //        return buyingServiceCategoryBLO.getBuyingServiceCategoryEntity(recordId, Utility.CurrentSession.Instance.Lang);
        //    }
        //}
        /////2013-09-25 ERP-607 Khoa.Truong INS END

        /////2013-09-25 ERP-607 Khoa.Truong INS START
        protected void popBuyingServiceCategory_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":
                    Session["BuyingServiceCategoryId"] = Guid.Empty;
                    frmBuyingServiceCategory.DataSourceID = null;
                    ClearForm();
                    break;
                case "edit":
                    ClearForm();
                    frmBuyingServiceCategory.DataSourceID = "dsBuyingServiceCategoryProperty";

                    if (args.Length > 1)
                    {
                        Guid customerCategoryId = Guid.Parse(args[1]);
                        Session["BuyingServiceCategoryId"] = customerCategoryId;
                        dsBuyingServiceCategoryProperty.CriteriaParameters["BuyingServiceCategoryId"].DefaultValue = customerCategoryId.ToString();
                        dsBuyingServiceCategoryProperty.CriteriaParameters["Language"].DefaultValue = Utility.CurrentSession.Instance.Lang;
                        //HtmlEditorDescription.Html = BuyingServiceCategoryEntity.Description;
                        //txtCode.Text = BuyingServiceCategoryEntity.Code;
                    }
                    break;
                case "save":
                    bool isSuccess = true;
                    string recordIdStr = null;
                    try
                    {
                        //Check validation
                        if (!ASPxEdit.AreEditorsValid(pagBuyingServiceCategory, true))
                        {
                            popBuyingServiceCategory.JSProperties.Add("cpInvalid", true);
                            pagBuyingServiceCategory.ActiveTabIndex = 0;
                            return;
                        }
                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            //recordIdStr = args[1];
                            //Guid recordId = Guid.Parse(recordIdStr);
                            //BuyingServiceCategoryEntity entity = new BuyingServiceCategoryEntity();
                            //entity.BuyingServiceCategoryId = recordId;
                            //entity.Code = txtCode.Text;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //buyingServiceCategoryBLO.Update(entity);
                        }
                        else
                        {
                            //Insert mode
                            //BuyingServiceCategoryEntity entity = new BuyingServiceCategoryEntity();
                            //entity.Code = txtCode.Text;
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //buyingServiceCategoryBLO.Insert(entity);
                        }
                    }
                    catch (Exception)
                    {
                        isSuccess = false;
                        throw;
                    }
                    finally
                    {
                        popBuyingServiceCategory.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));
                    }
                    break;
                default:
                    break;
            }
        }
        /////2013-09-25 ERP-607 Khoa.Truong INS END

        /////2013-09-25 ERP-607 Khoa.Truong INS START
        private void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmBuyingServiceCategory);
            pagBuyingServiceCategory.ActiveTabIndex = 0;
            cbRowStatus.SelectedIndex = 0;
            HtmlEditorDescription.Html = String.Empty;
            txtCode.IsValid = true;
            txtName.IsValid = true;
        }
        /////2013-09-25 ERP-607 Khoa.Truong INS END

        /////2013-09-25 ERP-607 Khoa.Truong INS START
        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String buyingServiceCategoryCode = e.Value.ToString().Trim();
            //New mode
            if ((Guid)Session["BuyingServiceCategoryId"] == Guid.Empty)
            {
                if (/*buyingServiceCategoryBLO.isCodeExist(buyingServiceCategoryCode)*/true)
                {
                    e.IsValid = false;
                    e.ErrorText = String.Format("Mã nhóm dịch vụ '{0}' đã được sử dụng", buyingServiceCategoryCode);
                }
                else
                {
                    e.IsValid = true;
                    e.ErrorText = String.Empty;
                }
            }
            //Edit mode
            else
            {
                //Validate if new code not equal old code
                if (/*!buyingServiceCategoryCode.Equals(this.BuyingServiceCategoryEntity.Code)*/true)
                {
                    if (/*buyingServiceCategoryBLO.isCodeExist(buyingServiceCategoryCode)*/true)
                    {
                        e.IsValid = false;
                        e.ErrorText = String.Format("Mã nhóm dịch vụ '{0}' đã được sử dụng", buyingServiceCategoryCode);
                    }
                    else
                    {
                        e.IsValid = true;
                        e.ErrorText = String.Empty;
                    }
                }
            }
        }
        /////2013-09-25 ERP-607 Khoa.Truong INS END
    }
}