using System;
using DevExpress.Xpo;
using System.Linq;
using NAS.DAL.CMS.ObjectDocument;

namespace NAS.DAL.Nomenclature.Organization
{

    public class TradingCategory : XPCustomObject
    {

        #region Logic
        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into TradingCategory table
                if (!Util.isExistXpoObject<TradingCategory>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    TradingCategory tradingCategory = new TradingCategory(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                        Name = "Mặc định",
                        Description = "Mặc định",
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };

                    tradingCategory.Save();
                }

                if (!Util.isExistXpoObject<TradingCategory>("Code", "CUSTOMER"))
                {
                    TradingCategory tradingCategory = new TradingCategory(session)
                    {
                        Code = "CUSTOMER",
                        Name = "Khách hàng",
                        Description = "Khách hàng",
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                    };

                    tradingCategory.Save();
                }

                if (!Util.isExistXpoObject<TradingCategory>("Code", "SUPPLIER"))
                {
                    TradingCategory tradingCategory = new TradingCategory(session)
                    {
                        Code = "SUPPLIER",
                        Name = "Nhà cung cấp",
                        Description = "Nhà cung cấp",
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                    };

                    tradingCategory.Save();
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

        public TradingCategory(Session session)
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

        // Fields...
        private ObjectType _ObjectTypeId;
        private short _RowStatus;
        private string _Description;
        private string _Name;
        private string _Code;
        private Guid _TradingCategoryId;

        [Key(true)]
        public Guid TradingCategoryId
        {
            get
            {
                return _TradingCategoryId;
            }
            set
            {
                SetPropertyValue("TradingCategoryId", ref _TradingCategoryId, value);
            }
        }

        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                SetPropertyValue("Code", ref _Code, value);
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetPropertyValue("Name", ref _Name, value);
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                SetPropertyValue("Description", ref _Description, value);
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
                SetPropertyValue("RowStatus", ref _RowStatus, value);
            }
        }

        [Association(@"OrganizationCategoryReferencesTradingCategory")]
        public XPCollection<OrganizationCategory> OrganizationCategories
        {
            get
            {
                return GetCollection<OrganizationCategory>("OrganizationCategories");
            }
        }

        [Association("TradingCategoryReferencesObjectType")]
        public ObjectType ObjectTypeId
        {
            get
            {
                return _ObjectTypeId;
            }
            set
            {
                SetPropertyValue("ObjectTypeId", ref _ObjectTypeId, value);
            }
        }

    }

}