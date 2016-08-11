using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.System.ShareDim;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Accounting.GeneralLedger;

namespace NAS.BO.Accounting.Report
{
    public class BO_GeneralLedger
    {
        public BO_GeneralLedger() { }

        #region get value
        public MonthDim get_MonthDimId(Session session, string Name, short RowStatus)
        {
            try
            {
                MonthDim montdim_id = session.FindObject<MonthDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Name", Name, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (montdim_id == null)
                    return null;
                return montdim_id;
            }
            catch (Exception) { throw; }
        }

        public YearDim get_YearDimId(Session session, string Name, short RowStatus)
        {
            try
            {
                YearDim yeardim_id = session.FindObject<YearDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Name", Name, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (yeardim_id == null)
                    return null;
                return yeardim_id;
            }
            catch (Exception) { throw; }
        }

        public CurrencyDim get_CurrencyDim(Session session, string Code, short RowStatus)
        {
            try
            {
                CurrencyDim CD = session.FindObject<CurrencyDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (CD != null)
                    return null;
                return CD;
            }
            catch (Exception) { throw; }
        }

        public FinancialAccountDim get_FinancialAccountDim(Session session, string Code, short RowStatus)
        {
            try
            {
                FinancialAccountDim FAD = session.FindObject<FinancialAccountDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FAD == null)
                    return null;
                return FAD;
            }
            catch (Exception) { throw; }
        }

        public CorrespondFinancialAccountDim get_CorrespondFinancialAccountDim(Session session, string Code, short RowStatus)
        {
            try
            {
                CorrespondFinancialAccountDim CFAD = session.FindObject<CorrespondFinancialAccountDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (CFAD == null)
                    return null;
                return CFAD;
            }
            catch (Exception) { throw; }
        }

        public CorrespondFinancialAccountDim get_CorrespondFinancialAccountDim_1(Session session, int CorrespondFinancialAccountDimId, short RowStatus)
        {
            try
            {
                CorrespondFinancialAccountDim CFAD = session.FindObject<CorrespondFinancialAccountDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (CFAD == null)
                    return null;
                return CFAD;
            }
            catch (Exception) { throw; }
        }

        public FinancialGeneralLedgerByYear_Fact get_FinancialGeneralLedgerByYear_Fact(Session session, int FinancialGeneralLedgerByYear_FactId, short RowStatus)
        {
            try
            {
                FinancialGeneralLedgerByYear_Fact FGLY = session.FindObject<FinancialGeneralLedgerByYear_Fact>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialGeneralLedgerByYear_FactId", FinancialGeneralLedgerByYear_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FGLY == null)
                    return null;
                return FGLY;
            }
            catch (Exception) { throw; }
        }

        public FinancialGeneralLedgerByYear_Fact get_FinancialGeneralLedgerByYear_Fact_1(Session session, int FinancialAccountDimId, int YearDimId, short RowStatus)
        {
            try
            {
                FinancialGeneralLedgerByYear_Fact FGL = session.FindObject<FinancialGeneralLedgerByYear_Fact>(
                     CriteriaOperator.And(
                         new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                         new BinaryOperator("YearDimId", YearDimId, BinaryOperatorType.Equal),
                         new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                     ));
                if (FGL == null)
                    return null;
                return FGL;
            }
            catch (Exception) { throw; }
        }

        public FinancialGeneralLedgerByMonth get_FinancialGeneralLedgerByMonth(Session session, int FinancialGeneralLedgerByMonthId, short RowStatus)
        {
            try
            {
                FinancialGeneralLedgerByMonth FGLM = session.FindObject<FinancialGeneralLedgerByMonth>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialGeneralLedgerByMonthId", FinancialGeneralLedgerByMonthId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FGLM == null)
                    return null;
                return FGLM;
            }
            catch (Exception) { throw; }
        }

        public FinancialGeneralLedgerByMonth get_FinancialGeneralLedgerByMonth_Credit(Session session, int FinancialGeneralLedgerByYear_FactId, int CorrespondFinancialAccountDimId, int MonthDimId, short RowStatus)
        {
            try
            {
                FinancialGeneralLedgerByMonth FGLM = session.FindObject<FinancialGeneralLedgerByMonth>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialGeneralLedgerByYear_FactId", FinancialGeneralLedgerByYear_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal),
                        new BinaryOperator("CreditSum", 0, BinaryOperatorType.Greater)
                    ));
                if (FGLM == null)
                    return null;
                return FGLM;
            }
            catch (Exception) { throw; }
        }

        public FinancialGeneralLedgerByMonth get_FinancialGeneralLedgerByMonth_Debit(Session session, int FinancialGeneralLedgerByYear_FactId, int CorrespondFinancialAccountDimId, int MonthDimId, short RowStatus)
        {
            try
            {
                FinancialGeneralLedgerByMonth FGLM = session.FindObject<FinancialGeneralLedgerByMonth>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialGeneralLedgerByYear_FactId", FinancialGeneralLedgerByYear_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal),
                        new BinaryOperator("DebitSum", 0, BinaryOperatorType.Greater)
                    ));
                if (FGLM == null)
                    return null;
                return FGLM;
            }
            catch (Exception) { throw; }
        }

        public FinancialGeneralLedgerByMonth get_FinancialGeneralLedgerByMonth_2(Session session, int FinancialGeneralLedgerByYear_FactId, int CorrespondFinancialAccountDimId, short RowStatus)
        {
            try
            {
                FinancialGeneralLedgerByMonth FGLM = session.FindObject<FinancialGeneralLedgerByMonth>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialGeneralLedgerByYear_FactId", FinancialGeneralLedgerByYear_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FGLM == null)
                    return null;
                return FGLM;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialGeneralLedgerByYear_Fact> get_xp_FinancialGeneralLedgerByYear_Fact(Session session, int FinancialAccountDimId, int YearDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialGeneralLedgerByYear_Fact> FGL = new XPCollection<FinancialGeneralLedgerByYear_Fact>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("YearDimId", YearDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FGL == null || FGL.Count == 0)
                    return null;
                return FGL;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialGeneralLedgerByMonth> get_xp_FinancialGeneralLedgerByMonth(Session session, int FinancialGeneralLedgerByYear_FactId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialGeneralLedgerByMonth> FGLM = new XPCollection<FinancialGeneralLedgerByMonth>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialGeneralLedgerByYear_FactId", FinancialGeneralLedgerByYear_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FGLM == null || FGLM.Count == 0)
                    return null;
                return FGLM;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialGeneralLedgerByMonth> get_xp_FinancialGeneralLedgerByMonth_1(Session session, int CorrespondFinancialAccountDimId, int FinancialGeneralLedgerByYear_FactId, int MonthDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialGeneralLedgerByMonth> FGLM = new XPCollection<FinancialGeneralLedgerByMonth>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("FinancialGeneralLedgerByYear_FactId", FinancialGeneralLedgerByYear_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FGLM == null || FGLM.Count == 0)
                    return null;
                return FGLM;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
