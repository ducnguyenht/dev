using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Tax;

namespace NAS.DAL.Nomenclature.Item
{
    public partial class Item
    {
        public enum TYPEOFITEM
        {
            PRODUCT, MATERIAL, TOOL, SERVICE
        }

        [NonPersistent]
        public string ManufacturerName
        {
            get
            {
                if (ManufacturerOrgId == null)
                    return "";
                return ManufacturerOrgId.Name;
            }
        }

        [NonPersistent]
        public Guid OrganizationId
        {
            get
            {
                if (ManufacturerOrgId != null)
                    return ManufacturerOrgId.OrganizationId;
                return ((ManufacturerOrg)Session.FindObject<ManufacturerOrg>(
                        new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE))).OrganizationId;
            }
        }

        [NonPersistent]
        public double VatPercentage
        {
            get
            {
                double vatPercentage = 0;
                
                CriteriaOperator filter = GroupOperator.And(new BinaryOperator("TaxId.TaxTypeId.Code", "VAT", BinaryOperatorType.Equal),
                                                            new BinaryOperator("ItemId.ItemId", ItemId, BinaryOperatorType.Equal));
                ItemTax itemTax = Session.FindObject<ItemTax>(filter);
                if (itemTax != null)
                {
                    vatPercentage = itemTax.TaxId.Percentage;
                }                
                                
                return vatPercentage;
            }
        }
    }
}
