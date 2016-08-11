using System;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Threading;

using Utility;
using System.Windows.Forms;
using Utility.ETL;
using System.Security.AccessControl;
using System.Security.Principal;

namespace NAS_ETLService
{
    public partial class ETLService : ServiceBase
    {
        public ETLService()
        {
            InitializeComponent();
        }

        protected void logs(string log)
        {
            var process = new System.Diagnostics.Process();
            var startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C echo " + log + " >>d:/logs.txt";
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.Start();
        }

        protected static ProcessStartInfo GetETLMasterProcessInfo()
        {
            ProcessStartInfo newProcess = null;
            try
            {
                ETLUtils util = new ETLUtils();
                var etlMasterpath = "C:/Program Files (x86)/NAANSolution/NAS_ETL";
                etlMasterpath = Application.StartupPath+"\\";
                newProcess = new ProcessStartInfo();
                newProcess.FileName = etlMasterpath + "NAS_ETLMaster.exe";
                newProcess.UseShellExecute = false;
                newProcess.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
            return newProcess;
        }
        protected void LaunchETLMaster()
        {
            try
            {
                System.Timers.Timer timer1 = new System.Timers.Timer(10000);
                timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                timer1.Interval = 10000;
                timer1.Enabled = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (!IsETLMasterRunning())
            {
                InvokeETLMaster();
            }
        }
        protected void StopETLMaster()
        {
            try
            {
                var stopEvent = new Mutex(true, Utility.Constant.Process_Stop_Mutex_Name_ETLMaster);
                while (IsETLMasterRunning())
                {
                    Console.WriteLine("Stopping ETLMaster");
                }
                stopEvent.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
        protected static bool InvokeETLMaster()
        {
            var ret = true;
            try
            {
                var ETLMaster = new Process();
                ETLMaster.StartInfo = GetETLMasterProcessInfo();
                ETLMaster.Start();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
            return ret;
        }
        protected static bool IsETLMasterRunning()
        {
            var ret = true;
            try
            {
                var processHelper = new ProcessHelper();
                ret = processHelper.IsExistMutex(Utility.Constant.Process_Running_Mutex_Name_ETLMaster);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
            return ret;
        }
        public void OnDebug()
        {
            try
            {
                OnStart(null);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
        protected override void OnPause()
        {
            Thread.Sleep(40000);
            base.OnPause();
        }        
        protected override void OnStart(string[] args)
        {
            try
            {
                bool createdNew;
                MutexSecurity mutexSecurity = new MutexSecurity();
                mutexSecurity.AddAccessRule(new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                                                                MutexRights.Synchronize | MutexRights.Modify, AccessControlType.Allow));
                Mutex ETLServiceMutex = new Mutex(true, Utility.Constant.Process_Running_Mutex_Name_ETLService,out createdNew,mutexSecurity);
                LaunchETLMaster();
                return;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
        protected override void OnStop()
        {
            try
            {
                Thread.Sleep(40000);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
    }
}
