using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Vouches
{

    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class ReceiptVouchesType : VouchesType
    {
        public ReceiptVouchesType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region Logic
        new public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                if (!Util.isExistXpoObject<ReceiptVouchesType>("Name", "SALES_GOODS"))
                {
                    ReceiptVouchesType unit = new ReceiptVouchesType(session)
                    {
                        Name = "SALES_GOODS",
                        Description = "Thu tiền hàng",
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
