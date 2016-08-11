using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.GUI.Sales.userControl
{
    public partial class uEditPromotionPolicy : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grv_PromotionLevels.DataSource = new[] {  new{ID = 1,ten = "Mức khuyến mãi 1",diengiai = "level 1"},
                                            new{ID = 2,ten = "Mức khuyến mãi 2",diengiai = "level 2"},
                                            new{ID = 3,ten = "Mức khuyến mãi 3",diengiai = "level 3"}
                                    };
            grv_PromotionLevels.DataBind();

        }
    }
}