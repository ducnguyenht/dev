using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.ASPxTreeList.Internal;
using DevExpress.Web.ASPxEditors;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL;
using NAS.DAL.System.Resource;
using Utility;


namespace WebModule.Authorization.Application.Usercontrol
{
    public partial class uApplication : System.Web.UI.UserControl
    {
        Session session;
        static List<AppComponent> lst = new List<AppComponent>();        

        protected void Page_Load(object sender, EventArgs e)
        {
            grdSitemap.DataBind();
            
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            ResourceXDS.Session = session;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void grdSitemap_HtmlRowPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlRowEventArgs e)
        {
            ASPxTreeList treeList = (ASPxTreeList)sender;
            ASPxCheckBox selectionCheckbox = null;
            foreach (TableCell cell in e.Row.Cells)
            {
                TreeListSelectionCell selectionCell = cell as TreeListSelectionCell;
                if (selectionCell != null)
                {
                    selectionCheckbox = (ASPxCheckBox)selectionCell.Controls[0];
                    break;
                }
            }
            if (selectionCheckbox != null)
            {
                TreeListNode node = treeList.FindNodeByKeyValue(e.NodeKey);
                selectionCheckbox.Checked = true;
            }
        }

        protected void chkSelect_Init(object sender, EventArgs e)
        {
            ASPxCheckBox chk = sender as ASPxCheckBox;
            TreeListDataCellTemplateContainer container = chk.NamingContainer as TreeListDataCellTemplateContainer;

            chk.ClientSideEvents.CheckedChanged = String.Format("function (s, e) {{ cpLine.PerformCallback('checkchange|{0}|' + s.GetChecked()); }}", container.NodeKey);
            chk.Checked = false;

            if (hdId.Count <= 0)
            {
                return;
            }
        }

        protected void cbLine_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            String[] p = e.Parameter.Split('|');
            switch (p[0])
            {
                case "save":
                    using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                    {

                        CriteriaOperator filter = new OperandProperty("AppId") == new OperandValue(hdId.Get("id").ToString());
                        XPCollection<AppComponent> colDelete = new XPCollection<AppComponent>(uow, filter);

                        uow.Delete(colDelete);


                        foreach (AppComponent x in lst)
                        {
                            //AppComponent ran = new AppComponent(uow);

                            //ran.Permitted = x.Permitted;

                            App a = uow.GetObjectByKey<App>(Guid.Parse(hdId.Get("id").ToString()));
                            x.AppId = a;
                            x.Save();
                            
                                //AppComponent r = uow.GetObjectByKey<AppComponent>(x.AppComponentId);
                            //ran.AppComponentId = r;

                            //ran.RowStatus = Constant.ROWSTATUS_ACTIVE;
                            //ran.RowCreationTimeStamp = DateTime.Now;
                            //ran.Save();
                        }

                        uow.CommitChanges();
                    }
                    break;
                case "show":
                  
                    break;
           
                default:
                    break;

            }
        }

        protected void cb_Callback1(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            //String[] p = e.Parameter.Split('|');

            //UnitOfWork uow = XpoHelper.GetNewUnitOfWork();

            //CriteriaOperator filter = CriteriaOperator.Parse("ResourceId='" + p[0].ToString() + "' AND ApplicationId='" + hdId.Get("id").ToString() + "'");
            //ViewResourceApplication obj = uow.FindObject<ViewResourceApplication>(filter);

            //ViewResourceApplication obj = session.GetObjectByKey<ViewResourceApplication>(Guid.Parse(p[0]));
            //obj.Permitted = Convert.ToBoolean(p[1]);



            //if (obj.Permitted)
            //{
            //    lst.Add(obj);
            //}
            //else
            //{
            //    var item = lst.First(x => x.ResourceId == obj.ResourceId);
            //    lst.Remove(item);

            //    TreeListNode node = grdSitemap.FindNodeByFieldValue("ResourceId", obj.ResourceId);

            //    if (node.HasChildren)
            //    {
            //        for (int i = 0; i < node.ChildNodes.Count; i++)
            //        {
            //            ASPxCheckBox chk1 = grdSitemap.FindDataCellTemplateControl(node.ChildNodes[i].Key, null, "chkSelect") as ASPxCheckBox;

            //            if (chk1.Checked)
            //            {
            //                var itemd = lst.First(x => x.ResourceId == Guid.Parse(node.ChildNodes[i].GetValue("ResourceId").ToString()));
            //                lst.Remove(itemd);

            //                chk1.Checked = false;   
            //            }

            //        }
            //    }

            //}
        }

        private void ProcessNodes(TreeListNode node)
        {
          
            ASPxCheckBox chk1 = grdSitemap.FindDataCellTemplateControl(node.Key, null, "chkSelect") as ASPxCheckBox;

            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            //CriteriaOperator filter = new OperandProperty("ResourceId") == new OperandValue(Guid.Parse(node.GetValue("ResourceId").ToString()));                        

            CriteriaOperator filter = CriteriaOperator.Parse("AppComponentId='" + node.GetValue("AppComponentId").ToString() + "' And AppId='" + hdId.Get("id").ToString() + "'");
            XPCollection<AppComponent> colDelete = new XPCollection<AppComponent>(uow, filter);

            if (colDelete.Count > 0)
            {
                filter = CriteriaOperator.Parse("AppComponentId='" + node.GetValue("AppComponentId").ToString() + "' And AppId='" + hdId.Get("id").ToString() + "'");
                //AppComponent obj = uow.FindObject<AppComponent>(filter);

                AppComponent obj = uow.GetObjectByKey<AppComponent>(Guid.Parse(node.GetValue("AppComponentId").ToString()));
                lst.Add(obj);

                chk1.Checked = true;                    
            }

            if (node.HasChildren)
            {
                if (node.Expanded)
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                        ProcessNodes(node.ChildNodes[i]);
            }
            
        }

        private void ResetNodes(TreeListNode node)
        {

            ASPxCheckBox chk1 = grdSitemap.FindDataCellTemplateControl(node.Key, null, "chkSelect") as ASPxCheckBox;
            chk1.Checked = false;

            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            //CriteriaOperator filter = new OperandProperty("ResourceId") == new OperandValue(Guid.Parse(node.GetValue("ResourceId").ToString()));                        

            CriteriaOperator filter = CriteriaOperator.Parse("AppComponentId='" + node.GetValue("AppComponentId").ToString() + "' And AppId='" + hdId.Get("id").ToString() + "'");

            XPCollection<AppComponent> colDelete = new XPCollection<AppComponent>(uow, filter);

            if (colDelete.Count > 0)
            {
                AppComponent obj = uow.GetObjectByKey<AppComponent>(Guid.Parse(node.GetValue("AppComponentId").ToString()));
                chk1.Checked = false;
            }

            if (node.HasChildren)
            {
                if (node.Expanded)
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                        ResetNodes(node.ChildNodes[i]);
            }

        }


        protected void cpLine_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            String[] p = e.Parameter.Split('|');

            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();

            switch (p[0])
            {
                case "checkchange":

                    //AppComponent obj = session.GetObjectByKey<AppComponent>(Guid.Parse(p[1]));


                    CriteriaOperator filter = CriteriaOperator.Parse("AppComponentId='" + Guid.Parse(p[1].ToString()) + "' And AppId='" + Guid.Parse(hdId.Get("id").ToString()) + "'");
                    AppComponent obj = uow.FindObject<AppComponent>(filter);

                    //obj.Permitted = Convert.ToBoolean(p[2]);

                    if (Convert.ToBoolean(p[2]))
                    {
                        lst.Add(obj);

                        TreeListNode node = grdSitemap.FindNodeByFieldValue("AppComponentId", obj.AppComponentId);

                        if (node.HasChildren)
                        {
                            for (int i = 0; i < node.ChildNodes.Count; i++)
                            {
                                ASPxCheckBox chk1 = grdSitemap.FindDataCellTemplateControl(node.ChildNodes[i].Key, null, "chkSelect") as ASPxCheckBox;

                                if (!chk1.Checked)
                                {
                                    var itemd = lst.FirstOrDefault(x => x.AppComponentId == Guid.Parse(node.ChildNodes[i].GetValue("ResourceId").ToString()));
                                    if (itemd == null)
                                    {
                                        filter = CriteriaOperator.Parse("AppComponentId='" + p[0].ToString() + "' And AppId='" + hdId.Get("id").ToString() + "'");
                                        obj = uow.FindObject<AppComponent>(filter);

                                        //obj = session.GetObjectByKey<ViewResourceApplication>(Guid.Parse(node.ChildNodes[i].GetValue("ResourceId").ToString()));
                                        
                                        lst.Add(obj);
                                    }

                                    chk1.Checked = true;
                                }


                            }
                        }
                    }
                    else
                    {
                        var item = lst.First(x => x.AppComponentId == obj.AppComponentId);
                        lst.Remove(item);

                        TreeListNode node = grdSitemap.FindNodeByFieldValue("AppComponentId", obj.AppComponentId);

                        if (node.HasChildren)
                        {
                            for (int i = 0; i < node.ChildNodes.Count; i++)
                            {
                                ASPxCheckBox chk1 = grdSitemap.FindDataCellTemplateControl(node.ChildNodes[i].Key, null, "chkSelect") as ASPxCheckBox;

                                if (chk1.Checked)
                                {
                                    var itemd = lst.First(x => x.AppComponentId == Guid.Parse(node.ChildNodes[i].GetValue("AppComponentId").ToString()));
                                    lst.Remove(itemd);

                                    chk1.Checked = false;
                                }


                            }
                        }
                    }


                    break;

                case "show":
                    lst.Clear();
                    
                    session = XpoHelper.GetNewSession();
                    ResourceXDS.Session = session;

                    //filter = new OperandProperty("AppId") == new OperandValue(Guid.Parse(hdId.Get("id").ToString()));
                    //ResourceXDS.Criteria = filter.ToString();

                    grdSitemap.DataBind();

                    for (int i = 0; i < grdSitemap.Nodes.Count; i++)
                    {
                        ResetNodes(grdSitemap.Nodes[i]);
                    }

                    for (int i = 0; i < grdSitemap.Nodes.Count; i++)
                    {
                        ProcessNodes(grdSitemap.Nodes[i]);
                    }


                    break;
                default:
                    break;

            }
        }

    }
}