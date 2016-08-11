using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NAS.GUI.Pattern
{
    [Serializable]
    public abstract class State : IStateAction
    {
        public State(System.Web.UI.Control _UIControl, bool dif = true)
        {
            UIControl = _UIControl;
        }

        public State(System.Web.UI.Control _UIControl)
        {
            UIControl = _UIControl;
            if (CRUD())
            {
                if (!UpdateGUI())
                {
                    throw new Exception("UpdateGUI failed");
                }
            }
            else
            {
                throw new Exception("CRUD failed");
            }
        }

        protected System.Web.UI.Control UIControl { get; set; }

        public virtual bool Handle(Context context, string transition, System.Web.UI.Control _UIControl)
        {
            try
            {
                UIControl = _UIControl;
                if (this.PreTransitionCRUD(transition))
                {
                    if (!Transit(context, transition, _UIControl))
                    {
                        if (CRUD())
                        {
                            UpdateGUI();
                        }
                        return false;
                    }
                }
                else
                {
                    if (CRUD())
                    {
                        UpdateGUI();
                    }
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public abstract bool Transit(Context context, string transition, System.Web.UI.Control _UIControl);

        #region Implements IStateAction interface

        public abstract bool PreTransitionCRUD(string transition);
        public abstract bool CRUD();
        public abstract bool UpdateGUI();

        #endregion
    }
}