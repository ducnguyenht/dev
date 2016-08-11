using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.BO.Accounting;
using NAS.DAL.Accounting.Currency;
using DevExpress.Data.Filtering;
using NAS.DAL;
using NAS.DAL.Vouches;
using NAS.DAL.Nomenclature.Bank;
using Utility;

namespace NAS.BO.Accounting
{
    public class CurrencyBO
    {
        public CurrencyBO()
        {
        }

        #region check
        public bool checkCurrency_Code(Session session, string code, string key)
        {
            try
            {
                Currency currencyCode = session.FindObject<Currency>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", code.ToString(), BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                        new BinaryOperator("CurrencyTypeId", Guid.Parse(key.ToString()), BinaryOperatorType.Equal)
                        ));
                if (currencyCode != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
        }

        public bool checkCurrencyType_Name(Session session, string name)
        {
            try
            {
                CurrencyType currencyTypeName = session.FindObject<CurrencyType>(
                    CriteriaOperator.And(
                        new BinaryOperator("Name", name, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                    ));
                if (currencyTypeName != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
        }

        public bool checkIsAlreadyHaveAChild(Session session, Guid parentCurrencyId)
        {
            try
            {
                Currency parent = session.GetObjectByKey<Currency>(parentCurrencyId);
                if (parent.Currencies != null && parent.Currencies.Count == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
        }

        public bool checkIsCurrencyTypeIdInCurrency(Session session, string currencyTypeId)
        {
            try
            {
                Currency dbcurrencyTypeId = session.FindObject<Currency>(
                    CriteriaOperator.And(
                    new BinaryOperator("CurrencyTypeId", currencyTypeId, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                    ));
                if (dbcurrencyTypeId != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
        }

        public bool checkCurrencyType_Select_IsMater(Session session, string currencyTypeId, bool isMaster)
        {
            try
            {
                CurrencyType currencyType = session.FindObject<CurrencyType>(
                    CriteriaOperator.And(
                        new BinaryOperator("CurrencyTypeId", currencyTypeId, BinaryOperatorType.Equal),
                        new BinaryOperator("IsMaster", isMaster, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                    ));
                if (currencyType != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
        }

        public bool check_ExchangeRate_BankId(Session session, string NumeratorCurrencyId, string DenomiratorCurrencyId, string BankId, short RowStatus)
        {
            try
            {
                ExchangeRate ex = session.FindObject<ExchangeRate>(
                    CriteriaOperator.And(
                        new BinaryOperator("BankId", BankId, BinaryOperatorType.Equal),
                        new BinaryOperator("DenomiratorCurrencyId", DenomiratorCurrencyId, BinaryOperatorType.Equal),
                        new BinaryOperator("NumeratorCurrencyId", NumeratorCurrencyId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (ex == null)
                    return false;
                return true;
            }
            catch (Exception) { throw; }
        }

        public bool IsUsedInExchangeRate(Session session, Guid CurrencyId)
        {
            bool result = false;
            try
            {
                Currency currency = session.GetObjectByKey<Currency>(CurrencyId);
                if (currency == null) return false;
                ExchangeRate exchangeRate = get_ExchangeRate_DenomiratorCurrencyId_IsValue(session, CurrencyId.ToString(), Constant.ROWSTATUS_ACTIVE);
                if (exchangeRate != null)
                {
                    return true;
                }

                exchangeRate = get_ExchangeRate_NumeratorCurrencyId_IsValue(session, CurrencyId.ToString(), Constant.ROWSTATUS_ACTIVE);
                if (exchangeRate != null)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }
        #endregion

        #region change
        public bool changeIsDefaultCurrency(UnitOfWork uow)
        {
            try
            {
                XPCollection<Currency> DBcurrency = new XPCollection<Currency>(uow);
                DBcurrency.Criteria = CriteriaOperator.And(
                    //new BinaryOperator("CurrencyId!Key", currencyId, BinaryOperatorType.NotEqual),
                    new BinaryOperator("IsDefault", true, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual)
                    );
                foreach (Currency cur in DBcurrency)
                {
                    cur.IsDefault = false;
                    cur.Save();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool changeIsMasterCurrencyType(Session session)
        {
            try
            {
                XPCollection<CurrencyType> currencyType = new XPCollection<CurrencyType>(session);
                currencyType.Criteria = CriteriaOperator.And(
                    //new BinaryOperator("CurrencyTypeId!Key", currencyTypeId, BinaryOperatorType.NotEqual),
                    new BinaryOperator("IsMaster", true, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual)
                    );
                foreach (CurrencyType ct in currencyType)
                {
                    ct.IsMaster = false;
                    ct.Save();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void changeCoefficientCurrency(UnitOfWork uow)
        {
            try
            {
                XPCollection<Currency> tmlst = new XPCollection<Currency>(uow, new BinaryOperator("RowStatus", 0, BinaryOperatorType.GreaterOrEqual));
                if (tmlst == null)
                {
                    throw new Exception("Currency is not exist in System");
                }
                foreach (Currency c in tmlst)
                {
                    c.Coefficient = 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region he so quy doi
        public void updateIsDefaultCurrency(UnitOfWork uow, Guid currentypeId, Guid currencyId, bool IsMaster)
        {
            try
            {
                CurrencyType ct = uow.GetObjectByKey<CurrencyType>(currentypeId);
                Currency cur = uow.GetObjectByKey<Currency>(currencyId);

                if (ct == null)
                    throw new Exception(String.Format("The CurrencyType is not exist in system"));
                if (cur == null)
                    throw new Exception(String.Format("The Currency is not exist in system"));

                XPCollection<Currency> tmlst = new XPCollection<Currency>(uow,
                    CriteriaOperator.And(
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.GreaterOrEqual),
                        new BinaryOperator("CurrencyTypeId!Key", currentypeId, BinaryOperatorType.Equal)));

                foreach (Currency c in tmlst)
                {
                    if (IsMaster)
                    {
                        if (c.CurrencyId.Equals(currencyId))
                        {
                            c.Coefficient = 1;
                            c.IsDefault = true;
                        }
                        else
                            c.IsDefault = false;
                    }
                    //else
                    //{
                    //    c.Coefficient = 0;
                    //}

                    c.Save();
                }

                DrillDownUpdateAlCoefficientOfCurrencyType(uow, currentypeId, currencyId, IsMaster);

                DrillUpUpdateAllCoeffcientOfCurrencyType(uow, currentypeId, currencyId, IsMaster);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DrillDownUpdateAlCoefficientOfCurrencyType(UnitOfWork uow, Guid currencyTypeId, Guid currencyId, bool IsMaster)
        {
            try
            {
                Currency currency = uow.GetObjectByKey<Currency>(currencyId);
                CurrencyType currencytype = uow.GetObjectByKey<CurrencyType>(currencyTypeId);

                if (currency == null)
                    throw new Exception(String.Format("The Currency is not exist in system"));
                if (currencytype == null)
                    throw new Exception(String.Format("The CurrencyType is not exist in system"));

                XPCollection<Currency> childrentCurrency = new XPCollection<Currency>(uow, new BinaryOperator("ParentCurrencyId!Key", currency.CurrencyId, BinaryOperatorType.Equal));

                foreach (Currency c in childrentCurrency)
                {
                    if (IsMaster)
                    {
                        c.Coefficient = currency.Coefficient * c.NumRequired;
                    }
                    //else
                    //{
                    //    c.Coefficient = 0;
                    //}
                    c.Save();
                    uow.FlushChanges();
                    DrillDownUpdateAlCoefficientOfCurrencyType(uow, currencyTypeId, c.CurrencyId, IsMaster);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DrillUpUpdateAllCoeffcientOfCurrencyType(UnitOfWork uow, Guid currencyTypeId, Guid currencyId, bool IsMaster)
        {
            try
            {
                Currency currency = uow.GetObjectByKey<Currency>(currencyId);
                CurrencyType currencytype = uow.GetObjectByKey<CurrencyType>(currencyTypeId);

                if (currency == null)
                    throw new Exception(String.Format("The Currency is not exist in system"));
                if (currencytype == null)
                    throw new Exception(String.Format("The CurrencyType is not exist in system"));

                Currency parent = currency.ParentCurrencyId;

                if (parent == null)
                    return;

                if (IsMaster)
                {
                    parent.Coefficient = currency.Coefficient / currency.NumRequired;
                }
                //else
                //{
                //    parent.Coefficient = 0;
                //}
                parent.Save();
                DrillUpUpdateAllCoeffcientOfCurrencyType(uow, currencyTypeId, parent.CurrencyId, IsMaster);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public static Currency DefaultCurrency(Session session)
        {
            CriteriaOperator criteria = new BinaryOperator("Code", "NAAN_DEFAULT", BinaryOperatorType.Equal);
            Currency result = session.FindObject<Currency>(criteria);
            return result;
        }

        #region get_value
        public Currency get_CurrencyID(Session session, string CurrencyId, short RowStatus)
        {
            try
            {
                Currency currencyId = session.FindObject<Currency>(
                    CriteriaOperator.And(
                        new BinaryOperator("CurrencyId", CurrencyId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                ));
                //if (currencyId == null)
                //    throw new Exception("Currency is not exist system");
                return currencyId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Currency get_CurrencyId_currencyId(Session session, string CurrencyId, short RowStatus)
        {
            Currency currencyID = session.FindObject<Currency>(
                CriteriaOperator.And(
                    new BinaryOperator("CurrencyId", CurrencyId, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                ));
            if (currencyID == null)
                throw new Exception("Currency is not exist system");
            return currencyID;
        }

        public Currency get_Currecncy_IsMaster_CurrencyType_IsDefault(Session session, bool IsMaster, bool IsDefault)
        {
            CurrencyType currencyTypeId = get_CurrencyType_true(session, IsMaster, Utility.Constant.ROWSTATUS_ACTIVE);
            Currency currencyID = session.FindObject<Currency>(
                CriteriaOperator.And(
                new BinaryOperator("CurrencyTypeId", currencyTypeId, BinaryOperatorType.Equal),
                new BinaryOperator("IsDefault", IsDefault, BinaryOperatorType.Equal),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
            ));
            if (currencyID == null)
                return null;
            //throw new Exception("Currency is not exist system");
            return currencyID;
        }

        public Currency get_Currency_true_master(Session session, string CurrencyTypeId, bool IsDefault, short RowStatus)
        {
            Currency currencyID = session.FindObject<Currency>(
                CriteriaOperator.And(
                    new BinaryOperator("CurrencyTypeId", CurrencyTypeId, BinaryOperatorType.Equal),
                    new BinaryOperator("IsDefault", IsDefault, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                ));
            if (currencyID == null)
            {
                return null;
            }
            //throw new Exception("Currency is not exist system");
            return currencyID;
        }

        public CurrencyType get_CurrencyType_true(Session session, bool IsMaster, short RowStatus)
        {
            CurrencyType currencytype_true = session.FindObject<CurrencyType>(
                    CriteriaOperator.And(
                    new BinaryOperator("IsMaster", true, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                    ));
            if (currencytype_true == null)
                throw new Exception("CurrencyType is not exist system");
            return currencytype_true;
        }

        public CurrencyType get_CurrencyTypeId(Session session, string CurrencyTypeId, short RowStatus)
        {
            CurrencyType currencyTypeId = session.FindObject<CurrencyType>(
                CriteriaOperator.And(
                    new BinaryOperator("CurrencyTypeId", CurrencyTypeId, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                ));
            //if (currencyTypeId == null)
            //    throw new Exception("CurrencyType is not exsist system");
            return currencyTypeId;
        }

        public CurrencyType get_currencyTypeId(Session session, string CurrencyTypeId, bool IsMaster, short RowStatus)
        {
            try
            {
                CurrencyType currencyType_ID = session.FindObject<CurrencyType>(
                    CriteriaOperator.And(
                        new BinaryOperator("CurrencyTypeId", CurrencyTypeId, BinaryOperatorType.Equal),
                        new BinaryOperator("IsMaster", IsMaster, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (currencyType_ID != null)
                    return currencyType_ID;
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ExchangeRate get_ExchangeRate_DenomiratorCurrencyId_IsValue(Session session, string DenomiratorCurrencyId, short RowStatus)
        {
            try
            {
                ExchangeRate ex = session.FindObject<ExchangeRate>(
                    CriteriaOperator.And(
                        new BinaryOperator("DenomiratorCurrencyId", DenomiratorCurrencyId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (ex == null)
                    return null;
                return ex;
            }
            catch (Exception) { throw; }
        }

        public ExchangeRate get_ExchangeRate_NumeratorCurrencyId_IsValue(Session session, string NumeratorCurrencyId, short RowStatus)
        {
            try
            {
                ExchangeRate ex = session.FindObject<ExchangeRate>(
                    CriteriaOperator.And(
                        new BinaryOperator("NumeratorCurrencyId", NumeratorCurrencyId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (ex == null)
                    return null;
                return ex;
            }
            catch (Exception) { throw; }
        }

        public ExchangeRate get_ExchangeRate(Session session, string ExchangeRateId, short RowStatus)
        {
            ExchangeRate exchangeRateID = session.FindObject<ExchangeRate>(
                CriteriaOperator.And(
                    new BinaryOperator("ExchangeRateId", ExchangeRateId, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                ));
            if (exchangeRateID == null)
                throw new Exception("ExchangeRate is not exist system");
            return exchangeRateID;
        }

        public Bank get_Bank_Id(Session session, string Code)
        {
            try
            {
                Bank bankId = session.FindObject<Bank>(new BinaryOperator("Code", Code, BinaryOperatorType.Equal));
                if (bankId == null)
                    throw new Exception("Bank is not exist system");
                return bankId;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<Currency> get_Collection_Currency_Id(Session session, string CurrencyType_Id, short RowStatus)
        {
            try
            {
                XPCollection<Currency> currencyid = new XPCollection<Currency>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("CurrencyTypeId", CurrencyType_Id, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (currencyid != null)
                    return currencyid;
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get value chung
        public NAS.DAL.Accounting.Currency.Currency GetDefaultCurrency(DevExpress.Xpo.Session session)
        {
            try
            {
                CurrencyType currencyType_Id_true = session.FindObject<CurrencyType>(
                    CriteriaOperator.And(
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                        new BinaryOperator("IsMaster", true, BinaryOperatorType.Equal)
                    ));
                Currency currency_Id_true = session.FindObject<Currency>(
                    CriteriaOperator.And(
                        new BinaryOperator("CurrencyTypeId", currencyType_Id_true.CurrencyTypeId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                        new BinaryOperator("IsDefault", true, BinaryOperatorType.Equal)
                    ));
                if (currency_Id_true != null)
                    return currency_Id_true;
                return null;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public NAS.DAL.Accounting.Currency.CurrencyType GetDefaultCurrencyType(DevExpress.Xpo.Session session)
        {
            try
            {
                CurrencyType currencyType_Id_true = session.FindObject<CurrencyType>(
                    CriteriaOperator.And(
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                        new BinaryOperator("IsMaster", true, BinaryOperatorType.Equal)
                    ));
                if (currencyType_Id_true != null)
                    return currencyType_Id_true;
                return null;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public string NumberToString(Session session, double Debit_Credit, string CurencyId, double Rate)
        {
            try
            {
                string a = "", rate = "", t = "";

                Currency currency_Id = get_CurrencyID(session, CurencyId.ToString(), Utility.Constant.ROWSTATUS_ACTIVE);
                Currency currency_Id_true = get_Currecncy_IsMaster_CurrencyType_IsDefault(session, true, true);
                if (Debit_Credit != null && !Debit_Credit.Equals("") && !Debit_Credit.Equals(0))
                    if (CurencyId != null && !CurencyId.Equals(""))
                        a = "Số Tiền: " + Utility.Accounting.NumberToStringFullCurrency((float)Debit_Credit, currency_Id.Code.ToString());
                //if (Rate != null && !Rate.Equals(""))
                //    rate = "<br/> Tỷ Giá: " + Rate.ToString();
                if (Debit_Credit != null && !Debit_Credit.Equals("") && !Debit_Credit.Equals(0) && Rate != null && !Rate.Equals("") && !Rate.Equals(0))
                    if (CurencyId != null && !CurencyId.Equals(""))
                        t = "<br/> Thành tiền quy đổi: " + Utility.Accounting.NumberToStringFullCurrency((float)(Debit_Credit * Rate), currency_Id_true.Code.ToString());


                string chuyenchuoi = a + rate + t;
                return chuyenchuoi;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
