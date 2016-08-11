using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebModule.Interfaces
{
    public delegate void FormEditEventHandler(object sender, FormEditEventArgs e);

    public interface IFormEditBase
    {
        event FormEditEventHandler Saved;
        void ClearForm();
        
        void OnSaved(FormEditEventArgs e);
    }

    public class FormEditEventArgs : EventArgs
    {
        public bool isSuccess { get; set; }
    }
}
