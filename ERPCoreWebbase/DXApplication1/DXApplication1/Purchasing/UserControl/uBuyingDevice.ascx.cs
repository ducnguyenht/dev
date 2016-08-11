using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxHtmlEditor;
//using BLL.BO.Purchasing;
//using BLL.PurchasingBLO;
using DevExpress.Xpo;
//using DAL.Purchasing;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTreeList;
//using DAL.PurchasingCode;
using DevExpress.Data.Filtering;

namespace WebModule.Purchasing.UserControl
{
    public partial class uBuyingDevice : System.Web.UI.UserControl
    {
        //private ToolEntity toolEntity = new ToolEntity();
        //ToolBLO toolBOL = new ToolBLO();
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelpers.GetNewSession();
            cboManufactureXPO.Session = session;
            //this.grdataBuyingToolCategory.DataSource = CategoryEntityList;
            this.grdataBuyingToolCategory.DataBind();

            //grdataSupplier.DataSource = SupplierEntityList;
            grdataSupplier.DataBind();
            //treelstToolUnits.DataSource = ToolUnitConstruction;
            //treelstToolUnits.RootNode = toolBOL.getToolToolUnitRootId();
            treelstToolUnits.DataBind();
        }

        protected void cboCodeSupplier_Init(object sender, EventArgs e)
        {
            //SupplierBLO supplierBLO = new SupplierBLO();
            ASPxComboBox cboCodeSupplier = sender as ASPxComboBox;
            //cboCodeSupplier.DataSource = supplierBLO.getSupplierList();
            cboCodeSupplier.DataBind();
        }
        protected void cboCodeCategory_Init(object sender, EventArgs e)
        {
            ASPxComboBox cboCodeCategory = sender as ASPxComboBox;
            //cboCodeCategory.DataSource = toolBOL.getBuyingToolCategoryList();
            cboCodeCategory.DataBind();
        }
        protected void cboCodeUnit_Init(object sender, EventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)sender;
            //comboBox.DataSource = this.toolBOL.getToolUnitList();
            comboBox.DataBind();

            TreeListEditCellTemplateContainer templateContainer = (TreeListEditCellTemplateContainer)comboBox.NamingContainer;
            if (templateContainer.NodeKey != null)
            {
                TreeListNode node = templateContainer.TreeList.FindNodeByKeyValue(templateContainer.NodeKey);
                //ViewToolUnitConstruction dataItem = (ViewToolUnitConstruction)node.DataItem;
                //comboBox.Value = dataItem.Code;
            }
        }
        //public List<ToolSupplierEntity> SupplierEntityList
        //{
        //    get
        //    {
        //        List<ToolSupplierEntity> table = Session["SuppliersTable"] as List<ToolSupplierEntity>;
        //        if (table == null)
        //            table = new List<ToolSupplierEntity>();
        //        return table;
        //    }

        //    set
        //    {
        //        Session["SuppliersTable"] = value;
        //    }
        //}
        //public XPCollection<ViewToolUnitConstruction> ToolUnitConstruction
        //{
        //    get
        //    {
        //        return Session["ToolUnitConstruction"] as XPCollection<ViewToolUnitConstruction>;
        //    }

        //    set
        //    {
        //        Session["ToolUnitConstruction"] = value;
        //    }
        //}
        //public List<ToolBuyingToolCategoryEntity> CategoryEntityList
        //{
        //    get
        //    {
        //        List<ToolBuyingToolCategoryEntity> table = Session["CategoriesTable"] as List<ToolBuyingToolCategoryEntity>;
        //        if (table == null)
        //            table = new List<ToolBuyingToolCategoryEntity>();
        //        return table;
        //    }

        //    set
        //    {
        //        Session["CategoriesTable"] = value;
        //    }

        //} 
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
            //this.toolBOL.DisposeSession();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pagDeviceEdit.ActiveTabIndex = 0;
            }
            frmlInfoGeneral.DataBind();
        }
        protected void CancelEdit()
        {
            this.grdataSupplier.CancelEdit();
            this.grdataBuyingToolCategory.CancelEdit();
            this.treelstToolUnits.CancelEdit();
        }
        public string Action { get; set; }
        public Guid toolKey { get; set; }
        protected ASPxHtmlEditor HtmlEditorDescription
        {
            get
            {
                return (ASPxHtmlEditor)navbarDetailInfo.Groups.FindByName("Description").FindControl("htmleditorDescription");
            }
        }

        protected void cpLineDevice_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "edit":
                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    this.ClearForm();
                    /////2013-09-21 ERP-580 Khoa.Truong INS END
                    Action = "edit";
                    //ToolEntity toolEntity;
                    Guid guid = new Guid(args[1]);
                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    Session["ToolId"] = guid;
                    /////2013-09-21 ERP-580 Khoa.Truong INS END
                    toolKey = guid;
                    LoadForm();
                    //frmlInfoGeneral.DataSource = this.toolBOL.getToolByKey(guid, out toolEntity);
                    frmlInfoGeneral.DataBind();

                    //HtmlEditorDescription.Html = toolEntity.Description;
                    //formDeviceEdit.HeaderText = "Thông tin công cụ dụng cụ - Mã số: " + toolEntity.Code;
                    formDeviceEdit.ShowOnPageLoad = true;

                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    //txtCode.Text = toolEntity.Code;
                    /////2013-09-21 ERP-580 Khoa.Truong INS END

                    break;
                default:
                    break;
            }
        }
        public void createToolEntity()
        {
            //toolEntity.RowStatus = cbRowStatus.SelectedItem.Value.ToString()[0];
            //toolEntity.ManufacturerId = new Guid(cboManufacturer.SelectedItem.GetValue("ManufacturerId").ToString());
            //toolEntity.Code = txtCode.Text;
            //toolEntity.Name = txtName.Text;
            //toolEntity.Description = HtmlEditorDescription.Html;
            //toolEntity.Language = "VN";
            //toolEntity.DocumentUrl = "http://www/google.com";
            //toolEntity.LibraryUrl = "http://www/google.com";
        }

        /////2013-09-21 ERP-580 Khoa.Truong INS START
        //protected ToolEntity ToolEntity
        //{
        //    get
        //    {
        //        Guid recordId = Guid.Empty;
        //        try
        //        {
        //            recordId = (Guid)Session["ToolId"];
        //            if (recordId == Guid.Empty) return null;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //        //ToolEntity toolEntity;
        //        //this.toolBOL.getToolByKey(recordId, out toolEntity);
        //        return toolEntity;
        //    }
        //}
        /////2013-09-20 ERP-580 Khoa.Truong INS END

        protected void popDeviceEdit_WindowCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":

                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    Session["ToolId"] = Guid.Empty;
                    /////2013-09-21 ERP-580 Khoa.Truong INS END

                    Action = "new";
                    frmlInfoGeneral.DataSourceID = null;
                    this.CancelEdit();
                    ClearForm();
                    //popManufacturerGroupEdit.ShowOnPageLoad = true;
                    break;
                case "edit":
                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    this.ClearForm();
                    /////2013-09-21 ERP-580 Khoa.Truong INS END
                    Action = "edit";
                    //ToolEntity toolEntity;
                    Guid guid = new Guid(args[1]);

                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    Session["ToolId"] = guid;
                    /////2013-09-21 ERP-580 Khoa.Truong INS END

                    toolKey = guid;
                    LoadForm();
                    //frmlInfoGeneral.DataSource = this.toolBOL.getToolByKey(guid, out toolEntity);
                    frmlInfoGeneral.DataBind();

                    //HtmlEditorDescription.Html = toolEntity.Description;
                    //formDeviceEdit.HeaderText = "Thông tin công cụ dụng cụ - Mã số: " + toolEntity.Code;
                    formDeviceEdit.ShowOnPageLoad = true;

                    /////2013-09-21 ERP-580 Khoa.Truong INS START
                    //txtCode.Text = toolEntity.Code;
                    /////2013-09-21 ERP-580 Khoa.Truong INS END

                    break;
                case "save":
                    bool isSuccess = true;
                    string recordIdStr = null;
                    try
                    {
                        /////2013-09-21 ERP-580 Khoa.Truong INS START
                        //Check validation
                        if (!ASPxEdit.AreEditorsValid(pagDeviceEdit, true))
                        {
                            formDeviceEdit.JSProperties.Add("cpInvalid", true);
                            pagDeviceEdit.ActiveTabIndex = 0;
                            return;
                        }
                        /////2013-09-21 ERP-580 Khoa.Truong INS START

                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            //ToolEntity entity = new ToolEntity();
                            //entity.ToolId = recordId;
                            //entity.Code = txtCode.Text;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //entity.LibraryUrl = "";
                            //entity.DocumentUrl = "";
                            //this.toolBOL.updateTool(entity, CategoryEntityList, SupplierEntityList, ToolUnitConstruction);
                        }
                        else
                        {
                            //Insert mode
                            //ToolEntity entity = new ToolEntity();
                            //entity.Code = txtCode.Text;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.ManufacturerId = new Guid(cboManufacturer.SelectedItem.GetValue("ManufacturerId").ToString());
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;
                            //entity.LibraryUrl = "";
                            //entity.DocumentUrl = "";
                            //this.toolBOL.insertTool(entity, CategoryEntityList, SupplierEntityList, ToolUnitConstruction);
                        }

                        //popManufacturerGroupEdit.ShowOnPageLoad = false;
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
                        formDeviceEdit.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));
                    }
                    break;
                case "cancel":
                    this.CancelEdit();
                    break;
                default:
                    break;
            }
        }

        /////2013-09-21 ERP-580 Khoa.Truong INS START
        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            //String toolCode = e.Value.ToString().Trim();
            //New mode
            //if ((Guid)Session["ToolId"] == Guid.Empty)
            //{
            //    if (toolBOL.isCodeExist(toolCode))
            //    {
            //        e.IsValid = false;
            //        e.ErrorText = String.Format("Mã công cụ dụng cụ '{0}' đã được sử dụng", toolCode);
            //    }
            //    else
            //    {
            //        e.IsValid = true;
            //        e.ErrorText = String.Empty;
            //    }
            //}
            //Edit mode
            //else
            //{
                //Validate if new code not equal old code
            //    if (!toolCode.Equals(this.ToolEntity.Code))
            //    {
            //        if (toolBOL.isCodeExist(toolCode))
            //        {
            //            e.IsValid = false;
            //            e.ErrorText = String.Format("Mã công cụ dụng cụ '{0}' đã được sử dụng", toolCode);
            //        }
            //        else
            //        {
            //            e.IsValid = true;
            //            e.ErrorText = String.Empty;
            //        }
            //    }
            //}
        }
        /////2013-09-21 ERP-580 Khoa.Truong INS END

        /// <summary>
        /// Implements IFormEditBase interface
        /// </summary>
        #region IFormEditBase Implementation
        public event WebModule.Interfaces.FormEditEventHandler Saved;
        public void LoadForm()
        {
            //this.grdataBuyingToolCategory.DataSource = this.toolBOL.getCategoriesOnTool(this.toolKey);
            //this.grdataBuyingToolCategory.DataBind();

            //this.grdataSupplier.DataSource = this.toolBOL.getSupplierOnTool(toolKey);
            //this.grdataSupplier.DataBind();

            //XPCollection<ViewToolUnitConstruction> construction = new XPCollection<ViewToolUnitConstruction>(session);
            //construction.Criteria = new BinaryOperator("ToolId", toolKey, BinaryOperatorType.Equal);
            //ToolUnitConstruction = construction;

            //treelstToolUnits.DataSource = construction;
           // treelstToolUnits.RootValue = toolBOL.getToolToolUnitRootId(this.toolKey);
            treelstToolUnits.DataBind();
        }
        public void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmlInfoGeneral);
            pagDeviceEdit.ActiveTabIndex = 0;
            this.cbRowStatus.SelectedIndex = 0;
            HtmlEditorDescription.Html = String.Empty;
            //CategoryEntityList = null;
            //SupplierEntityList = null;
            this.grdataBuyingToolCategory.DataSource = null;
            this.grdataBuyingToolCategory.DataBind();
            this.grdataSupplier.DataSource = null;
            this.grdataSupplier.DataBind();

            //ToolUnitConstruction = new XPCollection<ViewToolUnitConstruction>(session);
            //ToolUnitConstruction.Criteria = new BinaryOperator("ToolId", toolKey, BinaryOperatorType.Equal);
            //treelstToolUnits.DataSource = ToolUnitConstruction;
            treelstToolUnits.DataBind();
            //ToolUnitConstruction = ToolUnitConstruction;

            txtCode.IsValid = true;
            txtName.IsValid = true;
            cbRowStatus.IsValid = true;

        }

        public virtual void OnSaved(WebModule.Interfaces.FormEditEventArgs e)
        {
            if (Saved != null)
            {
                Saved(this, e);
            }
        }
        #endregion

        protected void treelstToolUnits_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            treelstToolUnits.DoNodeValidation();
            string keystr = e.Values["ToolToolUnitId"].ToString();
            CriteriaOperator criteria = new BinaryOperator("ToolToolUnitId", Guid.Parse(keystr),
                                        BinaryOperatorType.Equal);

            //if (ToolUnitConstruction != null)
            //{
            //    XPCollection<ViewToolUnitConstruction> table = ToolUnitConstruction;
            //    foreach (ViewToolUnitConstruction o in table)
            //        if (o.ToolToolUnitId == Guid.Parse(keystr))
            //        {
            //            table.Remove(o);
            //            treelstToolUnits.DataSource = table;
            //            treelstToolUnits.DataBind();
            //            ToolUnitConstruction = table;
            //            break;
            //        }
            //}
            e.Cancel = true;
            treelstToolUnits.CancelEdit();
        }

        protected void treelstToolUnits_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            treelstToolUnits.DoNodeValidation();

            //ViewToolUnitConstruction muConstruction = new ViewToolUnitConstruction(ToolUnitConstruction.Session);
            //ASPxComboBox cboCodeUnit = treelstToolUnits.FindEditCellTemplateControl(treelstToolUnits.Columns["Code"]
            //                            as TreeListDataColumn, "cboCodeUnit") as ASPxComboBox;

            //muConstruction.Code = cboCodeUnit.SelectedItem.Value.ToString();
            //muConstruction.ToolUnitId = Guid.Parse(cboCodeUnit.SelectedItem.GetValue("ToolUnitId").ToString());
            //muConstruction.ToolId = toolKey;
            //muConstruction.ToolToolUnitId = Guid.NewGuid();
            //muConstruction.Name = cboCodeUnit.SelectedItem.GetValue("Name").ToString();
            //muConstruction.NumRequired = long.Parse(e.NewValues["NumRequired"].ToString());
            ////muConstruction.ToolUnitPropertyId = long.Parse(cboCodeUnit.SelectedItem.GetValue("ToolUnitPropertyId").ToString()); 
            //if (e.NewValues[treelstToolUnits.ParentFieldName] != null)
            //    muConstruction.ParentToolToolUnitId = Guid.Parse(e.NewValues[treelstToolUnits.ParentFieldName].ToString());

            //XPCollection<ViewToolUnitConstruction> table = ToolUnitConstruction;
            //table.Add(muConstruction);
            //treelstToolUnits.DataSource = table;
            //treelstToolUnits.DataBind();
            //ToolUnitConstruction = table;

            e.Cancel = true;
            treelstToolUnits.CancelEdit();
        }

        protected void treelstToolUnits_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

            //ASPxTreeList tree = sender as ASPxTreeList;
            //XPCollection<ViewToolUnitConstruction> table = ToolUnitConstruction;
            //treelstToolUnits.DoNodeValidation();
            //string keystr = e.Keys["ToolToolUnitId"].ToString();
            //if (ToolUnitConstruction != null)
            //{
            //        XPCollection<ViewToolUnitConstruction> table = ToolUnitConstruction;
            //    foreach (ViewToolUnitConstruction o in table)
            //        if (o.ToolToolUnitId == Guid.Parse(keystr))
            //        {
            //            ASPxComboBox cboCodeUnit = treelstToolUnits.FindEditCellTemplateControl(treelstToolUnits.Columns["Code"]
            //                                as TreeListDataColumn, "cboCodeUnit") as ASPxComboBox;
            //            o.Code = cboCodeUnit.SelectedItem.Value.ToString();
            //            o.ToolUnitId = Guid.Parse(cboCodeUnit.SelectedItem.GetValue("ToolUnitId").ToString());
            //            o.Name = cboCodeUnit.SelectedItem.GetValue("Name").ToString();
            //            o.NumRequired = long.Parse(e.NewValues["NumRequired"].ToString());
            //            break;
            //        }
            //    treelstToolUnits.DataSource = table;
            //    treelstToolUnits.DataBind();
            //    ToolUnitConstruction = table;
            //}
           
            //e.Cancel = true;
            //treelstToolUnits.CancelEdit();
        }

        protected void treelstToolUnits_NodeValidating(object sender, TreeListNodeValidationEventArgs e)
        {
            #region Init some objest
            ASPxTreeList treelst = sender as ASPxTreeList;
            ASPxComboBox cboCodeUnit = treelstToolUnits.FindEditCellTemplateControl(treelstToolUnits.Columns["Code"]
                                        as TreeListDataColumn, "cboCodeUnit") as ASPxComboBox;
            TreeListEditCellTemplateContainer templateContainer = (TreeListEditCellTemplateContainer)cboCodeUnit.NamingContainer;
            string selectedCode = cboCodeUnit.SelectedItem != null ? cboCodeUnit.SelectedItem.Value.ToString() : selectedCode = null;

            //List<ViewToolUnitConstruction> table = ToolUnitConstruction.ToList<ViewToolUnitConstruction>();
            //if (table == null)
            //    table = new List<ViewToolUnitConstruction>();
            #endregion

            #region Check Require NumberRequired
            //if (treelst.NewNodeParentKey != null)
            //{
            //    //if (this.toolBOL.isToolToolUnitRootId(Guid.Parse(treelst.NewNodeParentKey)))
            //    //{
            //       // e.NodeError = String.Format("Không cho phép cập nhật đơn vị tính mặt định!");
            //        //return;
            //    //}
            //}
            //else if (treelst.EditingNodeKey != null)
            //{
            //    if (this.toolBOL.isToolToolUnitRootId(Guid.Parse(treelst.EditingNodeKey)))
            //    {
            //        //e.NodeError = String.Format("Không cho phép cập nhật đơn vị tính mặt định!");
            //        //return;
            //    }
            //}
            
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

            #region Check Exist ToolUnitCode ---- Mode Edit Node
            if (templateContainer.NodeKey != null)
            {
                TreeListNode node = templateContainer.TreeList.FindNodeByKeyValue(templateContainer.NodeKey);
               // ViewToolUnitConstruction dataItem = (ViewToolUnitConstruction)node.DataItem;
                //string oldCode = dataItem.Code;
                //Check Exist Code before
                //if (table.Count > 0 && treelst.IsEditing && selectedCode != oldCode)
                //{
                //    if (table.Find(items => items.Code == selectedCode) != null)
                //    {
                //        e.NodeError = String.Format("Đơn vị tính {0} đã được chọn! Vui lòng chọn cái khác", selectedCode);
                //    }
                //}
            }
            #endregion

            #region Check Exist ToolUnitCode ---- Mode Add Node
            //else
            //{
                //if (table.Count > 0 && treelst.IsEditing)
                //{
                //    if (table.Find(items => items.Code == selectedCode) != null)
                //    {
                //        e.NodeError = String.Format("Đơn vị tính {0} đã được chọn! Vui lòng chọn cái khác", selectedCode);
                //    }
                //}
            //}
            #endregion
        }

        protected void treelstToolUnits_CellEditorInitialize(object sender, TreeListColumnEditorEventArgs e)
        {
            ASPxTreeList tree = sender as ASPxTreeList;

            if (e.Column.FieldName == "NumRequired" && e.NodeKey != null)
            {
                TreeListNode node = tree.FindNodeByKeyValue(e.NodeKey);
                //ViewToolUnitConstruction dataItem = (ViewToolUnitConstruction)node.DataItem;
                //if (dataItem.ParentToolToolUnitId.GetHashCode() == 0)
                //{
                //    e.Editor.ClientEnabled = false;
                //    e.Editor.Value = 1;
                //}
            }
            else
            {
                if (tree.NewNodeParentKey == "")
                {
                    e.Editor.ClientEnabled = false;
                    e.Editor.Value = 1;
                }
            }
        }

        protected void treelstToolUnits_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
        {
            ASPxTreeList tree = sender as ASPxTreeList;
            if (e.Column.FieldName == "NumRequired" && (e.Level == 1 || e.Level == 2))
            {
                e.Cell.Text = "1";
            }

            if ( e.Column.FieldName == "Description" &&  e.Level == 2) 
            {
                TreeListNode node = tree.FindNodeByKeyValue(e.NodeKey);
                //ViewToolUnitConstruction parentItem = (ViewToolUnitConstruction)node.ParentNode.DataItem;
                //ViewToolUnitConstruction currentItem = (ViewToolUnitConstruction)node.DataItem;
                //e.Cell.Text = String.Format("CCDC có Đơn vị tính cao nhất là {0}", currentItem.Name);
            }
            else if (e.Column.FieldName == "Description" && (e.Level != 1 && e.Level != 2))
            {
                TreeListNode node = tree.FindNodeByKeyValue(e.NodeKey);
                //ViewToolUnitConstruction parentItem = (ViewToolUnitConstruction)node.ParentNode.DataItem;
                //ViewToolUnitConstruction currentItem = (ViewToolUnitConstruction)node.DataItem;
                //e.Cell.Text = String.Format("1 {0} bao gồm {1} {2}", parentItem.Name, currentItem.NumRequired, currentItem.Name);
            }
        }
        protected void grdataBuyingToolCategory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //ASPxGridView grdToolOnCategory = sender as ASPxGridView;
            //List<ToolBuyingToolCategoryEntity> table = CategoryEntityList;
            //if (table == null)
            //    table = new List<ToolBuyingToolCategoryEntity>();
            //ToolBuyingToolCategoryEntity rs = table.Find(items => items.Code == e.Keys["Code"].ToString());
            //if (rs != null)
            //{
            //    table.Remove(rs);
            //    CategoryEntityList = table;
            //}
            e.Cancel = true;
        }

        protected void grdataBuyingToolCategory_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxComboBox cboCode = this.grdataBuyingToolCategory.FindEditRowCellTemplateControl(grdataBuyingToolCategory.Columns["Code"] as GridViewDataColumn
                        , "cboCodeCategory") as ASPxComboBox;
            string selectedCode = cboCode.SelectedItem.Value.ToString();

            //List<ToolBuyingToolCategoryEntity> table = CategoryEntityList;
            //if (table == null)
            //    table = new List<ToolBuyingToolCategoryEntity>();
            //GridViewColumn colCode = grid.Columns["Code"];
            //if (table.Count > 0 && grid.IsNewRowEditing)
            //{
            //    if (table.Find(items => items.Code == selectedCode) != null)
            //    {
                    //e.Errors[colCode] = String.Format("Mã nhóm {0} đã được chọn", selectedCode);
                    //e.RowError = String.Format("Mã nhóm {0} đã được chọn! Vui lòng chọn nhóm khác", selectedCode);
            //}
            //}
        }

        protected void grdataBuyingToolCategory_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grdToolOnCategory = sender as ASPxGridView;
            ASPxComboBox cboCode = grdToolOnCategory.FindEditRowCellTemplateControl(this.grdataBuyingToolCategory.Columns["Code"] as GridViewDataColumn
                        , "cboCodeCategory") as ASPxComboBox;
            //List<ToolBuyingToolCategoryEntity> table = CategoryEntityList;
            //if (table == null)
            //    table = new List<ToolBuyingToolCategoryEntity>();
            //string selectedCode = cboCode.SelectedItem.Value.ToString();
            //ToolBuyingToolCategoryEntity category = new ToolBuyingToolCategoryEntity();
            //if (this.Action != "new")
            //{
            //    category = this.toolBOL.getCategoriesOnTool(selectedCode, this.toolKey);
            //}
            //else
            //{
            //    category = toolBOL.getCategoryByCode(selectedCode);
            //}

            //table.Add(new ToolBuyingToolCategoryEntity(
            //    category.ToolBuyingToolCategoryId,
            //    category.ToolId,
            //    category.BuyingToolCategoryId,
            //    selectedCode,
            //    category.Name
            //));

            //CategoryEntityList = table;
            //this.grdataBuyingToolCategory.DataSource = table;
            grdataBuyingToolCategory.DataBind();

            e.Cancel = true;
            grdataBuyingToolCategory.CancelEdit();
        }

        protected void grdataSupplier_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grdSupplierOnTool = sender as ASPxGridView;
            ASPxComboBox cboCode = grdSupplierOnTool.FindEditRowCellTemplateControl(grdSupplierOnTool.Columns["Code"] as GridViewDataColumn
                        , "cboCodeSupplier") as ASPxComboBox;

            //List<ToolSupplierEntity> table = SupplierEntityList;
            //if (table == null)
            //    table = new List<ToolSupplierEntity>();

            string selectedCode = cboCode.SelectedItem.Value.ToString();
            //ToolSupplierEntity supplier = new ToolSupplierEntity();
            //if (this.Action != "new")
            //{
            //    supplier = this.toolBOL.getSupplierOnTool(selectedCode, this.toolKey);
            //}
            //else
            //{
            //    supplier = toolBOL.getSupplierOnTool(selectedCode);
            //}

            //table.Add(new ToolSupplierEntity(
            //    supplier.ToolSupplierId,
            //    supplier.ToolId,
            //    supplier.SupplierId,
            //    selectedCode,
            //    supplier.Name
            //));

            //SupplierEntityList = table;

            //this.grdataSupplier.DataSource = table;
            grdataSupplier.DataBind();

            e.Cancel = true;
            grdataSupplier.CancelEdit();
        }

        protected void grdataSupplier_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grdSupplierOnTool = sender as ASPxGridView;

            //List<ToolSupplierEntity> table = SupplierEntityList;
            //ToolSupplierEntity rs = table.Find(items => items.Code == e.Keys["Code"].ToString());
            //if (rs != null)
            //{
            //    table.Remove(rs);
            //    SupplierEntityList = table;
            //}
            e.Cancel = true;
        }

        protected void grdataSupplier_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxComboBox cboCode = this.grdataSupplier.FindEditRowCellTemplateControl(grdataSupplier.Columns["Code"] as GridViewDataColumn
                        , "cboCodeSupplier") as ASPxComboBox;
            string selectedCode = cboCode.SelectedItem.Value.ToString();

            //List<ToolSupplierEntity> table = SupplierEntityList;
            //if (table == null)
            //    table = new List<ToolSupplierEntity>();
            //GridViewColumn colCode = grid.Columns["Code"];
            //if (table.Count > 0 && grid.IsNewRowEditing)
            //{
            //    if (table.Find(items => items.Code == selectedCode) != null)
            //    {
                    //e.Errors[colCode] = String.Format("Mã nhóm {0} đã được chọn", selectedCode);
                    e.RowError = String.Format("Mã NCC {0} đã được chọn! Vui lòng chọn NCC khác", selectedCode);
            //    }
            //}
        }

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
        }
    }
}