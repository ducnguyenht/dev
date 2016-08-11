using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.BI.Accounting.Account
{
    public class AccountingPeriodDim : XPCustomObject
    {
        public AccountingPeriodDim(Session session)
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

        // Fields...
        private string _Code;
        private string _Description;
        private short _RowStatus;
        private int _AccountingPeriodDimId;
        private DateTime _FromDateTime;
        private DateTime _ToDateTime;
        private Guid _RefId;
        [Key(true)]
        public int AccountingPeriodDimId
        {
            get { return _AccountingPeriodDimId; }
            set { SetPropertyValue("AccountingPeriodDimId", ref _AccountingPeriodDimId, value); }
        }
        //2.Description - string
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue("Description", ref _Description, value); }
        }
        //4.RowStatus - short
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue("RowStatus", ref _RowStatus, value); }
        }
        public string Code
        {
            get { return _Code; }
            set { SetPropertyValue("Code", ref _Code, value); }
        }

        public DateTime FromDateTime
        {
            get { return _FromDateTime; }
            set { SetPropertyValue("FromDateTime", ref _FromDateTime, value); }
        }

        public DateTime ToDateTime
        {
            get { return _ToDateTime; }
            set { SetPropertyValue("ToDateTime", ref _ToDateTime, value); }
        }

        public Guid RefId
        {
            get { return _RefId; }
            set { SetPropertyValue("RefId", ref _RefId, value); }
        }
    }
}
