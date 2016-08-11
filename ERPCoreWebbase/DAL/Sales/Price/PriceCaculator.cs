using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Sales.Price
{
    public class PriceCaculator : XPCustomObject
    {
        public PriceCaculator(Session session)
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
        private Guid _PriceCaculatorId;
        private byte[] _PriceExpression;
        private PricePolicy _PricePolicyId;

        //Properties
        [Key(true)]
        public Guid PriceCaculatorId
        {
            get
            {
                return _PriceCaculatorId;
            }
            set
            {
                SetPropertyValue<Guid>("PriceCaculatorId", ref _PriceCaculatorId, value);
            }
        }

        public byte[] PriceExpression
        {
            get
            {
                return _PriceExpression;
            }
            set
            {
                SetPropertyValue<byte[]>("PriceExpression", ref _PriceExpression, value);
            }
        }

        [Association(@"PriceCaculatorReferencesPricePolicy")]
        public PricePolicy PricePolicyId
        {
            get
            {
                return _PricePolicyId;
            }
            set
            {
                SetPropertyValue<PricePolicy>("PricePolicy", ref _PricePolicyId, value);
            }
        }

        [Association(@"PriceFormulaTaxAddedReferencesPriceCaculator", typeof(PriceFormulaTaxAdded))]
        public XPCollection<PriceFormulaTaxAdded> PriceFormulaTaxAddeds
        {
            get
            {
                return GetCollection<PriceFormulaTaxAdded>("PriceFormulaTaxAddeds");
            }
        }
    }
}
