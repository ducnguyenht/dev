using System.Collections;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;
using DevExpress.Utils;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.Web.Mvc;
using DevExpress.XtraPivotGrid;
using DevExpressMvcApplication1.Models;

namespace DevExpressMvcApplication1.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View(GetSalesPersonData(null));
        }

        [HttpPost]
        public ActionResult Index(string filter) {
            ViewBag.Filter = filter;
            var data = GetSalesPersonData(filter);
            if(Request["Export"] != null) {
                return PivotGridExtension.ExportToPdf(PivotGridHelper.CreateGeneralSettings(), data);
            } else {
                return View(data);
            }
        }

        public ActionResult PivotGridPartial(string filter) {
            ViewBag.Filter = filter;
            return PartialView("PivotGridPartial", GetSalesPersonData(filter));
        }

        static IEnumerable GetSalesPersonData(string filter) {
            NorthWindDataContext dc = new NorthWindDataContext();
            return dc.Invoices.Where(i => string.IsNullOrEmpty(filter) || i.Country == filter);
        }
    }

    public class PivotGridHelper {
        static HttpSessionState Session {
            get { return System.Web.HttpContext.Current.Session; }
        }
        public static PivotGridSettings CreateGeneralSettings() {
            var settings = new PivotGridSettings();
            settings.Name = "PivotGrid";
            settings.OptionsView.ShowHorizontalScrollBar = true;
            settings.Width = new System.Web.UI.WebControls.Unit(90, System.Web.UI.WebControls.UnitType.Percentage);
            settings.Fields.Add("Country", PivotArea.ColumnArea);
            settings.Fields.Add("City", PivotArea.FilterArea);

            settings.Fields.Add("ExtendedPrice", PivotArea.DataArea);
            settings.Fields.Add("Freight", PivotArea.FilterArea);
            settings.Fields.Add("Quantity", PivotArea.FilterArea);
            settings.Fields.Add(field => {
                field.Area = PivotArea.RowArea;
                field.FieldName = "OrderDate";
                field.GroupInterval = PivotGroupInterval.DateYear;
                field.UnboundFieldName = "DateYear";
                field.Caption = "Year";
            });
            settings.Fields.Add(field => {
                field.Area = PivotArea.RowArea;
                field.FieldName = "OrderDate";
                field.GroupInterval = PivotGroupInterval.DateQuarter;
                field.UnboundFieldName = "DateQuarter";
                field.Caption = "Quarter";

            });
            settings.Fields.Add(field => {
                field.Area = PivotArea.RowArea;
                field.FieldName = "OrderDate";
                field.GroupInterval = PivotGroupInterval.DateMonth;
                field.UnboundFieldName = "DateMonth";
                field.Caption = "Month";
                field.UseNativeFormat = DefaultBoolean.False;
            });

            // Saves layout after change
            settings.GridLayout = (sender, e) => {
                MVCxPivotGrid PivotGrid = sender as MVCxPivotGrid;
                Session["Layout"] = PivotGrid.SaveLayoutToString(PivotGridWebOptionsLayout.DefaultLayout);
            };

            // Loads layout after change
            settings.PreRender = (sender, e) => {
                MVCxPivotGrid PivotGrid = sender as MVCxPivotGrid;
                string layout = Session["Layout"] as string;
                if(!string.IsNullOrEmpty(layout)) {
                    PivotGrid.LoadLayoutFromString(layout, DevExpress.Web.ASPxPivotGrid.PivotGridWebOptionsLayout.DefaultLayout);
                }
            };
            return settings;
        }
    }
}