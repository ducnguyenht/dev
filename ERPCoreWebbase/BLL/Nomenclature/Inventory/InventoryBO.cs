using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Inventory;

namespace NAS.BO.Nomenclature.Inventory
{
    public class InventoryBO
    {
        public InventoryBO()
        {
        }
        //DND Duyet tu node con den node cha
        public void getParentInventoryTree(Session session, Guid startId, ref List<Guid> inventoryUnitIdList){
            NAS.DAL.Nomenclature.Inventory.Inventory i = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(startId);
            if (i != null && i.InventoryUnitId != null && i.InventoryUnitId.RowStatus >0)
            {
                inventoryUnitIdList.Add(i.InventoryUnitId.InventoryUnitId);
                getParentInventoryTree(session, i.ParentInventoryId.InventoryId, ref inventoryUnitIdList);
            }
        }

        //DND Duyet tu node cha xuong den node con
        public void getInventoryTree(Session session, Guid startId, ref List<Guid> inventoryUnitIdList)
        {
            NAS.DAL.Nomenclature.Inventory.Inventory root = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(startId);
            if (root != null)
            {
                foreach (NAS.DAL.Nomenclature.Inventory.Inventory i in root.Inventorys) {
                    if (i.InventoryUnitId.RowStatus > 0)
                    {
                        inventoryUnitIdList.Add(i.InventoryUnitId.InventoryUnitId);
                        getInventoryTree(session, i.InventoryId, ref inventoryUnitIdList);
                    }
                }
            }
        }

        public bool checkIsAlreadyHaveAChild(Session session, Guid parentInventoryId)
        {
            NAS.DAL.Nomenclature.Inventory.Inventory parent =  session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(parentInventoryId);
            if (parent.Inventorys != null && parent.Inventorys.Count == 1)
            {
                return true;
            }
           return false;
        }

    }
}
