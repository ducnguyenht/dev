using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Voucher.State
{
    public sealed class VoucherStateTransition
    {
        private string _value;

        public static readonly VoucherStateTransition SaveTransition =
            new VoucherStateTransition("SAVE");
        public static readonly VoucherStateTransition CancelTransition =
            new VoucherStateTransition("CANCEL");

        private VoucherStateTransition(string val)
        {
            _value = val;
        }

        public string TransitionName { get { return _value; } }
    }

    public abstract class VoucherState : NAS.GUI.Pattern.State
    {
        public VoucherState(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        { }

        protected abstract bool
            Save(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl);

        protected abstract bool
            Cancel(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl);

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            try
            {
                if (transition.ToUpper().Equals(VoucherStateTransition.CancelTransition.TransitionName))
                {
                    return Cancel(context, _UIControl);
                }
                else if (transition.ToUpper().Equals(VoucherStateTransition.SaveTransition.TransitionName))
                {
                    return Save(context, _UIControl);
                }
                else
                {
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override abstract bool PreTransitionCRUD(string transition);

        public override abstract bool CRUD();

        public override abstract bool UpdateGUI();
    }
}