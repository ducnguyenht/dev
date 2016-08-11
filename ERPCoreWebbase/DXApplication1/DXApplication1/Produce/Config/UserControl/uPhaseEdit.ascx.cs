using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Produce.Config.UserControl
{
    public partial class uPhaseEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // ASPxGridView gridview_UserPhaseMaterial = ASPxNavBar1.FindDetailRowTemplateControl(e.VisibleIndex, "gridview_detail") as ASPxGridView;
              ASPxGridView gridview_UserPhaseMaterial = ASPxNavBar1.Groups[0].Items[0].FindControl("grdataUserPhaseMaterial") as ASPxGridView;
              gridview_UserPhaseMaterial.DataSource = new[] { new { PhaseMaterialID = "NVL001", PhaseMaterialName = " Nguyên Vật Liệu 1",PhaseMaterialUnit = "Example 1", PhaseMaterialQuantity = "5", Price = ".........", Total = ".........", LossRate = "1"},
                                             new { PhaseMaterialID = "NVL001", PhaseMaterialName = " Nguyên Vật Liệu 1",PhaseMaterialUnit = "Example 1", PhaseMaterialQuantity = "5", Price = ".........", Total = ".........", LossRate = "0,5"},
                                               new { PhaseMaterialID = "NVL002", PhaseMaterialName = " Nguyên Vật Liệu 2",PhaseMaterialUnit = "Example 2", PhaseMaterialQuantity = "5", Price = ".........", Total = ".........", LossRate = "1,1"},
                                                new { PhaseMaterialID = "NVL003", PhaseMaterialName = " Nguyên Vật Liệu 3",PhaseMaterialUnit = "Example 3", PhaseMaterialQuantity = "5", Price = ".........", Total = ".........", LossRate = "1,3"}
                 };
              gridview_UserPhaseMaterial.DataBind();
              ASPxGridView gridview_UserUnFinishedProduct = ASPxNavBar1.Groups[1].Items[0].FindControl("grdataUserUnFinishedProduct") as ASPxGridView;
              gridview_UserUnFinishedProduct.DataSource = new[] { new { PhaseUnFinishedProductID = "SP001", PhaseUnFinishedProductName = " Sản Phẩm Dang Dở 1", PhaseUnFinishedQuantity = "20" , PhaseUnFinishedUnit ="Example",  Price = ".........", Total = ".........", LossRate = "1"},
                                           new { PhaseUnFinishedProductID = "SP002", PhaseUnFinishedProductName = " Sản Phẩm Dang Dở 2", PhaseUnFinishedQuantity = "20" , PhaseUnFinishedUnit ="Example",  Price = ".........", Total = ".........", LossRate = "1"},
                                new { PhaseUnFinishedProductID = "SP003", PhaseUnFinishedProductName = " Sản Phẩm Dang Dở 3", PhaseUnFinishedQuantity = "20" , PhaseUnFinishedUnit ="Example",  Price = ".........", Total = ".........", LossRate = "1"},
                                  new { PhaseUnFinishedProductID = "SP004", PhaseUnFinishedProductName = " Sản Phẩm Dang Dở 4", PhaseUnFinishedQuantity = "20" , PhaseUnFinishedUnit ="Example",  Price = ".........", Total = ".........", LossRate = "1"},
            };
              gridview_UserUnFinishedProduct.DataBind();
              ASPxGridView gridview_UserPhaseProduct = ASPxNavBar1.Groups[2].Items[0].FindControl("grdataUserPhaseProduct") as ASPxGridView;
              gridview_UserPhaseProduct.DataSource = new[] { new { PhaseProductID = "HH001", PhaseProductName = "Hàng Hóa 1",PhaseProductDescription = "Example 1", PhaseProductQuantity= "20" , PhaseProductUnit = "Example",  Price = ".........", Total = ".........", LossRate = "1"},
                                             new { PhaseProductID = "HH002", PhaseProductName = "Hàng Hóa 2",PhaseProductDescription = "Example 2" , PhaseProductQuantity= "20",  PhaseProductUnit = "Example",  Price = ".........", Total = ".........", LossRate = "1"},
                                 new { PhaseProductID = "HH003", PhaseProductName = "Hàng Hóa 3",PhaseProductDescription = "Example 3" , PhaseProductQuantity= "20" , PhaseProductUnit = "Example",  Price = ".........", Total = ".........", LossRate = "1"},
                                 new { PhaseProductID = "HH004", PhaseProductName = "Hàng Hóa 4",PhaseProductDescription = "Example 4" , PhaseProductQuantity= "20",  PhaseProductUnit = "Example",  Price = ".........", Total = ".........", LossRate = "1"}
            };
              gridview_UserPhaseProduct.DataBind();

              ASPxGridView gridview_UserUnFinishedProductOut = ASPxNavBar2.Groups[0].Items[0].FindControl("grdataUserUnFinishedProductOut") as ASPxGridView;
              gridview_UserUnFinishedProductOut.DataSource = new[] { new { PhaseUnFinishedProductID = "SP001", 
                    PhaseUnFinishedProductName = " Sản Phẩm Dang Dở 1",
                    PhaseUnFinishedProductQuantity = " 20", PhaseUnFinishedProductUnit = "Example", Price = "........."}        
            };
              gridview_UserUnFinishedProductOut.DataBind();


              ASPxGridView gridview_UserPhaseProductOut = ASPxNavBar2.Groups[1].Items[0].FindControl("grdataUserPhaseProductOut") as ASPxGridView;
              gridview_UserPhaseProductOut.DataSource = new[] { new { PhaseProductID = "HH001", PhaseProductName = "Hàng Hóa 1",
                  PhaseProductDescription = "Example 1", PhaseProductQuantity = " 20", 
                  PhaseProductUnit = "Example", Price = "........."}
              };
              gridview_UserPhaseProductOut.DataBind();


            grdataUserPeople.DataSource = new[] { new { BacNghe = "Example 1", SoLuong = "10",ThoiLuong = "Example 1", Price = ".........", GhiChu ="Example 1"},
                                            new { BacNghe = "Example 2", SoLuong = "10",ThoiLuong = "Example 2", Price = ".........", GhiChu ="Example 2"},
                                  new { BacNghe = "Example 3", SoLuong = "10",ThoiLuong = "Example 3", Price = ".........", GhiChu ="Example 3"},
                                 new { BacNghe = "Example 4", SoLuong = "10",ThoiLuong = "Example 4", Price = ".........", GhiChu ="Example 4"}
            };
            grdataUserPeople.DataBind();

            grdataUserCompany.DataSource = new[] { new { code = "Example 1", name = "Example 1",Time = "Example 1", CongSuat ="Example 1", ChiPhiVanHanh = "Example 1", GhiChu = "Example 1"},
                              new { code = "Example 2", name = "Example 2",Time = "Example 2", CongSuat ="Example 2", ChiPhiVanHanh = "Example 2", GhiChu = "Example 2"},
                                new { code = "Example 3", name = "Example 3",Time = "Example 3", CongSuat ="Example 3", ChiPhiVanHanh = "Example 3", GhiChu = "Example 3"},
                                new { code = "Example 4", name = "Example 4",Time = "Example 4", CongSuat ="Example 4", ChiPhiVanHanh = "Example 4", GhiChu = "Example 4"},
            };
            grdataUserCompany.DataBind();

            grdataUserDevice.DataSource = new[] { new { code = "Example 1", name = "Example 1",SoLuong = "20", DonViTinh ="Example 1",  Price = ".........", Total = ".........", GhiChu = "Example 1"},
                                new { code = "Example 2", name = "Example 2",SoLuong =  "20", DonViTinh ="Example 2", Price = ".........", Total = ".........", GhiChu = "Example 2"},
                                new { code = "Example 3", name = "Example 3",SoLuong = "20", DonViTinh ="Example 3",  Price = ".........", Total = ".........", GhiChu = "Example 3"},
                                new { code = "Example 4", name = "Example 4",SoLuong = "20", DonViTinh ="Example 4",  Price = ".........", Total = ".........", GhiChu = "Example 4"},
            };
            grdataUserDevice.DataBind();
        }

        protected void cbpanelUserPhase_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }

        protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
        {

        }
    }
}