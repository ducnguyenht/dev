using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Sales.Price
{
    public class PricePolicyType : XPCustomObject
    {
        public PricePolicyType(Session session)
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
        private Guid _PricePolicyTypeId;
        private string _Code;
        private DateTime _CreateDate;
        private string _Description;
        private DateTime _IssueDate;
        private DateTime _LastUpdateDate;
        private string _Name;
        private short _RowStatus;
        private OwnerOrg _OwnerOrgId;

        //Properties
        [Key(true)]
        public Guid PricePolicyTypeId
        {
            get
            {
                return _PricePolicyTypeId;
            }
            set
            {
                SetPropertyValue<Guid>("PricePolicyTypeId", ref _PricePolicyTypeId, value);
            }
        }

        [Size(36)]
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                SetPropertyValue<string>("Code", ref _Code, value);
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                SetPropertyValue<DateTime>("CreateDate", ref _CreateDate, value);
            }
        }
        [Size(1024)]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                SetPropertyValue<string>("Description", ref _Description, value);
            }
        }

        public DateTime IssueDate
        {
            get
            {
                return _IssueDate;
            }
            set
            {
                SetPropertyValue<DateTime>("IssueDate", ref _IssueDate, value);
            }
        }

        public DateTime LastUpdateDate
        {
            get
            {
                return _LastUpdateDate;
            }
            set
            {
                SetPropertyValue<DateTime>("LastUpdateDate", ref _LastUpdateDate, value);
            }
        }
        [Size(255)]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetPropertyValue<string>("Name", ref _Name, value);
            }
        }

        public short RowStatus
        {
            get
            {
                return _RowStatus;
            }
            set
            {
                SetPropertyValue<short>("RowStatus", ref _RowStatus, value);
            }
        }

        [Association(@"PricePolicyTypeReferencesOwnerOrg")]
        public OwnerOrg OwnerOrgId
        {
            get
            {
                return _OwnerOrgId;
            }
            set
            {
                SetPropertyValue<OwnerOrg>("OwnerOrgId", ref _OwnerOrgId, value);
            }
        }

        [Association(@"PricePolicyReferencesPricePolicyType", typeof(PricePolicy))]
        public XPCollection<PricePolicy> PricePolices
        {
            get
            {
                return GetCollection<PricePolicy>("PricePolices");
            }
        }


        #region
        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Organization table
                if (!Util.isExistXpoObject<PricePolicyType>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    OwnerOrg.Populate();
                    OwnerOrg org = session.FindObject<OwnerOrg>(
                            new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE, BinaryOperatorType.Equal)
                        );
                    PricePolicyType pricePolicyType = new PricePolicyType(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                        CreateDate = DateTime.Now,
                        Description = "",
                        IssueDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        RowStatus = -1,
                        OwnerOrgId = org
                    };
                    pricePolicyType.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }
        #endregion
    }
}
