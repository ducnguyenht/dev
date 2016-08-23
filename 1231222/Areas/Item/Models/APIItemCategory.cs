using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Item.Models
{
    public class APIItemCategory
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


    }

    public static class ItemCategoryList
    {
        public static List<APIItemCategory> GetData()
        {
            var key = "34FAA431-CF79-4869-9488-93F6AAE81300";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 100).Select(i => new APIItemCategory
                {
                    Code = "ItemCategory_" + i,
                    Name = "NameItemCategory_" + i,
                    Description = "",
                }).ToList();

            return (List<APIItemCategory>)Session[key];
        }

        #region Add ---------------------------------------
        public static void AddItemCategory(APIItemCategory e)
        {
            ItemCategoryList.GetData().Add(e);
        }
        #endregion ----------------------------------------

        #region Update ----------------------------
        public static void UpdateItemCategory(APIItemCategory e)
        {
            var editModel = ItemCategoryList.GetData().First(i => i.Code == e.Code);
            editModel.Code = e.Code;
            editModel.Name = e.Name;
            editModel.Description = e.Description;
        }
        #endregion -----------------------------------

        #region Delete ----------------------------
        public static void DeleteItemCategory(string e)
        {
            var deleteItemCategory = ItemCategoryList.GetData().First(i => i.Code == e);
            ItemCategoryList.GetData().Remove(deleteItemCategory);
        }
        #endregion -----------------------------------
    }
}