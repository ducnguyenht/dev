using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Organization
{

    public partial class AuthenticationProvider
    {
        public AuthenticationProvider(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region
        public int CheckUniqueName()
        {
            return 1;
        }

        public int CheckConstraint()
        {
            return 1;
        }

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into AuthenticationProvider table
                if (!Util.isExistXpoObject<AuthenticationProvider>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    AuthenticationProvider authenticationProvider = new AuthenticationProvider(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    authenticationProvider.Save();
                }
                //insert NAAN_ID provider
                if (!Util.isExistXpoObject<AuthenticationProvider>("Code", "NAAN_ID"))
                {
                    AuthenticationProvider authenticationProvider = new AuthenticationProvider(session)
                    {
                        Code = "NAAN_ID",
                        Name = "NAAN_ID",
                        Description = "NAAN ID",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    authenticationProvider.Save();
                }

                //insert GOOGLE provider
                if (!Util.isExistXpoObject<AuthenticationProvider>("Code", "GOOGLE"))
                {
                    AuthenticationProvider authenticationProvider = new AuthenticationProvider(session)
                    {
                        Code = "GOOGLE",
                        Name = "GOOGLE",
                        Description = "Google",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    authenticationProvider.Save();
                }

                //insert YAHOO provider
                if (!Util.isExistXpoObject<AuthenticationProvider>("Code", "YAHOO"))
                {
                    AuthenticationProvider authenticationProvider = new AuthenticationProvider(session)
                    {
                        Code = "YAHOO",
                        Name = "YAHOO",
                        Description = "Yahoo",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    authenticationProvider.Save();
                }

                //insert FACEBOOK provider
                if (!Util.isExistXpoObject<AuthenticationProvider>("Code", "FACEBOOK"))
                {
                    AuthenticationProvider authenticationProvider = new AuthenticationProvider(session)
                    {
                        Code = "FACEBOOK",
                        Name = "FACEBOOK",
                        Description = "Facebook",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    authenticationProvider.Save();
                }

                //insert TWITTER provider
                if (!Util.isExistXpoObject<AuthenticationProvider>("Code", "TWITTER"))
                {
                    AuthenticationProvider authenticationProvider = new AuthenticationProvider(session)
                    {
                        Code = "TWITTER",
                        Name = "TWITTER",
                        Description = "Twitter",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    authenticationProvider.Save();
                }

                //insert LINKIN provider
                if (!Util.isExistXpoObject<AuthenticationProvider>("Code", "LINKIN"))
                {
                    AuthenticationProvider authenticationProvider = new AuthenticationProvider(session)
                    {
                        Code = "LINKIN",
                        Name = "LINKIN",
                        Description = "LinkIn",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    authenticationProvider.Save();
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
