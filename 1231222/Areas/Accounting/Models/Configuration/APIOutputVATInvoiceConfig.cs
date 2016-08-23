using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Areas.Accounting.Models.Configuration
{
    public class APIOutputVATInvoiceConfig
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Ngày có hiệu lực
        /// </summary>
        public DateTime EffectiveDate { get; set; }


        /// <summary>
        /// Phân loại HĐ
        /// Type == Out
        /// </summary>
        /// private APIInvoiceType _Type;
        public string Type { get; set; }
        //[Display(Name = "Name")]
        //public APIInvoiceType Type
        //{
        //    get
        //    {
        //        return _Type;
        //    }
        //    set
        //    {
        //        if (value is string)
        //            _Type = InvoiceTypeList.GetData().First(i => i.Code == value.ToString());
        //        else
        //            _Type = value;
        //    }
        //}

        /// <summary>
        /// Mẫu số HĐ
        /// </summary>
        public string InvoiceForm { get; set; }

        /// <summary>
        /// Ký hiệu hóa đơn
        /// </summary>
        public string InvoiceCode { get; set; }
    }

    public class OutputVATInvoiceConfigList
    {
        public static List<APIOutputVATInvoiceConfig> GetData()
        {
            var key = "94B095CD-C54E-43C0-8895-3D77DBEA31B2";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
            {
                List<APIOutputVATInvoiceConfig> aPIInvoiceTypes = new List<APIOutputVATInvoiceConfig>();
                aPIInvoiceTypes.Add(new APIOutputVATInvoiceConfig()
                {
                    ID = 1,
                    EffectiveDate = new DateTime(2016, 01, 01),
                    InvoiceCode = "IT-20160504131459578",
                    InvoiceForm = "LGHA",
                    Type = "Hàng hóa, dịch vụ bán ra không chịu thuế GTGT"
                    //Type = InvoiceTypeList.GetData().Count > 0 ? InvoiceTypeList.GetData()[0] : null
                });
                aPIInvoiceTypes.Add(new APIOutputVATInvoiceConfig()
                {
                    ID = 2,
                    EffectiveDate = new DateTime(2016, 01, 17),
                    InvoiceCode = "IT-20160601131459578",
                    InvoiceForm = "FRSD",
                    Type = "Hàng hoá, dịch vụ bán ra chịu thuế suất 0%"
                    //Type = InvoiceTypeList.GetData().Count > 0 ? InvoiceTypeList.GetData()[0] : null
                });
                aPIInvoiceTypes.Add(new APIOutputVATInvoiceConfig()
                {
                    ID = 3,
                    EffectiveDate = new DateTime(2016, 02, 10),
                    InvoiceCode = "IT-20160301131459578",
                    InvoiceForm = "HYRD",
                    Type = "Hàng hoá, dịch vụ bán ra chịu thuế suất 5%"
                    //Type = InvoiceTypeList.GetData().Count > 0 ? InvoiceTypeList.GetData()[0] : null
                });
                aPIInvoiceTypes.Add(new APIOutputVATInvoiceConfig()
                {
                    ID = 4,
                    EffectiveDate = new DateTime(2016, 03, 05),
                    InvoiceCode = "IT-20160401131459578",
                    InvoiceForm = "RTES",
                    Type = "Hàng hoá, dịch vụ bán ra chịu thuế suất 10%"
                    //Type = InvoiceTypeList.GetData().Count > 0 ? InvoiceTypeList.GetData()[0] : null
                });

                Session[key] = aPIInvoiceTypes;
                aPIInvoiceTypes = null;
            }
            return (List<APIOutputVATInvoiceConfig>)Session[key];
        }


        public static void AddItem(APIOutputVATInvoiceConfig postedItem)
        {
            List<APIOutputVATInvoiceConfig> list = GetData();
            postedItem.ID = list.Count > 0 ? list.LastOrDefault().ID + 1 : 1;
            list.Add(postedItem);
        }


        public static void UpdateItem(APIOutputVATInvoiceConfig postedItem)
        {
            var editedModel = GetData().First(i => i.ID == postedItem.ID);
            editedModel.EffectiveDate = postedItem.EffectiveDate;
            editedModel.Type = postedItem.Type;
            editedModel.InvoiceForm = postedItem.InvoiceForm;
            editedModel.InvoiceCode = postedItem.InvoiceCode;
        }

        public static void DeleteItem(int id)
        {
            List<APIOutputVATInvoiceConfig> list = GetData();
            list.Remove(list.Where(w => w.ID == id).First());
        }
    }
}