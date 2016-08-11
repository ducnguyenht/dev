using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Invoice;
using NAS.DAL.Inventory.Journal;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL;
using NAS.DAL.Nomenclature.UnitItem;

namespace NAS.BO.Nomenclature.Items
{
    public class ItemBO
    {
        //public current VAT ItemTax
        public ItemTax GetCurrentVATOfItem(Session session, Guid itemId)
        {
            Item item = session.GetObjectByKey<Item>(itemId);
            //TaxType VATTaxType = session.FindObject<TaxType>(new BinaryOperator("Code", "GTGT"));
            TaxType VATTaxType = session.FindObject<TaxType>(new BinaryOperator("Code", "VAT"));
            if (item.ItemTaxes != null && item.ItemTaxes.Count > 0)
            {
                DateTime systemDate = DateTime.Now;
                ItemTax itemTax =
                    item.ItemTaxes.Where(r => r.TaxId.TaxTypeId == VATTaxType
                                        && r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE
                                        //&& r.ValidFromDate <= systemDate
                                        //&& systemDate <= r.ValidToDate
                                        ).FirstOrDefault();
                return itemTax;
            }
            else
            {
                return null;
            }
        }

        public bool checkIsDupplicateCode(Session session, string code)
        {
            try
            {
                Item item = session.FindObject<Item>(CriteriaOperator.And(
                        new BinaryOperator("Code", code, BinaryOperatorType.Equal), 
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)));
                if (item != null && !item.IsDeleted)
                    return false;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool checkConstraintAddCustomType(Session session, Guid ObjectTypeId, Guid ItemId, out string msg)
        {
            msg = "";
            try
            {
                ObjectType SERVICE = NAS.DAL.Util.getXpoObjectByName<ObjectType>(session, "SERVICE");
                ObjectType PRODUCT = NAS.DAL.Util.getXpoObjectByName<ObjectType>(session, "PRODUCT");

                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("ItemId", ItemId),
                    new BinaryOperator("ObjectTypeId", SERVICE.ObjectTypeId)
                    );

                ItemCustomType itemCustomType = session.FindObject<ItemCustomType>(criteria);

                if (itemCustomType != null && ObjectTypeId == PRODUCT.ObjectTypeId)
                {
                    msg = "Nếu loại đối tượng là 'dịch vụ' thì không thể chọn tiếp loại 'hàng hóa'";
                    return false;
                }

                criteria = CriteriaOperator.And(
                    new BinaryOperator("ItemId", ItemId),
                    new BinaryOperator("ObjectTypeId", PRODUCT.ObjectTypeId)
                    );

                itemCustomType = session.FindObject<ItemCustomType>(criteria);
                if (itemCustomType != null && ObjectTypeId == SERVICE.ObjectTypeId)
                {
                    msg = "Nếu loại đối tượng là 'hàng hóa' thì không thể chọn tiếp loại 'dịch vụ'";
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Có lỗi dữ liệu");
            }
        }

        public Item addDefaultItem(Session session, NAS.DAL.Nomenclature.Item.Item.TYPEOFITEM info)
        {
            try
            {
                session.BeginTransaction();
                //insert default data into Item table
                string name = "";
                switch (info)
                {
                    case NAS.DAL.Nomenclature.Item.Item.TYPEOFITEM.PRODUCT:
                        name = "PRODUCT";
                        break;
                    case NAS.DAL.Nomenclature.Item.Item.TYPEOFITEM.MATERIAL:
                        name = "MATERIAL";
                        break;
                    case NAS.DAL.Nomenclature.Item.Item.TYPEOFITEM.TOOL:
                        name = "TOOL";
                        break;
                    case NAS.DAL.Nomenclature.Item.Item.TYPEOFITEM.SERVICE:
                        name = "SERVICE";
                        break;
                    default:
                        break;
                }

                ObjectType objectType = NAS.DAL.Util.getXpoObjectByName<ObjectType>(session, name);

                Item item = new Item(session)
                {
                    Code = Guid.NewGuid().ToString(),
                    Description = Utility.Constant.NAAN_DEFAULT_NAME,
                    RowStatus = 0,
                    RowCreationTimeStamp = DateTime.Now
                };

                item.Save();

                XPCollection<NAS.DAL.Nomenclature.UnitItem.UnitType> unitTypes =
                        new XPCollection<NAS.DAL.Nomenclature.UnitItem.UnitType>(session,
                            CriteriaOperator.And(
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT,
                                BinaryOperatorType.Equal),
                            new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE,
                                BinaryOperatorType.NotEqual)));

                foreach (NAS.DAL.Nomenclature.UnitItem.UnitType unitType in unitTypes)
                {
                    ItemUnitTypeConfig iutc = new ItemUnitTypeConfig(session);
                    iutc.ItemId = item;
                    iutc.UnitTypeId = unitType;
                    iutc.IsMaster = false;
                    iutc.RowStatus = Utility.Constant.ROWSTATUS_DEFAULT;
                    iutc.Save();
                }

                session.CommitTransaction();
                return item;
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        public bool checkAlreadyHasObjectWithObjectType(Session session, Guid itemId, Guid objectTypeId)
        {
            try
            {
                Item it = session.GetObjectByKey<Item>(itemId);
                if (it == null)
                    throw new Exception("The key is not exist in Item");

                ObjectType ot = session.GetObjectByKey<ObjectType>(objectTypeId);
                if (ot == null)
                    throw new Exception("The key is not exist in Item");

                CriteriaOperator criteria = CriteriaOperator.And(
                        new BinaryOperator("ItemId", it),
                        new BinaryOperator("ObjectId.ObjectTypeId", ot)
                    );
                ItemObject io = session.FindObject<ItemObject>(criteria);
                if (io == null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void updateAllCommonInfoOfItem(Session session, Guid ItemId, string code, string name, 
            Guid manufacturerId, List<Guid> objectTypeId)
        {
            ObjectBO bo = new ObjectBO();
            try
            {
                session.BeginTransaction();
                Item item = session.GetObjectByKey<Item>(ItemId);
                if (item == null)
                    throw new Exception(String.Format("Không tồn tại ItemId: {0} trong Item table", ItemId));

                ManufacturerOrg manu = session.GetObjectByKey<ManufacturerOrg>(manufacturerId);
                //Issue dropdownlist ---START
                //if (manu == null)
                //    throw new Exception(String.Format("Không tồn tại ManufacturerOrgId: {0} trong Item ManufacturerOrg", manufacturerId));
                //Issue dropdownlist ---END
                item.Code = code;
                item.Name = name;
                item.ManufacturerOrgId = manu;
                item.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                item.Save();

                removeItemCustomTypeFromItem(session, ItemId);

                foreach (Guid g in objectTypeId)
                {
                    addItemCustomTypeToItem(session, g, ItemId);
                }

                foreach (Guid g in objectTypeId)
                {
                    if (!checkAlreadyHasObjectWithObjectType(session, ItemId, g))
                    {
                        NAS.DAL.CMS.ObjectDocument.Object o = bo.CreateCMSObject(session, g);
                        ItemObject it = new ItemObject(session);
                        it.ObjectId = o;
                        it.ItemId = item;
                        it.Save();
                    }
                }

                for (int i = item.ItemObjects.Count - 1; i >= 0; i--)
                {
                    ItemObject tmp = item.ItemObjects[i];
                    if (!item.ItemCustomTypes.Select(r => r.ObjectTypeId).Contains(tmp.ObjectId.ObjectTypeId))
                    {
                        bo.DeleteCMSObject(session, tmp.ObjectId.ObjectId);
                    }
                }

                //foreach (ItemObject io in item.ItemObjects)
                //{
                //    if(!item.ItemCustomTypes.Select(r => r.ObjectTypeId).Contains(io.ObjectId.ObjectTypeId)) {
                //        bo.DeleteCMSObject(session, io.ObjectId.ObjectId);
                //    }
                //}

                session.CommitTransaction();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }
        
        public void addItemCustomTypeToItem(Session session, Guid ObjectTypeId, Guid ItemId)
        {
            try
            {
                ObjectType objectType = (ObjectType)session.GetObjectByKey<ObjectType>(ObjectTypeId);

                if (objectType == null)
                    throw new Exception(String.Format("Không tồn tại ObjectTypeId: {0} trong ObjectType table", ObjectTypeId));

                Item item = (Item)session.GetObjectByKey<Item>(ItemId);

                if (item == null)
                    throw new Exception(String.Format("Không tồn tại ItemId: {0} trong Item table", ItemId));

                ItemCustomType itemCustomType = new ItemCustomType(session)
                {
                    ObjectTypeId = objectType,
                    ItemId = item
                };

                itemCustomType.Save();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        public void removeItemCustomTypeFromItem(Session session, Guid ItemId)
        {
            try
            {
                CriteriaOperator criteria =
                    new BinaryOperator("ItemId", ItemId, BinaryOperatorType.Equal);
                XPCollection<ItemCustomType> itemCustomTypes = new XPCollection<ItemCustomType>(session, criteria); 

                for(int i = itemCustomTypes.Count - 1; i >= 0; i--) {
                    ItemCustomType tmp = itemCustomTypes[i];
                    tmp.Delete();

                }
            }
            catch (Exception e)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        public bool checkIsItemInInventory(Session session, Guid ItemId)
        {
            List<Item> rs = null;
            try
            {
                XPQuery<ItemUnitRelationType> itemUnitRelationTypeQ = session.Query<ItemUnitRelationType>();
                ItemUnitRelationType unitRelationType = itemUnitRelationTypeQ.Where(r => r.Name == "UNIT").FirstOrDefault();
                XPQuery<InventoryJournal> inventoryJournalQuery = session.Query<InventoryJournal>();
                rs = (from ivj in inventoryJournalQuery
                      where ivj.InventoryId.RowStatus > 0 && ivj.ItemUnitId.ItemId.ItemId == ItemId
                      && ivj.ItemUnitId.ItemUnitRelationTypeId == unitRelationType
                      group ivj by ivj.ItemUnitId.ItemId into it
                      select it.Key).ToList();

                if (rs.Count > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }

        public bool checkIsExistInBillItem(Session session, Guid ItemId) {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And(
                         new BinaryOperator("ItemId", ItemId, BinaryOperatorType.Equal),
                         new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                    );
                XPCollection<ItemUnit> itemUnit = new XPCollection<ItemUnit>(session, criteria);

                criteria = CriteriaOperator.And(
                         new InOperator("ItemUnitId", itemUnit)
                         //new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)//,
                         //new BinaryOperator("BillId.RowStatus", 0, BinaryOperatorType.Greater)
                    );
                XPCollection<BillItem> billitem = new XPCollection<BillItem>(session, criteria);

                if (billitem == null || billitem.Count == 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public void deleteLogicItem(Session session, Guid itemId)
        {
            try
            {
               Item item = session.GetObjectByKey<Item>(itemId);
               if (item == null)
                   throw new Exception(string.Format("Not exist ItemId {0} in Item Table", itemId));
               item.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
               item.Save();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<NAS.DAL.CMS.ObjectDocument.Object> getAllCustomFieldObjects(Session session, Guid ItemId)
        {
            try
            {
                Item item = session.GetObjectByKey<Item>(ItemId);
                if (item == null)
                    throw new Exception("The key is not exist in Item"); 

                if (item.ItemObjects == null || item.ItemObjects.Count == 0)
                    throw new Exception("The item is wrong in configuration");
                return item.ItemObjects.Select(r => r.ObjectId).ToList();}
            catch (Exception)
            {
                return null;
            }
        }

        public bool updateMasterUnitTypeOfItem(Guid itemId, Guid masterUnitTypeId)
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    NAS.DAL.Nomenclature.UnitItem.UnitType ut =
                    uow.GetObjectByKey<NAS.DAL.Nomenclature.UnitItem.UnitType>(masterUnitTypeId);

                    NAS.DAL.Nomenclature.Item.Item i =
                        uow.GetObjectByKey<NAS.DAL.Nomenclature.Item.Item>(itemId);

                    if (ut == null)
                        throw new Exception("The UnitType is not exist in system");

                    if (i == null)
                        throw new Exception("The Item is not exist in system");

                    if (i.itemUnitTypeConfigs == null || i.itemUnitTypeConfigs.Count == 0)
                        throw new Exception("The UnitTypes are blank with this item");

                    IEnumerable<UnitType> tmpLst = i.itemUnitTypeConfigs.Select(o => o.UnitTypeId).Where(rs => rs.UnitTypeId == masterUnitTypeId);

                    if (tmpLst == null || tmpLst.Count() == 0)
                        throw new Exception("The MasterUnitTypeId is not in ItemUnitTypeConfig");

                    foreach (NAS.DAL.Nomenclature.Item.ItemUnitTypeConfig config in i.itemUnitTypeConfigs)
                    {
                        if (config.UnitTypeId.UnitTypeId.Equals(masterUnitTypeId))
                            config.IsMaster = true;
                        else
                            config.IsMaster = false;

                        config.Save();
                    }

                    uow.CommitChanges();
                    return true;
                }
                catch
                {
                    throw;
                }
                finally {
                    uow.Dispose();
                }
            }
        }

        public NAS.DAL.Nomenclature.Item.ItemUnitTypeConfig getItemUnitTypeConfig(Session session, Guid itemId, Guid unitTypeId)
        { 
            try
            {
                NAS.DAL.Nomenclature.UnitItem.UnitType ut =
                session.GetObjectByKey<NAS.DAL.Nomenclature.UnitItem.UnitType>(unitTypeId);

                NAS.DAL.Nomenclature.Item.Item i =
                    session.GetObjectByKey<NAS.DAL.Nomenclature.Item.Item>(itemId);

                ItemUnitTypeConfig iutc = session.FindObject<ItemUnitTypeConfig>(
                    CriteriaOperator.And(
                        new BinaryOperator("UnitTypeId", ut, BinaryOperatorType.Equal),
                        new BinaryOperator("ItemId", i, BinaryOperatorType.Equal)
                    )
                );

                return iutc;
            }
            catch
            {
                throw;
            }
        }

        public bool selectUnitTypeForItem(Guid itemUnitTypeConfigId)
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    ItemUnitTypeConfig iutc = uow.GetObjectByKey<ItemUnitTypeConfig>(itemUnitTypeConfigId);
                    iutc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    iutc.IsSelected = true;
                    iutc.Save();
                    UnitBO bo = new UnitBO();
                    bo.populateDefaultUnitToItemUnit(iutc.ItemId.ItemId, iutc.UnitTypeId.UnitTypeId);
                    uow.CommitChanges();
                    return true;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    uow.Dispose();
                }
            }
        }

        public bool unselectUnitTypeForItem(Guid itemUnitTypeConfigId)
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    ItemUnitTypeConfig iutc = uow.GetObjectByKey<ItemUnitTypeConfig>(itemUnitTypeConfigId);
                    iutc.RowStatus = Utility.Constant.ROWSTATUS_DEFAULT;
                    iutc.IsMaster = false;
                    iutc.IsSelected = false;
                    iutc.Save();

                    XPCollection<ItemUnit> itemUnits = new XPCollection<ItemUnit>(uow,
                        CriteriaOperator.And(
                            new BinaryOperator("ItemId", iutc.ItemId, BinaryOperatorType.Equal),
                            new NotOperator(new NullOperator("UnitId")),
                            new NotOperator(new NullOperator("UnitId.UnitTypeId")),
                            new BinaryOperator("UnitId.UnitTypeId.Code", iutc.UnitTypeId.Code, BinaryOperatorType.Equal)
                        ));

                    foreach (ItemUnit iu in itemUnits)
                    {
                        iu.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        iu.Save();
                    }

                    uow.FlushChanges();


                    uow.CommitChanges();
                    return true;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    uow.Dispose();
                }
            }
        }

        public void PopulateDefaultUnitTypeConfigForAllItems()
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    XPCollection<ItemUnitTypeConfig> configs = new XPCollection<ItemUnitTypeConfig>(uow);
                    uow.Delete(configs);

                    XPCollection<Item> items = new XPCollection<Item>(uow,
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.GreaterOrEqual));

                    XPCollection<NAS.DAL.Nomenclature.UnitItem.UnitType> unitTypes =
                        new XPCollection<NAS.DAL.Nomenclature.UnitItem.UnitType>(uow,
                            CriteriaOperator.And(
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT,
                                BinaryOperatorType.Equal),
                            new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE,
                                BinaryOperatorType.NotEqual)));

                    if (items == null || items.Count == 0)
                        return;

                    foreach (Item item in items)
                    {
                        foreach (NAS.DAL.Nomenclature.UnitItem.UnitType unitType in unitTypes)
                        {
                            ItemUnitTypeConfig iutc = new ItemUnitTypeConfig(uow);
                            iutc.ItemId = item;
                            iutc.RowStatus = Utility.Constant.ROWSTATUS_DEFAULT;
                            if (item.ItemUnits != null && item.ItemUnits.Count > 0)
                            {
                                foreach (ItemUnit iu in item.ItemUnits)
                                {
                                    if (iu.UnitId != null  && iu.UnitId.UnitTypeId != null && (iu.RowStatus == Utility.Constant.ROWSTATUS_DEFAULT ||
                                        iu.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE) && iu.UnitId.UnitTypeId == unitType)
                                    {
                                        iutc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                                    }
                                }
                            }

                            iutc.IsMaster = false;
                            if (unitType.Code.Equals("SPECIFICATION"))
                                iutc.IsMaster = true;
                            iutc.UnitTypeId = unitType;
                            iutc.Save();
                        }
                    }

                    uow.CommitChanges();
                }
                catch
                {
                    //uow.ExplicitRollbackTransaction();
                    throw;
                }
            }
        }

        public bool UpdateDefaultItemUnitOfItem(Guid itemId, Guid defaultItemUnitId)
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    NAS.DAL.Nomenclature.Item.ItemUnit iu =
                    uow.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(defaultItemUnitId);

                    NAS.DAL.Nomenclature.Item.Item i =
                        uow.GetObjectByKey<NAS.DAL.Nomenclature.Item.Item>(itemId);

                    if (iu == null)
                        throw new Exception("The ItemUnit is not exist in system");

                    if (i == null)
                        throw new Exception("The Item is not exist in system");

                    IEnumerable<NAS.DAL.Nomenclature.Item.ItemUnit> tmpLst =
                        i.ItemUnits.Where(
                            row => 
                            row.UnitId != null 
                            && row.ItemId != null 
                            && row.UnitId.UnitTypeId != null 
                            && row.UnitId.UnitTypeId.Code == iu.UnitId.UnitTypeId.Code);

                    foreach (NAS.DAL.Nomenclature.Item.ItemUnit o in tmpLst)
                    {
                        if (o.ItemUnitId.Equals(defaultItemUnitId))
                        {
                            o.Coefficient = 1;
                            o.IsDefault = true;
                        }
                        else
                            o.IsDefault = false;
                        o.Save();
                    }

                    DrillDownUpdateAllCoefficientsOfItem(uow, itemId, defaultItemUnitId);

                    DrillUpUpdateAllCoefficientsOfItem(uow, itemId, defaultItemUnitId);

                    uow.CommitChanges();
                    return true;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    uow.Dispose();
                }
            }
        }

        public void DrillDownUpdateAllCoefficientsOfItem(UnitOfWork uow, Guid itemId, Guid currentItemUnitId)
        {
            NAS.DAL.Nomenclature.Item.ItemUnit currentItemUnit =
            uow.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(currentItemUnitId);

            NAS.DAL.Nomenclature.Item.Item item =
                uow.GetObjectByKey<NAS.DAL.Nomenclature.Item.Item>(itemId);

            if (currentItemUnit == null)
                throw new Exception("The ItemUnit is not exist in system");

            if (item == null)
                throw new Exception("The Item is not exist in system");
            XPCollection<NAS.DAL.Nomenclature.Item.ItemUnit> childrentItemUnit = new XPCollection<ItemUnit>(uow, 
                new BinaryOperator("ParentItemUnitId!Key", currentItemUnit.ItemUnitId, BinaryOperatorType.Equal));

            foreach (NAS.DAL.Nomenclature.Item.ItemUnit currIU in childrentItemUnit)
            {
                currIU.Coefficient = currentItemUnit.Coefficient * currIU.NumRequired;
                currIU.Save();
                uow.FlushChanges();
                DrillDownUpdateAllCoefficientsOfItem(uow, itemId, currIU.ItemUnitId);
            }
        }

        public void DrillDownDeleteLogicUnitOfItem(UnitOfWork uow, Guid itemId, Guid deletingItemUnitId)
        {
            NAS.DAL.Nomenclature.Item.ItemUnit currentItemUnit =
            uow.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(deletingItemUnitId);

            NAS.DAL.Nomenclature.Item.Item item =
                uow.GetObjectByKey<NAS.DAL.Nomenclature.Item.Item>(itemId);

            if (currentItemUnit == null)
                throw new Exception("The ItemUnit is not exist in system");

            if (item == null)
                throw new Exception("The Item is not exist in system");
            XPCollection<NAS.DAL.Nomenclature.Item.ItemUnit> childrentItemUnit = new XPCollection<ItemUnit>(uow,
                new BinaryOperator("ParentItemUnitId!Key", currentItemUnit.ItemUnitId, BinaryOperatorType.Equal));

            currentItemUnit.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
            currentItemUnit.Save();
            uow.FlushChanges();

            foreach (NAS.DAL.Nomenclature.Item.ItemUnit currIU in childrentItemUnit)
            {
                DrillDownDeleteLogicUnitOfItem(uow, itemId, currIU.ItemUnitId);
            }
        }

        public void DrillUpUpdateAllCoefficientsOfItem(UnitOfWork uow, Guid itemId, Guid currentItemUnitId)
        {
            NAS.DAL.Nomenclature.Item.ItemUnit currentItemUnit =
            uow.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(currentItemUnitId);

            NAS.DAL.Nomenclature.Item.Item item =
                uow.GetObjectByKey<NAS.DAL.Nomenclature.Item.Item>(itemId);

            if (currentItemUnit == null)
                throw new Exception("The ItemUnit is not exist in system");

            if (item == null)
                throw new Exception("The Item is not exist in system");

            NAS.DAL.Nomenclature.Item.ItemUnit parent = currentItemUnit.ParentItemUnitId;

            if (parent == null)
                return;

            parent.Coefficient = currentItemUnit.Coefficient / currentItemUnit.NumRequired;
            parent.Save();
            uow.FlushChanges();
            DrillUpUpdateAllCoefficientsOfItem(uow, itemId, parent.ItemUnitId);
        }
    }
}
