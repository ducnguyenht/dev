using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Purchasing.Models
{
    public class APIDetailPanel
    {
         //[BindRex("ID")]
        public int ID { get; set; }
        //[BindRex("PurchasingDetailPanel_Items")]
        public string Items { get; set; }
        //[BindRex("PurchasingDetailPanel_Unit")]
        public decimal Unit { get; set; }
       // [BindRex("PurchasingDetailPanel_Quantity")]
        public decimal Quantity { get; set; }
       // [BindRex("PurchasingDetailPanel_UnitPrice")]
        public decimal UnitPrice { get; set; }
       // [BindRex("PurchasingDetailPanel_Total")]
        public string Total { get; set; }

    }
    public static class DetailPanelList
    {
        public static List<APIDetailPanel> GetData()
        {
            var key = "F49D0A4B-8A1C-465A-84BE-6B7C9880F6EF";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 50).Select(i => new APIDetailPanel
                {
                    ID = i,
                    Items = "text" + i,
                    Unit = 12 + i,
                    Quantity = 10,
                    UnitPrice = 10000 + i,
                    Total = "",


                }).ToList();
            return (List<APIDetailPanel>)Session[key];
        }
        public static void AddItem(APIDetailPanel postedItem)
        {

            DetailPanelList.GetData().Add(postedItem);


        }
        public static void UpdateItem(APIDetailPanel postedItem)
        {
            var editedModel = DetailPanelList.GetData().First(i => i.ID == postedItem.ID);

            editedModel.Items = postedItem.Items;
            editedModel.Unit = postedItem.Unit;
            editedModel.Quantity = postedItem.Quantity;
            editedModel.UnitPrice = postedItem.UnitPrice;
            editedModel.Total = postedItem.Total;
        }
        public static void DeleteItem(int ma)
        {
            var deleteItem = DetailPanelList.GetData().First(i => i.ID == ma);
            DetailPanelList.GetData().Remove(deleteItem);
        }
    }
}