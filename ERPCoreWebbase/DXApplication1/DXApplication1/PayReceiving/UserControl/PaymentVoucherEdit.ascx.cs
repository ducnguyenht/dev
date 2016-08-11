using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web.ASPxGridView;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Vouches;
using DevExpress.Web.ASPxEditors;
using NAS.BO.Vouches;
using Utility;
using WebModule.PayReceiving.Report;
using DevExpress.XtraPrintingLinks;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;
using NAS.BO.System.ArtifactCode;

namespace ERPCore.PayReceiving.UserControl
{
    public partial class PaymentVoucherEdit : System.Web.UI.UserControl
    {

        private class PrivateSession
        {
            //private constructor
            private PrivateSession()
            {
                this.PaymentVoucherId = Guid.Empty;
            }
            // Gets the current session
            public static PrivateSession Instance
            {
                get
                {
                    PrivateSession session =
                        (PrivateSession)HttpContext.Current.Session["ERPCore_PayReceiving_UserControl_PaymentVoucherEdit"];
                    if (session == null)
                    {
                        session = new PrivateSession();
                        HttpContext.Current.Session["ERPCore_PayReceiving_UserControl_PaymentVoucherEdit"] = session;
                    }
                    return session;
                }
            }

            /// <summary>
            /// Clear instance of PrivateSession
            /// </summary>
            public static void ClearInstance()
            {
                HttpContext.Current.Session.Remove("ERPCore_PayReceiving_UserControl_PaymentVoucherEdit");
            }

            /////Declares all session properties here
            public Guid PaymentVoucherId { get; set; }
        }

        private Session session;
        private ArtifactCodeRuleBO artifactCodeRuleBO;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsPaymentVoucher.Session = session;
            dsPaymentVouchersType.Session = session;
            dsReceivedOrg.Session = session;
            dsVouchersAmount.Session = session;
            dsForeignCurrency.Session = session;
        }

        private void SetCriteriaForOrganization()
        {
            //Get CUSTOMER trading type
            TradingCategory customerTradingCategory =
                NAS.DAL.Util.getXPCollection<TradingCategory>(session, "Code", "CUSTOMER").FirstOrDefault();
            //Get SUPPLIER trading type
            TradingCategory supplierTradingCategory =
                NAS.DAL.Util.getXPCollection<TradingCategory>(session, "Code", "SUPPLIER").FirstOrDefault();

            CriteriaOperator criteria = CriteriaOperator.And(
                CriteriaOperator.Or(
                    new ContainsOperator("OrganizationCategories",
                        CriteriaOperator.And(
                            new BinaryOperator("TradingCategoryId.TradingCategoryId", customerTradingCategory.TradingCategoryId),
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                        )
                    ),
                    new ContainsOperator("OrganizationCategories",
                        CriteriaOperator.And(
                            new BinaryOperator("TradingCategoryId.TradingCategoryId", supplierTradingCategory.TradingCategoryId),
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                        )
                    )
                ),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
            );

            dsReceivedOrg.Criteria = criteria.ToString();
            cbTargetOrganization.DataBindItems();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            artifactCodeRuleBO = new ArtifactCodeRuleBO();
            SetCriteriaForOrganization();
            dsVouchersAmount.CriteriaParameters["VouchesId"].DefaultValue = PrivateSession.Instance.PaymentVoucherId.ToString();
            /*2013-11-24 Khoa.Truong MOD START*/
            //grdVouchersAmount.DataBind();
            if (!IsPostBack)
            {
                grdVouchersAmount.DataBind();
            }
            /*2013-11-24 Khoa.Truong MOD END*/

            if (Page.IsPostBack)
            {
                if (hPayViewer.Contains("print"))
                {

                    XPQuery<PaymentVouches> paymentVouches = new XPQuery<PaymentVouches>(session);
                    XPQuery<VouchesAmount> vouchesAmount = new XPQuery<VouchesAmount>(session);

                    var list = from p in paymentVouches.AsEnumerable()
                               join va in vouchesAmount.AsEnumerable() on p.VouchesId equals va.VouchesId.VouchesId
                               where p.VouchesId == PrivateSession.Instance.PaymentVoucherId
                               select new C02TT
                               {
                                   Code = p.Code,
                                   IssuedDate = p.IssuedDate,
                                   Description = p.Description,
                                   SupplierName = p.TargetOrganizationId.Name,
                                   Address = p.Address,
                                   PayerName = p.Payee,
                                   Credit = va.Credit,
                                   ExchangeRate = va.ExchangeRate,
                                   CreditExchange = va.Credit * va.ExchangeRate,
                                   //CreditByString = Utility.Accounting.NumberToStringFullCurrency(float.Parse(va.Credit.ToString()), va.ForeignCurrencyId.Name),
                                   CreditByString = Utility.Accounting.NumberToStringFullCurrency(float.Parse(va.Credit.ToString()), va.CurrencyId.Name),
                                   //Currency = va.ForeignCurrencyId.Code
                                   Currency = va.CurrencyId.Code
                               };

                    List<C02TT> lst = new List<C02TT>();
                    try
                    {
                        if (list.Count<C02TT>() > 0)
                        {
                            lst = list.ToList();
                        }
                    }
                    catch (Exception)
                    {
                        list = null;
                    }
                    

                    XPQuery<PaymentVouchesTransaction> paymentVouchesTransaction = new XPQuery<PaymentVouchesTransaction>(session);
                    XPQuery<GeneralJournal> generalJournal = new XPQuery<GeneralJournal>(session);

                    var listg = from p in paymentVouchesTransaction.AsEnumerable()
                                join g in generalJournal.AsEnumerable() on p.TransactionId equals g.TransactionId.TransactionId
                                where p.PaymentVouchesId.VouchesId == PrivateSession.Instance.PaymentVoucherId
                                orderby g.Credit, g.Debit
                                select new
                                {
                                    Dc = g.Debit > g.Credit ? "Nợ :" : "Có :",
                                    Account = g.AccountId.Code,
                                    Amount = Math.Max(g.Debit, g.Credit)
                                };

                    grdBooking.DataSource = listg.ToList();
                    grdBooking.DataBind();

                    new ASPxGridViewCellMerger(grdBooking);

                    _02_TT report = new _02_TT();
                    report.DataSource = lst;
                    report.DataMember = "";
                    report.pccData.PrintableComponent = new PrintableComponentLinkBase() { Component = gvDataExporter };

                    rptPayViewer.Report = report;

                    //PrivateSession.Instance.ReceiptVoucherId

                    hPayViewer.Remove("print");

                    cpPayViewer.JSProperties.Add("cpShowForm", "report");
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        private PaymentVouches CurrentPaymentVouches
        {
            get
            {
                if (PrivateSession.Instance.PaymentVoucherId == Guid.Empty)
                {
                    return null;
                }
                return
                    session.GetObjectByKey<PaymentVouches>(PrivateSession.Instance.PaymentVoucherId);
            }
        }

    

        private void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmPaymentVoucher);
            txtCode.IsValid = true;
            cbTargetOrganization.IsValid = true;
            cbVouchesType.IsValid = true;
            dateIssuedDate.IsValid = true;
            grdVouchersAmount.CancelEdit();
        }

        protected void popPaymentVoucherEdit_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            string[] args = e.Parameter.Split('|');
            ReceiptVouchesBO bo = new ReceiptVouchesBO(); //ham dinh nghia ben NAS.BO

            switch (args[0])
            {
                //DND 851
                case "cbo_click":
                    if (args.Length > 1)
                    {
                        string textAdsress = "";
                        if (args[1].Equals(""))
                        {
                            textAdsress = "";
                        }
                        else
                        {
                            string[] org_code = args[1].ToString().Split('-');
                            org_code[0] = org_code[0].Trim();
                            textAdsress = bo.searchOrgnAdress(session, org_code[0]);
                        }

                        memoAddress.Value = textAdsress;
                        txtPayer.Focus();
                    }
                    break;
                //END DND 851
                case "new":
                    PaymentVouches tempPaymentVouches = PaymentVouches.InitNewRow(session);
                    popPaymentVoucherEdit.JSProperties["cpNewRecordId"] = tempPaymentVouches.VouchesId.ToString();
                    PrivateSession.Instance.PaymentVoucherId = tempPaymentVouches.VouchesId;
                    dsPaymentVoucher.CriteriaParameters["VouchesId"].DefaultValue = PrivateSession.Instance.PaymentVoucherId.ToString();
                    dsVouchersAmount.CriteriaParameters["VouchesId"].DefaultValue = PrivateSession.Instance.PaymentVoucherId.ToString();
                    ClearForm();
                    txtCode.Text = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.VOUCHER_PAYMENT);
                    dateIssuedDate.Value = DateTime.Now;//DND 851
                    break;
                case "edit":
                    ClearForm();
                    if (args.Length > 1)
                    {
                        PrivateSession.Instance.PaymentVoucherId = Guid.Parse(args[1]);
                        dsPaymentVoucher.CriteriaParameters["VouchesId"].DefaultValue = PrivateSession.Instance.PaymentVoucherId.ToString();
                        dsVouchersAmount.CriteriaParameters["VouchesId"].DefaultValue = PrivateSession.Instance.PaymentVoucherId.ToString();
                        txtCode.Text = CurrentPaymentVouches.Code;
                        //DND
                        if (bo.searchOrgDefault(session, CurrentPaymentVouches.TargetOrganizationId.OrganizationId.ToString()))
                        {
                            popPaymentVoucherEdit.JSProperties.Add("cpIsDefaultSourceOrg", true);
                        }
                        else
                        {
                            popPaymentVoucherEdit.JSProperties.Add("cpIsDefaultSourceOrg", false);
                        }
                        //END DND
                    }
                    break;
                case "save":
                    bool isSuccess = true;
                    string recordIdStr = null;
                    try
                    {
                        //Check validation
                        if (!ASPxEdit.AreEditorsValid(frmPaymentVoucher, true))
                        {
                            popPaymentVoucherEdit.JSProperties.Add("cpInvalid", true);
                            return;
                        }

                        //2013-11-18 Khoa.Truong DEL START
                        //DND 851
                        Guid targetOrgId;
                        bo = new ReceiptVouchesBO(); //ham dinh nghia ben NAS.BO
                        string cbVouchesType_name = cbVouchesType.Text;

                        if (cbTargetOrganization.Text == null || cbTargetOrganization.Text.Equals(""))
                        {
                            targetOrgId = Guid.Parse(bo.searchOrganizationId(session));
                        }
                        else
                        {
                            string[] org_code = cbTargetOrganization.Text.ToString().Split('-');
                            org_code[0] = org_code[0].Trim();

                            targetOrgId = Guid.Parse(bo.searchOrgId(session, org_code[0]));
                        }

                        Guid voucherTypeId = Guid.Parse(bo.searchVouchesTypeId(session, cbVouchesType_name));
                        //END DND
                        //2013-11-18 Khoa.Truong DEL START

                        //collect data for saving
                        string code = txtCode.Text;
                        DateTime issueDate = DateTime.Parse(dateIssuedDate.Text);
                        string description = memoDescription.Text;
                        string address = memoAddress.Text;
                        //2013-11-18 Khoa.Truong INS START
                        //Guid voucherTypeId = Guid.Parse(cbVouchesType.SelectedItem.Value.ToString());
                        //Guid targetOrgId = Guid.Parse(cbTargetOrganization.SelectedItem.Value.ToString());
                        //2013-11-18 Khoa.Truong INS END
                        string payee = txtPayer.Text;
                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            //Update general information
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            PaymentVouchesBO.Update(PrivateSession.Instance.PaymentVoucherId,
                                                    code,
                                                    issueDate,
                                                    description,
                                                    address,
                                                    payee,
                                                    Constant.ROWSTATUS_ACTIVE,
                                                    Utility.CurrentSession.Instance.AccessingOrganizationId,
                                                    targetOrgId,
                                                    voucherTypeId);
                        }
                        else
                        {
                            //Insert mode
                            PaymentVouchesBO.Insert(PrivateSession.Instance.PaymentVoucherId,
                                                    code,
                                                    issueDate,
                                                    description,
                                                    address,
                                                    payee,
                                                    Constant.ROWSTATUS_ACTIVE,
                                                    Utility.CurrentSession.Instance.AccessingOrganizationId,
                                                    targetOrgId,
                                                    voucherTypeId);
                        }
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        throw;
                    }
                    finally
                    {
                        popPaymentVoucherEdit.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));
                    }
                    break;
                default:
                    break;
            }
        }

        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String vouchesCode = e.Value.ToString().Trim();
            //New mode
            if (PrivateSession.Instance.PaymentVoucherId == Guid.Empty)
            {
                bool isExist = Util.isExistXpoObject<PaymentVouches>("Code", vouchesCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                if (isExist)
                {
                    e.IsValid = false;
                    e.ErrorText =
                        String.Format("Mã phiếu chi '{0}' đã được sử dụng", vouchesCode);
                }
                else
                {
                    e.IsValid = true;
                    e.ErrorText = String.Empty;
                }
            }
            //Edit mode  
            else
            {
                //Validate if new code not equal old code
                if (!vouchesCode.Equals(CurrentPaymentVouches.Code))
                {
                    bool isExist = Util.isExistXpoObject<PaymentVouches>("Code", vouchesCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                    if (isExist)
                    {
                        e.IsValid = false;
                        e.ErrorText = String.Format("Mã phiếu chi '{0}' đã được sử dụng", vouchesCode);
                    }
                    else
                    {
                        e.IsValid = true;
                        e.ErrorText = String.Empty;
                    }
                }
            }
        }

        protected void grdVouchersAmount_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["VouchesId!Key"] = PrivateSession.Instance.PaymentVoucherId;
            //If is default foreign currency
            ForeignCurrency currentForeignCurrency =
                session.GetObjectByKey<ForeignCurrency>((Guid)e.NewValues["ForeignCurrencyId!Key"]);
            if (currentForeignCurrency.Name.Equals("VNĐ"))
            {
                e.NewValues["ExchangeRate"] = (double)1;
            }
        }

        protected void grdVouchersAmount_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "ConvertedAmount")
            {
                decimal credit = Convert.ToDecimal(e.GetListSourceFieldValue("Credit"));
                decimal exchangeRate = Convert.ToDecimal(e.GetListSourceFieldValue("ExchangeRate"));
                e.Value = credit * exchangeRate;
            }
        }

        protected void grdVouchersAmount_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            //if (e.VisibleIndex >= 0)
            //{
            ASPxGridView grid = sender as ASPxGridView;
            if (e.Column.FieldName.Equals("ExchangeRate"))
            {
                //e.Editor.Focus();
                if (e.VisibleIndex >= 0)
                {
                    string foreignCurrencyName = (String)grid.GetRowValues(e.VisibleIndex, "ForeignCurrencyId.Name");
                    if (foreignCurrencyName.Equals("VNĐ"))
                    {
                        e.Editor.ClientVisible = false;
                    }
                }
            }
            if (e.Column.FieldName == "ForeignCurrencyId!Key")
            {
                e.Editor.Focus();
            }

            //}
        }

        protected void grdVouchersAmount_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex >= 0)
            {
                ASPxGridView grid = sender as ASPxGridView;
                if (e.Column.FieldName.Equals("ExchangeRate"))
                {
                    string foreignCurrencyName = (String)grid.GetRowValues(e.VisibleRowIndex, "ForeignCurrencyId.Name");
                    if (foreignCurrencyName != null && foreignCurrencyName.Equals("VNĐ"))
                    {
                        e.DisplayText = "-";
                    }
                }
            }
        }

        protected void grdVouchersAmount_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //If is default foreign currency
            ForeignCurrency currentForeignCurrency =
                session.GetObjectByKey<ForeignCurrency>((Guid)e.NewValues["ForeignCurrencyId!Key"]);
            if (currentForeignCurrency.Name.Equals("VNĐ"))
            {
                e.NewValues["ExchangeRate"] = (double)1;
            }
        }

        protected void grdVouchersAmount_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            grdVouchersAmount.JSProperties["cpEvent"] = "rowCountChanged";
            PaymentVouchesBO.UpdateSumOfCredit(PrivateSession.Instance.PaymentVoucherId);
        }

        protected void grdVouchersAmount_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {
            PaymentVouchesBO.UpdateSumOfCredit(PrivateSession.Instance.PaymentVoucherId);
        }

        protected void grdVouchersAmount_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
        {
            grdVouchersAmount.JSProperties["cpEvent"] = "rowCountChanged";
            PaymentVouchesBO.UpdateSumOfCredit(PrivateSession.Instance.PaymentVoucherId);
        }

        protected void grdVouchersAmount_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Equals("getRowCount"))
            {
                grdVouchersAmount.JSProperties["cpVisibleRowCount"] = grdVouchersAmount.VisibleRowCount;
            }
        }

    }
}