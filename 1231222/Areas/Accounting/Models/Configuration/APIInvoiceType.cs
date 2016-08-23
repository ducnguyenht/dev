using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Areas.Accounting.Models.Configuration
{
    public class APIInvoiceType
    {
        public APIInvoiceType()
        {
        }
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public InvoiceTypeEnum _Type;
        public InvoiceTypeEnum Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = (InvoiceTypeEnum)Enum.Parse(typeof(InvoiceTypeEnum), value.ToString());
            }
        }
    }

    public enum InvoiceTypeEnum
    {
        In = 0,  // VAT đầu vào 
        Out = 1  // VAT đầu ra
    }

    public class InvoiceTypeList
    {
        public static List<APIInvoiceType> GetData()
        {
            var key = "0ED16389-163C-45BD-81D4-8E7B8A6B8593";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
            {
                List<APIInvoiceType> aPIInvoiceTypes = new List<APIInvoiceType>();
                aPIInvoiceTypes.Add(new APIInvoiceType()
                {
                    Code = "IT-20160504131459578",
                    Name = "Hàng hoá, dịch vụ dùng riêng cho SXKD chịu thuế GTGT đủ điều kiện khấu trừ thuế",
                    Description = "",
                    Type = InvoiceTypeEnum.In
                });
                aPIInvoiceTypes.Add(new APIInvoiceType()
                {
                    Code = "IT-20160601131459578",
                    Name = "Hàng hoá, dịch vụ không đủ điều kiện khấu trừ",
                    Description = "",
                    Type = InvoiceTypeEnum.In
                });
                aPIInvoiceTypes.Add(new APIInvoiceType()
                {
                    Code = "IT-20160301131459578",
                    Name = "Hàng hóa, dịch vụ bán ra không chịu thuế GTGT",
                    Description = "",
                    Type = InvoiceTypeEnum.Out
                });
                aPIInvoiceTypes.Add(new APIInvoiceType()
                {
                    Code = "IT-20160401131459578",
                    Name = "Hàng hoá, dịch vụ bán ra chịu thuế suất 0%",
                    Description = "",
                    Type = InvoiceTypeEnum.Out
                });

                Session[key] = aPIInvoiceTypes;
                aPIInvoiceTypes = null;
            }
            return (List<APIInvoiceType>)Session[key];
        }


        public static void AddItem(APIInvoiceType postedItem)
        {
            List<APIInvoiceType> list = GetData();
            //postedItem.Code = String.Format("IT-{0:yyyyMMddHHmmssfff}", DateTime.Now);
            list.Add(postedItem);
        }


        public static void UpdateItem(APIInvoiceType postedItem)
        {
            var editedModel = GetData().First(i => i.Code == postedItem.Code);
            editedModel.Name = postedItem.Name;
            editedModel.Description = postedItem.Description;
            editedModel.Type = postedItem.Type;
        }

        public static void DeleteItem(string code)
        {
            List<APIInvoiceType> list = GetData();
            list.Remove(list.Where(w => w.Code == code).First());
        }
    }
}