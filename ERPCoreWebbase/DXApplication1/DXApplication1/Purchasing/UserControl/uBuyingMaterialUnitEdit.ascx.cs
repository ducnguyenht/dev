using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.PurchasingBLO;
//using BLL.BO.Purchasing;
using DevExpress.Web.ASPxHtmlEditor;

namespace DXApplication1.Purchasing.UserControl
{
    public partial class uBuyingMaterialUnitEdit : System.Web.UI.UserControl
    {
        //MaterialUnitBLO materialUnitBLO = new MaterialUnitBLO();

        //private MaterialUnitEntity unitEntity = new MaterialUnitEntity();

        //public MaterialUnitEntity UnitEntity
        //{
        //    get
        //    {
        //        return unitEntity;
        //    }
        //    set
        //    {
        //        unitEntity = value;
        //    }
        //}

        //public MaterialUnitEntity FirstUnitEntity
        //{
        //    get
        //    {
        //        MaterialUnitEntity o = Session["FirstUnitEntity"] as MaterialUnitEntity;
        //        if (o == null)
        //            o = new MaterialUnitEntity();
        //        return o;
        //    }

        //    set
        //    {
        //        Session["FirstUnitEntity"] = value;
        //    }
        //}

        //private MaterialUnitPropertyEntity propertyEntity = new MaterialUnitPropertyEntity();

        //public MaterialUnitPropertyEntity PropertyEntity
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
                ASPxHtmlEditor editDescription = navBarMaterialUnitDetail.Groups[0].FindControl("htmlEditDescription")
                                                as ASPxHtmlEditor;
                return editDescription;
            }
        }

        public string Mode
        {
            get
            {
                if (hiddenMode.Contains("Mode"))
                    return hiddenMode["Mode"].ToString();
                return null;
            }
            set
            {
                if (hiddenMode.Contains("Mode"))
                    hiddenMode.Set("Mode", value);
                else
                    hiddenMode.Add("Mode", value);
            }
        }

        public Guid KeyValue
        {
            get
            {
                if (hiddenMode.Contains("KeyValue"))
                    return (Guid)hiddenMode["KeyValue"];
                return new Guid("");
            }

            set
            {
                if (hiddenMode.Contains("KeyValue"))
                    hiddenMode.Set("KeyValue", value);
                else
                    hiddenMode.Add("KeyValue", value);
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cpLineMaterialUnit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string action = para[0];
            string id = para.Count<string>() == 2 ? id = para[1] : id = "";
            Action(action, id);
            pcMaterialUnitInfo.ActiveTabIndex = 0;
        }

        public void collectData()
        {
            //unitEntity.RowStatus = cboRowStatus.SelectedItem == null ? 'A' : cboRowStatus.SelectedItem.Value.ToString()[0];
            //unitEntity.Code = txtCode.Text;
            //propertyEntity.Name = txtName.Text;
            //propertyEntity.Description = HtmlEditDescription.Html;
            //propertyEntity.Language = "VN";
        }

        public void loadForm(string id, object materialBLO)
        {
            /*Load data to form usercontrol*/

            //MaterialUnitEntity unitE;
            //MaterialUnitPropertyEntity propertyE;
            Guid guid = new Guid(id);
            //formCommonEditMaterialUnit.DataSource = materialBLO.getMaterialUnitByKey(guid, out unitE, out propertyE);
            //FirstUnitEntity = unitE;
            formCommonEditMaterialUnit.DataBind();
            //txtCode.Text = unitE.Code;
            //HtmlEditDescription.Html = propertyE.Description;
            /*setting mode and key value*/
            Mode = "edit";
            KeyValue = guid;
            //formMaterialUnitEdit.HeaderText = "Thông Tin Đơn Vị Tính Nguyên Vật Liệu - Mã số: " + unitE.Code;
            formMaterialUnitEdit.ShowOnPageLoad = true;
        }

        public bool validateData()
        {
            collectData();
            switch (Mode)
            {
                case "edit":
                    if (/*!FirstUnitEntity.Code.Equals(unitEntity.Code) && !materialUnitBLO.isDupplicateCode(unitEntity)*/true)
                    {
                        txtCode.IsValid = false;
                        txtCode.ErrorText = String.Format("Mã nhóm nguyên vật liệu '{0}' đã tồn tại", txtCode.Text);
                        return false;
                    }
                    else
                    {
                        txtCode.IsValid = true;
                        txtCode.ErrorText = String.Empty;
                    }

                    break;

                case "add":
                    if (/*!materialUnitBLO.isDupplicateCode(unitEntity)*/ true)
                    {
                        txtCode.IsValid = false;
                        txtCode.ErrorText = String.Format("Mã nhóm nguyên vật liệu '{0}' đã tồn tại", txtCode.Text);
                        return false;
                    }
                    else
                    {
                        txtCode.IsValid = true;
                        txtCode.ErrorText = String.Empty;
                    }

                    break;

                default:
                    break;
            }

            return true;
        }

        public void resetForm()
        {
            //FirstUnitEntity = null;
            txtCode.Text = "";
            txtName.Text = "";
            txtCode.IsValid = true;
            txtName.IsValid = true;
            cboRowStatus.SelectedIndex = 0;
            cboRowStatus.IsValid = true;
            HtmlEditDescription.Html = "";
        }

        public void Action(string action, string id)
        {
            
            if (action == "AddUnit")
            {
                Mode = "add";
                resetForm();
                formMaterialUnitEdit.HeaderText = "Thông Tin Đơn Vị Tính Nguyên Vật Liệu - Thêm mới";
                formMaterialUnitEdit.ShowOnPageLoad = true;
            }

            if (action == "EditUnit")
            {
                //loadForm(id, materialUnitBLO);
            }

            if (action == "DeleteUnit")
            {
                Guid guid = new Guid(id);
                //materialUnitBLO.deleteMaterialUnit(guid);
            }

            if (action == "SaveUnit")
            {
                if (!validateData())
                    return;
                collectData();
                //if (Mode == "add")
                    //materialUnitBLO.insertMaterialUnit(unitEntity, propertyEntity);
                //else
                //{
                    //unitEntity.MaterialUnitId = KeyValue;
                    //materialUnitBLO.updateMaterialUnit(unitEntity, propertyEntity);
                //}
                formMaterialUnitEdit.ShowOnPageLoad = false;
                resetForm();
            }

            if (action == "CheckCategoryUnit")
            {
                if (!validateData())
                    return;
            }
            
        }

        protected void cpCheckMaterialUnitCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string action = para[0];
            Action(action, "");
        }
    }
}