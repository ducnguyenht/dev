using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
    public class APISales
    {
        public int ID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime PostingDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTaxCode { get; set; }
        public string VATInvoiceSerialNumber { get; set; }
        public string CustomerPhone { get; set; }
        public decimal Total { get; set; }
        public string CustomerAddress { get; set; }
        public string Discription { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public string IncludedVATVoice { get; set; }
    }
    public class APISalesList
    {
        public List<APISales> GetData()
        {
            string key = "48A87427-15CA-41A1-B7EC-02496F7F5AD7";
            if (HttpContext.Current.Session[key] == null)
            {
                List<APISales> Sale = new List<APISales>();
                Sale.Add(
                    new APISales()
                    {
                        ID = 0,                      
                        IssueDate = new DateTime(2016, 07, 02),
                        PostingDate = new DateTime(2016, 07, 02),
                        CustomerName = "(Á Châu) Công Ty TNHH Cơ Nhiệt Á Châu",
                        CustomerTaxCode= "0305662375",
                        VATInvoiceSerialNumber = "HD_01",
                        CustomerPhone="090123456",
                        Total=2000000000,
                        CustomerAddress = "14a/10 Nguyễn Duy",
                        Discription="",
                       CreatedBy="admin@naansolution.vn",
                       Status="Ghi Sổ",
                       IncludedVATVoice="",
                    }
                );
                Sale.Add(
                    new APISales()
                    {
                        ID = 1,
                        IssueDate = new DateTime(2016, 07, 02),
                        PostingDate = new DateTime(2016, 07, 02),
                        CustomerName = "(Anh Nhật Phát) Cty Cổ Phần Anh Nhật Phát",
                        CustomerTaxCode = "0305662375",
                        VATInvoiceSerialNumber = "HD_02",
                        CustomerPhone = "090111222",
                        Total = 2000000000,
                        CustomerAddress = "1B Núi Thành, P. 13, Q. Tân Bình, Tp. HCM",
                        Discription = "",
                        CreatedBy = "admin@naansolution.vn",
                        Status = "Hoàn tác",
                        IncludedVATVoice = "",
                    }
                );
                Sale.Add(
                    new APISales()
                    {
                        ID = 2,
                        IssueDate = new DateTime(2016, 07, 02),
                        PostingDate = new DateTime(2016, 07, 02),
                        CustomerName = "(An Khánh) Công Ty Cổ Phần Xây Dựng An Khánh",
                        CustomerTaxCode = "0305662375",
                        VATInvoiceSerialNumber = "HD_03",
                        CustomerPhone = "090333555",
                        Total = 2000000000,
                        CustomerAddress = "Số 10, Ấp 2,Xã Nhựt Chánh, Huyện Bến Lức, Tỉnh Long An",
                        Discription = "",
                        CreatedBy = "admin@naansolution.vn",
                        Status = "Nháp",
                        IncludedVATVoice = "",
                    }
                );
                HttpContext.Current.Session[key] = Sale;
            }
            return (List<APISales>)HttpContext.Current.Session[key];
        }
        public void AddItem(APISales postedItem)
        {
            List<APISales> list = GetData();
            postedItem.ID = (list.Count + 1);
            list.Add(postedItem);

           
        }


        public void UpdateItem(APISales postedItem)
        {
            var editedModel = GetData().First(i => i.ID == postedItem.ID);

            editedModel.ID = postedItem.ID;
            editedModel.IssueDate = postedItem.IssueDate;
            editedModel.PostingDate = postedItem.PostingDate;
            editedModel.CustomerName = postedItem.CustomerName;
            editedModel.CustomerTaxCode = postedItem.CustomerTaxCode;
            editedModel.VATInvoiceSerialNumber = postedItem.VATInvoiceSerialNumber;
            editedModel.CustomerPhone = postedItem.CustomerPhone;
            editedModel.Total = postedItem.Total;
            editedModel.CustomerAddress = postedItem.CustomerAddress;
            editedModel.Discription = postedItem.Discription;
            editedModel.CreatedBy = postedItem.CreatedBy;
            editedModel.Status = postedItem.Status;
            editedModel.IncludedVATVoice = postedItem.IncludedVATVoice;
        }
        public void DeleteItem(int id)
        {
            List<APISales> list = GetData();
            list.Remove(list.Where(w => w.ID == id).First());
        }
    }
}