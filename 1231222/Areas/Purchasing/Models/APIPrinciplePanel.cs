using MVC.Areas.Purchasing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Purchasing.Models
{
    public class APIPrinciplePanel
    {
        [BindRex("ID")]
        public int ID { get; set; }
        //[BindRex("PurchasingPrinciplelPanel_DebitAccount")]
        public string DebitAccount { get; set; }
        [BindRex("PurchasingPrinciplelPanel_DebitPostingActor")]
        public string DebitPostingActor { get; set; }
        [BindRex("PurchasingPrinciplelPanel_CreditAccount")]
        public string CreditAccount { get; set; }
        [BindRex("PurchasingPrinciplelPanel_CreditPostingActor")]
        public string CreditPostingActor { get; set; }
        [BindRex("PurchasingPrinciplelPanel_Amount")]
        public decimal Amount { get; set; }
        [BindRex("Purchasing_Description")]
        public string Description { get; set; }
    }
}public static class PrinciplePanelList
    {
        public static List<APIPrinciplePanel> GetData()
        {
            var key = "8BBCF715-9F45-426A-89FC-30729D314E17";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = Enumerable.Range(0, 50).Select(i => new APIPrinciplePanel
                {
                    ID = i,
                    DebitAccount= "tai khoang" +i,
                    DebitPostingActor = "doi tuong" + i,
                    CreditAccount="1000" +i,
                    CreditPostingActor="1000" +i,
                    Amount= 10000,
                    Description= "dien giai" +i,
                

                }).ToList();
            return (List<APIPrinciplePanel>)Session[key];
        }
        public static void AddItem(APIPrinciplePanel postedItem)
        {

            PrinciplePanelList.GetData().Add(postedItem);


        }
        public static void UpdateItem(APIPrinciplePanel postedItem)
        {
            var editedModel = PrinciplePanelList.GetData().First(i => i.ID == postedItem.ID);

            editedModel.DebitAccount = postedItem.DebitAccount;
            editedModel.DebitPostingActor = postedItem.DebitPostingActor;
            editedModel.CreditAccount = postedItem.CreditAccount;
            editedModel.CreditPostingActor = postedItem.CreditPostingActor;
              editedModel.Amount = postedItem.Amount;
            editedModel.Description = postedItem.Description;
        }
        public static void DeleteItem(int ma)
        {
            var deleteItem = PrinciplePanelList.GetData().First(i => i.ID == ma);
            PrinciplePanelList.GetData().Remove(deleteItem);
        }
    }



