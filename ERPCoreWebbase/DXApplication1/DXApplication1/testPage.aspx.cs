using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using Utility;
using NAS.BO.ETL.Accounting;
using NAS.BO.ETL.Accounting.TempData;
using NAS.DAL.Accounting.Currency;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Accounting.Journal;
using NAS.BO.ETL.Inventory;
using NAS.BO.ETL.Inventory.TempData;
using NAS.DAL.Inventory.Ledger;
using Evaluant.Calculator;

namespace WebModule
{
    public partial class testPage : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //ETLAccountingBO bo = new ETLAccountingBO();
            //ETL_Transaction test = bo.ExtractTransaction(session, Guid.Parse("de01050f-7f72-47b9-9b28-7d51731abf99"));
            //List<ETL_GeneralLedger> ledgerlist = bo.TransformGeneralJournalToGeneralLedger(test);
            //bo.LoadGeneralLedger(session,ledgerlist);
            //AccountingPeriodBO accBO = new AccountingPeriodBO();
            
            //ETLInventoryBO invBO = new ETLInventoryBO();
            //invBO.PopulateCOGS(session);
            //ETL_InventoryTransaction transaction = invBO.ExtractInventoryTransaction(session, Guid.Parse("e377fe17-9d84-4088-b006-69d93af18bda"));
            
            //COGS cogs = invBO.GetPreviousCOGS(session, Guid.Parse("bada6b03-a8ad-494a-9d8f-c134c633b2ce"));
            //invBO.RepairCOGS(session, Guid.Parse("00000000-0000-0000-0000-000000000002"));
            //XPQuery<AccountingPeriod> qr1 = new XPQuery<AccountingPeriod>(session);            
            //var col = from r in qr1
            //                             where r.RowStatus >= 0
            //                             && r.FromDateTime <= DateTime.Now
            //                             && r.ToDateTime >= DateTime.Now
            //                             select r;
            //AccountingPeriod a = AccountingPeriodBO.GetAccountingPeriod(session, DateTime.Now);

            Expression ex = new Expression("[01]-[02]");
            
            ex.Parameters.Add("01", 10);
            ex.Parameters.Add("02", 20);
            object r = ex.Evaluate();
            Response.Write(r.ToString());

        }
    }
}