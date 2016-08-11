using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.AccountChart;
using NAS.BO;
using NAS.DAL.Accounting.Journal;
using NAS.BO.Accounting.Journal;
using NAS.BO.Accounting;
using System.ComponentModel;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.JournalAllocation;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Web.ASPxTreeView;
using System.Data;
using System.Reflection;
using NAS.DAL.Inventory.Ledger;
using DevExpress.Web.ASPxTreeList;
using System.Drawing;
using System.Globalization;
using NAS.DAL.Accounting.Period;
using Utility;

namespace WebModule.Accounting.UserControl
{
    public class CAccountBalance
    {
        public Guid AccountId {get;set;}
        public Guid ParentAccountId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }

    public partial class ucBalanceInit : System.Web.UI.UserControl
    {
        Session session;

        public string m_FormatNumber = "{0:n6}";
        public int m_Number = 0;

        protected void BalanceLineSetData()
        {
            balanceLineXDS.CriteriaParameters.Clear();
            if (cboBalanceInitAccount.Value != null)
            {              
                balanceLineXDS.CriteriaParameters.Add("AccountId", cboBalanceInitAccount.Value.ToString());
            }
            //if (cboBalanceInitCurrency.Value != null)
            //{                
            //    balanceLineXDS.CriteriaParameters.Add("CurrencyId", cboBalanceInitCurrency.Value.ToString());
            //}
            if (cboAccountPeriod.Value != null)
            {
                balanceLineXDS.CriteriaParameters.Add("AccountingPeriodId", cboAccountPeriod.Value.ToString());
            }          

            grdBalanceLine.DataBind();
            

            // View All Account Balance
            

            XPQuery<Account> _account = new XPQuery<Account>(session);

            var list = from aa in _account.Where(aaa => aaa.RowStatus >= 1 
                                                            && !aaa.Code.StartsWith("5")
                                                            && !aaa.Code.StartsWith("6")
                                                            && !aaa.Code.StartsWith("7")
                                                            && !aaa.Code.StartsWith("8")
                                                            && !aaa.Code.StartsWith("9"))
                                                            
                       select new CAccountBalance
                       {
                           AccountId = aa.AccountId,
                           ParentAccountId = aa.ParentAccountId.AccountId,
                           Code = aa.Code,
                           Name = aa.Name,
                           Level = aa.Level,
                           Debit = getDebitCredit(aa.Code, aa.ParentAccountId)[0],
                           Credit = getDebitCredit(aa.Code, aa.ParentAccountId)[1]
                       };
            try
            {

                IList<CAccountBalance> _data = list.ToList();

                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(CAccountBalance));

                DataTable _table = new DataTable();

                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prp = props[i];
                    _table.Columns.Add(prp.Name, prp.PropertyType);
                }

                object[] values = new object[props.Count];
                foreach (CAccountBalance item in _data)
                {
                    if (item.Code == "3331")
                    {
                        string m = item.Code;
                    }

                    for (int i = 0; i < values.Length; i++)
                    {                        
                        values[i] = props[i].GetValue(item);
                    }
                    _table.Rows.Add(values);
                }

                tgrdAccountBalance.DataSource = _table;

            }
            catch
            {
            }

            tgrdAccountBalance.DataBind();


            refreshSummaryFooter();
           
        }

        private void refreshSummaryFooter()
        {
            double[] _value = getBalance();
            tgrdAccountBalance.Summary.Clear();

            TreeListSummaryItem _label = new TreeListSummaryItem();
            _label.ShowInColumn = "Name";
            _label.DisplayFormat = "Tổng số dư tài khoản nội bảng : ";
            tgrdAccountBalance.Summary.Add(_label);

            TreeListSummaryItem _debit = new TreeListSummaryItem();
            _debit.ShowInColumn = "Debit";
            _debit.DisplayFormat = "Nợ : " + String.Format(m_FormatNumber, _value[0]);
            tgrdAccountBalance.Summary.Add(_debit);

            TreeListSummaryItem _credit = new TreeListSummaryItem();
            _credit.ShowInColumn = "Credit";
            _credit.DisplayFormat = "Có : " + String.Format(m_FormatNumber, _value[1]);
            tgrdAccountBalance.Summary.Add(_credit);
        }


        protected double[] getBalance()
        {
            double[] _value = { 0, 0 };
            double _debit = 0;
            double _credit = 0;

            CriteriaOperator _filter = new GroupOperator(GroupOperatorType.And,
                                                    new BinaryOperator("RowStatus", "1", BinaryOperatorType.GreaterOrEqual),        
                                                    new BinaryOperator("TransactionId.AccountingPeriodId.AccountingPeriodId", Guid.Parse(cboAccountPeriod.Value==null?Guid.Empty.ToString():cboAccountPeriod.Value.ToString()), BinaryOperatorType.Equal),
                                                    CriteriaOperator.Or(new BinaryOperator("AccountId.Code", "1%", BinaryOperatorType.Like),
                                                                        new BinaryOperator("AccountId.Code", "2%", BinaryOperatorType.Like),
                                                                        new BinaryOperator("AccountId.Code", "3%", BinaryOperatorType.Like),
                                                                        new BinaryOperator("AccountId.Code", "4%", BinaryOperatorType.Like)));

            XPCollection<GeneralJournalBalanceForward> _generalJournalBalanceForward = new XPCollection<GeneralJournalBalanceForward>(session, _filter);

            foreach (GeneralJournalBalanceForward g in _generalJournalBalanceForward)
            {
                _debit = g.Debit;
                _credit = g.Credit;

                NAS.DAL.Accounting.Currency.Currency _currency;

                Guid _selectCurrency = Guid.Empty;
                double _numRequiredSelectCurrency = 1;

                if (cboBalanceInitCurrency.Value != null)
                {
                    _currency = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(Guid.Parse(cboBalanceInitCurrency.Value.ToString()));
                    if (_currency != null)
                    {
                        _selectCurrency = _currency.CurrencyId;
                        _numRequiredSelectCurrency = _currency.Coefficient;
                    }
                }

                Guid _currentCurrency = Guid.Empty;
                if (g.CurrencyId != null)
                {
                    _currentCurrency = g.CurrencyId.CurrencyId;
                }

                double _numRequiredCurrentCurrency = 1;

                _currency = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(_currentCurrency);
                if (_currency != null)
                {
                    _numRequiredCurrentCurrency = _currency.Coefficient;
                }

                if (_currentCurrency != _selectCurrency && _numRequiredCurrentCurrency > 0)
                {
                    _debit = _numRequiredSelectCurrency * g.Debit / _numRequiredCurrentCurrency;
                    _credit = _numRequiredSelectCurrency * g.Credit / _numRequiredCurrentCurrency;
                }

                _value[0] += _debit;
                _value[1] += _credit;
                
            }

            return _value;

        }

        protected double[] getDebitCredit(string code, Account parent)
        {
            double[] _value = {0,0};

            CriteriaOperator _filter;

            if (code == "3331")
            {
                _filter = "";
            }

            _filter = new GroupOperator(GroupOperatorType.And,
                                     new BinaryOperator("AccountId.Code", String.Format("{0}%", code), BinaryOperatorType.Like),
                                     new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual),
                                     new BinaryOperator("TransactionId.AccountingPeriodId.AccountingPeriodId", Guid.Parse(cboAccountPeriod.Value==null?Guid.Empty.ToString():cboAccountPeriod.Value.ToString()), BinaryOperatorType.Equal));

            XPCollection<GeneralJournalBalanceForward> _generalJournalBalanceForward = new XPCollection<GeneralJournalBalanceForward>(session, _filter);

            foreach (GeneralJournalBalanceForward g in _generalJournalBalanceForward)
            {                                
                double _debit = g.Debit;
                double _credit = g.Credit;

                NAS.DAL.Accounting.Currency.Currency _currency;

                Guid _selectCurrency = Guid.Empty;
                double _numRequiredSelectCurrency = 1;

                if (cboBalanceInitCurrency.Value != null)
                {
                    _currency = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(Guid.Parse(cboBalanceInitCurrency.Value.ToString()));
                    if (_currency != null)
                    {
                        _selectCurrency = _currency.CurrencyId;
                        _numRequiredSelectCurrency = _currency.Coefficient;
                    }
                }

                Guid _currentCurrency = g.CurrencyId.CurrencyId;
                double _numRequiredCurrentCurrency = 1;

                _currency = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(_currentCurrency);
                if (_currency != null)
                {
                    _numRequiredCurrentCurrency = _currency.Coefficient;
                }

                if (_currentCurrency != _selectCurrency)
                {
                    _debit = _numRequiredCurrentCurrency > 0 ? _numRequiredSelectCurrency * g.Debit / _numRequiredCurrentCurrency : 0;
                    _credit = _numRequiredCurrentCurrency > 0 ? _numRequiredSelectCurrency * g.Credit / _numRequiredCurrentCurrency : 0;
                }

                _value[0] += _debit;
                _value[1] += _credit;
            }

            return _value;
        }
       

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            //
            balanceLineXDS.Session = session;
            balanceLineXDS.Criteria = "AccountId.AccountId = ? And TransactionId.AccountingPeriodId.AccountingPeriodId = ? And RowStatus >= 1";
            
        }

        private int GetDecimals(double d, int i = 0)
        {
            double multiplied = (double)((double)d * Math.Pow(10, i));
            if (Math.Round(multiplied) == multiplied)
                return i;
            return GetDecimals(d, i + 1);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingBO accountBO = new AccountingBO();

            //try
            //{
            //    accountBO.InitBalanceForward(session, AccountingPeriodBO.getCurrentAccountingPeriod(session));

            //    lbAccountingPeriod.Text = AccountingPeriodBO.getCurrentAccountingPeriod(session).Code;
            //    lbAccountingPeriodStartdate.Text = AccountingPeriodBO.getCurrentAccountingPeriod(session).FromDateTime.ToShortDateString();
            //    lbAccountingPeriodEnddate.Text = AccountingPeriodBO.getCurrentAccountingPeriod(session).ToDateTime.Date.ToShortDateString();
            //}
            //catch
            //{
            //    cpMessageBox.JSProperties.Clear();
            //    MessageBox1.Message.Text = String.Format("Chưa tạo chu kỳ tháng {0} năm {1}", DateTime.Now.Month, DateTime.Now.Year);
            //    cpMessageBox.JSProperties.Add("cpWarning", "Chưa tạo chu kỳ tháng " + DateTime.Now.Month.ToString());
            //}
            
            List<NAS.BO.Accounting.AccountingBO.AccountingEntity> rs = accountBO.getInitBalanceForward(session, AccountingPeriodBO.getCurrentAccountingPeriod(session));
            AccountList.DataSource = rs;
            AccountList.DataBind();
            AccountList.Focus();

            NAS.DAL.Accounting.Currency.Currency currency;

            if (cboBalanceInitCurrency.Value == null)
            {
                CriteriaOperator _filter = new GroupOperator(GroupOperatorType.And,
                                                    new BinaryOperator("IsDefault", 1, BinaryOperatorType.Equal),
                                                    new BinaryOperator("CurrencyTypeId.IsMaster", 1, BinaryOperatorType.Equal));
                currency = session.FindObject<NAS.DAL.Accounting.Currency.Currency>(_filter);
                if (currency != null)
                {
                    cboBalanceInitCurrency.Value = currency.CurrencyId;
                }     
           
                
            }

            currency = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(Guid.Parse(cboBalanceInitCurrency.Value.ToString()));
            if (currency != null)
            {
                int i = 0;
                string _p = GetDecimals(currency.Coefficient, i).ToString();
                
                if (Int32.Parse(_p) <= 0)
                {
                    m_FormatNumber = m_FormatNumber.Replace('6', '0');
                }
                else
                {
                    m_FormatNumber = m_FormatNumber.Replace('6', char.Parse(_p));              
                }
            }


            BalanceLineSetData();            
            tgrdAccountBalance.ExpandToLevel(1);

            (tgrdAccountBalance.Columns["Debit"] as TreeListDataColumn).PropertiesEdit.DisplayFormatString = m_FormatNumber;
            (tgrdAccountBalance.Columns["Credit"] as TreeListDataColumn).PropertiesEdit.DisplayFormatString = m_FormatNumber;

            if (!Page.IsPostBack)
            {
                refreshSummaryFooter();
            }

            
        }

        protected void AccountList_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName.Equals("Balance"))
            {
                e.Editor.Focus();
            }
        }

        protected void AccountList_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                XPQuery<Account> accountQuery = session.Query<Account>();
                Account account = (from c in accountQuery
                                    where c.Code == e.OldValues["Code"]
                                    select c).FirstOrDefault();
                AccountingBO accountBO = new AccountingBO();
                double _Balance = Double.Parse(e.NewValues["Balance"].ToString());

                AccountingPeriod _accountingPeriod = session.GetObjectByKey<AccountingPeriod>(Guid.Parse(cboAccountPeriod.Value.ToString()));

                if (_accountingPeriod == null)
                    throw new Exception("Chưa chọn kỳ kế toán !");

                AccountingPeriod accountPeriod = _accountingPeriod;
     
                accountBO.UpdateBalanceForwardTransaction(session, account.AccountId, _Balance);
                ASPxGridView grid = (ASPxGridView)sender;
                grid.CancelEdit();
                List<NAS.BO.Accounting.AccountingBO.AccountingEntity> rs = accountBO.getInitBalanceForward(session, _accountingPeriod);
                AccountList.DataSource = rs;
                AccountList.DataBind();           
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                
            }
        }

        protected void AccountList_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (double.Parse(e.NewValues["Balance"].ToString()) < 0)
            {
                e.RowError = "Số dư không thể âm !";
                return;
            }else if(e.NewValues["Balance"]==null){
                e.RowError = "Balance không được để trống !";
                return;
            }
            
        }

        protected void cboBalanceInitCurrency_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            try
            {
                NAS.DAL.Accounting.Currency.Currency obj = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(Guid.Parse(e.Value.ToString()));

                if (obj != null)
                {
                    comboBox.DataSource = new NAS.DAL.Accounting.Currency.Currency[] { obj };
                    comboBox.DataBindItems();
                }
            }
            catch
            {

            }
        }

        protected void cboBalanceInitCurrency_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            XPCollection<NAS.DAL.Accounting.Currency.Currency> collection = new XPCollection<NAS.DAL.Accounting.Currency.Currency>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;
            collection.Criteria = new GroupOperator(GroupOperatorType.And, 
                        CriteriaOperator.Or(
                                new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                                new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)),
                        new BinaryOperator("RowStatus", "1", BinaryOperatorType.GreaterOrEqual));
           
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));
            comboBox.DataSource = collection;
            comboBox.DataBindItems();
        }

        protected void cboBalanceInitAccount_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            if (e.Value == null)
            {
                return;
            }

            Account obj = session.GetObjectByKey<Account>(Guid.Parse(e.Value.ToString()));

            if (obj != null)
            {
                combo.DataSource = new Account[] { obj };
                combo.DataBindItems();
            }
        }

        bool getNotParentAccountList(Account act)
        {
            return true;
        }

        protected void cboBalanceInitAccount_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<Account> collection = new XPCollection<Account>(session);

            collection.SkipReturnedObjects = e.BeginIndex;                        
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            XPCollection<Account> _childAccount = AccountingBO.getNotParentAccountCollection(session);
            CriteriaOperator _filter = CriteriaOperator.Or(
                                    new BinaryOperator("AccountTypeId.AccountCategoryId.AccountCategoryId", Guid.Parse("A5FD76BB-F0D8-40F5-ADF8-6648804BDC62"), BinaryOperatorType.Equal),
                                    new BinaryOperator("AccountTypeId.AccountCategoryId.AccountCategoryId", Guid.Parse("387208A7-8D9E-49DA-8131-A83BA97B9D6B"), BinaryOperatorType.Equal),
                                    new BinaryOperator("AccountTypeId.AccountCategoryId.AccountCategoryId", Guid.Parse("FF561A7E-00D8-4596-B46A-29064BCB09D2"), BinaryOperatorType.Equal),
                                    new BinaryOperator("AccountTypeId.AccountCategoryId.AccountCategoryId", Guid.Parse("C1EC8F33-C4F6-4312-AE4A-6F8FD3A1F5DB"), BinaryOperatorType.Equal));
                               
           _childAccount.Filter = _filter;
            
            CriteriaOperator criteria = CriteriaOperator.And(
                    CriteriaOperator.Or(
                        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                        new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)                                
                        ),                    
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual),                 
                    new InOperator("this", _childAccount)
                    );                                  
            
            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();

        }

        protected void cboBalanceInitAccount_Init(object sender, EventArgs e)
        {            
           
        }
        
        protected void grdBalanceLine_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (cboBalanceInitAccount.Value == null)
            {
                grdBalanceLine.JSProperties.Add("cpAccountInvalid", "invalid");
                e.Cancel = true;
                return;
            }

            if (cboBalanceInitCurrency.Value == null)
            {
                grdBalanceLine.JSProperties.Add("cpCurrencyInvalid", "invalid");
                e.Cancel = true;
                return;
            }

            BalanceForwardTransaction balanceForwardTransaction = new BalanceForwardTransaction(session);

            balanceForwardTransaction.TransactionId = Guid.NewGuid();
            balanceForwardTransaction.Code = e.NewValues["TransactionId.Code"] as string;
            balanceForwardTransaction.Description = e.NewValues["TransactionId.Description"] as string;
            balanceForwardTransaction.RowStatus = 1;
            balanceForwardTransaction.IssueDate = balanceForwardTransaction.CreateDate = DateTime.Now;

            if (cboAccountPeriod.Value == null)
                throw new Exception("Chưa chọn kỳ kế toán !");

            AccountingPeriod _accountingPeriod = session.GetObjectByKey<AccountingPeriod>(Guid.Parse(cboAccountPeriod.Value.ToString()));

            if (_accountingPeriod == null)
                throw new Exception("Chưa chọn kỳ kế toán !");

            balanceForwardTransaction.AccountingPeriodId = _accountingPeriod;

            balanceForwardTransaction.Save();

            e.NewValues["GeneralJournalId"] = Guid.NewGuid().ToString();

            ASPxSpinEdit c = (ASPxSpinEdit)grdBalanceLine.FindEditRowCellTemplateControl((GridViewDataColumn)grdBalanceLine.Columns["Debit"], "colBalanceInitDebit");
            e.NewValues["Debit"] = c.Value.ToString();
            c = (ASPxSpinEdit)grdBalanceLine.FindEditRowCellTemplateControl((GridViewDataColumn)grdBalanceLine.Columns["Credit"], "colBalanceInitCredit");
            e.NewValues["Credit"] = c.Value.ToString();

            e.NewValues["RowStatus"] = "1";
            e.NewValues["AccountId!Key"] = cboBalanceInitAccount.Value.ToString();
            e.NewValues["CurrencyId!Key"] = cboBalanceInitCurrency.Value.ToString();
            e.NewValues["TransactionId!Key"] = balanceForwardTransaction.TransactionId.ToString();         

            BalanceLineSetData();           
        }

        protected void grdBalanceLine_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            if (cboBalanceInitAccount.Value == null || cboBalanceInitCurrency.Value == null)
            {            
                return;
            }

            ObjectBO objectBO = new ObjectBO();
            NAS.DAL.CMS.ObjectDocument.Object debitJounalCMSObject = objectBO.CreateCMSObject(session, ObjectTypeEnum.OPENBALANCE_ACCOUTING);

            GeneralJournal debitGeneralJournal = session.GetObjectByKey<GeneralJournal>(Guid.Parse(e.NewValues["GeneralJournalId"].ToString()));

            GeneralJournalObject debitGeneralJournalObject = null;
            debitGeneralJournalObject = new GeneralJournalObject(session);
            debitGeneralJournalObject.GeneralJournalId = debitGeneralJournal;
            debitGeneralJournalObject.ObjectId = debitJounalCMSObject;       
            debitGeneralJournalObject.Save();

            ObjectType objectType = NAS.DAL.CMS.ObjectDocument.ObjectType.GetDefault(session, ObjectTypeEnum.OPENBALANCE_ACCOUTING);

            GeneralJournalCustomType generalJournalCustomType = new GeneralJournalCustomType(session);
            generalJournalCustomType.GeneralJournalId = debitGeneralJournal;
            generalJournalCustomType.ObjectTypeId = objectType;
            generalJournalCustomType.Save();

            grdBalanceLine.JSProperties.Add("cpRefreshTree", "refresh");
        }

        protected void grdBalanceLine_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (cboBalanceInitAccount.Value == null)
            {                
                grdBalanceLine.JSProperties.Add("cpAccountInvalid", "invalid");
                e.Cancel = true;
                return;
            }

            if (cboBalanceInitCurrency.Value == null)
            {
                grdBalanceLine.JSProperties.Add("cpCurrencyInvalid", "invalid");
                e.Cancel = true;
                return;
            }

            CriteriaOperator _filter = null;

            BalanceForwardTransaction balanceForwardTransaction = session.GetObjectByKey<BalanceForwardTransaction>(Guid.Parse(e.OldValues["TransactionId!Key"].ToString()));
            if (balanceForwardTransaction != null)
            {
                if (cboAccountPeriod.Value == null)
                    throw new Exception("Chưa chọn kỳ kế toán !");

                AccountingPeriod _accountingPeriod = session.GetObjectByKey<AccountingPeriod>(Guid.Parse(cboAccountPeriod.Value.ToString()));

                if (_accountingPeriod == null)
                    throw new Exception("Chưa chọn kỳ kế toán !");

                balanceForwardTransaction.AccountingPeriodId = _accountingPeriod;

                balanceForwardTransaction.Code = e.NewValues["TransactionId.Code"].ToString();
                balanceForwardTransaction.Description = e.NewValues["TransactionId.Description"].ToString();

                balanceForwardTransaction.Save();
            }

            ASPxSpinEdit c = (ASPxSpinEdit)grdBalanceLine.FindEditRowCellTemplateControl((GridViewDataColumn)grdBalanceLine.Columns["Debit"], "colBalanceInitDebit");
            e.NewValues["Debit"] = c.Value.ToString();
            c = (ASPxSpinEdit)grdBalanceLine.FindEditRowCellTemplateControl((GridViewDataColumn)grdBalanceLine.Columns["Credit"], "colBalanceInitCredit");
            e.NewValues["Credit"] = c.Value.ToString();

            e.NewValues["AccountId!Key"] = cboBalanceInitAccount.Value.ToString();
            e.NewValues["CurrencyId!Key"] = cboBalanceInitCurrency.Value.ToString();

            // Check not has Object to Create
            
            GeneralJournal debitGeneralJournal = session.GetObjectByKey<GeneralJournal>(Guid.Parse(e.OldValues["GeneralJournalId"].ToString()));
            if (debitGeneralJournal != null)
            {
                _filter = new BinaryOperator("GeneralJournalId.GeneralJournalId", debitGeneralJournal.GeneralJournalId, BinaryOperatorType.Equal);               
                GeneralJournalObject debitGeneralJournalObject = session.FindObject<GeneralJournalObject>(_filter);
                if (debitGeneralJournalObject == null)
                {
                    ObjectBO objectBO = new ObjectBO();
                    NAS.DAL.CMS.ObjectDocument.Object debitJounalCMSObject = objectBO.CreateCMSObject(session, ObjectTypeEnum.OPENBALANCE_ACCOUTING);              
                
                    debitGeneralJournalObject = new GeneralJournalObject(session);
                    debitGeneralJournalObject.GeneralJournalId = debitGeneralJournal;
                    debitGeneralJournalObject.ObjectId = debitJounalCMSObject;
                    debitGeneralJournalObject.Save();

                    ObjectType objectType = NAS.DAL.CMS.ObjectDocument.ObjectType.GetDefault(session, ObjectTypeEnum.OPENBALANCE_ACCOUTING);

                    GeneralJournalCustomType generalJournalCustomType = new GeneralJournalCustomType(session);
                    generalJournalCustomType.GeneralJournalId = debitGeneralJournal;
                    generalJournalCustomType.ObjectTypeId = objectType;
                    generalJournalCustomType.Save();

                }
                
            }

            BalanceLineSetData();
            grdBalanceLine.JSProperties.Add("cpRefreshTree", "refresh");  
        }

        protected void grdBalanceLine_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //object o = grid.GetRowValuesByKeyValue(e.Keys["ProductID"], "ProductName");

            GeneralJournalBalanceForward generalJournalBalanceForward = session.GetObjectByKey<GeneralJournalBalanceForward>(Guid.Parse(e.Keys["GeneralJournalId"].ToString()));
            if (generalJournalBalanceForward != null)
            {
                generalJournalBalanceForward.RowStatus = -1;
                generalJournalBalanceForward.Save();
            }

            e.Values["RowStatus"] = "-1";
            e.Cancel = true;

            BalanceLineSetData();
            grdBalanceLine.JSProperties.Add("cpRefreshTree", "refresh");
        }

        protected void grdBalanceLine_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] p = e.Parameters.Split('|');
            switch (p[0])
            {
                case "selectAccountChange":
                    if (cboBalanceInitAccount.Value != null)
                    {
                        balanceLineXDS.CriteriaParameters.Add("AccountId", cboBalanceInitAccount.Value.ToString());

                        if (cboAccountPeriod.Value == null)
                        {
                            throw new Exception("Chưa chọn kỳ kế toán !");
                        }

                        balanceLineXDS.CriteriaParameters.Add("TransactionId.AccountingPeriodId.AccountingPeriodId", cboBalanceInitAccount.Value.ToString());                  
                    }                    
                    
                    grdBalanceLine.DataBind();
                    grdBalanceLine.JSProperties.Add("cpRefreshTree", "refresh"); 

                    break;

                case "selectCurrencyChange":
                    if (cboBalanceInitAccount.Value != null)
                    {
                        balanceLineXDS.CriteriaParameters.Add("AccountId", cboBalanceInitAccount.Value.ToString());
                        if (cboAccountPeriod.Value == null)
                        {
                            throw new Exception("Chưa chọn kỳ kế toán !");
                        }

                        balanceLineXDS.CriteriaParameters.Add("TransactionId.AccountingPeriodId.AccountingPeriodId", cboBalanceInitAccount.Value.ToString());     
                    }
                
                    BalanceLineSetData();
                    grdBalanceLine.JSProperties.Add("cpRefreshTree", "refresh"); 

                    break;
                default:
                    break;
            }

        }

        protected void grdBalanceLine_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {         

            GridViewDataColumn col = ((ASPxGridView)sender).Columns["TransactionId.Code"] as GridViewDataColumn;
            if (e.NewValues["TransactionId.Code"] == null)
            {
                e.Errors.Add(col, "");
                e.RowError = "Chưa nhập mã bút toán";
                return;
            }

            ASPxSpinEdit c1 = (ASPxSpinEdit)grdBalanceLine.FindEditRowCellTemplateControl((GridViewDataColumn)grdBalanceLine.Columns["Debit"], "colBalanceInitDebit");
            ASPxSpinEdit c2 = (ASPxSpinEdit)grdBalanceLine.FindEditRowCellTemplateControl((GridViewDataColumn)grdBalanceLine.Columns["Credit"], "colBalanceInitCredit");
            if (c1.Value == null && c2.Value == null)
            {
                e.Errors.Add(col, "");
                e.RowError = "Số dư Nợ(Có) phải lớn hơn 0 ";
                return;
            }
            else
            {
                if (c1.Value != null && c2.Value != null)
                {
                    if (double.Parse(c1.Value.ToString()) > 0 && double.Parse(c2.Value.ToString()) > 0)
                    {
                        e.Errors.Add(col, "");
                        e.RowError = "Số dư Nợ(Có) phải bằng 0 ";
                        return;
                    }
                }
            }
        }

        protected void grdBalanceLine_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Debit" || e.Column.FieldName == "Credit")
            {
                NAS.DAL.Accounting.Currency.Currency _currency;

                Guid _selectCurrency = Guid.Empty;
                double _numRequiredSelectCurrency = 1;

                if (cboBalanceInitCurrency.Value != null)
                {
                    _currency = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(Guid.Parse(cboBalanceInitCurrency.Value.ToString()));
                    if (_currency != null)
                    {
                        _selectCurrency = _currency.CurrencyId;
                        _numRequiredSelectCurrency = _currency.Coefficient;
                    }
                }

                Guid _currentCurrency = Guid.Parse(e.GetFieldValue("CurrencyId!Key").ToString());
                double _numRequiredCurrentCurrency = 1;

                _currency = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(_currentCurrency);
                if (_currency != null)
                {
                    _numRequiredCurrentCurrency = _currency.Coefficient;
                }

                if (_currentCurrency != _selectCurrency)
                {
                    e.DisplayText = String.Format(m_FormatNumber, ((_numRequiredSelectCurrency * double.Parse(e.GetFieldValue(e.Column.FieldName).ToString())) / _numRequiredCurrentCurrency));
                }
             
            }
        }

        protected void grdBalanceLine_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Debit")
            {               
                ASPxSpinEdit c1 = (ASPxSpinEdit)grdBalanceLine.FindEditRowCellTemplateControl((GridViewDataColumn)grdBalanceLine.Columns["Debit"], "colBalanceInitDebit");                
                
            }         
        }      

        protected void grdBalanceLine_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            Guid key = Guid.Parse(e.GetListSourceFieldValue(e.ListSourceRowIndex, grdBalanceLine.KeyFieldName).ToString());
            if (e.Column.FieldName == "TransactionId.Code")
            {                              
                GeneralJournal generalJournal = session.GetObjectByKey<GeneralJournal>(key);

                if (generalJournal != null)
                {
                    e.Value = generalJournal.TransactionId.Code;
                }
            }
            else if (e.Column.FieldName == "TransactionId.Description")
            {
                GeneralJournal generalJournal = session.GetObjectByKey<GeneralJournal>(key);

                if (generalJournal != null)
                {
                    e.Value = generalJournal.TransactionId.Description;
                }
            }
        }

        protected void cpAllocation_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            CriteriaOperator _filter;

              string[] p = e.Parameter.Split('|');
              switch (p[0])
              {
                  case "allocation":

                      // Check not has Object to Create

                      GeneralJournal debitGeneralJournal = session.GetObjectByKey<GeneralJournal>(Guid.Parse(p[1]));
                      if (debitGeneralJournal != null)
                      {
                          _filter = new BinaryOperator("GeneralJournalId.GeneralJournalId", debitGeneralJournal.GeneralJournalId, BinaryOperatorType.Equal);
                          GeneralJournalObject debitGeneralJournalObject = session.FindObject<GeneralJournalObject>(_filter);
                          if (debitGeneralJournalObject == null)
                          {
                              ObjectBO objectBO = new ObjectBO();
                              NAS.DAL.CMS.ObjectDocument.Object debitJounalCMSObject = objectBO.CreateCMSObject(session, ObjectTypeEnum.OPENBALANCE_ACCOUTING);

                              debitGeneralJournalObject = new GeneralJournalObject(session);
                              debitGeneralJournalObject.GeneralJournalId = debitGeneralJournal;
                              debitGeneralJournalObject.ObjectId = debitJounalCMSObject;
                              debitGeneralJournalObject.Save();

                              ObjectType objectType = NAS.DAL.CMS.ObjectDocument.ObjectType.GetDefault(session, ObjectTypeEnum.OPENBALANCE_ACCOUTING);

                              GeneralJournalCustomType generalJournalCustomType = new GeneralJournalCustomType(session);
                              generalJournalCustomType.GeneralJournalId = debitGeneralJournal;
                              generalJournalCustomType.ObjectTypeId = objectType;
                              generalJournalCustomType.Save();

                          }

                      }

                      CriteriaOperator filter = new BinaryOperator("GeneralJournalId.GeneralJournalId", Guid.Parse(p[1]), BinaryOperatorType.Equal);
                      GeneralJournalObject generalJournalObject = session.FindObject<GeneralJournalObject>(filter);
                      
                      if (generalJournalObject != null)
                      {
                          NASCustomFieldDataGridView1.CMSObjectId = Guid.Parse(generalJournalObject.ObjectId.ObjectId.ToString());
                          NASCustomFieldDataGridView1.DataBind();
                      }
                      break;

                  default:
                      break;
              }
        }

        protected void colBalanceInitDebit_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                double _debit = 0;
             
                NAS.DAL.Accounting.Currency.Currency _currency;

                Guid _selectCurrency = Guid.Empty;
                double _numRequiredSelectCurrency = 1;

                if (cboBalanceInitCurrency.Value != null)
                {
                    _currency = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(Guid.Parse(cboBalanceInitCurrency.Value.ToString()));
                    if (_currency != null)
                    {
                        _selectCurrency = _currency.CurrencyId;
                        _numRequiredSelectCurrency = _currency.Coefficient;
                    }
                }

                Guid _currentCurrency = Guid.Parse(container.Grid.GetRowValuesByKeyValue(container.KeyValue, "CurrencyId!Key").ToString());
                double _numRequiredCurrentCurrency = 1;

                _currency = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(_currentCurrency);
                if (_currency != null)
                {
                    _numRequiredCurrentCurrency = _currency.Coefficient;
                }

                if (_currentCurrency != _selectCurrency)
                {
                    _debit = _numRequiredSelectCurrency * double.Parse(container.Grid.GetRowValuesByKeyValue(container.KeyValue, "Debit").ToString()) / _numRequiredCurrentCurrency;                    
                }


                if (_currentCurrency != _selectCurrency)
                {
                    spin.Text = String.Format(m_FormatNumber, _debit);
                }
                else
                {
                    spin.Text = DataBinder.Eval(container.DataItem, "Debit").ToString();
                }
            }
        }

        protected void colBalanceInitCredit_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {

                double _credit = 0;

                NAS.DAL.Accounting.Currency.Currency _currency;

                Guid _selectCurrency = Guid.Empty;
                double _numRequiredSelectCurrency = 1;

                if (cboBalanceInitCurrency.Value != null)
                {
                    _currency = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(Guid.Parse(cboBalanceInitCurrency.Value.ToString()));
                    if (_currency != null)
                    {
                        _selectCurrency = _currency.CurrencyId;
                        _numRequiredSelectCurrency = _currency.Coefficient;
                    }
                }

                Guid _currentCurrency = Guid.Parse(container.Grid.GetRowValuesByKeyValue(container.KeyValue, "CurrencyId!Key").ToString());
                double _numRequiredCurrentCurrency = 1;

                _currency = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(_currentCurrency);
                if (_currency != null)
                {
                    _numRequiredCurrentCurrency = _currency.Coefficient;
                }

                if (_currentCurrency != _selectCurrency)
                {
                    _credit = _numRequiredSelectCurrency * double.Parse(container.Grid.GetRowValuesByKeyValue(container.KeyValue, "Credit").ToString()) / _numRequiredCurrentCurrency;
                }


                if (_currentCurrency != _selectCurrency)
                {
                    spin.Text = String.Format("{0:n6}", _credit);
                }
                else
                {
                    spin.Text = DataBinder.Eval(container.DataItem, "Credit").ToString();
                }
            }
        }

        protected void ASPxTreeList1_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {

        }

        protected void tgrdAccountBalance_HtmlRowPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlRowEventArgs e)
        {
            ASPxTreeList treelist = sender as ASPxTreeList;
            int visibleIndex = treelist.GetVisibleNodes().IndexOf(treelist.FindNodeByKeyValue(e.NodeKey));
            if (e.Level == 1)
                e.Row.Font.Bold = true;
            
            Account _account = session.GetObjectByKey<Account>(e.GetValue("AccountId"));
            if (_account != null)
            {
                if (_account.AccountTypeId.AccountCategoryId.AccountCategoryId == Guid.Parse("C1EC8F33-C4F6-4312-AE4A-6F8FD3A1F5DB"))
                {
                    e.Row.BackColor = Color.FromArgb(211, 235, 183);
                }
                else
                {
                    e.Row.BackColor = Color.FromArgb(0, 255, 255);
                }
            }
            
        }

        protected void tgrdAccountBalance_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
        {

        }

        protected void cboAccountPeriod_Init(object sender, EventArgs e)
        {
            cboAccountPeriod.ClientSideEvents.ValueChanged = "function (s,e) {" +
                                                                 " if (!ASPxClientEdit.ValidateEditorsInContainerById('BalanceInitContainer')) {" +
                                                                    " e.processOnServer = false;" +
                                                                    " return;" +
                                                                " }" +
                                                                " grdBalanceLine.PerformCallback('selectAccountChange|' + e.GetValue);" +
                                                              " }";
        }

        protected void cboAccountPeriod_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                ASPxComboBox combo = source as ASPxComboBox;
                AccountingPeriod obj = session.GetObjectByKey<AccountingPeriod>(e.Value);

                if (obj != null)
                {
                    combo.DataSource = new AccountingPeriod[] { obj };
                    combo.DataBindItems();
                }
            }
            catch
            {
            }
        }

        protected void cboAccountPeriod_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<AccountingPeriod> collection = new XPCollection<AccountingPeriod>(session);

            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            AccountingPeriodTypeBO typeBO = new AccountingPeriodTypeBO();
            AccountingPeriodType minAccountingPeriodType = typeBO.GetMinAccountingPeriodType(session);

            CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
            CriteriaOperator criteria_IsActive = new BinaryOperator("IsActive", true, BinaryOperatorType.Equal);
            CriteriaOperator criteria_Type = new BinaryOperator("AccountingPeriodTypeId", minAccountingPeriodType, BinaryOperatorType.Equal);
            CriteriaOperator criteria = CriteriaOperator.And(criteria_IsActive, criteria_RowStatus, criteria_Type);
            XPCollection<AccountingPeriod> AccountingPeriodCol = new XPCollection<AccountingPeriod>(session, criteria);

            criteria = CriteriaOperator.And(
                    CriteriaOperator.Or(
                        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                        new BinaryOperator("Description", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                        ),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual),
                    new InOperator("this", AccountingPeriodCol)
                    );

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            combo.DataSource = collection;
            combo.DataBindItems();
        }

        protected void grdBalanceLine_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            if (cboAccountPeriod.Value == null)
                throw new Exception("Chưa chọn kỳ kế toán !");
        }

        protected void grdBalanceLine_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (cboAccountPeriod.Value == null)
                throw new Exception("Chưa chọn kỳ kế toán !");
        }

      

    

       
    }
}