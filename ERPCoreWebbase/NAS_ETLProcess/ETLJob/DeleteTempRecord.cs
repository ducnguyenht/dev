using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Utility;
using Utility.ETL;
using System.Windows.Forms;
using NAS.DAL.ETL;
using NAS.DAL.Accounting.Journal;

namespace NAS_ETLProcess.ETLJob
{
    public class DeleteTempRecord : IETLJob
    {
        private string fJobRegisterCode;
        public string JobRegisterCode
        {
            get { return "DeleteTempRecord"; }
        }        
        Guid JobId;

        Session session;
        private int fBusinessObjectTypeId;
        public List<int> BusinessObjectTypeId
        {
            get
            {
                return new List<int>{Utility.Constant.BusinessObjectType_InventoryTransaction,
                                        Constant.BusinessObjectType_OutputInventoryCommandItemTransaction,
                                        Constant.BusinessObjectType_InputInventoryCommandItemTransaction};
            }
        }
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
                CriteriaOperator criteria_2 = new BinaryOperator("Code", "Deleting", BinaryOperatorType.Equal);
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
                etlUtil.logs("d:/logs/Process_history.txt", DateTime.Now.ToString() + " : COGS GetJobId:" + ex.Message);
                return;
            }
        }
        public void Extract()
        {
        }

        public void Transform()
        {
        }

        public void Load()
        {
        }

        public void WorkFlow()
        {
        }
        public void Run()
        {
            ETLUtils etlUtil = new ETLUtils();
            string FilePath = Application.StartupPath + "\\";
            session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "ID"), etlUtil.GetValFromXML(FilePath + "dbConfig.xml", "DBName"));
            //session = Utility.ETL.DatabaseHelper.GetNewSession(etlUtil.GetValFromXML("../../dbConfig.xml", "ID"), etlUtil.GetValFromXML("../../dbConfig.xml", "DBName"));
            GetJobId(session);
            //delete GeneralLedger            
            CriteriaOperator criteria = new BinaryOperator("RowStatus",Constant.ROWSTATUS_DELETED,BinaryOperatorType.Equal);
            XPCollection<GeneralLedger> temp = new XPCollection<GeneralLedger>(session, criteria);
            session = Utility.ETL.DatabaseHelper.GetNewSession("192.168.1.133", "ERPCore");
            session.PurgeDeletedObjects();              
        }
    }
}
