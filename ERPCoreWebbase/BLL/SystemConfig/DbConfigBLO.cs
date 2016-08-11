using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entity;
using DAL;
using NAS.DAL;

namespace BLL.SystemConfig
{
    public class DbConfigBLO
    {
        DbConfig dbConfig = new DbConfig();

        public void Save(MSSQLDbConfigEntity entity)
        {
            dbConfig.Save(entity);
        }

        public MSSQLDbConfigEntity getActiveDbConfig()
        {
            return dbConfig.getActiveDbConfig();
        }

    }
}
