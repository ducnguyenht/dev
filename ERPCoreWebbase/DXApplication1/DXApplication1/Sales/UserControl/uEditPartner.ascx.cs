using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxGridView;

//using DAL.Purchasing;
//using BLL.SalesBLO;
//using BLL.BO.Sale;
using Utility;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxTabControl;


namespace WebModule.GUI.usercontrol
{
    public partial class uEditPartner : System.Web.UI.UserControl
    {
        Session session;

        protected void Page_Load(object sender, EventArgs e)
        {
            //XPCollection<ViewPartnerPartnerCategory> collection = null;

            //if (!Page.IsPostBack)
            //{

            //}
            //else
            //{
            //    collection = (XPCollection<ViewPartnerPartnerCategory>)Session["PartnerCategory"];
            //}

            //grdPartnerPartnerCategory.DataSource = collection;
            //grdPartnerPartnerCategory.DataBind();
        }

        private bool validate()
        {
            bool isValid = true;

            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();

            string oldCode = "";
            string oldName = "";

            //if (txtPartnerCode.Text.Trim().Equals(""))
            //{
            //    isValid = false;
            //}

            //if (txtPartnerName.Text.Trim().Equals(""))
            //{
            //    isValid = false;
            //}
            
            //if (!isValid)
            //{
            //    return false;
            //}

            //if (hPartnerEditId.Count > 0 && hPartnerEditId.Get("id").ToString() != "")
            //{
            //    ViewPartner vpu = uow.GetObjectByKey<ViewPartner>(long.Parse(hPartnerEditId.Get("id").ToString()));

            //    if (vpu != null)
            //    {
            //        oldCode = vpu.Code;
            //        oldName = vpu.Name;
            //    }
            //}

            //Partner p = uow.FindObject<Partner>(new BinaryOperator("Code", txtPartnerCode.Text));

            //if (p != null && oldCode != txtPartnerCode.Text)
            //{
            //    txtPartnerCode.ValidationSettings.ErrorText = "Mã cộng tác viên này đã tồn tại !";
            //    isValid = false;
            //}

            //PartnerCategoryProperty pp = uow.FindObject<PartnerCategoryProperty>(new BinaryOperator("Name", txtPartnerName.Text));

            //if (pp != null && oldName != txtPartnerName.Text)
            //{
            //    txtPartnerName.ValidationSettings.ErrorText = "Tên cộng tác viên này đã tồn tại !";
            //    isValid = false;
            //}

            return isValid;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelpers.GetNewSession();

            //XPCollection<ViewPartnerPartnerCategory> collectionPartnerPartnerCategory = (XPCollection<ViewPartnerPartnerCategory>)Session["PartnerCategory"];            
            //grdPartnerPartnerCategory.DataSource = collectionPartnerPartnerCategory;
            //grdPartnerPartnerCategory.DataBind();
        }

        protected void txtPartnerCode_Validation(object sender, ValidationEventArgs e)
        {         
            //ASPxTextBox txt = sender as ASPxTextBox;

            //string oldCode = "";
            //string oldName = "";

            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();

            //if (hPartnerEditId.Count > 0 && hPartnerEditId.Get("id").ToString() != "")
            //{
            //    ViewPartner vpu = uow.GetObjectByKey<ViewPartner>(long.Parse(hPartnerEditId.Get("id").ToString()));

            //    if (vpu != null)
            //    {
            //        oldCode = vpu.Code;
            //        oldName = vpu.Name;
            //    }
            //}

            //Partner p = uow.FindObject<Partner>(new BinaryOperator("Code", txt.Text));

            //if (p != null && oldCode != txtPartnerCode.Text)
            //{
            //    e.IsValid = false;
            //    e.ErrorText = "Mã cộng tác viên này đã tồn tại !";
            //}

        }

        protected void txtPartnerName_Validation(object sender, ValidationEventArgs e)
        {
            e.IsValid = validate();
            if (txtPartnerName.ValidationSettings.ErrorText == "")
            {
                e.IsValid = true;
            }
        }
       

        /////////////////////////////////PartnerCategory

        public void cboPartnerCategory_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {

        }

        public void cboPartnerCategory_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            //XPCollection<ViewPartnerCategory> collection = new XPCollection<ViewPartnerCategory>(session);
            //collection.SkipReturnedObjects = e.BeginIndex;
            //collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;
            //collection.Criteria = new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like);
            //collection.Sorting.Add(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));

            //comboBox.DataSource = collection;
            //comboBox.DataBindItems();
        }

        protected void grdPartnerCategory_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //XPCollection<ViewPartnerPartnerCategory> collection = (XPCollection<ViewPartnerPartnerCategory>)Session["PartnerCategory"];

            //ViewPartnerPartnerCategory v = new ViewPartnerPartnerCategory(collection.Session);
            //v.Code = e.NewValues["Code"].ToString();
            //v.Name = e.NewValues["Name"].ToString();
            //v.Description = e.NewValues["Description"] == null ? "" : e.NewValues["Description"].ToString();
            
            //collection.Add(v);

            //grdPartnerPartnerCategory.DataSource = collection;
            //grdPartnerPartnerCategory.DataBind();
         
            //e.Cancel = true;
            //grdPartnerPartnerCategory.CancelEdit();
        }

        protected void grdPartnerCategory_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //XPCollection<ViewPartnerPartnerCategory> collection = (XPCollection<ViewPartnerPartnerCategory>)Session["PartnerCategory"];

            //foreach (ViewPartnerPartnerCategory x in collection)
            //{
            //    if (x.Code == e.OldValues["Code"].ToString())
            //    {
            //        x.Code = e.NewValues["Code"].ToString();
            //        x.Name = e.NewValues["Name"].ToString();
            //        x.Description = e.NewValues["Description"] == null ? "" : e.NewValues["Description"].ToString();                 
            //    }
            //}

            //grdPartnerPartnerCategory.DataSource = collection;
            //grdPartnerPartnerCategory.DataBind();

            //e.Cancel = true;
            //grdPartnerPartnerCategory.CancelEdit();
        }

        protected void grdPartnerCategory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //XPCollection<ViewPartnerPartnerCategory> collection = (XPCollection<ViewPartnerPartnerCategory>)Session["PartnerCategory"];

            //ViewPartnerPartnerCategory v = null;

            //foreach (ViewPartnerPartnerCategory x in collection)
            //{
            //    if (x.Code == e.Values["Code"].ToString())
            //    {
            //        v = x;
            //    }
            //}

            //collection.Remove(v);

            //grdPartnerPartnerCategory.DataSource = collection;
            //grdPartnerPartnerCategory.DataBind();

            e.Cancel = true;
        }

        protected void grdPartnerCategory_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Code")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                           "grdPartnerPartnerCategory.GetEditor('Name').SetValue(s.GetSelectedItem().GetColumnText('Name'));" +
                                                           "grdPartnerPartnerCategory.GetEditor('Description').SetValue(s.GetSelectedItem().GetColumnText('Description'));" +
                                                       "}";
            }
        }

        protected void grdPartnerCategory_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //XPCollection<ViewPartnerPartnerCategory> collection = (XPCollection<ViewPartnerPartnerCategory>)Session["PartnerCategory"];

            //foreach (GridViewColumn column in grdPartnerPartnerCategory.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (e.NewValues["Code"] == null)
            //    {
            //        e.Errors[grdPartnerPartnerCategory.Columns["Code"]] = "Chưa chọn nhóm cộng tác viên !";
            //        return;
            //    }
            //}

            //foreach (ViewPartnerPartnerCategory x in collection)
            //{
            //    if (e.OldValues["Code"] != null)
            //    {
            //        if (x.Code.Equals(e.NewValues["Code"].ToString()) && !e.NewValues["Code"].ToString().Equals(e.OldValues["Code"].ToString()))
            //        {
            //            e.Errors[grdPartnerPartnerCategory.Columns["Code"]] = "Nhóm cộng tác viên này đã tồn tại !";
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        if (x.Code.Equals(e.NewValues["Code"].ToString()))
            //        {
            //            e.Errors[grdPartnerPartnerCategory.Columns["Code"]] = "Nhóm cộng tác viên này đã tồn tại !";
            //            break;
            //        }
            //    }
            //}
        }

        protected void cpPartnerEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

            //ASPxHtmlEditor htmlDescription;
            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();

            //String[] p = e.Parameter.Split('|');

            //switch (p[0])
            //{               
            //    case "edit":


            //        uow = XpoHelpers.GetNewUnitOfWork();

            //        ViewPartner vp = uow.GetObjectByKey<ViewPartner>(long.Parse(hPartnerEditId.Get("id").ToString()));

            //        txtPartnerCode.Text = vp.Code;
            //        txtPartnerName.Text = vp.Name;
            //        cboPartnerRowStatus.Value = Convert.ToString(vp.RowStatus);

            //        htmlDescription = (ASPxHtmlEditor)nbPartnerEdit.Items[0].FindControl("htmlDescription");
            //        htmlDescription.Html = vp.Description;

            //        formPartnerEdit.HeaderText = "Thông tin cộng tác viên - " + vp.Code;

            //        XPCollection<ViewPartnerPartnerCategory> collectionPartnerPartnerCategory = new XPCollection<ViewPartnerPartnerCategory>(session);
            //        collectionPartnerPartnerCategory.Criteria = new BinaryOperator("PartnerId", vp.PartnerId, BinaryOperatorType.Equal);

            //        grdPartnerPartnerCategory.DataSource = collectionPartnerPartnerCategory;
            //        grdPartnerPartnerCategory.DataBind();

            //        Session["PartnerCategory"] = collectionPartnerPartnerCategory;

            //        //formPartnerEdit.ShowOnPageLoad = true;  


            //        break;

            //    case "new":


            //        collectionPartnerPartnerCategory = new XPCollection<ViewPartnerPartnerCategory>(session);
            //        collectionPartnerPartnerCategory.Criteria = new BinaryOperator("PartnerId", Guid.NewGuid(), BinaryOperatorType.Equal);
            //        Session["PartnerCategory"] = collectionPartnerPartnerCategory;

            //        grdPartnerPartnerCategory.DataSource = collectionPartnerPartnerCategory;
            //        grdPartnerPartnerCategory.DataBind();

            //        txtPartnerCode.Text = "";
            //        txtPartnerName.Text = "";

            //        formPartnerEdit.HeaderText = "Thông tin cộng tác viên - Thêm mới";
            //        htmlDescription = (ASPxHtmlEditor)nbPartnerEdit.Items[0].FindControl("htmlDescription");
            //        htmlDescription.Html = "";

            //        cboPartnerRowStatus.Value = Convert.ToString(Constant.ROWSTATUS_ACTIVE);
            //        //formPartnerEdit.ShowOnPageLoad = true;


            //        break;

            //    case "save":

            //        if (!validate())
            //        {
            //            return;
            //        }

            //        //ASPxPageControl callbackPanel = (ASPxPageControl)sender;
            //        //bool isValid = ASPxEdit.ValidateEditorsInContainer(callbackPanel);

            //        htmlDescription = (ASPxHtmlEditor)nbPartnerEdit.Items[0].FindControl("htmlDescription");

            //        PartnerEntity pe = new PartnerEntity();
            //        pe.Code = txtPartnerCode.Text;
            //        pe.Name = txtPartnerName.Text;
            //        pe.Description = htmlDescription.Html;

            //        pe.collectionPartnerPartnerCategory = (XPCollection<ViewPartnerPartnerCategory>)Session["PartnerCategory"];

            //        if (hPartnerEditId.Count > 0 && hPartnerEditId.Get("id").ToString() != "")
            //        {
            //            vp = session.GetObjectByKey<ViewPartner>(long.Parse(hPartnerEditId.Get("id").ToString()));
            //            pe.PartnerId = vp.PartnerId;
            //            pe.PartnerProperty = vp.PartnerProperty;
            //            pe.RowStatus = char.Parse(cboPartnerRowStatus.Value.ToString());

            //            PartnerBLO.updatePartner(pe);
            //        }
            //        else
            //        {
            //            pe.RowCreationTimeStamp = DateTime.Now;
            //            pe.Language = Constant.LANG_DEFAULT;
            //            pe.RowStatus = Constant.ROWSTATUS_ACTIVE;

            //            PartnerBLO.insertPartner(pe);
            //        }

            //        hPartnerEditId.Clear();                    

            //        cpPartnerEdit.JSProperties.Add("cpRefresh", "resfresh");

            //        break;

            //    case "view":

            //        break;
            //    default:
            //        break;
            //}
        }

        protected void formPartnerEdit_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
           
        }

        protected void cpPartnerCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //String[] p = e.Parameter.Split('|');

            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();


            //switch (p[0])
            //{
            //    case "txtPartnerCode_Validation":
            //        string oldCode = "";
            //        string oldName = "";


            //        if (hPartnerEditId.Count > 0 && hPartnerEditId.Get("id").ToString() != "")
            //        {

            //            ViewPartner vpu = uow.GetObjectByKey<ViewPartner>(long.Parse(hPartnerEditId.Get("id").ToString()));

            //            if (vpu != null)
            //            {
            //                oldCode = vpu.Code;
            //                oldName = vpu.Name;
            //            }
            //        }

            //        Partner pp = uow.FindObject<Partner>(new BinaryOperator("Code", txtPartnerCode.Text));

            //        if (pp != null && oldCode != txtPartnerCode.Text)
            //        {
            //            txtPartnerCode.ValidationSettings.ErrorText = "Mã cộng tác viên này đã tồn tại !";
            //            txtPartnerCode.IsValid = false;

            //            //isValid = false;
            //        }

            //        break;
            //}
        }

        
        

    

        

     
     

      

   
       

        


    }
}