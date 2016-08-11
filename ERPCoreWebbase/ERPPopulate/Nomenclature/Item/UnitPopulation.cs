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
    public class UnitPopulation
    {

        public void Populate(string dataFilePath)
        {

            string filePath = dataFilePath;
            string connStr = Utils.GetOleConnectionString(filePath, true);

            OleDbConnection connection = null;
            List<UnitEntity> unitEntityList = null;
            OleDbDataReader dr = null;
            OleDbCommand command = null;
            try
            {
                connection = new OleDbConnection(connStr);
                connection.Open();

                //Populate NAAN_DEFAULT data
                Util.Populate();

                #region Unit
                //Get distinct Unit
                string productSheetName = "dmhanghoa-vattuyte";
                string materiaSheetName = "DMNguyenlieu";
                string toolSheetName = "DMCongCuDungCu";

                unitEntityList = new List<UnitEntity>();

                //Get Unit from Unit column
                string query = String.Empty;
                query = "select distinct [Unit] from [" + productSheetName + "$]";
                query += " union ";
                query += "select distinct [Unit] from [" + materiaSheetName + "$]";
                query += " union ";
                query += "select distinct [Unit] from [" + toolSheetName + "$]";

                command =
                    new OleDbCommand(query, connection);
                using (dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //Collect data
                        UnitEntity unitEntity = new UnitEntity()
                        {
                            Code = (string)Utils.ConvertToNullIfDbNull(dr["Unit"]),
                            Name = (string)Utils.ConvertToNullIfDbNull(dr["Unit"]),
                            Description = (string)Utils.ConvertToNullIfDbNull(dr["Unit"])
                        };
                        unitEntityList.Add(unitEntity);
                    }
                }

                //Get Unit from Dvt2 column
                query = "select distinct [Dvt2] from [" + productSheetName + "$]";
                query += " union ";
                query += "select distinct [Dvt2] from [" + materiaSheetName + "$]";
                query += " union ";
                query += "select distinct [Dvt2] from [" + toolSheetName + "$]";

                command =
                    new OleDbCommand(query, connection);
                using (dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //Collect data
                        UnitEntity unitEntity = new UnitEntity()
                        {
                            Code = (string)Utils.ConvertToNullIfDbNull(dr["Dvt2"]),
                            Name = (string)Utils.ConvertToNullIfDbNull(dr["Dvt2"]),
                            Description = (string)Utils.ConvertToNullIfDbNull(dr["Dvt2"])
                        };
                        unitEntityList.Add(unitEntity);
                    }
                }

                //Get Unit from Dvt3 column
                query = "select distinct [Dvt3] from [" + productSheetName + "$]";
                query += " union ";
                query += "select distinct [Dvt3] from [" + materiaSheetName + "$]";
                query += " union ";
                query += "select distinct [Dvt3] from [" + toolSheetName + "$]";

                command =
                    new OleDbCommand(query, connection);
                using (dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //Collect data
                        UnitEntity unitEntity = new UnitEntity()
                        {
                            Code = (string)Utils.ConvertToNullIfDbNull(dr["Dvt3"]),
                            Name = (string)Utils.ConvertToNullIfDbNull(dr["Dvt3"]),
                            Description = (string)Utils.ConvertToNullIfDbNull(dr["Dvt3"])
                        };
                        unitEntityList.Add(unitEntity);
                    }
                }

                IEqualityComparer<UnitEntity> comparer = new UnitEntityComparer();

                //get distinct Unit by Code
                unitEntityList = unitEntityList.Distinct(comparer).ToList();

                using (Session session = XpoHelper.GetNewSession())
                {

                    foreach (var unitEntity in unitEntityList)
                    {
                        try
                        {

                            //Check required
                            if (unitEntity.Code == null || unitEntity.Code.Trim().Length == 0)
                            {
                                continue;
                            }

                            //Check dupplicate code
                            bool isExist =
                                NAS.DAL.Util.isExistXpoObject<NAS.DAL.Nomenclature.Item.Unit>
                                    ("Code", unitEntity.Code,
                                        Constant.ROWSTATUS_ACTIVE,
                                        Constant.ROWSTATUS_DEFAULT,
                                        Constant.ROWSTATUS_INACTIVE);
                            if (isExist)
                            {
                                continue;
                            }

                            Unit unit = new Unit(session)
                            {
                                Code = unitEntity.Code,
                                Description = unitEntity.Description,
                                Name = unitEntity.Name,
                                RowStatus = Constant.ROWSTATUS_ACTIVE,
                                RowCreationTimeStamp = DateTime.Now
                            };
                            unit.Save();

                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                        }
                    }

                }
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
