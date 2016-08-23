using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Areas.Item.Models
{
    public class APIItemType
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


    }


    public static class ItemTypeList
    {
        public static List<APIItemType> GetData()
        {
            var key = "34FAA431-CF79-4869-9488-93F6AAE81200";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 100).Select(i => new APIItemType
                {
                    Code = "ItemType_" + i,
                    Name = "NameItemType_" + i,
                    Description = "...",
                }).ToList();

            return (List<APIItemType>)Session[key];
        }

        #region Add ---------------------------------------
        public static void AddItemType(APIItemType e)
        {
            ItemTypeList.GetData().Add(e);
        }
        #endregion ----------------------------------------

        #region Update ----------------------------
        public static void UpdateItemType(APIItemType e)
        {
            var editModel = ItemTypeList.GetData().First(i => i.Code == e.Code);
            editModel.Code = e.Code;
            editModel.Name = e.Name;
            editModel.Description = e.Description;
        }
        #endregion -----------------------------------

        #region Delete ----------------------------
        public static void DeleteItemType(string e)
        {
            var deleteItemType = ItemTypeList.GetData().First(i => i.Code == e);
            ItemTypeList.GetData().Remove(deleteItemType);
        }
        #endregion -----------------------------------
    }
}