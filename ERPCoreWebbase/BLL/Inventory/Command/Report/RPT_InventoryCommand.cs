using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace NAS.BO.Inventory.Command.Report
{
    public class RPT_InventoryJournal
    {
        public Guid InventoryJournalId { get; set; }
        public Guid AccountId { get; set; }
        public Guid ItemUnitId { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string Description { get; set; }
        public char JournalType { get; set; }
        public DateTime IssueDate { get; set; }
        public Guid TransactionId { get; set; }
    }

    public class RPT_GenernalJournal
    {
        public Guid GeneralJournalId {   get; set;   }
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string Description { get; set; }
        public char JournalType { get; set; }
        public DateTime IssueDate { get; set; }
        public Guid TransactionId { get; set; }
        public Guid CurrencyId { get; set; }
    }

    public class RPT_InventoryCommand_Row
    {
        public string SeqNo { get; set; }
        public string Code { get; set; }
        public DateTime CreateDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemUnit { get; set; }
        public double Quantity { get; set; }
        public double PlanQuantity { get; set; }
        public double Price { get; set; }
        public double TotalOfRow { get; set; }
        public double Total { get; set; }
        public string TotalString { get; set; }
        public string AmountByString { get; set; }
        public string InventoryName { get; set; }
        public string InventoryAddress { get; set; }
        public string ShipperName { get; set; }
        public string ReceiverName { get; set; }
        public string CreatorName { get; set; }
        public string StoreKeeperName { get; set; }
        public string BussinessReason { get; set; }
    }

    public class RPT_InputInventoryCommand_Row : RPT_InventoryCommand_Row
    {
        public string PurchaseInvoiceCode { get; set; }
        public DateTime PurchaseInvoiceDate { get; set; }
    }

    public class RPT_OutputInventoryCommand_Row : RPT_InventoryCommand_Row
    {
        public string SalesInvoiceCode { get; set; }
        public string RelevantDepartment { get; set; }
        public DateTime SalesInvoiceDate { get; set; }
    }

    public abstract class RPT_InventoryCommand
    {
        protected InventoryCommandBO ICBO = new InventoryCommandBO();

        public List<RPT_GenernalJournal> RPT_GenernalJournals_Objects
            = new List<RPT_GenernalJournal>();       

        public List<RPT_InventoryCommand_Row> RPT_InventoryCommand_Rows
            = new List<RPT_InventoryCommand_Row>();

        private List<RPT_GenernalJournal> ProcessingDupplicateDebit()
        {
            List<RPT_GenernalJournal> rs = new List<RPT_GenernalJournal>();
            foreach (RPT_GenernalJournal journal1 in RPT_GenernalJournals_Objects)
            {
                bool flg_isDupplicate = false;
                foreach (RPT_GenernalJournal journal2 in RPT_GenernalJournals_Objects)
                {
                    if (journal1 == journal2)
                        continue;

                    RPT_GenernalJournal journal = new RPT_GenernalJournal();
                    if (journal1.AccountId == journal2.AccountId
                        && journal1.Debit == journal2.Debit)
                    {
                        journal.AccountId = journal1.AccountId;
                        journal.Debit = journal1.Debit + journal2.Debit;
                        journal.Credit = journal1.Credit;
                        journal.CurrencyId = journal1.CurrencyId;
                        rs.Add(journal);
                        flg_isDupplicate = true;
                    }
                }

                if (!flg_isDupplicate)
                {
                    rs.Add(journal1);
                }
            }
            return rs;
        }

        private List<RPT_GenernalJournal> ProcessingDupplicateCredit()
        {
            List<RPT_GenernalJournal> rs = new List<RPT_GenernalJournal>();
            foreach (RPT_GenernalJournal journal1 in RPT_GenernalJournals_Objects)
            {
                bool flg_isDupplicate = false;
                foreach (RPT_GenernalJournal journal2 in RPT_GenernalJournals_Objects)
                {
                    if (journal1 == journal2)
                        continue;

                    RPT_GenernalJournal journal = new RPT_GenernalJournal();
                    if (journal1.AccountId == journal2.AccountId
                        && journal1.Credit == journal2.Credit)
                    {
                        journal.AccountId = journal1.AccountId;
                        journal.Credit = journal1.Credit + journal2.Credit;
                        journal.Debit = journal1.Debit;
                        journal.CurrencyId = journal1.CurrencyId;
                        rs.Add(journal);
                        flg_isDupplicate = true;
                    }
                }

                if (!flg_isDupplicate)
                {
                    rs.Add(journal1);
                }
            }
            return rs;
        }

        public List<RPT_GenernalJournal> GetProcessedJournalForReport()
        {
            List<RPT_GenernalJournal> rs = new List<RPT_GenernalJournal>();
            rs = ProcessingDupplicateCredit();
            rs = ProcessingDupplicateDebit();
            return rs;
        }

        public abstract void LoadInventoryCommandReport(Guid InventoryCommandId);
    }
}
