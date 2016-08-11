//////////////////////////////////////////////////////////////
///// Nguoi Thuc Hien - HO NHUT TUAN
//////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System.Collections;

//using DAL.Purchasing;

namespace ERPCore.ImExporting.UserControl
{
    public partial class uProductEdit : System.Web.UI.UserControl
    {
        /*
        UnitOfWork uow;
        ViewProduct viewProduct;
        ViewProductSupplier viewProductSupplier;

        Product product;
        ProductProperty productProperty;
        ProductSupplier productSupplier;
        Manufacturer manufacturer;
        Supplier supplier;
        */
        // sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectString"].ToString();
        // Customer function
        /*
        private bool validate()
        {
            bool isValid = true;

            uow = new UnitOfWork(ConnectionHelper.GetDataLayer(DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists));
            viewProduct = (ViewProduct)Session["productMode"];

            string oldCode = "";
            string oldName = "";

            if (viewProduct != null)
            {
                oldCode = viewProduct.Code;
                oldName = viewProduct.Name;
            }

            product = uow.FindObject<Product>(new BinaryOperator("Code", txtCode.Text));

            if (product != null && oldCode != txtCode.Text)
            {
                txtCode.ValidationSettings.ErrorText = "Mã Hàng Hóa này đã tồn tại !";
                isValid = false;
            }

            productProperty = uow.FindObject<ProductProperty>(new BinaryOperator("Name", txtName.Text));

            if (productProperty != null && oldName != txtName.Text)
            {
                txtName.ValidationSettings.ErrorText = "Tên Hàng Hóa này đã tồn tại !";
                isValid = false;
            }

            return isValid;
        }
        */
        private void BindGrid()
        {
            var datasource = new[]
            {
                new { OrganizationId=1, ParentOrganizationId=0, code="MS001", name="Thùng lớn", Description="Thùng lớn là đơn vị tính cao nhất", amount=""  },
                new { OrganizationId=2, ParentOrganizationId=1, code="LT002",name="Hợp lớn", Description="1 Thùng lớn chứa 20 Hộp lớn", amount="20"  },
                new { OrganizationId=3, ParentOrganizationId=2, code="LT003",name="Vĩ", Description="1 Hộp lớn chứa 30 Vĩ", amount="30"  },
                new { OrganizationId=4, ParentOrganizationId=3, code="LT004",name="Viên nén tròn", Description="1 Vĩ chứa 12 Viên nén tròn", amount="12"  }
            };
            ASPxTreeList1.DataSource = datasource;
            ASPxTreeList1.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //BindGrid();
            
        }

        protected void grdBuyingProductCategory_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {

        }
      
        protected void cpLineProduct_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            /*
            String[] aParam = e.Parameter.Split('|');
            
            switch (aParam[0])
            {
                case "new":

                    
                    Session["url"] = Guid.NewGuid();

                    uow = new UnitOfWork(ConnectionHelper.GetDataLayer(DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists));
                    
                    ProductSupplierXDS.Session = uow;
                    ProductSupplierXDS.Criteria = "[Language] = 'VN' And [ProductId] = @ProductId";
                    ProductSupplierXDS.CriteriaParameters.Add("@ProductId", System.Data.DbType.Guid, "00000000-0000-0000-0000-000000000000");

                    //grdSupplier.DataBind();
                    

                    break;

                case "edit":

                     
                    viewProduct = (ViewProduct)Session["productMode"];
                    if (viewProduct != null)
                    {
                        txtCode.Text = viewProduct.Code;
                        txtDescription.Text = viewProduct.Description;
                        txtName.Text = viewProduct.Name;

                        cboManufacturer.Value = viewProduct.ManufacturerCode;

                        cboRowStatus.Value = viewProduct.RowStatus.ToString();


                        uow = new UnitOfWork(ConnectionHelper.GetDataLayer(DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists));

                        ProductSupplierXDS.Session = uow;
                        ProductSupplierXDS.Criteria = "[Language] = 'VN' And [ProductId] = @ProductId";
                        
                        ProductSupplierXDS.CriteriaParameters.Clear();
                        ProductSupplierXDS.CriteriaParameters.Add("@ProductId", System.Data.DbType.Guid, viewProduct.ProductId.ToString());

                        //grdProductSupplier.DataSource = ProductSupplierXDS;
                        grdProductSupplier.DataBind();

                        for (int i = 0; i < grdProductSupplier.VisibleRowCount; i++)
                        {
                            String suppliercode = grdProductSupplier.GetRowValues(i, "SupplierCode").ToString();
                        }

                        //grdSupplier.DataBind();

                    }
                  
                    break;

                case "save":          
                                         
                    if (!validate())
                    {
                        return;
                    }

                    using (uow = new ExplicitUnitOfWork(ConnectionHelper.GetDataLayer(DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists)))
                    {
                        manufacturer = uow.FindObject<Manufacturer>(new BinaryOperator("Code", cboManufacturer.Value));

                        if (manufacturer == null)
                        {
                            return;
                        }

                        if (Session["productMode"] == null)
                        {
                            XPCollection<ProductSupplier> lproductSupplier = new XPCollection<ProductSupplier>();
                            //productSupplier = uow.FindObject<ProductSupplier>(new BinaryOperator(""));

                                                   
                            product = new Product(uow);
                            product.ProductId = Guid.NewGuid();
                            product.Code = txtCode.Text;
                            //product.ManufacturerId = manufacturer;
                            product.RowCreationTimeStamp = DateTime.Now;
                            product.RowStatus = Char.Parse(cboRowStatus.Value.ToString());

                            for (int i = 0; i < grdProductSupplier.VisibleRowCount; i++)
                            {
                                String suppliercode = grdProductSupplier.GetRowValues(i, "SupplierCode").ToString();
                                supplier = uow.FindObject<Supplier>(new BinaryOperator("Code", suppliercode));

                                if (supplier != null)
                                {
                                    productSupplier = new ProductSupplier(uow);
                                    productSupplier.SupplierId = supplier;
                                    productSupplier.ProductId = product;


                                }

                                //lproductSupplier.Add(productSupplier);
                            }



                            //for (int i = 0; i < grdSupplier.VisibleRowCount; i++)
                            //{
                            //    supplier = uow.FindObject<Supplier>(new BinaryOperator("SupplierCode", grdSupplier.GetRowValues(i, "SupplierCode")));

                            //    productSupplier = new ProductSupplier(uow);
                            //    productSupplier.SupplierId = supplier;

                            //    product.ProductSuppliers.Add(productSupplier);
                            //}

                            productProperty = new ProductProperty(uow);
                            productProperty.ProductId = product;
                            productProperty.Language = "VN";
                            productProperty.Name = txtName.Text;
                            productProperty.Description = txtDescription.Text;

                            productProperty = new ProductProperty(uow);
                            productProperty.ProductId = product;
                            productProperty.Language = "EN";
                            productProperty.Name = txtName.Text;
                            productProperty.Description = txtDescription.Text;

                        }
                        else
                        {
                            viewProduct = (ViewProduct)Session["productMode"];                          
                            product = uow.FindObject<Product>(new BinaryOperator("ProductId", viewProduct.ProductId));
                                                 
                            if (product != null)
                            {
                                product.Code = txtCode.Text;
                                //product.ManufacturerId = manufacturer;
                                product.RowStatus = Char.Parse(cboRowStatus.Value.ToString());

                                CriteriaOperator co = CriteriaOperator.Parse("[ProductId] = ? And Language = ?", viewProduct.ProductId, "VN");
                                productProperty  = uow.FindObject<ProductProperty>(new BinaryOperator("ProductId", product));

                                if (productProperty != null)
                                {
                                    productProperty.Name = txtName.Text;
                                    productProperty.Description = txtDescription.Text;
                                }
                            }
                        }

                        uow.CommitChanges();

                        Session["productMode"] = null;                        

                        formProductEdit.ShowOnPageLoad = false;
                    }

                    break;

                case "view":
                    uow = new UnitOfWork(ConnectionHelper.GetDataLayer(DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists));
                    Guid guid = new Guid(aParam[1]);

                    viewProduct = uow.FindObject<ViewProduct>(new BinaryOperator("ProductId", guid));

                    if (viewProduct != null)
                    {
                        txtCode.Text = viewProduct.Code;
                        txtDescription.Text = viewProduct.Description;
                        txtName.Text = viewProduct.Name;

                        cboRowStatus.Value = viewProduct.RowStatus;
                        cboManufacturer.Value = viewProduct.ManufacturerCode;

                        txtCode.ReadOnly = true;
                        txtDescription.ReadOnly = true;
                        txtName.ReadOnly = true;
                        cboRowStatus.ReadOnly = true;
                        cboManufacturer.ReadOnly = true;
                        buttonAccept.Enabled = false;
                    }

                    formProductEdit.ShowOnPageLoad = true;

                    break;
                default:
                    break;
            }
            */
        }

        protected void cboManufacturerProduct_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            /*
            string sql = "select result.Code, result.Name" +
                            " from (select a.Code, b.Name, row_number() over(order by a.Code) as rowno" +             
	                              " from dbo.Manufacturer a, dbo.ManufacturerProperty b" +
                                  " where a.ManufacturerId = b.ManufacturerId" +
	                              " and b.Language = @lang" +
                                  " and ((a.Code LIKE @filter or b.Name like @filter))) as result" +
                            " where result.rowno between @startIndex and @endIndex";

            ManufacturerSDS.SelectCommand = sql;
            
            ManufacturerSDS.SelectParameters.Clear();
            ManufacturerSDS.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            ManufacturerSDS.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            ManufacturerSDS.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            ManufacturerSDS.SelectParameters.Add("lang", TypeCode.String, "VN");

            cboManufacturer.DataSource = ManufacturerSDS;
            cboManufacturer.DataBind();
             */
        }

        protected void cboManufacturerProduct_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            /*
            if (e.Value == null)
            {
                return;
            }

            string sql = "select result.Code, result.Name" +
                        " from (select a.Code, b.Name" +
                              " from dbo.Manufacturer a, dbo.ManufacturerProperty b" +
                              " where a.ManufacturerId = b.ManufacturerId" +
                              " and b.Language = @lang" +
                              " and ((a.Code = @filter))) as result";                    

            ManufacturerSDS.SelectCommand = sql;

            ManufacturerSDS.SelectParameters.Add("filter", TypeCode.String, e.Value.ToString());
            ManufacturerSDS.SelectParameters.Add("lang", TypeCode.String, "VN");

            cboManufacturer.DataSource = ManufacturerSDS;
            cboManufacturer.DataBind();
            */
        }

        protected void colSupplierCode_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            /*
            ASPxComboBox combo = (ASPxComboBox)source;

            string sql = "select result.Code, result.Name" +
                          " from (select a.Code, b.Name, row_number() over(order by a.Code) as rowno" +
                                " from dbo.Supplier a, dbo.SupplierProperty b" +
                                " where a.SupplierId = b.SupplierId" +
                                " and b.Language = @lang" +
                                " and ((a.Code LIKE @filter or b.Name like @filter))) as result" +
                          " where result.rowno between @startIndex and @endIndex";

            SupplierSDS.SelectCommand = sql;

            SupplierSDS.SelectParameters.Clear();
            SupplierSDS.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            SupplierSDS.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            SupplierSDS.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            SupplierSDS.SelectParameters.Add("lang", TypeCode.String, "VN");

            combo.DataSource = SupplierSDS;
            combo.DataBind();
             */
        }

        protected void colSupplierCode_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            /*
            ASPxComboBox combo = (ASPxComboBox) source;

            if (e.Value == null)
            {
                return;
            }

            string sql = "select result.Code, result.Name" +
                        " from (select a.Code, b.Name" +
                              " from dbo.Supplier a, dbo.SupplierProperty b" +
                              " where a.SupplierId = b.SupplierId" +
                              " and b.Language = @lang" +
                              " and ((a.Code = @filter))) as result";

            SupplierSDS.SelectCommand = sql;

            SupplierSDS.SelectParameters.Clear();
            SupplierSDS.SelectParameters.Add("filter", TypeCode.String, e.Value.ToString());
            SupplierSDS.SelectParameters.Add("lang", TypeCode.String, "VN");

            combo.DataSource = SupplierSDS;
            combo.DataBind();
             */
        }
        
        protected void txtCodeProduct_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
            /*
            e.IsValid = validate();
            if (txtCode.ValidationSettings.ErrorText == "")
            {
                e.IsValid = true;
            }
             */
        }

        protected void txtNameProduct_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
            /*
            e.IsValid = validate();
            if (txtName.ValidationSettings.ErrorText == "")
            {
                e.IsValid = true;
            }
             */
        }

           
        protected void grdProductSupplier_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {

        }
       
        protected void grdProductSupplier_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

        }

        protected void grdProductSupplier_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void ProductSupplierXDS_Inserted(object sender, XpoDataSourceInsertedEventArgs e)
        {
            //viewProductSupplier = (ViewProductSupplier)e.Value;

            //uow.Save(viewProductSupplier);
        }

        protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
        {

        }

        protected void ASPxNavBar13232_ItemClick(object source, DevExpress.Web.ASPxNavBar.NavBarItemEventArgs e)
        {

        }
      


    }
}