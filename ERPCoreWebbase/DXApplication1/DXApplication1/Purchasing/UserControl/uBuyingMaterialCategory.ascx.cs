using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.PurchasingBLO;
//using BLL.BO.Purchasing;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxEditors;

namespace DXApplication1.Purchasing.UserControl
{
    public partial class uBuyingMaterialCategory : System.Web.UI.UserControl
    {
        public void initializeInsertingMode()
        {
            Mode = "add";
            //30/09/2013 Duc.Vo ADD-START (setting Mode to Client)
            formMaterialCategoryEdit.JSProperties.Add("cpMode", Mode);
            //30/09/2013 Duc.Vo ADD-END
            //categoryBLO.insertDefaultCategory(KeyValue);
            formMaterialCategoryEdit.HeaderText = "Thông Tin Nhóm Nguyên Vật Liệu - Thêm mới";
            buttonSaveMaterialCategory.Visible = true;
            buttonEditMaterialCategory.Visible = false;
            formMaterialCategoryEdit.ShowOnPageLoad = true;
        }

        //MaterialCategoryBLO categoryBLO = new MaterialCategoryBLO();

        //private BuyingMaterialCategoryEntity categoryEntity = new BuyingMaterialCategoryEntity();

        //public BuyingMaterialCategoryEntity FirstCategoryEntity
        //{
        //    get
        //    {
        //        BuyingMaterialCategoryEntity o = Session["FirstCategoryEntity"] as BuyingMaterialCategoryEntity;
        //        if (o == null)
        //            o = new BuyingMaterialCategoryEntity();
        //        return o;
        //    }

        //    set
        //    {
        //        Session["FirstCategoryEntity"] = value;
        //    }
        //}

        //public BuyingMaterialCategoryEntity CategoryEntity
        //{
        //    get
        //    {
        //        return categoryEntity;
        //    }
        //    set
        //    {
        //        categoryEntity = value;
        //    }
        //}

        //private BuyingMaterialCategoryPropertyEntity propertyEntity = new BuyingMaterialCategoryPropertyEntity();

        //public BuyingMaterialCategoryPropertyEntity PropertyEntity
        //{
        //    get
        //    {
        //        return propertyEntity;
        //    }
        //    set
        //    {
        //        propertyEntity = value;
        //    }
        //}

        public ASPxHtmlEditor HtmlEditDescription
        {
            get
            {
                ASPxHtmlEditor editDescription = navBarMaterialCategoryDetail.Groups[0].FindControl("htmlEditDescription")
                                                as ASPxHtmlEditor;
                return editDescription;
            }
        }

        public ASPxButton buttonEditMaterialCategory
        {
            get
            {
                ASPxButton button = formMaterialCategoryEdit.FindControl("buttonEditMaterialCategory") as ASPxButton;
                return button;
            }
        }

        public ASPxButton buttonSaveMaterialCategory
        {
            get
            {
                ASPxButton button = formMaterialCategoryEdit.FindControl("buttonSaveMaterialCategory") as ASPxButton;
                return button;
            }
        }

        public string Mode
        {
            get
            {
                if (hiddenModeCategory.Contains("Mode"))
                    return hiddenModeCategory["Mode"].ToString();
                return null;
            }
            set
            {
                if (hiddenModeCategory.Contains("Mode"))
                    hiddenModeCategory.Set("Mode", value);
                else
                    hiddenModeCategory.Add("Mode", value);
            }
        }

        public Guid KeyValue
        {
            get
            {
                if (hiddenModeCategory.Contains("KeyValue"))
                    return (Guid)hiddenModeCategory["KeyValue"];
                return new Guid("");
            }

            set
            {
                if (hiddenModeCategory.Contains("KeyValue"))
                    hiddenModeCategory.Set("KeyValue", value);
                else
                    hiddenModeCategory.Add("KeyValue", value);
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool validateData()
        {
            collectData();
            switch (Mode)
            {
                case "edit":
                    //if (!FirstCategoryEntity.Code.Equals(categoryEntity.Code) && !categoryBLO.isDupplicateCode(categoryEntity))
                    //{
                    //    txtCode.IsValid = false;
                    //    txtCode.ErrorText = String.Format("Mã nhóm nguyên vật liệu '{0}' đã tồn tại", txtCode.Text);
                    //    return false;
                    //}
                    //else
                    //{
                    //    txtCode.IsValid = true;
                    //    txtCode.ErrorText = String.Empty;
                    //}

                    break;

                case "add":
                    //if (!categoryBLO.isDupplicateCode(categoryEntity))
                    //{
                    //    txtCode.IsValid = false;
                    //    txtCode.ErrorText = String.Format("Mã nhóm nguyên vật liệu '{0}' đã tồn tại", txtCode.Text);
                    //    return false;
                    //}
                    //else
                    //{
                    //    txtCode.IsValid = true;
                    //    txtCode.ErrorText = String.Empty;
                    //}

                    break;

                default:
                    break;
            }

            return true;
        }

        public void collectData()
        {
            //30/09/2013 Duc.Vo-START
            //categoryEntity.BuyingMaterialCategoryId = KeyValue;
            //30/09/2013 Duc.Vo-END
            //categoryEntity.RowStatus = cboRowStatus.SelectedItem == null ? 'A': cboRowStatus.SelectedItem.Value.ToString()[0];
            //categoryEntity.Code = txtCode.Text;
            //propertyEntity.Name = txtName.Text;
            //propertyEntity.Description = HtmlEditDescription.Html;
            //propertyEntity.Language = "VN";
        }

        public void loadForm(string id, object categoryBLO)
        {
            setEnableForForm(false);
            /*Load data to usercontrol*/
            formMaterialCategoryEdit.ShowOnPageLoad = true;
            //BuyingMaterialCategoryEntity category;
            //BuyingMaterialCategoryPropertyEntity property;
            Guid guid = new Guid(id);
            //FormCommonMaterialCategoryEdit.DataSource = categoryBLO.getMaterialCategoryByKey(guid, out category, out property);
            FormCommonMaterialCategoryEdit.DataBind();
            //txtCode.Text = category.Code;
            //HtmlEditDescription.Html = property.Description;
            //FirstCategoryEntity = category;
            //formMaterialCategoryEdit.HeaderText = "Thông Tin Nhóm Nguyên Vật Liệu - Mã số: " + category.Code;
            /*setting mode and key value*/
            Mode = "edit";
            KeyValue = guid;
        }

        public void resetForm()
        {
            //FirstCategoryEntity = null;
            txtCode.Text = "";
            txtName.Text = "";
            cboRowStatus.SelectedIndex = 0;
            HtmlEditDescription.Html = "";

            txtCode.IsValid = true;
            txtName.IsValid = true;
            cboRowStatus.IsValid = true;
            KeyValue = Guid.NewGuid();
        }

        public void Action(string action, string id)
        {
            switch (action)
            {
                case "AddCategory":
                    resetForm();
                    initializeInsertingMode();
                    break;
                case "EditCategory":
                    resetForm();
                    //loadForm(id, categoryBLO);
                    break;
                case "DeleteCategory":
                    Guid guid = new Guid(id);
                    //categoryBLO.deleteMaterialCategory(guid);
                    break;
                case "SaveCategory":
                    if (!validateData())
                        return;
                    collectData();
                    //if (Mode == "add")
                    //    categoryBLO.updateMaterialCategory(categoryEntity, propertyEntity);
                    resetForm();
                    formMaterialCategoryEdit.ShowOnPageLoad = false;
                    break;
                case "CheckCategory":
                case "updateByLostFocus":
                    if (!validateData())
                        return;
                    collectData();
                    //categoryBLO.updateMaterialCategory(categoryEntity, propertyEntity);
                    //FirstCategoryEntity = categoryEntity;
                    break;
                case "ActivateForm":
                    setEnableForForm(true);
                    collectData();
                    //formMaterialCategoryEdit.HeaderText = "Thông Tin Nguyên Vật Liệu - Mã số: " + CategoryEntity.Code;
                    break;
                default:
                    break;
            }
        }

        public void setEnableForForm(bool isActivated)
        {
            FormCommonMaterialCategoryEdit.Enabled = isActivated;
            //HtmlEditDescription.Enabled = isActivated;
            if (isActivated)
            {
                buttonEditMaterialCategory.Visible = false;
            }
            else
                buttonEditMaterialCategory.Visible = true;

            buttonSaveMaterialCategory.Visible = false;
        }

        protected void cpLineMaterialCategory_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string action = para[0];
            string id = para.Count<string>() == 2 ? id = para[1] : id = "";
            if (id != "")
                KeyValue = new Guid(id);
            Action(action, id);
            pcMaterialCategory.ActiveTabIndex = 0;
        }

        protected void cpCheckMaterialCategoryCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string action = para[0];
            Action(action, "");
        }
    }
}