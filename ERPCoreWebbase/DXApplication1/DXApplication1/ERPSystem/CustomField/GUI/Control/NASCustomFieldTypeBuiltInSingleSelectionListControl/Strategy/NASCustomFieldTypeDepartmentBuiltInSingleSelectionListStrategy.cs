using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Web.ASPxEditors;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInSingleSelectionListControl.Strategy
{
    public class NASCustomFieldTypeDepartmentBuiltInSingleSelectionListStrategy : NASCustomFieldTypeBuiltInSingleSelectionListStrategy
    {
        public override NASCustomFieldPredefinitionData GetPredefinitionDataOfObject(Guid objectCustomFieldId)
        {
            NASCustomFieldPredefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                ObjectCustomFieldData objectCustomFieldData =
                    session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId).ObjectCustomFieldDatas.FirstOrDefault();
                if (objectCustomFieldData != null)
                {
                    PredefinitionData predefinitionData =
                        (PredefinitionData)objectCustomFieldData.CustomFieldDataId;

                    Department department =
                                session.GetObjectByKey<Department>(predefinitionData.RefId);

                    if (department != null)
                    {
                        ret = new NASCustomFieldPredefinitionData()
                        {
                            Code = department.Code,
                            Description = department.Description,
                            Name = department.Name,
                            PredefinitionType = Enum.GetName(typeof(SingleSelectionBuiltInTypeEnum),
                                SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_DEPARTMENT),
                            RefId = department.DepartmentId
                        };
                    }
                }
                return ret;
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

        public override NASCustomFieldPredefinitionData GetSelectedPredefinitionDataFromList(object source)
        {
            NASCustomFieldPredefinitionData ret = null;
            Session session = null;
            try
            {
                ASPxComboBox combo = source as ASPxComboBox;
                session = XpoHelper.GetNewSession();

                if (combo.Value == null)
                {
                    return null;
                }

                Guid customerId = (Guid)combo.Value;

                Department department =
                            session.GetObjectByKey<Department>(customerId);

                if (department != null)
                {
                    ret = new NASCustomFieldPredefinitionData()
                    {
                        Code = department.Code,
                        Description = department.Description,
                        Name = department.Name,
                        PredefinitionType = Enum.GetName(typeof(SingleSelectionBuiltInTypeEnum),
                            SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_DEPARTMENT),
                        RefId = department.DepartmentId
                    };
                }

                return ret;
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

        public override void ItemRequestedByValue(DevExpress.Xpo.Session session, object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            Department obj = session.GetObjectByKey<Department>(e.Value);
            if (obj != null)
            {
                combo.DataSource = new Department[] { obj };
                combo.DataBindItems();
            }
        }

        public override void ItemsRequestedByFilterCondition(DevExpress.Xpo.Session session, object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<Department> collection = new XPCollection<Department>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                //row status is active
                new BinaryOperator("OrganizationId.OrganizationId", Utility.CurrentSession.Instance.AccessingOrganizationId),
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

        public override void Init(object source)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            combo.TextField = "Name";
            combo.TextFormatString = "{0} - {1}";
            combo.ValueField = "DepartmentId";
            combo.ValueType = typeof(System.Guid);
            combo.Columns.Clear();
            combo.Columns.Add("Code", "Mã phòng ban");
            combo.Columns.Add("Name", "Tên phòng ban");
            combo.Columns.Add("ParentDepartmentId.Name", "Trực thuộc phòng ban");
        }
    }
}