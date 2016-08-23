using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
	public class APIFSOrderItem
	{
        public int NO { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        //đơn vị tính
        public string UoM { get; set; }
        public string QuantityInUoM { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Percentage { get; set; }
        public string Amount { get; set; }
    }
    public class APIFSOrderItemList
    {
        public List<APIFSOrderItem> GetData()
        {
            string key = "07C1CDE4-6036-4F9D-9BA0-5020BA4B757B";
            if (HttpContext.Current.Session[key] == null)
            {
                List<APIFSOrderItem> Sale = new List<APIFSOrderItem>();
                Sale.Add(
                    new APIFSOrderItem()
                    {
                        NO = 1,
                        Code = 1,
                        Name = "Á Châu",
                        Specification = "",
                        UoM = "gói",
                        QuantityInUoM = "kg",
                        Quantity = "2",
                        Price = "50000",
                        Percentage = "",
                        Amount = "100000"
                        //IssueDate = new DateTime(2016, 07, 02),
                        //Code = 1,
                        //Retailer = "(Á Châu) Công Ty TNHH Cơ Nhiệt Á Châu",
                        //RetailerPhone = "090123456",
                        //SalesPhone = "090999888",
                        //Status = "Submitted ",
                    }
                );
                Sale.Add(
                    new APIFSOrderItem()
                    {
                        NO = 2,
                        Code = 2,
                        Name = "Nhật Phát",
                        Specification = "",
                        UoM = "gói",
                        QuantityInUoM = "kg",
                        Quantity = "2",
                        Price = "50000",
                        Percentage = "",
                        Amount = "100000"
                        //IssueDate = new DateTime(2016, 07, 02),
                        //Code = 2,
                        //Retailer = "(Anh Nhật Phát) Cty Cổ Phần Anh Nhật Phát",
                        //RetailerPhone = "090111222",
                        //SalesPhone = "090313131",
                        //Status = "Approving",
                    }
                );
                Sale.Add(
                    new APIFSOrderItem()
                    {
                        NO = 3,
                        Code = 3,
                        Name = "An Khánh",
                        Specification = "",
                        UoM = "gói",
                        QuantityInUoM = "kg",
                        Quantity = "2",
                        Price = "50000",
                        Percentage = "",
                        Amount = "100000"
                        //IssueDate = new DateTime(2016, 07, 02),
                        //Code = 3,
                        //Retailer = "(An Khánh) Công Ty Cổ Phần Xây Dựng An Khánh",
                        //RetailerPhone = "090555666",
                        //SalesPhone = "090414141",
                        //Status = "In progress",
                    }
                );
                Sale.Add(
                   new APIFSOrderItem()
                   {
                       NO = 4,
                       Code = 4,
                       Name = "An Khánh",
                       Specification = "",
                       UoM = "gói",
                       QuantityInUoM = "kg",
                       Quantity = "2",
                       Price = "50000",
                       Percentage = "",
                       Amount = "100000"
                       //IssueDate = new DateTime(2016, 07, 02),
                       //Code = 4,
                       //Retailer = "(An Khánh) Công Ty Cổ Phần Xây Dựng An Khánh",
                       //RetailerPhone = "090567894",
                       //SalesPhone = "090414141",
                       //Status = "Shipping ",
                   }
               );
                Sale.Add(
                   new APIFSOrderItem()
                   {
                       NO = 5,
                       Code = 5,
                       Name = " Nhật Phát",
                       Specification = "",
                       UoM = "bich",
                       QuantityInUoM = "kg",
                       Quantity = "2",
                       Price = "50000",
                       Percentage = "",
                       Amount = "100000"
                       //IssueDate = new DateTime(2016, 07, 02),
                       //Code = 5,
                       //Retailer = "(Anh Nhật Phát) Cty Cổ Phần Anh Nhật Phát",
                       //RetailerPhone = "0913141516",
                       //SalesPhone = "090313131",
                       //Status = "Completed",
                   }
               );
                Sale.Add(
                    new APIFSOrderItem()
                    {
                        NO = 6,
                        Code = 6,
                        Name = "Á Châu",
                        Specification = "",
                        UoM = "gói",
                        QuantityInUoM = "kg",
                        Quantity = "2",
                        Price = "50000",
                        Percentage = "",
                        Amount = "100000"
                        //IssueDate = new DateTime(2016, 07, 02),
                        //Code = 6,
                        //Retailer = "(Á Châu) Công Ty TNHH Cơ Nhiệt Á Châu",
                        //RetailerPhone = "090123456",
                        //SalesPhone = "090999888",
                        //Status = "Pending  ",
                    }
                );
                HttpContext.Current.Session[key] = Sale;
            }
            return (List<APIFSOrderItem>)HttpContext.Current.Session[key];
        }
        public void AddItem(APIFSOrderItem postedItem)
        {
            List<APIFSOrderItem> list = GetData();
            postedItem.Code = (list.Count + 1);
            list.Add(postedItem);
        }


        public void UpdateItem(APIFSOrderItem postedItem)
        {
            var editedModel = GetData().First(i => i.Code == postedItem.Code);

            editedModel.NO = postedItem.NO;
            editedModel.Code = postedItem.Code;
            editedModel.Name = postedItem.Name;
            editedModel.Specification = postedItem.Specification;
            editedModel.UoM = postedItem.UoM;
            editedModel.QuantityInUoM = postedItem.QuantityInUoM;
            editedModel.Quantity = postedItem.Quantity;
            editedModel.Price = postedItem.Price;
            editedModel.Percentage = postedItem.Percentage;
            editedModel.Amount = postedItem.Amount;
        }
        public void DeleteItem(int id)
        {
            List<APIFSOrderItem> list = GetData();
            list.Remove(list.Where(w => w.Code == id).First());
        }
    }
}
