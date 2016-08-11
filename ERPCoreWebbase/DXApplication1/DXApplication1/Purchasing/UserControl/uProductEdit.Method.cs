using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;

namespace WebModule.Purchasing.UserControl
{
    public partial class uProductEdit
    {
        public void action()
        {
            switch (ACTION)
            {
                case "Add":
                    resetForm();
                    initializeInsertingMode();
                    setEnableForForm(true);
                    objectCustomFieldItems();
                    break;
                case "Edit":
                    loadForm();
                    objectCustomFieldItems();
                    setEnableForForm(true);
                    break;
                case "Delete":
                    if (itemBO.checkIsExistInBillItem(session, ItemId))
                        throw new Exception("Đối tượng này đã tồn tại trong phiếu mua hoặc phiếu bán nên không thể xóa");
                    if (itemBO.checkIsItemInInventory(session, ItemId))
                        throw new Exception("Đối tượng đã tồn tại trong kho hàng nên không thể chỉnh sửa hoặc xóa đơn vị tính");
                    itemBO.deleteLogicItem(session, ItemId);
                    break;
                case "Save":
                    if (!ASPxEdit.AreEditorsValid(pcProduct))
                    {
                        pcProduct.ActiveTabIndex = 0;
                        isValidForm = false;
                        objectCustomFieldItems();
                        setEnableForForm(true);
                        return;
                    }
                    collectData();
                    
                    try
                    {
                        string msg = string.Empty;
                        if (!validateDupplicateCode(out msg))
                        {
                            isValidForm = false;
                            return;
                        }
                        validatedOnSave();
                        OnSave();
                        objectCustomFieldItems();
                        MODE = "Edit";
                        setEnableForForm(true);
                    }
                    catch (Exception e)
                    {
                        isValidForm = false;
                        throw e;
                    }
                    
                    //resetForm();
                    //formProductEdit.ShowOnPageLoad = false;
                    
                    break;
                case "ActivateForm":
                    setEnableForForm(true);
                    break;
                default:
                    break;
            }
        }

        public void resetForm()
        {
            ItemId = Guid.Empty;
            txtProductCode.Text = "";
            txtProductName.Text = "";
            cboProductManufacturer.Value = Guid.Empty;
            cboProductManufacturer.Text = string.Empty;
            cboProductRowStatus.Text = string.Empty;
            //cboProductRowStatus.SelectedIndex = 0;
            cboProductManufacturer.SelectedIndex = -1;
            HtmlEditDescription.Html = "";
            txtProductCode.IsValid = true;
            txtProductName.IsValid = true;
            cboProductRowStatus.IsValid = true;
            cboProductManufacturer.IsValid = true;
            MODE = "";
            foreach (ListEditItem l in lbType.Items)
            {
                l.Selected = false;
            }
            pcProduct.ActiveTabIndex = 0;
            grdProductSupplier.CancelEdit();
            treelstProductUnits.CancelEdit();
        }

        public void collectData()
        {
            currentItem = session.GetObjectByKey<Item>(ItemId);
            if (currentItem != null)
            {
                currentItem.Code = txtProductCode.Text;
                currentItem.Name = txtProductName.Text;
                currentItem.Description = HtmlEditDescription.Html;
                currentItem.RowStatus = cboProductRowStatus.SelectedItem != null ?
                                        short.Parse(cboProductRowStatus.SelectedItem.Value.ToString()) : short.Parse("1");

                //if (cboProductManufacturer.SelectedIndex == -1) {
                //    throw new Exception("Vui lòng chọn nhà sản xuất");
                //}
                try
                {

                    if (cboProductManufacturer.SelectedItem != null)
                    {
                        ManufacturerOrg manufacturer = session.GetObjectByKey<ManufacturerOrg>(
                            Guid.Parse(
                                cboProductManufacturer.SelectedItem.GetValue("OrganizationId").ToString()
                                ));
                        if (manufacturer != null)
                            currentItem.ManufacturerOrgId = manufacturer;
                    }
                }
                catch (Exception)
                { }

                selectedObjectTypeId = new List<Guid>();
                foreach (ListEditItem l in lbType.Items)
                {
                    if (l.Selected) 
                        selectedObjectTypeId.Add(Guid.Parse(l.Value.ToString()));
                }
            }
        }

        public void initializeInsertingMode()
        {
            MODE = "Add";
            currentItem = itemBO.addDefaultItem(session, Item.TYPEOFITEM.PRODUCT);
            if (currentItem != null)
            {
                ItemId = currentItem.ItemId;
                grdProductSupplier.AddNewRow();
                ASPxGridTax.AddNewRow();
                treelstProductUnits.StartEditNewNode(treelstProductUnits.RootNode.Key);
                formProductEdit.ShowOnPageLoad = true;
            }
        }

        public void loadForm()
        {
            txtProductCode.IsValid = true;
            txtProductName.IsValid = true;
            cboProductRowStatus.IsValid = true;
            pcProduct.ActiveTabIndex = 0;
            MODE = "Edit";
            currentItem = session.GetObjectByKey<Item>(ItemId);
            if (currentItem != null)
            {
                txtProductCode.Text = currentItem.Code;
                txtProductName.Text = currentItem.Name;
                HtmlEditDescription.Html = currentItem.Description;
                try
                {
                    cboProductManufacturer.Value = currentItem.ManufacturerOrgId.OrganizationId;
                    cboProductManufacturer.Text = currentItem.ManufacturerOrgId.Name;
                }
                catch (Exception e)
                {
                    //Issue dropdownlist ---START
                    //throw new Exception(String.Format("Kiểm tra lại OrganizationId {0}", currentItem.ManufacturerOrgId.OrganizationId));
                    //Issue dropdownlist ---END
                }
                formProductEdit.ShowOnPageLoad = true;
            } else
                throw new Exception(string.Format("Not exist ItemId {0} in Item Table", ItemId));

            ASPxGridTax.DataBind();
            loadLbType();
            grdProductSupplier.CancelEdit();
            treelstProductUnits.CancelEdit();
            formProductEdit.HeaderText = string.Format("{0} {1}", formProductEdit.HeaderText, currentItem.Code);
        }

        public void setEnableForForm(bool isActivated)
        {
            frmlyItemEdit.Enabled = isActivated;
            treelstProductUnits.Columns["Action"].Visible = isActivated;
            grdProductSupplier.Columns["Action"].Visible = isActivated;
            treelstProductUnits.SettingsEditing.AllowNodeDragDrop = isActivated;
            HtmlEditDescription.Settings.AllowHtmlView = isActivated;
            HtmlEditDescription.Settings.AllowPreview = !isActivated;
            HtmlEditDescription.Settings.AllowDesignView = isActivated;
            //if (isActivated)
            //{
            //    //ButtonEditItem.Visible = false;
            //}
            //else
            //    //ButtonEditItem.Visible = true;

            //if (MODE.Equals("Edit") && ButtonEditItem.Visible)
            //    ButtonSaveItem.Visible = false;
            //else
            //    ButtonSaveItem.Visible = true;

            //ButtonEditItem.Visible = false;

            if (MODE.Equals("Edit"))
                formProductEdit.HeaderText = string.Format("{0} {1}", "Thông Tin Đối Tượng", currentItem.Code);
            else
                formProductEdit.HeaderText = string.Format("{0} {1}", formProductEdit.HeaderText, "Thêm mới");
        }

        public bool validateDupplicateCode(out string msg)
        {
            msg = "";
            if (ItemId.Equals(Guid.Empty))
                return true;
            currentItem = session.GetObjectByKey<Item>(ItemId);

            if (currentItem == null)
                throw new Exception(String.Format("Not existing data with ItemId {0}", ItemId));
            string inputCode = txtProductCode.Text.Trim();
            bool rs = itemBO.checkIsDupplicateCode(session, inputCode);
            switch (MODE)
            {
                case "Edit":
                    if (!currentItem.Code.Equals(inputCode))
                    {
                        if (!rs)
                        {
                            msg = String.Format("Mã hàng hóa '{0}' đã tồn tại", txtProductCode.Text.Trim());
                            return false;
                        }
                    }
                    break;

                case "Add":
                    if (!rs)
                    {
                        msg = String.Format("Mã hàng hóa '{0}' đã tồn tại", txtProductCode.Text.Trim());
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }

        public bool isSelectedLbType() {
            // Load listbox phân loại
            foreach (ListEditItem l in lbType.Items)
            {
                if (l.Selected)
                {
                    return true;
                }
            }
            return false;
        }

        public void loadLbType() {
            foreach (ListEditItem l in lbType.Items)
            {
                l.Selected = false;
            }
            // Load listbox phân loại
            foreach (ListEditItem l in lbType.Items)
            {
                Guid key = Guid.Parse(l.GetValue("ObjectTypeId").ToString());
                foreach (ItemCustomType t in currentItem.ItemCustomTypes)
                {
                    if (!t.IsDeleted && key == t.ObjectTypeId.ObjectTypeId)
                    {
                        l.Selected = true;
                        break;
                    }
                }
            }
        }

        public void validatedOnSave() {
            if (!isSelectedLbType())
                throw new Exception(String.Format("Phải chọn ít nhất một phân loại cho đối tượng"));
            
            int cnt = 0;
            
            foreach (ListEditItem l in lbType.Items)
            {
                string name = l.GetValue("Name").ToString();
                if (l.Selected && (name.Equals("SERVICE") || name.Equals("PRODUCT"))) 
                {
                    cnt++;
                }
            }

            if (cnt >= 2)
                throw new Exception("Nếu loại đối tượng là 'dịch vụ' thì không thể chọn tiếp loại 'hàng hóa'");
        }

        public void OnSave() {
            //Issue dropdownlist ---START
            Guid manuOrgId = currentItem.ManufacturerOrgId != null ? currentItem.ManufacturerOrgId.OrganizationId : Guid.Empty;
            //Issue dropdownlist ---END
            itemBO.updateAllCommonInfoOfItem(session, ItemId, currentItem.Code, currentItem.Name, manuOrgId, selectedObjectTypeId);
        }

        public void objectCustomFieldItems()
        {
            CustomFieldObjects = itemBO.getAllCustomFieldObjects(session, ItemId);
            if (CustomFieldObjects == null || CustomFieldObjects.Count == 0)
            {
                //TittleProductCustomFieldGrid.Visible =
                NASProductCustomFieldDataGridView.Visible =
                TittleToolCustomFieldGrid.Visible =
                NASToolCustomFieldDataGridView.Visible =
                TittleServiceCustomFieldGrid.Visible =
                NASServiceCustomFieldDataGridView.Visible =
                TittleMaterialCustomFieldGrid.Visible =
                NASMaterialCustomFieldDataGridView.Visible = false;
                TittleProductCustomFieldGrid.Text = "Để sử dụng chức năng cấu hình động vui lòng cập nhật phân loại đối tượng ở Tab-Thông tin chung";
                TittleProductCustomFieldGrid.Visible = true;
                return;
            }
            else {
                TittleProductCustomFieldGrid.Text = "Cấu hình thuộc tính cho hàng hóa";
            }

            NAS.DAL.CMS.ObjectDocument.Object product = null;
            NAS.DAL.CMS.ObjectDocument.Object tool = null;
            NAS.DAL.CMS.ObjectDocument.Object service = null;
            NAS.DAL.CMS.ObjectDocument.Object material = null;
            NAS.DAL.CMS.ObjectDocument.Object seflProduction = null;
            NAS.DAL.CMS.ObjectDocument.Object fixedAssest = null;
            bool flgProduct = false;
            bool flgTool = false;
            bool flgService = false;
            bool flgMaterial = false;
            bool flgSelfProduction= false;
            bool flgFixedAssest = false;

            foreach (NAS.DAL.CMS.ObjectDocument.Object o in CustomFieldObjects)
            {
                if (o.ObjectTypeId.Name.Equals("PRODUCT"))
                {
                    product = o;
                    flgProduct = true;
                }

                if (o.ObjectTypeId.Name.Equals("TOOL"))
                {
                    flgTool = true;
                    tool = o;
                }

                if (o.ObjectTypeId.Name.Equals("SERVICE"))
                {
                    service = o;
                    flgService = true;
                }

                if (o.ObjectTypeId.Name.Equals("MATERIAL"))
                {
                    material = o;
                    flgMaterial = true;
                }

                if (o.ObjectTypeId.Name.Equals("FIXED_ASSETS"))
                {
                    fixedAssest = o;
                    flgFixedAssest = true;
                }

                if (o.ObjectTypeId.Name.Equals("SELF_PRODUCTION"))
                {
                    seflProduction = o;
                    flgSelfProduction = true;
                }
            }

            if (flgProduct)
            {
                NASProductCustomFieldDataGridView.CMSObjectId = product.ObjectId;
                NASProductCustomFieldDataGridView.DataBind();
            }

            TittleProductCustomFieldGrid.Visible = flgProduct;
            NASProductCustomFieldDataGridView.Visible = flgProduct;

            if (flgTool)
            {
                NASToolCustomFieldDataGridView.CMSObjectId = tool.ObjectId;
                NASToolCustomFieldDataGridView.DataBind();
            }
            TittleToolCustomFieldGrid.Visible = flgTool;
            NASToolCustomFieldDataGridView.Visible = flgTool;


            if (flgService)
            {
                NASServiceCustomFieldDataGridView.CMSObjectId = service.ObjectId;
                NASServiceCustomFieldDataGridView.DataBind();
            }
            TittleServiceCustomFieldGrid.Visible = flgService;
            NASServiceCustomFieldDataGridView.Visible = flgService;

            if (flgMaterial)
            {
                NASMaterialCustomFieldDataGridView.CMSObjectId = material.ObjectId;
                NASMaterialCustomFieldDataGridView.DataBind();
            }

            TittleMaterialCustomFieldGrid.Visible = flgMaterial;
            NASMaterialCustomFieldDataGridView.Visible = flgMaterial;

            if (flgFixedAssest)
            {
                NASFixedAssestCustomFieldDataGridView.CMSObjectId = fixedAssest.ObjectId;
                NASFixedAssestCustomFieldDataGridView.DataBind();
            }

            TittleFixedAssestCustomFieldGrid.Visible = flgFixedAssest;
            NASFixedAssestCustomFieldDataGridView.Visible = flgFixedAssest;

            if (flgSelfProduction)
            {
                NASSelfProductionCustomFieldDataGridView.CMSObjectId = seflProduction.ObjectId;
                NASSelfProductionCustomFieldDataGridView.DataBind();
            }

            TittleSelfProductionCustomFieldGrid.Visible = flgSelfProduction;
            NASSelfProductionCustomFieldDataGridView.Visible = flgSelfProduction;
        }
    }
}