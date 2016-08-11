using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl.Strategy
{
    public class NASCustomFieldTypeSaleInvoiceBuiltInMultiSelectionListStrategyCreator
        : NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreator
    {
        public override NASCustomFieldTypeBuiltInMultiSelectionListStrategy Create()
        {
            return new NASCustomFieldTypeSaleInvoiceBuiltInMultiSelectionListStrategy();
        }
    }
}