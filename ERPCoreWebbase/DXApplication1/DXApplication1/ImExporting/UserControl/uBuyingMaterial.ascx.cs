using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Web.ASPxEditors;

namespace DXApplication1.ImExporting.UserControl
{
    public partial class uBuyingMaterial : System.Web.UI.UserControl
    {
        private bool validate()
        {
            bool isValid = true;

            return isValid;
        }

        private void BindGrid()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack || !Page.IsCallback)
            {
                BindGrid();
            }
            else
            {

            }

        }

        protected void grdBuyingProductCategory_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {

        }

        protected void cpLineMaterial_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        protected void cboManufacturerMaterial_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            string sql = "select result.Code, result.Name" +
                            " from (select a.Code, b.Name, row_number() over(order by a.Code) as rowno" +
                                  " from dbo.Manufacturer a, dbo.ManufacturerProperty b" +
                                  " where a.ManufacturerId = b.ManufacturerId" +
                                  " and b.Language = @lang" +
                                  " and ((a.Code LIKE @filter or b.Name like @filter))) as result" +
                            " where result.rowno between @startIndex and @endIndex";

            /*ManufacturerSDS.SelectCommand = sql;

            ManufacturerSDS.SelectParameters.Clear();
            ManufacturerSDS.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            ManufacturerSDS.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            ManufacturerSDS.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            ManufacturerSDS.SelectParameters.Add("lang", TypeCode.String, "VN");

            cboManufacturer.DataSource = ManufacturerSDS;*/
         
        }

        protected void cboManufacturerMaterial_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
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

            /*ManufacturerSDS.SelectCommand = sql;

            ManufacturerSDS.SelectParameters.Add("filter", TypeCode.String, e.Value.ToString());
            ManufacturerSDS.SelectParameters.Add("lang", TypeCode.String, "VN");

            cboManufacturer.DataSource = ManufacturerSDS;*/
          

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

            /*SupplierSDS.SelectCommand = sql;

            SupplierSDS.SelectParameters.Clear();
            SupplierSDS.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            SupplierSDS.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            SupplierSDS.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            SupplierSDS.SelectParameters.Add("lang", TypeCode.String, "VN");

            combo.DataSource = SupplierSDS;*/
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

            /*SupplierSDS.SelectCommand = sql;

            SupplierSDS.SelectParameters.Clear();
            SupplierSDS.SelectParameters.Add("filter", TypeCode.String, e.Value.ToString());
            SupplierSDS.SelectParameters.Add("lang", TypeCode.String, "VN");

            combo.DataSource = SupplierSDS;*/
            combo.DataBind();
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
    }
}