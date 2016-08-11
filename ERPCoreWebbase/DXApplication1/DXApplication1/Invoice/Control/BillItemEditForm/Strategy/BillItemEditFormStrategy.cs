using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using NAS.DAL.Invoice;

namespace WebModule.Invoice.Control.BillItemEditForm.Strategy
{
    public abstract class BillItemEditFormStrategy
    {
        public abstract void CreateBillItem(
            DevExpress.Xpo.Session session, 
            Guid billId, 
            Guid itemId, 
            Guid unitId, 
            double quantity, 
            double price, 
            double promotionInPercentage,
            string comment);

        public abstract void UpdateBillItem(
            DevExpress.Xpo.Session session, 
            Guid billItemId, 
            Guid itemId, 
            Guid unitId, 
            double quantity, 
            double price, 
            double promotionInPercentage,
            string comment);

        public abstract void DeleteBillItem(DevExpress.Xpo.Session session, Guid billItemId);

        public virtual DevExpress.Xpo.XPCollection<NAS.DAL.Invoice.BillItem> GetBillItems(DevExpress.Xpo.Session session, Guid billId)
        {
            Bill bill = session.GetObjectByKey<Bill>(billId);
            if (bill != null)
                return bill.BillItems;
            return null;
        }
    }
}