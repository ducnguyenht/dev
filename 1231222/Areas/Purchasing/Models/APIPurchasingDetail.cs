using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Purchasing.Models
{
    public class APIPurchasingDetail
    {
        [BindRex("ID")]
        public int ID { get; set; }
       [BindRex("PurchasingDetail_VATInvoiceSerialNumber")]
        public string VATInvoiceSerialNumber { get; set; }
        [BindRex("PurchasingDetail_InvoicedType")]
        public string InvoicedType { get; set; }
        [BindRex("Purchasing_IssueDate")]
        public string IssueDate { get; set; }
        [BindRex("PurchasingDetail_CreatedBy")]
        public string CreatedBy { get; set; }
         [BindRex("PurchasingDetail_TaxCode")]
        public string TaxCode { get; set; }
         [BindRex("PurchasingDetail_AmountBeforeTax")]
        public decimal AmountBeforeTax { get; set; }
        [BindRex("PurchasingDetail_Tax")]
        public decimal Tax { get; set; }
        [BindRex("PurchasingDetail_Amount")]
        public decimal Amount { get; set; }
    }
    public static class PurchasingDetailList
    {
        public static List<APIPurchasingDetail> GetData()
        {
            var key = "1CC1DD9B-8BC9-4054-9A17-9046E6DBE9D5";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 10).Select(i => new APIPurchasingDetail
                {
                    ID = i,
                    VATInvoiceSerialNumber = "",
                    InvoicedType = "",
                    IssueDate = "",
                    CreatedBy = "nhan vien" + i,
                    TaxCode = "text ",
                    AmountBeforeTax = 10000 + i,
                    Tax = 2000 + i,
                    Amount = 10000 + i,

                }).ToList();

            return (List<APIPurchasingDetail>)Session[key];

        }
        public static void AddItem(APIPurchasingDetail postedItem)
        {

            PurchasingDetailList.GetData().Add(postedItem);


        }
        public static void UpdateItem(APIPurchasingDetail postedItem)
        {
            var editedModel = PurchasingDetailList.GetData().First(i => i.ID == postedItem.ID);

            editedModel.VATInvoiceSerialNumber = postedItem.VATInvoiceSerialNumber;
            editedModel.InvoicedType = postedItem.InvoicedType;
            editedModel.IssueDate = postedItem.IssueDate;
            editedModel.CreatedBy = postedItem.CreatedBy;
            editedModel.TaxCode = postedItem.TaxCode;
            editedModel.AmountBeforeTax = postedItem.AmountBeforeTax;
            editedModel.Tax = postedItem.Tax;
            editedModel.Amount = postedItem.Amount;
        }
        public static void DeleteItem(int ma)
        {
            var deleteItem = PurchasingDetailList.GetData().First(i => i.ID == ma);
            PurchasingDetailList.GetData().Remove(deleteItem);
        }

    }
}