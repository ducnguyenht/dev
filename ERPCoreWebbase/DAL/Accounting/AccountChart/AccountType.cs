using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Accounting.AccountChart
{
    //2013-12-13 Khoa.Truong INS START
    public enum DefaultAccountTypeEnum
    {
        NAAN_DEFAULT,
        TK0,
        TK1,
        TK2,
        TK3,
        TK4,
        TK5,
        TK6,
        TK7,
        TK8,
        TK9
    }
    //2013-12-13 Khoa.Truong INS END

    public partial class AccountType
    {
        public AccountType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region Logic
        //2013-12-13 Khoa.Truong INS START
        public static AccountType GetDefault(Session session, DefaultAccountTypeEnum code)
        {
            AccountType ret = null;
            try
            {
                ret = session.FindObject<AccountType>(
                    new BinaryOperator("Code", Enum.GetName(typeof(DefaultAccountTypeEnum), code)));
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
                if (!Util.isExistXpoObject<AccountType>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    AccountCategory.Populate();
                    AccountCategory accountcategory =
                        AccountCategory.GetDefault(session, DefaultAccountCategoryEnum.NAAN_DEFAULT);
                    AccountType accountType = new AccountType(session)
                    {
                        AccountCategoryId = accountcategory,
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                        Name = Utility.Constant.NAAN_DEFAULT_CODE,
                        Description = Utility.Constant.NAAN_DEFAULT_CODE,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };
                    accountType.Save();
                }
                //2013-12-13 Khoa.Truong INS END

                //insert default data into OrganizationTypeBO table
                if (!Util.isExistXpoObject<AccountType>("Code", "TK0"))
                {
                    XPQuery<AccountCategory> accountcategoryQuery = session.Query<AccountCategory>();
                    AccountCategory.Populate();
                    AccountCategory accountcategory =
                    accountcategoryQuery.Where(r => r.Code == "OFFBALANCE").FirstOrDefault();
                    AccountType accountType = new AccountType(session)
                    {
                        AccountCategoryId = accountcategory,
                        Code = "TK0",
                        Name = "TÀI KHOẢN NGOẠI BẢNG",
                        Description = "Chi tiết theo yêu cầu quản lý",
                        RowStatus = 1,                        
                    };
                    accountType.Save();
                }

                if (!Util.isExistXpoObject<AccountType>("Code", "TK1"))
                {
                    XPQuery<AccountCategory> accountcategoryQuery = session.Query<AccountCategory>();
                    AccountCategory.Populate();
                    AccountCategory accountcategory =
                    accountcategoryQuery.Where(r => r.Code == "ASSET").FirstOrDefault();
                    AccountType accountType = new AccountType(session)
                    {
                        AccountCategoryId = accountcategory,
                        Code = "TK1",
                        Name = "TÀI SẢN LƯU ĐỘNG",
                        Description = "",
                        RowStatus = 1,
                    };
                    accountType.Save();
                }

                if (!Util.isExistXpoObject<AccountType>("Code", "TK2"))
                {
                    XPQuery<AccountCategory> accountcategoryQuery = session.Query<AccountCategory>();
                    AccountCategory.Populate();
                    AccountCategory accountcategory =
                    accountcategoryQuery.Where(r => r.Code == "ASSET").FirstOrDefault();
                    AccountType accountType = new AccountType(session)
                    {
                        AccountCategoryId = accountcategory,
                        Code = "TK2",
                        Name = "TÀI SẢN CỐ ĐỊNH",
                        Description = "",
                        RowStatus = 1,
                    };
                    accountType.Save();
                }

                if (!Util.isExistXpoObject<AccountType>("Code", "TK3"))
                {
                    XPQuery<AccountCategory> accountcategoryQuery = session.Query<AccountCategory>();
                    AccountCategory.Populate();
                    AccountCategory accountcategory =
                    accountcategoryQuery.Where(r => r.Code == "LIABILITY").FirstOrDefault();
                    AccountType accountType = new AccountType(session)
                    {
                        AccountCategoryId = accountcategory,
                        Code = "TK3",
                        Name = "NỢ PHẢI TRẢ",
                        Description = "",
                        RowStatus = 1,
                    };
                    accountType.Save();
                }

                if (!Util.isExistXpoObject<AccountType>("Code", "TK4"))
                {
                    XPQuery<AccountCategory> accountcategoryQuery = session.Query<AccountCategory>();
                    AccountCategory.Populate();
                    AccountCategory accountcategory =
                    accountcategoryQuery.Where(r => r.Code == "EQUITY").FirstOrDefault();
                    AccountType accountType = new AccountType(session)
                    {
                        AccountCategoryId = accountcategory,
                        Code = "TK4",
                        Name = "NGUỐN VỐN CHỦ SỞ HỮU",
                        Description = "",
                        RowStatus = 1,
                    };
                    accountType.Save();
                }

                if (!Util.isExistXpoObject<AccountType>("Code", "TK5"))
                {
                    XPQuery<AccountCategory> accountcategoryQuery = session.Query<AccountCategory>();
                    AccountCategory.Populate();
                    AccountCategory accountcategory =
                    accountcategoryQuery.Where(r => r.Code == "REVENUE").FirstOrDefault();
                    AccountType accountType = new AccountType(session)
                    {
                        AccountCategoryId = accountcategory,
                        Code = "TK5",
                        Name = "DOANH THU",
                        Description = "",
                        RowStatus = 1,
                    };
                    accountType.Save();
                }

                if (!Util.isExistXpoObject<AccountType>("Code", "TK6"))
                {
                    XPQuery<AccountCategory> accountcategoryQuery = session.Query<AccountCategory>();
                    AccountCategory.Populate();
                    AccountCategory accountcategory =
                    accountcategoryQuery.Where(r => r.Code == "EXPENSE").FirstOrDefault();
                    AccountType accountType = new AccountType(session)
                    {
                        AccountCategoryId = accountcategory,
                        Code = "TK6",
                        Name = "CHI PHÍ SẢN XUẤT, KINH DOANH",
                        Description = "",
                        RowStatus = 1,
                    };
                    accountType.Save();
                }

                if (!Util.isExistXpoObject<AccountType>("Code", "TK7"))
                {
                    XPQuery<AccountCategory> accountcategoryQuery = session.Query<AccountCategory>();
                    AccountCategory.Populate();
                    AccountCategory accountcategory =
                    accountcategoryQuery.Where(r => r.Code == "REVENUE").FirstOrDefault();
                    AccountType accountType = new AccountType(session)
                    {
                        AccountCategoryId = accountcategory,
                        Code = "TK7",
                        Name = "THU NHẬP KHÁC",
                        Description = "",
                        RowStatus = 1,
                    };
                    accountType.Save();
                }

                if (!Util.isExistXpoObject<AccountType>("Code", "TK8"))
                {
                    XPQuery<AccountCategory> accountcategoryQuery = session.Query<AccountCategory>();
                    AccountCategory.Populate();
                    AccountCategory accountcategory =
                    accountcategoryQuery.Where(r => r.Code == "EXPENSE").FirstOrDefault();
                    AccountType accountType = new AccountType(session)
                    {
                        AccountCategoryId = accountcategory,
                        Code = "TK8",
                        Name = "CHI PHÍ KHÁC",
                        Description = "",
                        RowStatus = 1,
                    };
                    accountType.Save();
                }

                if (!Util.isExistXpoObject<AccountType>("Code", "TK9"))
                {
                    XPQuery<AccountCategory> accountcategoryQuery = session.Query<AccountCategory>();
                    AccountCategory.Populate();
                    AccountCategory accountcategory =
                    accountcategoryQuery.Where(r => r.Code == "NETINCOME").FirstOrDefault();
                    AccountType accountType = new AccountType(session)
                    {
                        AccountCategoryId = accountcategory,
                        Code = "TK9",
                        Name = "XÁC ĐỊNH KẾT QUẢ KINH DOANH",
                        Description = "",
                        RowStatus = 1,
                    };
                    accountType.Save();
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
