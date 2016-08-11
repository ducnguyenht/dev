using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Data.Filtering;
using NAS.DAL;
using DevExpress.Web.ASPxEditors;
using NAS.BO.Nomenclature.Items;

namespace WebModule.Purchasing.UserControl
{
    public partial class uUnitEdit
    {
        public void Action()
        {
            switch (ACTION)
            {
                case "Add":
                    resetForm();
                    initializeInsertingMode();
                    setEnableForForm(true);
                    break;
                case "Edit":
                    resetForm();
                    loadForm();
                    setEnableForForm(false);
                    break;
                case "Delete":
                    if (unitBO.checkIsExistInItemUnit(session, UnitId))
                        throw new Exception("Quy cách này đã được sử dụng trong cấu hình hàng hóa nên không thể xóa!");

                    collectData();
                    currentUnit.RowStatus = -1;
                    UnitEdittingXDS.Session.Save(currentUnit);
                    break;
                case "Save":
                    ///Issue ERP-956-START
                    if (MODE.Equals("Edit"))
                    {
                        if (unitBO.checkIsExistInItemUnit(session, UnitId))
                            throw new Exception("Quy cách này đã được sử dụng trong cấu hình hàng hóa nên không thể sửa!");
                    }
                    ///Issue ERP-956-END
                    if (!ASPxEdit.AreEditorsValid(pcUnit))
                        return;
                    collectData();
                    UnitEdittingXDS.Session.Save(currentUnit);
                    resetForm();
                    formUnitEdit.ShowOnPageLoad = false;
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
            txtUnitEditCode.Text = "";
            txtUnitEditName.Text = "";
            cboUnitEditRowStatus.SelectedIndex = 0;
            cboUnitEditRowStatus.SelectedIndex = 0;
            HtmlEditDescription.Html = "";
            txtUnitEditCode.IsValid = true;
            txtUnitEditName.IsValid = true;
            cboUnitEditRowStatus.IsValid = true;
            pcUnit.ActiveTabIndex = 0;
        }

        public void collectData()
        {
            currentUnit = UnitEdittingXDS.Session.GetObjectByKey<Unit>(UnitId);
            if (currentUnit != null)
            {
                currentUnit.Code = txtUnitEditCode.Text;
                currentUnit.Name = txtUnitEditName.Text;
                currentUnit.Description = HtmlEditDescription.Html;
                currentUnit.RowStatus = cboUnitEditRowStatus.SelectedItem != null ?
                                        short.Parse(cboUnitEditRowStatus.SelectedItem.Value.ToString()) : short.Parse("1");
            }
        }

        public void initializeInsertingMode()
        {
            MODE = "Add";
            currentUnit = Unit.addDefaultUnitBO();
            UnitId = currentUnit.UnitId;
            if (currentUnit != null)
            {
                UnitEdittingXDS.Criteria = (new BinaryOperator("UnitId", UnitId)).ToString();
                formUnitEdit.ShowOnPageLoad = true;
            }
        }

        public void loadForm()
        {
            MODE = "Edit";
            UnitEdittingXDS.Criteria = (new BinaryOperator("UnitId", UnitId)).ToString();
            frmlytUnitInfo.DataBind();
            currentUnit = UnitEdittingXDS.Session.GetObjectByKey<Unit>(UnitId);
            if (currentUnit != null)
            {
                txtUnitEditCode.Text = currentUnit.Code;
                HtmlEditDescription.Html = currentUnit.Description;
                formUnitEdit.ShowOnPageLoad = true;
                formUnitEdit.HeaderText = string.Format("{0} {1}", formUnitEdit.HeaderText, currentUnit.Code);
            }
        }

        public void setEnableForForm(bool isActivated)
        {
            frmlytUnitInfo.Enabled = isActivated;
            HtmlEditDescription.Settings.AllowHtmlView = isActivated;
            HtmlEditDescription.Settings.AllowPreview = !isActivated;
            HtmlEditDescription.Settings.AllowDesignView = isActivated;
            if (isActivated)
            {
                ButtonEditUnit.Visible = false;
            }
            else
                ButtonEditUnit.Visible = true;

            if (MODE.Equals("Edit") && ButtonEditUnit.Visible)
                ButtonSaveUnit.Visible = false;
            else
                ButtonSaveUnit.Visible = true;

            if (MODE.Equals("Edit"))
                formUnitEdit.HeaderText = string.Format("{0} {1}", "Thông Tin Quy Cách - ", currentUnit.Code);
            else
                formUnitEdit.HeaderText = string.Format("{0} {1}", formUnitEdit.HeaderText, "Thêm mới");
        }

        public bool validateDupplicateCode(out string msg)
        {
            msg = "";
            if (UnitId.Equals(Guid.Empty))
                return true;
            currentUnit = UnitEdittingXDS.Session.GetObjectByKey<Unit>(UnitId);
            if (currentUnit == null)
                throw new Exception(String.Format("Not existing data with UnitId {0}", UnitId));
            string inputCode = txtUnitEditCode.Text.Trim();
            bool flg = Util.isExistXpoObject<Unit>("Code", inputCode, Utility.Constant.ROWSTATUS_INACTIVE, Utility.Constant.ROWSTATUS_ACTIVE, Utility.Constant.ROWSTATUS_DEFAULT); 
            switch (MODE)
            {
                case "Edit":
                    if (!currentUnit.Code.Equals(inputCode))
                    {
                        
                        if (flg) {
                            msg = String.Format("Mã quy cách '{0}' đã tồn tại", txtUnitEditCode.Text.Trim());
                            return false;
                        } 
                    }
                    break;

                case "Add":
                    if (flg)
                    {
                        msg = String.Format("Mã quy cách '{0}' đã tồn tại", txtUnitEditCode.Text.Trim());
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }

    }
}