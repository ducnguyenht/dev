using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Collections;
using Utility;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.BO.Vouches;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Vouches;
using NAS.BO.Accounting.Journal;
using WebModule.PayReceiving.Report;
using DevExpress.XtraPrintingLinks;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;
using NAS.BO.System.ArtifactCode;


namespace ERPCore.PayReceiving.UserControl
{
    public partial class ReceiptVoucherEdit : System.Web.UI.UserControl
    {

        private class PrivateSession
        {
            //private constructor
            private PrivateSession()
            {
                this.ReceiptVoucherId = Guid.Empty;
            }
            // Gets the current session
            public static PrivateSession Instance
            {
                get
                {
                    PrivateSession session =
                        (PrivateSession)HttpContext.Current.Session["ERPCore_PayReceiving_UserControl_ReceiptVoucherEdit"];
                    if (session == null)
                    {
                        session = new PrivateSession();
                        HttpContext.Current.Session["ERPCore_PayReceiving_UserControl_ReceiptVoucherEdit"] = session;
                    }
                    return session;
                }
            }

            /// <summary>
            /// Clear instance of PrivateSession
            /// </summary>
            public static void ClearInstance()
            {
                HttpContext.Current.Session.Remove("ERPCore_PayReceiving_UserControl_ReceiptVoucherEdit");
            }

            /////Declares all session properties here
            public Guid ReceiptVoucherId { get; set; }

        }

        private Session session;
        private ArtifactCodeRuleBO artifactCodeRuleBO;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsReceiptVouches.Session = session;
            dsReceiptVouchesType.Session = session;
            dsSourceOrg.Session = session;
            dsVouchersAmount.Session = session;
            dsForeignCurrency.Session = session;
            
        }


        public void InvisibleCommandColumnGridviewIfApprovedCosting(ASPxGridView gridview, string commandColumnName)
        {
            //if (!gridview.Columns[commandColumnName].Visible) return;

            //if (PrivateSession.Instance.ReceiptVoucherId != Guid.Empty)
            //{
            //    bool isApprovedCosting =
            //        TransactionBO.isApprovedCosting<NAS.DAL.Vouches.ReceiptVouches>(PrivateSession.Instance.ReceiptVoucherId);
            //    if (isApprovedCosting)
            //    {
            //        gridview.Columns[commandColumnName].Visible = false;
            //    }
            //    else
            //    {
            //        gridview.Columns[commandColumnName].Visible = true;
            //    }
            //}
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

            dsSourceOrg.Criteria = criteria.ToString();
            cbSourceOrganization.DataBindItems();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            artifactCodeRuleBO = new ArtifactCodeRuleBO();

            SetCriteriaForOrganization();
            dsVouchersAmount.CriteriaParameters["VouchesId"].DefaultValue = PrivateSession.Instance.ReceiptVoucherId.ToString();
            /*2013-11-24 Khoa.Truong MOD START*/
            //grdVouchersAmount.DataBind();
            if (!IsPostBack)
            {
                grdVouchersAmount.DataBind();
            }
            /*2013-11-24 Khoa.Truong MOD END*/
            this.InvisibleCommandColumnGridviewIfApprovedCosting(grdVouchersAmount, "CommonOperations");

            if (Page.IsPostBack)
            {
                if (hReceiptViewer.Contains("print"))
                {

                    XPQuery<ReceiptVouches> receiptVouches = new XPQuery<ReceiptVouches>(session);
                    XPQuery<VouchesAmount> vouchesAmount = new XPQuery<VouchesAmount>(session);

                    var list = from r in receiptVouches.AsEnumerable()
                               join va in vouchesAmount.AsEnumerable() on r.VouchesId equals va.VouchesId.VouchesId
                               where r.VouchesId == PrivateSession.Instance.ReceiptVoucherId
                               select new C01TT
                               {
                                   Code = r.Code,
                                   IssuedDate = r.IssuedDate,
                                   Description = r.Description,
                                   CustomerName = r.SourceOrganizationId.Name,
                                   Address = r.Address,
                                   ReceiverName = r.Payer,
                                   Debit = va.Debit,
                                   ExchangeRate = va.ExchangeRate,
                                   DebitExchange = va.Debit * va.ExchangeRate,
                                   //DebitByString = Utility.Accounting.NumberToStringFullCurrency(float.Parse(va.Debit.ToString()), va.ForeignCurrencyId.Name),
                                   //Currency = va.ForeignCurrencyId.Code
                                   DebitByString = Utility.Accounting.NumberToStringFullCurrency(float.Parse(va.Debit.ToString()), va.CurrencyId.Name),
                                   Currency = va.CurrencyId.Code
                               };

                    List<C01TT> lst = new List<C01TT>();
                    try
                    {
                        if (list.Count<C01TT>() > 0)
                        {
                            lst = list.Cast<C01TT>().ToList();
                        }
                    }
                    catch (Exception)
                    {
                        lst = null;
                    }
                    

                    XPQuery<ReceiptVouchesTransaction> receiptVouchesTransaction = new XPQuery<ReceiptVouchesTransaction>(session);
                    XPQuery<GeneralJournal> generalJournal = new XPQuery<GeneralJournal>(session);

                    var listg = from p in receiptVouchesTransaction.AsEnumerable()
                                join g in generalJournal.AsEnumerable() on p.TransactionId equals g.TransactionId.TransactionId
                                where p.ReceiptVouchesId.VouchesId == PrivateSession.Instance.ReceiptVoucherId
                                orderby g.Credit, g.Debit
                                select new
                                {
                                    Dc = g.Debit > g.Credit ? "Nợ :" : "Có :",
                                    Account = g.AccountId.Code,
                                    Amount = Math.Max(g.Debit, g.Credit)
                                };

                    grdBooking.DataSource = listg.ToList();
                    grdBooking.DataBind();

                    _01_TT report = new _01_TT();
                    report.DataSource = lst;
                    report.DataMember = "";
                    report.pccData.PrintableComponent = new PrintableComponentLinkBase() { Component = gvDataExporter };

                    rptReceiptViewer.Report = report;

                    hReceiptViewer.Remove("print");

                    cpReceiptViewer.JSProperties.Add("cpShowForm", "report");
                }
                
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void grdVouchersAmount_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["VouchesId!Key"] = PrivateSession.Instance.ReceiptVoucherId;
            //If is default foreign currency
            ForeignCurrency currentForeignCurrency =
                session.GetObjectByKey<ForeignCurrency>((Guid)e.NewValues["ForeignCurrencyId!Key"]);
            if (currentForeignCurrency.Name.Equals("VNĐ"))
            {
                e.NewValues["ExchangeRate"] = (double)1;
            }
        }

        protected void grdVouchersAmount_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //ASPxGridView grid = sender as ASPxGridView;

            ////Validate Debit 
            //if (e.NewValues["Debit"] == null
            //    || e.NewValues["Debit"].ToString().Trim().Length == 0)
            //{
            //    Helpers.AddErrorToGridViewColumn(e.Errors,
            //                    grid.Columns["Debit"],
            //                    "Chưa nhập số tiền thu");
            //}
            //else
            //{
            //    float debit = (float)e.NewValues["Debit"];
            //    if (debit <= 0)
            //    {
            //        Helpers.AddErrorToGridViewColumn(e.Errors,
            //                    grid.Columns["Debit"],
            //                    "Số tiền thu phải lớn hơn 0");
            //    }
            //}

            ////validate ExchangeRate
            //if (e.NewValues["ExchangeRate"] != null
            //    && e.NewValues["ExchangeRate"].ToString().Trim().Length > 0)
            //{
            //    float exchangeRate = (float)e.NewValues["ExchangeRate"];
            //    if (exchangeRate <= 0)
            //    {
            //        Helpers.AddErrorToGridViewColumn(e.Errors,
            //                    grid.Columns["ExchangeRate"],
            //                    "Tỉ giá phải lớn hơn 0");
            //    }
            //}


        }

        protected void grdVouchersAmount_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "ConvertedAmount")
            {
                decimal debit = Convert.ToDecimal(e.GetListSourceFieldValue("Debit"));
                decimal exchangeRate = Convert.ToDecimal(e.GetListSourceFieldValue("ExchangeRate"));
                e.Value = debit * exchangeRate;
            }
        }

        protected void grdVouchersAmount_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            if (e.Column.FieldName.Equals("ExchangeRate"))
            {
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
            ReceiptVouchesBO.UpdateSumOfDebit(PrivateSession.Instance.ReceiptVoucherId);
        }

        protected void grdVouchersAmount_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {
            ReceiptVouchesBO.UpdateSumOfDebit(PrivateSession.Instance.ReceiptVoucherId);
        }

        protected void grdVouchersAmount_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
        {
            grdVouchersAmount.JSProperties["cpEvent"] = "rowCountChanged";
            ReceiptVouchesBO.UpdateSumOfDebit(PrivateSession.Instance.ReceiptVoucherId);
        }

        private void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmReceiptVouches);
            txtCode.IsValid = true;
            cbSourceOrganization.IsValid = true;
            cbVouchesType.IsValid = true;
            dateIssuedDate.IsValid = true;
            grdVouchersAmount.CancelEdit();
        }

        private ReceiptVouches CurrentReceiptVouches
        {
            get
            {
                if (PrivateSession.Instance.ReceiptVoucherId == Guid.Empty)
                {
                    return null;
                }
                return
                    session.GetObjectByKey<ReceiptVouches>(PrivateSession.Instance.ReceiptVoucherId);
            }
        }

        protected void popReceiptVouchesEdit_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
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
                    ReceiptVouches tempReceiptVouches = ReceiptVouches.InitNewRow(session);
                    popReceiptVouchesEdit.JSProperties["cpNewRecordId"] = tempReceiptVouches.VouchesId.ToString();
                    PrivateSession.Instance.ReceiptVoucherId = tempReceiptVouches.VouchesId;
                    dsReceiptVouches.CriteriaParameters["VouchesId"].DefaultValue = PrivateSession.Instance.ReceiptVoucherId.ToString();
                    dsVouchersAmount.CriteriaParameters["VouchesId"].DefaultValue = PrivateSession.Instance.ReceiptVoucherId.ToString();
                    this.InvisibleCommandColumnGridviewIfApprovedCosting(grdVouchersAmount, "CommonOperations");
                    ClearForm();
                    txtCode.Text = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.VOUCHER_RECEIPT);
                    //DND 851
                    dateIssuedDate.Value = DateTime.Now;


                    //END DND
                    break;
                case "edit":
                    ClearForm();
                    if (args.Length > 1)
                    {
                        PrivateSession.Instance.ReceiptVoucherId = Guid.Parse(args[1]);
                        dsReceiptVouches.CriteriaParameters["VouchesId"].DefaultValue = PrivateSession.Instance.ReceiptVoucherId.ToString();
                        dsVouchersAmount.CriteriaParameters["VouchesId"].DefaultValue = PrivateSession.Instance.ReceiptVoucherId.ToString();
                        txtCode.Text = CurrentReceiptVouches.Code;
                        this.InvisibleCommandColumnGridviewIfApprovedCosting(grdVouchersAmount, "CommonOperations");
                        //DND
                        if (bo.searchOrgDefault(session, CurrentReceiptVouches.SourceOrganizationId.OrganizationId.ToString()))
                        {
                            popReceiptVouchesEdit.JSProperties.Add("cpIsDefaultSourceOrg", true);
                        }
                        else
                        {
                            popReceiptVouchesEdit.JSProperties.Add("cpIsDefaultSourceOrg", false);
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
                        if (!ASPxEdit.AreEditorsValid(frmReceiptVouches, true))
                        {
                            popReceiptVouchesEdit.JSProperties.Add("cpInvalid", true);
                            return;
                        }

                        //collect data for saving
                        string code = txtCode.Text;
                        DateTime issueDate = new DateTime();
                        if (!DateTime.TryParse(dateIssuedDate.Text, out issueDate)) issueDate = DateTime.Now;

                        string description = memoDescription.Text;
                        string address = memoAddress.Text;
                        //2013-11-18 Khoa.Truong INS START
                        //Guid voucherTypeId = Guid.Parse(cbVouchesType.SelectedItem.Value.ToString());
                        //Guid sourceOrgId = Guid.Parse(cbSourceOrganization.SelectedItem.Value.ToString());
                        //2013-11-18 Khoa.Truong INS END

                        //2013-11-18 Khong.Truong DEL START
                        ////DND 851
                        Guid sourceOrgId;
                        bo = new ReceiptVouchesBO(); //ham dinh nghia ben NAS.BO
                        string cbVouchesType_name = cbVouchesType.Text;


                        if (cbSourceOrganization.Text == null || cbSourceOrganization.Text.Equals(""))
                        {
                            sourceOrgId = Guid.Parse(bo.searchOrganizationId(session));
                        }
                        else
                        {
                            string[] org_code = cbSourceOrganization.Text.ToString().Split('-');

                            org_code[0] = org_code[0].Trim();

                            sourceOrgId = Guid.Parse(bo.searchOrgId(session, org_code[0]));
                        }
                        Guid voucherTypeId = Guid.Parse(bo.searchVouchesTypeId(session, cbVouchesType_name));
                        ////END DND
                        //2013-11-18 Khong.Truong DEL END

                        string payer = txtPayer.Text;
                        //Logic to save data 
                        
                        if (args.Length > 1)
                        {
                            //Update mode
                            //Update general information
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            ReceiptVouchesBO receiptVouchesBO = new ReceiptVouchesBO();
                            receiptVouchesBO.Update(PrivateSession.Instance.ReceiptVoucherId,
                                                    code,
                                                    issueDate,
                                                    description,
                                                    address,
                                                    payer,
                                                    Constant.ROWSTATUS_ACTIVE,
                                                    sourceOrgId,
                                                    Utility.CurrentSession.Instance.AccessingOrganizationId,
                                                    voucherTypeId);
                        }
                        else
                        {
                            //Insert mode
                            ReceiptVouchesBO receiptVouchesBO = new ReceiptVouchesBO();
                            receiptVouchesBO.Insert(PrivateSession.Instance.ReceiptVoucherId,
                                                    code,
                                                    issueDate,
                                                    description,
                                                    address,
                                                    payer,
                                                    Constant.ROWSTATUS_ACTIVE,
                                                    sourceOrgId,
                                                    Utility.CurrentSession.Instance.AccessingOrganizationId,
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
                        popReceiptVouchesEdit.JSProperties.Add("cpCallbackArgs",
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
            if (PrivateSession.Instance.ReceiptVoucherId == Guid.Empty)
            {
                bool isExist = Util.isExistXpoObject<ReceiptVouches>("Code", vouchesCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                if (isExist)
                {
                    e.IsValid = false;
                    e.ErrorText =
                        String.Format("Mã phiếu thu '{0}' đã được sử dụng", vouchesCode);
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
                if (!vouchesCode.Equals(CurrentReceiptVouches.Code))
                {
                    bool isExist = Util.isExistXpoObject<ReceiptVouches>("Code", vouchesCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                    if (isExist)
                    {
                        e.IsValid = false;
                        e.ErrorText = String.Format("Mã phiếu thu '{0}' đã được sử dụng", vouchesCode);
                    }
                    else
                    {
                        e.IsValid = true;
                        e.ErrorText = String.Empty;
                    }
                }
            }
        }

        protected void grdVouchersAmount_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Equals("getRowCount"))
            {
                grdVouchersAmount.JSProperties["cpVisibleRowCount"] = grdVouchersAmount.VisibleRowCount;
            }
        }

        //protected void cpReceiptViewer_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        //{
        //    string[] p = e.Parameter.Split('|');
        //    switch (p[0])
        //    {
        //        case "":
        //            break;
        //        default:
        //            break;
        //    }

        //}


    }
}