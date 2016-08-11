using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Utility;
using Microsoft.Win32;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using NAS.ETLBO.System.Object;
using NAS.ETLBO.System.ETL;
using Utility.ETL;
using NAS.DAL;
using NAS.BO.ETL;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Accounting.Journal;
using NAS.BO.ETL.Accounting;
using NAS.BO.ETL.Accounting.TempData;
using System.Windows.Forms;
using NAS.DAL.System.Log;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Nomenclature.Item;
using NAS.BO.ETL.Bill;
using NAS.BO.ETL.Bill.TempData;
using NAS.DAL.Inventory.Ledger;
using NAS.BO.ETL.Accounting.FinancialLiability;
using NAS.BO.ETL.Inventory;
using NAS.BO.ETL.Accounting.DiaryJournal;

namespace NAS_ETLMaster
{
    public class NAS_ETLMaster
    {
        //static IDataLayer ETL_DAL;
        //static IDataLayer BO_DAL;
        protected const int runAccounting = 0;
        protected const int runSystem = 1;
        protected const int runWareHouse = 2;
        protected const int runSale = 3;
        protected const int runPurchase = 4;

        protected static bool[] run = new bool[] { 
            true,   //COGSJob
            true,   //GeneralJournalToGeneralLedgerJob
            false,  //FinancialRevenueByItemJob
            true,   //FinancialCustomerLiabilityJob
            true,   //FinancialSupplierLiabilityJob    
            true,   //InventoryJournalToInventoryLedgerJob
            false,   //DiaryJournalJob
            false,  //CashFinancialJob8
            true,  //OnTheWayBuyingGoodJob: S04a6-DN
            true,  //ActualPriceJob: S04b3-DN, S04b9-DN
            true,  //SalesOrManufacturerExpenseJob: S04b4-DN, S04b5-DN
            true,  //PrepaidExpenseJob" S04b6-DN
            true,  //GoodsInInventoryBaseJob: S04b8-DN, S04b10-DN
            false,  //Delete Temp
            true   //S10-DN
        };     
        protected static string[] processName = new string[] {  
            "COGSJob",
            "GeneralJournalToGeneralLedgerJob",
            "FinancialRevenueByItemJob",
            "FinancialCustomerLiabilityJob",
            "FinancialSupplierLiabilityJob",
            "InventoryJournalToInventoryLedgerJob", 
            "DiaryJournalJob",
            "CashFinancialJob8",
            "OnTheWayBuyingGoodJob", //S04a6-DN
            "ActualPriceJob", //S04b3-DN, S04b9-DN
            "SalesOrManufacturerExpenseJob", //S04b4-DN, S04b5-DN
            "PrepaidExpenseJob", //S04b6-DN
            "GoodsInInventoryBaseJob", //S04b8-DN, S04b10-DN
            "DeleteTempRecord",
            "FinancialItemInventoryBaseJob"//S10-DN
        };
        public static Session session;
        protected static bool CallProcess(string processName)
        {
            bool ret = true;
            try
            {                   
                ProcessHelper processHelper = new ProcessHelper();
                Process newProcess = new Process();
                newProcess.StartInfo = processHelper.GetProcessInfo(processName);
                newProcess.StartInfo.UseShellExecute = false;
                newProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                newProcess.Start();                
                ret = true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
                ret = false;
            }
            return ret;
        }

        static void Main(string[] args)
        {            
            try
            {                
                ETLUtils etlUtil = new ETLUtils();
                //etlUtil.logs("d:/logs/history.txt", "Start");
                string FilePath = Application.StartupPath+"\\";                
                session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "ID"), etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "DBName"));                
                //session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML("../../dbConfig.xml", "ID"), etlUtil.GetValFromXML("../../dbConfig.xml", "DBName"));                
                //session = Utility.ETL.DatabaseHelper.GetNewSession("192.168.1.150","ERPCore_Vuong");
                ProcessHelper processHelper = new ProcessHelper();
                ETLUtils util = new ETLUtils();
                Util ETLUtil = new Util();
                Util.Populate(session);
                //etlUtil.logs("d:/logs/history.txt", "Start Launch");

                #region TestLogic
            
                #endregion

                #region Init TempData For Testing

                #endregion
                #region InitBO
                ETLEntryObjectHistoryBO _ETLEntryObjectHistoryBO = new ETLEntryObjectHistoryBO();
                ObjectEntryLogBO _ObjectEntryLogBO = new ObjectEntryLogBO();
                ETLJobBO _ETLJobBO = new ETLJobBO();
                ETLLogBO _ETLLogBO = new ETLLogBO();
                BusinessObjectBO _BusinessObjectBO = new BusinessObjectBO();
                ETL_DiaryJournal _ETL_DiaryJournal = new ETL_DiaryJournal();
                #endregion
                #region TestETL
                
                //FinancialSupplierLiabilityBO bo = new FinancialSupplierLiabilityBO();
                //ETLAccountingBO bo1 = new ETLAccountingBO();
                //ETLInventoryBO inventoryBO = new ETLInventoryBO();

                //double crbl = bo1.GetAccountingBalanceAtTime(session, Guid.Parse("11000000-0000-0000-0000-000000000001"), Guid.Parse("00110000-0000-0000-0000-000000000001"), DateTime.Parse("2014-01-03 12:00:00"));
                //COGS cogs = inventoryBO.GetPreviousCOGS(session, Guid.Parse("12300000-0000-0000-0000-000000000002"));
                //InventoryLedger crledger = session.GetObjectByKey<InventoryLedger>(Guid.Parse("e6be6f9a-a661-498a-a389-471945bde8c6"));
                //InventoryLedger preledger;
                //XPQuery<COGS> COGSQuery = new XPQuery<COGS>(session);
                //IQueryable<COGS> COGSCol = (from c in COGSQuery
                //                            where c.InventoryTransactionId.RowStatus >= Constant.ROWSTATUS_ACTIVE
                //                            select c);

                //preledger = inventoryBO.GetPreviousInventoryLedger(session, crledger);
                //XPQuery<InventoryLedger> ledgerQuery = new XPQuery<InventoryLedger>(session);
                //var TransactionList = (from r in ledgerQuery
                //                       select r.InventoryTransactionId).Distinct();

                //ETL_Transaction tran = bo1.ExtractTransaction(session, Guid.Parse("5cbaa5b8-9e7c-48ed-b2a7-7353f4a15fc4"),"131");
                //List<ETL_FinnancialSupplierLiabilityDetail> detail = bo.TransformTransactionToSupplierLiabilityDetail(session, tran, "131");
                //bo.LoadFinancialSupplierLiabilityDetail(session, detail, "131");
                //List<ETL_GeneralJournal> jl = new List<ETL_GeneralJournal>();
                //ETL_GeneralJournal jo1 = new ETL_GeneralJournal();
                //ETL_GeneralJournal jo2 = new ETL_GeneralJournal();
                //ETL_GeneralJournal jo3 = new ETL_GeneralJournal();
                //ETL_GeneralJournal jo4 = new ETL_GeneralJournal();

                //jo1.AccountId = Guid.Parse("2B7F3BA9-4AE4-42E7-8568-F5D5B8A009EB");//111
                //jo1.CreateDate = DateTime.Now;
                //jo1.Credit = 100;
                //jo1.Debit = 0;
                //jo1.Description = "111";
                //jo1.JournalType = 'C';
                //jl.Add(jo1);

                //jo2.AccountId = Guid.Parse("B107C675-2895-40B6-BD33-B4D501BBB0D9");//331
                //jo2.CreateDate = DateTime.Now;
                //jo2.Credit = 100;
                //jo2.Debit = 0;
                //jo2.Description = "331";
                //jo2.JournalType = 'C';
                //jl.Add(jo2);

                //jo3.AccountId = Guid.Parse("BDEF51E0-5318-42AA-BA2C-D5F713EA0711");//121
                //jo3.CreateDate = DateTime.Now;
                //jo3.Credit = 400;
                //jo3.Debit = 0;
                //jo3.Description = "121";
                //jo3.JournalType = 'C';
                //jl.Add(jo3);

                //jo4.AccountId = Guid.Parse("E573B446-D70D-46A3-9AE3-9E7F8A3C71C7");//521
                //jo4.CreateDate = DateTime.Now;
                //jo4.Credit = 0;
                //jo4.Debit = 600;
                //jo4.Description = "521";
                //jo4.JournalType = 'C';
                //jl.Add(jo4);
                //List<ETL_GeneralJournal> rs = _ETL_DiaryJournal.JoinJournalList(session, jl);
                //ETL_Transaction transaction = new ETL_Transaction();
                //transaction.Amount = 0;
                //transaction.Code = "ABC";
                //transaction.CreateDate = DateTime.Now;
                //transaction.Description = "Description";
                //transaction.GeneralJournalList = rs;
                //transaction.IsBalanceForward = false;
                //transaction.IssuedDate = DateTime.Now;
                //transaction.OwnerOrgId = Guid.Empty;                
                //transaction.TransactionId = Guid.NewGuid();
                //List<DJ_Fact> djList = _ETL_DiaryJournal.TransformToDiaryJournal(session, transaction);
                //List<ETL_GeneralJournal> rs = bo.JoinJournal(session, jl);
                //List<ETL_GeneralJournal> rs1 = bo.ClearJournalList(session, rs, Guid.Parse("C32268A6-5843-4199-A840-F6042B66686A"));
                //List<int> BusinessObjectTypeId = new List<int> { Constant.BusinessObjectType_Transaction,
                //                       Constant.BusinessObjectType_FinancialTransaction,
                //                       Constant.BusinessObjectType_InputInventoryCommandFinancialTransaction,
                //                       Constant.BusinessObjectType_OutputInventoryCommandFinancialTransaction,
                //                       Constant.BusinessObjectType_PaymentVoucherTransaction,
                //                       Constant.BusinessObjectType_PurcharseFinancialTransaction,
                //                       Constant.BusinessObjectType_ReceiptVoucherTransaction,
                //                       Constant.BusinessObjectType_SalesFinancialTransaction };

                //Guid refid = _ETLJobBO.ETLGetNextProcessObject(session, Guid.Parse("7688b739-f90e-43ac-8ccb-5c0663e30d0b"), BusinessObjectTypeId);
                #endregion

                bool checkDependency = true;
                bool checkService = true;
                checkService = processHelper.IsExistMutex(Utility.Constant.Process_Running_Mutex_Name_ETLService);
                //Check service running
                //if (!checkService)
                //{
                //    Console.WriteLine("You can't start NAS_ETLMaster without ETLService.\n2s to close...");
                //    //Thread.Sleep(2000);
                //    return;
                //}

                checkDependency = processHelper.IsExistMutex(Utility.Constant.Process_Running_Mutex_Name_ETLMaster);
                if (checkDependency)
                {
                    Console.WriteLine("Another NAS_ETLMaster Process Is Running.\nCan't open more than one NAS_ETLMaster Process...\n2s to close");                    
                    return;
                }
                Mutex ETLMasterMutex = new Mutex(true, Utility.Constant.Process_Running_Mutex_Name_ETLMaster);
                Console.WriteLine("ETLMaster Running");
                bool running = true;
                bool processRunning = false;
                do
                {
                    for (int i = 0; i < processName.Length; i++)
                    {
                        if (run[i])
                        {
                            processRunning = processHelper.IsExistMutex(Utility.Constant.Process_Running_Mutex_Name_ETLProcess + "_" + processName[i]);
                            if (!processRunning)
                            {
                                Console.WriteLine("Calling {0}", processName[i]);
                                CallProcess(processName[i]);
                            }
                        }
                    }
                    checkService = processHelper.IsExistMutex(Utility.Constant.Process_Running_Mutex_Name_ETLService);
                    running = !processHelper.IsExistMutex(Utility.Constant.Process_Stop_Mutex_Name_ETLMaster) && running;// && checkService;
                    Thread.Sleep(10000);
                }
                while (running);

                ETLMasterMutex.Dispose();
                ETLMasterMutex.Close();                            
            }
            catch (Exception ex) 
            {
                ETLUtils etlUtil = new ETLUtils();
                etlUtil.logs("d:/logs/history.txt", DateTime.Now.ToString() + " : Master Error:" + ex.Message);
                //Console.WriteLine(ex.ToString());
            }
            finally
            {
            }
        }
    }
}
