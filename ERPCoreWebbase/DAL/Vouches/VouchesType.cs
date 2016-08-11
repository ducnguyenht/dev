using System;
using DevExpress.Xpo;
using NAS.DAL.Invoice;
namespace NAS.DAL.Vouches
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class VouchesType: XPCustomObject
    {
        public VouchesType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        Guid fVouchesTypeId;
        [Key(true)]
        public Guid VouchesTypeId
        {
            get { return fVouchesTypeId; }
            set { SetPropertyValue<Guid>("VouchesTypeId", ref fVouchesTypeId, value); }
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
        [Association(@"VouchesReferencesVouchesType", typeof(Vouches))]
        public XPCollection<Vouches> Vouchess { get { return GetCollection<Vouches>("Vouchess"); } }

        #region Logic
        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                if (!Util.isExistXpoObject<VouchesType>("Name", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    VouchesType unit = new VouchesType(session)
                    {
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        RowStatus = -1,
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
