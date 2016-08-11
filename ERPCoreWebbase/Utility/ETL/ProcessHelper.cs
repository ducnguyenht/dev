using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Web;
using System.Windows.Forms;
using System.Security.AccessControl;

namespace Utility.ETL
{
    public class ProcessHelper
    {
        public ProcessStartInfo GetProcessInfo(string processName)
        {
            ETLUtils util = new ETLUtils();
            string processpath = System.IO.Directory.GetCurrentDirectory().Replace("\\","/")+"/";
            processpath = Application.StartupPath + "\\";
            //processpath = util.GetServiceImagePath("NAS_Service");
            //Console.WriteLine(processpath);            
            var info = new ProcessStartInfo();
            try
            {                
                info.FileName = processpath + "NAS_ETLProcess.exe";
                info.Arguments = processName;
                //info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            }
            catch (Exception)
            {                
                throw;
            }
            finally
            {
            }
            return info;
        }
        public bool IsExistMutex(string mutexName)
        {
            var ret = true;
            try
            {
                Mutex ETLMasterRunningMutex = Mutex.OpenExisting(mutexName);
                ETLMasterRunningMutex.Close();
                ret = true;
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                ret = false;
            }
            return ret;
        }
    }
}
