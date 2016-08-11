using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL.Invoice;
using DevExpress.Web.ASPxEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Item;
using System.Globalization;
using NAS.DAL;
using Utility;
using NAS.DAL.Accounting.LegalInvoice;
using WebModule.Accounting.Report;
using NAS.BO.Invoice;

namespace WebModule.Accounting.UserControl
{
    public partial class DeclareVat : System.Web.UI.UserControl
    {
        Bill bill;
        BillTaxClaim billTaxClaim;

        char m_VatType;

        Session session;

        protected void BindData(Bill bill)
        {
            double _amount = 0; 
            double _taxAmount = 0;
            double _taxPercent = 0;

            var taxPercentageList = BillBOBase.GetDistinctTaxPercentageList(bill.BillId);
            if (taxPercentageList == null || taxPercentageList.Count() == 0)
            {
                _amount = bill.SumOfItemPrice;
                _taxAmount = 0;
                _taxPercent = 0;
            }
            else
            {
                _taxPercent = taxPercentageList.First().Percentage;
                _taxAmount = bill.SumOfTax;
                _amount = bill.SumOfItemPrice - bill.SumOfPromotion;
            }

            lblTaxPercent.Text = _taxPercent > 0 ? _taxPercent.ToString() : "-";            
            lblTaxAmount.Text = _taxAmount > 0 ? _taxAmount.ToString("n0") : "-";
            lblAmount.Text = _amount > 0 ? _amount.ToString("n0") : "-";
            
            XPQuery<BillTaxClaim> billTaxClaim = new XPQuery<BillTaxClaim>(session);
            XPQuery<SaleInvoiceArtiface> saleInvoiceArtiface = new XPQuery<SaleInvoiceArtiface>(session);            

            

            var list = from btc in billTaxClaim.AsEnumerable()
                       join sia in saleInvoiceArtiface on btc.BillId.BillId equals sia.BillId.BillId
                       where btc.BillId.BillId == bill.BillId
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

            try
            {
                grdBillTaxClaim.DataSource = list.ToList();
                grdBillTaxClaim.DataBind();
                grdBillTaxClaim.ExpandAll();                                
            }
            catch
            {
            }               
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = NAS.DAL.XpoHelper.GetNewSession();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void cpDeclareVat_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split('|');
            CriteriaOperator filter;
            switch (para[0])
            {
                case "show":
                    cboItem.Text = "";
                    bill = session.GetObjectByKey<Bill>(Guid.Parse(para[1]));

                    if (bill != null)
                    {
                        filter = new BinaryOperator("BillId.BillId", bill.BillId, BinaryOperatorType.Equal);
                        SaleInvoiceArtiface saleInvoiceArtiface = session.FindObject<SaleInvoiceArtiface>(filter);                        

                        if (saleInvoiceArtiface != null)
                        {
                            cboLegalInvoiceArtifactType.Value = saleInvoiceArtiface.LegalInvoiceArtifactTypeId.LegalInvoiceArtifactTypeId;
                            txtIssuedArtifactCode.Text = saleInvoiceArtiface.IssuedArtifaceCode;
                            txtSeriesNumber.Text = saleInvoiceArtiface.SeriesNumber;
                        }
                        else
                        {
                            cboLegalInvoiceArtifactType.Value = null;
                            txtIssuedArtifactCode.Text = "";
                            txtSeriesNumber.Text = "";
                        }

                        filter = new BinaryOperator("BillId.BillId", Guid.Parse(para[1]), BinaryOperatorType.Equal);
                        billTaxClaim = session.FindObject<BillTaxClaim>(filter);

                        if (billTaxClaim != null)
                        {
                            filter = new BinaryOperator("Name", billTaxClaim.ClaimItem, BinaryOperatorType.Equal);
                            Item item = session.FindObject<Item>(filter);

                            if (item != null)
                                cboItem.Value = item.ItemId;

                            txtDescription.Text = billTaxClaim.Comment;
                        }
                        else
                        {
                            switch (bill.PaymentTerm)
                            {
                                case 1:
                                    txtDescription.Text = "Tiền mặt";
                                    break;
                                case 2:
                                    txtDescription.Text = "Chuyển khoản";
                                    break;
                                default:
                                    txtDescription.Text = "";
                                    break;
                            }
                        }

                        if (bill is SalesInvoice)
                        {
                            m_VatType = Constant.VAT_OUT;
                        }
                        else if (bill is PurchaseInvoice)
                        {
                            m_VatType = Constant.VAT_IN;
                        }

                        //Bill salesInvoice = session.GetObjectByKey<SalesInvoice>(bill.BillId);
                        //m_VatType = Constant.VAT_OUT;
                        //if (salesInvoice == null)
                        //{
                        //    m_VatType = Constant.VAT_IN;
                        //}

                        BindData(bill);                                                 
                    }

                    break;

                case "declare":

                    filter = new BinaryOperator("BillId", Guid.Parse(para[1]), BinaryOperatorType.Equal);
                    bill = session.GetObjectByKey<Bill>(Guid.Parse(para[1]));

                    if (bill != null)
                    {
                        filter = new BinaryOperator("BillId.BillId", Guid.Parse(para[1]), BinaryOperatorType.Equal);
                        billTaxClaim = session.FindObject<BillTaxClaim>(filter);
                        if (billTaxClaim == null)
                        {
                            billTaxClaim = new BillTaxClaim(session);
                            billTaxClaim.BillId = bill;
                            billTaxClaim.CreateDate = bill.IssuedDate;
                            billTaxClaim.RowStatus = 1;
                            bill.TaxClaimStatus = Constant.VAT_YES_DECLARE;
                        }                        

                        billTaxClaim.ClaimItem = cboItem.Text;
                        var taxPercentageList = BillBOBase.GetDistinctTaxPercentageList(bill.BillId);
                        if (taxPercentageList == null || taxPercentageList.Count() == 0)
                        {
                            billTaxClaim.TaxInPercentage = 0;
                        }
                        else
                        {
                            billTaxClaim.TaxInPercentage = taxPercentageList.First().Percentage;
                        }
                        //billTaxClaim.TaxInPercentage = bill.BillItems[0].BillItemTaxs[0].TaxInPercentage;
                        billTaxClaim.Amount = bill.SumOfItemPrice - bill.SumOfPromotion;
                        //billTaxClaim.TaxInNumber = billTaxClaim.Amount * billTaxClaim.TaxInPercentage / 100;
                        billTaxClaim.TaxInNumber = bill.SumOfTax;
                        billTaxClaim.Comment = txtDescription.Text;
                        billTaxClaim.LastUpdateDate = DateTime.Now;
                        billTaxClaim.CreateDate = bill.IssuedDate;

                        billTaxClaim.Save();

                        filter = new BinaryOperator("BillId.BillId", bill.BillId, BinaryOperatorType.Equal);
                        SaleInvoiceArtiface saleInvoiceArtiface = session.FindObject<SaleInvoiceArtiface>(filter);
                        if (saleInvoiceArtiface == null)
                        {
                            saleInvoiceArtiface = new SaleInvoiceArtiface(session);
                            saleInvoiceArtiface.SaleInvoiceArtifaceId = Guid.NewGuid();
                            saleInvoiceArtiface.RowStatus = 1;
                            saleInvoiceArtiface.BillId = bill;
                        }

                        NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType legalInvoiceArtifactType = session.GetObjectByKey<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType>(Guid.Parse(cboLegalInvoiceArtifactType.Value.ToString()));
                        if (legalInvoiceArtifactType != null)
                        {
                            saleInvoiceArtiface.LegalInvoiceArtifactTypeId = legalInvoiceArtifactType;
                        }

                        saleInvoiceArtiface.SeriesNumber = txtSeriesNumber.Text;
                        saleInvoiceArtiface.IssuedArtifaceCode = txtIssuedArtifactCode.Text;

                        saleInvoiceArtiface.Save();

                        //SalesInvoice salesInvoice = session.GetObjectByKey<SalesInvoice>(bill.BillId);
                        //m_VatType = Constant.VAT_OUT;
                        //if (salesInvoice == null)
                        //{
                        //    m_VatType = Constant.VAT_IN;
                        //}

                        if (bill is SalesInvoice)
                        {
                            m_VatType = Constant.VAT_OUT;
                        }
                        else if (bill is PurchaseInvoice)
                        {
                            m_VatType = Constant.VAT_IN;
                        }

                        BindData(bill);

                        cpDeclareVat.JSProperties.Add("cpDisableAccept", "Disable");
                    }

                    break;
            }
        }

        protected void cboItem_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {           

                ASPxComboBox comboItemUnit = source as ASPxComboBox;
                XPCollection<BillItem> collection = new XPCollection<BillItem>(session);

                collection.SkipReturnedObjects = e.BeginIndex;
                collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;
             
                CriteriaOperator criteria = CriteriaOperator.And(
                        CriteriaOperator.Or(
                            new BinaryOperator("ItemUnitId.ItemId.Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                            new BinaryOperator("ItemUnitId.ItemId.Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                            ),
                        new BinaryOperator("BillId.BillId", bill.BillId, BinaryOperatorType.Equal));


                collection.Criteria = criteria;
                collection.Sorting.Add(new SortProperty("ItemUnitId.ItemId.Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

                comboItemUnit.DataSource = collection;
                comboItemUnit.DataBindItems();
            }
            catch
            {
            }
        }

        protected void cboItem_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            try
            {

                BillItem obj = session.GetObjectByKey<BillItem>(Guid.Parse(e.Value.ToString()));
                
                if (obj != null)
                {
                    comboItemUnit.DataSource = new BillItem[] { obj };
                    comboItemUnit.DataBindItems();
                }
            }
            catch
            {
            }
        }

        protected void cboLegalInvoiceArtifactType_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                ASPxComboBox comboItemUnit = source as ASPxComboBox;
                XPCollection<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType> collection = new XPCollection<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType>(session);

                collection.SkipReturnedObjects = e.BeginIndex;
                collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            
                CriteriaOperator criteria = CriteriaOperator.And(
                        CriteriaOperator.Or(
                            new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                            new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                            ),
                        new BinaryOperator("Description", "F", BinaryOperatorType.NotEqual),
                        new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual),
                        new BinaryOperator("Category", m_VatType, BinaryOperatorType.Equal));


                collection.Criteria = criteria;
                collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

                comboItemUnit.DataSource = collection;
                comboItemUnit.DataBindItems();
            }
            catch
            {
            }
        }

        protected void cboLegalInvoiceArtifactType_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            try
            {

                NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType obj = session.GetObjectByKey<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType>(Guid.Parse(e.Value.ToString()));

                if (obj != null)
                {
                    comboItemUnit.DataSource = new NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType[] { obj };
                    comboItemUnit.DataBindItems();
                }
            }
            catch
            {
            }
        }
    }
}