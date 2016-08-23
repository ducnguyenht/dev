using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
    public class APISalesOnline
    {
        [BindRex("APISalesOnline_Code")]
        public string Code { get; set; }
        [BindRex("APISalesOnline_OrderDate")]
        public DateTime OrderDate { get; set; }
        [BindRex("APISalesOnline_PostingDate")]
        public DateTime PostingDate { get; set; }
        [BindRex("APISalesOnline_Customer")]
        public string Customer { get; set; }
        [BindRex("APISalesOnline_CustomerTaxCode")]
        public string CustomerTaxCode { get; set; }
        [BindRex("APISalesOnline_VATInvoiceSerialNumber")]
        public string VATInvoiceSerialNumber { get; set; }
        [BindRex("APISalesOnline_Phone")]
        public string Phone { get; set; }
        /// <summary>
        /// email, điện thoại nhân viên kinh doanh
        /// </summary>
        [BindRex("APISalesOnline_Email")]
        public string Email { get; set; }
        [BindRex("APISalesOnline_Total")]
        public decimal Total { get; set; }
        [BindRex("APISalesOnline_CustomerAddress")]
        public string CustomerAddress { get; set; }
        [BindRex("APISalesOnline_Discription")]
        public string Discription { get; set; }
        [BindRex("APISalesOnline_CreatedBy")]
        public string CreatedBy { get; set; }
        [BindRex("APISalesOnline_Status")]
        public string Status { get; set; }
        [BindRex("APISalesOnline_IncludedVATVoice")]
        public string IncludedVATVoice { get; set; }

        public enum SalesOnlineStatus
        {
            /// <summary>
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

    public class APISalesOnlineList
    {
        public static List<APISalesOnline> GetData()
        {
            string key = "62E40249-DAE6-477B-8422-0C4EC42017CF";
            if (HttpContext.Current.Session[key] == null)
            {
                List<APISalesOnline> Sale = new List<APISalesOnline>();
                Sale.Add(
                    new APISalesOnline()
                    {
                        Code = (DateTime.Now.Ticks).ToString(),
                        OrderDate = new DateTime(2016, 07, 02),
                        PostingDate = new DateTime(2016, 07, 02),
                        Customer = "(Á Châu) Công Ty TNHH Cơ Nhiệt Á Châu",
                        CustomerTaxCode = "0305662375",
                        VATInvoiceSerialNumber = "HD_01",
                        Phone = "090123456",
                        Total = 2000000000,
                        CustomerAddress = "14a/10 Nguyễn Duy",
                        Discription = "",
                        CreatedBy = "admin@naansolution.vn",
                        Status = MVC.Areas.Sell.Models.APISalesOnline.SalesOnlineStatus.Approving.ToString(),
                        IncludedVATVoice = "",
                    }
                );
                Sale.Add(
                    new APISalesOnline()
                    {
                        Code = (DateTime.Now.Ticks + 1).ToString(),
                        OrderDate = new DateTime(2016, 07, 02),
                        PostingDate = new DateTime(2016, 07, 02),
                        Customer = "(Anh Nhật Phát) Cty Cổ Phần Anh Nhật Phát",
                        CustomerTaxCode = "0305662375",
                        VATInvoiceSerialNumber = "HD_02",
                        Phone = "090111222",
                        Total = 2000000000,
                        CustomerAddress = "1B Núi Thành, P. 13, Q. Tân Bình, Tp. HCM",
                        Discription = "",
                        CreatedBy = "admin@naansolution.vn",
                        Status = MVC.Areas.Sell.Models.APISalesOnline.SalesOnlineStatus.Completed.ToString(),
                        IncludedVATVoice = "",
                    }
                );
                Sale.Add(
                    new APISalesOnline()
                    {
                        Code = (DateTime.Now.Ticks + 2).ToString(),
                        OrderDate = new DateTime(2016, 07, 02),
                        PostingDate = new DateTime(2016, 07, 02),
                        Customer = "(An Khánh) Công Ty Cổ Phần Xây Dựng An Khánh",
                        CustomerTaxCode = "0305662375",
                        VATInvoiceSerialNumber = "HD_03",
                        Phone = "090333555",
                        Total = 2000000000,
                        CustomerAddress = "Số 10, Ấp 2,Xã Nhựt Chánh, Huyện Bến Lức, Tỉnh Long An",
                        Discription = "",
                        CreatedBy = "admin@naansolution.vn",
                        Status = MVC.Areas.Sell.Models.APISalesOnline.SalesOnlineStatus.Shipping.ToString(),
                        IncludedVATVoice = "",
                    }
                );
                HttpContext.Current.Session[key] = Sale;
            }
            return (List<APISalesOnline>)HttpContext.Current.Session[key];
        }
        public static void AddItem(APISalesOnline postedItem)
        {
            List<APISalesOnline> list = GetData();
            postedItem.Code = (DateTime.Now.Ticks).ToString();
            list.Add(postedItem);


        }


        public static void UpdateItem(APISalesOnline postedItem)
        {
            var editedModel = GetData().First(i => i.Code == postedItem.Code);

            editedModel.Code = postedItem.Code;
            editedModel.OrderDate = postedItem.OrderDate;
            editedModel.PostingDate = postedItem.PostingDate;
            editedModel.Customer = postedItem.Customer;
            editedModel.CustomerTaxCode = postedItem.CustomerTaxCode;
            editedModel.VATInvoiceSerialNumber = postedItem.VATInvoiceSerialNumber;
            editedModel.Phone = postedItem.Phone;
            editedModel.Email = postedItem.Email;
            editedModel.Total = postedItem.Total;
            editedModel.CustomerAddress = postedItem.CustomerAddress;
            editedModel.Discription = postedItem.Discription;
            editedModel.CreatedBy = postedItem.CreatedBy;
            editedModel.Status = postedItem.Status;
            editedModel.IncludedVATVoice = postedItem.IncludedVATVoice;
        }
        public static void DeleteItem(string code)
        {
            List<APISalesOnline> list = GetData();
            list.Remove(list.Where(w => w.Code == code).First());
        }
    }

}