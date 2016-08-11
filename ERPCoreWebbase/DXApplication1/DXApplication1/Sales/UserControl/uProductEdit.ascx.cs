using System;
using System.Collections;
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
using System.Collections.Generic;
using DevExpress.Web.ASPxTreeList;

namespace ERPCore.Sale.UserControl
{
    public partial class uProductEdit : System.Web.UI.UserControl
    {
        Session session;
     
        private bool validate()
        {
            bool isValid = true;

            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();

            //string oldCode = "";
            //string oldName = "";

            //if (hProductEditId.Count > 0 && hProductEditId.Get("id").ToString() != "")
            //{
            //    ViewProduct vpu = uow.GetObjectByKey<ViewProduct>(long.Parse(hProductEditId.Get("id").ToString()));

            //    if (vpu != null)
            //    {
            //        oldCode = vpu.Code;
            //        oldName = vpu.Name;
            //    }
            //}

            //Product p = uow.FindObject<Product>(new BinaryOperator("Code", txtProductCode.Text));

            //if (p != null && oldCode != txtProductCode.Text)
            //{
            //    txtProductCode.ValidationSettings.ErrorText = "Mã hàng này đã tồn tại !";
            //    isValid = false;
            //}

            //ProductProperty pp = uow.FindObject<ProductProperty>(new BinaryOperator("Name", txtProductName.Text));

            //if (pp != null && oldName != txtProductName.Text)
            //{
            //    txtProductName.ValidationSettings.ErrorText = "Tên hàng này đã tồn tại !";
            //    isValid = false;
            //}

            return isValid;
        }
             

        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelpers.GetNewSession();

            ////ProductSalingProductCategoryXDS.Session = session;
            ////ProductSalingProductCategoryXDS.Criteria = "";            

            //if (Session["Unit"] != null)
            //{
            //    XPCollection<ViewProductProductUnit> collection = (XPCollection<ViewProductProductUnit>)Session["Unit"];

            //    grdProductUnit.DataSource = collection;
            //    grdProductUnit.DataBind();
            //}
         
            ////if (Session["SalingProductCategory"] != null)
            ////{
            //    XPCollection<ViewProductSalingProductCategory> collection1 = (XPCollection<ViewProductSalingProductCategory>)Session["SalingProductCategory"];

            //    grdSalingProductCategory.DataSource = collection1;
            //    grdSalingProductCategory.DataBind();
            ////}
      
            ////if (Session["Supplier"] != null)
            ////{
            //    XPCollection<ViewProductSupplier> collection2 = (XPCollection<ViewProductSupplier>)Session["Supplier"];

            //    grdProductSupplier.DataSource = collection2;
            //    grdProductSupplier.DataBind();
            ////}

            ////if (Session["Active"] != null)
            ////{
            //    XPCollection<ViewProductActiveElement> collection3 = (XPCollection<ViewProductActiveElement>)Session["Active"];
            //    ASPxGridView grdActiveElement = (ASPxGridView)nbOtherInfo.Items[0].FindControl("grdActiveElement");
            //    grdActiveElement.DataSource = collection3;
            //    grdActiveElement.DataBind();
            ////}

        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
            }
            else
            {

              
            }

            //ASPxGridView grdActiveElement = (ASPxGridView)nbOtherInfo.Items[0].FindControl("grdActiveElement");
            //grdActiveElement.DataSource = (XPCollection<ViewProductActiveElement>)Session["Active"]; 
            //grdActiveElement.DataBind();
        }

        protected void grdSalingProductCategory_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {

        }
         
        protected void ProductSupplierXDS_Inserted(object sender, XpoDataSourceInsertedEventArgs e)
        {           
        }

        protected void cpProductEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
           // ASPxHtmlEditor htmlDescription;
           // UnitOfWork uow;
           // ViewProduct vp;

           //String[] p = e.Parameter.Split('|');
            
           //switch (p[0])
           //{

           //    case "edit":

           //         uow = XpoHelpers.GetNewUnitOfWork();

           //         vp = uow.GetObjectByKey<ViewProduct>(long.Parse(hProductEditId.Get("id").ToString()));
                    
           //         txtProductCode.Text = vp.Code;
           //         txtProductName.Text = vp.Name;
           //         cboProductRowStatus.Value = Convert.ToString(vp.RowStatus);
           //         cboProductManufacturer.Value = vp.ManufacturerCode;

           //         htmlDescription = (ASPxHtmlEditor)nbProduct.Items[0].FindControl("htmlDescription");
           //         htmlDescription.Html = vp.Description;

           //         XPCollection<ViewProductSalingProductCategory> collectionSalingProductCategory = new XPCollection<ViewProductSalingProductCategory>(session);
           //         collectionSalingProductCategory.Criteria = new BinaryOperator("ProductId", vp.ProductId, BinaryOperatorType.Equal);

           //         grdSalingProductCategory.DataSource = collectionSalingProductCategory;
           //         grdSalingProductCategory.DataBind();

           //         Session["SalingProductCategory"] = collectionSalingProductCategory;

           //         XPCollection<ViewProductSupplier> collectionSupplier = new XPCollection<ViewProductSupplier>(session);
           //         collectionSupplier.Criteria = new BinaryOperator("ProductId", vp.ProductId, BinaryOperatorType.Equal);

           //         grdProductSupplier.DataSource = collectionSupplier;
           //         grdProductSupplier.DataBind();

           //         Session["Supplier"] = collectionSupplier;

           //         XPCollection<ViewProductProductUnit> collectionUnit = new XPCollection<ViewProductProductUnit>(session);
           //         collectionUnit.Criteria = new BinaryOperator("ProductId", vp.ProductId, BinaryOperatorType.Equal);                    

           //         grdProductUnit.DataSource = collectionUnit;
           //         grdProductUnit.DataBind();

           //         Session["Unit"] = collectionUnit;

           //         XPCollection<ViewProductActiveElement> collectionActive = new XPCollection<ViewProductActiveElement>(session);
           //         collectionActive.Criteria = new BinaryOperator("ProductId", vp.ProductId, BinaryOperatorType.Equal);

           //         ASPxGridView grdActiveElement = (ASPxGridView)nbOtherInfo.Items[0].FindControl("grdActiveElement");
           //         grdActiveElement.DataSource = collectionActive;
           //         grdActiveElement.DataBind();

           //         Session["Active"] = collectionActive;

           //         formProductEdit.HeaderText = "Thông tin hàng hóa - " + vp.Code;               

           //        break;

           //    case "new":
           //        pcProduct.Visible = false;

           //        cboProductRowStatus.Value = "A";
           //        formProductEdit.HeaderText = "Thông tin hàng hóa - Thêm mới";

           //        uow = XpoHelpers.GetNewUnitOfWork();

           //        collectionSalingProductCategory = new XPCollection<ViewProductSalingProductCategory>(session);
           //        collectionSalingProductCategory.Criteria = new BinaryOperator("ProductId", Guid.NewGuid(), BinaryOperatorType.Equal);
           //        Session["SalingProductCategory"] = collectionSalingProductCategory;
           //        grdSalingProductCategory.DataSource = collectionSalingProductCategory;
           //        grdSalingProductCategory.DataBind();                   

           //        collectionSupplier = new XPCollection<ViewProductSupplier>(session);
           //        collectionSupplier.Criteria = new BinaryOperator("ProductId", Guid.NewGuid(), BinaryOperatorType.Equal);
           //        Session["Supplier"] = collectionSupplier;                   
           //        grdProductSupplier.DataSource = collectionSupplier;
           //        grdProductSupplier.DataBind();

           //        collectionUnit = new XPCollection<ViewProductProductUnit>(session);
           //        collectionUnit.Criteria = new BinaryOperator("ProductId", Guid.NewGuid(), BinaryOperatorType.Equal);
           //        Session["Unit"] = collectionUnit;                   
           //        grdProductUnit.DataSource = collectionUnit;
           //        grdProductUnit.DataBind();

           //        collectionActive = new XPCollection<ViewProductActiveElement>(session);                   
           //        grdActiveElement = (ASPxGridView)nbOtherInfo.Items[0].FindControl("grdActiveElement");
           //        collectionActive.Criteria = new BinaryOperator("ProductId", Guid.NewGuid(), BinaryOperatorType.Equal);
           //        Session["Active"] = collectionActive;
           //        grdActiveElement.DataSource = collectionActive;
           //        grdActiveElement.DataBind();

           //        htmlDescription = (ASPxHtmlEditor)nbProduct.Items[0].FindControl("htmlDescription");
           //        htmlDescription.Html = "";

           //        pcProduct.Visible = true;

           //        break;

           //    case "save":          
                                         
           //        if (!validate())
           //        {
           //            return;
           //        }
                
           //         htmlDescription = (ASPxHtmlEditor)nbProduct.Items[0].FindControl("htmlDescription");

           //         ProductEntity pe = new ProductEntity();
           //         pe.Code = txtProductCode.Text;
           //         pe.Name = txtProductName.Text;
           //         pe.Description = htmlDescription.Html;
           //         pe.ManufacturerCode = cboProductManufacturer.Value.ToString();

           //         pe.collectionProductSalingProductCategory = (XPCollection<ViewProductSalingProductCategory>)Session["SalingProductCategory"];
           //         pe.collectionProductSupplier = (XPCollection<ViewProductSupplier>)Session["Supplier"];
           //         pe.collectionProductUnit = (XPCollection<ViewProductProductUnit>)Session["Unit"];
           //         pe.collectionProductActive = (XPCollection<ViewProductActiveElement>)Session["Active"];

           //         if (hProductEditId.Count > 0 && hProductEditId.Get("id").ToString() != "")
           //         {                           
           //             vp = session.GetObjectByKey<ViewProduct>(long.Parse(hProductEditId.Get("id").ToString()));
           //             pe.ProductId = vp.ProductId;
           //             pe.ProductPropertyId = vp.ProductPropertyId;
           //             pe.RowCreationTimeStamp = DateTime.Now;
           //             pe.RowStatus = char.Parse(cboProductRowStatus.Value.ToString());
           //             pe.Description = htmlDescription.Html;

           //             ProductBLO.updateProduct(pe);
                    
           //         }
           //         else
           //         {
           //             pe.ProductId = Guid.NewGuid();
           //             pe.RowCreationTimeStamp = DateTime.Now;
           //             pe.Language = Constant.LANG_DEFAULT;                        
           //             pe.RowStatus = Constant.ROWSTATUS_ACTIVE;
           //             pe.Description = htmlDescription.Html;

           //             ProductBLO.insertProduct(pe);
           //         }

           //         hProductEditId.Clear();    
           //         formProductEdit.ShowOnPageLoad = false;

           //         cpProductEdit.JSProperties.Add("cpRefresh", "resfresh");

           //         break;

           //    case "view":
           //         break;
           //    case "grdUnit_Refresh":
           //         grdProductUnit.DataBind();
           //         break;
           //    default:
           //        break;
           //}
           
        }


        //////////////////////////////// Main Info

        protected void cboProductManufacturer_ItemRequestedByValue1(object source, ListEditItemRequestedByValueEventArgs e)
        {
            /*
            ViewManufacturer vm = session.GetObjectByKey<ViewManufacturer>(e.Value);
            if (vm != null)
            {
                cboProductManufacturer.DataSource = new ViewManufacturer[] { vm };
                cboProductManufacturer.DataBindItems();
            }
            */
        }

        protected void cboProductManufacturer_ItemsRequestedByFilterCondition1(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            //XPCollection<ViewManufacturer> collection = new XPCollection<ViewManufacturer>(session);
            //collection.SkipReturnedObjects = e.BeginIndex;
            //collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;
            //collection.Criteria = new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like);
            //collection.Sorting.Add(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));

            //cboProductManufacturer.DataSource = collection;
            //cboProductManufacturer.DataBindItems();
        }

        protected void cboProductManufacturer_Validation(object sender, ValidationEventArgs e)
        {
            e.IsValid = validate();
            if (cboProductManufacturer.ValidationSettings.ErrorText == "")
            {
                e.IsValid = true;
            }
        }

        protected void txtProductName_Validation(object sender, ValidationEventArgs e)
        {
            //e.IsValid = validate();
            //if (txtProductName.ValidationSettings.ErrorText == "")
            //{
            //    e.IsValid = true;
            //}
        }

        protected void txtProductCode_Validation(object sender, ValidationEventArgs e)
        {

            ASPxTextBox txt = sender as ASPxTextBox;

            string oldCode = "";
            string oldName = "";

            //UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();
         
            //if (hProductEditId.Count > 0 && hProductEditId.Get("id").ToString() != "")
            //{
            //    ViewProduct vpu = uow.GetObjectByKey<ViewProduct>(long.Parse(hProductEditId.Get("id").ToString()));

            //    if (vpu != null)
            //    {
            //        oldCode = vpu.Code;
            //        oldName = vpu.Name;
            //    }
            //}

            //Product pp = uow.FindObject<Product>(new BinaryOperator("Code", txtProductCode.Text));

            //if (pp != null && oldCode != txtProductCode.Text)
            //{
            //    e.IsValid = false;
            //    e.ErrorText = "Mã hàng này đã tồn tại !";
            //}

            //e.IsValid = validate();
            //if (txtProductCode.ValidationSettings.ErrorText == "")
            //{
            //    e.IsValid = true;
            //}
        }


        //////////////////////////////// SalingProductCategory

        public void SalingProductCategoryItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {

        }

        public void SalingProductCategoryItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            //XPCollection<ViewSalingProductCategory> collection = new XPCollection<ViewSalingProductCategory>(session);
            //collection.SkipReturnedObjects = e.BeginIndex;
            //collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;
            //collection.Criteria = new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like);
            //collection.Sorting.Add(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));

            //comboBox.DataSource = collection;
            //comboBox.DataBindItems();
        }


        protected void grdSalingProductCategory_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //XPCollection<ViewProductSalingProductCategory> collection = (XPCollection<ViewProductSalingProductCategory>)Session["SalingProductCategory"];

            //foreach (ViewProductSalingProductCategory x in collection)
            //{
            //    if (e.NewValues["Code"].Equals(x.Code))
            //    {
            //        e.Cancel = false;
            //    }
            //}
                

            //ViewProductSalingProductCategory v = new ViewProductSalingProductCategory(collection.Session);
            //v.Code = e.NewValues["Code"].ToString();
            //v.Name = e.NewValues["Name"].ToString();
            //v.Description = e.NewValues["Description"] == null ? "" : e.NewValues["Description"].ToString();

            //collection.Add(v);

            //grdSalingProductCategory.DataSource = collection;
            //grdSalingProductCategory.DataBind();

            //e.Cancel = true;
            //grdSalingProductCategory.CancelEdit();

        }

        protected void grdSalingProductCategory_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //XPCollection<ViewProductSalingProductCategory> collection = (XPCollection<ViewProductSalingProductCategory>)Session["SalingProductCategory"];

            //foreach (ViewProductSalingProductCategory x in collection)
            //{
            //    if (x.Code == e.OldValues["Code"].ToString())
            //    {
            //        x.Code = e.NewValues["Code"].ToString();
            //        x.Name = e.NewValues["Name"].ToString();
            //        x.Description = e.NewValues["Description"] == null ? "" : e.NewValues["Description"].ToString();
            //    }
            //}

            //grdSalingProductCategory.DataSource = collection;
            //grdSalingProductCategory.DataBind();

            //e.Cancel = true;
            //grdSalingProductCategory.CancelEdit();
        }

        protected void grdSalingProductCategory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //XPCollection<ViewProductSalingProductCategory> collection = (XPCollection<ViewProductSalingProductCategory>)Session["SalingProductCategory"];

            //ViewProductSalingProductCategory v = null;

            //foreach (ViewProductSalingProductCategory x in collection)
            //{
            //    if (x.Code == e.Values["Code"].ToString())
            //    {
            //        v = x;
            //    }
            //}
            
            //collection.Remove(v);

            //grdSalingProductCategory.DataSource = collection;
            grdSalingProductCategory.DataBind();

            e.Cancel = true;
        }

     

        protected void grdSalingProductCategory_CellEditorInitialize1(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Code")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                           "grdSalingProductCategory.GetEditor('Name').SetValue(s.GetSelectedItem().GetColumnText('Name'));" +
                                                           "grdSalingProductCategory.GetEditor('Description').SetValue(s.GetSelectedItem().GetColumnText('Description'));" +                                                           
                                                       "}";   
            }
        }

        protected void grdSalingProductCategory_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //XPCollection<ViewProductSalingProductCategory> collection = (XPCollection<ViewProductSalingProductCategory>)Session["SalingProductCategory"];
            //foreach (GridViewColumn column in grdSalingProductCategory.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (e.NewValues["Code"] == null)
            //    {
            //        e.Errors[grdSalingProductCategory.Columns["Code"]] = "Chưa chọn nhóm hàng hóa !";
            //        return;
            //    }
            //}

            //foreach(ViewProductSalingProductCategory x in collection)
            //{
            //    if (e.OldValues["Code"] != null)
            //    {
            //        if (x.Code.Equals(e.NewValues["Code"].ToString()) && !e.NewValues["Code"].ToString().Equals(e.OldValues["Code"].ToString()))
            //        {
            //            e.Errors[grdSalingProductCategory.Columns["Code"]] = "Nhóm hàng hóa này đã tồn tại !";
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        if (x.Code.Equals(e.NewValues["Code"].ToString()))
            //        {
            //            e.Errors[grdSalingProductCategory.Columns["Code"]] = "Nhóm hàng hóa này đã tồn tại !";
            //            break;
            //        }
            //    }
            //}
        }

        //////////////////////////////// ProductSupplier


        public void ProductSupplierItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {

        }

        public void ProductSupplierItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            //XPCollection<ViewSupplier> collection = new XPCollection<ViewSupplier>(session);
            //collection.SkipReturnedObjects = e.BeginIndex;
            //collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;
            //collection.Criteria = new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like);
            //collection.Sorting.Add(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));

            //comboBox.DataSource = collection;
            //comboBox.DataBindItems();
        }

        protected void grdProductSupplier_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //XPCollection<ViewProductSupplier> collection = (XPCollection<ViewProductSupplier>)Session["Supplier"];

            //ViewProductSupplier v = new ViewProductSupplier(collection.Session);
            //v.Code = e.NewValues["Code"].ToString();
            //v.Name = e.NewValues["Name"].ToString();
            //v.Description = e.NewValues["Description"] == null ? "" : e.NewValues["Description"].ToString();

            //collection.Add(v);

            //grdProductSupplier.DataSource = collection;
            //grdProductSupplier.DataBind();

            //e.Cancel = true;
            //grdProductSupplier.CancelEdit();
        }

        protected void grdProductSupplier_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //XPCollection<ViewProductSupplier> collection = (XPCollection<ViewProductSupplier>)Session["Supplier"];

            //foreach (ViewProductSupplier x in collection)
            //{
            //    if (x.Code == e.OldValues["Code"].ToString())
            //    {
            //        x.Code = e.NewValues["Code"].ToString();
            //        x.Name = e.NewValues["Name"].ToString();
            //        x.Description = e.NewValues["Description"] == null ? "" : e.NewValues["Description"].ToString();
            //    }
            //}

            //grdProductSupplier.DataSource = collection;
            //grdProductSupplier.DataBind();

            //e.Cancel = true;
            //grdProductSupplier.CancelEdit();
        }

        protected void grdProductSupplier_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //XPCollection<ViewProductSupplier> collection = (XPCollection<ViewProductSupplier>)Session["Supplier"];

            //ViewProductSupplier v = null;

            //foreach (ViewProductSupplier x in collection)
            //{
            //    if (x.Code == e.Values["Code"].ToString())
            //    {
            //        v = x;
            //    }
            //}

            //collection.Remove(v);

            //grdProductSupplier.DataSource = collection;
            //grdProductSupplier.DataBind();

            //e.Cancel = true;
        }

        protected void grdProductSupplier_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {
           
            if (e.Column.FieldName == "Code")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                           "grdProductSupplier.GetEditor('Name').SetValue(s.GetSelectedItem().GetColumnText('Name'));" +
                                                           "grdProductSupplier.GetEditor('Description').SetValue(s.GetSelectedItem().GetColumnText('Description'));" +
                                                       "}";
            }
             
        }

        protected void grdProductSupplier_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //XPCollection<ViewProductSupplier> collection = (XPCollection<ViewProductSupplier>)Session["Supplier"];

            //foreach (GridViewColumn column in grdProductSupplier.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (e.NewValues["Code"] == null)
            //    {
            //        e.Errors[grdProductSupplier.Columns["Code"]] = "Chưa chọn nhà cung cấp !";
            //        return;
            //    }
            //}

            //foreach (ViewProductSupplier x in collection)
            //{
            //    if (e.OldValues["Code"] != null)
            //    {
            //        if (x.Code.Equals(e.NewValues["Code"].ToString()) && !e.NewValues["Code"].ToString().Equals(e.OldValues["Code"].ToString()))
            //        {
            //            e.Errors[grdProductSupplier.Columns["Code"]] = "Nhà cung cấp này đã tồn tại !";
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        if (x.Code.Equals(e.NewValues["Code"].ToString()))
            //        {
            //            e.Errors[grdProductSupplier.Columns["Code"]] = "Nhà cung cấp này đã tồn tại !";
            //            break;
            //        }
            //    }
            //}
        }

        protected void pcProduct_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
        {

        }

        //////////////////////////////// ProductUnit

        public void ProductUnitItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {

        }

        public void ProductUnitItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            //XPCollection<ViewProductUnit> collection = new XPCollection<ViewProductUnit>(session);
            //collection.SkipReturnedObjects = e.BeginIndex;
            //collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;
            //collection.Criteria = new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like);
            //collection.Sorting.Add(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));

            //comboBox.DataSource = collection;
            //comboBox.DataBindItems();
        }

        protected void grdProductUnit_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //XPCollection<ViewProductProductUnit> collection = (XPCollection<ViewProductProductUnit>)Session["Unit"];

            //ViewProductProductUnit v = null;

            //foreach (ViewProductProductUnit x in collection)
            //{
            //    if (x.Code == e.Values["Code"].ToString())
            //    {
            //        v = x;
            //    }
            //}

            //collection.Remove(v);

            //grdProductUnit.DataSource = collection;
            //grdProductUnit.DataBind();

            e.Cancel = true;
        }

        protected void grdProductUnit_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //grdProductUnit.DoNodeValidation();

            //XPCollection<ViewProductProductUnit> collection = (XPCollection<ViewProductProductUnit>)Session["Unit"];

            //ViewProductProductUnit v = new ViewProductProductUnit(collection.Session);
            
            //v.Code = e.NewValues["Code"].ToString();

            //CriteriaOperator filter = CriteriaOperator.Parse("Code = ?", e.NewValues["Code"].ToString());
            //ProductUnit s = session.FindObject<ProductUnit>(filter);

            //v.ProductUnitId = s.ProductUnitId;

            //v.Name = e.NewValues["Name"].ToString();
            ////v.Description = e.NewValues["Description"] == null ? "" : e.NewValues["Description"].ToString();
            //v.ProductProductUnitId = Guid.NewGuid();
            ////v.ParentProductProductUnitId = Guid.NewGuid();
            //v.ProductId = Guid.NewGuid();

            //if (e.NewValues[grdProductUnit.ParentFieldName] != null)
            //{
            //    //v.ParentProductProductUnitId = Guid.Parse(e.NewValues[grdProductUnit.ParentFieldName].ToString());
            //}
            
            //ASPxSpinEdit c = (ASPxSpinEdit)grdProductUnit.FindEditCellTemplateControl((TreeListDataColumn)grdProductUnit.Columns["NumRequired"], "colNumRequired");
            //v.NumRequired = float.Parse(c.Value.ToString());

            //collection.Add(v);

            //grdProductUnit.DataSource = collection;
            //grdProductUnit.DataBind();

            //e.Cancel = true;
            //grdProductUnit.CancelEdit();
        }

        protected void grdProductUnit_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //XPCollection<ViewProductProductUnit> collection = (XPCollection<ViewProductProductUnit>)Session["Unit"];

            //foreach (ViewProductProductUnit x in collection)
            //{
            //    if (x.Code == e.OldValues["Code"].ToString())
            //    {
            //        CriteriaOperator filter = CriteriaOperator.Parse("Code = ?", e.NewValues["Code"].ToString());
            //        ProductUnit s = session.FindObject<ProductUnit>(filter);

            //        x.ProductUnitId = s.ProductUnitId;

            //        x.Code = e.NewValues["Code"].ToString();
            //        x.Name = e.NewValues["Name"].ToString();
            //        //x.Description = e.NewValues["Description"] == null ? "" : e.NewValues["Description"].ToString();

            //        if (e.NewValues[grdProductUnit.ParentFieldName] != null)
            //        {
            //            //x.ParentProductProductUnitId = Guid.Parse(e.NewValues[grdProductUnit.ParentFieldName].ToString());
            //        }

            //        ASPxSpinEdit c = (ASPxSpinEdit)grdProductUnit.FindEditCellTemplateControl((TreeListDataColumn)grdProductUnit.Columns["NumRequired"], "colNumRequired");
            //        x.NumRequired = float.Parse(c.Value.ToString());
            //    }
            //}

            //grdProductUnit.DataSource = collection;
            //grdProductUnit.DataBind();

            //e.Cancel = true;
            //grdProductUnit.CancelEdit();

        }

        protected void grdProductUnit_CellEditorInitialize(object sender, DevExpress.Web.ASPxTreeList.TreeListColumnEditorEventArgs e)
        {
            if (e.Column.FieldName == "Code")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                           "grdProductUnit.GetEditor('Name').SetValue(s.GetSelectedItem().GetColumnText('Name'));" +
                                                           "grdProductUnit.GetEditor('Description').SetValue(s.GetSelectedItem().GetColumnText('Description'));" +
                                                           "colNumRequired.SetText(1)" +
                                                       "}";
            }
        }

        protected void grdProductUnit_NodeValidating(object sender, TreeListNodeValidationEventArgs e)
        {
            //XPCollection<ViewProductProductUnit> collection = (XPCollection<ViewProductProductUnit>)Session["Unit"];

            //if (e.NewValues["Code"] == null)
            //{
            //    e.Errors["Code"] = "Chưa chọn đơn vị tính !";
            //}

            //foreach (ViewProductProductUnit x in collection)
            //{
            //    if (e.OldValues["Code"] != null)
            //    {
            //        if (x.Code.Equals(e.NewValues["Code"].ToString()) && !e.NewValues["Code"].ToString().Equals(e.OldValues["Code"].ToString()))
            //        {
            //            e.Errors["Code"] = "Đơn vị tính này đã tồn tại !";
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        if (x.Code.Equals(e.NewValues["Code"].ToString()))
            //        {
            //            e.Errors["Code"] = "Đơn vị tính này đã tồn tại !";
            //            break;
            //        }
            //    }
            //}

            //ASPxSpinEdit c = (ASPxSpinEdit)grdProductUnit.FindEditCellTemplateControl((TreeListDataColumn)grdProductUnit.Columns["NumRequired"], "colNumRequired");
            //if (float.Parse(c.Value.ToString()) <= 0)
            //{
            //    e.Errors["NumRequired"] = c.ErrorText = "Giá trị >= 0";
            //    c.IsValid = false;

            //}
        }

        protected void colNumRequired_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            TreeListDataCellTemplateContainer container = spin.NamingContainer as TreeListDataCellTemplateContainer;

            if (!container.TreeList.IsNewNodeEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "NumRequired").ToString();
            }
        }

        protected void grdProductUnit_InitNewNode(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
        }
        protected void grdProductUnit_StartNodeEditing(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeEditingEventArgs e)
        {
          
        }
        protected void grdProductUnit_Init(object sender, EventArgs e)
        {
        }
        protected void grdProductUnit_NodeCollapsing(object sender, TreeListNodeCancelEventArgs e)
        {
        }

        //////////////////////////////// ProductActive

        //public void ActiveItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        //{

        //}

        //public void ActiveItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        //{
        //    ASPxComboBox comboBox = (ASPxComboBox)source;

        //    XPCollection<ViewActiveElement> collection = new XPCollection<ViewActiveElement>(session);
        //    collection.SkipReturnedObjects = e.BeginIndex;
        //    collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;
        //    collection.Criteria = new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like);
        //    collection.Sorting.Add(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));

        //    comboBox.DataSource = collection;
        //    comboBox.DataBindItems();
        //}


        //protected void grdActiveElement_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        //{
        //    XPCollection<ViewProductActiveElement> collection = (XPCollection<ViewProductActiveElement>)Session["Active"];

        //    ViewProductActiveElement v = null;

        //    foreach (ViewProductActiveElement x in collection)
        //    {
        //        if (x.Code == e.Values["Code"].ToString())
        //        {
        //            v = x;
        //        }
        //    }

        //    collection.Remove(v);

        //    ASPxGridView grdActiveElement = (ASPxGridView)nbOtherInfo.Items[0].FindControl("grdActiveElement");
        //    grdActiveElement.DataSource = collection;
        //    grdActiveElement.DataBind();

        //    e.Cancel = true;
        //}

        protected void grdActiveElement_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //XPCollection<ViewProductActiveElement> collection = (XPCollection<ViewProductActiveElement>)Session["Active"];

            //ViewProductActiveElement v = new ViewProductActiveElement(collection.Session);
            //v.Code = e.NewValues["Code"].ToString();
            //v.Name = e.NewValues["Name"].ToString();
            //v.Description = e.NewValues["Description"] == null ? "" : e.NewValues["Description"].ToString() ;
            //v.Component = e.NewValues["Component"] == null ? "" : e.NewValues["Component"].ToString();
            //v.ActiveFunction = e.NewValues["ActiveFunction"] == null ? "" : e.NewValues["ActiveFunction"].ToString();

            //collection.Add(v);

            //grdSalingProductCategory.DataSource = collection;
            //grdSalingProductCategory.DataBind();

            //ASPxGridView grdActiveElement = (ASPxGridView)nbOtherInfo.Items[0].FindControl("grdActiveElement");
            //grdActiveElement.DataSource = collection;
            //grdActiveElement.DataBind();

            //e.Cancel = true;
            //grdActiveElement.CancelEdit();
        }

        //protected void grdActiveElement_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        //{
        //    XPCollection<ViewProductActiveElement> collection = (XPCollection<ViewProductActiveElement>)Session["Active"];

        //    foreach (ViewProductActiveElement x in collection)
        //    {
        //        if (x.Code == e.OldValues["Code"].ToString())
        //        {
        //            x.Code = e.NewValues["Code"].ToString();
        //            x.Name = e.NewValues["Name"].ToString();
        //            x.Description = e.NewValues["Description"] == null ? "" : e.NewValues["Description"].ToString();
        //            x.Component = e.NewValues["Component"] == null ? "" : e.NewValues["Component"].ToString();
        //            x.ActiveFunction = e.NewValues["ActiveFunctio"] == null ? "" : e.NewValues["ActiveFunction"].ToString();
        //        }
        //    }           

        //    ASPxGridView grdActiveElement = (ASPxGridView)nbOtherInfo.Items[0].FindControl("grdActiveElement");
        //    grdActiveElement.DataSource = collection;
        //    grdActiveElement.DataBind();

        //    e.Cancel = true;
        //    grdActiveElement.CancelEdit();
        //}

        //protected void grdActiveElement_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        //{
        //    if (e.Column.FieldName == "Code")
        //    {
        //        ASPxComboBox combo = e.Editor as ASPxComboBox;
        //        combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
        //                                                   "grdActiveElement.GetEditor('Name').SetValue(s.GetSelectedItem().GetColumnText('Name'));" +
        //                                                   "grdActiveElement.GetEditor('Description').SetValue(s.GetSelectedItem().GetColumnText('Description'));" +
        //                                                   "grdActiveElement.GetEditor('ActiveFunction').SetValue(s.GetSelectedItem().GetColumnText('ActiveFunction'));" +
        //                                                   "grdActiveElement.GetEditor('Component').SetValue(s.GetSelectedItem().GetColumnText('Component'));" +
        //                                               "}";
        //    }
        //}

        //protected void grdActiveElement_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        //{
        //    XPCollection<ViewProductActiveElement> collection = (XPCollection<ViewProductActiveElement>)Session["Active"];
        //    ASPxGridView grdActiveElement = (ASPxGridView)nbOtherInfo.Items[0].FindControl("grdActiveElement");

        //    foreach (GridViewColumn column in grdActiveElement.Columns)
        //    {
        //        GridViewDataColumn dataColumn = column as GridViewDataColumn;
        //        if (e.NewValues["Code"] == null)
        //        {
        //            e.Errors[grdActiveElement.Columns["Code"]] = "Chưa chọn dược liệu, hoạt chất !";
        //            return;
        //        }
        //    }

        //    foreach (ViewProductActiveElement x in collection)
        //    {
        //        if (e.OldValues["Code"] != null)
        //        {
        //            if (x.Code.Equals(e.NewValues["Code"].ToString()) && !e.NewValues["Code"].ToString().Equals(e.OldValues["Code"].ToString()))
        //            {
        //                e.Errors[grdActiveElement.Columns["Code"]] = "Dược liệu, hoạt chất này đã tồn tại !";
        //                break;
        //            }
        //        }
        //        else
        //        {
        //            if (x.Code.Equals(e.NewValues["Code"].ToString()))
        //            {
        //                e.Errors[grdActiveElement.Columns["Code"]] = "Dược liệu, hoạt chất này đã tồn tại !";
        //                break;
        //            }
        //        }
        //    }
        //}

        //protected void cpProductCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        //{
        //    String[] p = e.Parameter.Split('|');

        //    UnitOfWork uow = XpoHelpers.GetNewUnitOfWork();

        //    switch (p[0])
        //    {
        //        case "txtProductCode_Validation":
        //            string oldCode = "";
        //            string oldName = "";


        //            if (hProductEditId.Count > 0 && hProductEditId.Get("id").ToString() != "")
        //            {
        //                ViewProduct vpu = uow.GetObjectByKey<ViewProduct>(long.Parse(hProductEditId.Get("id").ToString()));

        //                if (vpu != null)
        //                {
        //                    oldCode = vpu.Code;
        //                    oldName = vpu.Name;
        //                }
        //            }

        //            Product pp = uow.FindObject<Product>(new BinaryOperator("Code", txtProductCode.Text));

        //            if (pp != null && oldCode != txtProductCode.Text)
        //            {
        //                txtProductCode.IsValid = false;
        //                txtProductCode.ValidationSettings.ErrorText = "Mã hàng này đã tồn tại !";
        //            }

        //            break;
        //    }
        //}

        

        

      
      

     
      

       

       

     

    }
}