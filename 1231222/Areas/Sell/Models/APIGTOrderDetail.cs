using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
    public class APIGTOrderDetail
    {
        //[BindRex("ID")]
        public int Code { get; set; }
        //[BindRex("PurchasingDetailPanel_Items")]
        public string Items { get; set; }
        //[BindRex("PurchasingDetailPanel_Unit")]
        public decimal Unit { get; set; }
        //[BindRex("PurchasingDetailPanel_Quantity")]
        public decimal Quantity { get; set; }
        //[BindRex("PurchasingDetailPanel_UnitPrice")]
        public decimal UnitPrice { get; set; }
       // [BindRex("PurchasingDetailPanel_Total")]
        public string Total { get; set; }
    }
    public static class GTOrderItemList
    {
        public static List<APIGTOrderDetail> GetData()
        {
            var key = "F49D0A4B-8A1C-465A-84BE-6B7C9880F6EF";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 50).Select(i => new APIGTOrderDetail
                {
                    Code = i,
                    Items = "text" + i,
                    Unit = 12 + i,
                    Quantity = 10,
                    UnitPrice = 10000 + i,
                    Total = "",


                }).ToList();
            return (List<APIGTOrderDetail>)Session[key];
        }
        public static void AddItem(APIGTOrderDetail postedItem)
        {

            GTOrderItemList.GetData().Add(postedItem);


        }
        public static void UpdateItem(APIGTOrderDetail postedItem)
        {
            var editedModel = GTOrderItemList.GetData().First(i => i.Code == postedItem.Code);

            editedModel.Items = postedItem.Items;
            editedModel.Unit = postedItem.Unit;
            editedModel.Quantity = postedItem.Quantity;
            editedModel.UnitPrice = postedItem.UnitPrice;
            editedModel.Total = postedItem.Total;
        }
        public static void DeleteItem(int code)
        {
            var deleteItem = GTOrderItemList.GetData().First(i => i.Code == code);
            GTOrderItemList.GetData().Remove(deleteItem);
        }
    }
}