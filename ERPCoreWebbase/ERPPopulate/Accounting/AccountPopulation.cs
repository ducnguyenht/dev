using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Data.OleDb;
using DevExpress.Xpo;
using NAS.DAL;
using Utility;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Nomenclature.Organization;
using System.IO;
using System.Data;

namespace ERPPopulate.Accounting
{
    public class AccountPopulation
    {
        public void PopulateStandardTemplate(string dataFilePath)
        {
            string logFileName = String.Format("Log.xml", DateTime.Now);
            string logPath = dataFilePath.Substring(0, dataFilePath.LastIndexOf("\\") + 1);

            XDocument document = new XDocument(new XElement("messages"));

            //add root node
            XElement messages = document.Element("messages");
            document.Save(logPath + logFileName);

            messages.Add(new XElement("message", "Start populate accounting data"));
            document.Save(logPath + logFileName);
            Console.WriteLine("Start populate accounting data");

            messages.Add(new XElement("message", "Connecting to datasource..."));
            document.Save(logPath + logFileName);
            Console.WriteLine("Connecting to datasource...");

            string filePath = dataFilePath;
            string connStr = Utils.GetOleConnectionString(filePath, true);
            string dataSheetName = "Template";
            OleDbConnection connection = null;
            try
            {
                connection = new OleDbConnection(connStr);
                connection.Open();

                messages.Add(new XElement("message", "Connect successfully!"));
                document.Save(logPath + logFileName);
                Console.WriteLine("Connect successfully!");

                messages.Add(new XElement("message", "Start query data from datasource"));
                document.Save(logPath + logFileName);
                Console.WriteLine("Start query data from datasource");
                OleDbCommand command =
                    new OleDbCommand("select * from [" + dataSheetName + "$]", connection);

                messages.Add(new XElement("message", "End query data from datasource"));
                document.Save(logPath + logFileName);
                Console.WriteLine("End query data from datasource");

                List<AccountEntity> accountEntityList = new List<AccountEntity>();

                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    messages.Add(new XElement("message", "Start transform data"));
                    document.Save(logPath + logFileName);
                    Console.WriteLine("Start transform data");
                    while (dr.Read())
                    {
                        string code = null;
                        string parentCode = null;
                        int level = 0;
                        int balanceType = 0;

                        code = Utils.ConvertToNullIfDbNull(dr["Code"]) != null ?
                                        Utils.ConvertToNullIfDbNull(dr["Code"]).ToString() : null;
                        parentCode = Utils.ConvertToNullIfDbNull(dr["ParentCode"]) != null ?
                                    Utils.ConvertToNullIfDbNull(dr["ParentCode"]).ToString() : null;
                        level = Utils.ConvertToNullIfDbNull(dr["Level"]) != null ?
                                    int.Parse(Utils.ConvertToNullIfDbNull(dr["Level"]).ToString()) : 0;
                        balanceType = Utils.ConvertToNullIfDbNull(dr["BalanceType"]) != null ?
                                    int.Parse(Utils.ConvertToNullIfDbNull(dr["BalanceType"]).ToString()) : 0;   

                        //Collect data
                        AccountEntity accountEntity = new AccountEntity()
                        {
                            AccountType = (string)Utils.ConvertToNullIfDbNull(dr["AccountType"]),
                            Code = code,
                            ParentCode = parentCode,
                            Level = level,
                            Name = (string)Utils.ConvertToNullIfDbNull(dr["Name"]),
                            Description = (string)Utils.ConvertToNullIfDbNull(dr["Description"]),
                            BalanceType = balanceType
                        };

                        accountEntityList.Add(accountEntity);
                    }
                    messages.Add(new XElement("message", "End transform data"));
                    document.Save(logPath + logFileName);
                    Console.WriteLine("End transform data");
                }

                messages.Add(new XElement("message", "Start insert data to destination"));
                document.Save(logPath + logFileName);
                Console.WriteLine("Start insert data to destination");
                using (Session session = XpoHelper.GetNewSession())
                {

                    //Sort by level
                    accountEntityList = accountEntityList.OrderBy(r => r.Level).ToList();
                    int currentLevel = 0;

                    //Populate NAAN_DEFAULT data
                    Util.Populate();

                    //Get owner organization
                    Organization organization =
                            Util.getXPCollection<Organization>(session, "Code", "QUASAPHARCO").FirstOrDefault();

                    foreach (var accountEntity in accountEntityList)
                    {
                        try
                        {
                            if (accountEntity.Level != currentLevel)
                            {
                                currentLevel = accountEntity.Level;
                                messages.Add(new XElement("message", "Insert account level " + currentLevel + "..."));
                                document.Save(logPath + logFileName);
                                Console.WriteLine("Insert account level " + currentLevel + "...");
                            }

                            //Check required
                            if (accountEntity.Code == null || accountEntity.Code.Trim().Length == 0)
                            {
                                continue;
                            }

                            //Check dupplicate code
                            bool isExist = NAS.DAL.Util.isExistXpoObject<Account>("Code", accountEntity.Code, Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                            if (isExist)
                            {
                                messages.Add(new XElement("message", String.Format("Account code '{0}' is exist", accountEntity.Code)));
                                document.Save(logPath + logFileName);
                                continue;
                            }

                            AccountCategory accountCategory =
                                Util.getXPCollection<AccountCategory>(session, "Code", accountEntity.AccountCategory).FirstOrDefault();
                            AccountType accountType =
                                Util.getXPCollection<AccountType>(session, "Code", accountEntity.AccountType).FirstOrDefault();
                            Account parentAccount =
                                Util.getXPCollection<Account>(session, "Code", accountEntity.ParentCode).FirstOrDefault();

                            Account account = new Account(session)
                            {
                                AccountTypeId = accountType,
                                Code = accountEntity.Code,
                                Description = accountEntity.Description,
                                BalanceType = accountEntity.BalanceType,
                                Level = accountEntity.Level,
                                Name = accountEntity.Name,
                                OrganizationId = organization,
                                ParentAccountId = parentAccount,
                                RowStatus = Constant.ROWSTATUS_ACTIVE
                            };

                            account.Save();
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }
                messages.Add(new XElement("message", "End insert data to destination"));
                document.Save(logPath + logFileName);
                Console.WriteLine("End insert data to destination");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                messages.Add(new XElement("message", "Error: " + ex.Message));
                document.Save(logPath + logFileName);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();

                messages.Add(new XElement("message", "End populate accounting data"));
                document.Save(logPath + logFileName);
                Console.WriteLine("End populate accounting data");

            }
        }

        public void Populate(string dataFilePath)
        {

            string logFileName = String.Format("Log.xml", DateTime.Now);
            string logPath = dataFilePath.Substring(0, dataFilePath.LastIndexOf("\\") + 1);

            XDocument document = new XDocument(new XElement("messages"));

            //add root node
            XElement messages = document.Element("messages");
            document.Save(logPath + logFileName);

            messages.Add(new XElement("message", "Start populate accounting data"));
            document.Save(logPath + logFileName);
            Console.WriteLine("Start populate accounting data");

            messages.Add(new XElement("message", "Connecting to datasource..."));
            document.Save(logPath + logFileName);
            Console.WriteLine("Connecting to datasource...");

            string filePath = dataFilePath;
            string connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                                + filePath + ";Extended Properties='Excel 8.0;HDR=Yes;'";
            string dataSheetName = "Account";
            OleDbConnection connection = null;
            try
            {
                connection = new OleDbConnection(connStr);
                connection.Open();

                messages.Add(new XElement("message", "Connect successfully!"));
                document.Save(logPath + logFileName);
                Console.WriteLine("Connect successfully!");

                messages.Add(new XElement("message", "Start query data from datasource"));
                document.Save(logPath + logFileName);
                Console.WriteLine("Start query data from datasource");
                OleDbCommand command =
                    new OleDbCommand("select * from [" + dataSheetName + "$]", connection);
                messages.Add(new XElement("message", "End query data from datasource"));
                document.Save(logPath + logFileName);
                Console.WriteLine("End query data from datasource");

                List<AccountEntity> accountEntityList = new List<AccountEntity>();
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    messages.Add(new XElement("message", "Start transform data"));
                    document.Save(logPath + logFileName);
                    Console.WriteLine("Start transform data");
                    while (dr.Read())
                    {

                        string code = null;
                        string parentCode = null;
                        int level = 0;

                        if (Utils.ConvertToNullIfDbNull(dr["Level2"]) == null)
                        {
                            code = Utils.ConvertToNullIfDbNull(dr["Level1"]) != null ?
                                        Utils.ConvertToNullIfDbNull(dr["Level1"]).ToString() : null;
                            parentCode = null;
                            level = 1;
                        }
                        else
                        {
                            code = Utils.ConvertToNullIfDbNull(dr["Level2"]) != null ?
                                        Utils.ConvertToNullIfDbNull(dr["Level2"]).ToString() : null;
                            parentCode = Utils.ConvertToNullIfDbNull(dr["Level1"]) != null ?
                                        Utils.ConvertToNullIfDbNull(dr["Level1"]).ToString() : null;
                            level = 2;
                        }

                        AccountEntity accountEntity = new AccountEntity()
                        {
                            AccountCategory = (string)Utils.ConvertToNullIfDbNull(dr["AccountCategory"]),
                            AccountType = (string)Utils.ConvertToNullIfDbNull(dr["AccountType"]),
                            Code = code,
                            ParentCode = parentCode,
                            Level = level,
                            Name = (string)Utils.ConvertToNullIfDbNull(dr["Description"]),
                            Description = (string)Utils.ConvertToNullIfDbNull(dr["Description"]),
                            Comment = (string)Utils.ConvertToNullIfDbNull(dr["Comment"]),
                        };

                        accountEntityList.Add(accountEntity);
                    }
                    messages.Add(new XElement("message", "End transform data"));
                    document.Save(logPath + logFileName);
                    Console.WriteLine("End transform data");
                }

                messages.Add(new XElement("message", "Start insert data to destination"));
                document.Save(logPath + logFileName);
                Console.WriteLine("Start insert data to destination");
                using (Session session = XpoHelper.GetNewSession())
                {

                    //Sort by level
                    accountEntityList = accountEntityList.OrderBy(r => r.Level).ToList();
                    int currentLevel = 0;

                    Util.Populate();

                    Organization organization =
                            Util.getXPCollection<Organization>(session, "Code", "QUASAPHARCO").FirstOrDefault();

                    foreach (var accountEntity in accountEntityList)
                    {
                        try
                        {
                            if (accountEntity.Level != currentLevel)
                            {
                                currentLevel = accountEntity.Level;
                                messages.Add(new XElement("message", "Insert account level " + currentLevel + "..."));
                                document.Save(logPath + logFileName);
                                Console.WriteLine("Insert account level " + currentLevel + "...");
                            }

                            bool isExist = NAS.DAL.Util.isExistXpoObject<Account>("Code", accountEntity.Code, Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                            if (isExist)
                            {
                                messages.Add(new XElement("message", String.Format("Account code '{0}' is exist", accountEntity.Code)));
                                document.Save(logPath + logFileName);
                                continue;
                            }

                            AccountCategory accountCategory =
                                Util.getXPCollection<AccountCategory>(session, "Code", accountEntity.AccountCategory).FirstOrDefault();
                            AccountType accountType =
                                Util.getXPCollection<AccountType>(session, "Code", accountEntity.AccountType).FirstOrDefault();
                            Account parentAccount =
                                Util.getXPCollection<Account>(session, "Code", accountEntity.ParentCode).FirstOrDefault();

                            Account account = new Account(session)
                            {
                                AccountTypeId = accountType,
                                Code = accountEntity.Code,
                                Description = accountEntity.Description,
                                Level = accountEntity.Level,
                                Name = accountEntity.Name,
                                OrganizationId = organization,
                                ParentAccountId = parentAccount,
                                RowStatus = Constant.ROWSTATUS_ACTIVE
                            };

                            account.Save();
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }
                messages.Add(new XElement("message", "End insert data to destination"));
                document.Save(logPath + logFileName);
                Console.WriteLine("End insert data to destination");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (connection != null) connection.Dispose();
                messages.Add(new XElement("message", "End populate accounting data"));
                document.Save(logPath + logFileName);
                Console.WriteLine("End populate accounting data");

            }
        }
    }
}
