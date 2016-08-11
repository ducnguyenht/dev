using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL.Invoice;
using NAS.DAL;
using Utility;

namespace WebModule.Invoice.PurchaseInvoice.GUI
{
    public partial class ServicePurchaseInvoiceListingForm : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}