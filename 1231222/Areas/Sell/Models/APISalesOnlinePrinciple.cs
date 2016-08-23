using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
    public class APISalesOnlinePrinciple
    {
        //[BindRex("ID")]
        public int ID { get; set; }
        //[BindRex("PurchasingPrinciplelPanel_DebitAccount")]
        public string DebitAccount { get; set; }
        //[BindRex("PurchasingPrinciplelPanel_DebitPostingActor")]
        public string DebitPostingActor { get; set; }
        //[BindRex("PurchasingPrinciplelPanel_CreditAccount")]
        public string CreditAccount { get; set; }
        //[BindRex("PurchasingPrinciplelPanel_CreditPostingActor")]
        public string CreditPostingActor { get; set; }
        //[BindRex("PurchasingPrinciplelPanel_Amount")]
        public decimal Amount { get; set; }
        //[BindRex("Purchasing_Description")]
        public string Description { get; set; }

    }

    public class SalesOnlinePrincipleList
    {
        public static List<APISalesOnlinePrinciple> GetData()
        {
            var key = "1F314ECC-E95E-44F0-B681-ECAA77EDDA21";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 50).Select(i => new APISalesOnlinePrinciple
                {
                    ID = i,
                    DebitAccount = "tai khoang" + i,
                    DebitPostingActor = "doi tuong" + i,
                    CreditAccount = "1000" + i,
                    CreditPostingActor = "1000" + i,
                    Amount = 10000,
                    Description = "dien giai" + i,


                }).ToList();
            return (List<APISalesOnlinePrinciple>)Session[key];
        }
        public static void AddItem(APISalesOnlinePrinciple postedItem)
        {
            SalesOnlinePrincipleList.GetData().Add(postedItem);
        }
        public static void UpdateItem(APISalesOnlinePrinciple postedItem)
        {
            var editedModel = SalesOnlinePrincipleList.GetData().First(i => i.ID == postedItem.ID);

            editedModel.DebitAccount = postedItem.DebitAccount;
            editedModel.DebitPostingActor = postedItem.DebitPostingActor;
            editedModel.CreditAccount = postedItem.CreditAccount;
            editedModel.CreditPostingActor = postedItem.CreditPostingActor;
            editedModel.Amount = postedItem.Amount;
            editedModel.Description = postedItem.Description;
        }
        public static void DeleteItem(int ma)
        {
            var deleteItem = SalesOnlinePrincipleList.GetData().First(i => i.ID == ma);
            SalesOnlinePrincipleList.GetData().Remove(deleteItem);
        }
    }
}