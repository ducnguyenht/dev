using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Vouches
{

    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class PaymentVouchesType : VouchesType
    {
        public PaymentVouchesType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region Logic
        new public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                if (!Util.isExistXpoObject<PaymentVouchesType>("Name", "PAYMENT_GOODS"))
                {
                    PaymentVouchesType unit = new PaymentVouchesType(session)
                    {
                        Name = "PAYMENT_GOODS",
                        Description = "Trả tiền hàng",
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
