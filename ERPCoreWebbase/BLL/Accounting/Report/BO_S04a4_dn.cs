using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Actor;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using NAS.DAL.BI.Accounting;

namespace NAS.BO.Accounting.Report
{
    public class BO_S04a4_dn
    {
        public BO_S04a4_dn() { }

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

        public OwnerOrgDim get_OwnerOrgDimId(Session session, string Code, short RowStatus)
        {
            try
            {
                OwnerOrgDim OOD_id = session.FindObject<OwnerOrgDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (OOD_id == null)
                    return null;
                return OOD_id;
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

        public FinancialAccountDim get_FinancialAccountDim_1(Session session, int FinancialAccountDimId, short RowStatus)
        {
            try
            {
                FinancialAccountDim FAD = session.FindObject<FinancialAccountDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
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

        public FinancialTransactionDim get_FinancialTransactionDim(Session session, Guid FinancialTransactionDimId, short RowStatus)
        {
            try
            {
                FinancialTransactionDim FTD = session.FindObject<FinancialTransactionDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialTransactionDimId", FinancialTransactionDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FTD == null)
                    return null;
                return FTD;
            }
            catch (Exception) { throw; }
        }

        public FinancialTransactionDim get_FinancialTransactionDim_1(Session session, string Name, short RowStatus)
        {
            try
            {
                FinancialTransactionDim FTD = session.FindObject<FinancialTransactionDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Name", Name , BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FTD == null)
                    return null;
                return FTD;
            }
            catch (Exception) { throw; }
        }

        public DiaryJournal_Detail get_DiaryJournal_Detail(Session session, int DiaryJournal_DetailId)
        {
            try
            {
                DiaryJournal_Detail DJD = session.FindObject<DiaryJournal_Detail>(
                    CriteriaOperator.And(
                        new BinaryOperator("DiaryJournal_DetailId", DiaryJournal_DetailId, BinaryOperatorType.Equal)
                    ));
                if (DJD == null)
                    return null;
                return DJD;
            }
            catch (Exception) { throw; }
        }

        public DiaryJournal_Fact get_DiaryJournal_Fact(Session session, int DiaryJournal_FactId)
        {
            try
            {
                DiaryJournal_Fact DJF = session.FindObject<DiaryJournal_Fact>(
                    CriteriaOperator.And(
                        new BinaryOperator("DiaryJournal_FactId", DiaryJournal_FactId, BinaryOperatorType.Equal)
                    ));
                if (DJF == null)
                    return null;
                return DJF;
            }
            catch (Exception) { throw; }
        }

        public DiaryJournal_Fact get_DiaryJournal_Fact_1(Session session, int FinancialAccountDimId, int MonthDimId, int YearDimId) //, int OwnerOrgDimId
        {
            try
            {
                DiaryJournal_Fact DJF = session.FindObject<DiaryJournal_Fact>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                       // new BinaryOperator("OwnerOrgDimId", OwnerOrgDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("YearDimId", YearDimId, BinaryOperatorType.Equal)
                    ));
                if (DJF == null)
                    return null;
                return DJF;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<DiaryJournal_Fact> get_xp_DiaryJournal_Fact(Session session, int FinancialAccountDimId, int MonthDimId, int YearDimId) //, int OwnerOrgDimId
        {
            try
            {
                XPCollection<DiaryJournal_Fact> DJF = new XPCollection<DiaryJournal_Fact>(session,
                    CriteriaOperator.And(
                    new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                    //new BinaryOperator("OwnerOrgDimId", OwnerOrgDimId, BinaryOperatorType.Equal),
                    new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                    new BinaryOperator("YearDimId", YearDimId, BinaryOperatorType.Equal)
                    ));
                if (DJF == null || DJF.Count == 0)
                    return null;
                return DJF;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<DiaryJournal_Detail> get_Xp_DiaryJournal_Detail(Session session, int FinancialAccountDimId, int DiaryJournal_FactId)
        {
            try
            {
                XPCollection<DiaryJournal_Detail> DJD = new XPCollection<DiaryJournal_Detail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("DiaryJournal_FactId", DiaryJournal_FactId, BinaryOperatorType.Equal)
                    ));
                if (DJD == null || DJD.Count == 0)
                    return null;
                return DJD;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<DiaryJournal_Detail> get_Xp_DiaryJournal_Detail_1(Session session, int CorrespondFinancialAccountDimId, int DiaryJournal_FactId)
        {
            try
            {
                XPCollection<DiaryJournal_Detail> DJD = new XPCollection<DiaryJournal_Detail>( session, 
                    CriteriaOperator.And(
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("DiaryJournal_FactId", DiaryJournal_FactId, BinaryOperatorType.Equal)
                    ));
                if (DJD == null || DJD.Count == 0)
                    return null;
                return DJD;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<DiaryJournal_Detail> get_xp_DiaryJournal_Detail_2(Session session, int DiaryJournal_FactId)
        {
            try
            {
                XPCollection<DiaryJournal_Detail> DJD = new XPCollection<DiaryJournal_Detail>(session, new BinaryOperator("DiaryJournal_FactId", DiaryJournal_FactId, BinaryOperatorType.Equal));
                if (DJD == null || DJD.Count == 0)
                    return null;
                return DJD;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<DiaryJournal_Detail> get_Xp_DiaryJournal_Detail_3(Session session, int FinancialTransactionDimId, int DiaryJournal_FactId)
        {
            try
            {
                XPCollection<DiaryJournal_Detail> DJD = new XPCollection<DiaryJournal_Detail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialTransactionDimId", FinancialTransactionDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("DiaryJournal_FactId", DiaryJournal_FactId, BinaryOperatorType.Equal)
                    ));
                if (DJD == null || DJD.Count == 0)
                    return null;
                return DJD;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<DiaryJournal_Detail> get_xp_DiaryJournal_Detail_4(Session session, int FinancialTransactionDimId, int DiaryJournal_FactId)
        {
            try
            {
                XPCollection<DiaryJournal_Detail> DJD = new XPCollection<DiaryJournal_Detail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("DiaryJournal_FactId", DiaryJournal_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("FinancialTransactionDimId", FinancialTransactionDimId, BinaryOperatorType.Equal)
                    ));
                if (DJD == null || DJD.Count == 0)
                    return null;
                return DJD;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<DiaryJournal_Detail> get_Xp_DiaryJournal_Detail_5(Session session, int FinancialAccountDimId, int FinancialTransactionDimId, int DiaryJournal_FactId)
        {
            try
            {
                XPCollection<DiaryJournal_Detail> DJD = new XPCollection<DiaryJournal_Detail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("DiaryJournal_FactId", DiaryJournal_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("FinancialTransactionDimId", FinancialTransactionDimId, BinaryOperatorType.Equal)
                    ));
                if (DJD == null || DJD.Count == 0)
                    return null;
                return DJD;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<DiaryJournal_Detail> get_Xp_DiaryJournal_Detail_6(Session session, int CorrespondFinancialAccountDimId, int FinancialTransactionDimId, int DiaryJournal_FactId)
        {
            try
            {
                XPCollection<DiaryJournal_Detail> DJD = new XPCollection<DiaryJournal_Detail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("DiaryJournal_FactId", DiaryJournal_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("FinancialTransactionDimId", FinancialTransactionDimId, BinaryOperatorType.Equal)
                    ));
                if (DJD == null || DJD.Count == 0)
                    return null;
                return DJD;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<DiaryJournal_Detail> get_Xp_DiaryJournal_Detail_7(Session session, int CorrespondFinancialAccountDimId, int DiaryJournal_FactId)
        {
            try
            {
                XPCollection<DiaryJournal_Detail> DJD = new XPCollection<DiaryJournal_Detail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("DiaryJournal_FactId", DiaryJournal_FactId, BinaryOperatorType.Equal)
                    ));
                if (DJD == null || DJD.Count == 0)
                    return null;
                return DJD;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
