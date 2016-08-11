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
    public class SupplierOrgPopulation
    {
        public void Populate(string dataFilePath)
        {

            string filePath = dataFilePath;
            string connStr = Utils.GetOleConnectionString(filePath, true);
            string dataSheetName = "dmkhachhang-nhacungcap";
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
                            Name = (string)Utils.ConvertToNullIfDbNull(dr["Name"]),
                            Address = (string)Utils.ConvertToNullIfDbNull(dr["Address"]),
                            TaxNumber = (string)Utils.ConvertToNullIfDbNull(dr["TaxNumber"]),
                            Description = (string)Utils.ConvertToNullIfDbNull(dr["Description"]),
                            AccountNumber = (string)Utils.ConvertToNullIfDbNull(dr["AccountNumber"]),
                            BankName = (string)Utils.ConvertToNullIfDbNull(dr["BankName"]),
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
                                NAS.DAL.Util.isExistXpoObject<NAS.DAL.Nomenclature.Organization.SupplierOrg>
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

                            SupplierOrg supplierOrg = new SupplierOrg(session)
                            {
                                AccountNumber = organizationEntity.AccountNumber,
                                Address = organizationEntity.Address,
                                BankName = organizationEntity.BankName,
                                Code = organizationEntity.Code,
                                Description = organizationEntity.Description,
                                Name = organizationEntity.Name,
                                TaxNumber = organizationEntity.TaxNumber,
                                OrganizationTypeId = defaultOrganizationType,
                                RowStatus = Constant.ROWSTATUS_ACTIVE,
                                RowCreationTimeStamp = DateTime.Now
                            };
                            supplierOrg.Save();
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
