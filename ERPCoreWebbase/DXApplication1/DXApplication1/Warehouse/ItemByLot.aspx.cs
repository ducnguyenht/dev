using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL;
using DevExpress.Xpo;
using Utility;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Data.Filtering;
using NAS.DAL.Inventory.Lot;

namespace WebModule.Warehouse
{
    public partial class ItemByLot : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session;

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_INPUTCOMM_ID;
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
            ItemXDS.Session = session;
            DBLot.Session = session;
            grdItem.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //NAS.DAL.Nomenclature.Item.Item item = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.Item>("");         
        }


        protected void cboItem_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<ItemUnit> collection = new XPCollection<ItemUnit>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                new ContainsOperator("ItemId.ItemCustomTypes", new BinaryOperator("ObjectTypeId.ObjectTypeId", Guid.Parse("5817b239-e150-4c8e-a313-eaa8bd6944c4"))),
                new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                CriteriaOperator.Or(
                    new BinaryOperator("ItemId.Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                    new BinaryOperator("ItemId.Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                    new BinaryOperator("UnitId.Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
            ));

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("ItemId.Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();

        }


        protected void cboItem_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            ItemUnit obj = session.GetObjectByKey<ItemUnit>(e.Value);

            if (obj != null)
            {
                comboItemUnit.DataSource = new ItemUnit[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void grdItem_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Code")
            {
                e.Editor.Focus();
            }

            if (e.Column.FieldName == "ItemUnitId!Key")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                           "grdItem.GetEditor('ItemUnitId.ItemId.Name').SetValue(s.GetSelectedItem().GetColumnText('ItemId.Name'));" +
                                                           "grdItem.GetEditor('ItemUnitId.UnitId.Name').SetValue(s.GetSelectedItem().GetColumnText('UnitId.Name'));" +
                                                           "grdItem.GetEditor('ItemUnitId.ItemId.ManufacturerOrgId.Name').SetValue(s.GetSelectedItem().GetColumnText('ItemId.ManufacturerOrgId.Name'));" +
                                                       "}";
            }
        }

        protected void grdItem_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["Code"] == null)
            {
                e.RowError = "Chưa nhập số lô !";
                return;
            }
            else
            {
                if (e.OldValues["Code"] != null)
                {
                    if (!e.NewValues["Code"].ToString().Equals(e.OldValues["Code"].ToString()))
                    {
                        Lot lot = session.FindObject<Lot>(CriteriaOperator.Parse("RowStatus<>-1 And Code=?", e.NewValues["Code"].ToString()));
                        if (lot != null)
                        {
                            e.RowError = "Số lô này đã tồn tại";
                            return;
                        }
                    }
                }
            }

        }

        protected void grdItem_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Values["RowStatus"] = Utility.Constant.ROWSTATUS_DELETED;
            grdItem.DataBind();
        }

        protected void grdItem_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            grdItem.DataBind();
        }

        protected void grdItem_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            grdItem.DataBind();
            
        }

        protected void grdItem_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["RowStatus"] = Utility.Constant.ROWSTATUS_ACTIVE;
        }


    }
}