using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Accounting.Models.Configuration
{
    public class APIAccountingPeriod
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; }
        public AccountingPeriodModelStatus Status { get; set; }
    }

    public enum AccountingPeriodModelStatus
    {
        /// <summary>
        /// The draft:Nháp
        /// </summary>
        Draft = 0,

        /// <summary>
        /// The postable:Hoạt động
        /// </summary>
        Postable = 1,

        /// <summary>
        /// The locked:Khóa
        /// </summary>
        Locked = 2,

        /// <summary>
        /// The unlocked:Đã mở khóa
        /// </summary>
        Unlocked = 3,

        /// <summary>
        /// The obsolete:Ngưng hoạt động
        /// </summary>
        Obsolete = 4
    }

    public class AccountingPeriodModelList
    {
        public static List<APIAccountingPeriod> GetData()
        {
            var key = "623B9611-83DD-4D1B-B8D7-BD838FC2BB60";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
            {
                List<APIAccountingPeriod> accountingPeriodModel = new List<APIAccountingPeriod>();
                for (int y = 2016; y <= 2020; y++)
                {
                    for (int m = 1; m <= 12; m++)
                    {
                        accountingPeriodModel.Add(new APIAccountingPeriod()
                        {
                            Code = String.Format("{0}{1}", y, m),
                            Name = String.Format("Tháng {0}/{1}", m, y),
                            FromDate = new DateTime(y, m, 1),
                            ToDate = new DateTime(y, m, DateTime.DaysInMonth(y, m)),
                            Description = String.Format("Chu kỳ kế toán tháng {0}/{1}", m, y),
                            Status = AccountingPeriodModelStatus.Postable
                        });
                    }
                }
                Session[key] = accountingPeriodModel;
            }
            return (List<APIAccountingPeriod>)Session[key];
        }


        public static void AddItem(APIAccountingPeriod postedItem)
        {
            List<APIAccountingPeriod> list = GetData();
            postedItem.Code = (list.Count + 1).ToString();
            list.Add(postedItem);
        }


        public static void UpdateItem(APIAccountingPeriod postedItem)
        {
            var editedModel = GetData().First(i => i.Code == postedItem.Code);
            editedModel.Name = postedItem.Name;
            editedModel.FromDate = postedItem.FromDate;
            editedModel.ToDate = postedItem.ToDate;
            editedModel.Description = postedItem.Description;
            editedModel.Status = postedItem.Status;
        }
        public static void DeleteItem(string code)
        {
            List<APIAccountingPeriod> list = GetData();
            list.Remove(list.Where(w => w.Code == code).First());
        }
    }

}