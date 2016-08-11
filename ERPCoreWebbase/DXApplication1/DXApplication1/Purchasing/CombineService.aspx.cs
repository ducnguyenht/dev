using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;


namespace WebModule.Purchasing
{
    public partial class CombineService : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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

        private Session session;

        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    session = XpoHelpers.GetNewSession();
        //    dsBuyingServiceCategoryProperty.Session = session;
        //    dsBuyingService.Session = session;
        //}

        //private BuyingServiceBLO buyingServiceBLO;
        //private BuyingServiceCategoryBLO buyingServiceCategoryBLO;

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    buyingServiceBLO = new BuyingServiceBLO();
        //    buyingServiceCategoryBLO = new BuyingServiceCategoryBLO();

        //    if (!IsPostBack)
        //    {
        //        pagCombineService.ActiveTabIndex = 0;
        //    }

        //    dsBuyingServiceCategoryProperty.CriteriaParameters["Language"].DefaultValue = Utility.CurrentSession.Instance.Lang;
        //    dsBuyingService.CriteriaParameters["Language"].DefaultValue = Utility.CurrentSession.Instance.Lang;
        //}

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void grdDataServiceCategory_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("BuyingServiceCategoryId.RowStatus"))
            {
                //if (e.Value.Equals(DbRowStatusCode.Active.Value))
                //{
                //    e.DisplayText = "Hoạt động";
                //}
                //else if (e.Value.Equals(DbRowStatusCode.Inactive.Value))
                //{
                //    e.DisplayText = "Ngừng hoạt động";
                //}
            }
        }

        protected void grdDataServiceCategory_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] args = e.Parameters.Split('|');
            switch (args[0])
            {
                case "delete":
                    try
                    {
                        if (args.Length > 1)
                        {
                            Guid recordId = Guid.Parse(args[1]);
                            //buyingServiceCategoryBLO.DeleteLogical(recordId);
                            grdDataServiceCategory.JSProperties.Add("cpEvent", "deleted");
                        }
                        else
                        {
                            throw new Exception("Must be pass id of the record");
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;
                default:
                    break;
            }
        }

        protected void grdDataService_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("RowStatus"))
            {
                //if (e.Value.Equals(DbRowStatusCode.Active.Value))
                //{
                //    e.DisplayText = "Hoạt động";
                //}
                //else if (e.Value.Equals(DbRowStatusCode.Inactive.Value))
                //{
                //    e.DisplayText = "Ngừng hoạt động";
                //}
            }
        }

        protected void grdDataService_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] args = e.Parameters.Split('|');
            switch (args[0])
            {
                case "delete":
                    try
                    {
                        if (args.Length > 1)
                        {
                            Guid recordId = Guid.Parse(args[1]);
                            //buyingServiceBLO.DeleteLogical(recordId);
                            grdDataService.JSProperties.Add("cpEvent", "deleted");
                        }
                        else
                        {
                            throw new Exception("Must be pass id of the record");
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;
                default:
                    break;
            }
        }
        
    }
}