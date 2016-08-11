using System;
using DevExpress.Xpo;
using Utility;

namespace NAS.DAL.Accounting.Configure
{

    public class AllocationType : XPCustomObject
    {

        #region Logic

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into AllocationType table
                if (!Util.isExistXpoObject<AllocationType>("Code", "VOUCHER_RECEIPT"))
                {
                    AllocationType allocationType = new AllocationType(session)
                    {
                        Code = "VOUCHER_RECEIPT",
                        Name = "Phiếu thu",
                        Description = "Phiếu thu",
                        RowStatus = Constant.ROWSTATUS_ACTIVE
                    };
                    allocationType.Save();
                }

                if (!Util.isExistXpoObject<AllocationType>("Code", "VOUCHER_PAYMENT"))
                {
                    AllocationType allocationType = new AllocationType(session)
                    {
                        Code = "VOUCHER_PAYMENT",
                        Name = "Phiếu chi",
                        Description = "Phiếu chi",
                        RowStatus = Constant.ROWSTATUS_ACTIVE
                    };
                    allocationType.Save();
                }

                if (!Util.isExistXpoObject<AllocationType>("Code", "INVENTORY_INPUT"))
                {
                    AllocationType allocationType = new AllocationType(session)
                    {
                        Code = "INVENTORY_INPUT",
                        Name = "Phiếu nhập kho",
                        Description = "Phiếu nhập kho",
                        RowStatus = Constant.ROWSTATUS_ACTIVE
                    };
                    allocationType.Save();
                }

                if (!Util.isExistXpoObject<AllocationType>("Code", "INVENTORY_OUTPUT"))
                {
                    AllocationType allocationType = new AllocationType(session)
                    {
                        Code = "INVENTORY_OUTPUT",
                        Name = "Phiếu xuất kho",
                        Description = "Phiếu xuất kho",
                        RowStatus = Constant.ROWSTATUS_ACTIVE
                    };
                    allocationType.Save();
                }

                if (!Util.isExistXpoObject<AllocationType>("Code", "INVOICE_PURCHASE"))
                {
                    AllocationType allocationType = new AllocationType(session)
                    {
                        Code = "INVOICE_PURCHASE",
                        Name = "Phiếu mua hàng",
                        Description = "Phiếu mua hàng",
                        RowStatus = Constant.ROWSTATUS_ACTIVE
                    };
                    allocationType.Save();
                }

                if (!Util.isExistXpoObject<AllocationType>("Code", "INVOICE_SALE"))
                {
                    AllocationType allocationType = new AllocationType(session)
                    {
                        Code = "INVOICE_SALE",
                        Name = "Phiếu bán hàng",
                        Description = "Phiếu bán hàng",
                        RowStatus = Constant.ROWSTATUS_ACTIVE
                    };
                    allocationType.Save();
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

        public AllocationType(Session session)
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
        private short _RowStatus;
        private string _Name;
        private string _Description;
        private string _Code;
        private Guid _AllocationTypeId;

        [Key(true)]
        public Guid AllocationTypeId
        {
            get
            {
                return _AllocationTypeId;
            }
            set
            {
                SetPropertyValue("AllocationTypeId", ref _AllocationTypeId, value);
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

        [Association("AllocationRefencesAllcationType", typeof(Allocation))]
        public XPCollection<Allocation> Allocations
        {
            get
            {
                return GetCollection<Allocation>("Allocations");
            }
        }
        
    }

}