using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Utility;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Invoice;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using NAS.BO.System.ArtifactCode;
using DevExpress.Data.Filtering;

namespace ERPCore.Purchasing
{
    public partial class Purchase : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);

        }
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_PRODUCT_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_GROUPID;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {            
            session = NAS.DAL.XpoHelper.GetNewSession();
            
            PurchaseReceiptXDS.Session = session;
            PurchaseReceiptXDS.Criteria = "[RowStatus] > 0"; 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxWebControl.RegisterUtilsScript(this);

            if (!Page.IsPostBack || !Page.IsCallback)
            {
                //grdPurchase.DataSourceID = "PurchaseReceiptXDS";
                //grdPurchase.DataBind();
            }
        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdPurchase.CancelEdit();
            grdPurchase.JSProperties.Add("cpPurchaseEdit", "new");
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdPurchase.CancelEdit();
            grdPurchase.JSProperties.Add("cpPurchaseEdit", "edit");
        }

        protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SalesInvoice salesInvoice = session.GetObjectByKey<SalesInvoice>(Guid.Parse(e.Values["BillId"].ToString()));

            if (salesInvoice.RowStatus > 1)
            {
                ((ASPxGridView)sender).JSProperties.Add("cpUndelete", "true");
            }
            else
            {
                salesInvoice.RowStatus = -1;
                salesInvoice.Save();
            }

            e.Cancel = true;
            
        }

        protected void grdPurchase_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] p = e.Parameters.Split('|');
            switch (p[0])
            {
                case "copy":
                    Guid id = Guid.Parse(p[1].ToString());

                    Bill bill = session.GetObjectByKey<Bill>(id);
                    if (bill != null)
                    {
                        ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();

                        Bill billCopy = new Bill(session);
                        billCopy = bill;

                        billCopy.BillId = new Guid();                        
                        billCopy.RowStatus = 1;

                        billCopy.Save();

                        CriteriaOperator filter = new BinaryOperator("BillId", bill.BillId, BinaryOperatorType.Equal);
                        XPCollection<BillItem> collectBillItem = new XPCollection<BillItem>(session, filter);

                        foreach (BillItem bi in collectBillItem)
                        {
                            BillItem billItem = new BillItem(session);
                            billItem = bi;

                            billItem.BillId = billCopy;
                            billItem.RowStatus = 1;

                            billItem.Save();
                        }

                        grdPurchase.JSProperties.Add("cpRefresh", billCopy.Code);
                    }

                    break;
                default:
                    break;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }
    }
}