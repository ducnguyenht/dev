using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Collections;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Nomenclature.Item;

namespace WebModule.Purchasing
{
    public partial class CombineProduct : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session;

        /*2013-11-28 Khoa.Truong DEL START*/
        //private XPCollection<ObjectType> ObjectTypes {
        //    get { return Session["ObjectTypes" + ClientID] as XPCollection<ObjectType>; }
        //    set { Session["ObjectTypes" + ClientID] = value;}
        //}
        /*2013-11-28 Khoa.Truong DEL END*/

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

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            ProductXDS.Session = session;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            //session.Dispose();
            /*2013-11-28 Khoa.Truong INS START*/
            session.Dispose();
            /*2013-11-28 Khoa.Truong INS END*/
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /*2013-11-28 Khoa.Truong DEL START*/
            //if (!IsPostBack)
            //{
            //    XPCollection<Item> items = new XPCollection<Item>(session, new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
            //    CriteriaOperator criteria = new ContainsOperator("ItemCustomTypes", new InOperator("ItemId", items));
            //    ObjectTypes = new XPCollection<ObjectType>(session);
            //    ObjectTypes.Filter = criteria;
            //}
            //cboObjectType.DataSource = ObjectTypes;
            /*2013-11-28 Khoa.Truong DEL END*/

            /*2013-11-28 Khoa.Truong INS START*/
            CriteriaOperator criteria = new BinaryOperator("Category",
                Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.ITEM));
            var objectTypes = new XPCollection<ObjectType>(session, criteria);
            cboObjectType.DataSource = objectTypes;
            /*2013-11-28 Khoa.Truong INS END*/
            cboObjectType.DataBind();

            loadItemByCboType();
        }

        protected void grdProduct_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            loadItemByCboType();
        }

        public void loadItemByCboType()
        {
            string selectedType = cboObjectType.SelectedItem != null ? cboObjectType.SelectedItem.GetValue("ObjectTypeId").ToString() : "";
            Guid selectedObjectTypeId;
            if (!selectedType.Equals(""))
            {
                selectedObjectTypeId = Guid.Parse(selectedType);
                ProductXDS.Criteria = CriteriaOperator.And(new ContainsOperator("ItemCustomTypes",
                    new BinaryOperator("ObjectTypeId", selectedObjectTypeId, BinaryOperatorType.Equal)),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                    ).ToString();
            }
            else
            {
                ProductXDS.Criteria = (new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)).ToString();
            }
            grdProduct.DataBind();

            if (cboObjectType.SelectedItem != null)
            {
                grdProduct.Columns["Code"].Caption = String.Format("{0} {1}", "Mã", cboObjectType.SelectedItem.GetValue("Description").ToString());
                grdProduct.Columns["Name"].Caption = String.Format("{0} {1}", "Tên", cboObjectType.SelectedItem.GetValue("Description").ToString());
            }
        }
    }
}