using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
//using DAL.Sale;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxEditors;
//using BLL.SalesBLO;
//using BLL.BO.Sale;

namespace WebModule.GUI.Sales.userControl
{
    public partial class uEditCustomerGrp : System.Web.UI.UserControl
    {
        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelpers.GetNewSession();
            dsCustomerCategory.Session = session;
        }

        protected ASPxHtmlEditor HtmlEditorDescription
        {
            get
            {
                return (ASPxHtmlEditor)navbarDetail.Groups.FindByName("Description").FindControl("htmleditorDescription");
            }
        }

        //private CustomerCategoryBLO customerCategoryBLO;

        protected void Page_Load(object sender, EventArgs e)
        {
            //customerCategoryBLO = new CustomerCategoryBLO();

            //if (!IsPostBack)
            //{
            //    pagCustomerCategory.ActiveTabIndex = 0;
            //}

            //frmCustomerCategoryGeneralInfo.DataBind();

        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        private void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmCustomerCategoryGeneralInfo);
            pagCustomerCategory.ActiveTabIndex = 0;
            cbRowStatus.SelectedIndex = 0;
            HtmlEditorDescription.Html = String.Empty;

            /////2013-09-23 ERP-570 Khoa.Truong INS START
            txtCode.IsValid = true;
            txtName.IsValid = true;
            /////2013-09-23 ERP-570 Khoa.Truong INS END

        }

        /////2013-09-23 ERP-570 Khoa.Truong INS START
        //protected CustomerCategoryEntity CustomerCategoryEntity
        //{
        //    get
        //    {
        //        Guid recordId = Guid.Empty;
        //        try
        //        {
        //            recordId = (Guid)Session["CustomerCategoryId"];
        //            if (recordId == Guid.Empty) return null;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //        //return customerCategoryBLO.getCustomerCategoryEntity(recordId, Utility.CurrentSession.Instance.Lang);
        //    }
        //}
        /////2013-09-23 ERP-570 Khoa.Truong INS END

        protected void popCustomerCategory_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":
                    /////2013-09-23 ERP-570 Khoa.Truong INS START
                    Session["CustomerCategoryId"] = Guid.Empty;
                    /////2013-09-23 ERP-570 Khoa.Truong INS END

                    frmCustomerCategoryGeneralInfo.DataSourceID = null;
                    ClearForm();
                    //popManufacturerGroupEdit.ShowOnPageLoad = true;
                    break;
                case "edit":
                    /////2013-09-23 ERP-570 Khoa.Truong INS START
                    ClearForm();
                    /////2013-09-23 ERP-570 Khoa.Truong INS END
                    pagCustomerCategory.ActiveTabIndex = 0;
                    frmCustomerCategoryGeneralInfo.DataSourceID = "dsCustomerCategory";
                    
                    if (args.Length > 1)
                    {
                        Guid customerCategoryId = Guid.Parse(args[1]);
                        /////2013-09-23 ERP-570 Khoa.Truong INS START
                        Session["CustomerCategoryId"] = customerCategoryId;
                        /////2013-09-23 ERP-570 Khoa.Truong INS END
                        dsCustomerCategory.CriteriaParameters["CustomerCategoryId"].DefaultValue = customerCategoryId.ToString();
                        dsCustomerCategory.CriteriaParameters["Language"].DefaultValue = Utility.CurrentSession.Instance.Lang;

                        //HtmlEditorDescription.Html = CustomerCategoryEntity.Description;

                        /////2013-09-23 ERP-570 Khoa.Truong INS START
                        //txtCode.Text = CustomerCategoryEntity.Code;
                        /////2013-09-23 ERP-570 Khoa.Truong INS END
                    }
                    //popManufacturerGroupEdit.ShowOnPageLoad = true;
                    break;
                case "save":
                    bool isSuccess = true;
                    string recordIdStr = null;
                    try
                    {

                        /////2013-09-23 ERP-570 Khoa.Truong INS START
                        //Check validation
                        if (!ASPxEdit.AreEditorsValid(pagCustomerCategory, true))
                        {
                            popCustomerCategory.JSProperties.Add("cpInvalid", true);
                            pagCustomerCategory.ActiveTabIndex = 0;
                            return;
                        }
                        /////2013-09-23 ERP-570 Khoa.Truong INS END

                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            //CustomerCategoryEntity entity = new CustomerCategoryEntity();
                            //entity.CustomerCategoryId = recordId;
                            //entity.Code = txtCode.Text;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //customerCategoryBLO.Update(entity);
                        }
                        else
                        {
                            //Insert mode
                            //CustomerCategoryEntity entity = new CustomerCategoryEntity();
                            //entity.Code = txtCode.Text;
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //customerCategoryBLO.Insert(entity);
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
                        popCustomerCategory.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));
                    }
                    break;
                default:
                    break;
            }
        }


        /////2013-09-23 ERP-570 Khoa.Truong INS START
        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String customerCategoryCode = e.Value.ToString().Trim();
            //New mode
            if ((Guid)Session["CustomerCategoryId"] == Guid.Empty)
            {
                //if (customerCategoryBLO.isCodeExist(customerCategoryCode))
                //{
                //    e.IsValid = false;
                //    e.ErrorText = String.Format("Mã nhóm khách hàng '{0}' đã được sử dụng", customerCategoryCode);
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
                //if (!customerCategoryCode.Equals(this.CustomerCategoryEntity.Code))
                //{
                //    if (customerCategoryBLO.isCodeExist(customerCategoryCode))
                //    {
                //        e.IsValid = false;
                //        e.ErrorText = String.Format("Mã nhóm khách hàng '{0}' đã được sử dụng", customerCategoryCode);
                //    }
                //    else
                //    {
                //        e.IsValid = true;
                //        e.ErrorText = String.Empty;
                //    }
                //}
            }
        }
        /////2013-09-23 ERP-570 Khoa.Truong INS END

    }
}