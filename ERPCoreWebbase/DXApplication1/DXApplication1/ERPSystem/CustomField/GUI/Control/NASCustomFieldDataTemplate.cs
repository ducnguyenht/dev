using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web.ASPxGridView;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeDateTimeControl.State;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInSingleSelectionListControl.Strategy;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl.Strategy;

namespace WebModule.ERPSystem.CustomField.GUI.Control
{
    public class NASCustomFieldGridViewDataItemTemplate : ITemplate
    {
        private List<INASCustomFieldTypeControl> _NASCustomFieldTypeControlList;
        public List<INASCustomFieldTypeControl> NASCustomFieldTypeControls
        {
            get
            {
                if (_NASCustomFieldTypeControlList == null)
                    _NASCustomFieldTypeControlList = new List<INASCustomFieldTypeControl>();
                return _NASCustomFieldTypeControlList;
            }
            set
            {
                _NASCustomFieldTypeControlList = value;
            }
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            try
            {
                using (Session session = XpoHelper.GetNewSession())
                {
                    GridViewDataItemTemplateContainer itemContainer = (GridViewDataItemTemplateContainer)container;
                    ASPxGridView gridview = (ASPxGridView)itemContainer.Grid;

                    NASCustomFieldDataGridView userControl = (NASCustomFieldDataGridView)gridview.Parent;
                    //if (userControl.isAddCustomFieldControls)
                    //    return;

                    Guid ObjectCustomFieldId = (Guid)itemContainer.KeyValue;
                    //Get ObjectCustomField
                    ObjectCustomField objectCustomField =
                        session.GetObjectByKey<ObjectCustomField>(ObjectCustomFieldId);

                    //Mark to re-bind gridview when the custom field was deleted
                    if (objectCustomField.ObjectTypeCustomFieldId == null)
                    {
                        throw new Exception("Một hay nhiều trường động bị xóa. Xin hãy nhấn 'F5' để làm mới.");
                    }

                    string customFieldType =
                        objectCustomField.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldTypeId.Code;

                    string uniquePart = ObjectCustomFieldId.ToString().Replace("-", "");

                    string controlID = String.Format("{0}_{1}_{2}",
                        uniquePart,
                        userControl.ViewStateControlId,
                        customFieldType);

                    //Check control is exist
                    int countControlExist = NASCustomFieldTypeControls.Count(
                        r => ((System.Web.UI.Control)r).ID.Split('_')[0].Equals(uniquePart));

                    System.Web.UI.Control customFieldControl = null;

                    if (countControlExist != 0)
                    {
                        //Add existed control to container
                        customFieldControl = (System.Web.UI.Control)NASCustomFieldTypeControls.FirstOrDefault(
                            r => ((System.Web.UI.Control)r).ID.Split('_')[0].Equals(uniquePart));
                        if (customFieldControl != null)
                        {
                            NASCustomFieldTypeControls.Remove((INASCustomFieldTypeControl)customFieldControl);
                        }
                    }

                    #region Create controls dynamically
                    switch (customFieldType)
                    {
                        #region Basic custom field types
                        case "STRING":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeStringControl/NASCustomFieldTypeStringControl.ascx")
                                as NASCustomFieldTypeStringControl.NASCustomFieldTypeStringControl;
                            break;
                        case "DATETIME":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeDateTimeControl/NASCustomFieldTypeDateTimeControl.ascx")
                                as NASCustomFieldTypeDateTimeControl.NASCustomFieldTypeDateTimeControl;
                            break;
                        case "INTEGER":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeIntegerControl/NASCustomFieldTypeIntegerControl.ascx")
                                as NASCustomFieldTypeIntegerControl.NASCustomFieldTypeIntegerControl;
                            break;
                        case "FLOAT":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeFloatControl/NASCustomFieldTypeFloatControl.ascx")
                                as NASCustomFieldTypeFloatControl.NASCustomFieldTypeFloatControl;
                            break;
                        case "SINGLE_CHOICE_LIST":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeSingleSelectionListControl/NASCustomFieldTypeSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeSingleSelectionListControl.NASCustomFieldTypeSingleSelectionListControl;
                            break;
                        case "MULTI_CHOICE_LIST":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeMultiSelectionListControl/NASCustomFieldTypeMultiSelectionListControl.ascx")
                                as NASCustomFieldTypeMultiSelectionListControl.NASCustomFieldTypeMultiSelectionListControl;
                            break;
                        #endregion

                        #region Single choice built-in types
                        case "SINGLE_CHOICE_LIST_MANUFACTURER":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInSingleSelectionListControl/NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl;
                            ((NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
                                    .GetCreator(SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_MANUFACTURER).Create());
                            break;
                        case "SINGLE_CHOICE_LIST_ORGANIZATION":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInSingleSelectionListControl/NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl;
                            ((NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
                                    .GetCreator(SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_ORGANIZATION).Create());
                            break;
                        case "SINGLE_CHOICE_LIST_DEPARTMENT":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInSingleSelectionListControl/NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl;
                            ((NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
                                    .GetCreator(SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_DEPARTMENT).Create());
                            break;
                        case "SINGLE_CHOICE_LIST_PERSON":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInSingleSelectionListControl/NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl;
                            ((NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
                                    .GetCreator(SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_PERSON).Create());
                            break;
                        case "SINGLE_CHOICE_LIST_CUSTOMER":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInSingleSelectionListControl/NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl;
                            ((NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
                                    .GetCreator(SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_CUSTOMER).Create());
                            break;
                        case "SINGLE_CHOICE_LIST_SUPPLIER":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInSingleSelectionListControl/NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl;
                            ((NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
                                    .GetCreator(SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_SUPPLIER).Create());
                            break;
                        case "SINGLE_CHOICE_LIST_INVENTORY":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInSingleSelectionListControl/NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl;
                            ((NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
                                    .GetCreator(SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INVENTORY).Create());
                            break;
                        case "SINGLE_CHOICE_LIST_LOT":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInSingleSelectionListControl/NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl;
                            ((NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
                                    .GetCreator(SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_LOT).Create());
                            break;
                        case "SINGLE_CHOICE_LIST_INVOICE_PURCHASE":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInSingleSelectionListControl/NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl;
                            ((NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
                                    .GetCreator(SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INVOICE_PURCHASE).Create());
                            break;
                        case "SINGLE_CHOICE_LIST_INVOICE_SALE":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInSingleSelectionListControl/NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl;
                            ((NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
                                    .GetCreator(SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INVOICE_SALE).Create());
                            break;
                        case "SINGLE_CHOICE_LIST_ITEM":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInSingleSelectionListControl/NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl;
                            ((NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
                                    .GetCreator(SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_ITEM).Create());
                            break;
                        /*2014/03/01 Duc.Vo INS*/
                        case "SINGLE_CHOICE_LIST_INPUT_INVENTORY_COMMAND":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInSingleSelectionListControl/NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl;
                            ((NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
                                    .GetCreator(SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INPUT_INVENTORY_COMMAND).Create());
                            break;
                        /*2014/03/01 Duc.Vo INS*/
                        #endregion

                        #region Multi choice built-in types
                        case "MULTI_CHOICE_LIST_MANUFACTURER":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInMultiSelectionListControl/NASCustomFieldTypeBuiltInMultiSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl;
                            ((NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInMultiSelectionListStrategy(NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreators
                                    .GetCreator(MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_MANUFACTURER).Create());
                            break;
                        case "MULTI_CHOICE_LIST_ORGANIZATION":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInMultiSelectionListControl/NASCustomFieldTypeBuiltInMultiSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl;
                            ((NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInMultiSelectionListStrategy(NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreators
                                    .GetCreator(MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_ORGANIZATION).Create());
                            break;
                        case "MULTI_CHOICE_LIST_DEPARTMENT":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInMultiSelectionListControl/NASCustomFieldTypeBuiltInMultiSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl;
                            ((NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInMultiSelectionListStrategy(NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreators
                                    .GetCreator(MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_DEPARTMENT).Create());
                            break;
                        case "MULTI_CHOICE_LIST_PERSON":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInMultiSelectionListControl/NASCustomFieldTypeBuiltInMultiSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl;
                            ((NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInMultiSelectionListStrategy(NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreators
                                    .GetCreator(MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_PERSON).Create());
                            break;
                        case "MULTI_CHOICE_LIST_CUSTOMER":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInMultiSelectionListControl/NASCustomFieldTypeBuiltInMultiSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl;
                            ((NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInMultiSelectionListStrategy(NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreators
                                    .GetCreator(MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_CUSTOMER).Create());
                            break;
                        case "MULTI_CHOICE_LIST_SUPPLIER":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInMultiSelectionListControl/NASCustomFieldTypeBuiltInMultiSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl;
                            ((NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInMultiSelectionListStrategy(NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreators
                                    .GetCreator(MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_SUPPLIER).Create());
                            break;
                        case "MULTI_CHOICE_LIST_INVENTORY":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInMultiSelectionListControl/NASCustomFieldTypeBuiltInMultiSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl;
                            ((NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInMultiSelectionListStrategy(NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreators
                                    .GetCreator(MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_INVENTORY).Create());
                            break;
                        case "MULTI_CHOICE_LIST_LOT":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInMultiSelectionListControl/NASCustomFieldTypeBuiltInMultiSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl;
                            ((NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInMultiSelectionListStrategy(NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreators
                                    .GetCreator(MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_LOT).Create());
                            break;
                        case "MULTI_CHOICE_LIST_INVOICE_PURCHASE":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInMultiSelectionListControl/NASCustomFieldTypeBuiltInMultiSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl;
                            ((NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInMultiSelectionListStrategy(NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreators
                                    .GetCreator(MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_INVOICE_PURCHASE).Create());
                            break;
                        case "MULTI_CHOICE_LIST_INVOICE_SALE":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInMultiSelectionListControl/NASCustomFieldTypeBuiltInMultiSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl;
                            ((NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInMultiSelectionListStrategy(NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreators
                                    .GetCreator(MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_INVOICE_SALE).Create());
                            break;
                        case "MULTI_CHOICE_LIST_ITEM":
                            customFieldControl =
                                gridview.Page.LoadControl("~/ERPSystem/CustomField/GUI/Control/NASCustomFieldTypeBuiltInMultiSelectionListControl/NASCustomFieldTypeBuiltInMultiSelectionListControl.ascx")
                                as NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl;
                            ((NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl)customFieldControl)
                                .SetNASCustomFieldTypeBuiltInMultiSelectionListStrategy(NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreators
                                    .GetCreator(MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_ITEM).Create());
                            break;
                        #endregion
                        
                        default:
                            break;
                    }
                    #endregion
                    if (customFieldControl != null)
                    {
                        customFieldControl.ID = controlID;
                        ((INASCustomFieldTypeControl)customFieldControl).DataUpdated += 
                            new CustomFieldControlDataUpdatedEventHandler(userControl.customFieldControl_DataUpdated);
                        ((INASCustomFieldTypeControl)customFieldControl).BeforeDataEditing +=
                            new CustomFieldControlBeforeDataEditingEventHandler(userControl.customFieldControl_BeforeDataEditing);
                        NASCustomFieldTypeControls.Add((INASCustomFieldTypeControl)customFieldControl);
                        container.Controls.Add(customFieldControl);
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}