using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.CMS.ObjectDocument
{
    public class PredefinitionData : CustomFieldData
    {
        public PredefinitionData(Session session)
            : base(session)
        {
            
        }

        // Fields...
        private Guid _RefId;
        private string _PredefinitionType;

        public string PredefinitionType
        {
            get
            {
                return _PredefinitionType;
            }
            set
            {
                SetPropertyValue("PredefinitionType", ref _PredefinitionType, value);
            }
        }

        public Guid RefId
        {
            get
            {
                return _RefId;
            }
            set
            {
                SetPropertyValue("RefId", ref _RefId, value);
            }
        }

    }
}
