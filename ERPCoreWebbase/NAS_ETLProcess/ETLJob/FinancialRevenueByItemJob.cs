using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.ETL;
using DevExpress.Xpo;
using System.Windows.Forms;
using NAS.ETLBO.System.ETL;
using NAS.ETLBO.System.Object;
using NAS.BO.ETL.Inventory;
using DevExpress.Data.Filtering;
using NAS.DAL.ETL;
using Utility;
using NAS.BO.ETL.Accounting;
using NAS.BO.ETL.Bill;
using NAS.BO.ETL.Bill.TempData;

namespace NAS_ETLProcess.ETLJob
{
    public class FinancialRevenueByItemJob : IETLJob
    {
        
        private string fJobRegisterCode;
        public string JobRegisterCode
        {
            get { return "FinancialRevenueByItemJob"; }
        }
        private int fBusinessObjectTypeId;
        public List<int> BusinessObjectTypeId
        {
            get 
            {
                return new List<int> { Constant.BusinessObjectType_SalesFinancialTransaction };
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
        ETLFinancialRevenueByItemBO _ETLFinancialRevenueByItemBO = new ETLFinancialRevenueByItemBO();
        ETLBillBO _ETLBillBO = new ETLBillBO();
        ETL_Bill ETL_Bill_temp = new ETL_Bill();
        #endregion

        //string Data = null;
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
                etlUtil.logs("d:/logs/Process_history.txt", DateTime.Now.ToString() + " : FinancialRevenueByItem GetJobId:" + ex.Message);
                return;
            }
        }

        public void Extract()
        {
            //_ETLLogBO.JobLog(session, JobId, "Next", "Status1");
            //Console.WriteLine("Extract");
            RefId = _ETLJobBO.ETLGetNextProcessObject(session, JobId, BusinessObjectTypeId);
            ETL_Bill_temp = _ETLBillBO.ExtractSalesInvoice(session, RefId);
            //Console.WriteLine(RefId);
            //Thread.Sleep(3000);
        }

        public void Transform()
        {
            //if (Stop == true) return;            
            //Console.WriteLine("Transform");
            if (RefId == Guid.Empty) return;
            //etlBO.RepairCOGS(session, RefId);
        }

        public void Load()
        {
            //if (Stop == true) return;
            if (RefId == Guid.Empty) return;
            _ETLFinancialRevenueByItemBO.CreateFinancialRevenueByItem(session, ETL_Bill_temp);
            //Console.WriteLine("Load");
            if (RefId != Guid.Empty)
            {
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
            //session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "ID"), etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "DBName"));
            session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML("../../dbConfig.xml", "ID"), etlUtil.GetValFromXML("../../dbConfig.xml", "DBName"));
            GetJobId(session);
            //Stop = false;
            WorkFlow();
        }
    }
}
