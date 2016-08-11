using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxGridLookup;
using DevExpress.Xpo;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data.Filtering;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Nomenclature.Organization;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl.Strategy
{
    public class NASCustomFieldTypeManufacturerBuiltInMultiSelectionListStrategy
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

                        ManufacturerOrg manufacturerOrg =
                                    session.GetObjectByKey<ManufacturerOrg>(predefinitionData.RefId);

                        if (manufacturerOrg != null)
                        {
                            NASCustomFieldPredefinitionData retItem = new NASCustomFieldPredefinitionData()
                            {
                                Code = manufacturerOrg.Code,
                                Description = manufacturerOrg.Description,
                                Name = manufacturerOrg.Name,
                                PredefinitionType = Enum.GetName(typeof(MultiSelectionBuiltInTypeEnum),
                                    MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_MANUFACTURER),
                                RefId = manufacturerOrg.OrganizationId
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
                        ManufacturerOrg manufacturerOrg =
                                    session.GetObjectByKey<ManufacturerOrg>(Id);

                        if (manufacturerOrg != null)
                        {
                            NASCustomFieldPredefinitionData retItem = new NASCustomFieldPredefinitionData()
                            {
                                Code = manufacturerOrg.Code,
                                Description = manufacturerOrg.Description,
                                Name = manufacturerOrg.Name,
                                PredefinitionType = Enum.GetName(typeof(MultiSelectionBuiltInTypeEnum),
                                    MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_MANUFACTURER),
                                RefId = manufacturerOrg.OrganizationId
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
                Caption = "Mã nhà sản xuất",
                VisibleIndex = 1,
                Width = 120
            };

            GridViewColumn nameColumn = new GridViewDataTextColumn()
            {
                ShowInCustomizationForm = true,
                FieldName = "Name",
                Caption = "Tên nhà sản xuất",
                VisibleIndex = 2,
                Width = 200
            };

            gridlookup.GridView.Width = 320;

            gridlookup.Columns.Add(selectionColumn);
            gridlookup.Columns.Add(codeColumn);
            gridlookup.Columns.Add(nameColumn);
        }

        public override void InitXpoDatasource(XpoDataSource datasource)
        {
            //Set TypeName
            datasource.TypeName = typeof(NAS.DAL.Nomenclature.Organization.ManufacturerOrg).FullName;
            //Set Criteria
            CriteriaOperator criteria =
                //row status is active
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE);

            datasource.Criteria = criteria.ToString();
            datasource.DefaultSorting = "Code";
        }
    }
}