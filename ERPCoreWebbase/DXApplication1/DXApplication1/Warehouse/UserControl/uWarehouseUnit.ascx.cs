using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.PurchasingBLO;
//using BLL.BO.Purchasing;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Nomenclature.Inventory;

namespace WebModule.Warehouse.UserControl
{
    public partial class uWarehouseUnit : System.Web.UI.UserControl
    {
        private class PrivateSession
        {
            //private constructor
            private PrivateSession()
            {
                this.InventoryUnitId = Guid.Empty;
            }
            // Gets the current session
            public static PrivateSession Instance
            {
                get
                {
                    PrivateSession session =
                        (PrivateSession)HttpContext.Current.Session["ERPCore_uWarehouseUnit"];
                    if (session == null)
                    {
                        session = new PrivateSession();
                        HttpContext.Current.Session["ERPCore_uWarehouseUnit"] = session;
                    }
                    return session;
                }
            }

            /// <summary>
            /// Clear instance of PrivateSession
            /// </summary>
            public static void ClearInstance()
            {
                HttpContext.Current.Session.Remove("ERPCore_uWarehouseUnit");
            }

            /////Declares all session properties here
            public Guid InventoryUnitId { get; set; }

        }
        private InventoryUnit CurrentInventoryUnit
        {
            get
            {
                if (PrivateSession.Instance.InventoryUnitId == Guid.Empty)
                {
                    return null;
                }
                return
                    session.GetObjectByKey<InventoryUnit>(PrivateSession.Instance.InventoryUnitId);
            }
        }
        //WarehouseBLO toolBOL = new WarehouseBLO();
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pagWarehouseUnitEdit.ActiveTabIndex = 0;
            }
            frmlInfoGeneral.DataBind();
        }
        //protected ASPxHtmlEditor HtmlEditorDescription
        //{
        //    get
        //    {
        //        return (ASPxHtmlEditor)navbarDetailInfo.Groups.FindByName("Description").FindControl("htmleditorDescription");
        //    }
        //}
        private Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsInventoryUnit.Session = session;
        }

        protected void popWarehouseUnitEdit_WindowCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":
                    InventoryUnit invUnit = InventoryUnit.InitNewRow(this.session);
                    PrivateSession.Instance.InventoryUnitId = invUnit.InventoryUnitId;

                    this.dsInventoryUnit.CriteriaParameters["InventoryUnitId"].DefaultValue = PrivateSession.Instance.InventoryUnitId.ToString();
                    this.ClearForm();
                    break;
                case "edit":
                    this.ClearForm();

                    if (args.Length > 1)
                    {
                        PrivateSession.Instance.InventoryUnitId = Guid.Parse(args[1]);
                        dsInventoryUnit.CriteriaParameters["InventoryUnitId"].DefaultValue = PrivateSession.Instance.InventoryUnitId.ToString();
                        //this.txtName.Text = CurrentInventoryUnit.Name;
                        //txtCode.Text = CurrentManufacturerOrg.Code;
                    }
                    break;
                case "save":
                    bool isSuccess = true;
                    string recordIdStr = null;
                    try
                    {
                        //Check validation
                        if (!ASPxEdit.AreEditorsValid(this.pagWarehouseUnitEdit, true))
                        {
                            formWarehouseUnitEdit.JSProperties.Add("cpInvalid", true);
                            pagWarehouseUnitEdit.ActiveTabIndex = 0;
                            return;
                        }
                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            //Update general information
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            InventoryUnit editManufacturerOrg =
                                session.GetObjectByKey<InventoryUnit>(PrivateSession.Instance.InventoryUnitId);
                            //editManufacturerOrg.Code = txtCode.Text;
                            editManufacturerOrg.Name = txtName.Text;
                            //editManufacturerOrg.RowStatus = short.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            editManufacturerOrg.Save();
                        }
                        else
                        {
                            //Insert mode
                            InventoryUnit newManufacturerOrg =
                                session.GetObjectByKey<InventoryUnit>(PrivateSession.Instance.InventoryUnitId);
                            //newManufacturerOrg.Code = txtCode.Text;
                            newManufacturerOrg.Name = txtName.Text;
                            //newManufacturerOrg.RowStatus = short.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            newManufacturerOrg.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                            newManufacturerOrg.Save();
                        }
                    }
                    catch (Exception)
                    {
                        isSuccess = false;
                        throw;
                    }
                    finally
                    {
                        OnSaved(new WebModule.Interfaces.FormEditEventArgs() { isSuccess = isSuccess });
                        formWarehouseUnitEdit.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));
                        PrivateSession.ClearInstance();
                    }
                    break;
                default:
                    break;
            }
        }

        protected void cpLineWarehouseUnit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
            pagWarehouseUnitEdit.ActiveTabIndex = 0;
            this.cbRowStatus.SelectedIndex = 0;
            //HtmlEditorDescription.Html = String.Empty;

            /////2013-09-21 ERP-580 Khoa.Truong INS START
            //txtCode.IsValid = true;
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