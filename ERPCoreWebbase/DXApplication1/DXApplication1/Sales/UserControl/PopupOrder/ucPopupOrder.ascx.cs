using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Invoice;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;

namespace WebModule.Sales.UserControl.PopupOrder
{
    public partial class ucPopupOrder : System.Web.UI.UserControl
    {
        Session session;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            BuyerXDS.Session = session; 
            BillActorXDS.Session = session;
        }

        protected void grdBillActor_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

        }

        protected void grdBillActor_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
        {

        }
        protected void colBillActorTypeItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            if (e.Value == null)
            {
                return;
            }

            BillActorType obj = session.GetObjectByKey<BillActorType>(Guid.Parse(e.Value.ToString()));

            if (obj != null)
            {
                comboItemUnit.DataSource = new BillActorType[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void colBillActorTypeItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<BillActorType> collection = new XPCollection<BillActorType>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));


            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();

        }
        protected void colPersonOnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            if (e.Value == null)
            {
                return;
            }

            Person obj = session.GetObjectByKey<Person>(Guid.Parse(e.Value.ToString()));

            if (obj != null)
            {
                comboItemUnit.DataSource = new Person[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void colPersonOnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {

            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<Person> collection = new XPCollection<Person>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = //new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator.And(
                    CriteriaOperator.Or(
                        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                        new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                        ),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));



            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();

        }
        protected void formBillActor_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {

        }
    }
}