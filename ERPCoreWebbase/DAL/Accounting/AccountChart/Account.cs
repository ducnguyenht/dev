using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Accounting.AccountChart
{
    public enum DefaultAccountEnum
    {
        /// <summary>
        /// Mặc định
        /// </summary>
        NAAN_DEFAULT,
        /// <summary>
        /// Hàng trên đường
        /// </summary>
        ON_THE_WAY,
        /// <summary>
        /// Hàng trong kho chủ sở hữu
        /// </summary>
        OWNER_INVENTORY,
        /// <summary>
        /// Hàng trong kho nhà cung cấp
        /// </summary>
        SUPPLIER_INVENTORY,
        /// <summary>
        /// Hàng trong kho khách hàng
        /// </summary>
        CUSTOMER_INVENTORY
    }

    public partial class Account
    {
        public Account(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }   

        #region Logic

        public static Account GetDefault(Session session, DefaultAccountEnum code)
        {
            Account ret = null;
            try
            {
                ret = session.FindObject<Account>(
                    new BinaryOperator("Code", Enum.GetName(typeof(DefaultAccountEnum), code)));
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Populate()
        {               
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                Organization.Populate();
                AccountType.Populate();
                //insert default data into Account table
                if (!Util.isExistXpoObject<Account>("Code", 
                    Enum.GetName(typeof(DefaultAccountEnum), DefaultAccountEnum.NAAN_DEFAULT)))
                {
                    AccountType accountType =
                        AccountType.GetDefault(session, DefaultAccountTypeEnum.NAAN_DEFAULT);
                    Organization organization =
                        session.FindObject<Organization>(new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE));
                    Account accounting= new Account(session)
                    {
                        Code = Enum.GetName(typeof(DefaultAccountEnum), DefaultAccountEnum.NAAN_DEFAULT),
                        Name = "N/A",
                        Level = 0,
                        AccountTypeId = accountType, 
                        OrganizationId = organization,
                        Description = "N/A",
                        BalanceType = 0,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT                        
                    };
                    accounting.Save();
                }
                //2013-12-13 Khoa.Truong INS START
                if (!Util.isExistXpoObject<Account>("Code", 
                    Enum.GetName(typeof(DefaultAccountEnum), DefaultAccountEnum.CUSTOMER_INVENTORY)))
                {
                    AccountType accountType =
                        AccountType.GetDefault(session, DefaultAccountTypeEnum.NAAN_DEFAULT);
                    Organization organization =
                        session.FindObject<Organization>(new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE));
                    Account accounting = new Account(session)
                    {
                        Code = Enum.GetName(typeof(DefaultAccountEnum), DefaultAccountEnum.CUSTOMER_INVENTORY),
                        Name = "Hàng trong kho khách hàng",
                        Level = 0,
                        AccountTypeId = accountType,
                        OrganizationId = organization,
                        Description = "Hàng trong kho khách hàng",
                        BalanceType = 0,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };
                    accounting.Save();
                }
                //2013-12-13 Khoa.Truong INS END

                //2013-12-13 Khoa.Truong INS START
                if (!Util.isExistXpoObject<Account>("Code",
                    Enum.GetName(typeof(DefaultAccountEnum), DefaultAccountEnum.ON_THE_WAY)))
                {
                    AccountType accountType =
                        AccountType.GetDefault(session, DefaultAccountTypeEnum.NAAN_DEFAULT);
                    Organization organization =
                        session.FindObject<Organization>(new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE));
                    Account accounting = new Account(session)
                    {
                        Code = Enum.GetName(typeof(DefaultAccountEnum), DefaultAccountEnum.ON_THE_WAY),
                        Name = "Hàng trên đường",
                        Level = 0,
                        AccountTypeId = accountType,
                        OrganizationId = organization,
                        Description = "Hàng trên đường",
                        BalanceType = 0,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };
                    accounting.Save();
                }
                //2013-12-13 Khoa.Truong INS END

                //2013-12-13 Khoa.Truong INS START
                if (!Util.isExistXpoObject<Account>("Code",
                    Enum.GetName(typeof(DefaultAccountEnum), DefaultAccountEnum.OWNER_INVENTORY)))
                {
                    AccountType accountType =
                        AccountType.GetDefault(session, DefaultAccountTypeEnum.NAAN_DEFAULT);
                    Organization organization =
                        session.FindObject<Organization>(new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE));
                    Account accounting = new Account(session)
                    {
                        Code = Enum.GetName(typeof(DefaultAccountEnum), DefaultAccountEnum.OWNER_INVENTORY),
                        Name = "Hàng trong kho chủ sở hữu",
                        Level = 0,
                        AccountTypeId = accountType,
                        OrganizationId = organization,
                        Description = "Hàng trong kho chủ sở hữu",
                        BalanceType = 0,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };
                    accounting.Save();
                }
                //2013-12-13 Khoa.Truong INS END

                //2013-12-13 Khoa.Truong INS START
                if (!Util.isExistXpoObject<Account>("Code",
                    Enum.GetName(typeof(DefaultAccountEnum), DefaultAccountEnum.SUPPLIER_INVENTORY)))
                {
                    AccountType accountType =
                        AccountType.GetDefault(session, DefaultAccountTypeEnum.NAAN_DEFAULT);
                    Organization organization =
                        session.FindObject<Organization>(new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE));
                    Account accounting = new Account(session)
                    {
                        Code = Enum.GetName(typeof(DefaultAccountEnum), DefaultAccountEnum.SUPPLIER_INVENTORY),
                        Name = "Hàng trong kho nhà cung cấp",
                        Level = 0,
                        AccountTypeId = accountType,
                        OrganizationId = organization,
                        Description = "Hàng trong kho nhà cung cấp",
                        BalanceType = 0,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };
                    accounting.Save();
                }
                //2013-12-13 Khoa.Truong INS END
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
