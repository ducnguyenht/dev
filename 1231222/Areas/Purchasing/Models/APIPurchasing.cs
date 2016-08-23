using MVC.Areas.Purchasing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Purchasing.Models
{
    public class APIPurchasing
    {
        [BindRex("ID")]
        public int ID { get; set; }
         [BindRex("Purchasing_IssueDate")]
        public DateTime IssueDate { get; set; }
        [BindRex("Purchasing_PostingDate")]
         public DateTime PostingDate { get; set; }
        [BindRex("Purchasing_Supplier")]
        public string Supplier { get; set; }
        [BindRex("Purchasing_Status")]
        public string Status { get; set; }
        [BindRex("Purchasing_Description")]
        public String Description { get; set; }
        [BindRex("Purchasing_VATInvoiceSerialNumber")]
        public string VATInvoiceSerialNumber { get; set; }
        [BindRex("Purchasing_IsPurchaseCost")]
        public string IsPurchaseCost { get; set; }

        public enum PurchasingStatus
        {   /// <summary>
            /// Ghi nhận
            /// </summary>
            Submitted = 0,
            /// <summary>
            /// Đang duyệt
            /// </summary>
            Approving = 1,
            /// <summary>
            /// Đang xử lý
            /// </summary>
            InProgress = 2,
            /// <summary>
            /// Đang giao hàng
            /// </summary>
            Shipping = 3,
            /// <summary>
            /// Hoàn tất
            /// </summary>
            Completed = 4,
            /// <summary>
            /// Tạm hoãn
            /// </summary>
            Pending = 5
        }
    }
    //public class PurchasingDeatilModel
    //{
    //    public int MaPhieu { get; set; }
    //    public string HoaDonGTGT { get; set; }
    //    public string PhanLoaiHD { get; set; }

    //    public string NgayLapHD { get; set; }
    //    public string NguoiBan { get; set; }
    //    public string MST { get; set; }
    //    public string SoTienTruocThue { get; set; }
    //    public string Thue { get; set; }
    //    public string TienThue { get; set; }
    //}
    
}
    public  class PurchasingList
    {
        public static List<APIPurchasing> GetData()
        {
            var key = "40F4ABB8-AAF1-422E-9D49-939D4AB2761B";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
            {
               List<APIPurchasing> purchase= new List<APIPurchasing>();
            purchase.Add(
                new APIPurchasing()
                {
                    ID = 1,
                    IssueDate = new DateTime(2016, 08, 22),
                    PostingDate = new DateTime(2016, 08, 22),
                    Supplier = "Supplier 1",
                    Status =MVC.Areas.Purchasing.Models.APIPurchasing.PurchasingStatus.Approving.ToString(),
                    Description = "Text " ,
                });
              purchase.Add(
                new APIPurchasing()
                {
                    ID = 2,
                    IssueDate = new DateTime(2016, 08, 22),
                    PostingDate = new DateTime(2016, 08, 22),
                    Supplier = "Supplier 2" ,
                    Status =MVC.Areas.Purchasing.Models.APIPurchasing.PurchasingStatus.Completed.ToString(),
                    Description = "Text " ,
                }); 
                purchase.Add(
                new APIPurchasing()
                {
                    ID = 3,
                    IssueDate = new DateTime(2016, 08, 22),
                    PostingDate = new DateTime(2016, 08, 22),
                    Supplier = "Supplier 3" ,
                    Status = MVC.Areas.Purchasing.Models.APIPurchasing.PurchasingStatus.InProgress.ToString(),
                    Description = "Text " ,
                }); 
                HttpContext.Current.Session[key] = purchase;
            }
            return (List<APIPurchasing>)Session[key];
        }
        public static void AddItem(APIPurchasing postedItem)
        {

            PurchasingList.GetData().Add(postedItem);


        }
        public static void UpdateItem(APIPurchasing postedItem)
        {
            var editedModel = PurchasingList.GetData().First(i => i.ID == postedItem.ID);

            editedModel.IssueDate = postedItem.IssueDate;
            editedModel.PostingDate = postedItem.PostingDate;
            editedModel.Supplier = postedItem.Supplier;
            editedModel.Status = postedItem.Status;
            editedModel.Description = postedItem.Description;
        }
        public static void DeleteItem(int ma)
        {
            var deleteItem = PurchasingList.GetData().First(i => i.ID == ma);
            PurchasingList.GetData().Remove(deleteItem);
        }
    }



