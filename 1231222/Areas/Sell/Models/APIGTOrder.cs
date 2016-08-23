using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
    public class APIGTOrder
    {
        //[BindRex("GTOrder_IssueDate")]
        public DateTime OrderDate  { get; set; }
        //[BindRex("GTOrder_Code")]
        public int Code { get; set; }
        //[BindRex("GTOrder_Retailer")]
        public string Retailer { get; set; }
        //[BindRex("GTOrder_RetailerPhone")]
        public string RetailerPhone { get; set; }
        // [BindRex("GTOrder_SalesPhone")]
        public string SalesPhone { get; set; }
        // [BindRex("GTOrder_Status")]
        public APIGTOrderStatus Status { get; set; }
    }
    public enum APIGTOrderStatus
    {
        /// <summary>
        /// The draft:Ghi nhận
        /// </summary>
        Submitted = 0,

        /// <summary>
        /// The postable:Đang duyệt
        /// </summary>
        Approving = 1,

        /// <summary>
        /// The locked:Đang xử lý
        /// </summary>
        Inprogress = 2,

        /// <summary>
        /// The unlocked:Đang giao hàng
        /// </summary>
        Shipping = 3,

        /// <summary>
        /// The obsolete:Hoàn tất
        /// </summary>
        Completed = 4 ,

        /// <summary>
        /// The obsolete:Tạm hoãn
        /// </summary>
        Pending = 5,
    }
    public class APIGTOrderList
    {
        public List<APIGTOrder> GetData()
        {
            string key = "48A87427-15CA-41A1-B7EC-02496F7F5AD7";
            if (HttpContext.Current.Session[key] == null)
            {
                List<APIGTOrder> Sale = new List<APIGTOrder>();
                Sale.Add(
                    new APIGTOrder()
                    {
                        OrderDate  = new DateTime(2016, 07, 02),                      
                        Code = 1,                    
                        Retailer = "(Á Châu) Công Ty TNHH Cơ Nhiệt Á Châu",
                        RetailerPhone="090123456",
                        SalesPhone="090999888",
                        Status = APIGTOrderStatus.Approving,                     
                    }
                );
                Sale.Add(
                    new APIGTOrder()
                    {
                        OrderDate = new DateTime(2016, 07, 02), 
                        Code = 2,                    
                        Retailer = "(Anh Nhật Phát) Cty Cổ Phần Anh Nhật Phát",                     
                        RetailerPhone = "090111222",
                        SalesPhone = "090313131",
                        Status = APIGTOrderStatus.Completed,
                    }
                );
                Sale.Add(
                    new APIGTOrder()
                    {
                        OrderDate = new DateTime(2016, 07, 02),
                      Code = 3,                 
                      Retailer = "(An Khánh) Công Ty Cổ Phần Xây Dựng An Khánh",
                      RetailerPhone = "090555666",
                      SalesPhone = "090414141",
                      Status = APIGTOrderStatus.Inprogress,                       
                    }
                );
                Sale.Add(
                   new APIGTOrder()
                   {
                       OrderDate = new DateTime(2016, 07, 02),
                       Code = 4,
                       Retailer = "(An Khánh) Công Ty Cổ Phần Xây Dựng An Khánh",
                       RetailerPhone = "090567894",
                       SalesPhone = "090414141",
                       Status = APIGTOrderStatus.Pending,
                   }
               );
                Sale.Add(
                   new APIGTOrder()
                   {
                       OrderDate = new DateTime(2016, 07, 02),
                       Code = 5,
                       Retailer = "(Anh Nhật Phát) Cty Cổ Phần Anh Nhật Phát",
                       RetailerPhone = "0913141516",
                       SalesPhone = "090313131",
                       Status = APIGTOrderStatus.Shipping,
                   }
               );
                Sale.Add(
                    new APIGTOrder()
                    {
                        OrderDate = new DateTime(2016, 07, 02),
                        Code = 6,
                        Retailer = "(Á Châu) Công Ty TNHH Cơ Nhiệt Á Châu",
                        RetailerPhone = "090123456",
                        SalesPhone = "090999888",
                        Status = APIGTOrderStatus.Submitted,
                    }
                );
                HttpContext.Current.Session[key] = Sale;
            }
            return (List<APIGTOrder>)HttpContext.Current.Session[key];
        }
        public void AddItem(APIGTOrder postedItem)
        {
            List<APIGTOrder> list = GetData();
            postedItem.Code = (list.Count + 1);
            list.Add(postedItem);

           
        }


        public void UpdateItem(APIGTOrder postedItem)
        {
            var editedModel = GetData().First(i => i.Code == postedItem.Code);

            editedModel.Code = postedItem.Code;
            editedModel.OrderDate = postedItem.OrderDate;
            editedModel.Retailer = postedItem.Retailer;
            editedModel.RetailerPhone = postedItem.RetailerPhone;
            editedModel.SalesPhone = postedItem.SalesPhone;
            editedModel.Status = postedItem.Status;
           
        }
        public void DeleteItem(int id)
        {
            List<APIGTOrder> list = GetData();
            list.Remove(list.Where(w => w.Code == id).First());
        }
    }
}
