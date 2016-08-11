using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Inventory.Lot;
using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data.Filtering;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl.Strategy
{
    public class NASCustomFieldTypeLotBuiltInMultiSelectionListStrategy
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

                        Lot lot = session.GetObjectByKey<Lot>(predefinitionData.RefId);

                        if (lot != null)
                        {
                            NASCustomFieldPredefinitionData retItem = new NASCustomFieldPredefinitionData()
                            {
                                Code = lot.Code,
                                Description = lot.Description,
                                Name = lot.Description,
                                PredefinitionType = Enum.GetName(typeof(MultiSelectionBuiltInTypeEnum),
                                    MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_LOT),
                                RefId = lot.LotId
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
                                .GetSelectedFieldValues("LotId")
                                .Select(r => Guid.Parse(r.ToString()));

                if (selectedIds != null)
                {
                    ret = new List<NASCustomFieldPredefinitionData>();
                    foreach (var Id in selectedIds)
                    {
                        Lot lot =
                                    session.GetObjectByKey<Lot>(Id);

                        if (lot != null)
                        {
                            NASCustomFieldPredefinitionData retItem = new NASCustomFieldPredefinitionData()
                            {
                                Code = lot.Code,
                                Description = lot.Description,
                                Name = lot.Description,
                                PredefinitionType = Enum.GetName(typeof(MultiSelectionBuiltInTypeEnum),
                                    MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_LOT),
                                RefId = lot.LotId
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
            gridlookup.KeyFieldName = "LotId";
            gridlookup.TextFormatString = "{0} - {1}";

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
                Caption = "Số lô",
                VisibleIndex = 1
            };

            GridViewColumn nameColumn = new GridViewDataTextColumn()
            {
                ShowInCustomizationForm = true,
                FieldName = "Description",
                Caption = "Diễn giải",
                VisibleIndex = 2
            };

            GridViewColumn itemColumn = new GridViewDataTextColumn()
            {
                ShowInCustomizationForm = true,
                FieldName = "ItemId.Name",
                Caption = "Tên mặt hàng",
                VisibleIndex = 3
            };

            gridlookup.Columns.Add(selectionColumn);
            gridlookup.Columns.Add(codeColumn);
            gridlookup.Columns.Add(nameColumn);
            gridlookup.Columns.Add(itemColumn);
        }

        public override void InitXpoDatasource(XpoDataSource datasource)
        {
            //Set TypeName
            datasource.TypeName = typeof(NAS.DAL.Inventory.Lot.Lot).FullName;
            //Set Criteria
            CriteriaOperator criteria =
                //row status is active
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE);

            datasource.Criteria = criteria.ToString();
            datasource.DefaultSorting = "Code";
        }
    }
}