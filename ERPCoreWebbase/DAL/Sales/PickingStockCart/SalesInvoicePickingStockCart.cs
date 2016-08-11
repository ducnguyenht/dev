using System;
using DevExpress.Xpo;
using NAS.DAL.Invoice;
namespace NAS.DAL.Sales.PickingStockCart
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class SalesInvoicePickingStockCart:NAS.DAL.Inventory.Operation.PickingStockCart
    {
        private SalesInvoice _SalesInvoiceId;
        public SalesInvoicePickingStockCart(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        [Association("SalesInvoicePickingStockCartReferencesSalesInvoice")]
        public SalesInvoice SalesInvoiceId
        {
            get
            {
                return _SalesInvoiceId;
            }
            set
            {
                SetPropertyValue("SalesInvoiceId", ref _SalesInvoiceId, value);
            }
        }
        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                SalesInvoicePickingStockCart sisc = new SalesInvoicePickingStockCart(session);
                sisc.Save();
            }
            catch (Exception ex)
            {
            }
        }
    }

}
