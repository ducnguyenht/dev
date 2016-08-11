using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL;

namespace WebModule
{
    public partial class LoginAccountPopulation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void Populate()
        {
            DevExpress.Xpo.UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                if (!Util.isExistXpoObject<Person>("Code", "ADM1"))
                {
                    Person person = new Person(uow)
                    {
                        Code = "ADM1",
                        Name = "Admin1",
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    LoginAccount loginAccount = new LoginAccount(uow)
                    {
                        Email = "snl.qn.adm@gmail.com",
                        RowCreationTimeStamp = DateTime.Now,
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                        PersonId = person
                    };
                }
                uow.CommitChanges();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
            }
        }

    }
}