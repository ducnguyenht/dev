using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using ERPPopulate.Accounting;
using ERPPopulate.Nomenclature.Organization;
using ERPPopulate.Nomenclature.Item;

namespace WebModule.NAANAdmin.SystemConfig
{
    public partial class ERPPopulate : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_SYSTEM_MAILSERVERCONFIG_ID;
            }
        }

        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_SYSTEM_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        private AccountPopulation acountingDataPopulation;
        private SupplierOrgPopulation supplierOrgPopulation;
        private ManufacturerOrgPopulation manufacturerOrgPopulation;
        private ItemPopulation itemPopulation;
        private UnitPopulation unitPopulation;

        protected void Page_Load(object sender, EventArgs e)
        {
            acountingDataPopulation = new AccountPopulation();
            supplierOrgPopulation = new SupplierOrgPopulation();
            manufacturerOrgPopulation = new ManufacturerOrgPopulation();
            itemPopulation = new ItemPopulation();
            unitPopulation = new UnitPopulation();
        }

        protected void cpPopulation_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string path = txtDataPath.Text;
            NAS.DAL.XpoHelper.ClearDataLayer(); 
            acountingDataPopulation.PopulateStandardTemplate(Server.MapPath(path+"/Account.xls"));
            manufacturerOrgPopulation.Populate(Server.MapPath(path + "/dmnoisanxuat.xls"));
            supplierOrgPopulation.Populate(Server.MapPath(path + "/dmkhachhang-nhacungcap.xls"));
            unitPopulation.Populate(Server.MapPath(path + "/dmhanghoa-vattuyte.xls"));
            itemPopulation.Populate(Server.MapPath(path + "/dmhanghoa-vattuyte.xls"));
        }
    }
}