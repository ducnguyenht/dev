using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Web.ASPxEditors;
//using BLL.PurchasingBLO;
//using DAL.Purchasing;
//using BLL.BO.Purchasing;
using DevExpress.Web.ASPxHtmlEditor;
using System.Data;
using DevExpress.Web.ASPxGridView;
//using DAL.PurchasingCode;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxTreeList;

namespace DXApplication1.Purchasing.UserControl
{
    public partial class uBuyingMaterial : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelpers.GetNewSession();
            //if (!IsPostBack)
            //{
            //    materialUnitBLO.insertRootMaterialUnit();
            //}
            //cboManufacturerXDS.Session = session;
            //grdMaterialOnCategory.DataSource = CategoryEntityList;
            //grdMaterialOnCategory.DataBind();
            //grdSupplierOnMaterial.DataSource = SupplierEntityList;
            //grdSupplierOnMaterial.DataBind();
            //treelstMaterialUnits.DataSource = MaterialUnitConstruction;
            //treelstMaterialUnits.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void grdMaterialOnCategory_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grdMaterialOnCategory = sender as ASPxGridView;
            ASPxComboBox cboCode = grdMaterialOnCategory.FindEditRowCellTemplateControl(grdMaterialOnCategory.Columns["Code"] as GridViewDataColumn
                        , "cboCodeCategory") as ASPxComboBox;
            //List<MaterialBuyingMaterialCategoryEntity> table = CategoryEntityList;

            string selectedCode = cboCode.SelectedItem.Value.ToString();

            //MaterialBuyingMaterialCategoryEntity category = new MaterialBuyingMaterialCategoryEntity();
            //if (Mode != "add")
            //{
            //    category = materialBLO.getCategoriesOnMaterial(selectedCode, KeyValue);
            //}
            //else
            //{
            //    category = materialBLO.getCategoryByCode(selectedCode);
            //}

            //table.Add(new MaterialBuyingMaterialCategoryEntity(
            //    category.MaterialBuyingMaterialCategoryId,
            //    category.MaterialId,
            //    category.BuyingMaterialCategoryId,
            //    selectedCode,
            //    category.Name
            //));

            //materialBLO.addMaterialToCategory(category, KeyValue);

            //CategoryEntityList = table;
            //grdMaterialOnCategory.DataSource = table;
            grdMaterialOnCategory.DataBind();

            e.Cancel = true;
            grdMaterialOnCategory.CancelEdit();
        }

        protected void grdMaterialOnCategory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grdMaterialOnCategory = sender as ASPxGridView;
            //List<MaterialBuyingMaterialCategoryEntity> table = CategoryEntityList;
            //if (table == null)
            //    table = new List<MaterialBuyingMaterialCategoryEntity>();
            //MaterialBuyingMaterialCategoryEntity rs = table.Find(items => items.Code == e.Keys["Code"].ToString());
            //if (rs != null)
            //{
            //    table.Remove(rs);
            //    materialBLO.deleteMaterialFromCategory(rs);
            //    CategoryEntityList = table;
            //}
            e.Cancel = true;
        }

        protected void grdSupplierOnMaterial_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grdSupplierOnMaterial = sender as ASPxGridView;
            ASPxComboBox cboCode = grdSupplierOnMaterial.FindEditRowCellTemplateControl(grdSupplierOnMaterial.Columns["Code"] as GridViewDataColumn
                        , "cboCodeSupplier") as ASPxComboBox;

            //List<MaterialSupplierEntity> table = SupplierEntityList;
            //if (table == null)
            //    table = new List<MaterialSupplierEntity>();

            //string selectedCode = cboCode.SelectedItem.Value.ToString();
            //MaterialSupplierEntity supplier = new MaterialSupplierEntity();
            //if (Mode != "add")
            //{
            //    supplier = materialBLO.getSupplierOnMaterial(selectedCode, KeyValue);
            //}
            //else
            //{
            //    supplier = materialBLO.getSupplierOnMaterial(selectedCode);
            //}

            //table.Add(new MaterialSupplierEntity(
            //    supplier.MaterialSupplierId,
            //    supplier.MaterialId,
            //    supplier.SupplierId,
            //    selectedCode,
            //    supplier.Name
            //));

            //materialBLO.addMaterialToSupplier(supplier, KeyValue);

            //SupplierEntityList = table;

            //grdSupplierOnMaterial.DataSource = table;
            grdSupplierOnMaterial.DataBind();

            e.Cancel = true;
            grdSupplierOnMaterial.CancelEdit();
        }

        protected void grdSupplierOnMaterial_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grdSupplierOnMaterial = sender as ASPxGridView;

            //List<MaterialSupplierEntity> table = SupplierEntityList;
            //MaterialSupplierEntity rs = table.Find(items => items.Code == e.Keys["Code"].ToString());
            //if (rs != null)
            //{
            //    table.Remove(rs);
            //    SupplierEntityList = table;
            //}
            e.Cancel = true;
        }

        protected void cpLineMaterial_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string action = para[0];
            string id = para.Count<string>() == 2 ? id = para[1] : id = "";
            if (id != "")
                KeyValue = new Guid(id);
            //Action(action, id);
            pcMaterial.ActiveTabIndex = 0;
        }

        protected void cboCodeCategory_Init(object sender, EventArgs e)
        {
            ASPxComboBox cboCodeCategory = sender as ASPxComboBox;
            //cboCodeCategory.DataSource = categoryBLO.getMaterialCategoryList();
            cboCodeCategory.DataBind();
        }

        protected void cboCodeUnit_Init(object sender, EventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)sender;
            //comboBox.DataSource = materialUnitBLO.getMaterialUnitList();
            //comboBox.DataBind();

            //TreeListEditCellTemplateContainer templateContainer = (TreeListEditCellTemplateContainer)comboBox.NamingContainer;
            //if (templateContainer.NodeKey != null)
            //{
            //    TreeListNode node = templateContainer.TreeList.FindNodeByKeyValue(templateContainer.NodeKey);
            //    ViewMaterialUnitConstruction dataItem = (ViewMaterialUnitConstruction)node.DataItem;
            //    comboBox.Value = dataItem.Code;
            //}
        }

        protected void cboCodeSupplier_Init(object sender, EventArgs e)
        {
            //SupplierBLO supplierBLO = new SupplierBLO();
            ASPxComboBox cboCodeSupplier = sender as ASPxComboBox;
            //cboCodeSupplier.DataSource = supplierBLO.getSupplierList();
            cboCodeSupplier.DataBind();
        }

        protected void grdMaterialOnCategory_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxComboBox cboCode = grdMaterialOnCategory.FindEditRowCellTemplateControl(grdMaterialOnCategory.Columns["Code"] as GridViewDataColumn
                        , "cboCodeCategory") as ASPxComboBox;
            string selectedCode = cboCode.SelectedItem.Value.ToString();

            //List<MaterialBuyingMaterialCategoryEntity> table = CategoryEntityList;
            //if (table == null)
            //    table = new List<MaterialBuyingMaterialCategoryEntity>();
            //GridViewColumn colCode = grid.Columns["Code"];
            //if (table.Count > 0 && grid.IsNewRowEditing)
            //{
            //    if (table.Find(items => items.Code == selectedCode) != null)
            //    {
            //        //e.Errors[colCode] = String.Format("Mã nhóm {0} đã được chọn", selectedCode);
            //        e.RowError = String.Format("Mã nhóm {0} đã được chọn! Vui lòng chọn nhóm khác", selectedCode);
            //    }
            //}

        }

        protected void grdSupplierOnMaterial_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxComboBox cboCode = grdSupplierOnMaterial.FindEditRowCellTemplateControl(grdSupplierOnMaterial.Columns["Code"] as GridViewDataColumn
                        , "cboCodeSupplier") as ASPxComboBox;
            string selectedCode = cboCode.SelectedItem.Value.ToString();

            //List<MaterialSupplierEntity> table = SupplierEntityList;
            //if (table == null)
            //    table = new List<MaterialSupplierEntity>();
            //GridViewColumn colCode = grid.Columns["Code"];
            //if (table.Count > 0 && grid.IsNewRowEditing)
            //{
            //    if (table.Find(items => items.Code == selectedCode) != null)
            //    {
            //        //e.Errors[colCode] = String.Format("Mã nhóm {0} đã được chọn", selectedCode);
            //        e.RowError = String.Format("Mã NCC {0} đã được chọn! Vui lòng chọn NCC khác", selectedCode);
            //    }
            //}
        }

        protected void treelstMaterialUnits_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //XPCollection<ViewMaterialUnitConstruction> table = MaterialUnitConstruction;
            //ASPxTreeList tree = sender as ASPxTreeList;
            //TreeListNode node = tree.FocusedNode;

            //List<ViewMaterialUnitConstruction> tmpList = new List<ViewMaterialUnitConstruction>();

            //if (node != null)
            //{
            //    ViewMaterialUnitConstruction row;
            //    TreeListNodeIterator iterator = new TreeListNodeIterator(node);
            //    while (iterator.Current != null)
            //    {
            //        row = (ViewMaterialUnitConstruction)iterator.Current.DataItem;
            //        tmpList.Add(row);
            //        iterator.GetNext();
            //    }

            //    row = (ViewMaterialUnitConstruction)node.DataItem;

            //    materialBLO.deleteMaterialUnitOnTree(tmpList);

            //}
            //MaterialUnitConstruction = GetMaterialUnitConstruction();
            //treelstMaterialUnits.DataSource = MaterialUnitConstruction;
            treelstMaterialUnits.DataBind();
            e.Cancel = true;
            treelstMaterialUnits.CancelEdit();
        }

        protected void treelstMaterialUnits_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //ViewMaterialUnitConstruction newNode = settingNewNodeUnit(e.NewValues[treelstMaterialUnits.ParentFieldName], 
            //        long.Parse(e.NewValues["NumRequired"].ToString()
            //        ));

            //XPCollection<ViewMaterialUnitConstruction> table = MaterialUnitConstruction;
            //materialBLO.insertMaterialUnitOnTree(newNode, KeyValue);
            //table.Add(newNode);
            //treelstMaterialUnits.DataSource = table;
            //treelstMaterialUnits.DataBind();
            //MaterialUnitConstruction = table;
            e.Cancel = true;
            treelstMaterialUnits.CancelEdit();
        }

        protected void treelstMaterialUnits_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //Init data
            //collectData();
            string keystr = e.Keys[treelstMaterialUnits.KeyFieldName].ToString();

            ASPxComboBox cboCodeUnit = treelstMaterialUnits.FindEditCellTemplateControl(treelstMaterialUnits.Columns["Code"]
                                            as TreeListDataColumn, "cboCodeUnit") as ASPxComboBox;
            TreeListEditCellTemplateContainer templateContainer = (TreeListEditCellTemplateContainer)cboCodeUnit.NamingContainer;
            TreeListNode node = templateContainer.TreeList.FindNodeByKeyValue(templateContainer.NodeKey);
            // Delete old node and add new node
            //ViewMaterialUnitConstruction oldNode = (ViewMaterialUnitConstruction)node.DataItem;
            //List<ViewMaterialUnitConstruction> ChildrenOfOldNode = getAllChildOfNode(oldNode);
            //ViewMaterialUnitConstruction newNode = settingNewNodeUnit(oldNode.ParentMaterialMaterialUnitId,
            //   long.Parse(e.NewValues["NumRequired"].ToString()));
            //XPCollection<ViewMaterialUnitConstruction> table = MaterialUnitConstruction;
            //table.Remove(oldNode);
            //table.Add(newNode);
            ////Change children's parents
            //foreach (ViewMaterialUnitConstruction v in ChildrenOfOldNode)
            //{
            //    v.ParentMaterialMaterialUnitId = newNode.MaterialMaterialUnitHierachyId;
            //}

            ////update to database
            //materialBLO.updateMaterialUnitOnTree(oldNode, newNode, KeyValue);

            //MaterialUnitConstruction = table;
            //treelstMaterialUnits.DataSource = table;
            treelstMaterialUnits.DataBind();
            e.Cancel = true;
            treelstMaterialUnits.CancelEdit();
        }

        protected void treelstMaterialUnits_NodeValidating(object sender, TreeListNodeValidationEventArgs e)
        {
            #region Init some objest
            ASPxTreeList treelst = sender as ASPxTreeList;
            ASPxComboBox cboCodeUnit = treelstMaterialUnits.FindEditCellTemplateControl(treelstMaterialUnits.Columns["Code"]
                                        as TreeListDataColumn, "cboCodeUnit") as ASPxComboBox;
            TreeListEditCellTemplateContainer templateContainer = (TreeListEditCellTemplateContainer)cboCodeUnit.NamingContainer;
            string selectedCode = cboCodeUnit.SelectedItem != null ? cboCodeUnit.SelectedItem.Value.ToString() : null;
            TreeListNode node;
            //List<ViewMaterialUnitConstruction> table = MaterialUnitConstruction.ToList<ViewMaterialUnitConstruction>();
            //if (table == null)
            //    table = new List<ViewMaterialUnitConstruction>();
            #endregion

            #region Check Require NumberRequired
            if (selectedCode == null || selectedCode == "")
            {
                e.NodeError = String.Format("Bắt buộc chọn đơn vị tính!");
                return;
            }
            #endregion

            #region Check Require NumberRequired
            if (e.NewValues["NumRequired"] == null)
            {
                e.NodeError = String.Format("Bắt buộc nhập số lượng!");
                return;
            }
            #endregion

            #region Check Exist MaterialUnitCode ---- Mode Edit Node
            if (templateContainer.NodeKey != null)
            {
                node = templateContainer.TreeList.FindNodeByKeyValue(templateContainer.NodeKey);
                //ViewMaterialUnitConstruction dataItem = (ViewMaterialUnitConstruction)node.DataItem;
                //string oldCode = dataItem.Code;
                ////Check Exist Code before
                //if (table.Count > 0 && treelst.IsEditing && selectedCode != oldCode)
                //{

                //    if (!isValidCodeEditting(node, selectedCode))
                //    {
                //        e.NodeError = String.Format("Đơn vị tính {0} đã được chọn! Vui lòng chọn cái khác", selectedCode);
                //    }
                //}
            }
            #endregion

            #region Check Exist MaterialUnitCode ---- Mode Add Node
            else
            {
                //if (table.Count > 0 && treelst.IsEditing)
                //{
                //    node = templateContainer.TreeList.FindNodeByKeyValue(treelstMaterialUnits.NewNodeParentKey);

                //    if (node.Level == 0)
                //        return;

                //    if (!isValidCodeAdding(node, selectedCode))
                //    {
                //        e.NodeError = String.Format("Đơn vị tính {0} đã được chọn! Vui lòng chọn cái khác", selectedCode);
                //    }
                //}
            }
            #endregion
        }

        protected void treelstMaterialUnits_CellEditorInitialize(object sender, TreeListColumnEditorEventArgs e)
        {
            ASPxTreeList tree = sender as ASPxTreeList;

            if (e.Column.FieldName == "NumRequired" && e.NodeKey != null)
            {
                TreeListNode node = tree.FindNodeByKeyValue(e.NodeKey);
                //ViewMaterialUnitConstruction dataItem = (ViewMaterialUnitConstruction)node.DataItem;
                //ViewMaterialUnitConstruction fhjfj = (ViewMaterialUnitConstruction)grdMaterialOnCategory.GetRow(1);
                //if (dataItem.ParentMaterialMaterialUnitId.GetHashCode() == 0)
                //{
                //    e.Editor.ClientEnabled = false;
                //    e.Editor.Value = 1;
                //}
            }
            //else
            //{
            //    if (tree.NewNodeParentKey.GetHashCode() == 0)
            //    {
            //        e.Editor.ClientEnabled = false;
            //        e.Editor.Value = 1;
            //    }
            //}
        }

        protected void treelstMaterialUnits_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
        {
            //Setting numberRequired
            ASPxTreeList tree = sender as ASPxTreeList;
            if (e.Column.FieldName == "NumRequired" && e.Level == 1)
            {
                e.Cell.Text = "";
            }

            //Setting Description for node
            if (e.Column.FieldName == "Description" && e.Level > 2)
            {
                TreeListNode node = tree.FindNodeByKeyValue(e.NodeKey);
                //ViewMaterialUnitConstruction parentItem = (ViewMaterialUnitConstruction)node.ParentNode.DataItem;
                //ViewMaterialUnitConstruction currentItem = (ViewMaterialUnitConstruction)node.DataItem;
                //e.Cell.Text = String.Format("1 {0} bao gồm {1} {2}", parentItem.Name, currentItem.NumRequired, currentItem.Name);
            }
        }

        protected void cpCheckMaterialCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string action = para[0];
            //Action(action, "");
        }

        protected void treelstMaterialUnits_CommandColumnButtonInitialize(object sender, TreeListCommandColumnButtonEventArgs e)
        {
            ASPxTreeList tree = sender as ASPxTreeList;
            if (e.NodeKey == null) 
                return;
            TreeListNode node = tree.FindNodeByKeyValue(e.NodeKey);
            //ViewMaterialUnitConstruction currentItem = (ViewMaterialUnitConstruction)node.DataItem;
            if ( e.NodeKey == Utility.Constant.ROOTUNITNODECODE)
            {
                e.CommandColumn.DeleteButton.Visible = false;
                e.CommandColumn.EditButton.Visible = false;
            }
        }

        protected void grdMaterialOnCategory_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.VisibleIndex == -1 && ButtonEditMaterial.Visible &&
                (e.ButtonType == DevExpress.Web.ASPxGridView.ColumnCommandButtonType.New ||
                e.ButtonType == DevExpress.Web.ASPxGridView.ColumnCommandButtonType.Edit ||
                e.ButtonType == DevExpress.Web.ASPxGridView.ColumnCommandButtonType.Delete))
                e.Visible = false;
        }
    }
}