using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;

namespace NAS.BO.Nomenclature.Organization
{
    public class LoginAccountBO
    {
        public bool AuthenticateByOpenID(Session session, string openID, out LoginAccount loginAccount)
        {
            loginAccount = null;
            try
            {
                CriteriaOperator criteriaLoginAccount = 
                    CriteriaOperator.And(
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                        new BinaryOperator("Email", openID)
                    );
                loginAccount = session.FindObject<LoginAccount>(criteriaLoginAccount);
                if (loginAccount != null)
                {
                    if (loginAccount.PersonId.RowStatus.Equals(Utility.Constant.ROWSTATUS_ACTIVE))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                loginAccount = null;
                return false;
            }
        }
    }
}
