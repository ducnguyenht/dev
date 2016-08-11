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

namespace WebModule.Invoice.SalesInvoice.GUI
{
    public partial class SalesInvoiceListingForm : System.Web.UI.UserControl
    {
        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsSalesInvoice.Session = session;
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
            dsSalesInvoice.Criteria = criteria.ToString();

            salesInvoiceEditingForm.BillType = BillType;
        }

        protected void grdInvoice_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] args = e.Parameters.Split('|');
            string command = args[0];
            Guid billId = Guid.Empty;
            BillBOBase billBOBase = new SalesInvoiceBO();
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
                        grdInvoice.DataBind();
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
                        grdInvoice.DataBind();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}