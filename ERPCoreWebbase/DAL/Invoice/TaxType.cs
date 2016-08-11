using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Invoice
{
    public partial class TaxType
    {
        public TaxType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Unit table
                if (!Util.isExistXpoObject<TaxType>("Code", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    TaxType taxType = new TaxType(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_NAME,
                        fName = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    taxType.Save();
                }

                //insert VAT tax type
                //if (!Util.isExistXpoObject<TaxType>("Code", "GTGT"))
                if (!Util.isExistXpoObject<TaxType>("Code", "VAT"))
                {
                    TaxType taxType = new TaxType(session)
                    {
                        //Code = "GTGT",
                        Code = "VAT",
                        Name = @"Nhóm thuế giá trị gia tăng",
                        RowStatus = 1,
                        IsInternal = true,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    taxType.Save();
                }

                //insert VAT tax type
                if (!Util.isExistXpoObject<TaxType>("Code", "TN"))
                {
                    TaxType taxType = new TaxType(session)
                    {
                        Code = "TN",
                        Name = @"Nhóm thuế tài nguyên",
                        RowStatus = 1,
                        IsInternal = true,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    taxType.Save();
                }

                if (!Util.isExistXpoObject<TaxType>("Code", "MT"))
                {
                    TaxType taxType = new TaxType(session)
                    {
                        Code = "MT",
                        Name = @"Nhóm thuế môi trường",
                        RowStatus = 1,
                        IsInternal = true,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    taxType.Save();
                }

                if (!Util.isExistXpoObject<TaxType>("Code", "TTDB"))
                {
                    TaxType taxType = new TaxType(session)
                    {
                        Code = "TTDB",
                        Name = @"Nhóm thuế tiêu thụ đặc biệt",
                        RowStatus = 1,
                        IsInternal = true,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    taxType.Save();
                }
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        #endregion

    }

}
