using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Inventory.Audit;
using DevExpress.Data.Filtering;

namespace NAS.BO.Inventory.Audit
{
    public class InventoryAuditArtifactBO
    {
        //InventoryAuditArtifact

        public XPCollection<InventoryAuditArtifact> getAuditingInventoryCommandAll(Session session)
        {
            return (new XPCollection<InventoryAuditArtifact>(session)); 
        }

        public XPCollection<InventoryAuditArtifact> getAuditingInventoryCommandByFilter(Session session, CriteriaOperator filter)
        {
            
            return (new XPCollection<InventoryAuditArtifact>(session, filter));
        }

        public InventoryAuditArtifact getAuditingInventoryCommandById(Session session, Guid id)
        {
            InventoryAuditArtifact _inventoryAuditArtifact = session.GetObjectByKey<InventoryAuditArtifact>(id);
            return _inventoryAuditArtifact;
        }     

        public Guid insertAuditingInventoryCommand(Session session)
        {
            Guid _id = Guid.Empty;

            InventoryAuditArtifact _inventoryAuditArtifact = new InventoryAuditArtifact(session);
            
            return _id;
        }

        public void updateAuditingInventoryCommand(Session session)
        {
        }

        public void deleteAuditingInventoryCommand(Session session)
        {
        }

        // InventoryAuditUnitItem
        public Guid insertInventoryAuditUnitItem(Session session)
        {
            Guid _id = Guid.Empty;

            InventoryAuditArtifact _inventoryAuditArtifact = new InventoryAuditArtifact(session);


            return _id;
        }

        public void updateInventoryAuditUnitItem(Session session)
        {
            
        }


        public void deleteInventoryAuditUnitItem(Session session)
        {

        }


    }
}
