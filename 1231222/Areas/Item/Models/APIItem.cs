using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Areas.Item.Models
{
    public class APIItem
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal ReferenceCost { get; set; }

        public decimal Cost { get; set; }

        public decimal ServiceChangeInPercentage { get; set; }

        public bool IncludedVATInPrice { get; set; }

        public bool IsStorable { get; set; }

        public string ItemType { get; set; }

        public string Category { get; set; }

        public string Unit { get; set; }
    }

    public static class ItemList
    {
        public static List<APIItem> GetData()
        {
            var key = "34FAA431-CF79-4869-9488-93F6AAE81212";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 100).Select(i => new APIItem
                {
                    Code = "Item_" + i,
                    Name = "Test String" + i,
                    Description = "",
                    Price = i + 100,
                    ReferenceCost = i * 10000,
                    Cost = 0,
                    ServiceChangeInPercentage = 0,
                    IncludedVATInPrice = false,
                    IsStorable = false,
                    ItemType = null,
                    Category = null,
                    Unit = null,

                }).ToList();
            return (List<APIItem>)Session[key];
        }
        public static void AddItem(APIItem postedItem)
        {
            ItemList.GetData().Add(postedItem);
        }
        public static void UpdateItem(APIItem postedItem)
        {
            var editedModel = ItemList.GetData().First(i => i.Code == postedItem.Code);
            editedModel.Code = postedItem.Code;
            editedModel.Name = postedItem.Name;
            editedModel.Description = postedItem.Description;
            editedModel.Price = postedItem.Price;
            editedModel.ReferenceCost = postedItem.ReferenceCost;
            editedModel.Cost = postedItem.Cost;
            editedModel.ServiceChangeInPercentage = postedItem.ServiceChangeInPercentage;
            editedModel.IncludedVATInPrice = postedItem.IncludedVATInPrice;
            editedModel.IsStorable = postedItem.IsStorable;
            editedModel.ItemType = postedItem.ItemType;
            editedModel.Category = postedItem.Category;
            editedModel.Unit = postedItem.Unit;

        }
        public static void DeleteItem(string ID)
        {
            var deleteItem = ItemList.GetData().First(i => i.Code == ID);
            ItemList.GetData().Remove(deleteItem);
        }
    }
}