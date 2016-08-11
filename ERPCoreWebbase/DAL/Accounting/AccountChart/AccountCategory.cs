using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Accounting.AccountChart
{
    //2013-12-13 Khoa.Truong INS START
    public enum DefaultAccountCategoryEnum
    {
        NAAN_DEFAULT,
        OFFBALANCE,
        REVENUE,
        EXPENSE,
        NETINCOME,
        ASSET,
        LIABILITY,
        EQUITY
    }
    //2013-12-13 Khoa.Truong INS END

    public partial class AccountCategory
    {
        public AccountCategory(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region Logic

        //2013-12-13 Khoa.Truong INS START
        public static AccountCategory GetDefault(Session session, DefaultAccountCategoryEnum code)
        {
            AccountCategory ret = null;
            try
            {
                ret = session.FindObject<AccountCategory>(
                    new BinaryOperator("Code", Enum.GetName(typeof(DefaultAccountCategoryEnum), code)));
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //2013-12-13 Khoa.Truong INS END

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //2013-12-13 Khoa.Truong INS START
                if (!Util.isExistXpoObject<AccountCategory>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    AccountCategory accountCategory = new AccountCategory(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                        Description = Utility.Constant.NAAN_DEFAULT_CODE,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };
                    accountCategory.Save();
                }
                //2013-12-13 Khoa.Truong INS START

                //insert default data into Organization table
                if (!Util.isExistXpoObject<AccountCategory>("Code", "OFFBALANCE"))
                {                                     
                    AccountCategory accountCategory = new AccountCategory(session)
                    {
                        Code = "OFFBALANCE",                        
                        Description = "Ngoại bảng",
                        RowStatus = +1                        
                    };
                    accountCategory.Save();
                }
                if (!Util.isExistXpoObject<AccountCategory>("Code", "REVENUE"))
                {
                    AccountCategory accountCategory = new AccountCategory(session)
                    {
                        Code = "REVENUE",
                        Description = "Doanh thu",
                        RowStatus = +1
                    };
                    accountCategory.Save();
                }
                if (!Util.isExistXpoObject<AccountCategory>("Code", "EXPENSE"))
                {
                    AccountCategory accountCategory = new AccountCategory(session)
                    {
                        Code = "EXPENSE",
                        Description = "Chi phí",
                        RowStatus = +1
                    };
                    accountCategory.Save();
                }
                if (!Util.isExistXpoObject<AccountCategory>("Code", "NETINCOME"))
                {
                    AccountCategory accountCategory = new AccountCategory(session)
                    {
                        Code = "NETINCOME",
                        Description = "Kết quả kinh doanh",
                        RowStatus = +1
                    };
                    accountCategory.Save();
                }
                if (!Util.isExistXpoObject<AccountCategory>("Code", "ASSET"))
                {
                    AccountCategory accountCategory = new AccountCategory(session)
                    {
                        Code = "ASSET",
                        Description = "Tài sản",
                        RowStatus = +1
                    };
                    accountCategory.Save();
                }
                if (!Util.isExistXpoObject<AccountCategory>("Code", "LIABILITY"))
                {
                    AccountCategory accountCategory = new AccountCategory(session)
                    {
                        Code = "LIABILITY",
                        Description = "Nợ phải trả",
                        RowStatus = +1
                    };
                    accountCategory.Save();
                }
                if (!Util.isExistXpoObject<AccountCategory>("Code", "EQUITY"))
                {
                    AccountCategory accountCategory = new AccountCategory(session)
                    {
                        Code = "EQUITY",
                        Description = "Vốn chủ sở hữu",
                        RowStatus = +1
                    };
                    accountCategory.Save();
                }
            }
            catch (Exception)
            {
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
