using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Xpo;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Nomenclature.Item;
using NAS.BO.Nomenclature.Items;

namespace WebModule.Purchasing.UserControl
{
    public partial class uProductEdit
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

        public List<NAS.DAL.CMS.ObjectDocument.Object> CustomFieldObjects
        {
            set { Session[this.ClientID + "CustomFieldObjects"] = value; }
            get { return Session[this.ClientID + "CustomFieldObjects"] as List<NAS.DAL.CMS.ObjectDocument.Object>; }
        }

        public Guid ItemId
        {
            set { Session["ItemId"] = value; }
            get
            {
                if (Session["ItemId"] == null)
                    return Guid.NewGuid();
                return Guid.Parse(Session["ItemId"].ToString());
            }
        }

        public string UnitTypeCode
        {
            set { Session["UnitTypeCode"] = value; }
            get
            {
                return Session["UnitTypeCode"].ToString();
            }
        }

        public ASPxHtmlEditor HtmlEditDescription
        {
            get
            {
                ASPxHtmlEditor editDescription = nbProduct.Groups[0].FindControl("htmlEditDescription")
                                                as ASPxHtmlEditor;
                return editDescription;
            }
        }

        public List<Guid> selectedObjectTypeId;

        public ASPxButton ButtonSaveItem
        {
            get
            {
                ASPxButton button = formProductEdit.FindControl("btnSaveItem") as ASPxButton;
                return button;
            }
        }

        private Session session;

        private Item currentItem{
            get{
                return Session["currentItem"] as Item;
            }

            set{ Session["currentItem"] = value; }
        }

        private ItemBO itemBO = new ItemBO();

        private bool isValidForm = true;
    }
}