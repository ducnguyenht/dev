using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting;

namespace NAS.DAL.BI.Actor
{
    public class PersonDim:XPCustomObject
    {
        public PersonDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        #endregion

        #region Properties
        int fPersonDimId;
        [Key(true)]
        public int PersonDimId
        {
            get { return fPersonDimId; }
            set { SetPropertyValue<int>("PersonDimId", ref fPersonDimId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fDescription;
        public string Description
        {
            get {return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        #endregion
    }
}
