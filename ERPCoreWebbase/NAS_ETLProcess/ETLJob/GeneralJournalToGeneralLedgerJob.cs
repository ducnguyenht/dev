using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.ETL;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Utility;
using NAS.DAL.ETL;
using NAS.BO.ETL.Accounting.TempData;
using NAS.BO.ETL.Accounting;
using NAS.ETLBO.System.ETL;
using NAS.ETLBO.System.Object;
using System.Threading;
using System.Windows.Forms;

namespace NAS_ETLProcess.ETLJob
{
    public class GeneralJournalToGeneralLedgerJob:IETLJob
    {
        private string fJobRegisterCode;
        public string JobRegisterCode{
            get { return "GeneralJournalToGeneralLedgerJob"; }
        }

        private int fBusinessObjectTypeId;
        public List<int> BusinessObjectTypeId
        {
            get
            {
                return new List<int> { Constant.BusinessObjectType_Transaction,
                                       Constant.BusinessObjectType_FinancialTransaction,
                                       Constant.BusinessObjectType_InputInventoryCommandFinancialTransaction,
                                       Constant.BusinessObjectType_OutputInventoryCommandFinancialTransaction,
                                       Constant.BusinessObjectType_PaymentVoucherTransaction,
                                       Constant.BusinessObjectType_PurcharseFinancialTransaction,
                                       Constant.BusinessObjectType_ReceiptVoucherTransaction,
                                       Constant.BusinessObjectType_SalesFinancialTransaction };
            }
        }

        Guid JobId;

        Session session;
        public void Dispose()
        {
            if (session != null)
            {
                session.Dispose();
                session = null;
            }
        }
        #region InitBO
        ETLEntryObjectHistoryBO _ETLEntryObjectHistoryBO = new ETLEntryObjectHistoryBO();
        ObjectEntryLogBO _ObjectEntryLogBO = new ObjectEntryLogBO();
        ETLJobBO _ETLJobBO = new ETLJobBO();
        ETLLogBO _ETLLogBO = new ETLLogBO();
        BusinessObjectBO _BusinessObjectBO = new BusinessObjectBO();
        ETLAccountingBO etlAccountingBO = new ETLAccountingBO();
        #endregion

        #region TempData
        ETL_Transaction transaction = null;
        List<ETL_GeneralLedger> ETL_GeneralLedgerList = null;
        #endregion

        Guid RefId = Guid.Empty;
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
                CriteriaOperator criteria_1 = new BinaryOperator("Code",JobRegisterCode,BinaryOperatorType.Equal);
                CriteriaOperator criteria_2 = new BinaryOperator("Code","Accounting",BinaryOperatorType.Equal);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1);
                CriteriaOperator criteria_cat = new GroupOperator(GroupOperatorType.And,criteria_2);
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
                etlUtil.logs("d:/logs/Process_history.txt", DateTime.Now.ToString() + " : GeneralJournalToGeneralLedgerJob GetJobId:" + ex.Message);
                return;
            }
        }

        public void Extract()
        {
            if (Stop == true) return;
            //Console.WriteLine("Extract");
            RefId = _ETLJobBO.ETLGetNextProcessObject(session, JobId, BusinessObjectTypeId);
            Console.WriteLine(RefId +" "+JobId);
            if (RefId != Guid.Empty)
            {
                transaction = etlAccountingBO.ExtractTransaction(session, RefId);
            }
            //Code for Extracting
        }
        public void Transform()
        {
            if (Stop == true) return;
            //Console.WriteLine("Transform");
            
            if (RefId == Guid.Empty) return;
            ETLJobBO _ETLJobBO = new ETLJobBO();
            _ETLLogBO.JobLog(session, JobId, "Running", "Status1");

            if (transaction != null && RefId!=Guid.Empty)
            {             
                ETL_GeneralLedgerList = etlAccountingBO.TransformGeneralJournalToGeneralLedger(transaction);
            }
            //Code for Transforming
        }
        public void Load()
        {
            if (Stop == true) return;
            //Console.WriteLine("Load");
            if (ETL_GeneralLedgerList != null && RefId != Guid.Empty)
            {
                etlAccountingBO.LoadGeneralLedger(session, ETL_GeneralLedgerList);
            }

            ETLEntryObjectHistoryBO _ETLEntryObjectHistoryBO = new ETLEntryObjectHistoryBO();            
            ETLJobBO _ETLJobBO = new ETLJobBO();            
            //Console.WriteLine("Load DataBase");

            if (RefId != Guid.Empty)
            {
                _ETLLogBO.JobLog(session, JobId, "State1", "Status1");
                _ETLLogBO.SetETLBusinessObjectStatus(session, JobId, RefId, 1);
                _ETLEntryObjectHistoryBO.SetETLEntryObjectHistoryStatus(session, JobId, RefId, 1);
            }
            //Code for Loading 
        }
        public void WorkFlow()
        {
            try
            {
                for (var i = 1; i <= 100;i++ )
                {
                    Extract();
                    Transform();
                    Load();
                    if (RefId == Guid.Empty) return;
                    RefId = Guid.Empty;
                }                
            }
            catch (Exception)
            {
                return;
            }
        }
        public void Run()
        {
            ETLUtils etlUtil = new ETLUtils();
            string FilePath = Application.StartupPath + "\\";
            session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "ID"), etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "DBName"));
            //session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML("../../dbConfig.xml", "ID"), etlUtil.GetValFromXML("../../dbConfig.xml", "DBName"));
            GetJobId(session);
            Stop = false;
            //Console.WriteLine("Start Job");
            WorkFlow();
            //Console.WriteLine("End Job");
        }
    }
}
