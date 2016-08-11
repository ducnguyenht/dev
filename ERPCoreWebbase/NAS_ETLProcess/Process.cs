using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Utility;
using System.Threading;
using NAS_ETLProcess.ETLJob;
using Utility.ETL;
using System.Windows.Forms;
using System.Diagnostics;

namespace NAS_ETLProcess
{
    class Process
    {
        public static Session session;
        //static void Main(string[] args)
        //{
        //    string jobName = null;
        //    try
        //    {
        //        if (args.Length > 0)
        //        {
        //            jobName = args[0];
        //        }
        //        //string conn = MSSqlConnectionProvider.GetConnectionString("192.168.1.171", "ERPCORE_ETL_Test");                
        //        //XpoDefault.DataLayer = XpoDefault.GetDataLayer(conn, AutoCreateOption.DatabaseAndSchema);
        //        ETLUtils etlUtil = new ETLUtils();
        //        string FilePath = Application.StartupPath + "\\";
        //        session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "ID"), etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "DBName"));
        //        //session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML("../../dbConfig.xml", "ID"), etlUtil.GetValFromXML("../../dbConfig.xml", "DBName"));
        //        ProcessHelper processHelper = new ProcessHelper();
        //        //Check ETLMaster Is Running
        //        bool checkMaster = processHelper.IsExistMutex(Utility.Constant.Process_Running_Mutex_Name_ETLMaster);
        //        if (!checkMaster)
        //        {
        //            Console.WriteLine("Can't start this process without ETLMaster.\n2s to close");
        //            //Thread.Sleep(2000);
        //            return;

        //        }
        //        //Check Accounting Process Is Running
        //        bool checkDependency = true;
        //        checkDependency = processHelper.IsExistMutex(Utility.Constant.Process_Running_Mutex_Name_ETLProcess + "_" + jobName);
        //        if (checkDependency)
        //        {
        //            //Console.WriteLine("Another Process Is Running.\nCan't open more than one Process...\n");                    
        //            return;
        //        }
        //        //Stop Check Accounting Process Is Running

        //        Mutex JobMutex = new Mutex(true, Utility.Constant.Process_Running_Mutex_Name_ETLProcess + "_" + jobName);
        //        //Console.WriteLine("Process " + jobName + " Is Start....");
        //        CallJob(jobName);
        //        //Console.WriteLine("Process " + jobName + " Is Stop....");
        //        JobMutex.Dispose();
        //        JobMutex.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        ETLUtils etlUtil = new ETLUtils();
        //        etlUtil.logs("d:/logs/Process_history.txt", DateTime.Now.ToString() + " : Process Error (Main):" + ex.Message);
        //    }
        //    finally
        //    {
        //    }
        //}

        public static void CallJob(string jobName)
        {
            try
            {                
                Type jobType = Type.GetType("NAS_ETLProcess.ETLJob."+jobName, true);
                Console.WriteLine("JobName: -" + jobName + "-");
                Object obj = (Activator.CreateInstance(jobType));

                IETLJob job = (IETLJob)obj;                
                //Console.WriteLine("Process " + jobName + " Is Running....");

                job.Run();                
                //Thread.Sleep(1000);
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR StackTrace: " + ex.StackTrace);
                Console.WriteLine("ERROR Source: " + ex.Source);
                //Console.WriteLine("ERROR MyMessage: " + ex.myMessage);
            }
            finally
            {
            }
        }
    }
}
