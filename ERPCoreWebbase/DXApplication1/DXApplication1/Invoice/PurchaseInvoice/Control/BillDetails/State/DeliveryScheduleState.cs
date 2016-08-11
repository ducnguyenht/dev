using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Invoice.PurchaseInvoice.Control.BillDetails.State
{
    public class DeliveryScheduleState : NAS.GUI.Pattern.State
    {
        public DeliveryScheduleState(System.Web.UI.Control control) : base(control) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "BillItems":
                    context.State = new BillItemsState(_UIControl);
                    break;
                case "DeliverySchedule":
                    context.State = new DeliveryScheduleState(_UIControl);
                    break;
                case "PaymentPlanning":
                    context.State = new PaymentPlanningState(_UIControl);
                    break;
                default:
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
            }
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return GetOwnerUIControl().DeliveryScheduleState_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return GetOwnerUIControl().DeliveryScheduleState_CRUD();
        }

        public override bool UpdateGUI()
        {
            return GetOwnerUIControl().DeliveryScheduleState_UpdateGUI();
        }

        public BillDetails GetOwnerUIControl()
        {
            BillDetails ret = null;
            if (UIControl != null)
            {
                if (UIControl is BillDetails)
                {
                    ret = (BillDetails)UIControl;
                }
            }
            return ret;
        }
    }
}