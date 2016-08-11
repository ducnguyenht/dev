using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data.OleDb;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Nomenclature.Organization;
using Utility;

namespace ERPPopulate.Nomenclature.Organization
{
    public class ManufacturerOrgPopulation
    {
        public void Populate(string dataFilePath)
        {

            string filePath = dataFilePath;
            string connStr = Utils.GetOleConnectionString(filePath, true);
            string dataSheetName = "dmnoisanxuat";
            OleDbConnection connection = null;
            try
            {
                connection = new OleDbConnection(connStr);
                connection.Open();

                OleDbCommand command =
                    new OleDbCommand("select * from [" + dataSheetName + "$]", connection);


                List<OrganizationEntity> organizationEntityList = new List<OrganizationEntity>();

                using (OleDbDataReader dr = command.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        //Collect data
                        OrganizationEntity organizationEntity = new OrganizationEntity()
                        {
                            Code = (string)Utils.ConvertToNullIfDbNull(dr["Code"]),
                            Name = (string)Utils.ConvertToNullIfDbNull(dr["Name"])
                        };

                        organizationEntityList.Add(organizationEntity);
                    }

                }

                using (Session session = XpoHelper.GetNewSession())
                {

                    //Populate NAAN_DEFAULT data
                    Util.Populate();

                    
                    foreach (var organizationEntity in organizationEntityList)
                    {
                        try
                        {

                            //Check required
                            if (organizationEntity.Code == null || organizationEntity.Code.Trim().Length == 0)
                            {
                                continue;
                            }

                            //Check dupplicate code
                            bool isExist =
                                NAS.DAL.Util.isExistXpoObject<NAS.DAL.Nomenclature.Organization.ManufacturerOrg>
                                    ("Code", organizationEntity.Code, 
                                        Constant.ROWSTATUS_ACTIVE, 
                                        Constant.ROWSTATUS_DEFAULT, 
                                        Constant.ROWSTATUS_INACTIVE);
                            if (isExist)
                            {
                                continue;
                            }

                            //Get default organization type
                            OrganizationType defaultOrganizationType =
                                    Util.getXPCollection<OrganizationType>(session, "Code", "NAAN_DEFAULT").FirstOrDefault();

                            ManufacturerOrg manufacturerOrg = new ManufacturerOrg(session)
                            {
                                Code = organizationEntity.Code,
                                Name = organizationEntity.Name,
                                RowStatus = Constant.ROWSTATUS_ACTIVE,
                                RowCreationTimeStamp = DateTime.Now
                            };
                        
                            manufacturerOrg.Save();
                        
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }
    }
}
