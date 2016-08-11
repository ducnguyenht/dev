using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.BO.ETL.Accounting.FinancialGeneralLedger
{
    public class FinancialGeneralLedgerBO
    {
        public void LoadTemplateAreaToDiaryJournalDetail(Session session, string _AccountRun)
        {
            string m_Sql = " delete from FinancialGeneralLedgerByYear_Fact where ";
        }
    }
}
