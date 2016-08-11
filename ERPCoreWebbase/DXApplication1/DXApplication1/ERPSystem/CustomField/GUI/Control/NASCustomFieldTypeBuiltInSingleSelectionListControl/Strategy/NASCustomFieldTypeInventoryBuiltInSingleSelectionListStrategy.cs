using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Web.ASPxEditors;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Inventory;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInSingleSelectionListControl.Strategy
{
    public class NASCustomFieldTypeInventoryBuiltInSingleSelectionListStrategy : NASCustomFieldTypeBuiltInSingleSelectionListStrategy
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

                    Inventory inventory = session.GetObjectByKey<Inventory>(predefinitionData.RefId);

                    if (inventory != null)
                    {
                        ret = new NASCustomFieldPredefinitionData()
                        {
                            Code = inventory.Code,
                            Description = inventory.Description,
                            Name = inventory.Name,
                            PredefinitionType = Enum.GetName(typeof(SingleSelectionBuiltInTypeEnum),
                                SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INVENTORY),
                            RefId = inventory.InventoryId
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

                Guid inventoryId = (Guid)combo.Value;

                Inventory inventory = session.GetObjectByKey<Inventory>(inventoryId);

                if (inventory != null)
                {
                    ret = new NASCustomFieldPredefinitionData()
                    {
                        Code = inventory.Code,
                        Description = inventory.Description,
                        Name = inventory.Name,
                        PredefinitionType = Enum.GetName(typeof(SingleSelectionBuiltInTypeEnum),
                            SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INVENTORY),
                        RefId = inventory.InventoryId
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
            Inventory obj = session.GetObjectByKey<Inventory>(e.Value);
            if (obj != null)
            {
                combo.DataSource = new Inventory[] { obj };
                combo.DataBindItems();
            }
        }

        public override void ItemsRequestedByFilterCondition(DevExpress.Xpo.Session session, object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<Inventory> collection = new XPCollection<Inventory>(session);
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

        public override void Init(object source)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            combo.TextField = "Name";
            combo.TextFormatString = "{0} - {1}";
            combo.ValueField = "InventoryId";
            combo.ValueType = typeof(System.Guid);
            combo.Columns.Clear();
            combo.Columns.Add("Code", "Mã kho");
            combo.Columns.Add("Name", "Tên kho");
            combo.Columns.Add("Description", "Mô tả");
        }
    }
}