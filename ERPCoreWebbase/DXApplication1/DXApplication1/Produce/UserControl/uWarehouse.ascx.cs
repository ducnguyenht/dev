using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Web.ASPxEditors;

namespace WebModule.Produce.UserControl
{
    public partial class uWarehouse : System.Web.UI.UserControl
    {
        private bool validate()
        {
            bool isValid = true;

            return isValid;
        }

        private void BindGrid()
        {
            var datasource = new[]
            {
                new { OrganizationId=1, ParentOrganizationId=0, code="LT001", name="Khu 1", Description="Khu 1 là đơn vị lưu trữ cao nhất", amount=""  },
                new { OrganizationId=2, ParentOrganizationId=1, code="LT002",name="Dãy", Description="Mỗi Khu 1 có 5 Dãy", amount="5"  },
                new { OrganizationId=3, ParentOrganizationId=2, code="LT003",name="Kệ", Description="Mỗi Dãy có 7 Kệ", amount="7"  },
                new { OrganizationId=4, ParentOrganizationId=3, code="LT004",name="Học", Description="Mỗi Kệ có 10 Học", amount="10"  }
            };
            ASPxTreeList1.DataSource = datasource;
            ASPxTreeList1.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            BindGrid();
            

        }

        protected void grdBuyingProductCategory_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {

        }

        protected void cpLine_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            String[] aParam = e.Parameter.Split('|');

            switch (aParam[0])
            {
                case "new":
                    Session["url"] = Guid.NewGuid();


                    //grdSupplier.DataBind();

                    break;

                case "edit":

                    break;

                case "save":

                    if (!validate())
                    {
                        return;
                    }




                    break;

                case "view":

                    break;
                default:
                    break;
            }
        }

        protected void cboManufacturer_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
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

        }

        protected void cboManufacturer_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
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


        }

        protected void colSupplierCode_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
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
        }

        protected void colSupplierCode_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox combo = (ASPxComboBox)source;

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
        }

        protected void txtCode_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
            e.IsValid = validate();
            
        }

        protected void txtName_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
            e.IsValid = validate();
            
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


            //uow.Save(viewProductSupplier);
        }

        protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
        {

        }
    }
}