using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using NAS.BO.Inventory.Jouranl;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Accounting.Journal;

namespace WebModule.Warehouse.UserControl
{
    public partial class uInitInventory : System.Web.UI.UserControl
    {
        Session session;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ItemUnitId"] == null)
                {
                    Session["ItemUnitId"] = Guid.NewGuid();
                }
                if (Session["InventorySelected"] == null)
                {
                    Session["InventorySelected"] = Guid.NewGuid();
                }
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        public void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmlInfoGeneral);
            pagWarehouseUnitEdit.ActiveTabIndex = 0;
            txtCode.IsValid = true;
            this.txtBalance.IsValid = true;
            this.txtPrice.IsValid = true;
            /////2013-09-21 ERP-580 Khoa.Truong INS END

        }

        protected void popWarehouseUnitEdit_WindowCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":
                    this.ClearForm();
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
                            InventoryTransactionBO invTBO = new InventoryTransactionBO();
                            Guid itemUnitId = Guid.Parse(Session["ItemUnitId"].ToString());
                            Guid inventoryId = Guid.Parse(Session["InventorySelected"].ToString());
                            float balance = 0;
                            try {
                                balance = float.Parse(this.txtBalance.Text);
                            } catch (Exception ex) {
                                balance = 0;
                            }
                            float price = 0;
                            try {
                                price = float.Parse(this.txtPrice.Text);
                            } catch (Exception ex) {
                                price = 0;
                            }
                            invTBO.ReceiptType = Utility.Constant.RECEIPT_PURCHASE;
                            //invTBO.dateTime = DateTime.Parse(txtIssuedDate.Value.ToString());
                            AccountingPeriod accPeriod = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                            //invTBO.dateTime = accPeriod.FromDateTime;
                            //invTBO.CreateInventoryTransactionBalanceForward(session, accPeriod.AccountingPeriodId, DateTime.Parse(txtIssuedDate.Value.ToString()),
                            //                                                                inventoryId, itemUnitId, this.txtCode.Text, "", balance, price);
                            invTBO.CreateInventoryTransactionBalanceForward(session, accPeriod.AccountingPeriodId, accPeriod.FromDateTime,
                                                                                            inventoryId, itemUnitId, this.txtCode.Text, "", balance, price);
                        
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
                    }
                    break;
                default:
                    break;
            }
        }
        public event WebModule.Interfaces.FormEditEventHandler Saved;
        public virtual void OnSaved(WebModule.Interfaces.FormEditEventArgs e)
        {
            if (Saved != null)
            {
                Saved(this, e);
            }
        }
        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String inventoryCode = e.Value.ToString().Trim();
            //New mode
            //if (PrivateSession.Instance.InventoryId == Guid.Empty)
            //{
            //    bool isExist = Util.isExistXpoObject<Inventory>("Code", inventoryCode);
            //    if (isExist)
            //    {
            //        e.IsValid = false;
            //        e.ErrorText =
            //            String.Format("Mã kho '{0}' đã được sử dụng", inventoryCode);
            //    }
            //    else
            //    {
            //        e.IsValid = true;
            //        e.ErrorText = String.Empty;
            //    }
            //}
            ////Edit mode  
            //else
            //{
            //    //Validate if new code not equal old code
            //    if (!inventoryCode.Equals(CurrentInventoryOrg.Code))
            //    {
            //        bool isExist = Util.isExistXpoObject<Inventory>("Code", inventoryCode);
            //        if (isExist)
            //        {
            //            e.IsValid = false;
            //            e.ErrorText = String.Format("Mã kho '{0}' đã được sử dụng", inventoryCode);
            //        }
            //        else
            //        {
            //            e.IsValid = true;
            //            e.ErrorText = String.Empty;
            //        }
            //    }
            //}
        }
        protected void cpLineWarehouseUnit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }
        
    }
    
}