using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;
using NAS.DAL.Inventory.Audit;
using NAS.DAL;
using System.Data;
using DevExpress.Utils;
using NAS.DAL.Inventory.Command;

namespace WebModule.Warehouse.Command.PopupCommand.AuditingInventoryCommand
{
    public partial class AuditingInventoryCommandList : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session;

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_WAREHOUSEINVENTORY_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            InventoryAuditArtifactXDS.Session = session;
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            uAuditingInventoryCommand1.SettingInit(grdInventoryCommand, "EndCallback");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdInventoryCommand_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdInventoryCommand.CancelEdit();
            grdInventoryCommand.JSProperties.Add("cpNew", "new");
        }

        protected void grdInventoryCommand_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdInventoryCommand.CancelEdit();
            grdInventoryCommand.JSProperties.Add("cpUpdate", e.EditingKeyValue.ToString());
        }

        protected void grdInventoryCommand_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
            InventoryAuditArtifact _inventoryAuditArtifact = session.GetObjectByKey<InventoryAuditArtifact>(e.Values["InventoryCommandId"]);
            if (_inventoryAuditArtifact != null)
            {
                _inventoryAuditArtifact.RowStatus = Constant.ROWSTATUS_DELETED;
                _inventoryAuditArtifact.Save();
            }

            grdInventoryCommand.JSProperties.Add("cpRefresh", "refresh");
        }

        protected void grdInventoryCommand_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ApprovalStatus")
            {
                if (char.Parse(e.Value.ToString()).Equals(Constant.APRROVE_YES))
                {
                    e.DisplayText = "Đã duyệt";
                }
                else
                {
                    e.DisplayText = "Chưa duyệt";
                }
            }
        }

        protected void grdInventoryCommand_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] para = e.Parameters.Split('|');
            InventoryAuditArtifact _inventoryAuditArtifact = session.GetObjectByKey<InventoryAuditArtifact>(Guid.Parse(para[0]));

            if (_inventoryAuditArtifact != null)
            {
                _inventoryAuditArtifact.ApprovalStatus = Constant.APRROVE_YES;
                _inventoryAuditArtifact.Save();

                grdInventoryCommand.JSProperties.Add("cpRefreshGrid", "refresh");
            }

        }

        protected void grdInventoryCommand_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.VisibleIndex == -1) return;

            if (e.CellType == DevExpress.Web.ASPxGridView.GridViewTableCommandCellType.Data)
            {
                InventoryAuditArtifact row = (InventoryAuditArtifact)grdInventoryCommand.GetRow(e.VisibleIndex);
                if (e.ButtonID == "buttonApprove" && row.ApprovalStatus.ToString().Contains(Constant.APRROVE_YES))
                {
                    e.Visible = DefaultBoolean.False;
                }
            }

        }

        protected void grdInventoryCommand_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "PersonName")
            {
                InventoryAuditArtifact _inventoryAuditArtifact = session.GetObjectByKey<InventoryAuditArtifact>(e.GetListSourceFieldValue("InventoryCommandId"));
                if (_inventoryAuditArtifact != null)
                {
                    foreach (InventoryCommandActor ica in _inventoryAuditArtifact.InventoryCommandActors)
                    {
                        if (ica.InventoryCommandActorTypeId.Name == "CHIEFCHECKING")
                        {
                            if (ica.PersonId != null)
                            {
                                e.Value = ica.PersonId.Name;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}