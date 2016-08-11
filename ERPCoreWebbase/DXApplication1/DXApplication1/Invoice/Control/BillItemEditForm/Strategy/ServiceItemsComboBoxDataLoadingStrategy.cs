using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using NAS.DAL.CMS.ObjectDocument;

namespace WebModule.Invoice.Control.BillItemEditForm.Strategy
{
    public class ServiceItemsComboBoxDataLoadingStrategy : TradingItemsComboBoxDataLoadingStrategy
    {
        public override void ComboBoxItem_ItemsRequestedByFilterCondition(DevExpress.Xpo.Session session, object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<Item> collection = new XPCollection<Item>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                //row status is active
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                CriteriaOperator.Or(
                //find code contains the filter
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                //find name contains the filter
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                ),
                new ContainsOperator("ItemCustomTypes",
                    new BinaryOperator("ObjectTypeId", ObjectType.GetDefault(session, ObjectTypeEnum.SERVICE)))
            );

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            combo.DataSource = collection;
            combo.DataBindItems();
        }

        public override void ComboBoxItem_ItemRequestedByValue(DevExpress.Xpo.Session session, object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            Item obj = session.GetObjectByKey<Item>(e.Value);
            if (obj != null)
            {
                combo.DataSource = new Item[] { obj };
                combo.DataBindItems();
            }
        }
    }
}