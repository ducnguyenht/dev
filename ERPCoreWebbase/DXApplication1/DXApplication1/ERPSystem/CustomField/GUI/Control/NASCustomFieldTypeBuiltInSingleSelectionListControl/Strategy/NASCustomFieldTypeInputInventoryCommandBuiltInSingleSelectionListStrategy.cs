using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Inventory.Command;
using DevExpress.Data.Filtering;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInSingleSelectionListControl.Strategy
{
    public class NASCustomFieldTypeInputInventoryCommandBuiltInSingleSelectionListStrategy : NASCustomFieldTypeBuiltInSingleSelectionListStrategy
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

                    InventoryCommand command =
                                session.GetObjectByKey<InventoryCommand>(predefinitionData.RefId);

                    if (command != null)
                    {
                        ret = new NASCustomFieldPredefinitionData()
                        {
                            Code = command.Code,
                            Description = command.Description,
                            Name = command.Name,
                            PredefinitionType = Enum.GetName(typeof(SingleSelectionBuiltInTypeEnum),
                                SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INPUT_INVENTORY_COMMAND),
                            RefId = command.InventoryCommandId
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

                Guid commandId = (Guid)combo.Value;

                InventoryCommand command =
                            session.GetObjectByKey<InventoryCommand>(commandId);

                if (command != null)
                {
                    ret = new NASCustomFieldPredefinitionData()
                    {
                        Code = command.Code,
                        Description = command.Description,
                        Name = command.Name,
                        PredefinitionType = Enum.GetName(typeof(SingleSelectionBuiltInTypeEnum),
                            SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INPUT_INVENTORY_COMMAND),
                        RefId = command.InventoryCommandId
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
            InventoryCommand obj = session.GetObjectByKey<InventoryCommand>(e.Value);
            if (obj != null)
            {
                combo.DataSource = new InventoryCommand[] { obj };
                combo.DataBindItems();
            }
        }

        public override void ItemsRequestedByFilterCondition(DevExpress.Xpo.Session session, object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<InventoryCommand> collection = new XPCollection<InventoryCommand>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            //Get SUPPLIER trading type
            //TradingCategory supplierTradingCategory =
            //    session.FindObject<TradingCategory>(new BinaryOperator("Code", "SUPPLIER"));

            CriteriaOperator criteria = CriteriaOperator.And(
                //row status is active
                //CriteriaOperator.Or(
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                //    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                //),
                CriteriaOperator.Or(
                //find code contains the filter
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                //find name contains the filter
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                ),
                new BinaryOperator("CommandType", 'I', BinaryOperatorType.Equal)
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
            combo.ValueField = "InventoryCommandId";
            combo.ValueType = typeof(System.Guid);
            combo.Columns.Clear();
            combo.Columns.Add("Code", "Mã phiếu nhập");
            combo.Columns.Add("Name", "Tên phiếu nhập");
        }
    }
}