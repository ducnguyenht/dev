using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Invoice.Control.BillItemEditForm.Strategy
{
    public abstract class TradingItemsComboBoxDataLoadingStrategy
    {
        public abstract void ComboBoxItem_ItemsRequestedByFilterCondition(
            DevExpress.Xpo.Session session,
            object source,
            DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e);

        public abstract void ComboBoxItem_ItemRequestedByValue(
            DevExpress.Xpo.Session session,
            object source,
            DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e);
    }
}