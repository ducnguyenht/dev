using System;
using DevExpress.Xpo;
using NAS.DAL.Invoice;
namespace NAS.DAL.Vouches
{
    public partial class ForeignCurrency : XPCustomObject
    {
        public ForeignCurrency(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        Guid fForeignCurrencyId;
        [Key(true)]
        public Guid ForeignCurrencyId
        {
            get { return fForeignCurrencyId; }
            set { SetPropertyValue<Guid>("ForeignCurrencyId", ref fForeignCurrencyId, value); }
        }
        string fCode;
        [Size(36)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        string fName;
        [Size(255)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fDescription;
        [Size(1024)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        DateTime fRowCreationTimeStamp;
        public DateTime RowCreationTimeStamp
        {
            get { return fRowCreationTimeStamp; }
            set { SetPropertyValue<DateTime>("RowCreationTimeStamp", ref fRowCreationTimeStamp, value); }
        }
        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        /*2014-01-17 ERP-1411 Khoa.Truong DEL START*/
        //[Association(@"VouchesAmountReferencesForeignCurrency", typeof(VouchesAmount))]
        //public XPCollection<VouchesAmount> VouchesAmounts { get { return GetCollection<VouchesAmount>("VouchesAmounts"); } }
        /*2014-01-17 ERP-1411 Khoa.Truong DEL END*/

        [Association(@"ExchangeRagteReferencesForeignCurrency", typeof(ExchangeRate))]
        public XPCollection<ExchangeRate> ExchangeRates { get { return GetCollection<ExchangeRate>("ExchangeRates"); } }
        #region Logic
        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                if (!Util.isExistXpoObject<ForeignCurrency>("Name", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    ForeignCurrency unit = new ForeignCurrency(session)
                    {
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    unit.Save();
                }
                if (!Util.isExistXpoObject<ForeignCurrency>("Name", "VNĐ"))
                {
                    ForeignCurrency unit = new ForeignCurrency(session)
                    {
                        Name = "VNĐ",
                        Description = "Việt Nam đồng",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    unit.Save();
                }
                if (!Util.isExistXpoObject<ForeignCurrency>("Name", "USD"))
                {
                    ForeignCurrency unit = new ForeignCurrency(session)
                    {
                        Name = "USD",
                        Description = "Đô la Mỹ",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    unit.Save();
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }

}
