using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Organization
{

    public partial class LoginAccount
    {
        public LoginAccount(Session session) : base(session) { }
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

        public int Populate()
        {
            return 1;
        }
        #endregion
    }

}
