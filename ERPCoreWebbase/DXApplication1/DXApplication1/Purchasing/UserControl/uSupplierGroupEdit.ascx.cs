using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.PurchasingBLO;
//using BLL.BO.Purchasing;
using DevExpress.Web.ASPxHtmlEditor;

namespace ERPCore.Purchasing.UserControl
{
    public partial class uSupplierGroupEdit : System.Web.UI.UserControl
    {
        //SupplierCategoryBLO categoryBLO = new SupplierCategoryBLO();

        //private SupplierCategoryEntity categoryEntity = new SupplierCategoryEntity();

        //public SupplierCategoryEntity CategoryEntity
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

        //public SupplierCategoryEntity FirstCategoryEntity
        //{
        //    get
        //    {
        //        SupplierCategoryEntity o = Session["FirstCategoryEntity"] as SupplierCategoryEntity;
        //        if (o == null)
        //            o = new SupplierCategoryEntity();
        //        return o;
        //    }

        //    set
        //    {
        //        Session["FirstCategoryEntity"] = value;
        //    }
        //}

        //private SupplierCategoryPropertyEntity propertyEntity = new SupplierCategoryPropertyEntity();

        //public SupplierCategoryPropertyEntity PropertyEntity
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
                ASPxHtmlEditor editDescription = navBarSupplierCategoryDetail.Groups[0].FindControl("htmlEditDescription")
                                                as ASPxHtmlEditor;
                return editDescription;
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
            //collectData();
            //switch (Mode)
            //{
            //    case "edit":
            //        if (!FirstCategoryEntity.Code.Equals(categoryEntity.Code) && !categoryBLO.isDupplicateCode(categoryEntity))
            //        {
            //            txtCodeCategory.IsValid = false;
            //            txtCodeCategory.ErrorText = String.Format("Mã nhóm nhà cung cấp '{0}' đã tồn tại", txtCodeCategory.Text);
            //            return false;
            //        }
            //        else
            //        {
            //            txtCodeCategory.IsValid = true;
            //            txtCodeCategory.ErrorText = String.Empty;
            //        }

            //        break;

            //    case "add":
            //        if (!categoryBLO.isDupplicateCode(categoryEntity))
            //        {
            //            txtCodeCategory.IsValid = false;
            //            txtCodeCategory.ErrorText = String.Format("Mã nhóm nhà cung cấp '{0}' đã tồn tại", txtCodeCategory.Text);
            //            return false;
            //        }
            //        else
            //        {
            //            txtCodeCategory.IsValid = true;
            //            txtCodeCategory.ErrorText = String.Empty;
            //        }

            //        break;

            //    default:
            //        break;
            //}

            return true;
        }

        public void collectData()
        {
            //categoryEntity.RowStatus = cboRowStatusCategory.SelectedItem.Value.ToString()[0];
            //categoryEntity.Code = txtCodeCategory.Text;
            //propertyEntity.Name = txtNameCategory.Text;
            //propertyEntity.Description = HtmlEditDescription.Html;
            //propertyEntity.Language = "VN";
        }

        //public void loadForm(string id, SupplierCategoryBLO categoryBLO)
        //{
        //    ///*Load data to usercontrol*/
        //    //SupplierCategoryEntity category;
        //    //SupplierCategoryPropertyEntity property;
        //    //Guid guid = new Guid(id);
        //    //formEditCommonCategory.DataSource = categoryBLO.getSupplierCategoryByKey(guid, out category, out property);
        //    //FirstCategoryEntity = category;
        //    //txtCodeCategory.Text = category.Code;
        //    //formEditCommonCategory.DataBind();
        //    //HtmlEditDescription.Html = property.Description;
        //    //formSupplierGroupEdit.HeaderText = "Thông Tin Nhóm Nhà Cung Cấp - Mã số: " + category.Code;
        //    //formSupplierGroupEdit.ShowOnPageLoad = true;
        //    ///*setting mode and key value*/
        //    //Mode = "edit";
        //    //KeyValue = guid;
        //}

        public void resetForm()
        {
            //txtCodeCategory.Text = "";
            //txtNameCategory.Text = "";
            //cboRowStatusCategory.SelectedIndex = 0;
            //HtmlEditDescription.Html = "";
            //FirstCategoryEntity = null;

            //txtCodeCategory.IsValid = true;
            //txtNameCategory.IsValid = true;
        }

        public void Action(string action, string id)
        {
            
            if (action == "AddCategory")
            {
                Mode = "add";
                resetForm();
                formSupplierGroupEdit.HeaderText = "Thông Tin Nhóm Nhà Cung Cấp - Thêm mới";
                formSupplierGroupEdit.ShowOnPageLoad = true;
            }

            //if (action == "EditCategory")
            //{
            //    resetForm();
            //    loadForm(id, categoryBLO);
            //}

            //if (action == "DeleteCategory")
            //{
            //    Guid guid = new Guid(id);
            //    categoryBLO.deleteSupplierCategory(guid);
            //}

            //if (action == "SaveCategory")
            //{
            //    if (!validateData())
            //        return;
            //    collectData();
            //    if (Mode == "add")
            //        categoryBLO.insertSupplierCategory(categoryEntity, propertyEntity);
            //    else
            //    {
            //        categoryEntity.SupplierCategoryId = KeyValue;
            //        categoryBLO.updateSupplierCategory(categoryEntity, propertyEntity);
            //    }
            //    formSupplierGroupEdit.ShowOnPageLoad = false;
            //    resetForm();
            //}

            //if (action == "CheckSupplierCategory")
            //{
            //    if (!validateData())
            //        return;
            //}
        }

        protected void cpLineSupplierGroup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string action = para[0];
            string id = para.Count<string>() == 2 ? id = para[1] : id = "";
            Action(action, id);
            pcSupplierCategory.ActiveTabIndex = 0;
        }

        protected void cpCheckSupplierCategoryCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string action = para[0];
            Action(action, "");
        }

    }
}