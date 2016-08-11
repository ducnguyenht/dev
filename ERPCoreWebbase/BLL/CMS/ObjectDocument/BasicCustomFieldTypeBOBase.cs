using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using DevExpress.Xpo;
using NAS.DAL.CMS.ObjectDocument;
using System.IO;

namespace NAS.BO.CMS.ObjectDocument
{
    public abstract class BasicCustomFieldTypeBOBase
    {
        public abstract bool UpdateCustomFieldData(Guid objectCustomFieldId, object value, CustomFieldTypeFlag flag);
    }
}
