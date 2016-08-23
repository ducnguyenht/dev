using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models.Accounting.Configuration
{
    public class AccountOrderModelList
    {
        public static IList<APIAccountOrder> AccountOrders
        {
            get
            {
                string key = "A546BCB7-4EB1-4D76-ADA7-661AA43B1DB0";
                if (HttpContext.Current.Session[key] == null)
                    HttpContext.Current.Session[key] = GenerateAccountOrders();
                return (IList<APIAccountOrder>)HttpContext.Current.Session[key];
            }
        }
        public static IList<APIAccount> Accounts
        {
            get
            {
                string key = "0CFA04B4-3698-4A0F-9A98-4F68CF790982";
                if (HttpContext.Current.Session[key] == null)
                    HttpContext.Current.Session[key] = GenerateAccounts();
                return (IList<APIAccount>)HttpContext.Current.Session[key];
            }
        }
        public static IEnumerable GetAccountsByAccountOrder(int accountOrderID)
        {
            return from account in Accounts where account.AccountOrderID == accountOrderID select account;
        }
        public static APIAccount GetAccountByID(int id)
        {
            return (from account in Accounts where account.IDAccount == id select account).SingleOrDefault<APIAccount>();
        }
        public static APIAccountOrder GetAccountOrderByID(int id)
        {
            return (from accountOrder in AccountOrders where accountOrder.IDAccountOrder == id select accountOrder).SingleOrDefault<APIAccountOrder>();
        }
        public static int GenerateAccountOrderID()
        {
            return AccountOrders.Count > 0 ? AccountOrders.Last().IDAccountOrder + 1 : 0;
        }
        public static int GenerateAccountID()
        {
            return Accounts.Count > 0 ? Accounts.Last().IDAccount + 1 : 0;
        }

        public static void InsertAccount(APIAccount account)
        {
            if (account != null)
            {
                account.IDAccount = GenerateAccountID();
                Accounts.Add(account);
            }
        }
        public static void UpdateAccount(APIAccount account)
        {
            APIAccount editableAccount = GetAccountByID(account.IDAccount);
            if (editableAccount != null)
            {
                editableAccount.IDAccount = account.IDAccount;
                editableAccount.AccountOrderID = account.AccountOrderID;
                editableAccount.Name = account.Name;
                editableAccount.Level = account.Level;
                editableAccount.PreDefinitionName = account.PreDefinitionName;
                editableAccount.AccountType = account.AccountType;
            }
        }
        public static void RemoveAccountByID(int id)
        {
            APIAccount editableAccount = GetAccountByID(id);
            if (editableAccount != null)
                Accounts.Remove(editableAccount);
        }

        public static void InsertAccountOrder(APIAccountOrder accountOrder)
        {
            if (accountOrder != null)
            {
                accountOrder.IDAccountOrder = GenerateAccountOrderID();
                AccountOrders.Add(accountOrder);
            }
        }
        public static void UpdateAccountOrder(APIAccountOrder accountOrder)
        {
            APIAccountOrder editableAccountOrder = GetAccountOrderByID(accountOrder.IDAccountOrder);
            if (editableAccountOrder != null)
            {
                editableAccountOrder.IDAccountOrder = accountOrder.IDAccountOrder;
                editableAccountOrder.Name = accountOrder.Name;
                editableAccountOrder.Title = accountOrder.Title;
                editableAccountOrder.Notes = accountOrder.Notes;
                editableAccountOrder.AllowPosting = accountOrder.AllowPosting;
                editableAccountOrder.IsActive = accountOrder.IsActive;
            }
        }
        public static void RemoveAccountOrderByID(int id)
        {
            APIAccountOrder editableAccountOrder = GetAccountOrderByID(id);
            if (editableAccountOrder != null)
                AccountOrders.Remove(editableAccountOrder);
        }

        static IList<APIAccount> GenerateAccounts()
        {
            return new List<APIAccount>{
                        new APIAccount(){
                            IDAccount = 0,
                            AccountOrderID = 0,
                            Name = "111",
                            Level = 1,
                            PreDefinitionName = "F",
                            AccountType = "Bold"
                        },
                        new APIAccount(){
                            IDAccount = 1,
                            AccountOrderID = 0,
                            Name = "112",
                            Level = 1,
                            PreDefinitionName = "F",
                            AccountType = "Bold"
                        },
                        new APIAccount(){
                            IDAccount = 2,
                            AccountOrderID = 1,
                            Name = "113",
                            Level = 1,
                            PreDefinitionName = "F",
                            AccountType = "Bold"
                        },
                        new APIAccount(){
                            IDAccount = 3,
                            AccountOrderID = 2,
                            Name = "114",
                            Level = 1,
                            PreDefinitionName = "F",
                            AccountType = "Bold"
                        }
                    };
        }
        static IList<APIAccountOrder> GenerateAccountOrders()
        {
            List<APIAccountOrder> accountOrders = new List<APIAccountOrder>();
            accountOrders.Add(
                new APIAccountOrder()
                {
                    IDAccountOrder = 0,
                    Name = "1",
                    Title = "Tài khoản 01",
                    Notes = "Tài khoản 01",
                    AllowPosting = true,
                    IsActive = true
                }
            );
            accountOrders.Add(
                new APIAccountOrder()
                {
                    IDAccountOrder = 1,
                    Name = "2",
                    Title = "Tài khoản 02",
                    Notes = "Tài khoản 02",
                    AllowPosting = true,
                    IsActive = true
                }
            );
            accountOrders.Add(
                new APIAccountOrder()
                {
                    IDAccountOrder = 2,
                    Name = "3",
                    Title = "Tài khoản 03",
                    Notes = "Tài khoản 03",
                    AllowPosting = true,
                    IsActive = true
                }
            );
            return accountOrders;
        }
    }
    public class APIAccountOrder
    {
        public int IDAccountOrder { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public bool AllowPosting { get; set; }
        public bool IsActive { get; set; }

    }
    public class APIAccount
    {
        public int IDAccount { get; set; }
        public int AccountOrderID { get; set; }


        public string Name { get; set; }
        public short Level { get; set; }
        public string PreDefinitionName { get; set; }
        public string AccountType { get; set; }
        public APIAccount ParentAccount { get; set; }

    }
}