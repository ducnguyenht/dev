using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evaluant.Calculator;
using NAS.DAL.Accounting.Currency;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.Period;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Data.Filtering;
using WebModule.Accounting.UserControl.uLegalInvoiceArtifact.State;
using NAS.DAL.Nomenclature.Item;
using NAS.BO.Accounting.LegalInvoiceArtifact;
using DevExpress.Web.ASPxGridView;
using NAS.BO.Accounting;
using NAS.DAL.Vouches;
using NAS.DAL.Nomenclature.Bank;
using DevExpress.Web.ASPxEditors;

namespace WebModule.Accounting.UserControl
{
    public partial class uCurrency : System.Web.UI.UserControl
    {


        Session session;
        CurrencyBO BO = new CurrencyBO();
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            DBExchangeRate.Session = session;
            DBBank.Session = session;
            DBCurrency.Session = session;
            DBCurrencyType.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected object GetMasterRowKeyValue(ASPxGridView Grid)
        {
            GridViewBaseRowTemplateContainer container = null;
            Control control = Grid;
            while (control.Parent != null)
            {
                container = control.Parent as GridViewBaseRowTemplateContainer;
                if (container != null) break;
                control = control.Parent;
            }
            return container.KeyValue;
        }

        #region tao bien session
        private string Currency_Id
        {
            get { return (string)Session["CurrencyId"]; }
            set { Session["CurrencyId"] = value; }
        }
        private string CurrencyType_Id
        {
            get { return (string)Session["CurrencyType_Id"]; }
            set { Session["CurrencyType_Id"] = value; }
        }

        private string CurrencyType_CurencyId
        {
            get { return (string)Session["CurrencyType_CurencyId"]; }
            set { Session["CurrencyType_CurencyId"] = value; }
        }
        #endregion

        protected void Grid_ExchangeRate_Init(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView grid = sender as ASPxGridView;
                object keyvalue = GetMasterRowKeyValue(grid);
                CurrencyType currencyTypeid = session.GetObjectByKey<CurrencyType>(keyvalue);
                NAS.DAL.Accounting.Currency.Currency currencyId = session.FindObject<NAS.DAL.Accounting.Currency.Currency>(
                    CriteriaOperator.And(
                        new BinaryOperator("CurrencyTypeId", currencyTypeid, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                        new BinaryOperator("IsDefault", true, BinaryOperatorType.Equal)
                    ));
                if (currencyId != null)
                {
                    CurrencyType_CurencyId = currencyId.CurrencyId.ToString();
                    CurrencyType_Id = currencyTypeid.CurrencyTypeId.ToString();

                }
                else
                {
                    throw new Exception(String.Format("Bạn chưa chọn Đơn Vị Mặc Định của {0}", currencyTypeid.Name));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Grid_ExchangeRate_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                session.BeginTransaction();

                ASPxGridView grid = sender as ASPxGridView;

                ExchangeRate exchangeid = new ExchangeRate(session);
                object keyvalue = grid.GetMasterRowKeyValue();
                CurrencyType currencyTypeid = session.GetObjectByKey<CurrencyType>(Guid.Parse(keyvalue.ToString()));
                NAS.DAL.Accounting.Currency.Currency currencyid = BO.get_Currency_true_master(session, currencyTypeid.CurrencyTypeId.ToString(), true, Utility.Constant.ROWSTATUS_ACTIVE);

                //NAS.DAL.Accounting.Currency.Currency NumeratorCurrencyId = BO.get_CurrencyId_currencyId(session, currencyid.CurrencyId.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);
                //NAS.DAL.Accounting.Currency.Currency DenomiratorCurrencyId = BO.get_CurrencyId_currencyId(session, Currency_Id.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);

                //if (BO.check_ExchangeRate_BankId(
                //    session,
                //    NumeratorCurrencyId.CurrencyId.ToString(),
                //    DenomiratorCurrencyId.CurrencyId.ToString(),
                //    bankid.BankId.ToString(),
                //    Utility.Constant.ROWSTATUS_ACTIVE
                //    ))
                //{
                //    throw new Exception(String.Format("Ngân Hàng {0} Trùng, Yêu Cầu Nhập Lại Ngân Hàng", bankid.Code));
                //}

                exchangeid.AffectedDate = DateTime.Parse(e.NewValues["AffectedDate"].ToString());
                if (e.NewValues["BankId!Key"] != null)
                {
                    Bank bankid = session.GetObjectByKey<Bank>(Guid.Parse(e.NewValues["BankId!Key"].ToString()));

                    if (bankid != null)
                    {
                        exchangeid.BankId = bankid;
                        exchangeid.Name = bankid.Code;
                    }
                }
                exchangeid.Rate = float.Parse(e.NewValues["Rate"].ToString());
                if (e.NewValues["Description"] != null)
                    exchangeid.Description = e.NewValues["Description"].ToString();
                exchangeid.NumeratorCurrencyId = BO.get_CurrencyId_currencyId(session, currencyid.CurrencyId.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);
                exchangeid.DenomiratorCurrencyId = BO.get_CurrencyId_currencyId(session, Currency_Id.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);
                exchangeid.Status = short.Parse(e.NewValues["Status"].ToString());
                exchangeid.RowCreationTimeStamp = DateTime.Now;
                exchangeid.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                exchangeid.Save();

                session.CommitTransaction();
                grid.CancelEdit();
                grid.DataBind();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        protected void Grid_ExchangeRate_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                session.BeginTransaction();

                ASPxGridView grid = sender as ASPxGridView;

                ExchangeRate exchangeid = session.GetObjectByKey<ExchangeRate>(Guid.Parse(e.Keys[0].ToString()));
                object keyvalue = grid.GetMasterRowKeyValue();
                CurrencyType currencyTypeid = session.GetObjectByKey<CurrencyType>(Guid.Parse(keyvalue.ToString()));
                NAS.DAL.Accounting.Currency.Currency currencyid = BO.get_Currency_true_master(session, currencyTypeid.CurrencyTypeId.ToString(), true, Utility.Constant.ROWSTATUS_ACTIVE);

                //NAS.DAL.Accounting.Currency.Currency NumeratorCurrencyId = BO.get_CurrencyId_currencyId(session, currencyid.CurrencyId.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);
                //NAS.DAL.Accounting.Currency.Currency DenomiratorCurrencyId = BO.get_CurrencyId_currencyId(session, Currency_Id.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);
                //if (!e.NewValues["BankId!Key"].ToString().Equals(e.OldValues["BankId!Key"].ToString()))
                //    if (BO.check_ExchangeRate_BankId(
                //        session,
                //        NumeratorCurrencyId.CurrencyId.ToString(),
                //        DenomiratorCurrencyId.CurrencyId.ToString(),
                //        bankid.BankId.ToString(),
                //        Utility.Constant.ROWSTATUS_ACTIVE
                //        ))
                //    {
                //        throw new Exception(String.Format("Ngân Hàng {0} Trùng, Yêu Cầu Nhập Lại Ngân Hàng", bankid.Code));
                //    }

                exchangeid.AffectedDate = DateTime.Parse(e.NewValues["AffectedDate"].ToString());
                if (e.NewValues["BankId!Key"]!= null)
                {
                    Bank bankid = session.GetObjectByKey<Bank>(Guid.Parse(e.NewValues["BankId!Key"].ToString()));
                    if (bankid != null)
                    {
                        exchangeid.BankId.BankId = bankid.BankId;
                        exchangeid.Name = bankid.Code;
                    }
                }
                exchangeid.Rate = float.Parse(e.NewValues["Rate"].ToString());
                if (e.NewValues["Description"] != null)
                    exchangeid.Description = e.NewValues["Description"].ToString();
                exchangeid.NumeratorCurrencyId = BO.get_CurrencyId_currencyId(session, currencyid.CurrencyId.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);
                exchangeid.DenomiratorCurrencyId = BO.get_CurrencyId_currencyId(session, Currency_Id.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);
                exchangeid.Status = short.Parse(e.NewValues["Status"].ToString());
                exchangeid.Save();

                session.CommitTransaction();
                grid.CancelEdit();
                grid.DataBind();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        protected void Grid_ExchangeRate_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                //e.Cancel = true;
                session.BeginTransaction();
                ASPxGridView grid = sender as ASPxGridView;

                //ExchangeRate exchangeid = session.GetObjectByKey<ExchangeRate>(Guid.Parse(e.Keys[0].ToString()));
                //exchangeid.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                //exchangeid.Save();

                session.CommitTransaction();
                grid.CancelEdit();
                grid.DataBind();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }

        }

        protected void Grid_ExchangeRate_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            try
            {
                ASPxGridView grid = sender as ASPxGridView;
                NAS.DAL.Accounting.Currency.Currency currency_true_master = BO.get_Currency_true_master(session, CurrencyType_Id, true, Utility.Constant.ROWSTATUS_ACTIVE);
                ExchangeRate exchangerate_DenomiratorCurrencyId = BO.get_ExchangeRate(session, e.KeyValue.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);
                NAS.DAL.Accounting.Currency.Currency currencyId = BO.get_CurrencyID(session, exchangerate_DenomiratorCurrencyId.DenomiratorCurrencyId.CurrencyId.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);

                if (currencyId != null)
                {
                    //Setting Description for node
                    if (e.DataColumn.Name == "DenomiratorCurrencyId")
                    {
                        e.Cell.Text = String.Format("{0}", currencyId.Code);
                    }
                    if (e.DataColumn.Name == "Description_edit")
                    {
                        e.Cell.Text = String.Format("1 {0} = {1} {2}", currency_true_master.Code, exchangerate_DenomiratorCurrencyId.Rate, currencyId.Code);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Grid_ExchangeRate_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                ASPxGridView grid = sender as ASPxGridView;
                CurrencyType currencytype_true = BO.get_CurrencyType_true(session, true, Utility.Constant.ROWSTATUS_ACTIVE);
                NAS.DAL.Accounting.Currency.Currency currency_true = BO.get_Currency_true_master(session, currencytype_true.CurrencyTypeId.ToString(), true, Utility.Constant.ROWSTATUS_ACTIVE);
                //ExchangeRate ex = session.FindObject<ExchangeRate>(
                //    CriteriaOperator.And(
                //        new BinaryOperator("DenomiratorCurrencyId", currencytype_true, BinaryOperatorType.Equal),
                //        new BinaryOperator("NumeratorCurrencyId", CurrencyType_CurencyId, BinaryOperatorType.Equal),
                //        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                //    ));
                if (e.Column.Name.Equals("AffectedDate") && grid.IsEditing)
                    e.Editor.Focus();
                if (e.Column.Name.Equals("AffectedDate") && grid.IsNewRowEditing)
                    e.Editor.Focus();
                if (currency_true != null)
                {
                    Currency_Id = currency_true.CurrencyId.ToString();
                    //if (ex != null)
                    //{
                    NAS.DAL.Accounting.Currency.Currency currencyId = BO.get_CurrencyID(session, CurrencyType_CurencyId.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);


                    if (e.Column.Name.Equals("DenomiratorCurrencyId") && grid.IsEditing)
                        e.Editor.Value = String.Format("{0}", currency_true.Code.ToString());
                    if (e.Column.Name.Equals("DenomiratorCurrencyId") && grid.IsNewRowEditing)
                        e.Editor.Value = String.Format("{0}", currency_true.Code.ToString());

                    if (e.Column.Name.Equals("Description_edit") && grid.IsEditing)
                        e.Editor.Visible = false;
                    if (e.Column.Name.Equals("Description_edit") && grid.IsNewRowEditing)
                        e.Editor.Visible = false;
                    //}
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Grid_CurrencyType_Init(object sender, EventArgs e)
        {
            //Grid_CurrencyType.SettingsDetail.AllowOnlyOneMasterRowExpanded = true;
            //if (Grid_CurrencyType.SettingsDetail.AllowOnlyOneMasterRowExpanded)
            //{
            //    Grid_CurrencyType.DetailRows.CollapseAllRows();
            //}
        }

        protected void Grid_ExchangeRate_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView grid = sender as ASPxGridView;
                object keyvalue = GetMasterRowKeyValue(grid);
                CurrencyType currencyTypeid = session.GetObjectByKey<CurrencyType>(keyvalue);
                NAS.DAL.Accounting.Currency.Currency currencyId = session.FindObject<NAS.DAL.Accounting.Currency.Currency>(
                    CriteriaOperator.And(
                        new BinaryOperator("CurrencyTypeId", currencyTypeid, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                        new BinaryOperator("IsDefault", true, BinaryOperatorType.Equal)
                    ));
                if (currencyId != null)
                {
                    Session["SessionNumeratorCurrencyId"] = currencyId.CurrencyId.ToString();
                    CurrencyType_CurencyId = currencyId.CurrencyId.ToString();
                    CurrencyType_Id = currencyTypeid.CurrencyTypeId.ToString();

                }
                else
                {
                    throw new Exception(String.Format("Bạn chưa chọn Đơn Vị Mặc Định của {0}", currencyTypeid.Name));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Grid_CurrencyType_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit
                || e.ButtonType == ColumnCommandButtonType.Delete)
            {
                bool isInternal = (bool)Grid_CurrencyType.GetRowValues(e.VisibleIndex, "IsInternal");
                if (isInternal)
                {
                    e.Visible = false;
                }
            }
        }
    }
}