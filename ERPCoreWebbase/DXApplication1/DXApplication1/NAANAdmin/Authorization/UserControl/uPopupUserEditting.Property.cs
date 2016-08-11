using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;

namespace WebModule.NAANAdmin.Authorization.UserControl
{
    public partial class uPopupUserEditting
    {
        public string MODE
        {
            set { Session["MODE"] = value; }
            get
            {
                if (Session["MODE"] == null)
                    return "";
                return Session["MODE"].ToString();
            }
        }

        public string ACTION
        {
            set { Session["ACTION"] = value; }
            get { return Session["ACTION"].ToString(); }
        }

        public Guid PersonId
        {
            set { Session["uPopupUserEditting_PersonId"] = value; }
            get
            {
                if (Session["uPopupUserEditting_PersonId"] == null)
                {
                    Session["uPopupUserEditting_PersonId"] = Guid.Empty;
                    return Guid.Empty;
                }
                return Guid.Parse(Session["uPopupUserEditting_PersonId"].ToString());
            }
        }

        public ASPxButton ButtonEditItem
        {
            get
            {
                ASPxButton button = popup_PersonEdit.FindControl("btnPersonEditEdit") as ASPxButton;
                return button;
            }
        }

        public ASPxButton ButtonSaveItem
        {
            get
            {
                ASPxButton button = popup_PersonEdit.FindControl("btnPersonEditSave") as ASPxButton;
                return button;
            }
        }

        public string defaultDepartment {
            set { Session["uPopupUserEditting_defaultDepartment"] = value; }
            get { return Session["uPopupUserEditting_defaultDepartment"].ToString(); }
        }

        private Session session;

        private Person currentPerson;
    }
}