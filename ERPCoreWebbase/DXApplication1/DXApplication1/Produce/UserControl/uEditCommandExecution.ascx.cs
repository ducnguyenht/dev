using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Produce.UserControl
{
    public partial class uEditCommandExecution : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //navbarInput
            ASPxGridView gridview_ExecutionMaterial = nvbInput.Groups[0].Items[0].FindControl("grdataExecutionMaterial") as ASPxGridView;
            gridview_ExecutionMaterial.DataSource = new[] { new { ExecutionMaterialID = "NVL001", ExecutionMaterialName = " Nguyên Vật Liệu 1",ExecutionMaterialUnit = "Example 1", ExecutionMaterialQuantity = "5" , ExecutionMaterialCost="10000"},
                                             new { ExecutionMaterialID = "NVL001", ExecutionMaterialName = " Nguyên Vật Liệu 1",ExecutionMaterialUnit = "Example 1", ExecutionMaterialQuantity = "5" , ExecutionMaterialCost="10000"},
                                               new { ExecutionMaterialID = "NVL002", ExecutionMaterialName = " Nguyên Vật Liệu 2",ExecutionMaterialUnit = "Example 2", ExecutionMaterialQuantity = "5" , ExecutionMaterialCost="10000"},
                                                new { ExecutionMaterialID = "NVL003", ExecutionMaterialName = " Nguyên Vật Liệu 3",ExecutionMaterialUnit = "Example 3", ExecutionMaterialQuantity = "5" , ExecutionMaterialCost="10000"}
                 };
            gridview_ExecutionMaterial.DataBind();
            ASPxGridView gridview_ExecutionUnFinishedProduct = nvbInput.Groups[1].Items[0].FindControl("grdataExecutionUnFinishedProduct") as ASPxGridView;
            gridview_ExecutionUnFinishedProduct.DataSource = new[] { new { ExecutionUnFinishedProductID = "SP001", ExecutionUnFinishedProductName = " Sản Phẩm Dang Dở 1",ExecutionUnFinishedParcel = "20" , ExecutionUnFinishedQuantity= "20", ExecutionUnFinishedUnit = "Example", ExecutionUnFinishedCost ="10000"},
                                           new { ExecutionUnFinishedProductID = "SP002", ExecutionUnFinishedProductName = " Sản Phẩm Dang Dở 2",ExecutionUnFinishedParcel = "20" , ExecutionUnFinishedQuantity= "20", ExecutionUnFinishedUnit = "Example", ExecutionUnFinishedCost ="10000"},
                                new { ExecutionUnFinishedProductID = "SP003", ExecutionUnFinishedProductName = " Sản Phẩm Dang Dở 3",ExecutionUnFinishedParcel = "20" , ExecutionUnFinishedQuantity= "20", ExecutionUnFinishedUnit = "Example", ExecutionUnFinishedCost ="10000"},
                                  new { ExecutionUnFinishedProductID = "SP004",ExecutionUnFinishedProductName = " Sản Phẩm Dang Dở 4",ExecutionUnFinishedParcel = "20" , ExecutionUnFinishedQuantity= "20", ExecutionUnFinishedUnit = "Example", ExecutionUnFinishedCost ="10000"},
            };
            gridview_ExecutionUnFinishedProduct.DataBind();
            ASPxGridView gridview_ExecutionProduct = nvbInput.Groups[2].Items[0].FindControl("grdataExecutionProduct") as ASPxGridView;
            gridview_ExecutionProduct.DataSource = new[] { new { ExecutionProductID = "HH001",ExecutionProductName = "Hàng Hóa 1",ExecutionProductDescription = "Example 1" , ExecutionProductQuantity= "20",ExecutionProductUnit="Example", ExecutionProductCost="10000"},
                                             new { ExecutionProductID = "HH002", ExecutionProductName = "Hàng Hóa 2",ExecutionProductDescription = "Example 2" , ExecutionProductQuantity= "20",ExecutionProductUnit="Example", ExecutionProductCost="10000"},
                                 new {ExecutionProductID = "HH003", ExecutionProductName = "Hàng Hóa 3",ExecutionProductDescription = "Example 3" , ExecutionProductQuantity= "20",ExecutionProductUnit="Example", ExecutionProductCost="10000"},
                                 new { ExecutionProductID = "HH004", ExecutionProductName = "Hàng Hóa 4",ExecutionProductDescription = "Example 4" , ExecutionProductQuantity= "20",ExecutionProductUnit="Example", ExecutionProductCost="10000"}
            };
            gridview_ExecutionProduct.DataBind();

            //navbar


            grdataExecutionPeople.DataSource = new[] { new { BacNghe = "Example 1", SoLuong = "10",ThoiLuong = "Example 1", GhiChu ="Example 1" ,ChiPhi = "10000"},
                                            new { BacNghe = "Example 2", SoLuong = "10",ThoiLuong = "Example 2", GhiChu ="Example 2"  ,ChiPhi = "10000"},
                                  new { BacNghe = "Example 3", SoLuong = "10",ThoiLuong = "Example 3", GhiChu ="Example 3"  ,ChiPhi = "10000"},
                                 new { BacNghe = "Example 4", SoLuong = "10",ThoiLuong = "Example 4", GhiChu ="Example 4"  ,ChiPhi = "10000"}
            };
            grdataExecutionPeople.DataBind();

            grdataExecutionCompany.DataSource = new[] { new { code = "Example 1", name = "Example 1",Time = "Example 1", CongSuat ="Example 1", ChiPhiVanHanh = "Example 1", GhiChu = "Example 1"},
                              new { code = "Example 2", name = "Example 2",Time = "Example 2", CongSuat ="Example 2", ChiPhiVanHanh = "Example 2", GhiChu = "Example 2"},
                                new { code = "Example 3", name = "Example 3",Time = "Example 3", CongSuat ="Example 3", ChiPhiVanHanh = "Example 3", GhiChu = "Example 3"},
                                new { code = "Example 4", name = "Example 4",Time = "Example 4", CongSuat ="Example 4", ChiPhiVanHanh = "Example 4", GhiChu = "Example 4"},
            };
            grdataExecutionCompany.DataBind();

            grdataExecutionDevice.DataSource = new[] { new { code = "Example 1", name = "Example 1",SoLuong = "20", DonViTinh ="Example 1",  GhiChu = "Example 1"  ,ChiPhi = "10000"},
                                new { code = "Example 2", name = "Example 2",SoLuong =  "20", DonViTinh ="Example 2", GhiChu = "Example 2"  ,ChiPhi = "10000"},
                                new { code = "Example 3", name = "Example 3",SoLuong = "20", DonViTinh ="Example 3",  GhiChu = "Example 3"  ,ChiPhi = "10000"},
                                new { code = "Example 4", name = "Example 4",SoLuong = "20", DonViTinh ="Example 4",  GhiChu = "Example 4" ,ChiPhi = "10000"},
            };
            grdataExecutionDevice.DataBind();

            //navibar 2
            ASPxGridView gridview_ExecutionUnFinishedProductOut = nvbOutput.Groups[0].Items[0].FindControl("grdataExecutionUnFinishedProductOut") as ASPxGridView;
            gridview_ExecutionUnFinishedProductOut.DataSource = new[] { new { ExecutionUnFinishedProductID = "SP001", ExecutionUnFinishedProductName = " Sản Phẩm Dang Dở 1",ExecutionUnFinishedParcel = "20" , ExecutionUnFinishedQuantity = "20", ExecutionUnFinishedUnit="Example", ExecutionUnFinishedCost="10000"},
                                //           new { ExecutionUnFinishedProductID = "SP002", ExecutionUnFinishedProductName = " Sản Phẩm Dang Dở 2",ExecutionUnFinishedParcel = "20"  , ExecutionUnFinishedQuantity = "20", ExecutionUnFinishedUnit="Example", ExecutionUnFinishedCost="10000"},
                                //new { ExecutionUnFinishedProductID = "SP003", ExecutionUnFinishedProductName = " Sản Phẩm Dang Dở 3",ExecutionUnFinishedParcel = "20"  , ExecutionUnFinishedQuantity = "20", ExecutionUnFinishedUnit="Example", ExecutionUnFinishedCost="10000"},
                                //  new { ExecutionUnFinishedProductID = "SP004", ExecutionUnFinishedProductName = " Sản Phẩm Dang Dở 4",ExecutionUnFinishedParcel = "20"  , ExecutionUnFinishedQuantity = "20", ExecutionUnFinishedUnit="Example", ExecutionUnFinishedCost="10000"},
            };
            gridview_ExecutionUnFinishedProductOut.DataBind();


            //ASPxGridView gridview_ExecutionProductOut = nvbOutput.Groups[1].Items[0].FindControl("grdataExecutionProductOut") as ASPxGridView;
            //gridview_ExecutionProductOut.DataSource = new[] { new { ExecutionProductID = "HH001", ExecutionProductName = "Hàng Hóa 1",ExecutionProductDescription = "Example 1", ExecutionProductQuantity = "20", ExecutionProductUnit = "Example", ExecutionProductCost = "10000"},
            ////                                 new { ExecutionProductID = "HH002", ExecutionProductName = "Hàng Hóa 2",ExecutionProductDescription = "Example 2", ExecutionProductQuantity = "20", ExecutionProductUnit = "Example", ExecutionProductCost = "10000"},
            ////                     new { ExecutionProductID = "HH003", ExecutionProductName = "Hàng Hóa 3",ExecutionProductDescription = "Example 3" , ExecutionProductQuantity = "20", ExecutionProductUnit = "Example", ExecutionProductCost = "10000"},
            ////                     new { ExecutionProductID = "HH004", ExecutionProductName = "Hàng Hóa 4",ExecutionProductDescription = "Example 4" , ExecutionProductQuantity = "20", ExecutionProductUnit = "Example", ExecutionProductCost = "10000"}
            //};
            //gridview_ExecutionProductOut.DataBind();
        }
    }
}