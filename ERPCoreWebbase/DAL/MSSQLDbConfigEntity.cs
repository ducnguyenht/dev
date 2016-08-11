using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DAL.Entity
{
    public class MSSQLDbConfigEntity
    {
        public string Provider { get { return "MSSQL"; } }
        public string Server { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public bool isAuth { get; set; }
        public string Status { get; set; }

        public string ConnectionString
        {
            get
            {
                string connStr = null;
                if (isAuth)
                {
                    connStr = DevExpress.Xpo.DB.MSSqlConnectionProvider
                                .GetConnectionString(Server, UserId, Password, Database);
                }
                else
                {
                    connStr = DevExpress.Xpo.DB.MSSqlConnectionProvider
                                .GetConnectionString(Server, Database);
                }
                return connStr;
            }
        }

        public bool isServerConnected()
        {
            string connStr;
            int connTimeout = 15;
            if (isAuth)
            {
                connStr = String.Format("Server={0};User Id={1};Password={2};Initial Catalog={3};Connection Timeout={4}", Server, UserId, Password, Database, connTimeout);
            }
            else
            {

                connStr = String.Format("Server={0};Integrated Security=SSPI;Initial Catalog={1};Connection Timeout={2}", Server, Database, connTimeout);
            }
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

    }
}
