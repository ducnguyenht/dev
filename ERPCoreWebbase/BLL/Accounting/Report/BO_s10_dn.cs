using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.BI.Accounting.ItemInventory;
using NAS.DAL.BI.Inventory;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Item;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Accounting;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.BO.Accounting.Report
{
    public class BO_s10_dn
    {
        public BO_s10_dn() { }

        #region get value
        public InventoryDim get_InventoryDim(Session session, string Code, short RowStatus)
        {
            try
            {
                InventoryDim IDim = session.FindObject<InventoryDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (IDim == null)
                    return null;
                return IDim;
            }
            catch (Exception) { return null; }
        }

        public InventoryDim get_InventoryDim_1(Session session, string Name, short RowStatus)
        {
            try
            {
                InventoryDim IDim = session.FindObject<InventoryDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Name", Name, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (IDim == null)
                    return null;
                return IDim;
            }
            catch (Exception) { return null; }
        }

        public OwnerOrgDim get_OwnerOrgDim(Session session, string Code, short RowStatus)
        {
            try
            {
                OwnerOrgDim OOD = session.FindObject<OwnerOrgDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (OOD == null)
                    return null;
                return OOD;
            }
            catch (Exception) { return null; }
        }

        public MonthDim get_MonthDim(Session session, string Name, short RowStatus)
        {
            try
            {
                MonthDim MD = session.FindObject<MonthDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Name", Name, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (MD == null)
                    return null;
                return MD;
            }
            catch (Exception) { return null; }
        }

        public YearDim get_YearDim(Session session, string Name, short RowStatus)
        {
            try
            {
                YearDim YD = session.FindObject<YearDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Name", Name, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (YD == null)
                    return null;
                return YD;
            }
            catch (Exception) { return null; }
        }

        public ItemDim get_ItemDim(Session session, string Code, short RowStatus)
        {
            try
            {
                ItemDim ID = session.FindObject<ItemDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (ID == null)
                    return null;
                return ID;
            }
            catch (Exception) { return null; }
        }

        public ItemDim get_ItemDim_1(Session session, string Name, short RowStatus)
        {
            try
            {
                ItemDim ID = session.FindObject<ItemDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Name", Name, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (ID == null)
                    return null;
                return ID;
            }
            catch (Exception) { return null; }
        }

        public UnitDim get_UnitDim(Session session, string Code, short RowStatus)
        {
            try
            {
                UnitDim UD = session.FindObject<UnitDim>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (UD == null)
                    return null;
                return UD;
            }
            catch (Exception) { return null; }
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
                if (CD == null) return null;
                return CD;
            }
            catch (Exception) { return null; }
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
            catch (Exception) { return null; }
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
            catch (Exception) { return null; }
        }

        public ItemInventoryByArtifact get_ItemInventoryByArtifact(Session session, Guid ItemInventoryByArtifactId, short RowStatus)
        {
            try
            {
                ItemInventoryByArtifact IIBA = session.FindObject<ItemInventoryByArtifact>(
                    CriteriaOperator.And(
                        new BinaryOperator("ItemInventoryByArtifactId", ItemInventoryByArtifactId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (IIBA == null)
                    return null;
                return IIBA;
            }
            catch (Exception) { return null; }
        }

        public FinancialItemInventorySummary_Fact get_FinancialItemInventorySummary_Fact(Session session, int FinancialItemInventorySummary_FactId, short RowStatus)
        {
            try
            {
                FinancialItemInventorySummary_Fact FIISF = session.FindObject<FinancialItemInventorySummary_Fact>(
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialItemInventorySummary_FactId", FinancialItemInventorySummary_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FIISF == null)
                    return null;
                return FIISF;
            }
            catch (Exception) { return null; }
        }
        #endregion

        #region XPCollection
        public XPCollection<FinancialItemInventorySummary_Fact> get_xp_FinancialItemInventorySummary_Fact(Session session, int FinancialAccountDimId, int InventoryDimId, int OwnerOrgDimId, int ItemDimId, int UnitDimId, int MonthDimId, int YearDimId, short RowStatus)
        {
            try
            {
                XPCollection<FinancialItemInventorySummary_Fact> FIISF = new XPCollection<FinancialItemInventorySummary_Fact>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialAccountDimId", FinancialAccountDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("InventoryDimId", InventoryDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("ItemDimId", ItemDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("OwnerOrgDimId", OwnerOrgDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("MonthDimId", MonthDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("YearDimId", YearDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("UnitDimId", UnitDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (FIISF == null || FIISF.Count == 0)
                    return null;
                return FIISF;
            }
            catch (Exception) { return null; }
        }

        public XPCollection<ItemInventoryByArtifact> get_xp_ItemInventoryByArtifact(Session session, int FinancialItemInventorySummary_FactId, short RowSataus)
        {
            try
            {
                XPCollection<ItemInventoryByArtifact> IIBA = new XPCollection<ItemInventoryByArtifact>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialItemInventorySummary_FactId", FinancialItemInventorySummary_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowSataus, BinaryOperatorType.Equal)
                    ));
                if (IIBA == null || IIBA.Count == 0)
                    return null;
                return IIBA;
            }
            catch (Exception) { return null; }
        }

        public XPCollection<ItemInventoryByArtifact> get_xp_InventoryByArtifact_1(Session session, int FinancialItemInventorySummary_FactId, int InventoryCommandDimId, short RowStatus)
        {
            try
            {
                XPCollection<ItemInventoryByArtifact> IIBA = new XPCollection<ItemInventoryByArtifact>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("FinancialItemInventorySummary_FactId", FinancialItemInventorySummary_FactId, BinaryOperatorType.Equal),
                        new BinaryOperator("InventoryCommandDimId", InventoryCommandDimId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (IIBA == null)
                    return null;
                return IIBA;
            }
            catch (Exception) { return null; }
        }
        #endregion
    }
}
