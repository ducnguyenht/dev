using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace WebModule.Accounting.AllocationConfigure.Controls
{
    public interface AccountActorComboBoxStrategy
    {
        AccountActor GetSelectedItem(object source);
        void ItemRequestedByValue(Session session, 
                                  object source, 
                                  DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e);
        void ItemsRequestedByFilterCondition(Session session, 
                                             object source, 
                                             DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e);
        void Init(object source);
    }
}
