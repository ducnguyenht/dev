using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl.Strategy
{
    public class NASCustomFieldTypeSupplierBuiltInMultiSelectionListStrategy
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

                        Organization organization =
                                    session.GetObjectByKey<Organization>(predefinitionData.RefId);

                        if (organization != null)
                        {
                            NASCustomFieldPredefinitionData retItem = new NASCustomFieldPredefinitionData()
                            {
                                Code = organization.Code,
                                Description = organization.Description,
                                Name = organization.Name,
                                PredefinitionType = Enum.GetName(typeof(MultiSelectionBuiltInTypeEnum),
                                    MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_SUPPLIER),
                                RefId = organization.OrganizationId
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
                                .GetSelectedFieldValues("OrganizationId")
                                .Select(r => Guid.Parse(r.ToString()));

                if (selectedIds != null)
                {
                    ret = new List<NASCustomFieldPredefinitionData>();
                    foreach (var Id in selectedIds)
                    {
                        Organization organization =
                                    session.GetObjectByKey<Organization>(Id);

                        if (organization != null)
                        {
                            NASCustomFieldPredefinitionData retItem = new NASCustomFieldPredefinitionData()
                            {
                                Code = organization.Code,
                                Description = organization.Description,
                                Name = organization.Name,
                                PredefinitionType = Enum.GetName(typeof(MultiSelectionBuiltInTypeEnum),
                                    MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_SUPPLIER),
                                RefId = organization.OrganizationId
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
            gridlookup.KeyFieldName = "OrganizationId";
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
                Caption = "Mã nhà cung cấp",
                VisibleIndex = 1
            };

            GridViewColumn nameColumn = new GridViewDataTextColumn()
            {
                ShowInCustomizationForm = true,
                FieldName = "Name",
                Caption = "Tên nhà cung cấp",
                VisibleIndex = 2,
                Width = 300
            };

            gridlookup.Columns.Add(selectionColumn);
            gridlookup.Columns.Add(codeColumn);
            gridlookup.Columns.Add(nameColumn);
        }

        public override void InitXpoDatasource(XpoDataSource datasource)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Set TypeName
                datasource.TypeName = typeof(NAS.DAL.Nomenclature.Organization.Organization).FullName;
                //Set Criteria
                //Get SUPPLIER trading type
                TradingCategory supplierTradingCategory =
                    session.FindObject<TradingCategory>(new BinaryOperator("Code", "SUPPLIER"));

                CriteriaOperator criteria = CriteriaOperator.And(
                    //row status is active
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                    //find customer and supplier
                    new ContainsOperator("OrganizationCategories",
                        CriteriaOperator.And(
                            new BinaryOperator("TradingCategoryId.TradingCategoryId", 
                                supplierTradingCategory.TradingCategoryId),
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                        )
                    )
                );

                datasource.Criteria = criteria.ToString();
                datasource.DefaultSorting = "Code";
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
    }
}