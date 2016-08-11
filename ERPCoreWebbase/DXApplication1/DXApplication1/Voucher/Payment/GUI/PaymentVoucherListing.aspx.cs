using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.BO.Vouches;
using Utility;

namespace WebModule.Voucher.Payment.GUI
{
    public partial class PaymentVoucherListing : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PAYRECEIVE_GROUPID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_PAYRECEIVE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsReceiptVoucher.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void gridviewReceiptVoucher_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] args = e.Parameters.Split('|');
            switch (args[0])
            {
                case "delete":
                    try
                    {
                        if (args.Length > 1)
                        {
                            VoucherBO voucherBO = new VoucherBO();
                            Guid recordId = Guid.Parse(args[1]);
                            voucherBO.DeleteLogical(recordId);
                            gridviewReceiptVoucher.JSProperties.Add("cpEvent", "deleted");
                        }
                        else
                        {
                            throw new Exception("Must be pass id of the record");
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;
                default:
                    break;
            }
        }

        protected void gridviewReceiptVoucher_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("RowStatus"))
            {
                if (e.Value.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                {
                    e.DisplayText = "Đã hạch toán";
                }
                else
                {
                    e.DisplayText = "Chưa hạch toán";
                }
            }
        }

        protected void cpInfoContent_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            try
            {
                string[] args = e.Parameter.Split('|');
                if (args.Length < 2)
                {
                    throw new Exception("Input parameters are not valid. The valid format is 'rowKey|fieldName'");
                }
                string rowKey = args[0];
                string fieldName = args[1];
                string info = (string)gridviewReceiptVoucher.GetRowValuesByKeyValue(Guid.Parse(rowKey), fieldName);
                if (info != null)
                {
                    lblMoreInfoContent.Text = info;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
    }
}