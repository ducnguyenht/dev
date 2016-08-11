using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Web.ASPxEditors;
//using BLL.BO.Purchasing;
//using BLL.PurchasingBLO;
//using DAL.Purchasing;
//using DevExpress.Web.ASPxTreeList;
//using DAL.PurchasingCode;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxGridView;
using NAS.DAL;
using NAS.DAL.Nomenclature.Inventory;
using DevExpress.Web.ASPxTreeList;
using NAS.BO.Nomenclature.Inventory;

namespace WebModule.Warehouse.UserControl
{
    public partial class uWarehouse : System.Web.UI.UserControl
    {
        public Guid InventoryId
        {
            set { Session["InventoryId"] = value; }
            get
            {
                if (Session["InventoryId"] == null)
                    return Guid.NewGuid();
                return Guid.Parse(Session["InventoryId"].ToString());
            }
        }
        private class PrivateSession
        {
            //private constructor
            private PrivateSession()
            {
                this.InventoryId = Guid.Empty;
            }
            // Gets the current session
            public static PrivateSession Instance
            {
                get
                {
                    PrivateSession session =
                        (PrivateSession)HttpContext.Current.Session["ERPCore_uWarehouse"];
                    if (session == null)
                    {
                        session = new PrivateSession();
                        HttpContext.Current.Session["ERPCore_uWarehouse"] = session;
                    }
                    return session;
                }
            }

            /// <summary>
            /// Clear instance of PrivateSession
            /// </summary>
            public static void ClearInstance()
            {
                HttpContext.Current.Session.Remove("ERPCore_uWarehouse");
            }

            /////Declares all session properties here
            public Guid InventoryId { get; set; }
            public string CurrentInventoryCodeCode { get; set; }

        }
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsInventory.Session = session;
            dsInventoryUnit.Session = session;
            dsInventoryForUnit.Session = session;
        }
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        //protected void cboCodeCategory_Init(object sender, EventArgs e)
        //{
        //    ASPxComboBox cboCodeCategory = sender as ASPxComboBox;
        //    //cboCodeCategory.DataSource = toolBOL.getWarehouseCategoryList();
        //    cboCodeCategory.DataBind();
        //}
        protected void cboCodeUnit_Init(object sender, EventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)sender;
            //comboBox.DataSource = this.toolBOL.getStorageUnitList();
            //comboBox.DataBind();

            //TreeListEditCellTemplateContainer templateContainer = (TreeListEditCellTemplateContainer)comboBox.NamingContainer;
            //if (templateContainer.NodeKey != null)
            //{
            //    TreeListNode node = templateContainer.TreeList.FindNodeByKeyValue(templateContainer.NodeKey);
            //    //ViewWarehouseUnitConstruction dataItem = (ViewWarehouseUnitConstruction)node.DataItem;
            //    //comboBox.Value = dataItem.Code;
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pagWarehouseEdit.ActiveTabIndex = 0;
            }
            frmlInfoGeneral.DataBind();
            this.treelstToolUnits.RootValue = PrivateSession.Instance.InventoryId;
            treelstToolUnits.DataBind();
        }
        protected void CancelEdit()
        {
            this.treelstToolUnits.CancelEdit();
        }
        public string Action { get; set; }
        //protected ASPxHtmlEditor HtmlEditorDescription
        //{
        //    get
        //    {
        //        return (ASPxHtmlEditor)navbarDetailInfo.Groups.FindByName("Description").FindControl("htmleditorDescription");
        //    }
        //}b

        protected void cpLineWarehouse_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "edit":
                    this.ClearForm();
                    frmlInfoGeneral.DataSourceID = "dsInventory";
                    if (args.Length > 1)
                    {
                        PrivateSession.Instance.InventoryId = Guid.Parse(args[1]);
                        dsInventory.CriteriaParameters["InventoryId"].DefaultValue = PrivateSession.Instance.InventoryId.ToString();
                        dsInventoryForUnit.CriteriaParameters["InventoryId"].DefaultValue = PrivateSession.Instance.InventoryId.ToString();
                        treelstToolUnits.DataSourceID = dsInventoryForUnit.ID;
                        treelstToolUnits.StartEditNewNode(treelstToolUnits.RootNode.Key);//DND
                        this.treelstToolUnits.RootValue = PrivateSession.Instance.InventoryId;
                        treelstToolUnits.DataBind();
                        txtCode.Text = CurrentInventoryOrg.Code;
                        txtName.Text = CurrentInventoryOrg.Name;
                        formWarehouseEdit.HeaderText = string.Format("Thông Tin Kho - {0}", CurrentInventoryOrg.Code);
                    }
                    formWarehouseEdit.ShowOnPageLoad = true;
                    break;
                default:
                    break;
            }
        }

        public void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmlInfoGeneral);
            this.pagWarehouseEdit.ActiveTabIndex = 0;
            this.cbRowStatus.SelectedIndex = 0;
            //HtmlEditorDescription.Html = String.Empty;
            txtCode.IsValid = true;
            txtName.IsValid = true;
            txtName.Text = "";
            PrivateSession.ClearInstance();
        }
        private Inventory CurrentInventoryOrg
        {
            get
            {
                if (PrivateSession.Instance.InventoryId == Guid.Empty)
                {
                    return null;
                }
                return
                    session.GetObjectByKey<Inventory>(PrivateSession.Instance.InventoryId);
            }
        }
        protected void popWarehouseEdit_WindowCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":
                    this.ClearForm();
                    Inventory inventory = Inventory.InitNewRow(this.session);
                    PrivateSession.Instance.InventoryId = inventory.InventoryId;
                    frmlInfoGeneral.DataSourceID = "dsInventory";
                    this.dsInventory.CriteriaParameters["InventoryId"].DefaultValue = PrivateSession.Instance.InventoryId.ToString();
                    treelstToolUnits.StartEditNewNode(treelstToolUnits.RootNode.Key); //DND
                    //this.treelstToolUnits.RootValue = PrivateSession.Instance.InventoryId;
                    this.treelstToolUnits.RootValue = null;
                    treelstToolUnits.DataSourceID = null;
                    treelstToolUnits.DataBind();
                    txtName.Text = "";
                    break;
                case "save":
                    bool isSuccess = true;
                    string recordIdStr = null;
                    try
                    {
                        //Check validation
                        if (!ASPxEdit.AreEditorsValid(pagWarehouseEdit, true))
                        {
                            formWarehouseEdit.JSProperties.Add("cpInvalid", true);
                            pagWarehouseEdit.ActiveTabIndex = 0;
                            return;
                        }

                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            //Update general information
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            Inventory editManufacturerOrg =
                                session.GetObjectByKey<Inventory>(PrivateSession.Instance.InventoryId);
                            editManufacturerOrg.Code = txtCode.Text;
                            editManufacturerOrg.Name = txtName.Text;
                            editManufacturerOrg.Address = memoAddress.Text;
                            //editManufacturerOrg.RowStatus = short.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            editManufacturerOrg.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                            editManufacturerOrg.Save();
                        }
                        else
                        {
                            //Insert mode
                            Inventory newManufacturerOrg =
                                session.GetObjectByKey<Inventory>(PrivateSession.Instance.InventoryId);
                            newManufacturerOrg.Code = txtCode.Text;
                            newManufacturerOrg.Name = txtName.Text;
                            newManufacturerOrg.Address = memoAddress.Text;
                            newManufacturerOrg.RowStatus = short.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            newManufacturerOrg.Save();
                        }
                    }
                    catch (Exception)
                    {
                        isSuccess = false;
                        throw;
                    }
                    finally
                    {
                        this.CancelEdit();
                        OnSaved(new WebModule.Interfaces.FormEditEventArgs() { isSuccess = isSuccess });
                        formWarehouseEdit.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));
                        PrivateSession.ClearInstance();
                    }
                    break;
                case "cancel":
                    this.CancelEdit();
                    break;
                default:
                    break;
            }
        }
        public event WebModule.Interfaces.FormEditEventHandler Saved;
        public virtual void OnSaved(WebModule.Interfaces.FormEditEventArgs e)
        {
            if (Saved != null)
            {
                Saved(this, e);
            }
        }
        /////2013-09-21 ERP-580 Khoa.Truong INS START
        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String inventoryCode = e.Value.ToString().Trim();
            //New mode
            if (PrivateSession.Instance.InventoryId == Guid.Empty)
            {
                bool isExist = Util.isExistXpoObject<Inventory>("Code", inventoryCode);
                if (isExist)
                {
                    e.IsValid = false;
                    e.ErrorText =
                        String.Format("Mã kho '{0}' đã được sử dụng", inventoryCode);
                }
                else
                {
                    e.IsValid = true;
                    e.ErrorText = String.Empty;
                }
            }
            //Edit mode  
            else
            {
                //Validate if new code not equal old code
                if (!inventoryCode.Equals(CurrentInventoryOrg.Code))
                {
                    bool isExist = Util.isExistXpoObject<Inventory>("Code", inventoryCode);
                    if (isExist)
                    {
                        e.IsValid = false;
                        e.ErrorText = String.Format("Mã kho '{0}' đã được sử dụng", inventoryCode);
                    }
                    else
                    {
                        e.IsValid = true;
                        e.ErrorText = String.Empty;
                    }
                }
            }
        }
        /////2013-09-21 ERP-580 Khoa.Truong INS END

        /// <summary>
        /// Implements IFormEditBase interface
        /// </summary>
        #region IFormEditBase Implementation


        #endregion



        //protected void grdataWarehouseCategory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        //{
        //    e.Cancel = true;
        //}

        //protected void grdataWarehouseCategory_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        //{
        //    ASPxGridView grid = sender as ASPxGridView;
        //    ASPxComboBox cboCode = this.grdataWarehouseCategory.FindEditRowCellTemplateControl(grdataWarehouseCategory.Columns["Code"] as GridViewDataColumn
        //                , "cboCodeCategory") as ASPxComboBox;
        //    string selectedCode = cboCode.SelectedItem.Value.ToString();
        //}

        //protected void grdataWarehouseCategory_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        //{
        //    ASPxGridView grdToolOnCategory = sender as ASPxGridView;
        //    ASPxComboBox cboCode = grdToolOnCategory.FindEditRowCellTemplateControl(this.grdataWarehouseCategory.Columns["Code"] as GridViewDataColumn
        //                , "cboCodeCategory") as ASPxComboBox;
        //    //List<WarehouseWarehouseCategoryEntity> table = CategoryEntityList;
        //    //if (table == null)
        //    //    table = new List<WarehouseWarehouseCategoryEntity>();
        //    //string selectedCode = cboCode.SelectedItem.Value.ToString();
        //    //WarehouseWarehouseCategoryEntity category = new WarehouseWarehouseCategoryEntity();
        //    //if (this.Action != "new")
        //    //{
        //    //    category = this.toolBOL.getCategoriesOnWarehouse(selectedCode, this.toolKey);
        //    //}
        //    //else
        //    //{
        //    //    category = toolBOL.getCategoryByCode(selectedCode);
        //    //}

        //    //table.Add(new WarehouseWarehouseCategoryEntity(
        //    //    category.WarehouseWarehouseCategoryId,
        //    //    category.WarehouseId,
        //    //    category.WarehouseCategoryId,
        //    //    selectedCode,
        //    //    category.Name
        //    //));

        //    //CategoryEntityList = table;
        //    //this.grdataWarehouseCategory.DataSource = table;
        //    //grdataWarehouseCategory.DataBind();

        //    e.Cancel = true;
        //    grdataWarehouseCategory.CancelEdit();
        //}

        protected void treelstToolUnits_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlDataCellEventArgs e)
        {
            ASPxTreeList tree = sender as ASPxTreeList;
            TreeListNode node = tree.FindNodeByKeyValue(e.NodeKey);
            Inventory currentProductProductUnit = node.DataItem as Inventory;

            if (currentProductProductUnit != null)
            {
                //Setting Description for node
                if (e.Column.Name == "Description" && e.Level > 1)
                {

                    e.Cell.Text = String.Format("Một {0} bao gồm {1} {2}",
                        currentProductProductUnit.ParentInventoryId.InventoryUnitId.Name,
                        currentProductProductUnit.NumRequired,
                        currentProductProductUnit.InventoryUnitId.Name);
                }
                else if (e.Column.Name == "Description" && e.Level == 1)
                {
                    e.Cell.Text = String.Format("Kho bao gồm {1} {0}", currentProductProductUnit.InventoryUnitId.Name, currentProductProductUnit.NumRequired);
                }
            }
        }

        protected void treelstToolUnits_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //DND
            //Guid inventoryID = PrivateSession.Instance.InventoryId;
            Guid inventoryUnitId = Guid.Parse(e.NewValues["InventoryUnitId!Key"].ToString());
            Boolean check = false;
            InventoryBO bo = new InventoryBO();

            Guid checkGuid = Guid.Empty;
            
            List<Guid> inventotyUnitList = new List<Guid>();
            bo.getInventoryTree(session, PrivateSession.Instance.InventoryId, ref inventotyUnitList);
            if (inventotyUnitList != null || inventotyUnitList.Count > 0)
                checkGuid = inventotyUnitList.Find(p => p == inventoryUnitId);

            if (!Guid.Equals(checkGuid, Guid.Empty))
            {
                check = true;
            }

            Guid parentInventotyId;
            if (!treelstToolUnits.NewNodeParentKey.Equals(String.Empty))
            {
                parentInventotyId = Guid.Parse(treelstToolUnits.NewNodeParentKey);
                //    parentInventotyId = PrivateSession.Instance.InventoryId;
                if (bo.checkIsAlreadyHaveAChild(session, parentInventotyId))
                {
                    throw new Exception(String.Format("Lỗi: 1 nhánh chỉ được tạo 1 đơn vị lưu trữ"));
                }

            }
            if(check == true)
            {
                //SQL search Name trong InventoryUnit
                XPCollection<InventoryUnit> collectionUnit = new XPCollection<InventoryUnit>(session);
                collectionUnit.Criteria = CriteriaOperator.And(
                    new BinaryOperator("InventoryUnitId", inventoryUnitId, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.Equal)
                    );
                InventoryUnit nameInventoryUnit = collectionUnit.First(); //Sau khi search lay duoc Mang gia tri
                //nameInventoryUnit . goi ra ten cot can lay
                throw new Exception(String.Format("Lỗi trùng đơn vị lưu trữ {0} trên cây", nameInventoryUnit.Name.ToString()));
            }
            else
            {
                string parentKey = treelstToolUnits.NewNodeParentKey;
                if (parentKey.Equals(String.Empty))
                {
                    e.NewValues["ParentInventoryId!Key"] = this.treelstToolUnits.RootValue;
                }
            }
            //END DND
        }
        //DND kiem tra khi Press Tab se focus vao Combobox
        protected void treelstToolUnits_CellEditorInitialize(object sender, TreeListColumnEditorEventArgs e)
        {
            if (e.Column.Name.Equals("Name_InventoryUnitId") && !treelstToolUnits.IsNewNodeEditing)
            {
                ASPxComboBox cboEdit = (ASPxComboBox)e.Editor;
                cboEdit.Enabled = false;
            }

            if (e.Column.Name.Equals("Name_InventoryUnitId") && treelstToolUnits.IsNewNodeEditing)
            {
                e.Editor.Focus();
            }
        }

        protected void treelstToolUnits_NodeValidating(object sender, TreeListNodeValidationEventArgs e)
        {

           
            if (e.NewValues["NumRequired"] == null)
            {
                throw new Exception(String.Format("Bao Gồm không được để trống"));
            }
            else if (int.Parse(e.NewValues["NumRequired"].ToString()) <= 0)
            {
                throw new Exception(String.Format("Số phải lớn hơn 0"));
            }
            
        }
        //DND end

    }
}