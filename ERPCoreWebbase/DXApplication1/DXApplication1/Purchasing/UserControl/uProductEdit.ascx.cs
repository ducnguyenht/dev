using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxGridView;
using Utility;
//using DAL.Purchasing;
//using BLL.PurchasingBLO;
//using BLL.BO.Purchasing;
using DevExpress.Web.ASPxEditors;
using System.Collections.Generic;
using DevExpress.Web.ASPxTreeList;
using NAS.DAL;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.Inventory.Jouranl;
using NAS.DAL.CMS.ObjectDocument;

namespace WebModule.Purchasing.UserControl
{
    public partial class uProductEdit : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //Bind data for current Item
            session = XpoHelper.GetNewSession();

            //Bind data for manufacturer list
            //ManufactuerCboXDS.Session = session;
            //cboProductManufacturer.DataBind();

            //Bind data for Object type
            ObjectTypeLbXDS.Session = session;

            CriteriaOperator selectionTypeCriteria = new BinaryOperator("Category",
                Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.ITEM));
            ObjectTypeLbXDS.Criteria = selectionTypeCriteria.ToString();
            lbType.DataBind();
            //Bind data for UnitCombobox in line of Tree
            UnitCboXDS.Session = session;

            //Bind data for supplier cbo
            //SupplierCboXDS.Session = session;

            //Bind data for SupplierList
            SupplierListXDS.Session = session;

            //Bind data for ItemUnit Tree
            ItemUnitTreeXDS.Session = session;

            //bind data for Tax
            TaxXDS.Session = session;

            //bind data for itemTax
            ItemTaxXDS.Session = session;

            //Bind data for UnitItem
            UnitTypeXDS.Session = session;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void cpItemEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            ACTION = para[0];
            string id = para.Count<string>() == 2 ? id = para[1] : id = "";
            if (id != "")
                ItemId = Guid.Parse(id);
            action();

            if (ACTION.Equals("Save"))
                cpItemEdit.JSProperties.Add("cpIsSaved", isValidForm);
        }

        #region Main Info

        protected void txtProductName_Validation(object sender, ValidationEventArgs e)
        {

        }

        protected void txtProductCode_Validation(object sender, ValidationEventArgs e)
        {
            string msg = "";
            if (MODE.Equals(string.Empty))
                return;
            bool rs = validateDupplicateCode(out msg);
            if (!rs)
            {
                txtProductCode.IsValid = false;
                txtProductCode.ErrorText = msg;
                e.IsValid = false;
                e.ErrorText = msg;
            }
        }

        #endregion

        #region BuyingProductCategory

        public void BuyingProductCategoryItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {

        }

        public void BuyingProductCategoryItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {

        }


        protected void grdBuyingProductCategory_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {


        }

        protected void grdBuyingProductCategory_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

        }

        protected void grdBuyingProductCategory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }



        protected void grdBuyingProductCategory_CellEditorInitialize1(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {

        }

        protected void grdBuyingProductCategory_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }
        #endregion

        #region ProductSupplier

        protected void grdProductSupplier_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            collectData();
            e.NewValues["ItemId"] = currentItem;
            NAS.DAL.Nomenclature.Organization.SupplierOrg supp = session.FindObject<NAS.DAL.Nomenclature.Organization.SupplierOrg>
                (new BinaryOperator("OrganizationId", e.NewValues["SupplierOrgId!Key"]));
            if (supp != null)
                e.NewValues["SupplierOrgId"] = supp;
            treelstProductUnits.CancelEdit();
        }

        protected void grdProductSupplier_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

        }

        protected void grdProductSupplier_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void grdProductSupplier_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }

        protected void pcProduct_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
        {

        }
        #endregion

        #region ProductUnit

        public void ProductUnitItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {

        }

        public void ProductUnitItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {

        }

        protected void grdProductUnit_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            collectData();
            e.NewValues["RowStatus"] = Constant.ROWSTATUS_ACTIVE;
            e.NewValues["ItemId"] = currentItem;
            NAS.DAL.Nomenclature.Item.Unit unit = session.FindObject<NAS.DAL.Nomenclature.Item.Unit>(new BinaryOperator("Code", e.NewValues["UnitId.Code"]));
            if (unit != null)
                e.NewValues["UnitId"] = unit;
            ItemUnitRelationType itemUnitRType = session.FindObject<ItemUnitRelationType>(new BinaryOperator("Name", "UNIT"));
            if (itemUnitRType != null)
                e.NewValues["ItemUnitRelationTypeId"] = itemUnitRType;
            e.NewValues["RowCreationTimeStamp"] = DateTime.Now;
            treelstProductUnits.CancelEdit();
        }

        protected void grdProductUnit_CellEditorInitialize(object sender, DevExpress.Web.ASPxTreeList.TreeListColumnEditorEventArgs e)
        {
            if (e.Column.Name.Equals("Code") && !treelstProductUnits.IsNewNodeEditing)
            {
                ASPxComboBox cboEdit = (ASPxComboBox)e.Editor;
                cboEdit.Enabled = false;
            }

            if (e.Column.Name.Equals("Code") && treelstProductUnits.IsNewNodeEditing)
            {
                e.Editor.Focus();
            }

            if (e.Column.Name.Equals("NumRequired") && !treelstProductUnits.IsNewNodeEditing)
            {
                TreeListNode node = treelstProductUnits.FindNodeByKeyValue(e.NodeKey);
                if (node.Level == 1)
                {
                    ASPxTextEdit txtEdit = (ASPxTextEdit)e.Editor;
                    txtEdit.Enabled = false;
                }
            }

            if (e.Column.Name.Equals("NumRequired") && treelstProductUnits.IsNewNodeEditing
                && treelstProductUnits.NewNodeParentKey == treelstProductUnits.RootNode.Key)
            {
                e.Editor.Value = 1;
                e.Editor.Enabled = false;
            }


            if (e.Column.Name.Equals("NumRequired") && !treelstProductUnits.IsNewNodeEditing)
            {
                e.Editor.Focus();
            }
        }

        protected void grdProductUnit_NodeValidating(object sender, TreeListNodeValidationEventArgs e)
        {
            if (!e.IsNewNode)
            {
                if (itemBO.checkIsExistInBillItem(session, ItemId))
                    throw new Exception("Đối tượng này đã tồn tại trong phiếu mua hoặc phiếu bán nên không thể sửa hoặc xóa");

                if (itemBO.checkIsItemInInventory(session, ItemId))
                    throw new Exception("Đối tượng đã tồn tại trong kho hàng nên không thể chỉnh sửa hoặc xóa đơn vị tính");
            }
        }

        protected void colNumRequired_Init(object sender, EventArgs e)
        {
        }

        protected void grdProductUnit_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
        {
            //Setting numberRequired
            ASPxTreeList tree = sender as ASPxTreeList;
            if (e.Column.FieldName == "NumRequired" && e.Level == 1)
            {
                e.Cell.Text = "";
            }

            //Setting Description for node
            if (e.Column.Name == "Description")
            {
                TreeListNode node = tree.FindNodeByKeyValue(e.NodeKey);

                NAS.DAL.Nomenclature.Item.ItemUnit currentItem = (NAS.DAL.Nomenclature.Item.ItemUnit)node.DataItem;
                if (e.Level > 1)
                {
                    NAS.DAL.Nomenclature.Item.ItemUnit parentItem = (NAS.DAL.Nomenclature.Item.ItemUnit)node.ParentNode.DataItem;
                    e.Cell.Text = String.Format("1 {0} bao gồm {1} {2}",
                                parentItem.UnitId.Name,
                                currentItem.NumRequired,
                                currentItem.UnitId.Name);
                }
                else
                    e.Cell.Text = String.Format("{0} là đơn vị tính cao nhất",
                                currentItem.UnitId.Name);
            }
        }

        protected void grdProductUnit_InitNewNode(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }

        protected void grdProductUnit_StartNodeEditing(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeEditingEventArgs e)
        {

        }

        protected void grdProductUnit_Init(object sender, EventArgs e)
        {
        }

        protected void grdProductUnit_NodeCollapsing(object sender, TreeListNodeCancelEventArgs e)
        {
        }
        #endregion

        #region

        protected void cpProductCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (MODE != "Edit" && MODE != "Add")
                return;
            string[] para = e.Parameter.Split(',');
            ACTION = para[0];
            string index = para.Count<string>() == 2 ? index = para[1] : index = "";
            //if (index != "")
            //    currentIdxLbType = int.Parse(index);
            action();

            //pcProduct.ActiveTabIndex = 0;
        }

        #endregion

        protected void lbType_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            loadLbType();
        }

        protected void grdProductSupplier_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            //if (e.VisibleIndex == -1 && ButtonEditItem.Visible &&
            //    (e.ButtonType == DevExpress.Web.ASPxGridView.ColumnCommandButtonType.New ||
            //    e.ButtonType == DevExpress.Web.ASPxGridView.ColumnCommandButtonType.Edit ||
            //    e.ButtonType == DevExpress.Web.ASPxGridView.ColumnCommandButtonType.Delete))
            //    e.Visible = false;

            //if (e.VisibleIndex == -1 && ButtonSaveItem.Visible &&
            //    e.ButtonType == DevExpress.Web.ASPxGridView.ColumnCommandButtonType.New)
            //    e.Visible = true;
        }

        protected void grdProductSupplier_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {

        }

        protected void treelstProductUnits_NodeInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            ItemUnitRelationType itemUnitRelationType = session.FindObject<ItemUnitRelationType>(new BinaryOperator("Name", "UNIT", BinaryOperatorType.Equal));
            if (itemUnitRelationType == null)
            {
                throw new Exception("The key is not exist in ItemUnitRelationType");
            }
            e.NewValues["ItemUnitRelationTypeId!Key"] = itemUnitRelationType.ItemUnitRelationTypeId;
        }

        protected void grdProductSupplier_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.Name.Equals("Supplier"))
            {
                e.Editor.Focus();
            }
        }

        protected void treelstProductUnits_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
        {
            //if (e.RowKind == TreeListRowKind.Data && e.Level == 1) {
            //    treelstProductUnits.FindNodeByKeyValue(e.NodeKey).Focus();
            //}
        }

        protected void cboSupplierColumn_OnItemsRequestedByFilterCondition_SQL(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            //if (comboBox.IsCallback) {
            //Get SUPPLIER trading type

            //2013-11-03 ERP-872 Khoa.Truong MOD START
            TradingCategory supplierTradingCategory =
                NAS.DAL.Util.getXPCollection<TradingCategory>(session, "Code", "SUPPLIER").FirstOrDefault();

            //XPCollection<NAS.DAL.Nomenclature.Organization.SupplierOrg> collection = new XPCollection<NAS.DAL.Nomenclature.Organization.SupplierOrg>(session);
            XPCollection<NAS.DAL.Nomenclature.Organization.Organization> collection = new XPCollection<NAS.DAL.Nomenclature.Organization.Organization>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;
            //collection.Criteria = CriteriaOperator.Or(
            //        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
            //        new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
            //);
            collection.Criteria = CriteriaOperator.And(
                CriteriaOperator.Or(
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                ),
                new ContainsOperator("OrganizationCategories",
                    CriteriaOperator.And(
                        new BinaryOperator("TradingCategoryId.TradingCategoryId", supplierTradingCategory.TradingCategoryId),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                    )
                ),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
            );
            //2013-11-03 ERP-872 Khoa.Truong MOD END

            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));
            comboBox.DataSource = collection;
            comboBox.DataBindItems();
            //}
        }

        protected void cboSupplierColumn_OnItemRequestedByValue_SQL(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            //2013-11-03 ERP-872 Khoa.Truong MOD START
            //NAS.DAL.Nomenclature.Organization.SupplierOrg obj = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.SupplierOrg>(e.Value);
            NAS.DAL.Nomenclature.Organization.Organization obj = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.Organization>(e.Value);

            if (obj != null)
            {
                comboBox.DataSource = new NAS.DAL.Nomenclature.Organization.Organization[] { obj };
                comboBox.DataBindItems();
            }
            //2013-11-03 ERP-872 Khoa.Truong MOD END
        }

        protected void treelstProductUnits_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
            if (itemBO.checkIsExistInBillItem(session, ItemId))
                throw new Exception("Đối tượng này đã tồn tại trong phiếu mua hoặc phiếu bán nên không thể sửa hoặc xóa");

            if (itemBO.checkIsItemInInventory(session, ItemId))
                throw new Exception("Đối tượng đã tồn tại trong kho hàng nên không thể chỉnh sửa hoặc xóa đơn vị tính");

            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    NAS.DAL.Nomenclature.Item.ItemUnit itemunit = uow.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(Guid.Parse(treelstProductUnits.FocusedNode.Key));
                    itemunit.Coefficient = 0;
                    itemunit.Save();
                    uow.FlushChanges();
                    itemBO.DrillUpUpdateAllCoefficientsOfItem(uow, ItemId, Guid.Parse(treelstProductUnits.FocusedNode.Key));
                    itemBO.DrillDownDeleteLogicUnitOfItem(uow, ItemId, Guid.Parse(treelstProductUnits.FocusedNode.Key));
                }
                catch
                {
                    throw;
                }
                finally
                {
                    uow.Dispose();
                }
            }

            treelstProductUnits.DataBind();
        }

        protected void ASPxGridTax_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            collectData();
            e.NewValues["ItemId"] = currentItem;
            e.NewValues["RowStatus"] = Utility.Constant.ROWSTATUS_ACTIVE;
            ASPxGridTax.CancelEdit();
        }

        //DND
        protected void ASPxGridTax_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.Name.Equals("txt_TaxId"))
            {
                e.Editor.Focus();
            }
        }
 
        protected void ASPxGridTax_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
            Guid ItemTaxId = (Guid)e.Keys[0];
            if (ItemTaxId != null)
            {
                NAS.DAL.Nomenclature.Item.ItemTax itemtax = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemTax>(ItemTaxId);
                itemtax.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                itemtax.Save();
                //ASPxGridTax.DataBind();
            }
        }

        protected void grdUnitType_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == null || e.Parameters.Count() == 0)
                return;

            string[] para = e.Parameters.Split(',');


            Guid key = Guid.Parse(para[1]);

            if (para[0].Equals("add"))
            {
                itemBO.selectUnitTypeForItem(key);
                grdUnitType.JSProperties.Add("cpChangedSelection", true);
            }
            else if (para[0].Equals("delete"))
            {
                itemBO.unselectUnitTypeForItem(key);
                grdUnitType.JSProperties.Add("cpChangedSelection", true);
            }
            else if (para[0].Equals("master"))
            {
                itemBO.updateMasterUnitTypeOfItem(ItemId, key);
                grdUnitType.JSProperties.Add("cpChangedSelection", true);
            }
        }

        protected void ChkIsSelected_Init(object sender, EventArgs e)
        {
            ASPxCheckBox checkbox = sender as ASPxCheckBox;
            DevExpress.Web.ASPxGridView.GridViewDataItemTemplateContainer container = checkbox.NamingContainer as
                DevExpress.Web.ASPxGridView.GridViewDataItemTemplateContainer;
            Guid key = Guid.Parse((container).KeyValue.ToString());

            string code = "function(s, e){ var params; " +
                          "if (s.GetChecked()) " +
                          string.Format("params = new Array('add', '{0}'); ", key.ToString()) +
                          "else " +
                          string.Format("params = new Array('delete', '{0}'); ", key.ToString()) +
                          "grdUnitType.PerformCallback(params); }";

            checkbox.ClientSideEvents.CheckedChanged = code;
        }

        protected void rdoIsMater_Init(object sender, EventArgs e)
        {
            ASPxRadioButton radioBtn = sender as ASPxRadioButton;
            DevExpress.Web.ASPxGridView.GridViewDataItemTemplateContainer container = radioBtn.NamingContainer as
                DevExpress.Web.ASPxGridView.GridViewDataItemTemplateContainer;
            Guid key = Guid.Parse((container).KeyValue.ToString());

            NAS.DAL.Nomenclature.Item.ItemUnitTypeConfig config = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnitTypeConfig>(key);

            string para = "master," + config.UnitTypeId.UnitTypeId.ToString();
            radioBtn.ClientInstanceName = String.Format("rdoIsMater_{0}", container.VisibleIndex);
            string code = "function(s, e){ ";
            for (int i = 0; i < grdUnitType.VisibleRowCount; i++)
            {
                if (container.VisibleIndex != i)
                    code += String.Format("rdoIsMater_{0}.SetChecked(false); ", i);
            }
            code += "if (s.GetChecked())" +
                string.Format("grdUnitType.PerformCallback('{0}');", para + ",1") +
                " else " +
                string.Format("grdUnitType.PerformCallback('{0}');", para + ",0") + "}";

            radioBtn.ClientSideEvents.CheckedChanged = code;
        }

        protected void grdUnitType_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                Guid key = Guid.Parse(e.KeyValue.ToString());
                NAS.DAL.Nomenclature.Item.ItemUnitTypeConfig config = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnitTypeConfig>(key);
                
                if (config == null)
                    throw new Exception("The ItemUnitTypeConfig is not exist in system");

                if (config.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                    grdUnitType.Selection.SetSelectionByKey(key, true);
                else
                    grdUnitType.Selection.SetSelectionByKey(key, false);
            }
        }

        protected void popZoneTreelstProductUnits_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            NAS.DAL.Nomenclature.Item.ItemUnitTypeConfig config = 
                session.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnitTypeConfig>(Guid.Parse(e.Parameter.ToString()));
            UnitTypeCode = config.UnitTypeId.Code;
            treelstProductUnits.DataBind();

            if (config.IsMaster)
            {
                treelstProductUnits.Columns["IsDefault"].Visible = true;
                treelstProductUnits.Columns["Coefficient"].Visible = true;
            }
            else
            {
                treelstProductUnits.Columns["IsDefault"].Visible = false;
                treelstProductUnits.Columns["Coefficient"].Visible = false;
            }

            //if (!config.UnitTypeId.Code.Equals("SPECIFICATION"))
            //    treelstProductUnits.Columns["Action"].Visible = false;
            //else
            //    treelstProductUnits.Columns["Action"].Visible = true;
        }

        protected void hyperlinkDetail_Init(object sender, EventArgs e)
        {
            ASPxHyperLink link = sender as ASPxHyperLink;
            DevExpress.Web.ASPxGridView.GridViewDataItemTemplateContainer container = link.NamingContainer as
                DevExpress.Web.ASPxGridView.GridViewDataItemTemplateContainer;
            Guid key = Guid.Parse((container).KeyValue.ToString());

            link.ClientSideEvents.Click = "function(s, e){ " +
                string.Format("popZoneTreelstProductUnits.PerformCallback('{0}'); ", key.ToString()) +
                "}";
        }

        protected void rdoIsDefault_Init(object sender, EventArgs e)
        {
            ASPxRadioButton radioBtn = sender as ASPxRadioButton;
            DevExpress.Web.ASPxTreeList.TreeListDataCellTemplateContainer container = radioBtn.NamingContainer as
                DevExpress.Web.ASPxTreeList.TreeListDataCellTemplateContainer;
            Guid key = Guid.Parse((container).NodeKey.ToString());
            radioBtn.Checked = false;
            string para = "default," + key.ToString();
            radioBtn.ClientInstanceName = String.Format("rdoIsDefault_{0}", container.NodeKey.ToString());
            string code = "function(s, e){ ";
            foreach (TreeListNode node in treelstProductUnits.Nodes)
            {
                TreeListNodeIterator iterator = new TreeListNodeIterator(node);
                while (iterator.Current != null)
                {
                    if (!container.NodeKey.Equals(iterator.Current.Key))
                        code += String.Format("rdoIsDefault_{0}.SetChecked(false); ", iterator.Current.Key);
                    iterator.GetNext();
                }
            }
            code += "if (s.GetChecked())" +
                string.Format("treelstProductUnits.PerformCallback('{0}');", para + ",1") +
                " else " +
                string.Format("treelstProductUnits.PerformCallback('{0}');", para + ",0") + "}";

            radioBtn.ClientSideEvents.CheckedChanged = code;
        }

        protected void treelstProductUnits_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
        {
            if (e.Argument == null || e.Argument.Equals(string.Empty))
                return;

            string[] para = e.Argument.Split(',');

            if (para[0].Equals("default"))
            {
                Guid key = Guid.Parse(para[1]);
                itemBO.UpdateDefaultItemUnitOfItem(ItemId, key);
            }            
            treelstProductUnits.DataBind();
        }

        protected void cboProductManufacturer_OnItemsRequestedByFilterCondition_SQL(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            XPCollection<NAS.DAL.Nomenclature.Organization.ManufacturerOrg> collection = new XPCollection<NAS.DAL.Nomenclature.Organization.ManufacturerOrg>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;
            collection.Criteria = CriteriaOperator.Or(
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
            );
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));
            comboBox.DataSource = collection;
            comboBox.DataBindItems();
        }

        protected void cboProductManufacturer_OnItemRequestedByValue_SQL(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            try
            {
                NAS.DAL.Nomenclature.Organization.ManufacturerOrg obj = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.ManufacturerOrg>(Guid.Parse(e.Value.ToString()));

                if (obj != null)
                {
                    comboBox.DataSource = new NAS.DAL.Nomenclature.Organization.ManufacturerOrg[] { obj };
                    comboBox.DataBindItems();
                }
            }
            catch { 
                
            }
        }
    }
}