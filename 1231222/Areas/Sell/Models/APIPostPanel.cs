using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
    public class APIPostPanel
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
}