using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using DevExpress.Xpo;
using NAS.ETLBO.System.ETL;
using NAS.ETLBO.System.Object;
using NAS.BO.ETL.Accounting.Interface;
using NAS.BO.ETL.Inventory;
using DevExpress.Data.Filtering;
using NAS.DAL.ETL;
using Utility.ETL;
using NAS.DAL.System.Log;
using System.Windows.Forms;
using NAS.BO.ETL.Accounting.FinancialSalesOrManufactureExpense;
using System.Diagnostics;

namespace NAS_ETLProcess.ETLJob
{
    public class SalesOrManufacturerExpenseJob : IETLJob
    {
        public string JobRegisterCode
        {
            get { return "SalesOrManufacturerExpenseJob"; }
        }

        public List<int> BusinessObjectTypeId
        {
            get
            {
                return new List<int> {      Constant.BusinessObjectType_Transaction,
                                            Constant.BusinessObjectType_InputInventoryCommandFinancialTransaction,
                                            Constant.BusinessObjectType_OutputInventoryCommandFinancialTransaction,
                                            Constant.BusinessObjectType_FinancialTransaction,
                                            Constant.BusinessObjectType_PaymentVoucherTransaction,
                                            Constant.BusinessObjectType_ReceiptVoucherTransaction,
                                            Constant.BusinessObjectType_SalesFinancialTransaction,
                                            Constant.BusinessObjectType_PurcharseFinancialTransaction
                                            };
            }
        }

        private Guid JobId;
        private Session session;

        #region InitBO
        private ETLEntryObjectHistoryBO _ETLEntryObjectHistoryBO = new ETLEntryObjectHistoryBO();
        private ObjectEntryLogBO _ObjectEntryLogBO = new ObjectEntryLogBO();
        private ETLJobBO _ETLJobBO = new ETLJobBO();
        private ETLLogBO _ETLLogBO = new ETLLogBO();
        private BusinessObjectBO _BusinessObjectBO = new BusinessObjectBO();
        List<IELTLogicJob> Strategies = null;
        private ETLInventoryBO etlInventoryBO = new ETLInventoryBO();

        #endregion

        private Guid RefId = Guid.Empty;

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
                etlUtil.logs("d:/logs/Process_history.txt", DateTime.Now.ToString() + " : SalesOrManufacturerExpenseJob GetJobId:" + ex.Message);
                return;
            }
        }
        public void Extract()
        {
            RefId = _ETLJobBO.ETLGetNextProcessObject(session, JobId, BusinessObjectTypeId);
            if (RefId == Guid.Empty) return;

            XPCollection<BusinessObject> oldbusinessObjects = _BusinessObjectBO.GetAllBussinessObjectsAfterSpecificTransaction(
                session,
                JobId,
                BusinessObjectTypeId, RefId);

            Strategies = new List<IELTLogicJob>();
            Strategies.Add(new SalesOrManufacturerExpense154Strategy(RefId));
            Strategies.Add(new SalesOrManufacturerExpense241Strategy(RefId));
            Strategies.Add(new SalesOrManufacturerExpense621Strategy(RefId));
            Strategies.Add(new SalesOrManufacturerExpense622Strategy(RefId));
            Strategies.Add(new SalesOrManufacturerExpense623Strategy(RefId));
            Strategies.Add(new SalesOrManufacturerExpense627Strategy(RefId));
            Strategies.Add(new SalesOrManufacturerExpense631Strategy(RefId));
            Strategies.Add(new SalesOrManufacturerExpense641Strategy(RefId));
            Strategies.Add(new SalesOrManufacturerExpense642Strategy(RefId));            

            foreach (IELTLogicJob Strategy in Strategies)
                Strategy.ExtractTransaction(session);

            foreach (IELTLogicJob Strategy in Strategies)
            {
                if (Strategy.IsRelatedStrategy)
                {
                    Strategy.FixInvokedBussinessObjects(session, oldbusinessObjects);
                    return;
                }
            }
        }

        public void Transform()
        {
            if (RefId == Guid.Empty) return;

            _ETLLogBO.JobLog(session, JobId, "Running", "Status1");

            foreach (IELTLogicJob Strategy in Strategies)
            {
                Strategy.TransformTransaction(session);
            }
        }

        public void Load()
        {
            //if (Stop == true) return;
            if (RefId != Guid.Empty)
            {
                foreach (IELTLogicJob Strategy in Strategies)
                {
                    Strategy.LoadTransaction(session);
                }
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
            session = Utility.ETL.DatabaseHelper.GetNewSession(
                etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "ID"), 
                etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "DBName"));

            GetJobId(session);
            WorkFlow();
        }
    }
}
