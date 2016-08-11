using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Web.ASPxEditors;
using DevExpress.Data.Filtering;
using NAS.DAL.Invoice;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInSingleSelectionListControl.Strategy
{
    public class NASCustomFieldTypePurchaseInvoiceBuiltInSingleSelectionListStrategy : NASCustomFieldTypeBuiltInSingleSelectionListStrategy
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

                    PurchaseInvoice purchaseInvoice =
                                session.GetObjectByKey<PurchaseInvoice>(predefinitionData.RefId);

                    if (purchaseInvoice != null)
                    {
                        ret = new NASCustomFieldPredefinitionData()
                        {
                            Code = purchaseInvoice.Code,
                            Description = String.Empty,
                            Name = String.Empty,
                            PredefinitionType = Enum.GetName(typeof(SingleSelectionBuiltInTypeEnum),
                                SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INVOICE_PURCHASE),
                            RefId = purchaseInvoice.BillId
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

                Guid purchaseInvoiceId = (Guid)combo.Value;

                PurchaseInvoice purchaseInvoice =
                            session.GetObjectByKey<PurchaseInvoice>(purchaseInvoiceId);

                if (purchaseInvoice != null)
                {
                    ret = new NASCustomFieldPredefinitionData()
                    {
                        Code = purchaseInvoice.Code,
                        Description = String.Empty,
                        Name = String.Empty,
                        PredefinitionType = Enum.GetName(typeof(SingleSelectionBuiltInTypeEnum),
                            SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INVOICE_PURCHASE),
                        RefId = purchaseInvoice.BillId
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
            PurchaseInvoice obj = session.GetObjectByKey<PurchaseInvoice>(e.Value);
            if (obj != null)
            {
                combo.DataSource = new PurchaseInvoice[] { obj };
                combo.DataBindItems();
            }
        }

        public override void ItemsRequestedByFilterCondition(DevExpress.Xpo.Session session, object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<PurchaseInvoice> collection = new XPCollection<PurchaseInvoice>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                //row status is active
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual),
                //find code contains the filter
                new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
            );

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            combo.DataSource = collection;
            combo.DataBindItems();
        }

        public override void Init(object source)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            combo.TextField = "Code";
            combo.ValueField = "BillId";
            combo.ValueType = typeof(System.Guid);
            combo.Columns.Clear();
            combo.Columns.Add("Code", "Số phiếu mua");
            combo.Columns.Add("IssuedDate", "Ngày");
        }
    }
}