using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Web.ASPxEditors;
using DevExpress.Xpo;
using NAS.DAL.Staging.Accounting.Journal;
using DevExpress.Data.Filtering;
using NAS.DAL;

namespace WebModule.Accounting.AllocationConfigure.Controls
{
    public class AccountActorCustomerComboBoxStrategy : AccountActorComboBoxStrategy
    {
        public AccountActor GetSelectedItem(object source)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                ASPxComboBox combo = source as ASPxComboBox;
                //Get selected manufacturer
                if (combo.Value == null)
                {
                    return null;
                }
                Guid selectedId = (Guid)combo.Value;
                Organization organization = session.GetObjectByKey<Organization>(selectedId);
                if (organization == null)
                {
                    return null;
                }
                AccountActor ret = new AccountActor()
                {
                    AccountActorTypeId =
                        session.FindObject<AccountActorType>(new BinaryOperator("Code",
                            Enum.GetName(typeof(AccountActorTypeEnum),
                                AccountActorTypeEnum.CUSTOMER))).AccountActorTypeId,
                    Code = organization.Code,
                    Description = organization.Description,
                    Name = organization.Name,
                    RefId = organization.OrganizationId
                };
                return ret;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public void ItemRequestedByValue(Session session, object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            Organization obj = session.GetObjectByKey<Organization>(e.Value);
            if (obj != null)
            {
                combo.DataSource = new Organization[] { obj };
                combo.DataBindItems();
            }
        }

        public void ItemsRequestedByFilterCondition(Session session, object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<Organization> collection = new XPCollection<Organization>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            //Get CUSTOMER trading type
            TradingCategory customerTradingCategory =
                session.FindObject<TradingCategory>(new BinaryOperator("Code", "CUSTOMER"));

            CriteriaOperator criteria = CriteriaOperator.And(
                //row status is active
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                CriteriaOperator.Or(
                //find code contains the filter
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                //find name contains the filter
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                ),
                //find customer and supplier
                new ContainsOperator("OrganizationCategories",
                    CriteriaOperator.And(
                        new BinaryOperator("TradingCategoryId", customerTradingCategory),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                    )
                )
            );

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            combo.DataSource = collection;
            combo.DataBindItems();
        }

        public void Init(object source)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            combo.TextField = "Name";
            combo.TextFormatString = "{0} - {1}";
            combo.ValueField = "OrganizationId";
            combo.ValueType = typeof(System.Guid);
            combo.Columns.Clear();
            combo.Columns.Add("Code", "Mã khách hàng");
            combo.Columns.Add("Name", "Tên khách hàng");
        }
    }
}