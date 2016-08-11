using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.System.ShareDim;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Accounting.SupplierLiability;
using NAS.DAL.BI.Accounting.CustomerLiability;


namespace NAS.BO.Accounting.Report
{
    public class FinancialCustomerLiabilitySummary_FactBO
    {
        public FinancialCustomerLiabilitySummary_FactBO() { }

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

        public CustomerOrgDim get_CustomerOrgDimId(Session session, string Code, short RowStaus)
        {
            try
            {
                CustomerOrgDim COD_id = session.FindObject<CustomerOrgDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStaus, BinaryOperatorType.Equal)
                    ));
                if (COD_id == null)
                    return null;
                return COD_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialAccountDim get_FinancialAccountDimId(Session session, string Code, short RowStatus)
        {
            try
            {
                FinancialAccountDim FAD_Id = session.FindObject<FinancialAccountDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FAD_Id == null)
                    return null;
                return FAD_Id;
            }
            catch (Exception) { throw; }
        }

        public CorrespondFinancialAccountDim get_CorrespondFinancialAccountDimId(Session session, string Code, short RowStatus)
        {
            try
            {
                CorrespondFinancialAccountDim CFAD_id = session.FindObject<CorrespondFinancialAccountDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (CFAD_id == null)
                    return null;
                return CFAD_id;
            }
            catch (Exception) { throw; }
        }

        public CorrespondFinancialAccountDim get_CorrespondFinancialAccountDimId_1(Session session, Guid CorrespondFinancialAccountDimId, short RowStatus)
        {
            try
            {
                CorrespondFinancialAccountDim CFAD_id = session.FindObject<CorrespondFinancialAccountDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (CFAD_id == null)
                    return null;
                return CFAD_id;
            }
            catch (Exception) { throw; }
        }

        public CurrencyDim get_CurrencyDimId(Session session, string Code, short RowStatus)
        {
            try
            {
                CurrencyDim CD_id = session.FindObject<CurrencyDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if(CD_id == null)
                    return null;
                return CD_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialTransactionDim get_FinancialTransactionDimId(Session session, string Name, short RowStatus)
        {
            try
            {
                FinancialTransactionDim FTD_id = session.FindObject<FinancialTransactionDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Name", Name, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FTD_id == null)
                    return null;
                return FTD_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilitySummary_Fact get_FinancialCustomerLiabilitySummary_FactId_1(Session session, Guid FinancialCustomerLiabilitySummary_FactId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilitySummary_Fact FCLSF_id = session.FindObject<FinancialCustomerLiabilitySummary_Fact>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialCustomerLiabilitySummary_FactId", FinancialCustomerLiabilitySummary_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus",RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLSF_id == null)
                    return null;
                return FCLSF_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilitySummary_Fact get_FinancialCustomerLiabilitySummary_FactId_2(Session session, int FinancialAccountDimId, int MonthDimId, int YearDimId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilitySummary_Fact FCLSF_id = session.FindObject<FinancialCustomerLiabilitySummary_Fact>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("YearDimId", YearDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLSF_id == null)
                    return null;
                return FCLSF_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilitySummary_Fact get_FinancialCustomerLiabilitySummary_FactId_3(Session session, int FinancialAccountDimId, int OwnerOrgDimId, int MonthDimId, int YearDimId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilitySummary_Fact FCLSF_id = session.FindObject<FinancialCustomerLiabilitySummary_Fact>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("OwnerOrgDimId", OwnerOrgDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("YearDimId", YearDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLSF_id == null)
                    return null;
                return FCLSF_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilitySummary_Fact get_FinancialCustomerLiabilitySummary_FactId_4(Session session, int FinancialAccountDimId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilitySummary_Fact FCLSF_id = session.FindObject<FinancialCustomerLiabilitySummary_Fact>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLSF_id == null)
                    return null;
                return FCLSF_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilitySummary_Fact get_FinancialCustomerLiabilitySummary_FactId_5(Session session, Guid CustomerOrgDim, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilitySummary_Fact FCLSF_id = session.FindObject<FinancialCustomerLiabilitySummary_Fact>(
                    CriteriaOperator.And(
                        new BinaryOperator("CustomerOrgDim", CustomerOrgDim, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLSF_id == null)
                    return null;
                return FCLSF_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilitySummary_Fact get_FinancialCustomerLiabilitySUmmary_FactId_6(Session session, int FinacialAccountDimId, int CustomerOrgDimId, int MonthDimId, int YearDimId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilitySummary_Fact fclsf = session.FindObject<FinancialCustomerLiabilitySummary_Fact>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinacialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("CustomerOrgDimId", CustomerOrgDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("YearDimId", YearDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (fclsf == null)
                    return null;
                return fclsf;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilitySummary_Fact> get_xp_FinancialCustomerLiabilitySummary_Fact_1(Session session, Guid FinancialCustomerLiabilitySummary_FactId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilitySummary_Fact> FCLSF_id = new XPCollection<FinancialCustomerLiabilitySummary_Fact>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialCustomerLiabilitySummary_FactId", FinancialCustomerLiabilitySummary_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus",RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLSF_id == null || FCLSF_id.Count == 0)
                    return null;
                return FCLSF_id;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilitySummary_Fact> get_xp_FinancialCustomerLiabilitySummary_Fact_2(Session session, int FinancialAccountDimId, int MonthDimId, int YearDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilitySummary_Fact> FCLSF_id = new XPCollection<FinancialCustomerLiabilitySummary_Fact>(session,
                    CriteriaOperator.And(
                       new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("YearDimId", YearDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLSF_id == null || FCLSF_id.Count == 0)
                    return null;
                return FCLSF_id;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilitySummary_Fact> get_xp_FinancialCustomerLiabilitySummary_Fact_3(Session session, int FinancialAccountDimId, int OwnerOrgDimId, int MonthDimId, int YearDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilitySummary_Fact> FCLSF_id = new XPCollection<FinancialCustomerLiabilitySummary_Fact>(session,
                    CriteriaOperator.And(
                          new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("OwnerOrgDimId", OwnerOrgDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("YearDimId", YearDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLSF_id == null || FCLSF_id.Count == 0)
                    return null;
                return FCLSF_id;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilitySummary_Fact> get_xp_FinancialCustomerLiabilitySummary_Fact_4(Session session, int FinancialAccountDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilitySummary_Fact> FCLSF_id = new XPCollection<FinancialCustomerLiabilitySummary_Fact>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLSF_id == null || FCLSF_id.Count == 0)
                    return null;
                return FCLSF_id;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilitySummary_Fact> get_xp_FinancialCustomerLiabilitySummary_Fact_5(Session session, Guid CustomerOrgDim, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilitySummary_Fact> FCLSF_id = new XPCollection<FinancialCustomerLiabilitySummary_Fact>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("CustomerOrgDim", CustomerOrgDim, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLSF_id == null || FCLSF_id.Count == 0)
                    return null;
                return FCLSF_id;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilitySummary_Fact> get_xp_FinancialCustomerLiabilitySUmmary_FactId_6(Session session, int FinacialAccountDimId, int CustomerOrgDimId, int MonthDimId, int YearDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilitySummary_Fact> fclsf = new XPCollection<FinancialCustomerLiabilitySummary_Fact>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinacialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("CustomerOrgDimId", CustomerOrgDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("YearDimId", YearDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (fclsf == null)
                    return null;
                return fclsf;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilityDetail get_FinancialCustomerLiabilityDetailId_1(Session session, Guid FinancialCustomerLiabilitySummary_FactId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilityDetail FCLD_id = session.FindObject<FinancialCustomerLiabilityDetail>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialCustomerLiabilitySummary_FactId", FinancialCustomerLiabilitySummary_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLD_id == null)
                    return null;
                return FCLD_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilityDetail get_FinancialCustomerLiabilityDetailId_2(Session session, Guid FinancialCustomerLiabilitySummary_FactId, int FinancialAccountDimId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilityDetail FCLD_id = session.FindObject<FinancialCustomerLiabilityDetail>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialCustomerLiabilitySummary_FactId", FinancialCustomerLiabilitySummary_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLD_id == null)
                    return null;
                return FCLD_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilityDetail get_FinancialCustomerLiabilityDetailId_3(Session session, string CorrespondFinancialAccountDimId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilityDetail FCLD_id = session.FindObject<FinancialCustomerLiabilityDetail>(
                    CriteriaOperator.And(
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLD_id == null)
                    return null;
                return FCLD_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilityDetail get_FinancialCustomerLiabilityDetailId_4(Session session, Guid FinancialTransactionDimId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilityDetail FCLD_id = session.FindObject<FinancialCustomerLiabilityDetail>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialTransactionDimId", FinancialTransactionDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLD_id == null)
                    return null;
                return FCLD_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilityDetail get_FinancialCustomerLiabilityDetailId_5(Session session, Guid FinancialCustomerLiabilitySummary_FactId, int FinancialAccountDimId, Guid CorrespondFinancialAccountDimId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilityDetail FCLD_id = session.FindObject<FinancialCustomerLiabilityDetail>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialCustomerLiabilitySummary_FactId", FinancialCustomerLiabilitySummary_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLD_id == null)
                    return null;
                return FCLD_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilityDetail get_FinancialCustomerLiabilityDetailId_6(Session session,  int FinancialAccountDimId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilityDetail FCLD_id = session.FindObject<FinancialCustomerLiabilityDetail>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLD_id == null)
                    return null;
                return FCLD_id;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilityDetail get_FinancialCustomerLiabilityDetailId_7(Session session, Guid FinancialCustomerLiabilityDetailId, Guid CorrespondFinancialAccountDimId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilityDetail fcld = session.FindObject<FinancialCustomerLiabilityDetail>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialCustomerLiabilityDetailId", FinancialCustomerLiabilityDetailId, BinaryOperatorType.Equal),
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (fcld == null)
                    return null;
                return fcld;
            }
            catch (Exception) { throw; }
        }

        public FinancialCustomerLiabilityDetail get_FinancialCustomerLiabilityDetailId_8(Session session, Guid FinancialCustomerLiabilityDetailId, short RowStatus)
        {
            try
            {
                FinancialCustomerLiabilityDetail fcld = session.FindObject<FinancialCustomerLiabilityDetail>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialCustomerLiabilityDetailId", FinancialCustomerLiabilityDetailId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (fcld == null)
                    return null;
                return fcld;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilityDetail> get_xp_FinancialCustomerLiabilityDetailId_1(Session session, Guid FinancialCustomerLiabilitySummary_FactId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilityDetail> FCLD_id = new XPCollection<FinancialCustomerLiabilityDetail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialCustomerLiabilitySummary_FactId", FinancialCustomerLiabilitySummary_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLD_id == null || FCLD_id.Count == 0)
                    return null;
                return FCLD_id;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilityDetail> get_xp_FinancialCustomerLiabilityDetailId_2(Session session, Guid FinancialCustomerLiabilitySummary_FactId, int FinancialAccountDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilityDetail> FCLD_id = new XPCollection<FinancialCustomerLiabilityDetail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialCustomerLiabilitySummary_FactId", FinancialCustomerLiabilitySummary_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLD_id == null || FCLD_id.Count == 0)
                    return null;
                return FCLD_id;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilityDetail> get_xp_FinancialCustomerLiabilityDetailId_3(Session session, Guid CorrespondFinancialAccountDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilityDetail> FCLD_id = new XPCollection<FinancialCustomerLiabilityDetail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLD_id == null && FCLD_id.Count == 0)
                    return null;
                return FCLD_id;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilityDetail> get_xp_FinancialCustomerLiabilityDetailId_4(Session session, Guid FinancialTransactionDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilityDetail> FCLD_id = new XPCollection<FinancialCustomerLiabilityDetail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialTransactionDimId", FinancialTransactionDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLD_id == null|| FCLD_id.Count == 0)
                    return null;
                return FCLD_id;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilityDetail> get_xp_FinancialCustomerLiabilityDetailId_5(Session session, Guid FinancialCustomerLiabilitySummary_FactId, int FinancialAccountDimId, Guid CorrespondFinancialAccountDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilityDetail> FCLD_id = new XPCollection<FinancialCustomerLiabilityDetail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialCustomerLiabilitySummary_FactId", FinancialCustomerLiabilitySummary_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLD_id == null)
                    return null;
                return FCLD_id;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilityDetail> get_xp_FinancialCustomerLiabilityDetailId_6(Session session, int FinancialAccountDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilityDetail> FCLD_id = new XPCollection<FinancialCustomerLiabilityDetail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FCLD_id == null || FCLD_id.Count == 0)
                    return null;
                return FCLD_id;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilityDetail> get_xp_FinancialCustomerLiabilityDetailId_7(Session session, Guid FinancialCustomerLiabilityDetailId, Guid CorrespondFinancialAccountDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilityDetail> fcld = new  XPCollection<FinancialCustomerLiabilityDetail>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialCustomerLiabilityDetailId", FinancialCustomerLiabilityDetailId, BinaryOperatorType.Equal),
                        new BinaryOperator("CorrespondFinancialAccountDimId", CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (fcld == null)
                    return null;
                return fcld;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<FinancialCustomerLiabilityDetail> get_xp_FinancialCustomerLiabiltiyDetailId_8(Session session, Guid FinancialCustomerLiabilitySummary_FactId, int CorrespondFinancialAccountDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialCustomerLiabilityDetail> fcld = new XPCollection<FinancialCustomerLiabilityDetail>(session, 
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialCustomerLiabilitySummary_FactId" , FinancialCustomerLiabilitySummary_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("CorrespondFinancialAccountDimId",  CorrespondFinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (fcld == null)
                    return null;
                return fcld;
            }
            catch (Exception) { throw; }
        }


        #endregion
    }
}
