using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.System.ShareDim;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting.Finance.OnTheWayBuyingGood;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.BO.Accounting.Report
{
    public class BO_S04a6_dn
    {
        public BO_S04a6_dn() { }

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
            }catch(Exception){throw;}
        }

        public FinancialOnTheWayBuyingGoodSummary get_FinancialOntheWayBuyingGoodSummary(Session session, Guid FinancialOnTheWayBuyingGoodSummaryId, short RowStatus)
        {
            try
            {
                FinancialOnTheWayBuyingGoodSummary fot = session.FindObject<FinancialOnTheWayBuyingGoodSummary>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialOnTheWayBuyingGoodSummaryId", FinancialOnTheWayBuyingGoodSummaryId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (fot == null)
                    return null;
                return fot;
            }
            catch (Exception) { throw; }
        }

        public CorrespondFinancialAccountDim get_CorrespondFinancialAccountDim(Session session, int CorrespondFinancialAccountDimId, short RowStatus)
        {
            try
            {
                CorrespondFinancialAccountDim cfad = session.FindObject<CorrespondFinancialAccountDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (cfad == null)
                    return null;
                return cfad;
            }
            catch (Exception) { throw; }
        }

        public CorrespondFinancialAccountDim get_CorrespondFinancialAccountDim1(Session session, string Code, short RowStatus)
        {
            try
            {
                CorrespondFinancialAccountDim cfad = session.FindObject<CorrespondFinancialAccountDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (cfad == null)
                    return null;
                return cfad;
            }
            catch (Exception) { throw; }
        }

        public OnTheWayBuyingGoodArtifact get_OnTheWayBuyingGoodArtifact(Session session, Guid OnTheWayBuyingGoodArtifactId, short RowStatus)
        {
            try
            {
                OnTheWayBuyingGoodArtifact OTWBGA = session.FindObject<OnTheWayBuyingGoodArtifact>(
                    CriteriaOperator.And(
                        new BinaryOperator("OnTheWayBuyingGoodArtifactId", OnTheWayBuyingGoodArtifactId, BinaryOperatorType.Equal ),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (OTWBGA == null)
                    return null;
                return OTWBGA;
            }
            catch (Exception) { throw; }
        }

        public FinancialOnTheWayBuyingGoodDetail get_FinancialOnTheWayBuyingGoodDetail(Session session, Guid FinancialOnTheWayBuyingGoodDetailId, short RowStatus)
        {
            try
            {
                FinancialOnTheWayBuyingGoodDetail FOTWBGD = session.FindObject<FinancialOnTheWayBuyingGoodDetail>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialOnTheWayBuyingGoodDetailId", FinancialOnTheWayBuyingGoodDetailId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FOTWBGD == null)
                    return null;
                return FOTWBGD;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<OnTheWayBuyingGoodArtifact> get_xp_OnTheWayBuyingGoodArtifact(Session session, Guid FinancialOnTheWayBuyingGoodSummaryId, short RowStatus)
        {
            try
            {
                XPCollection<OnTheWayBuyingGoodArtifact> OTWB = new XPCollection<OnTheWayBuyingGoodArtifact>(session, 
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialOnTheWayBuyingGoodSummaryId", FinancialOnTheWayBuyingGoodSummaryId,BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (OTWB == null)
                    return null;
                return OTWB;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<OnTheWayBuyingGoodArtifact> get_xp_OnTheWayBuyingGoodArtifact_1(Session session, Guid OnTheWayBuyingGoodArtifactId, short RowStatus)
        {
            try
            {
                XPCollection<OnTheWayBuyingGoodArtifact> OTWB = new XPCollection<OnTheWayBuyingGoodArtifact>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("OnTheWayBuyingGoodArtifactId", OnTheWayBuyingGoodArtifactId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (OTWB == null)
                    return null;
                return OTWB;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialOnTheWayBuyingGoodSummary> get_xp_FinancialOnTheWayBuyingGoodSummary(Session session, int FinancialAccountDimId, int OwnerOrgDimId, int MonthDimId, int YearDimId)
        {
            try
            {
                XPCollection<FinancialOnTheWayBuyingGoodSummary> FOTWBGS = new XPCollection<FinancialOnTheWayBuyingGoodSummary>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("OwnerOrgDimId", OwnerOrgDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("YearDimId", YearDimId, BinaryOperatorType.Equal)
                    ));
                if (FOTWBGS == null)
                    return null;
                return FOTWBGS;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialOnTheWayBuyingGoodDetail> get_xp_FinancialOnTheWayBuyingGoodDetail(Session session, int FinancialAccountDimId, Guid OnTheWayBuyingGoodArtifactId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialOnTheWayBuyingGoodDetail> FOTWBGD = new XPCollection<FinancialOnTheWayBuyingGoodDetail>(session, 
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("OnTheWayBuyingGoodArtifactId", OnTheWayBuyingGoodArtifactId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FOTWBGD == null)
                    return null;
                return FOTWBGD;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialOnTheWayBuyingGoodDetail> get_xp_FinancialOnTheWayBuyingGoodDetail1(Session session, int CorrespondFinancialAccountDimId, Guid OnTheWayBuyingGoodArtifactId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialOnTheWayBuyingGoodDetail> FOTWBGD = new XPCollection<FinancialOnTheWayBuyingGoodDetail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("OnTheWayBuyingGoodArtifactId", OnTheWayBuyingGoodArtifactId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FOTWBGD == null)
                    return null;
                return FOTWBGD;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
