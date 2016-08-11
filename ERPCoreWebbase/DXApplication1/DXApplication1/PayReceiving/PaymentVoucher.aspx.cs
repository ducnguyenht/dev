using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Collections;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.BO.Vouches;
using DevExpress.Web.ASPxGridView;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Vouches;

namespace ERPCore.PayReceiving
{
    public partial class PaymentVoucher : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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
            dsPaymentVoucher.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void grdPaymentVoucher_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] args = e.Parameters.Split('|');
            switch (args[0])
            {
                case "delete":
                    try
                    {
                        if (args.Length > 1)
                        {
                            Guid recordId = Guid.Parse(args[1]);
                            PaymentVouchesBO.DeleteLogical(recordId);
                            grdPaymentVoucher.JSProperties.Add("cpEvent", "deleted");
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
                string info = (string)grdPaymentVoucher.GetRowValuesByKeyValue(Guid.Parse(rowKey), fieldName);
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

        protected void grdPaymentVoucher_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;
            if (e.Column.FieldName.Equals("EntryBookingStatus"))
            {
                try
                {
                    Guid voucherId = (Guid)gridview.GetRowValues(e.VisibleRowIndex, "VouchesId");
                    bool isApproved = TransactionBO.isApprovedCosting<PaymentVouches>(session, voucherId);
                    if (isApproved)
                    {
                        e.DisplayText = "Đã hạch toán";
                    }
                    else
                    {
                        e.DisplayText = "Chưa hạch toán";
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

    }
}