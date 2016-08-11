using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using BLL.PurchasingBLO;
//using BLL.BO.Purchasing;
//using DAL.Purchasing;
//using DAL.PurchasingCode;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxTabControl;


namespace DXApplication1.Purchasing.UserControl
{
    public partial class uBuyingMaterial
    {
        //public bool isValidCodeEditting(TreeListNode current, string newUnitCode)
        //{
        //    if (current == null) return true;

        //    TreeListNodeCollection childrens = current.ChildNodes;
        //    // Kiểm tra từ current đến toàn bộ các node lá
        //    foreach (TreeListNode n in childrens)
        //    {
        //        TreeListNodeIterator iterator = new TreeListNodeIterator(n);
        //        while (iterator.Current != null)
        //        {
        //            ViewMaterialUnitConstruction dataItem = (ViewMaterialUnitConstruction)iterator.Current.DataItem;
        //            if (dataItem.Code == newUnitCode)
        //                return false;
        //            iterator.GetNext();
        //        }
        //    }

        //    // Kiểm tra lên tới node cha level 1
        //    while (current != null && current.Level != 1)
        //    {
        //        current = current.ParentNode;
        //        ViewMaterialUnitConstruction dataItem = (ViewMaterialUnitConstruction)current.DataItem;
        //        if (dataItem.Code == newUnitCode)
        //            return false;
        //    }
        //    return true;
        //}

        //public bool isValidCodeAdding(TreeListNode current, string newUnitCode)
        //{
        //    if (current == null) return true;
        //    // Kiểm tra lên tới node cha level 1
        //    while (current != null && current.Level != 1)
        //    {
        //        ViewMaterialUnitConstruction dataItem = (ViewMaterialUnitConstruction)current.DataItem;
        //        if (dataItem.Code == newUnitCode)
        //            return false;
        //        current = current.ParentNode;
        //    }
        //    return true;
        //}

        //public void setEnableForForm(bool isActivated) {
        //    FormCommonMaterialEdit.Enabled = isActivated;
        //    grdMaterialOnCategory.Columns["action"].Visible = isActivated;
        //    grdSupplierOnMaterial.Columns["action"].Visible = isActivated;
        //    treelstMaterialUnits.Columns["action"].Visible = isActivated;                
        //    //HtmlEditDescription.Enabled = isActivated;
        //    if (isActivated)
        //    {
        //        ButtonEditMaterial.Visible = false;
        //    } else
        //        ButtonEditMaterial.Visible = true;

        //    ButtonSaveMaterial.Visible = false;
        //}

        //public void initializeInsertingMode() {
        //    Mode = "add";
        //    //30/09/2013 Duc.Vo ADD-START (setting Mode to Client)
        //    formMaterialEdit.JSProperties.Add("cpMode", Mode);
        //    //30/09/2013 Duc.Vo ADD-END
        //    //materialBLO.insertDefaultMaterial(KeyValue);
        //    formMaterialEdit.HeaderText = "Thông Tin Nguyên Vật Liệu - Thêm mới";
        //    ButtonSaveMaterial.Visible = true;
        //    ButtonEditMaterial.Visible = false;
        //    formMaterialEdit.ShowOnPageLoad = true;
        //}

        ////public bool validateData()
        ////{
        ////    collectData();
        ////    bool rs = true;
        ////    switch (Mode)
        ////    {
        ////        case "edit":
        ////            //if (!FirstMaterialEntity.Code.Equals(MaterialEntity.Code) && !materialBLO.isDupplicateCode(MaterialEntity))
        ////            //{
        ////            //    txtCode.IsValid = false;
        ////            //    txtCode.ErrorText = String.Format("Mã nguyên vật liệu '{0}' đã tồn tại", txtCode.Text);
        ////            //    rs = false;
        ////            //}
        ////            //else
        ////            //{
        ////            //    txtCode.IsValid = true;
        ////            //    txtCode.ErrorText = String.Empty;
        ////            //}

        ////            break;

        ////        case "add":
        ////            //if (!materialBLO.isDupplicateCode(MaterialEntity))
        ////            //{
        ////            //    txtCode.IsValid = false;
        ////            //    txtCode.ErrorText = String.Format("Mã nguyên vật liệu '{0}' đã tồn tại", txtCode.Text);
        ////            //    rs = false;
        ////            //}
        ////            else
        ////            {
        ////                txtCode.IsValid = true;
        ////                txtCode.ErrorText = String.Empty;
        ////            }

        ////            break;

        ////        default:
        ////            break;
        ////    }

        ////    //30/09/2013 Duc.Vo ADD-START (setting Mode to Client)
        ////    formMaterialEdit.JSProperties.Add("cpIsValidMaterial", rs);
        ////    //30/09/2013 Duc.Vo ADD-END

        ////    return rs;
        ////}

        //public void collectData()
        //{
        //    //27/09/2013 Duc.Vo-START
        //    //materialEntity.MaterialId = KeyValue;
        //    ////27/09/2013 Duc.Vo-END
        //    //materialEntity.RowStatus = cboRowStatus.SelectedItem != null ? cboRowStatus.SelectedItem.Value.ToString()[0] : 'A';
        //    //materialEntity.ManufacturerId = cboManufacturer.SelectedItem == null ?
        //    //    Guid.Parse(Utility.Constant.DEFAULT_MANUFACTURER) : Guid.Parse(cboManufacturer.SelectedItem.GetValue("ManufacturerId").ToString());
        //    //materialEntity.Code = txtCode.Text;
        //    //materialEntity.Name = txtName.Text;
        //    //materialEntity.Description = HtmlEditDescription.Html;
        //    //materialEntity.Language = Utility.Constant.LANG_DEFAULT;
        //    //materialEntity.DocumentUrl = "http://www/google.com";
        //    //materialEntity.LibraryUrl = "http://www/google.com";
        //}

        //public void resetForm()
        //{
        //    //Reset intput field
        //    txtCode.Text = "";
        //    txtName.Text = "";
        //    cboRowStatus.SelectedIndex = 0;
        //    cboManufacturer.Value = "";
        //    HtmlEditDescription.Html = "";
        //    txtCode.IsValid = true;
        //    txtName.IsValid = true;
        //    cboRowStatus.IsValid = true;
        //    cboManufacturer.IsValid = true;
        //    //Reset datasource 
        //    //CategoryEntityList = null;
        //    //SupplierEntityList = null;
        //    //grdMaterialOnCategory.DataSource = null;
        //    //grdMaterialOnCategory.DataBind();
        //    //grdSupplierOnMaterial.DataSource = null;
        //    //grdSupplierOnMaterial.DataBind();
        //    //KeyValue = Guid.NewGuid();
        //    //MaterialUnitConstruction = new XPCollection<ViewMaterialUnitConstruction>(session);
        //    //MaterialUnitConstruction.Criteria = new BinaryOperator("MaterialId", KeyValue, BinaryOperatorType.Equal);
        //    //treelstMaterialUnits.DataSource = MaterialUnitConstruction;
        //    //treelstMaterialUnits.DataBind();
        //    //MaterialUnitConstruction = MaterialUnitConstruction;

        //    //FirstMaterialEntity = null;

        //    treelstMaterialUnits.CancelEdit();
        //    grdMaterialOnCategory.CancelEdit();
        //    grdSupplierOnMaterial.CancelEdit();
        //}

        ////public void loadForm(string id, MaterialBLO materialBLO)
        ////{
        //    //28/09/2013 Duc.Vo MODIFY-START
        //    //setEnableForForm(false);
        //    //27/09/2013 Duc.Vo MODIFY-END
        //    /*Load data to form usercontrol*/

        //    //MaterialEntity material;
        //    //Guid guid = new Guid(id);
        //    ///*setting mode and key value*/
        //    //Mode = "edit";
        //    ////30/09/2013 Duc.Vo ADD-START (setting Mode to Client)
        //    //formMaterialEdit.JSProperties.Add("cpMode", Mode);
        //    ////30/09/2013 Duc.Vo ADD-END
        //    //KeyValue = guid;
        //    //FormCommonMaterialEdit.DataSource = materialBLO.getMaterialByKey(guid, out material);
        //    //FormCommonMaterialEdit.DataBind();
        //    //txtCode.Text = material.Code;
        //    //HtmlEditDescription.Html = material.Description;
        //    //FirstMaterialEntity = material;
        //    ////30/09/2013 Duc.Vo ADD-START (setting First Code to Client)
        //    //formMaterialEdit.JSProperties.Add("cpFirstCode", FirstMaterialEntity.Code);
        //    ////30/09/2013 Duc.Vo ADD-END

        //    ///*Load data to Category gridview usercontrol*/
        //    //grdMaterialOnCategory.DataSource = GetCategoriesTable();
        //    //grdMaterialOnCategory.DataBind();

        //    //grdSupplierOnMaterial.DataSource = GetSuppliersTable();
        //    //grdSupplierOnMaterial.DataBind();

        //    //treelstMaterialUnits.DataSource = GetMaterialUnitConstruction();
        //    //treelstMaterialUnits.DataBind();

        //    //formMaterialEdit.HeaderText = "Thông Tin Nguyên Vật Liệu - Mã số: " + material.Code;
        //    //formMaterialEdit.ShowOnPageLoad = true;
        ////}

        ////public List<MaterialBuyingMaterialCategoryEntity> GetCategoriesTable()
        ////{
        ////    List<MaterialBuyingMaterialCategoryEntity> table = Session["CategoriesTable"] as List<MaterialBuyingMaterialCategoryEntity>;//CategoryEntityList;
        ////    if (table == null)
        ////    {
        ////        table = materialBLO.getCategoriesOnMaterial(KeyValue);
        ////        CategoryEntityList = table;
        ////    }
        ////    return table;
        ////}

        ////public List<MaterialSupplierEntity> GetSuppliersTable()
        ////{
        ////    List<MaterialSupplierEntity> table = Session["SuppliersTable"] as List<MaterialSupplierEntity>;//SupplierEntityList;
        ////    if (table == null)
        ////    {
        ////        table = materialBLO.getSupplierOnMaterial(KeyValue);
        ////        SupplierEntityList = table;
        ////    }
        ////    return table;
        ////}

        ////public XPCollection<ViewMaterialUnitConstruction> GetMaterialUnitConstruction()
        ////{
        ////    XPCollection<ViewMaterialUnitConstruction> construction = new XPCollection<ViewMaterialUnitConstruction>(session);
        ////    construction.Criteria = new BinaryOperator("MaterialId", KeyValue, BinaryOperatorType.Equal);
        ////    MaterialUnitConstruction = construction;
        ////    return construction;
        ////}

        ////public ViewMaterialUnitConstruction findNodeInUnitTree(string KeyValue) {
        ////    TreeListNode node = treelstMaterialUnits.FindNodeByKeyValue(KeyValue);
        ////    ViewMaterialUnitConstruction dataItem = (ViewMaterialUnitConstruction)node.DataItem;
        ////    return dataItem;
        ////}

        ////public ViewMaterialUnitConstruction settingNewNodeUnit(object ParentValue, long number)
        ////{
        ////    ViewMaterialUnitConstruction newNode = new ViewMaterialUnitConstruction(MaterialUnitConstruction.Session);
        ////    ASPxComboBox cboCodeUnit = treelstMaterialUnits.FindEditCellTemplateControl(treelstMaterialUnits.Columns["Code"]
        ////                                as TreeListDataColumn, "cboCodeUnit") as ASPxComboBox;
         
        ////    newNode.MaterialUnitId = Guid.Parse(cboCodeUnit.SelectedItem.GetValue("MaterialUnitId").ToString());
        ////    newNode.Code = cboCodeUnit.SelectedItem.Value.ToString();
        ////    newNode.MaterialId = KeyValue;
        ////    newNode.MaterialMaterialUnitId = Guid.NewGuid();
        ////    newNode.Name = cboCodeUnit.SelectedItem.GetValue("Name").ToString();
        ////    newNode.RowCreationTimeStamp = DateTime.Now;

        ////    if (ParentValue != null)
        ////    {
        ////        Guid parentId = Guid.Parse(ParentValue.ToString());
        ////        newNode.ParentMaterialMaterialUnitId = parentId;
        ////    }

        ////    newNode.NumRequired = number;
        ////    newNode.MaterialMaterialUnitHierachyId = Guid.NewGuid();

        ////    return newNode;
        ////}

        ////public List<ViewMaterialUnitConstruction> getAllChildOfNode(ViewMaterialUnitConstruction node) {
        ////    List<ViewMaterialUnitConstruction> rs = new List<ViewMaterialUnitConstruction>();
        ////    foreach (ViewMaterialUnitConstruction v in MaterialUnitConstruction)
        ////    {
        ////        if (v.ParentMaterialMaterialUnitId == node.MaterialMaterialUnitHierachyId) {
        ////            rs.Add(v);
        ////        }
        ////    }
        ////    return rs;
        ////}

        //public void Action(string action, string id)
        //{
        //    //switch (action)
        //    //{
        //    //    case "AddMaterial":
        //    //        resetForm();
        //    //        initializeInsertingMode();
        //    //        break;
        //    //    case "EditMaterial":
        //    //        resetForm();
        //    //        loadForm(id, materialBLO);
        //    //        break;
        //    //    case "DeleteMaterial":
        //    //        Guid guid = new Guid(id);
        //    //        materialBLO.deleteMaterial(guid);
        //    //        break;
        //    //    case "SaveMaterial":
        //    //        if (!validateData())
        //    //            return;
        //    //        collectData();
        //    //        if (Mode == "add")
        //    //            materialBLO.updateMaterial(materialEntity);
        //    //        resetForm();
        //    //        break;
        //    //    case "CheckMaterial":
        //    //    case "updateByLostFocus":
        //    //        if (!validateData())
        //    //            return;
        //    //        collectData();
        //    //        materialBLO.updateMaterial(materialEntity);
        //    //        FirstMaterialEntity = materialEntity;
        //    //        break;
        //    //    case "ActivateForm":
        //    //        setEnableForForm(true);
        //    //        collectData();
        //    //        formMaterialEdit.HeaderText = "Thông Tin Nguyên Vật Liệu - Mã số: " + MaterialEntity.Code;
        //    //        break;
        //    //    default:
        //    //        break;
        //    }
        //}
    }
}