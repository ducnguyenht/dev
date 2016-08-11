using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Nomenclature.Item
{
    public partial class ItemSupplier
    {
        [NonPersistent]
        public string SupplierName
        {
            get
            {
                if (SupplierOrgId == null)
                    return "";
                return SupplierOrgId.Name;
            }
        }

        [NonPersistent]
        public string SupplierCode
        {
            get
            {
                if (SupplierOrgId == null)
                    return "";
                return SupplierOrgId.Code;
            }
        }

        [NonPersistent]
        public string SupplierDescription
        {
            get
            {
                if (SupplierOrgId == null)
                    return "";
                return SupplierOrgId.Description;
            }
        }
    }
}
