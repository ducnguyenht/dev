using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Nomenclature.UnitItem;
using NAS.DAL;

namespace NAS.BO.Nomenclature.Items
{
    public class UnitBO
    {
        public bool checkIsExistInItemUnit(Session session, Guid UnitId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And(
                         new BinaryOperator("UnitId.UnitId", UnitId, BinaryOperatorType.Equal),
                         new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                         new BinaryOperator("ItemId.RowStatus", 0, BinaryOperatorType.Greater)
                    );
                XPCollection<ItemUnit> itemUnit = new XPCollection<ItemUnit>(session, criteria);

                if (itemUnit == null || itemUnit.Count == 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        //DND
        public bool populateDefaultUnitToItemUnit(Guid itemId, Guid unitTypeId)
        {
            using (UnitOfWork uow = NAS.DAL.XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    string[] values = {"km,m,dm,cm,mm",
                                       "tan,kg,g,mg",
                                       "m³,lit,ml",
                                       "m²,dm²,cm²,mm²"};

                    string[] numOfUnits = {"1,1000,10,10,10",
                                       "1,1000,1000,1000",
                                       "1,1000,1000",
                                       "1,100,10000,1000000"};

                    string[] Code_unittype = { "LENGTH", "WEIGHT", "CAPACITY", "AREA" };

                    #region check database not null
                    Item item_id = uow.GetObjectByKey<Item>(itemId);
                    if (item_id == null)
                    {
                        throw new Exception("The key is not exist in Item");
                    }

                    UnitType unittype_id = uow.GetObjectByKey<UnitType>(unitTypeId);
                    if (unittype_id == null)
                    {
                        throw new Exception("The key is not exist in UnitType");
                    }

                    ItemUnitRelationType itemUnitRelationType = uow.FindObject<ItemUnitRelationType>(
                        CriteriaOperator.And(
                            new BinaryOperator("Name", "UNIT", BinaryOperatorType.Equal), 
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.GreaterOrEqual    
                        )));
                    if (itemUnitRelationType == null)
                    {
                        throw new Exception("The key is not exist in ItemUnitRelationType");
                    }
                    #endregion

                    XPCollection<ItemUnit> itemUnits = new XPCollection<ItemUnit>(uow,
                        CriteriaOperator.And(
                            new BinaryOperator("ItemId", item_id, BinaryOperatorType.Equal),
                            new BinaryOperator("UnitId.UnitTypeId.Code", unittype_id.Code, BinaryOperatorType.Equal)
                        ));

                    foreach (ItemUnit iu in itemUnits)
                    {
                        iu.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        iu.Save();
                    }

                    uow.FlushChanges();


                    for (int i = 0; i < Code_unittype.Length; i++)
                    {
                        if (unittype_id.Code.Equals(Code_unittype[i]))
                        {
                            string[] codeOfUnits = values[i].Split(',');
                            string[] numOfUnit = numOfUnits[i].Split(',');
                            ItemUnit parentItemUnit = null;
                            

                            for (int j = 0; j < codeOfUnits.Length; j++)
                            {
                                
                                Unit Unit_id = uow.FindObject<Unit>(
                                    CriteriaOperator.And(
                                        new BinaryOperator("Code", codeOfUnits[j], BinaryOperatorType.Equal), 
                                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT, BinaryOperatorType.Equal)
                                    ));
                                if (Unit_id == null)
                                {
                                    throw new Exception(string.Format("The code '{0}' is not exist in Unit", codeOfUnits[j]));
                                }

                                ItemUnit itemunit = new ItemUnit(uow)
                                    {
                                        ItemId = item_id,
                                        UnitId = Unit_id,
                                        NumRequired = int.Parse(numOfUnit[j].ToString()),
                                        ParentItemUnitId = j == 0 ? null : parentItemUnit,
                                        ItemUnitRelationTypeId = itemUnitRelationType,
                                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                                        RowCreationTimeStamp = DateTime.Now
                                    };
                                itemunit.Save();
                                uow.FlushChanges();
                                parentItemUnit = itemunit;
                            }
                        }
                    }

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

        public bool populateAllUnitInCombineUnitToSPCEIFICATIONType()
        {
            using (UnitOfWork uow = NAS.DAL.XpoHelper.GetNewUnitOfWork())
            {
            try
                {
                    XPCollection<Unit> units = new XPCollection<Unit>(uow,
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.GreaterOrEqual));

                    UnitType ut = uow.FindObject<UnitType>( CriteriaOperator.And(
                        new BinaryOperator("Code", "SPECIFICATION", BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT, BinaryOperatorType.Equal)
                    ));

                    foreach (Unit u in units)
                    {
                        u.UnitTypeId = ut;
                        u.Save();
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
        //END DND
    }
}
