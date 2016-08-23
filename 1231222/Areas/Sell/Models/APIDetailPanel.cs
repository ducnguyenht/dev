using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
    public class APIDetailPanel
    {
        //[BindRex("ID")]
        public int ID { get; set; }
        //Lô [BindRex("ID")]
        public int Lot { get; set; }
        //Loại [BindRex("ID")]
        public string ItemCategory { get; set; }
        //Quy cách [BindRex("ID")]
        public string Specification { get; set; }
        //Số lượng [BindRex("ID")]
        public int QuantitySpecification { get; set; }
        //Đơn vị tính [BindRex("ID")]
        public string UoM { get; set; }
        //Khối lượng [BindRex("ID")]
        public int QuantityUoM { get; set; }
        //Đơn giá [BindRex("ID")]
        public decimal Price { get; set; }
        //Tiền hàng trc thuế [BindRex("ID")]
        public decimal SubAmount { get; set; }
        //Thuế(%) [BindRex("ID")]
        public string VATInPercentage { get; set; }
        //Tiền thuế [BindRex("ID")]
        public decimal VATAmount { get; set; }
        //Tiền hàng [BindRex("ID")]
        public decimal Amount { get; set; }
        //Ghi chú [BindRex("ID")]
        public string Note { get; set; }
    }
}