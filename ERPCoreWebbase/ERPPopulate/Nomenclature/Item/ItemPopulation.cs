using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using DevExpress.Xpo;
using NAS.DAL;
using Utility;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.CMS.ObjectDocument;

namespace ERPPopulate.Nomenclature.Item
{
    public class ItemPopulation
    {

        public List<ItemEntity> getItemData(string connectionString, string dataSheetName)
        {
            //Get PRODUCT
            OleDbConnection connection = null;
            List<ItemEntity> itemEntityList = null;
            OleDbDataReader dr = null;
            OleDbCommand command = null;

            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();

                command =
                    new OleDbCommand("select * from [" + dataSheetName + "$]", connection);

                itemEntityList = new List<ItemEntity>();

                using (dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        try
                        {
                            //Collect data
                            ItemEntity itemEntity = new ItemEntity()
                            {
                                Code = (string)Utils.ConvertToNullIfDbNull(dr["Code"]),
                                Name = (string)Utils.ConvertToNullIfDbNull(dr["Name"]),
                                ManufacturerCode = (string)Utils.ConvertToNullIfDbNull(dr["ManufacturerCode"]),
                                SupplierCode = (string)Utils.ConvertToNullIfDbNull(dr["SupplierCode"])
                            };
                            //When unit tree has 3 level
                            if ((string)Utils.ConvertToNullIfDbNull(dr["Dvt3"]) != null
                                && ((string)Utils.ConvertToNullIfDbNull(dr["Dvt3"])).Trim().Length > 0)
                            {
                                ItemUnitEntity itemUnitEntity = new ItemUnitEntity()
                                {
                                    ItemCode = itemEntity.Code,
                                    UnitCode = (string)Utils.ConvertToNullIfDbNull(dr["Dvt3"]),
                                    ParentUnitCode = null,
                                    NumRequired = (float)1,
                                    Level = 1
                                };
                                itemEntity.ItemUnits.Add(itemUnitEntity);

                                itemUnitEntity = new ItemUnitEntity()
                                {
                                    ItemCode = itemEntity.Code,
                                    UnitCode = (string)Utils.ConvertToNullIfDbNull(dr["Dvt2"]),
                                    ParentUnitCode = (string)Utils.ConvertToNullIfDbNull(dr["Dvt3"]),
                                    NumRequired = float.Parse(Utils.ConvertToNullIfDbNull(dr["Hs_dvt3"]).ToString()) /
                                                  float.Parse(Utils.ConvertToNullIfDbNull(dr["Hs_dvt2"]).ToString()),
                                    Level = 2
                                };
                                itemEntity.ItemUnits.Add(itemUnitEntity);

                                itemUnitEntity = new ItemUnitEntity()
                                {
                                    ItemCode = itemEntity.Code,
                                    UnitCode = (string)Utils.ConvertToNullIfDbNull(dr["Unit"]),
                                    ParentUnitCode = (string)Utils.ConvertToNullIfDbNull(dr["Dvt2"]),
                                    NumRequired = float.Parse(Utils.ConvertToNullIfDbNull(dr["Hs_dvt2"]).ToString()),
                                    Level = 3
                                };
                                itemEntity.ItemUnits.Add(itemUnitEntity);
                            }
                            //When unit tree has 2 level
                            else if ((string)Utils.ConvertToNullIfDbNull(dr["Dvt2"]) != null
                                && ((string)Utils.ConvertToNullIfDbNull(dr["Dvt2"])).Trim().Length > 0)
                            {
                                ItemUnitEntity itemUnitEntity = new ItemUnitEntity()
                                {
                                    ItemCode = itemEntity.Code,
                                    UnitCode = (string)Utils.ConvertToNullIfDbNull(dr["Dvt2"]),
                                    ParentUnitCode = null,
                                    NumRequired = (float)1,
                                    Level = 1,
                                };
                                itemEntity.ItemUnits.Add(itemUnitEntity);

                                itemUnitEntity = new ItemUnitEntity()
                                {
                                    ItemCode = itemEntity.Code,
                                    UnitCode = (string)Utils.ConvertToNullIfDbNull(dr["Unit"]),
                                    ParentUnitCode = (string)Utils.ConvertToNullIfDbNull(dr["Dvt2"]),
                                    NumRequired = float.Parse(Utils.ConvertToNullIfDbNull(dr["Hs_dvt2"]).ToString()),
                                    Level = 2
                                };
                                itemEntity.ItemUnits.Add(itemUnitEntity);
                            }
                            //When unit tree has 1 level
                            else if ((string)Utils.ConvertToNullIfDbNull(dr["Unit"]) != null
                                && ((string)Utils.ConvertToNullIfDbNull(dr["Unit"])).Trim().Length > 0)
                            {
                                ItemUnitEntity itemUnitEntity = new ItemUnitEntity()
                                {
                                    ItemCode = itemEntity.Code,
                                    UnitCode = (string)Utils.ConvertToNullIfDbNull(dr["Unit"]),
                                    ParentUnitCode = null,
                                    NumRequired = (float)1,
                                    Level = 1
                                };
                                itemEntity.ItemUnits.Add(itemUnitEntity);
                            }

                            itemEntityList.Add(itemEntity);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                return itemEntityList;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        public void InsertItems(List<ItemEntity> itemEntityList, string itemTradingTypeName, string objectTypeName)
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                foreach (var itemEntity in itemEntityList)
                {
                    try
                    {
                        if (itemEntity.Code.Equals("EVE006"))
                        {
                            string debug = "Y";
                        }
                        //Check required
                        if (itemEntity.Code == null || itemEntity.Code.Trim().Length == 0)
                        {
                            continue;
                        }

                        //Check dupplicate code
                        bool isExist =
                            NAS.DAL.Util.isExistXpoObject<NAS.DAL.Nomenclature.Item.Item>
                                ("Code", itemEntity.Code,
                                    Constant.ROWSTATUS_ACTIVE,
                                    Constant.ROWSTATUS_DEFAULT,
                                    Constant.ROWSTATUS_INACTIVE);
                        if (isExist)
                        {
                            continue;
                        }
                        //Get item trading type
                        ItemTradingType itemTradingType =
                                Util.getXPCollection<ItemTradingType>(uow, "Name", itemTradingTypeName).FirstOrDefault();
                        //Get Manufacturer
                        ManufacturerOrg manufacturerOrg =
                            Util.getXPCollection<ManufacturerOrg>(uow, "Code", itemEntity.ManufacturerCode,
                                Constant.ROWSTATUS_ACTIVE).FirstOrDefault();

                        if (manufacturerOrg == null)
                        {
                            manufacturerOrg = Util.getDefaultXpoObject<ManufacturerOrg>(uow);
                        }

                        //Insert into Item table
                        NAS.DAL.Nomenclature.Item.Item item = new NAS.DAL.Nomenclature.Item.Item(uow)
                        {
                            Code = itemEntity.Code,
                            Description = itemEntity.Description,
                            ManufacturerOrgId = manufacturerOrg,
                            ItemTradingTypeId = itemTradingType,
                            Name = itemEntity.Name,
                            RowStatus = Constant.ROWSTATUS_ACTIVE,
                            RowCreationTimeStamp = DateTime.Now
                        };
                        item.Save();

                        //Get Supplier
                        SupplierOrg supplierOrg = Util.getXPCollection<SupplierOrg>(uow, "Code", itemEntity.SupplierCode,
                                Constant.ROWSTATUS_ACTIVE).FirstOrDefault();
                        if (supplierOrg != null)
                        {
                            //Insert into ItemSupplier table
                            ItemSupplier itemSupplier = new ItemSupplier(uow)
                            {
                                ItemId = item,
                                SupplierOrgId = supplierOrg
                            };
                            itemSupplier.Save();
                        }

                        ObjectType objectType = Util.getXPCollection<ObjectType>(uow, "Name", objectTypeName,
                                Constant.ROWSTATUS_ACTIVE).FirstOrDefault();
                        if (objectType != null)
                        {
                            //Insert into ItemSupplier table
                            ItemCustomType itemCustomType = new ItemCustomType(uow)
                            {
                                ItemId = item,
                                ObjectTypeId = objectType
                            };
                            itemCustomType.Save();
                        }

                        uow.CommitChanges();

                        //using (Session itemUnitSession = XpoHelper.GetNewSession())
                        //{
                        //    //Get UNIT relation type
                        //    ItemUnitRelationType unitRelationType =
                        //        NAS.DAL.Util.getXPCollection<ItemUnitRelationType>(itemUnitSession, "Name", "UNIT").FirstOrDefault();
                        //    itemEntity.ItemUnits = itemEntity.ItemUnits.OrderBy(r => r.Level).ToList();

                        //    ////Insert into ItemUnit
                        //    foreach (ItemUnitEntity itemUnitEntity in itemEntity.ItemUnits)
                        //    {

                        //        NAS.DAL.Nomenclature.Item.Item itemTemp =
                        //            NAS.DAL.Util.getXPCollection<NAS.DAL.Nomenclature.Item.Item>
                        //                (itemUnitSession, "Code", itemUnitEntity.ItemCode).FirstOrDefault();

                        //        Unit unit =
                        //            NAS.DAL.Util.getXPCollection<Unit>(itemUnitSession, "Code", itemUnitEntity.UnitCode).FirstOrDefault();

                        //        if (unit == null)
                        //            break;

                        //        Unit parentUnit =
                        //            NAS.DAL.Util.getXPCollection<Unit>(itemUnitSession, "Code", itemUnitEntity.ParentUnitCode).FirstOrDefault();

                        //        ItemUnit parentItemUnit = itemTemp.ItemUnits.Where(r => r.UnitId == parentUnit).FirstOrDefault();

                        //        ItemUnit itemUnit = new ItemUnit(itemUnitSession)
                        //        {
                        //            ItemId = itemTemp,
                        //            ItemUnitRelationTypeId = unitRelationType,
                        //            NumRequired = itemUnitEntity.NumRequired,
                        //            RowStatus = Constant.ROWSTATUS_ACTIVE,
                        //            UnitId = unit,
                        //            ParentItemUnitId = parentItemUnit,
                        //            RowCreationTimeStamp = DateTime.Now
                        //        };
                        //        itemUnit.Save();
                        //    }
                        //}
                        
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                    }
                }

            }
        }

        public void Populate(string dataFilePath)
        {

            string filePath = dataFilePath;
            string connStr = Utils.GetOleConnectionString(filePath, true);

            OleDbConnection connection = null;
            List<ItemEntity> itemEntityList = null;

            try
            {
                connection = new OleDbConnection(connStr);
                connection.Open();

                //Populate NAAN_DEFAULT data
                Util.Populate();

                #region PRODUCT
                //Get PRODUCT
                string dataSheetName = "dmhanghoa-vattuyte";
                itemEntityList = this.getItemData(connStr, dataSheetName);
                InsertItems(itemEntityList, "BUYING_AND_SALES", "PRODUCT");
                #endregion


                #region MATERIAL
                //Get MATERIAL
                dataSheetName = "DMNguyenlieu";
                itemEntityList = this.getItemData(connStr, dataSheetName);
                InsertItems(itemEntityList, "BUYING_AND_SALES", "MATERIAL");
                #endregion


                #region TOOL
                //Get TOOL
                dataSheetName = "DMCongCuDungCu";
                itemEntityList = this.getItemData(connStr, dataSheetName);
                InsertItems(itemEntityList, "BUYING_FOR_INTERNAL_USING", "TOOL");
                #endregion


            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

    }
}
