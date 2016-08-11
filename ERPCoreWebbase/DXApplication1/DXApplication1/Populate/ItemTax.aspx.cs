using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Tax;
using NAS.DAL.Invoice;

namespace WebModule.Populate
{
    public partial class ItemTax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateDefaultTaxForAllItems();
        }

        private void PopulateDefaultTaxForAllItems()
        {
            using (Session session = XpoHelper.GetNewSession())
            {
                XPCollection<Item> itemList = new XPCollection<Item>(session,
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE));

                //TaxType VATTaxType = session.FindObject<TaxType>(new BinaryOperator("Code", "GTGT"));
                TaxType VATTaxType = session.FindObject<TaxType>(new BinaryOperator("Code", "VAT"));

                CriteriaOperator defaultTaxCriteria = CriteriaOperator.And(
                    new BinaryOperator("Percentage", "5"),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                    new BinaryOperator("TaxTypeId", VATTaxType)
                );

                //Get default Tax
                Tax defaultTax = session.FindObject<Tax>(defaultTaxCriteria);

                if (defaultTax == null)
                    throw new Exception("Could not found default tax");

                foreach (var item in itemList)
                {
                    int countExistDefaultTax =
                        item.ItemTaxes.Count(r => 
                            (r.TaxId != null
                                && r.TaxId.TaxTypeId == VATTaxType)
                            && r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE);

                    if (countExistDefaultTax != 0)
                        continue;

                    NAS.DAL.Nomenclature.Item.ItemTax itemTax = new NAS.DAL.Nomenclature.Item.ItemTax(session)
                    {
                        ItemId = item,
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                        TaxId = defaultTax
                    };

                    itemTax.Save();
                }

            }
        }

    }
}