using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Accounting.Models.Configuration
{
    /// <summary>
    /// Hóa đơn GTGT đầu ra
    /// </summary>
    public class APIOutputVATInvoice
    {
        public APIOutputVATInvoice()
        {
            AmountBeforeVAT = 0;
            VAT = 0;
            VATAmount = 0;
        }

        public int Id { get; set; }

        /// <summary>
        /// Phân loại HĐ
        /// </summary>
        public string InvoiceType { get; set; }

        /// <summary>
        /// Mẫu số hóa đơn
        /// </summary>
        public string InvoiceForm { get; set; }

        /// <summary>
        /// Ký hiệu HĐ
        /// </summary>
        public string InvoiceCode { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Ngày hóa đơn
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Cá nhân/tổ chức
        /// </summary>
        public string ExternalOrganization { get; set; }

        /// <summary>
        /// Người mua
        /// </summary>
        public string Buyer { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string TaxCode { get; set; }

        /// <summary>
        /// Số tiền trước thuế
        /// </summary>
        public decimal AmountBeforeVAT { get; set; }

        /// <summary>
        /// Thuế suất(%)
        /// </summary>
        public decimal VAT { get; set; }

        public decimal VATAmount { get; set; }

        public string Description { get; set; }
    }


    public class OutputVATInvoiceModelList
    {
        public static List<APIOutputVATInvoice> OutputVATInvoiceModels()
        {
            string key = "752E82B2-A7A0-48BD-A767-E4BFB0B80F8F";
            if (HttpContext.Current.Session[key] == null)
            {
                List<APIOutputVATInvoice> outputVATInvoiceModels = new List<APIOutputVATInvoice>();
                outputVATInvoiceModels.Add(
                    new APIOutputVATInvoice()
                    {
                        Id = 0,
                        InvoiceCode = "rfrt5rf57",
                        InvoiceNumber = "56858959",
                        InvoiceDate = new DateTime(2016, 07, 02),
                        ExternalOrganization = "(Cơ Khí Đức Trung) Công Ty TNHH Cơ Khí Đức Trung",
                        Buyer = "Nguyen Van A",
                        TaxCode = "0305662375",
                        AmountBeforeVAT = 540000,
                        VAT = 10,
                        VATAmount = 54000,
                        Description = ""
                    }
                );
                outputVATInvoiceModels.Add(
                    new APIOutputVATInvoice()
                    {
                        Id = 1,
                        InvoiceCode = "23123213",
                        InvoiceNumber = "543534",
                        InvoiceDate = new DateTime(2016, 06, 25),
                        ExternalOrganization = "(Á Châu) Công Ty TNHH Cơ Nhiệt Á Châu",
                        Buyer = "(Á Châu) Công Ty TNHH Cơ Nhiệt Á Châu",
                        TaxCode = "1101784659",
                        AmountBeforeVAT = 1250000,
                        VAT = 10,
                        VATAmount = 125000,
                        Description = ""
                    }
                );
                outputVATInvoiceModels.Add(
                    new APIOutputVATInvoice()
                    {
                        Id = 2,
                        InvoiceCode = "123",
                        InvoiceNumber = "321",
                        InvoiceDate = new DateTime(2016, 08, 01),
                        ExternalOrganization = "(CN Sài Gòn) Công Ty TNHH TM Và Xây Lắp CN Sài Gòn",
                        Buyer = "(CN Sài Gòn) Công Ty TNHH TM Và Xây Lắp CN Sài Gòn",
                        TaxCode = "0302262724",
                        AmountBeforeVAT = 2450000,
                        VAT = 10,
                        VATAmount = 245000,
                        Description = ""
                    }
                );
                HttpContext.Current.Session[key] = outputVATInvoiceModels;
            }
            return (List<APIOutputVATInvoice>)HttpContext.Current.Session[key];
        }

        public static void AddItem(APIOutputVATInvoice postedItem)
        {
            List<APIOutputVATInvoice> list = OutputVATInvoiceModels();
            try
            {
                postedItem.Id = list.Max(m => m.Id) + 1;
            }
            catch { postedItem.Id = 1; }
            list.Add(postedItem);
        }

        public static void UpdateItem(APIOutputVATInvoice postedItem)
        {
            var editedModel = OutputVATInvoiceModels().First(i => i.Id == postedItem.Id);
            editedModel.InvoiceType = postedItem.InvoiceType;
            editedModel.InvoiceCode = postedItem.InvoiceCode;
            editedModel.InvoiceNumber = postedItem.InvoiceNumber;
            editedModel.InvoiceDate = postedItem.InvoiceDate;
            editedModel.ExternalOrganization = postedItem.ExternalOrganization;
            editedModel.Buyer = postedItem.Buyer;
            editedModel.TaxCode = postedItem.TaxCode;
            editedModel.AmountBeforeVAT = postedItem.AmountBeforeVAT;
            editedModel.VAT = postedItem.VAT;
            editedModel.VATAmount = postedItem.VATAmount;
            editedModel.Description = postedItem.Description;
        }
        public static void DeleteItem(int id)
        {
            IList<APIOutputVATInvoice> list = OutputVATInvoiceModels();
            list.Remove(list.Where(w => w.Id == id).First());
        }

    }
}