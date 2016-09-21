using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dnGroupMVC01.Areas.DemoClientSideShowHide.Models
{
    public class Master
    {
        public Guid? Id_Master { get; set; }
        public string Name { get; set; }
    }
    public class Detail
    {
        public Guid? Id_Detail { get; set; }
        public Guid? Id_Master { get; set; }
        public string Name { get; set; }
    }
    public class MasterList
    {
        static List<Master> lst;
        public static List<Master> GetData()
        {
            if (lst == null)
            {
                lst = new List<Master>();
                lst.Add(new Master { Id_Master = new Guid("385EEF8E-42A7-44B4-B29A-BBDFDD58DB91"), Name = "master" });
                lst.Add(new Master { Id_Master = new Guid("385EEF8E-42A7-44B4-B29A-BBDFDD58DB92"), Name = "master1" });

            }
            return lst;
        }
        public static List<Master> GetDataByIdRef(Guid Id_Master)
        {
            return GetData().Where(i => i.Id_Master == Id_Master).ToList();
        }
        public static Master FindItem(Guid Id_Master)
        {
            List<Master> list = GetData();
            return list.FirstOrDefault(t => t.Id_Master == Id_Master);
        }
        public static void AddItem(Master postedItem)
        {
            List<Master> list = GetData();
            postedItem.Id_Master = Guid.NewGuid();
            list.Add(postedItem);
        }

        public static void UpdateItem(Master postedItem)
        {
            var editedModel = GetData().First(i => i.Id_Master == postedItem.Id_Master);
            GetData().Remove(editedModel);
            GetData().Add(postedItem);
        }

        public static void DeleteItem(Guid Id_MasterTransaction)
        {
            List<Master> list = GetData();
            list.Remove(list.Where(i => i.Id_Master == Id_MasterTransaction).First());
        }
    }
    public class DetailList
    {
        static List<Detail> lst;
        public static List<Detail> GetData()
        {
            if (lst == null)
            {
                lst = new List<Detail>();
                lst.Add(new Detail { Id_Detail = Guid.NewGuid(), Id_Master = new Guid("385EEF8E-42A7-44B4-B29A-BBDFDD58DB91"), Name = "detail" });
                lst.Add(new Detail { Id_Detail = Guid.NewGuid(), Id_Master = new Guid("385EEF8E-42A7-44B4-B29A-BBDFDD58DB92"), Name = "detail 1" });

            }
            return lst;
        }
        public static List<Detail> GetDataByIdRef(Guid Id_Master)
        {
            return GetData().Where(i => i.Id_Master == Id_Master).ToList();
        }
        public static Detail FindItem(Guid Id_Detail)
        {
            List<Detail> list = GetData();
            return list.FirstOrDefault(t => t.Id_Detail == Id_Detail);
        }
        public static void AddItem(Detail postedItem)
        {
            List<Detail> list = GetData();
            postedItem.Id_Detail = Guid.NewGuid();
            list.Add(postedItem);
        }

        public static void UpdateItem(Detail postedItem)
        {
            var editedModel = GetData().First(i => i.Id_Detail == postedItem.Id_Detail);
            GetData().Remove(editedModel);
            GetData().Add(postedItem);
        }

        public static void DeleteItem(Guid Id_Detail)
        {
            List<Detail> list = GetData();
            list.Remove(list.Where(i => i.Id_Detail == Id_Detail).First());
        }
    }
}