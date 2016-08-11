using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Invoice;
using DevExpress.Data.Filtering;
using NAS.BO.Invoice;

namespace WebModule.Invoice.PurchaseInvoice.GUI
{
    public partial class PurchaseInvoiceListingForm : System.Web.UI.UserControl
    {
        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsPurchaseInvoice.Session = session;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        public BillTypeEnum BillType { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("BillType", (byte)BillType),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual)
                );
            dsPurchaseInvoice.Criteria = criteria.ToString();

            purchaseInvoiceEditingForm.BillType = BillType;
        }

        protected void grdPurchase_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] args = e.Parameters.Split('|');
            string command = args[0];
            Guid billId = Guid.Empty;
            BillBOBase billBOBase = new PurchaseInvoiceBO();
            switch (command)
            {
                case "Delete":
                    if (args.Length < 2)
                        throw new Exception("Invalid parameter");
                    billId = Guid.Parse(args[1]);
                    using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                    {
                        billBOBase.Delete(uow, billId);
                        uow.CommitChanges();
                        grdPurchase.DataBind();
                    }
                    break;
                case "Copy":
                    if (args.Length < 2)
                        throw new Exception("Invalid parameter");
                    billId = Guid.Parse(args[1]);
                    using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                    {
                        billBOBase.Clone(uow, billId);
                        uow.CommitChanges();
                        grdPurchase.DataBind();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}