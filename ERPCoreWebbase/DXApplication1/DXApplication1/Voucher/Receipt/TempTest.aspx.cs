using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.BO.Accounting.Journal;
using DevExpress.Xpo;
using NAS.DAL;
using WebModule.Accounting.Journal.Transaction.Control.Strategy;
using NAS.DAL.Vouches;

namespace WebModule.Voucher.Receipt
{
    public partial class TempTest : System.Web.UI.Page
    {
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ReceiptVouches voucher = 
                session.GetObjectByKey<ReceiptVouches>(Guid.Parse("C5E3DCD2-F8F1-4D5D-9531-B29548B9D699"));
            GridViewBookingEntries1.SetDataSource(voucher.ReceiptVouchesTransactions);
            GridViewBookingEntries2.SetDataSource(voucher.ReceiptVouchesTransactions);
        }
    }
}