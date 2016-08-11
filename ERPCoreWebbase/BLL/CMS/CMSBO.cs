using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL;
using DevExpress.Xpo;

namespace NAS.BO.CMS
{
    public class CMSBO
    {
        //Obbject Type
        //Obbject
        public void CreateObject(Session session, NAS.DAL.CMS.ObjectDocument.ObjectType objectType)
        {
            NAS.DAL.CMS.ObjectDocument.Object newobject = new NAS.DAL.CMS.ObjectDocument.Object(session);            
            InitObjectCustomField(session, newobject, objectType);
        }
        //CustomFieldType
        //Customfield
        //ObjectTypeCustomfield
        public List<object> GetFullObjectTypeCustomFieldList(NAS.DAL.CMS.ObjectDocument.ObjectType objecttype)
        {
            List<object> result = new List<object>();
            return result;
        }
        //ObjectCustomField        
        public void InitObjectCustomField(Session session, NAS.DAL.CMS.ObjectDocument.Object newobject, NAS.DAL.CMS.ObjectDocument.ObjectType objectType)
        {
        }
        public List<object> GetFullObjectCustomFieldList(NAS.DAL.CMS.ObjectDocument.Object newobject)
        {
            List<object> result = new List<object>();
            return result;
        }
    }
}
