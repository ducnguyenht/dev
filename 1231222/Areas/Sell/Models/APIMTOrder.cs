using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
    public class APIMTOrder
    {
        
        [Key]
        public int Code { get; set; }

        public string SuperMarket { get; set; }

        public string CustomerPhone { get; set; }

        public string SupportPhone { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }
    }
    //public enum APIGTOrderStatus
    //{
    //    /// <summary>
    //    /// The draft:Ghi nhận
    //    /// </summary>
    //    Submitted = 0,

    //    /// <summary>
    //    /// The postable:Đang duyệt
    //    /// </summary>
    //    Approving = 1,

    //    /// <summary>
    //    /// The locked:Đang xử lý
    //    /// </summary>
    //    Inprogress = 2,

    //    /// <summary>
    //    /// The unlocked:Đang giao hàng
    //    /// </summary>
    //    Shipping = 3,

    //    /// <summary>
    //    /// The obsolete:Hoàn tất
    //    /// </summary>
    //    Completed = 4,

    //    /// <summary>
    //    /// The obsolete:Tạm hoãn
    //    /// </summary>
    //    Pending = 5,
    //}
    public class APIMTOrderList
    {
        public List<APIMTOrder> GetData()
        {
            string key = "3529EC16-47C0-480F-A51F-F82A4FD38594";
                if(HttpContext.Current.Session[key]==null)
                {
                    List<APIMTOrder> Sale =new List<APIMTOrder>();
                    Sale.Add(
                        new APIMTOrder()
                        {
                             Code = 1,              
                             SuperMarket="",
                             CustomerPhone="090123456",
                             SupportPhone="090999888",
                             OrderDate  = new DateTime(2016, 07, 02),                         
                             Status = "Approving",  
                        }
                       );
                     Sale.Add(
                        new APIMTOrder()
                        {
                             Code = 2,              
                             SuperMarket="",
                             CustomerPhone="090123456",
                             SupportPhone="090999888",
                             OrderDate  = new DateTime(2016, 07, 02),  
                             Status = "Approving",  
                        }
                       );
                     Sale.Add(
                        new APIMTOrder()
                        {
                             Code = 3,              
                             SuperMarket="",
                             CustomerPhone="090123456",
                             SupportPhone="090999888",
                             OrderDate  = new DateTime(2016, 07, 02),  
                             Status = "Approving",  
                        }
                       );
              HttpContext.Current.Session[key] = Sale;
            }
            return (List<APIMTOrder>)HttpContext.Current.Session[key];
        
        }
        public void AddItem(APIMTOrder postedItem)
        {
            List<APIMTOrder> list = GetData();
            postedItem.Code = (list.Count + 1);
            list.Add(postedItem);


        }


        public void UpdateItem(APIMTOrder postedItem)
        {
            var editedModel = GetData().First(i => i.Code == postedItem.Code);

            editedModel.Code = postedItem.Code;
            editedModel.SuperMarket = postedItem.SuperMarket;
            editedModel.CustomerPhone = postedItem.CustomerPhone;
            editedModel.SupportPhone = postedItem.SupportPhone;
            editedModel.OrderDate = postedItem.OrderDate;
            editedModel.Status = postedItem.Status;

        }
        public void DeleteItem(int code)
        {
            List<APIMTOrder> list = GetData();
            list.Remove(list.Where(w => w.Code == code).First());
        }
    }
}
