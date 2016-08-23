using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Accounting.Models.Configuration
{
    /// <summary>
    /// Hóa đơn GTGT đầu vào
    /// </summary>
    public class APIInputVATInvoice
    {
        public APIInputVATInvoice()
        {
            AmountBeforeVAT = 0;
            VAT = 0;
            VATAmount = 0;
        }

        public int Id { get; set; }

        // Bổ sung thêm cột combobox InvoiceType với giá trị TYPE_IN
        public string InvoiceType { get; set; }

        /// <summary>
        /// Ký hiệu hóa đơn
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
        /// Cá nhân, tổ chức
        /// </summary>
        public string ExternalOrganization { get; set; }

        /// <summary>
        /// Tên người bán
        /// </summary>
        public string Seller { get; set; }

        /// <summary>
        /// Mã số thuế người bán
        /// </summary>
        public string TaxCode { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal AmountBeforeVAT { get; set; }

        /// <summary>
        /// Thuế VAT (%)
        /// </summary>
        public decimal VAT { get; set; }

        /// <summary>
        /// Tiền thuế
        /// </summary>
        public decimal VATAmount { get; set; }

        /// <summary>
        /// Diễn giải
        /// </summary>
        public string Description { get; set; }

    }

    public class InvoiceType
    {
        public const string TYPE_IN_01 = "Hàng hoá, dịch vụ dùng riêng cho SXKD chịu thuế GTGT đủ điều kiện khấu trừ thuế";
        public const string TYPE_IN_02 = "Hàng hoá, dịch vụ không đủ điều kiện khấu trừ";
        public const string TYPE_IN_03 = "Hàng hoá, dịch vụ dùng chung cho SXKD chịu thuế và không chịu thuế đủ điều kiện khấu trừ thuế";
        public const string TYPE_IN_04 = "Hàng hóa, dịch vụ dùng cho dự án đầu tư đủ điều kiện được khấu trừ thuế";

        public const string TYPE_OUT_NOT_VAT = "Hàng hóa, dịch vụ bán ra không chịu thuế GTGT";
        public const string TYPE_OUT_VAT_0_PERCENTAGE = "Hàng hoá, dịch vụ bán ra chịu thuế suất 0%";
        public const string TYPE_OUT_VAT_5_PERCENTAGE = "Hàng hoá, dịch vụ bán ra chịu thuế suất 5%";
        public const string TYPE_OUT_VAT_10_PERCENTAGE = "Hàng hoá, dịch vụ bán ra chịu thuế suất 10%";
    }

    public class InputVATInvoiceModelList
    {
        public static List<APIInputVATInvoice> InputVATInvoiceModels()
        {
            string key = "C5951F7E-7511-42F0-B1CF-53CB3F97F6B8";
            if (HttpContext.Current.Session[key] == null)
            {
                List<APIInputVATInvoice> inputVATInvoiceModels = new List<APIInputVATInvoice>();
                inputVATInvoiceModels.Add(
                    new APIInputVATInvoice()
                    {
                        Id = 0,
                        InvoiceCode = "rfrt5rf57",
                        InvoiceNumber = "56858959",
                        InvoiceDate = new DateTime(2016, 07, 02),
                        ExternalOrganization = "(Cơ Khí Đức Trung) Công Ty TNHH Cơ Khí Đức Trung",
                        Seller = "Nguyen Van A",
                        TaxCode = "0305662375",
                        AmountBeforeVAT = 540000,
                        VAT = 10,
                        VATAmount = 54000,
                        Description = ""
                    }
                );
                inputVATInvoiceModels.Add(
                    new APIInputVATInvoice()
                    {
                        Id = 1,
                        InvoiceCode = "23123213",
                        InvoiceNumber = "543534",
                        InvoiceDate = new DateTime(2016, 06, 25),
                        ExternalOrganization = "(Á Châu) Công Ty TNHH Cơ Nhiệt Á Châu",
                        Seller = "(Á Châu) Công Ty TNHH Cơ Nhiệt Á Châu",
                        TaxCode = "1101784659",
                        AmountBeforeVAT = 1250000,
                        VAT = 10,
                        VATAmount = 125000,
                        Description = ""
                    }
                );
                inputVATInvoiceModels.Add(
                    new APIInputVATInvoice()
                    {
                        Id = 2,
                        InvoiceCode = "123",
                        InvoiceNumber = "321",
                        InvoiceDate = new DateTime(2016, 08, 01),
                        ExternalOrganization = "(CN Sài Gòn) Công Ty TNHH TM Và Xây Lắp CN Sài Gòn",
                        Seller = "(CN Sài Gòn) Công Ty TNHH TM Và Xây Lắp CN Sài Gòn",
                        TaxCode = "0302262724",
                        AmountBeforeVAT = 2450000,
                        VAT = 10,
                        VATAmount = 245000,
                        Description = ""
                    }
                );
                HttpContext.Current.Session[key] = inputVATInvoiceModels;
            }
            return (List<APIInputVATInvoice>)HttpContext.Current.Session[key];
        }

        public static void AddItem(APIInputVATInvoice postedItem)
        {
            List<APIInputVATInvoice> list = InputVATInvoiceModels();
            try
            {
                postedItem.Id = list.Max(m => m.Id) + 1;
            }
            catch { postedItem.Id = 1; }
            list.Add(postedItem);
        }

        public static void UpdateItem(APIInputVATInvoice postedItem)
        {
            var editedModel = InputVATInvoiceModels().First(i => i.Id == postedItem.Id);
            editedModel.InvoiceType = postedItem.InvoiceType;
            editedModel.InvoiceCode = postedItem.InvoiceCode;
            editedModel.InvoiceNumber = postedItem.InvoiceNumber;
            editedModel.InvoiceDate = postedItem.InvoiceDate;
            editedModel.ExternalOrganization = postedItem.ExternalOrganization;
            editedModel.Seller = postedItem.Seller;
            editedModel.TaxCode = postedItem.TaxCode;
            editedModel.AmountBeforeVAT = postedItem.AmountBeforeVAT;
            editedModel.VAT = postedItem.VAT;
            editedModel.VATAmount = postedItem.VATAmount;
            editedModel.Description = postedItem.Description;
        }
        public static void DeleteItem(int id)
        {
            IList<APIInputVATInvoice> list = InputVATInvoiceModels();
            list.Remove(list.Where(w => w.Id == id).First());
        }

    }
}