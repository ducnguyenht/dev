using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MVC.Areas.Accounting.Models.Configuration
{
    public class APIBank
    {
        [Key]
        //[Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string SwiftCode { get; set; }
        public Image Logo { get; set; }
    }

    public class BankModelList
    {
        public static List<APIBank> GetData()
        {
            var key = "0D1E3E53-F80D-4FA6-9B5D-A81489BD0D58";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 100).Select(i => new APIBank
                {
                    Code = i.ToString(),
                    Name = "Test String " + i,
                    Address = i + " Street",
                    SwiftCode = ""
                }).ToList();
            return (List<APIBank>)Session[key];
        }


        public static void AddItem(APIBank postedItem)
        {
            List<APIBank> list = GetData();
            postedItem.Code = (list.Count + 1).ToString();
            list.Add(postedItem);
        }


        public static void UpdateItem(APIBank postedItem)
        {
            var editedModel = GetData().First(i => i.Code == postedItem.Code);
            editedModel.Name = postedItem.Name;
            editedModel.Address = postedItem.Address;
            editedModel.SwiftCode = postedItem.SwiftCode;
            editedModel.Logo = postedItem.Logo;
        }
        public static void DeleteItem(string code)
        {
            List<APIBank> list = GetData();
            list.Remove(list.Where(w => w.Code == code).First());
        }
    }
}