using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPPopulate
{
    public class Utils
    {
        public static object ConvertToNullIfDbNull(object value)
        {
            if (value != null)
            {
                if (value.Equals(DBNull.Value))
                {
                    return null;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return null;
            }
        }

        public static string Platform
        {
            get
            {
                if (IntPtr.Size == 8)
                    return "x64";
                else
                    return "x86";
            }
        }

        public static string GetOleConnectionString(string fileName, bool withHeader)
        {
            string connectionString = null;
            string HDR = withHeader ? "YES" : "NO";
            if (Utils.Platform.Equals("x64"))
            {
                connectionString =
                    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName +
                    ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=" + HDR +
                    ";TypeGuessRows=0;ImportMixedTypes=Text\"";
            }
            else
            {
                connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                                + fileName + ";Extended Properties='Excel 8.0;HDR=" + HDR + ";'";
            }
            return connectionString;
        }

    }
}
