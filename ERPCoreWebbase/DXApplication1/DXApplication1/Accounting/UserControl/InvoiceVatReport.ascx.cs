using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebModule.Accounting.Report;
using DevExpress.Xpo;
using NAS.DAL.Invoice;
using NAS.DAL;
using Utility;
using DevExpress.Data.Filtering;

namespace WebModule.Accounting.UserControl
{
    public partial class InvoiceVatReport : System.Web.UI.UserControl
    {

        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ReportHiddenField.Contains("typeReport"))
            {
                XPQuery<BillTaxClaim> billTaxClaim = new XPQuery<BillTaxClaim>(session);
                XPQuery<Bill> bill = new XPQuery<Bill>(session);
                XPQuery<SaleInvoiceArtiface> saleInvoiceArtiface = new XPQuery<SaleInvoiceArtiface>(session);

                char vatType = (ReportHiddenField.Get("typeReport").ToString() == "01-1/GTGT" ? Constant.VAT_OUT : Constant.VAT_IN);

                var list = from btc in billTaxClaim.AsEnumerable()                           
                           join sia in saleInvoiceArtiface on btc.BillId.BillId equals sia.BillId.BillId
                           where sia.LegalInvoiceArtifactTypeId.Category == vatType
                select new C011GTGT()
                {
                    Amount = btc.Amount,
                    ClaimItem = btc.ClaimItem,
                    Comment = btc.Comment,
                    CreateDate = btc.CreateDate,
                    TaxInNumber = btc.TaxInNumber,
                    TaxInPercentage = btc.TaxInPercentage,
                    TotalAmount = btc.TotalAmount,
                    BillCode = sia.IssuedArtifaceCode,
                    SeriesNumber = sia.SeriesNumber,
                    ObjectName = btc.BillId.SourceOrganizationId.Name,
                    ObjectTax = btc.BillId.SourceOrganizationId.TaxNumber,
                    LegalInvoiceArtifactTypeCode = sia.LegalInvoiceArtifactTypeId.Code,
                    LegalInvoiceArtifactTypeName = sia.LegalInvoiceArtifactTypeId.Name
                };

                List<C011GTGT> data = new List<C011GTGT>();

                try
                {

                    CriteriaOperator _filter = new GroupOperator(GroupOperatorType.And,
                                               new BinaryOperator("Category", vatType, BinaryOperatorType.Equal),
                                               new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));
                    XPCollection<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType> _legalInvoiceArtifactType = new XPCollection<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType>(session);
                    _legalInvoiceArtifactType.Criteria = _filter;

                    var _result = list;

                    foreach (NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType liat in _legalInvoiceArtifactType)
                    {
                        if (!list.ToList().Any(l => l.LegalInvoiceArtifactTypeCode == liat.Code))
                        {
                            C011GTGT _c011GTGT = new C011GTGT()
                            {
                                Amount = 0,
                                ClaimItem = "",
                                Comment = "",
                                CreateDate = DateTime.Now,
                                TaxInNumber = 0,
                                TaxInPercentage = 0,
                                TotalAmount = 0,
                                BillCode = "",
                                SeriesNumber = "",
                                ObjectName = "",
                                ObjectTax = "",
                                LegalInvoiceArtifactTypeCode = liat.Code,
                                LegalInvoiceArtifactTypeName = liat.Name
                            };

                            _result = _result.Concat(new List<C011GTGT> { _c011GTGT });

                        }
                    }

                    data = _result.ToList();
                }
                catch
                {
                }
                if (vatType == Constant.VAT_OUT)
                {
                    _01_1_GTGT report = new _01_1_GTGT();
                    report.DataSource = data;
                    report.DataMember = "";
                    reportViewer.Report = report;
                }
                else
                {
                    _01_2_GTGT report = new _01_2_GTGT();
                    report.DataSource = data;
                    report.DataMember = "";
                    reportViewer.Report = report;
                }
                

                formInvoiceVatReport.JSProperties.Add("cpShowReport", "report");
            }
        }

        protected void formInvoiceVatReport_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            if (ReportHiddenField.Contains("typeReport"))
            {
            }
        }

      
    }
}