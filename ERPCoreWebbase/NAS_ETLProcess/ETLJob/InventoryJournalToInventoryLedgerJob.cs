using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.ETL;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.ETLBO.System.ETL;
using NAS.ETLBO.System.Object;
using NAS.BO.ETL.Inventory;
using DevExpress.Data.Filtering;
using NAS.DAL.ETL;
using Utility;
using System.Threading;
using NAS.BO.ETL.Accounting.TempData;
using NAS.BO.ETL.Accounting.FinancialLiability;
using NAS.BO.ETL.Accounting;
using System.Windows.Forms;
using NAS.BO.ETL.Inventory.TempData;
using NAS.BO.ETL.Accounting.Interface;
using NAS.BO.ETL.Accounting.FinancialItemInventory;
using NAS.DAL.System.Log;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Accounting.Journal;

namespace NAS_ETLProcess.ETLJob
{
    class InventoryJournalToInventoryLedgerJob : IETLJob
    {
        public string JobRegisterCode
        {
            get { return "InventoryJournalToInventoryLedger"; }
        }

        public List<int> BusinessObjectTypeId
        {
            get
            {
                return new List<int> { Constant.BusinessObjectType_InventoryTransaction,                //11
                                       Constant.BusinessObjectType_InputInventoryCommandItemTransaction,//1
                                       Constant.BusinessObjectType_OutputInventoryCommandItemTransaction//2
                                       //Constant.BusinessObjectType_InputInventoryCommandFinancialTransaction,
                                       //Constant.BusinessObjectType_OutputInventoryCommandFinancialTransaction
                                     };
            }
        }

        Guid JobId;
        Session session;

        #region InitBO
        ETLEntryObjectHistoryBO _ETLEntryObjectHistoryBO = new ETLEntryObjectHistoryBO();
        ObjectEntryLogBO _ObjectEntryLogBO = new ObjectEntryLogBO();
        ETLJobBO _ETLJobBO = new ETLJobBO();
        ETLLogBO _ETLLogBO = new ETLLogBO();
        BusinessObjectBO _BusinessObjectBO = new BusinessObjectBO();
        List<IELTLogicJob> Strategies = null;
        FinancialCustomerLiabilityBO liabilityBO = new FinancialCustomerLiabilityBO();
        ETLAccountingBO accountingBO = new ETLAccountingBO();
        ETLInventoryBO inventoryBO = new ETLInventoryBO();
        ETLInventoryBO etlInventoryBO = new ETLInventoryBO();

        #endregion

        //string Data = null;
        Guid RefId = Guid.Empty;
        ETL_InventoryTransaction transaction;
        List<ETL_InventoryLedger> LedgerList = new List<ETL_InventoryLedger>();
        private bool fStop;
        public bool Stop
        {
            get { return fStop; }
            set { fStop = value; }
        }

        public void GetJobId(Session session)
        {
            try
            {
                CriteriaOperator criteria_0 = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_1 = new BinaryOperator("Code", JobRegisterCode, BinaryOperatorType.Equal);
                CriteriaOperator criteria_2 = new BinaryOperator("Code", "Accounting", BinaryOperatorType.Equal);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1);
                CriteriaOperator criteria_cat = new GroupOperator(GroupOperatorType.And, criteria_2);
                NAS.DAL.ETL.ETLJob _ETLJob = session.FindObject<NAS.DAL.ETL.ETLJob>(criteria);
                if (_ETLJob == null)
                {
                    _ETLJob = new NAS.DAL.ETL.ETLJob(session);
                    _ETLJob.Code = JobRegisterCode;
                    _ETLJob.Description = "";
                    _ETLJob.ETLCategory = session.FindObject<ETLCategory>(criteria_cat);
                    _ETLJob.Is24x7 = true;
                    _ETLJob.Priority = 1;
                    _ETLJob.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    _ETLJob.Save();
                }
                JobId = _ETLJob.ETLJobId;
            }
            catch (Exception ex)
            {
                ETLUtils etlUtil = new ETLUtils();
                etlUtil.logs("d:/logs/Process_history.txt", DateTime.Now.ToString() + " : InventoryJournalToInventoryLedger GetJobId:" + ex.Message);
                return;
            }
        }
        public void Extract()
        {
            //_ETLLogBO.JobLog(session, JobId, "Next", "Status1");
            //Console.WriteLine("Extract");
            RefId = _ETLJobBO.ETLGetNextProcessObject(session, JobId, BusinessObjectTypeId);
            if (RefId == null) return;
            transaction = inventoryBO.ExtractInventoryTransaction(session, RefId);
            Console.WriteLine("InventoryJournalToInventoryLedger: "+RefId);
            //Thread.Sleep(3000);
        }
        public void Transform()
        {
            if (RefId == Guid.Empty) return;

            _ETLLogBO.JobLog(session, JobId, "Running", "Status1");

            LedgerList = inventoryBO.TransformInventoryJournalToInventoryLedger(transaction);
        }
        public void Load()
        {
            //if (Stop == true) return;
            if (RefId != Guid.Empty)
            {
                inventoryBO.LoadInventoryLedger(session, LedgerList);
                _ETLLogBO.JobLog(session, JobId, "State1", "Status1");
                _ETLLogBO.SetETLBusinessObjectStatus(session, JobId, RefId, 1);
                _ETLEntryObjectHistoryBO.SetETLEntryObjectHistoryStatus(session, JobId, RefId, 1);
            }
        }

        public void WorkFlow()
        {
            for (int i = 1; i <= 100; i++)
            {
                Extract();
                Transform();
                Load();
                if (RefId == Guid.Empty)
                {
                    return;
                }
                else
                {
                    RefId = Guid.Empty;
                }
            }
        }

        public void Run()
        {
            ETLUtils etlUtil = new ETLUtils();
            string FilePath = Application.StartupPath + "\\";
            session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "ID"), etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "DBName"));
            //session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML("../../dbConfig.xml", "ID"), etlUtil.GetValFromXML("../../dbConfig.xml", "DBName"));
            GetJobId(session);
            WorkFlow();
        }
    }
}