using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Models;
namespace GridViewBatchEdit.Models
{
    public class SampleModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [CustomDisplayName("String1")]
        public string SampleString { get; set; }
        public DateTime SampleDate { get; set; }
        public bool? SampleBool { get; set; }
        public decimal SampleDecimal { get; set; }
    }
    public static class SampleModelList
    {
        public static List<SampleModel> GetData()
        {

            var key = "34FAA431-CF79-4869-9488-93F6AAE81263";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 100).Select(i => new SampleModel
                {
                    ID = i,
                    SampleString = "Test String" + i,
                    SampleDate = new DateTime(2000 + i, 12, 16),
                    SampleBool = i % 2 == 0,
                    SampleDecimal = i * 10000,
                }).ToList();
            return (List<SampleModel>)Session[key];
        }
        public static void AddItem(SampleModel postedItem)
        {

            SampleModelList.GetData().Add(postedItem);


        }
        public static void UpdateItem(SampleModel postedItem)
        {
            var editedModel = SampleModelList.GetData().First(i => i.ID == postedItem.ID);
            editedModel.SampleString = postedItem.SampleString;
            editedModel.SampleBool = postedItem.SampleBool;
            editedModel.SampleDate = postedItem.SampleDate;
            editedModel.SampleDecimal = postedItem.SampleDecimal;
        }
        public static void DeleteItem(int ID)
        {
            var deleteItem = SampleModelList.GetData().First(i => i.ID == ID);
            SampleModelList.GetData().Remove(deleteItem);
        }
    }
}