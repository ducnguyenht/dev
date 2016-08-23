using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
    /// <summary>
    /// Giá trị đơn hàng
    /// </summary>
    public class APISalesOnlineItems
    {
        //[BindRex("APISalesOnlineItems_NumberOrder")]
        public int NumberOrder { get; set; }
        //[BindRex("APISalesOnlineItems_Code")]
        public string Code { get; set; }
        //[BindRex("APISalesOnlineItems_Name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Quy cách
        /// </summary>
        //[BindRex("APISalesOnlineItems_Specification")]
        public string Specification { get; set; }
        //[BindRex("APISalesOnlineItems_Weight")]
        public decimal Weight { get; set; }
        //[BindRex("APISalesOnlineItems_Unit")]
        public string Unit { get; set; }
        //[BindRex("APISalesOnlineItems_Quantity")]
        public decimal Quantity { get; set; }
        //[BindRex("APISalesOnlineItems_Price")]
        public decimal Price { get; set; }
        //[BindRex("APISalesOnlineItems_Discount")]
        public decimal Discount { get; set; }
        //[BindRex("APISalesOnlineItems_Amount")]
        public decimal Amount { get; set; }
        
    }

    /// <summary>
    /// Mặt hàng khuyến mãi
    /// </summary>
    public class APISalesOnlineItemsPromotion
    {
        //[BindRex("APISalesOnlineItemsPromotion_NumberOrder")]
        public int NumberOrder { get; set; }
        //[BindRex("APISalesOnlineItemsPromotion_Code")]
        public string Code { get; set; }
        //[BindRex("APISalesOnlineItemsPromotion_Name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Quy cách
        /// </summary>
        //[BindRex("APISalesOnlineItemsPromotion_Specification")]
        public string Specification { get; set; }
        //[BindRex("APISalesOnlineItemsPromotion_Weight")]
        public decimal Weight { get; set; }
        //[BindRex("APISalesOnlineItemsPromotion_Unit")]
        public string Unit { get; set; }
        //[BindRex("APISalesOnlineItemsPromotion_Quantity")]
        public decimal Quantity { get; set; }
        //[BindRex("APISalesOnlineItemsPromotion_Note")]
        public decimal Note { get; set; }
        
    }

    public class SalesOnlineItemsList
    {
        public static List<APISalesOnlineItems> GetData()
        {
            var key = "5A2D44FF-339E-48E8-9470-FCCA2B90BA7E";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
            {
                List<APISalesOnlineItems> listData = new List<APISalesOnlineItems>();
                var item = new APISalesOnlineItems();
                item.NumberOrder = 1;
                item.Code = "CFRX";
                item.Name = "Bánh";
                item.Specification = "thùng";
                item.Weight = 5M;
                item.Unit = "gói";
                item.Quantity = 30;
                item.Price = 10000;
                item.Discount = 10;
                item.Amount = (item.Quantity * item.Price) / 100 * (100 - item.Discount);
                listData.Add(item);

                item = new APISalesOnlineItems();
                item.NumberOrder = 2;
                item.Code = "GTCV";
                item.Name = "Bia";
                item.Specification = "thùng";
                item.Weight = 10M;
                item.Unit = "lon";
                item.Quantity = 24;
                item.Price = 18000;
                item.Discount = 0;
                item.Amount = (item.Quantity * item.Price) / 100 * (100 - item.Discount);
                listData.Add(item);

                item = new APISalesOnlineItems();
                item.NumberOrder = 3;
                item.Code = "NNCC";
                item.Name = "Nước ngọt";
                item.Specification = "thùng";
                item.Weight = 7M;
                item.Unit = "chai";
                item.Quantity = 6;
                item.Price = 20000;
                item.Discount = 2;
                item.Amount = (item.Quantity * item.Price) / 100 * (100 - item.Discount);
                listData.Add(item);

                Session[key] = listData;
                listData = null;
            }
            return (List<APISalesOnlineItems>)Session[key];
        }
        public static void AddItem(APISalesOnlineItems postedItem)
        {
            APISalesOnlineItems item = postedItem;
            item.NumberOrder = GetData().Max(m => m.NumberOrder) + 1;
            SalesOnlineItemsList.GetData().Add(postedItem);
        }
        public static void UpdateItem(APISalesOnlineItems postedItem)
        {
            var editedModel = SalesOnlineItemsList.GetData().First(i => i.NumberOrder == postedItem.NumberOrder);

            editedModel.NumberOrder = postedItem.NumberOrder;
            editedModel.Code = postedItem.Code;
            editedModel.Name = postedItem.Name;
            editedModel.Specification = postedItem.Specification;
            editedModel.Weight = postedItem.Weight;
            editedModel.Unit = postedItem.Unit;
            editedModel.Quantity = postedItem.Quantity;
            editedModel.Price = postedItem.Price;
            editedModel.Discount = postedItem.Discount;
            editedModel.Amount = postedItem.Amount;
        }
        public static void DeleteItem(int ma)
        {
            var deleteItem = SalesOnlineItemsList.GetData().First(i => i.NumberOrder == ma);
            SalesOnlineItemsList.GetData().Remove(deleteItem);
        }
    }


    public class SalesOnlineItemsPromotionList
    {
        public static List<APISalesOnlineItemsPromotion> GetData()
        {
            var key = "428FF47D-6E6F-433A-969E-3F9D5545C402";
            Random r = new Random();
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 3).Select(i => new APISalesOnlineItemsPromotion
                {
                    NumberOrder = i,
                    Code = "ITEM" + i,
                    Name = "Sản phẩm" + i,
                    Specification = "gói",
                    Weight = i,
                    Unit = "",
                    Quantity = 3,
                }).ToList();
            return (List<APISalesOnlineItemsPromotion>)Session[key];
        }
        public static void AddItem(APISalesOnlineItemsPromotion postedItem)
        {
            APISalesOnlineItemsPromotion item = postedItem;
            item.NumberOrder = GetData().Max(m => m.NumberOrder) + 1;
            SalesOnlineItemsPromotionList.GetData().Add(postedItem);
        }
        public static void UpdateItem(APISalesOnlineItemsPromotion postedItem)
        {
            var editedModel = SalesOnlineItemsPromotionList.GetData().First(i => i.NumberOrder == postedItem.NumberOrder);

            editedModel.NumberOrder = postedItem.NumberOrder;
            editedModel.Code = postedItem.Code;
            editedModel.Name = postedItem.Name;
            editedModel.Specification = postedItem.Specification;
            editedModel.Weight = postedItem.Weight;
            editedModel.Unit = postedItem.Unit;
            editedModel.Quantity = postedItem.Quantity;
            editedModel.Note = postedItem.Note;
        }
        public static void DeleteItem(int ma)
        {
            var deleteItem = SalesOnlineItemsPromotionList.GetData().First(i => i.NumberOrder == ma);
            SalesOnlineItemsPromotionList.GetData().Remove(deleteItem);
        }
    }
}