using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using Utility;

namespace DXApplication1.GUI
{
    public partial class PromotionPolicy : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var qr = new[]{   new{ma = "CT0001", ten = "Chương trình 1",hltu = "01/06/2013",hlden = "30/06/2013",diengiai = "Khuyến mãi tháng 6",trangthai = "Đã kết thúc"},
                              new{ma = "CT0002", ten = "Chương trình 2",hltu = "01/07/2013",hlden = "31/07/2013",diengiai = "Khuyến mãi tháng 7",trangthai = "Đang hoạt động"},
                              new{ma = "CT0003", ten = "Chương trình 3",hltu = "01/08/2013",hlden = "31/08/2013",diengiai = "Khuyến mãi tháng 8",trangthai = "Dự kiến"}};
            grv_promotionPolicy.DataSource = qr.ToList();
            grv_promotionPolicy.DataBind();
        }

        protected void grv_subPromotion_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grv_subPromotion = (ASPxGridView)sender;
            grv_subPromotion.Load += new EventHandler(gridview2_Load);
            grv_subPromotion.DataSource = new[] {  new{ID = 1,ten = "Mức khuyến mãi 1",diengiai = "level 1"},
                                            new{ID = 2,ten = "Mức khuyến mãi 2",diengiai = "level 2"},
                                            new{ID = 3,ten = "Mức khuyến mãi 3",diengiai = "level 3"}
                                    };
            grv_subPromotion.KeyFieldName = "ID";
        }

        void gridview2_Load(object sender, EventArgs e)
        {
            ASPxGridView gridview2 = (ASPxGridView)sender;
            
            gridview2.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_SALES_PROMOTION_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_SALES_GROUPID; }
        }
    }
}