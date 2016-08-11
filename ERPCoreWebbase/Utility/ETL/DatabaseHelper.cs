using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;

namespace Utility.ETL
{
    public class DatabaseHelper
    {
        public static Session GetNewSession(string IP,string DBName)
        {
            Session result = null;
            try
            {
                #region ETLDatabase Config
                string conn = MSSqlConnectionProvider.GetConnectionString(IP,DBName);
                IDataLayer dl = XpoDefault.GetDataLayer(conn, AutoCreateOption.DatabaseAndSchema);
                result = new Session(dl);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
            return result;
        }
        
    }
}
