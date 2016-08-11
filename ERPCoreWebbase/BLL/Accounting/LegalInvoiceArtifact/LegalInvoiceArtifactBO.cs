using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using NAS.DAL.Accounting.LegalInvoice;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Nomenclature.Organization;

namespace NAS.BO.Accounting.LegalInvoiceArtifact
{
    public class LegalInvoiceArtifactBO
    {
        #region get value
        public LegalInvoiceArtifactIdentifierType get_IdentifierTypeId(Session session, string Name)
        {
            try
            {
                LegalInvoiceArtifactIdentifierType id_indentifier = session.FindObject<LegalInvoiceArtifactIdentifierType>(
                    CriteriaOperator.And(
                        new BinaryOperator("Name", Name, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                    ));
                if (id_indentifier == null)
                    return null;
                return id_indentifier;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LegalInvoiceArtifactOrgActor get_OrgActorId(Session session, Guid LegalInvoiceArtifactId, string orgType, string RowStatus)
        {
            try
            {
                LegalInvoiceArtifactOrgActor orgActor = session.FindObject<LegalInvoiceArtifactOrgActor>(
                       CriteriaOperator.And(
                       new BinaryOperator("LegalInvoiceArtifactId", LegalInvoiceArtifactId, BinaryOperatorType.Equal),
                       new BinaryOperator("OrgActorType", orgType, BinaryOperatorType.Equal),
                       new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                       ));
                if (orgActor == null)
                    return null;
                return orgActor;
            }
            catch (Exception) { throw; }
        }

        public Organization get_OrganizationId(Session session, string code)
        {
            try
            {
                Organization organization = session.FindObject<Organization>(
                    CriteriaOperator.And(
                         new BinaryOperator("Code", code, BinaryOperatorType.Equal),
                         new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                    ));
                if (organization == null)
                    return null;
                return organization;
            }
            catch (Exception) { throw; }
        }

        public NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifact get_LegalInvoiceArtifactId(Session session, Guid id, string RowStatus)
        {
            try
            {
                NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifact legalInvoiceArtifactId = session.FindObject<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifact>(
                   CriteriaOperator.And(
                     new BinaryOperator("LegalInvoiceArtifactId", id, BinaryOperatorType.Equal),
                     new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                   ));

                if (legalInvoiceArtifactId == null)
                    return null;
                return legalInvoiceArtifactId;
            }
            catch (Exception) { throw; }
        }

        public LegalInvoiceArtifactDetail get_LegalInvoiceArtifactDetailId(Session session, Guid Id, string RowStatus)
        {
            try
            {
                LegalInvoiceArtifactDetail detail = session.FindObject<LegalInvoiceArtifactDetail>(
                    CriteriaOperator.And(
                    new BinaryOperator("LegalInvoiceArtifactId", Id, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (detail == null)
                    return null;
                return detail;
            }
            catch (Exception) { throw; }
        }

        public LegalInvoiceArtifactIdentifier get_LegalInvoiceArtifactIdentifier(Session session, Guid Id_artifact, Guid Id_IdentifierType, string RowStatus)
        {
            try
            {
                LegalInvoiceArtifactIdentifier IdentifierId = session.FindObject<LegalInvoiceArtifactIdentifier>(
                    CriteriaOperator.And(
                        new BinaryOperator("LegalInvoiceArtifactId", Id_artifact, BinaryOperatorType.Equal),
                        new BinaryOperator("LegalInvoiceArtifactIdentifierTypeId", Id_IdentifierType, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (IdentifierId == null)
                    return null;
                return IdentifierId;
            }
            catch (Exception) { throw; }
        }

        public LegalInvoiceArtifactType get_LegalInvoiceArtifactTypeId(Session session, string Code, string RowStatus)
        {
            try
            {
                LegalInvoiceArtifactType artifactTypeid = session.FindObject<LegalInvoiceArtifactType>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (artifactTypeid == null)
                    return null;
                return artifactTypeid;
            }
            catch (Exception) { throw; }
        }

        public Item get_ItemId(Session session, string ItemId, string RowStatus)
        {
            try
            {
                Item Item_Id = session.FindObject<Item>(
                    CriteriaOperator.And(
                        new BinaryOperator("ItemId", ItemId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (ItemId == null)
                    return null;
                return Item_Id;
            }
            catch (Exception) { throw; }
        }

        public ItemUnit get_ItemUnit_UnitId(Session session, Guid ItemId, short RowStatus)
        {
            try
            {

                ItemUnit ItemUnitId = session.FindObject<ItemUnit>(
                    CriteriaOperator.And(
                        new BinaryOperator("ItemId", ItemId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (ItemUnitId == null)
                    return null;
                return ItemUnitId;
            }
            catch (Exception) { throw; }
        }

        public XPCollection<ItemUnit> get_xp_ItemUnit(Session session, Guid UnitId, short RowStatus)
        {
            try
            {
                XPCollection<ItemUnit> itemUnit = new XPCollection<ItemUnit>(session,
                    CriteriaOperator.And(
                        new BinaryOperator("UnitId", UnitId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus,BinaryOperatorType.Equal)
                    ));
                if (itemUnit == null)
                    return null;
                return itemUnit;
            }
            catch (Exception) { throw; }
        }

        public Unit get_UnitId(Session session, string UnitId, string RowStatus)
        {
            try
            {
                Unit unitid = session.FindObject<Unit>(
                    CriteriaOperator.And(
                        new BinaryOperator("UnitId", UnitId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (unitid == null)
                    return null;
                return unitid;
            }
            catch (Exception) { throw; }
        }

        public Unit get_UnitId_1(Session session, Guid ItemUnitId, short RowStatus){
            try{
                Unit unit = session.FindObject<Unit>(
                    CriteriaOperator.And(
                        new BinaryOperator("ItemUnitId", ItemUnitId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (unit == null)
                    return null;
                return unit;
            }catch(Exception){throw;}
        }

        #endregion

        #region check
        public bool check_LegalInvoiceArtifacType(Session session, string Code, string RowStatus)
        {
            try
            {
                LegalInvoiceArtifactType ArtifactType = session.FindObject<LegalInvoiceArtifactType>(
                    CriteriaOperator.And(
                        new BinaryOperator("Code", Code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", RowStatus, BinaryOperatorType.Equal)
                    ));
                if (ArtifactType == null)
                    return false;
                return true;
            }
            catch (Exception) { throw; }
        }
        #endregion 

    }
}
