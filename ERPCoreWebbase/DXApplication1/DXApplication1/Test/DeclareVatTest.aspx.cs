using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL.Invoice;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Data.Filtering;

namespace WebModule.Test
{
    public partial class DeclareVatTest : System.Web.UI.Page
    {
        Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void cboBill_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {

            XPCollection<Bill> collection = new XPCollection<Bill>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(                    
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),                                            
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            cboBill.DataSource = collection;
            cboBill.DataBindItems();
        }

        protected void cboBill_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                Bill bill = session.GetObjectByKey<Bill>(Guid.Parse(e.Value.ToString()));
                if (bill != null)
                {
                    cboBill.DataSource = new Bill[]{bill};
                    cboBill.DataBindItems();
                }
            }
            catch
            {
            }
        }
    }
}