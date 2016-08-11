using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.GUI.Pattern;

namespace WebModule.NAANAdmin.Authorization.Sate
{
    public class PersonListing : State
    {
        public PersonListing(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(Context context, string transition, System.Web.UI.Control _UIControl)
        {
            throw new NotImplementedException();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            throw new NotImplementedException();
        }

        public override bool CRUD()
        {
            throw new NotImplementedException();
        }

        public override bool UpdateGUI()
        {
            throw new NotImplementedException();
        }
    }
}