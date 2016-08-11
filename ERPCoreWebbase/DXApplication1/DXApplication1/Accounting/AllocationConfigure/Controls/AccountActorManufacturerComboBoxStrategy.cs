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
    public class AccountActorManufacturerComboBoxStrategy : AccountActorComboBoxStrategy
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
                ManufacturerOrg manufacturerOrg = session.GetObjectByKey<ManufacturerOrg>(selectedId);
                if (manufacturerOrg == null)
                {
                    return null;
                }
                AccountActor ret = new AccountActor()
                {
                    AccountActorTypeId =
                        session.FindObject<AccountActorType>(new BinaryOperator("Code",
                            Enum.GetName(typeof(AccountActorTypeEnum),
                                AccountActorTypeEnum.MANUFACTURER))).AccountActorTypeId,
                    Code = manufacturerOrg.Code,
                    Description = manufacturerOrg.Description,
                    Name = manufacturerOrg.Name,
                    RefId = manufacturerOrg.OrganizationId
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
            ManufacturerOrg obj = session.GetObjectByKey<ManufacturerOrg>(e.Value);
            if (obj != null)
            {
                combo.DataSource = new ManufacturerOrg[] { obj };
                combo.DataBindItems();
            }
        }

        public void ItemsRequestedByFilterCondition(Session session, object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<ManufacturerOrg> collection = new XPCollection<ManufacturerOrg>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                //row status is active
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                CriteriaOperator.Or(
                //find code contains the filter
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                //find name contains the filter
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
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
            combo.Columns.Add("Code", "Mã nhà sản xuất");
            combo.Columns.Add("Name", "Tên nhà sản xuất");
        }
    }
}