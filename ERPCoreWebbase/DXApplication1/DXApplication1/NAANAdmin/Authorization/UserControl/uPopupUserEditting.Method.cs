using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Xpo;
using NAS.BO.Nomenclature.Organization;
using NAS.DAL;

namespace WebModule.NAANAdmin.Authorization.UserControl
{
    public partial class uPopupUserEditting
	{
        public void collectData()
        {
            currentPerson = PersonEdittingXDS.Session.GetObjectByKey<Person>(PersonId);
            currentPerson.Code = txtPersonCode.Text.Trim();
            currentPerson.Name = txtPersonName.Text.Trim();
            currentPerson.RowStatus = cboPersonRowStatus.Value != null ? 
                short.Parse(cboPersonRowStatus.Value.ToString()) : Utility.Constant.ROWSTATUS_TEMP;
            if (MODE.Equals("Add"))
                currentPerson.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
        }

        public void resetForm() {
            txtPersonCode.Text= string.Empty;
            txtPersonName.Text = string.Empty;
            cboPersonRowStatus.SelectedIndex = 0;
            txtPersonCode.IsValid = true;
            txtPersonName.IsValid = true;
            cboPersonRowStatus.IsValid = true;
            List<TreeListNode> nodes = getAllDepartmentNode();

            foreach (TreeListNode n in nodes)
            {
                n.Selected = false;
            }

        }

        public void action()
        {
            switch (ACTION)
            {
                case "Add":
                    resetForm();
                    initializeInsertingMode();
                    break;
                case "Edit":
                    resetForm();
                    loadForm();
                    break;
                case "Save":
                    if (!ASPxEdit.AreEditorsValid(popup_PersonEdit))
                        return;
                    collectData();
                    updatePerson(currentPerson);
                    popup_PersonEdit.ShowOnPageLoad = false;
                    grdEmailList.UpdateEdit();
                    break;
                case "ActivateForm":
                    setEnableForForm(true);
                    break;
                default:
                    break;
            }
        }
        
        public void initializeInsertingMode()
        {
            MODE = "Add";
            currentPerson = Person.addDefaultPerson();
            PersonId = currentPerson.PersonId;
            if (currentPerson != null)
            {
                TreeListNode n = trlDepartment.FindNodeByKeyValue(defaultDepartment);
                if (!defaultDepartment.Equals(string.Empty) && n != null)
                    n.Selected = true;
                //DND
                grdEmailList.AddNewRow();
                cpPersonCode.Focus();
                //END DND
                popup_PersonEdit.HeaderText = string.Format("{0} - {1}", "Thông Tin Người Dùng", "Thêm mới");
                popup_PersonEdit.ShowOnPageLoad = true;
                //grdEmailList.AddNewRow();
            }
        }

        public void setEnableForForm(bool isActivated)
        {
            frmPersonEdit.Enabled = isActivated;
            if (isActivated)
            {
                ButtonEditItem.Visible = false;
            }
            else
                ButtonEditItem.Visible = true;

            if (MODE.Equals("Add"))
                ButtonSaveItem.Visible = true;
            else
                ButtonSaveItem.Visible = false;

            if (MODE.Equals("Edit"))
                popup_PersonEdit.HeaderText = string.Format("{0} - {1}", "Thông Tin Người Dùng", currentPerson.Code);
            else
                popup_PersonEdit.HeaderText = string.Format("{0} - {1}", popup_PersonEdit.HeaderText, "Thêm mới");
        }

        public void loadForm() {
            MODE = "Edit";
            PersonEdittingXDS.DataBind();
            frmPersonEdit.DataBind();
            LoginEmailAccountXDS.DataBind();
            grdEmailList.DataBind();
            currentPerson = PersonEdittingXDS.Session.GetObjectByKey<Person>(PersonId);
            txtPersonCode.Text = currentPerson.Code;
            loadDepartmentsOnPerson();
            popup_PersonEdit.HeaderText = string.Format("{0} - {1}", "Thông Tin Người Dùng", currentPerson.Code);
            popup_PersonEdit.ShowOnPageLoad = true;
        }

        public List<TreeListNode> getAllDepartmentNode() {
            List<TreeListNode> nodes = new List<TreeListNode>();
            TreeListNodeIterator iterator = new TreeListNodeIterator(trlDepartment.RootNode);
            while (iterator.Current != null)
            {
                if (iterator.Current != trlDepartment.RootNode)
                {
                    nodes.Add(iterator.Current);
                }
                iterator.GetNext();
            }

            return nodes;
        }

        public void loadDepartmentsOnPerson() {
            Dictionary<Guid, NAS.DAL.Nomenclature.Organization.DepartmentPerson> dpDictation = new Dictionary<Guid, DepartmentPerson>();
            dpDictation = currentPerson.DepartmentPersons.ToDictionary(d => d.DepartmentPersonId);
            List<TreeListNode> nodes = getAllDepartmentNode();

            foreach(TreeListNode n in nodes){
                NAS.DAL.Nomenclature.Organization.Department d = (NAS.DAL.Nomenclature.Organization.Department)n.DataItem;
                bool flg = false;
                foreach (DepartmentPerson dp in d.DepartmentPersons)
                    if (dpDictation.ContainsKey(dp.DepartmentPersonId) && dp.RowStatus > 0)
                    {
                        flg = true;
                        break;
                    }
                n.Selected = flg;
            }
        }

        public void updatePerson(Person p) {
            List<TreeListNode> nodes = trlDepartment.GetSelectedNodes();
            List<NAS.DAL.Nomenclature.Organization.Department> departmentList = new List<NAS.DAL.Nomenclature.Organization.Department>();
            foreach(TreeListNode n in nodes){
                NAS.DAL.Nomenclature.Organization.Department d = (NAS.DAL.Nomenclature.Organization.Department)n.DataItem;
                departmentList.Add(d);
            }
            DepartmentBO bo = new DepartmentBO();
            bo.updatePerson(session, departmentList, PersonId, p.Code, p.Name, p.RowStatus);
        }

        public bool isDupplicateCode(string code) {
            //collectData();
            if (MODE.Equals("Edit"))
            {
                Person p = PersonEdittingXDS.Session.GetObjectByKey<Person>(PersonId);
                if (p == null)
                    throw new Exception(String.Format("Không tồn tại PersonId {0}", PersonId));
                string oldcode = p.Code;
                if (oldcode.Equals(code))
                    return false;
            }

            return Util.isExistXpoObject<Person>("Code", code,
                Utility.Constant.ROWSTATUS_INACTIVE,
                Utility.Constant.ROWSTATUS_ACTIVE,
                Utility.Constant.ROWSTATUS_DEFAULT);
        }
	}
}