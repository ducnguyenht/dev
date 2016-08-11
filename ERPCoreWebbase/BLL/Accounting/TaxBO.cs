using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Invoice;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Tax;
using NAS.DAL.Sales.Price;
using NAS.DAL.Nomenclature.Item;

namespace NAS.BO.Accounting
{
    public class TaxBO
    {
        public bool checkIsDupplicateTaxTypeCode(Session session, string code)
        {
            try
            {
                TaxType taxtype = session.FindObject<TaxType>(CriteriaOperator.And(
                        new BinaryOperator("Code", code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)));
                if (taxtype != null)
                    return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool checkIsDupplicateTaxCode(Session session, string code)
        {
            try
            {
                Tax tax = session.FindObject<Tax>(CriteriaOperator.And(
                        new BinaryOperator("Code", code, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)));
                if (tax != null)
                    return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool checkTaxTypeIsExistInPricePolicy(Session session, string code, out string relatedPricePolicy)
        {
            try
            {
                PriceFormulaTaxAdded formula = session.FindObject<PriceFormulaTaxAdded>(
                    new BinaryOperator("TaxTypeId.Code", code, BinaryOperatorType.Equal));
                if (formula != null)
                {
                    relatedPricePolicy = formula.PriceCaculatorId.PricePolicyId.Code;
                    return true;
                }
                relatedPricePolicy = string.Empty;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool checkTaxIsExistInItemTax(Session session, string code, out string relatedItemCode)
        {
            try
            {
                ItemTax itemtax = session.FindObject<ItemTax>( CriteriaOperator.And(
                    new BinaryOperator("TaxId.Code", code, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Equal))
                );
                if (itemtax != null)
                {
                    relatedItemCode = itemtax.ItemId.Code;
                    return true;
                }
                relatedItemCode = string.Empty;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
