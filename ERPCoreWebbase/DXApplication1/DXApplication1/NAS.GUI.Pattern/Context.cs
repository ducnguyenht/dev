using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NAS.GUI.Pattern
{
    [Serializable]
    public class Context
    {
        public State State { get; set; }

        public Context()
        {
        }

        public Context(State state)
        {
            this.State = state;
        }

        public bool Request(string transition, System.Web.UI.Control _UIControl)
        {
            return State.Handle(this, transition, _UIControl);
        }

        public bool Request(string method)
        {
            bool result = false;

            switch (method)
            {
                case "CRUD":
                    result = State.PreTransitionCRUD("");
                    break;
                case "PreTransitionCRUD":
                    result = State.CRUD();
                    break;
                case "UpdateGUI":
                    result = State.UpdateGUI();
                    break;
            }

            return result;
        }
    }
}