using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
    public class APIFSOrder
    {
        [Key]
        public decimal Code { get; set; }

        public string FoodService { get; set; }

        public string CustomerPhone { get; set; }

        public string SupportPhone { get; set; }

        public DateTime OrderDate { get; set; }

        public FSOrderStatus Status { get; set; }
    }

    public enum FSOrderStatus
    {
        Submitted = 0,
        Approving = 1,
        InProgress = 2,
        Shipping = 3,
        Completed = 4,
        Pending = 5,
    }

    public static class APIFSOrderList
    {
        public static List<APIFSOrder> GetData()
        {
            var key = "D8933689-A9D4-4905-91DD-A1B661CB5DB1";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 9).Select(i => new APIFSOrder
                {
                    Code =  i,
                    FoodService = "FoodService_" + i,
                    CustomerPhone = "090522200" + i,
                    SupportPhone = "090523000" + i,
                    OrderDate = DateTime.Now,
                    Status = FSOrderStatus.Submitted,

                }).ToList();

            return (List<APIFSOrder>)Session[key];
        }

        #region Add ---------------------------------------
        public static void AddItem(APIFSOrder postedItem)
        {
            List<APIFSOrder> list = GetData();
            postedItem.Code = (list.Count + 1);
            list.Add(postedItem);

        }
        #endregion ----------------------------------------

        #region Update ----------------------------
        public static void UpdateItem(APIFSOrder e)
        {
            var editModel = APIFSOrderList.GetData().First(i => i.Code == e.Code);
            editModel.Code = e.Code;
            editModel.FoodService = e.FoodService;
            editModel.CustomerPhone = e.CustomerPhone;
            editModel.SupportPhone = e.SupportPhone;
            editModel.OrderDate = e.OrderDate;
            editModel.Status = e.Status;
        }
        #endregion -----------------------------------

        #region Delete ----------------------------
        public static void DeleteItem(decimal e)
        {
            var deleteItemCategory = APIFSOrderList.GetData().First(i => i.Code == e);
            APIFSOrderList.GetData().Remove(deleteItemCategory);
        }
        #endregion -----------------------------------
    }

}


