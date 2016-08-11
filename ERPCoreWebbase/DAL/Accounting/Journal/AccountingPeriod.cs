using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Accounting.AccountChart;
using Utility;

namespace NAS.DAL.Accounting.Journal
{
    public partial class AccountingPeriod
    {
        public AccountingPeriod(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Person table
                if (!Util.isExistXpoObject<AccountingPeriod>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    XPQuery<Organization> organizationQuery = session.Query<Organization>();
                    XPQuery<AccountType> AccountTypeQuery = session.Query<AccountType>();
                    Organization.Populate();
                    AccountType.Populate();
                    AccountType accountType =
                        AccountTypeQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();
                    Organization organization =
                        organizationQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();
                    AccountingPeriod accountingPeriod = new AccountingPeriod(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                        OrganizationId = organization,
                        Description = "",
                        fIsActive = false,
                        fFromDateTime = DateTime.Now,
                        fToDateTime = DateTime.Now,
                        RowStatus = Constant.ROWSTATUS_DEFAULT
                    };
                    accountingPeriod.Save();
                }


                if (!Util.isExistXpoObject<AccountingPeriod>("AccountingPeriodId", "5eaa2bf5-34ba-47c6-88df-48ad25bc6e18"))
                {                                                                                        
                    XPQuery<Organization> organizationQuery = session.Query<Organization>();
                    XPQuery<AccountType> AccountTypeQuery = session.Query<AccountType>();
                    Organization.Populate();
                    AccountType.Populate();
                    AccountType accountType =
                        AccountTypeQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();
                    Organization organization =
                        organizationQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();
                    AccountingPeriod accountingPeriod = new AccountingPeriod(session)
                    {
                        AccountingPeriodId = Guid.Parse("5eaa2bf5-34ba-47c6-88df-48ad25bc6e18"),
                        Code = "MACDINH",
                        OrganizationId = organization,
                        Description = "Chu kì Mặc định",
                        fIsActive = true,
                        fFromDateTime = DateTime.Today,
                        fToDateTime = DateTime.Today.AddMonths(1),
                        RowStatus = Constant.ROWSTATUS_ACTIVE
                    };
                    accountingPeriod.Save();
                }

            }
            catch (Exception)
            {
                
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            this.FromDateTime.AddHours(-this.ToDateTime.Hour);
            this.FromDateTime.AddMinutes(-this.FromDateTime.Minute);
            this.FromDateTime.AddMilliseconds(-this.FromDateTime.Millisecond);

            this.ToDateTime.AddHours(23 - this.ToDateTime.Hour);
            this.ToDateTime.AddMinutes(59 - this.ToDateTime.Minute);
            this.ToDateTime.AddMilliseconds(59 - this.ToDateTime.Millisecond);            
        }        
        #endregion
    }
}
