using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Areas.Item.Models
{
    public class APIItemUnit
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }


    public static class ItemUnitList
    {
        public static List<APIItemUnit> GetData()
        {
            var key = "34FAA431-CF79-4869-9488-93F6AAE81400";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 100).Select(i => new APIItemUnit
                {
                    Code = "ItemUnit" + i,
                    Name = "NameItemUnit_" + i,
                    Description = "",
                }).ToList();

            return (List<APIItemUnit>)Session[key];
        }

        #region Add ---------------------------------------
        public static void AddItemUnit(APIItemUnit e)
        {
            ItemUnitList.GetData().Add(e);
        }
        #endregion ----------------------------------------

        #region Update ----------------------------
        public static void UpdateItemUnit(APIItemUnit e)
        {
            var editModel = ItemUnitList.GetData().First(i => i.Code == e.Code);
            editModel.Code = e.Code;
            editModel.Name = e.Name;
            editModel.Description = e.Description;
        }
        #endregion -----------------------------------

        #region Delete ----------------------------
        public static void DeleteItemUnit(string e)
        {
            var deleteItemUnit = ItemUnitList.GetData().First(i => i.Code == e);
            ItemUnitList.GetData().Remove(deleteItemUnit);
        }
        #endregion -----------------------------------
    }
}