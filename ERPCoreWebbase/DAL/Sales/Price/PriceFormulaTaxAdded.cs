using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Invoice;

namespace NAS.DAL.Sales.Price
{
    public class PriceFormulaTaxAdded : XPCustomObject
    {
        public PriceFormulaTaxAdded(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        //attribute
        private Guid _PriceFormulaTaxAddedId;
        private TaxType _TaxTypeId;
        private PriceCaculator _PriceCaculatorId;
        private double _ByPercentage;
        private double _ByValue;

        //Properties
        [Key(true)]
        public Guid PriceFormulaTaxAddedId
        {
            get
            {
                return _PriceFormulaTaxAddedId;
            }
            set
            {
                SetPropertyValue<Guid>("PriceFormulaTaxAddedId", ref _PriceFormulaTaxAddedId, value);
            }
        }

        public double ByPercentage
        {
            get
            {
                return _ByPercentage;
            }
            set
            {
                SetPropertyValue<double>("ByPercentage", ref _ByPercentage, value);
            }
        }

        public double ByValue
        {
            get
            {
                return _ByValue;
            }
            set
            {
                SetPropertyValue<double>("ByValue", ref _ByValue, value);
            }
        }

        [Association(@"PriceFormulaTaxAddedReferencesPriceCaculator")]
        public PriceCaculator PriceCaculatorId
        {
            get
            {
                return _PriceCaculatorId;
            }
            set
            {
                SetPropertyValue<PriceCaculator>("PriceCaculatorId", ref _PriceCaculatorId, value);
            }
        }

        [Association(@"PriceFormulaTaxAddedReferencesTaxType")]
        public TaxType TaxTypeId
        {
            get
            {
                return _TaxTypeId;
            }
            set
            {
                SetPropertyValue<TaxType>("TaxTypeId", ref _TaxTypeId, value);
            }
        }
    }
}
