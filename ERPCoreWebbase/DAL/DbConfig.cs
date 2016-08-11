using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using DAL.Entity;

namespace NAS.DAL
{
    public class DbConfig
    {
        

        public string ConfigFilePath { get; set; }

        public DbConfig()
            : this(HttpContext.Current.Server.MapPath("~/erpconfig.xml"))
        {

        }

        public DbConfig(string xmlConfigFile)
        {
            ConfigFilePath = xmlConfigFile;
        }

        public MSSQLDbConfigEntity getActiveDbConfig()
        {
            //Utility.LogWriter.Instance.WriteToLog("---START DAL.DbConfig.getActiveDbConfig()---");
            try
            {
                MSSQLDbConfigEntity rs = null;
                //Utility.LogWriter.Instance.WriteToLog("Load data form '" + ConfigFilePath + "'");
                XElement dbConfig = XDocument.Load(ConfigFilePath).Descendants("database").
                    Where(e => e.Attribute("status") != null
                               && e.Attribute("status").Value.Equals("active"))
                        .FirstOrDefault();
                if (dbConfig != null)
                {
                    //Utility.LogWriter.Instance.WriteToLog("Read data...");
                    rs = new MSSQLDbConfigEntity();

                    rs.Server = Utility.Security.DecryptString(dbConfig.Element("server").Value, Utility.Security.PRIVATE_KEY_DEFAULT);
                    rs.Database = Utility.Security.DecryptString(dbConfig.Element("dbname").Value, Utility.Security.PRIVATE_KEY_DEFAULT);
                    rs.isAuth = bool.Parse(Utility.Security.DecryptString(dbConfig.Element("auth").Value, Utility.Security.PRIVATE_KEY_DEFAULT));
                    rs.UserId = Utility.Security.DecryptString(dbConfig.Element("userid").Value, Utility.Security.PRIVATE_KEY_DEFAULT);
                    rs.Password = Utility.Security.DecryptString(dbConfig.Element("password").Value, Utility.Security.PRIVATE_KEY_DEFAULT);
                    rs.Status = dbConfig.Attribute("status").Value;

                    //rs.Server = dbConfig.Element("server").Value;
                    //rs.Database = dbConfig.Element("dbname").Value;
                    //rs.isAuth = bool.Parse(dbConfig.Element("auth").Value);
                    //rs.UserId = dbConfig.Element("userid").Value;
                    //rs.Password = dbConfig.Element("password").Value;
                    //rs.Status = dbConfig.Attribute("status").Value;
                    
                    //Utility.LogWriter.Instance.WriteToLog("Read data successfully");
                    //Utility.LogWriter.Instance.WriteToLog(String.Format("Received data:: Server:{0} - Database:{1} - isAuth:{2} - UserId:{3} - Password:{4}",
                    //                                                rs.Server, rs.Database, rs.isAuth, rs.UserId, rs.Password));
                }
                
                return rs;
            }
            catch (Exception)
            {
                //Utility.LogWriter.Instance.WriteToLog("Exception:" + ex.Message);
                throw;
            }
            finally
            {
                //Utility.LogWriter.Instance.WriteToLog("---END DAL.DbConfig.getActiveDbConfig()---");
            }
        }

        /// <summary>
        ///     <database status="active|inactive">
        ///        <provider></provider>
        ///        <server></server>
        ///        <auth>true|false</auth>
        ///        <userid></userid>
        ///        <password></password>
        ///        <dbname></dbname>
        ///     </database>
        /// </summary>
        /// <returns></returns>
        public string parseToXMLString(MSSQLDbConfigEntity entity)
        {
            XElement elm = new XElement("database",
                                new XAttribute("status", entity.Status),
                                new XElement("provider", Utility.Security.EncryptString(entity.Provider, Utility.Security.PRIVATE_KEY_DEFAULT)),
                                new XElement("server", Utility.Security.EncryptString(entity.Server, Utility.Security.PRIVATE_KEY_DEFAULT)),
                                new XElement("auth", Utility.Security.EncryptString(entity.isAuth.ToString().ToLower(), Utility.Security.PRIVATE_KEY_DEFAULT)),
                                new XElement("userid", Utility.Security.EncryptString(entity.UserId, Utility.Security.PRIVATE_KEY_DEFAULT)),
                                new XElement("password", Utility.Security.EncryptString(entity.Password, Utility.Security.PRIVATE_KEY_DEFAULT)),
                                new XElement("dbname", Utility.Security.EncryptString(entity.Database, Utility.Security.PRIVATE_KEY_DEFAULT)));

            //XElement elm = new XElement("database",
            //                    new XAttribute("status", entity.Status),
            //                    new XElement("provider", entity.Provider),
            //                    new XElement("server", entity.Server),
            //                    new XElement("auth", entity.isAuth.ToString().ToLower()),
            //                    new XElement("userid", entity.UserId),
            //                    new XElement("password", entity.Password),
            //                    new XElement("dbname", entity.Database));

            return elm.ToString();
        }

        public void Save(MSSQLDbConfigEntity entity)
        {
            XDocument document = XDocument.Load(ConfigFilePath);
            //Update status of all database configuration to inactive
            var databaseConfig = document.Descendants("database");
            foreach (var item in databaseConfig)
            {
                item.SetAttributeValue("status", "inactive");
            }

            document.Element("config").AddFirst(XElement.Parse(this.parseToXMLString(entity)));
            document.Save(ConfigFilePath);
        }

    }
}
