using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.BO.Invoice;
using NAS.DAL.Invoice;

namespace WebModule.Invoice.Control.BillItemEditForm.Strategy
{
    public class SalesInvoiceBillItemEditFormStrategy : BillItemEditFormStrategy
    {
        public override void CreateBillItem(DevExpress.Xpo.Session session, Guid billId, Guid itemId, Guid unitId, double quantity, double price, double promotionInPercentage, string comment)
        {
            SalesInvoiceBO salesInvoiceBO = new SalesInvoiceBO();
            salesInvoiceBO.CreateBillItem(session, billId, itemId, unitId, quantity, price, promotionInPercentage, comment);
        }

        public override void UpdateBillItem(DevExpress.Xpo.Session session, Guid billItemId, Guid itemId, Guid unitId, double quantity, double price, double promotionInPercentage, string comment)
        {
            SalesInvoiceBO salesInvoiceBO = new SalesInvoiceBO();
            salesInvoiceBO.UpdateBillItem(session, billItemId, itemId, unitId, quantity, price, promotionInPercentage, comment);
        }

        public override void DeleteBillItem(DevExpress.Xpo.Session session, Guid billItemId)
        {
            SalesInvoiceBO salesInvoiceBO = new SalesInvoiceBO();
            salesInvoiceBO.DeleteBillItem(session, billItemId);
        }
    }
}