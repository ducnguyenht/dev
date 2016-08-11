using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.GUI.Pattern;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.Configure;
using Utility;

namespace WebModule.Accounting.AllocationConfigure.GUI
{
    public partial class AllocationListing : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_CONFIG_ALLOCATION;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsAllocation.Session = session;
            dsAllocationType.Session = session;
        }

        public override void Dispose()
        {
            base.Dispose();
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateViewStateControlId();
                GUIContext = new Context();
            }
        }

        #region State Pattern

        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        private string ViewStateControlId
        {
            get
            {
                return (string)ViewState["ViewStateControlId"];
            }
            set
            {
                ViewState["ViewStateControlId"] = value;
            }
        }

        private Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_" + ViewStateControlId]; }
            set { Session["GUIContext_" + ViewStateControlId] = value; }
        }


        #region UpdateGUI
        #endregion


        #region CRUD

        public bool AllocationListing_CRUD()
        {
            return true;
        }

        #endregion

        protected void gridviewAllocation_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] args = e.Parameters.Split('|');
            string command = args[0];

            if (command.Equals("Delete"))
            {
                bool isSuccess = false;
                try
                {
                    if (args.Length < 2)
                    {
                        throw new Exception("Invalid parameters");
                    }
                    Guid allocationId = Guid.Parse(args[1]);
                    Allocation allocation = session.GetObjectByKey<Allocation>(allocationId);
                    if (allocation.VoucherAllocations != null && allocation.VoucherAllocations.Count > 0)
                    {
                        throw new Exception("The allocation has already reference to Voucher(s).");
                    }
                    allocation.Delete();
                    isSuccess = true;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (isSuccess)
                    {
                        gridviewAllocation.JSProperties["cpEvent"] = "deleted";
                    }
                }
            }
        }


        #region PreTransitionCRUD
        #endregion


        #endregion

    }
}