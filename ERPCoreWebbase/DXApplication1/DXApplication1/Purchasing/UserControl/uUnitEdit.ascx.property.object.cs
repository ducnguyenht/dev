using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Web.ASPxEditors;
using NAS.BO.Nomenclature.Items;

namespace WebModule.Purchasing.UserControl
{
    public partial class uUnitEdit
    {
        public string MODE
        {
            set { Session["MODE"] = value; }
            get {
                if (Session["MODE"] == null)
                    return "";
                return Session["MODE"].ToString(); }
        }

        public string ACTION
        {
            set { Session["ACTION"] = value; }
            get { return Session["ACTION"].ToString(); }
        }

        public Guid UnitId
        {
            set { Session["UnitId"] = value; }
            get {
                if (Session["UnitId"] == null)
                    return Guid.NewGuid();
                return Guid.Parse(Session["UnitId"].ToString());
            }
        }

        public ASPxHtmlEditor HtmlEditDescription
        {
            get
            {
                ASPxHtmlEditor editDescription = nbUnitEdit.Groups[0].FindControl("htmlEditDescription")
                                                as ASPxHtmlEditor;
                return editDescription;
            }
        }

        public ASPxButton ButtonEditUnit
        {
            get
            {
                ASPxButton button = formUnitEdit.FindControl("btnUnitEdit") as ASPxButton;
                return button;
            }
        }

        public ASPxButton ButtonSaveUnit
        {
            get
            {
                ASPxButton button = formUnitEdit.FindControl("btnUnitSave") as ASPxButton;
                return button;
            }
        }

        private Session session;
        
        private Unit currentUnit;

        UnitBO unitBO = new UnitBO();
    }
}