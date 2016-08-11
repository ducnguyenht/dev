using System;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.Journal
{

    public class ManualBookingTransaction : Transaction
    {
        public ManualBookingTransaction(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}