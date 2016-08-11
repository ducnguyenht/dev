using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;
using NAS.DAL;
//using Evaluant.Calculator;

namespace WebModule
{
    public partial class PopulateCustomerSupplier : System.Web.UI.Page
    {
        Session session;
        protected void Page_Load(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            if (!IsPostBack)
                moveData(session);
        }

        private void moveData(Session session)
        {
            try
            {
                session.BeginTransaction();
                CriteriaOperator criteria = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
                XPCollection<SupplierOrg> sol = new XPCollection<SupplierOrg>(session);
                XPCollection<TradingCategory> tcl = new XPCollection<TradingCategory>(session, criteria);
                foreach (SupplierOrg so in sol)
                {
                    foreach (TradingCategory tc in tcl)
                    {
                        criteria = CriteriaOperator.And(
                            new BinaryOperator("OrganizationId", so, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                            new BinaryOperator("TradingCategoryId", tc, BinaryOperatorType.Greater)
                        );

                        OrganizationCategory oc = session.FindObject<OrganizationCategory>(criteria);
                        if (oc == null)
                        {
                            oc = new OrganizationCategory(session);
                            oc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                            oc.OrganizationId = so;
                            oc.TradingCategoryId = tc;
                            oc.Save();
                        }
                        else
                        {
                            oc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                            oc.Save();
                        }

                    }
                }

                XPCollection<CustomerOrg> col = new XPCollection<CustomerOrg>(session);
                foreach (CustomerOrg co in col)
                {
                    foreach (TradingCategory tc in tcl)
                    {
                        criteria = CriteriaOperator.And(
                            new BinaryOperator("OrganizationId", co, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                            new BinaryOperator("TradingCategoryId", tc, BinaryOperatorType.Greater)
                        );

                        OrganizationCategory oc = session.FindObject<OrganizationCategory>(criteria);
                        if (oc == null)
                        {
                            oc = new OrganizationCategory(session);
                            oc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                            oc.OrganizationId = co;
                            oc.TradingCategoryId = tc;
                            oc.Save();
                        }
                        else
                        {
                            oc.TradingCategoryId = tc;
                            oc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                            oc.Save();
                        }

                    }
                }
                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }
        }

    }
}