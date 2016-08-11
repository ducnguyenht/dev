using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxEditors;

namespace WebModule.Accounting.AllocationConfigure.Controls
{
    public partial class AccountActorComboBox : System.Web.UI.UserControl
    {

        private AccountActorComboBoxStrategy AccountActorComboBoxStrategy
        {
            get;
            set;
        }

        public AccountActor GetSelectedItem()
        {
            return AccountActorComboBoxStrategy.GetSelectedItem(combo);
        }

        public void SetAccountActorComboBoxStrategy(AccountActorComboBoxStrategy accountActorComboBoxStrategy)
        {
            AccountActorComboBoxStrategy = accountActorComboBoxStrategy;
            AccountActorComboBoxStrategy.Init(combo);
        }

        public ASPxComboBox ComboBox
        {
            get { return combo; }
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        public override void Dispose()
        {
            base.Dispose();
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void combo_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            if (AccountActorComboBoxStrategy == null)
                return;
            AccountActorComboBoxStrategy.ItemRequestedByValue(session, source, e);
        }

        protected void combo_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (AccountActorComboBoxStrategy == null)
                return;
            AccountActorComboBoxStrategy.ItemsRequestedByFilterCondition(session, source, e);
        }

        protected void combo_Init(object sender, EventArgs e)
        {
            //if (AccountActorComboBoxStrategy == null)
            //    return;
            //AccountActorComboBoxStrategy.Init(sender);
        }
    }
}