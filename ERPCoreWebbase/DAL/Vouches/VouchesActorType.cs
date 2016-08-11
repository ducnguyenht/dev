using System;
using DevExpress.Xpo;
using NAS.DAL.Invoice;
namespace NAS.DAL.Vouches
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class VouchesActorType: XPCustomObject
    {
        public VouchesActorType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        Guid fVouchesActorTypeId;
        [Key(true)]
        public Guid VouchesActorTypeId
        {
            get { return fVouchesActorTypeId; }
            set { SetPropertyValue<Guid>("VouchesActorTypeId", ref fVouchesActorTypeId, value); }
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
        [Association(@"VouchesActorReferencesVouchesActorType", typeof(VouchesActor))]
        public XPCollection<VouchesActor> VouchesActors { get { return GetCollection<VouchesActor>("VouchesActors"); } }

        #region Logic
        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Insert default vouches actor type
                if (!Util.isExistXpoObject<VouchesActorType>("Name", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    VouchesActorType vouchesActorType = new VouchesActorType(session)
                    {
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    vouchesActorType.Save();
                }
                //Insert director actor type
                if (!Util.isExistXpoObject<VouchesActorType>("Name", "DIRECTOR"))
                {
                    VouchesActorType vouchesActorType = new VouchesActorType(session)
                    {
                        Name = "DIRECTOR",
                        Description = "Giám đốc",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    vouchesActorType.Save();
                }
                //Insert Chief accountant actor type
                if (!Util.isExistXpoObject<VouchesActorType>("Name", "CHIEF_ACCOUNTANT"))
                {
                    VouchesActorType vouchesActorType = new VouchesActorType(session)
                    {
                        Name = "CHIEF_ACCOUNTANT",
                        Description = "Kế toán trưởng",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    vouchesActorType.Save();
                }
                //Insert Payer actor type
                if (!Util.isExistXpoObject<VouchesActorType>("Name", "PAYER"))
                {
                    VouchesActorType vouchesActorType = new VouchesActorType(session)
                    {
                        Name = "PAYER",
                        Description = "Người nộp tiền",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    vouchesActorType.Save();
                }
                //Insert Slip maker actor type
                if (!Util.isExistXpoObject<VouchesActorType>("Name", "SLIP_MAKER"))
                {
                    VouchesActorType vouchesActorType = new VouchesActorType(session)
                    {
                        Name = "SLIP_MAKER",
                        Description = "Người lập phiếu",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    vouchesActorType.Save();
                }
                //Insert Cashier actor type
                if (!Util.isExistXpoObject<VouchesActorType>("Name", "CASHIER"))
                {
                    VouchesActorType vouchesActorType = new VouchesActorType(session)
                    {
                        Name = "CASHIER",
                        Description = "Thủ quỹ",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    vouchesActorType.Save();
                }
                //Insert Payee actor type
                if (!Util.isExistXpoObject<VouchesActorType>("Name", "PAYEE"))
                {
                    VouchesActorType vouchesActorType = new VouchesActorType(session)
                    {
                        Name = "PAYEE",
                        Description = "Người nhận tiền",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    vouchesActorType.Save();
                }
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw;
            }
            finally
            {
                session.Dispose();
            }
        }
        #endregion
    }

}
