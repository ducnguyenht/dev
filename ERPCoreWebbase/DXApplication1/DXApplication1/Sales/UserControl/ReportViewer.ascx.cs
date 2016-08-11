using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERPCore.Sales.Report;
using DevExpress.Xpo;
using NAS.DAL.Invoice;
using NAS.DAL.Nomenclature.Item;
using System.Data;
using System.Data.SqlClient;
using Utility;
using NAS.DAL;
using DevExpress.Data.Filtering;

namespace ERPCore.Sales.UserControl
{
    public partial class ReportViewer : System.Web.UI.UserControl
    {
        Session session;


        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (hReportBillId.Contains("id"))
            {

                session = NAS.DAL.XpoHelper.GetNewSession();

                XPQuery<BillItem> billItem = new XPQuery<BillItem>(session);
                XPQuery<ItemUnit> itemUnit = new XPQuery<ItemUnit>(session);
                XPQuery<Item> item = new XPQuery<Item>(session);

                Guid billId = Guid.Parse(hReportBillId.Get("id").ToString());
                var list = billItem.Where(b => b.BillId.BillId == billId).Select(b => new C01GTKT3_001()
                {
                    BillId = b.BillId.BillId,
                    BuyerCode = b.BillId.TargetOrganizationId.Code,
                    BuyerName = b.BillId.TargetOrganizationId.Name,
                    CustomerCode = b.BillId.SourceOrganizationId.Code,
                    CustomerName = b.BillId.SourceOrganizationId.Name,
                    CustomerTax = b.BillId.SourceOrganizationId.TaxNumber,
                    CustomerAddress = b.BillId.SourceOrganizationId.Address,
                    Code = b.ItemUnitId.ItemId.Code,
                    Name = b.ItemUnitId.ItemId.Name,
                    Unit = b.ItemUnitId.UnitId.Code,
                    Quantity = b.Quantity,
                    Price = b.Price,
                    Amount = b.TotalPrice,
                    PromotionInPercentage = b.PromotionInPercentage,
                    PromotionInNumber = b.BillId.BillPromotions.Max(p => p.PromotionInNumber),
                    PriceA = b.PromotionInPercentage > 0 ? b.Price * (1 - b.PromotionInPercentage / 100) : b.Price,
                    TaxInPercentage = b.ItemUnitId.ItemId.ItemTaxes.Max(x => x.TaxId.Percentage)/100,
                    TaxInNumber = b.BillId.SumOfTax,
                    Total = b.BillId.Total,
                    TotalByString = Utility.Accounting.NumberToString(b.BillId.Total),
                    CreateDate = b.BillId.IssuedDate,
                    AmountA = b.PromotionInPercentage > 0 ? b.Price * (1 - b.PromotionInPercentage / 100) * b.Quantity : b.TotalPrice

                });

                List<C01GTKT3_001> lst = new List<C01GTKT3_001>();

                try
                {
                    lst = list.ToList();
                }
                catch
                {
                }

                _01GTKT3_001 report = new _01GTKT3_001();

                report.DataSource = lst;
                report.DataMember = "";

                rptViewer.Report = report;

                hReportBillId.Remove("id");

                formReportViewer.JSProperties.Add("cpShowReport", "report");
              
            }
        }

        protected void formReportViewer_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {

        }
    }
}