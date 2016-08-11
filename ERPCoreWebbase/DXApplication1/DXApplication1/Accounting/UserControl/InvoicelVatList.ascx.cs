using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Invoice;
using WebModule.Accounting.Report;
using Utility;
using DevExpress.Data.Filtering;

namespace WebModule.Accounting.UserControl
{
    public partial class InvoicelVatList : System.Web.UI.UserControl
    {
        Session session;
    

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                grdBillTaxClaim.DataSource = (object) Session["data"];
                grdBillTaxClaim.DataBind();
                grdBillTaxClaim.ExpandAll();
            }
        }

        protected void cpInvoiceVatList_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split('|');
            
            if (para[0] != "")
            {              
                XPQuery<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType> legalInvoiceArtifactType = new XPQuery<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType>(session);
                XPQuery<BillTaxClaim> billTaxClaim = new XPQuery<BillTaxClaim>(session);
                XPQuery<SaleInvoiceArtiface> saleInvoiceArtiface = new XPQuery<SaleInvoiceArtiface>(session);

                char vatType = (para[0] == "01-1/GTGT" ? Constant.VAT_OUT : Constant.VAT_IN);

                var list = from liat in legalInvoiceArtifactType.Where(l => l.RowStatus >= 1).AsEnumerable()
                           join sia in saleInvoiceArtiface.Where(s => s.LegalInvoiceArtifactTypeId.Category == vatType).AsEnumerable() on liat.LegalInvoiceArtifactTypeId equals sia.LegalInvoiceArtifactTypeId.LegalInvoiceArtifactTypeId
                           join btc in billTaxClaim.AsEnumerable().DefaultIfEmpty(new BillTaxClaim(session)) on sia.BillId.BillId equals btc.BillId.BillId                    
                           //where g.LegalInvoiceArtifactTypeId.Category == vatType
                           //from gg in g.DefaultIfEmpty(new BillTaxClaim(session))
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
                               LegalInvoiceArtifactTypeCode = liat.Code,
                               LegalInvoiceArtifactTypeName = liat.Name
                           };      

                try
                {
                    CriteriaOperator _filter =  new GroupOperator(GroupOperatorType.And,
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

                    grdBillTaxClaim.DataSource = _result.ToList();
                    grdBillTaxClaim.DataBind();
                    grdBillTaxClaim.ExpandAll();

                    Session["data"] = _result.ToList();

                    cpInvoiceVatList.JSProperties.Add("cpShowForm", "show");
                }
                catch
                {
                }
            }
        }
    }
}