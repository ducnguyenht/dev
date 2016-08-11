using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Interface;
using NAS.DAL.Accounting.Configure;
using NAS.DAL.Vouches.Allocation;



namespace NAS.DAL.Staging.Accounting.Journal
{

    public enum AccountActorTypeEnum
    {
        ORGANIZATION,
        DEPARTMENT,
        PERSON,
        CUSTOMER,
        MANUFACTURER,
        SUPPLIER,
        INVENTORY,
        LOT,
        BILL
    }

    public partial class AccountActorType : XPCustomObject, IDALValidate
    {
        #region Logic
        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into AllocationType table
                if (!Util.isExistXpoObject<AccountActorType>("Code", "ORGANIZATION"))
                {
                    AccountActorType accountActorType = new AccountActorType(session)
                    {
                        Code = "ORGANIZATION",
                        Name = "Tổ chức",
                        Description = "Tổ chức"
                    };
                    accountActorType.Save();
                }

                if (!Util.isExistXpoObject<AccountActorType>("Code", "DEPARTMENT"))
                {
                    AccountActorType accountActorType = new AccountActorType(session)
                    {
                        Code = "DEPARTMENT",
                        Name = "Phòng ban",
                        Description = "Phòng ban"
                    };
                    accountActorType.Save();
                }

                if (!Util.isExistXpoObject<AccountActorType>("Code", "PERSON"))
                {
                    AccountActorType accountActorType = new AccountActorType(session)
                    {
                        Code = "PERSON",
                        Name = "Nhân viên",
                        Description = "Nhân viên"
                    };
                    accountActorType.Save();
                }

                if (!Util.isExistXpoObject<AccountActorType>("Code", "CUSTOMER"))
                {
                    AccountActorType accountActorType = new AccountActorType(session)
                    {
                        Code = "CUSTOMER",
                        Name = "Khách hàng",
                        Description = "Khách hàng"
                    };
                    accountActorType.Save();
                }

                if (!Util.isExistXpoObject<AccountActorType>("Code", "MANUFACTURER"))
                {
                    AccountActorType accountActorType = new AccountActorType(session)
                    {
                        Code = "MANUFACTURER",
                        Name = "Nhà sản xuất",
                        Description = "Nhà sản xuất"
                    };
                    accountActorType.Save();
                }

                if (!Util.isExistXpoObject<AccountActorType>("Code", "SUPPLIER"))
                {
                    AccountActorType accountActorType = new AccountActorType(session)
                    {
                        Code = "SUPPLIER",
                        Name = "Nhà cung cấp",
                        Description = "Nhà cung cấp"
                    };
                    accountActorType.Save();
                }

                if (!Util.isExistXpoObject<AccountActorType>("Code", "INVENTORY"))
                {
                    AccountActorType accountActorType = new AccountActorType(session)
                    {
                        Code = "INVENTORY",
                        Name = "Kho",
                        Description = "Kho"
                    };
                    accountActorType.Save();
                }

                //if (!Util.isExistXpoObject<AccountActorType>("Code", "PRODUCT"))
                //{
                //    AccountActorType accountActorType = new AccountActorType(session)
                //    {
                //        Code = "PRODUCT",
                //        Name = "Hàng hóa",
                //        Description = "Hàng hóa"
                //    };
                //    accountActorType.Save();
                //}

                //if (!Util.isExistXpoObject<AccountActorType>("Code", "SERVICE"))
                //{
                //    AccountActorType accountActorType = new AccountActorType(session)
                //    {
                //        Code = "SERVICE",
                //        Name = "Dịch vụ",
                //        Description = "Dịch vụ"
                //    };
                //    accountActorType.Save();
                //}

                //if (!Util.isExistXpoObject<AccountActorType>("Code", "MATERIAL"))
                //{
                //    AccountActorType accountActorType = new AccountActorType(session)
                //    {
                //        Code = "MATERIAL",
                //        Name = "Nguyên vật liệu",
                //        Description = "Nguyên vật liệu"
                //    };
                //    accountActorType.Save();
                //}

                //if (!Util.isExistXpoObject<AccountActorType>("Code", "TOOL"))
                //{
                //    AccountActorType accountActorType = new AccountActorType(session)
                //    {
                //        Code = "TOOL",
                //        Name = "Công cụ dụng cụ",
                //        Description = "Công cụ dụng cụ"
                //    };
                //    accountActorType.Save();
                //}

                if (!Util.isExistXpoObject<AccountActorType>("Code", "LOT"))
                {
                    AccountActorType accountActorType = new AccountActorType(session)
                    {
                        Code = "LOT",
                        Name = "Lô",
                        Description = "Lô"
                    };
                    accountActorType.Save();
                }

                if (!Util.isExistXpoObject<AccountActorType>("Code", "BILL"))
                {
                    AccountActorType accountActorType = new AccountActorType(session)
                    {
                        Code = "BILL",
                        Name = "Hóa đơn",
                        Description = "Hóa đơn"
                    };
                    accountActorType.Save();
                }

                //if (!Util.isExistXpoObject<AccountActorType>("Code", "TAX"))
                //{
                //    AccountActorType accountActorType = new AccountActorType(session)
                //    {
                //        Code = "TAX",
                //        Name = "Thuế ",
                //        Description = "Thuế "
                //    };
                //    accountActorType.Save();
                //}

                //if (!Util.isExistXpoObject<AccountActorType>("Code", "PROJECT"))
                //{
                //    AccountActorType accountActorType = new AccountActorType(session)
                //    {
                //        Code = "PROJECT",
                //        Name = "Dự án ",
                //        Description = "Dự án"
                //    };
                //    accountActorType.Save();
                //}

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

        #region Properties
        public AccountActorType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private string _Code;
        private Guid fAccountActorTypeId;
        [Key(true)]
        public Guid AccountActorTypeId
        {
            get { return fAccountActorTypeId; }
            set { SetPropertyValue<Guid>("AccountActorTypeId", ref fAccountActorTypeId, value); }
        }
        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
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

        private string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        #endregion

        #region References

        [Association(@"AccountActorReferencesAccountActorType", typeof(AccountActor))]
        public XPCollection<AccountActor> AccountActors { get { return GetCollection<AccountActor>("AccountActors"); } }

        [Association(@"VoucherAllocationSubjectReferencesAccountActorType", typeof(VoucherAllocationSubject))]
        public XPCollection<VoucherAllocationSubject> VoucherAllocationSubjects { get { return GetCollection<VoucherAllocationSubject>("VoucherAllocationSubjects"); } }

        /*2013-11-27 Khoa.Truong DEL START*/
        //[Association(@"AllocationTypeTemplatesRefencesAccountActorType")]
        //public XPCollection<AllocationAccountTemplate> AllocationAccountTemplates
        //{
        //    get
        //    {
        //        return GetCollection<AllocationAccountTemplate>("AllocationAccountTemplates");
        //    }
        //}
        /*2013-11-27 Khoa.Truong DEL END*/

        [Association("AllocationAccountActorTypeReferencesAccountActorType"), Aggregated]
        public XPCollection<AllocationAccountActorType> AllocationAccountActorTypes
        {
            get
            {
                return GetCollection<AllocationAccountActorType>("AllocationAccountActorTypes");
            }
        }


#endregion

        #region validate database

        public bool ValidateParameter()
        {
            if (this.Name.Equals(string.Empty))
                return false;
            return true;
        }

        public bool ValidateUnique()
        {
            return true;
        }

        public bool IsExist()
        {
            return true;
            //throw new NotImplementedException();
        }

        protected override void OnSaving()
        {
            if (ValidateParameter())
            {
                if (ValidateUnique())
                    base.OnSaving();
            }
        }

        #endregion
    }
}
