using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Accounting.UserControl
{
    public partial class uChungTuGoc_2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView_ctg.DataSource = new[] {  new{ID = "CT001", Name = "Chứng từ A", Sign = "CT01/SNL",Total = "1.000.000",CN = "500.000",Tax = "0%"},
                                                new{ID = "CT002", Name = "Chứng từ B", Sign = "CT02/SNL",Total = "2.000.000",CN = "1.500.000",Tax = "Không chịu thuế"},
                                                new{ID = "CT003", Name = "Chứng từ C", Sign = "CT03/SNL",Total = "3.000.000",CN = "800.000",Tax = "5%"},
                                                new{ID = "CT004", Name = "Chứng từ D", Sign = "CT04/SNL",Total = "4.000.000",CN = "2.500.000",Tax = "10%"}};
            ASPxGridView_ctg.KeyFieldName = "ID";
            ASPxGridView_ctg.DataBind();
        }

    }
}