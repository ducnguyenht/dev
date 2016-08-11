using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Inventory.Command
{
    public enum DefaultInventoryCommandActorTypeEnum
    {
        /// <summary>
        /// Mặc định
        /// </summary>
        NAAN_DEFAULT,
        /// <summary>
        /// Người mua
        /// </summary>
        BUYER,
        /// <summary>
        /// Người lập
        /// </summary>
        CREATOR,
        /// <summary>
        /// Kế toán trưởng
        /// </summary>
        CHIEFACCOUNTANT,
        /// <summary>
        /// Giám đốc
        /// </summary>
        DIRECTOR,
        /// <summary>
        /// Thủ kho
        /// </summary>
        STOREKEEPER,
        /// <summary>
        /// Người giao
        /// </summary>
        SHIPPER,
        /// <summary>
        /// Người nhận
        /// </summary>
        RECEIVER
    }


    public class InventoryCommandActorType : XPCustomObject
    {
        public InventoryCommandActorType(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid _InventoryCommandActorTypeId;
        [Key(true)]
        public Guid InventoryCommandActorTypeId
        {
            get
            {
                return _InventoryCommandActorTypeId;
            }
            set
            {
                SetPropertyValue("InventoryCommandActorTypeId", ref _InventoryCommandActorTypeId, value);
            }
        }

        private string _Description;
        [Size(512)]
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

        private string _Name;
        [Size(255)]
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

        short _RowStatus;
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }

        [Association(@"InventoryCommandActorReferencesInventoryCommandActorType", typeof(InventoryCommandActor))]
        public XPCollection<InventoryCommandActor> InventoryCommandActors
        {
            get
            {
                return GetCollection<InventoryCommandActor>("InventoryCommandActors");
            }
        }

        public static void Populate() {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                session.BeginTransaction();
                if (!Util.isExistXpoObject<InventoryCommandActorType>("Name",
                    Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.NAAN_DEFAULT)))
                {
                    InventoryCommandActorType actorType = new InventoryCommandActorType(session)
                    {
                        Description = "Mặc định",
                        Name = Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.NAAN_DEFAULT),
                        RowStatus = -1
                    };
                    actorType.Save();
                }

                if (!Util.isExistXpoObject<InventoryCommandActorType>("Name",
                    Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.BUYER)))
                {
                    InventoryCommandActorType actorType = new InventoryCommandActorType(session)
                    {
                        Description = "Người mua",
                        Name = Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.BUYER),
                        RowStatus = 1
                    };
                    actorType.Save();
                }

                if (!Util.isExistXpoObject<InventoryCommandActorType>("Name",
                    Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.CHIEFACCOUNTANT)))
                {
                    InventoryCommandActorType actorType = new InventoryCommandActorType(session)
                    {
                        Description = "Kế toán trưởng",
                        Name = Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.CHIEFACCOUNTANT),
                        RowStatus = 1
                    };
                    actorType.Save();
                }

                if (!Util.isExistXpoObject<InventoryCommandActorType>("Name",
                    Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.DIRECTOR)))
                {
                    InventoryCommandActorType actorType = new InventoryCommandActorType(session)
                    {
                        Description = "Giám đốc",
                        Name = Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.DIRECTOR),
                        RowStatus = 1
                    };
                    actorType.Save();
                }

                if (!Util.isExistXpoObject<InventoryCommandActorType>("Name",
                    Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.CREATOR)))
                {
                    InventoryCommandActorType actorType = new InventoryCommandActorType(session)
                    {
                        Description = "Người tạo",
                        Name = Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.CREATOR),
                        RowStatus = 1
                    };
                    actorType.Save();
                }


                if (!Util.isExistXpoObject<InventoryCommandActorType>("Name",
                    Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.STOREKEEPER)))
                {
                    InventoryCommandActorType actorType = new InventoryCommandActorType(session)
                    {
                        Description = "Thủ kho",
                        Name = Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.STOREKEEPER),
                        RowStatus = 1
                    };
                    actorType.Save();
                }

                if (!Util.isExistXpoObject<InventoryCommandActorType>("Name",
                    Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.RECEIVER)))
                {
                    InventoryCommandActorType actorType = new InventoryCommandActorType(session)
                    {
                        Description = "Người nhận",
                        Name = Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.RECEIVER),
                        RowStatus = 1
                    };
                    actorType.Save();
                }

                if (!Util.isExistXpoObject<InventoryCommandActorType>("Name",
                    Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.SHIPPER)))
                {
                    InventoryCommandActorType actorType = new InventoryCommandActorType(session)
                    {
                        Description = "Người giao",
                        Name = Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), DefaultInventoryCommandActorTypeEnum.SHIPPER),
                        RowStatus = 1
                    };
                    actorType.Save();
                }

                session.CommitTransaction();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public static InventoryCommandActorType GetDefault(Session session, DefaultInventoryCommandActorTypeEnum code)
        {
            try
            {
                return session.FindObject<InventoryCommandActorType>(
                    new BinaryOperator("Name", Enum.GetName(typeof(DefaultInventoryCommandActorTypeEnum), code)));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
