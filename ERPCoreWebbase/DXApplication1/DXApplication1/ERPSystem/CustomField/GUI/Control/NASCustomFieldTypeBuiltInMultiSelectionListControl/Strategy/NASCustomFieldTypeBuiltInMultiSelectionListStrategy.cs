using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Web.ASPxGridLookup;
using NAS.BO.CMS.ObjectDocument;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl.Strategy
{
    public abstract class NASCustomFieldTypeBuiltInMultiSelectionListStrategy
    {
        public abstract List<NASCustomFieldPredefinitionData> GetPredefinitionDataOfObject(Guid objectCustomFieldId);

        public abstract List<NASCustomFieldPredefinitionData> GetSelectedPredefinitionDataFromList(object source);

        public virtual bool UpdatePredefinitionDataForObject(Guid objectCustomFieldId, List<NASCustomFieldPredefinitionData> data)
        {
            try
            {
                if (data == null || data.Count == 0)
                    return true;

                ObjectCustomFieldBO objectCustomFieldBO = new ObjectCustomFieldBO();

                PredefinitionCustomFieldTypeEnum predefinitionType = 
                    (PredefinitionCustomFieldTypeEnum)Enum.Parse(
                        typeof(PredefinitionCustomFieldTypeEnum), data.First().PredefinitionType);

                return objectCustomFieldBO.UpdatePredefinitionData(
                    objectCustomFieldId, data.Select(r => r.RefId), predefinitionType);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public abstract void InitGridLookup(ASPxGridLookup gridlookup);

        public abstract void InitXpoDatasource(XpoDataSource datasource);
    }
}