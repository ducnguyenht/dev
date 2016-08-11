using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.GUI.Pattern
{

    public delegate bool StateEventHandler(State sender, StateEventArgs args);

    public class StateEventArgs {
        public string Transition { get; set; }
    }

    public interface IStateAction
    {
        bool PreTransitionCRUD(string transition);
        bool CRUD();
        bool UpdateGUI();
    }
}
