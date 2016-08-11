using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Xml;
using DevExpress.Web.ASPxTreeList;

namespace WebModule.UserControls
{
    public partial class uPopupEditHelper : System.Web.UI.UserControl
    {
        public string HTMLVALUE;

        const string XmlSessionKey = "HelpTreeView";

        static XmlDocument Xml
        {
            get
            {
                if (HttpContext.Current.Session[XmlSessionKey] == null)
                    HttpContext.Current.Session[XmlSessionKey] = CreateXml("~/Help/HelpTreeView.xml");
                return (XmlDocument)HttpContext.Current.Session[XmlSessionKey];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Bind(treelistHelper, "/helper[@LinkId='root']");
            if (!IsPostBack) {
                refreshContent("~/Help/ItemUnitInstruction.xml");
            }
        }

        public void refreshContent(string linkId)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath(linkId));
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            doc.WriteTo(tx);
            HTMLVALUE = sw.ToString();
        }

        protected void popupItemUnitHelper_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            if (treelistHelper.FocusedNode.Level != 1)
            {
                string id = treelistHelper.FocusedNode.Key;
                refreshContent(id);
            }
        }

        public static void Bind(ASPxTreeList tree, string path)
        {
            tree.ClearNodes();
            XmlElement root = Xml.SelectSingleNode(path) as XmlElement;
            BindCore(tree, tree.RootNode, root);
        }

        static void BindCore(ASPxTreeList tree, TreeListNode node, XmlElement xmlNode)
        {
            node.SetValue("Text", xmlNode.Attributes["Text"].Value);
            foreach (XmlElement element in xmlNode.ChildNodes)
            {
                TreeListNode child = tree.AppendNode(element.GetAttribute("LinkId"), node);
                BindCore(tree, child, element);
            }
        }

        static XmlDocument CreateXml(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Request.MapPath(path));
            return doc;
        }
    }
}