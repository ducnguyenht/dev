using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Entry;

namespace NAS.DAL.Accounting.Journal
{    
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class BookingEntryTransaction : Transaction
    {
        // Fields...
        BookingEntry fBookingEntryId;
        [Association(@"BookingEntryReferencesBookingEntryTransaction")]
        public BookingEntry BookingEntryId
        {
            get
            {
                return fBookingEntryId;
            }
            set
            {
                SetPropertyValue<BookingEntry>("BookingEntryId", ref fBookingEntryId, value);
            }
        }
    }
}
