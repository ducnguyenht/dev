using MVC.Areas.Accounting.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Accounting.Models.Configuration
{
    public class APIInternalBankAccount
    {
        public string Code { get; set; }
        public string Bank { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Branch { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }

    public class InternalBankAccountModelList
    {
        public static List<APIInternalBankAccount> GetData()
        {
            var key = "AD10A2ED-C633-4661-8BF0-4B7D8B280E02";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
            {
                List<APIInternalBankAccount> listInternalBankAccounts = new List<APIInternalBankAccount>();
                listInternalBankAccounts.Add(new APIInternalBankAccount()
                {
                    Code = Guid.NewGuid().ToString(),
                    Bank = "Ngân hàng VietcomBank",
                    AccountNumber = RandomNumber(14),
                    AccountName = RandomString(20),
                    Branch = RandomString(2),
                    IsActive = true
                });
                listInternalBankAccounts.Add(new APIInternalBankAccount()
                {
                    Code = Guid.NewGuid().ToString(),
                    Bank = "Ngân hàng AgriBank",
                    AccountNumber = RandomNumber(14),
                    AccountName = RandomString(20),
                    Branch = RandomString(2),
                    IsActive = true
                });
                listInternalBankAccounts.Add(new APIInternalBankAccount()
                {
                    Code = Guid.NewGuid().ToString(),
                    Bank = "Ngân hàng TechcomBank",
                    AccountNumber = RandomNumber(14),
                    AccountName = RandomString(20),
                    Branch = RandomString(2),
                    IsActive = true
                });

                Session[key] = listInternalBankAccounts;
                listInternalBankAccounts = null;
            }
            return (List<APIInternalBankAccount>)Session[key];
        }


        private static Random random = new Random();
        public static string RandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }



        public static void AddItem(APIInternalBankAccount postedItem)
        {
            List<APIInternalBankAccount> list = GetData();
            postedItem.Code = Guid.NewGuid().ToString();
            list.Add(postedItem);
        }


        public static void UpdateItem(APIInternalBankAccount postedItem)
        {
            var editedModel = GetData().First(i => i.Code == postedItem.Code);
            editedModel.AccountNumber = postedItem.AccountNumber;
            editedModel.AccountName = postedItem.AccountName;
            editedModel.Branch = postedItem.Branch;
            editedModel.IsActive = postedItem.IsActive;
            editedModel.IsDefault = postedItem.IsDefault;
        }
        public static void DeleteItem(string code)
        {
            List<APIInternalBankAccount> list = GetData();
            list.Remove(list.Where(w => w.Code == code).First());
        }
    }
}