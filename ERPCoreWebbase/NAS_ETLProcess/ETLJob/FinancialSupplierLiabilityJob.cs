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
using System.Windows.Forms;
using NAS.BO.ETL.Accounting.FinancialLiability;
using NAS.BO.ETL.Accounting.TempData;
using NAS.BO.ETL.Accounting;
using NAS.DAL.System.Log;

namespace NAS_ETLProcess.ETLJob
{
    public class FinancialSupplierLiabilityJob : IETLJob
    {
        private string fJobRegisterCode;
        public string JobRegisterCode
        {
            get { return "FinancialSupplierLiabilityJob"; }
        }

        private int fBusinessObjectTypeId;
        public List<int> BusinessObjectTypeId
        {
            get
            {
                return new List<int> {  Constant.BusinessObjectType_Transaction,
                                            Constant.BusinessObjectType_FinancialTransaction,
                                            Constant.BusinessObjectType_PaymentVoucherTransaction,
                                            Constant.BusinessObjectType_ReceiptVoucherTransaction,
                                            Constant.BusinessObjectType_SalesFinancialTransaction,
                                            Constant.BusinessObjectType_PurcharseFinancialTransaction
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
        FinancialSupplierLiabilityBO liabilityBO = new FinancialSupplierLiabilityBO();
        ETLAccountingBO accountingBO = new ETLAccountingBO();
        ETLInventoryBO etlInventoryBO = new ETLInventoryBO();

        #endregion

        //string Data = null;
        Guid RefId = Guid.Empty;
        ETL_Transaction transaction;
        List<ETL_FinnancialSupplierLiabilityDetail> detailList = new List<ETL_FinnancialSupplierLiabilityDetail>();
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
                etlUtil.logs("d:/logs/Process_history.txt", DateTime.Now.ToString() + " : FinancialSupplierLiability GetJobId:" + ex.Message);
                return;
            }
        }
        public void Extract()
        {
            //_ETLLogBO.JobLog(session, JobId, "Next", "Status1");
            //Console.WriteLine("Extract");
            RefId = _ETLJobBO.ETLGetNextProcessObject(session, JobId, BusinessObjectTypeId);
            if (RefId == Guid.Empty) return;
            XPCollection<BusinessObject> oldbusinessObjects = _BusinessObjectBO.GetAllBussinessObjectsAfterSpecificTransaction(
               session,
               JobId,
               BusinessObjectTypeId,
               RefId);            

            transaction = accountingBO.ExtractTransaction(session, RefId, "331");
            if (transaction != null)
                liabilityBO.FixInvokedBussinessObjects(session, oldbusinessObjects);
        }

        public void Transform()
        {
            if (RefId == Guid.Empty) return;

            _ETLLogBO.JobLog(session, JobId, "Running", "Status1");

            detailList = liabilityBO.TransformTransactionToSupplierLiabilityDetail(session, transaction, "331");
        }

        public void Load()
        {
            //if (Stop == true) return;
            if (RefId != Guid.Empty)
            {
                liabilityBO.LoadFinancialSupplierLiabilityDetail(session, detailList, "331");
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
