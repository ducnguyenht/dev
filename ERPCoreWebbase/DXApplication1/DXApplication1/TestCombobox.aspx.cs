using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Accounting.AccountChart;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL;
using NAS.DAL.Nomenclature.Item;
using Utility;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using NAS.DAL.BI.Accounting.GoodsInTransit;
using NAS.DAL.BI.Accounting.GoodsInInventory;

namespace WebModule
{
    public partial class TestCombobox : System.Web.UI.Page
    {

        private Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsGeneralJournal.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GoodsInTransitForSaleDetail detail = new GoodsInTransitForSaleDetail(session);
            detail.Credit = 1;
            detail.Save();

            GoodsInInventoryDetail detail2 = new GoodsInInventoryDetail(session);
            detail2.Credit = 1;
            detail2.Save();
           // ASPxComboBox1.ItemsRequestedByFilterCondition += new ListEditItemsRequestedByFilterConditionEventHandler(accountCombobox_ItemsRequestedByFilterCondition);
           // ASPxComboBox1.ItemRequestedByValue += new ListEditItemRequestedByValueEventHandler(accountCombobox_ItemRequestedByValue);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void grdGeneralJournal_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {
            //if (e.Column.FieldName.Equals("AccountId!Key"))
            //{
            //    try
            //    {
            //        ASPxComboBox accountCombobox = e.Editor as ASPxComboBox;
            //        if (accountCombobox != null)
            //        {
            //            accountCombobox.ItemsRequestedByFilterCondition += new ListEditItemsRequestedByFilterConditionEventHandler(accountCombobox_ItemsRequestedByFilterCondition);
            //            accountCombobox.ItemRequestedByValue += new ListEditItemRequestedByValueEventHandler(accountCombobox_ItemRequestedByValue);
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
        }

        void accountCombobox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox accountCombobox = source as ASPxComboBox;
            Item obj = session.GetObjectByKey<Item>(e.Value);

            if (obj != null)
            {
                accountCombobox.DataSource = new Item[] { obj };
                accountCombobox.DataBindItems();
            }
        }

        void accountCombobox_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox accountCombobox = source as ASPxComboBox;
            XPCollection<Item> collection = new XPCollection<Item>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater), 
                CriteriaOperator.Or(
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                    new BinaryOperator("Description", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
            ));

            collection.Criteria = criteria;

            //collection.Criteria = new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like);
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            accountCombobox.DataSource = collection;
            accountCombobox.DataBindItems();
        }

    }
}