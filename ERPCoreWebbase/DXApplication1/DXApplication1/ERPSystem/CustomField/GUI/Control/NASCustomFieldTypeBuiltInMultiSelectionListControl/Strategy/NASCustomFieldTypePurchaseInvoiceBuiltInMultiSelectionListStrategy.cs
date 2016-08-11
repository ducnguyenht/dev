using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Invoice;
using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data.Filtering;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl.Strategy
{
    public class NASCustomFieldTypePurchaseInvoiceBuiltInMultiSelectionListStrategy
        : NASCustomFieldTypeBuiltInMultiSelectionListStrategy
    {
        public override List<NASCustomFieldPredefinitionData> GetPredefinitionDataOfObject(Guid objectCustomFieldId)
        {
            List<NASCustomFieldPredefinitionData> ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new List<NASCustomFieldPredefinitionData>();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        PurchaseInvoice purchaseInvoice =
                                    session.GetObjectByKey<PurchaseInvoice>(predefinitionData.RefId);

                        if (purchaseInvoice != null)
                        {
                            NASCustomFieldPredefinitionData retItem = new NASCustomFieldPredefinitionData()
                            {
                                Code = purchaseInvoice.Code,
                                Description = String.Empty,
                                Name = String.Empty,
                                PredefinitionType = Enum.GetName(typeof(MultiSelectionBuiltInTypeEnum),
                                    MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_INVOICE_PURCHASE),
                                RefId = purchaseInvoice.BillId
                            };
                            ret.Add(retItem);
                        }
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

        public override List<NASCustomFieldPredefinitionData> GetSelectedPredefinitionDataFromList(object source)
        {
            List<NASCustomFieldPredefinitionData> ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ASPxGridLookup gridlookup = source as ASPxGridLookup;

                var selectedIds = gridlookup.GridView
                                .GetSelectedFieldValues("BillId")
                                .Select(r => Guid.Parse(r.ToString()));

                if (selectedIds != null)
                {
                    ret = new List<NASCustomFieldPredefinitionData>();
                    foreach (var Id in selectedIds)
                    {
                        PurchaseInvoice purchaseInvoice =
                                    session.GetObjectByKey<PurchaseInvoice>(Id);

                        if (purchaseInvoice != null)
                        {
                            NASCustomFieldPredefinitionData retItem = new NASCustomFieldPredefinitionData()
                            {
                                Code = purchaseInvoice.Code,
                                Description = String.Empty,
                                Name = String.Empty,
                                PredefinitionType = Enum.GetName(typeof(MultiSelectionBuiltInTypeEnum),
                                    MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_INVOICE_PURCHASE),
                                RefId = purchaseInvoice.BillId
                            };
                            ret.Add(retItem);
                        }
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

        public override void InitGridLookup(ASPxGridLookup gridlookup)
        {
            gridlookup.KeyFieldName = "BillId";
            gridlookup.TextFormatString = "{0}";

            gridlookup.Columns.Clear();

            GridViewColumn selectionColumn = new GridViewCommandColumn()
            {
                ShowSelectCheckbox = true,
                VisibleIndex = 0
            };

            GridViewColumn codeColumn = new GridViewDataTextColumn()
            {
                ShowInCustomizationForm = true,
                FieldName = "Code",
                Caption = "Số phiếu mua",
                VisibleIndex = 1
            };

            GridViewColumn nameColumn = new GridViewDataTextColumn()
            {
                ShowInCustomizationForm = true,
                FieldName = "IssuedDate",
                Caption = "Ngày",
                VisibleIndex = 2
            };

            gridlookup.Columns.Add(selectionColumn);
            gridlookup.Columns.Add(codeColumn);
            gridlookup.Columns.Add(nameColumn);
        }

        public override void InitXpoDatasource(XpoDataSource datasource)
        {
            //Set TypeName
            datasource.TypeName = typeof(NAS.DAL.Invoice.PurchaseInvoice).FullName;
            //Set Criteria
            CriteriaOperator criteria =
                //row status is active
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE,
                    BinaryOperatorType.GreaterOrEqual);

            datasource.Criteria = criteria.ToString();
            datasource.DefaultSorting = "Code";
        }
    }
}