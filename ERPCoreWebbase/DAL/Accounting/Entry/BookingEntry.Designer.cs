using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;

namespace NAS.DAL.Accounting.Entry
{
    public partial class BookingEntry : XPCustomObject
    {
        Guid fBookingEntryId;
        [Key(true)]
        public Guid BookingEntryId
        {
            get { return fBookingEntryId; }
            set { SetPropertyValue<Guid>("BookingEntryId", ref fBookingEntryId, value); }
        }

        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue<DateTime>("CreateDate", ref fCreateDate, value); }
        }

        DateTime fIssueDate;
        public DateTime IssueDate
        {
            get { return fIssueDate; }
            set { SetPropertyValue<DateTime>("IssueDate", ref fIssueDate, value); }
        }

        DateTime fUpdateDate;
        public DateTime UpdateDate
        {
            get { return fUpdateDate; }
            set { SetPropertyValue<DateTime>("UpdateDate", ref fUpdateDate, value); }
        }
        
        string fCode;
        [Size(36)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        string fDescription;
        [Size(1024)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        string fName;
        [Size(255)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        [Association(@"BookingEntryReferencesBookingEntryTransaction", typeof(BookingEntryTransaction))]
        public XPCollection<BookingEntryTransaction> BookingEntryTransactions { get { return GetCollection<BookingEntryTransaction>("BookingEntryTransactions"); } } 
    }
}
