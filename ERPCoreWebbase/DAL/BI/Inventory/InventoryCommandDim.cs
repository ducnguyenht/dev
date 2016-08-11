using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.GoodsInInventory;
using NAS.DAL.BI.Accounting.GoodsInTransit;
using DevExpress.Data.Filtering;

namespace NAS.DAL.BI.Inventory
{
    public enum InventoryCommandDimEnum
    {
        NAAN_DEFAULT,
        UNKNOWN
    }

    public class InventoryCommandDim: XPCustomObject
    {
        public InventoryCommandDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        public static InventoryCommandDim GetDefault(Session session, InventoryCommandDimEnum code)
        {
            InventoryCommandDim ret = null;
            try
            {
                ret = session.FindObject<InventoryCommandDim>(
                    new BinaryOperator("Code", Enum.GetName(typeof(InventoryCommandDimEnum), code)));
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Organization table
                if (!Util.isExistXpoObject<InventoryCommandDim>("Code", Enum.GetName(typeof(InventoryCommandDimEnum), InventoryCommandDimEnum.NAAN_DEFAULT)))
                {
                    InventoryCommandDim command = new InventoryCommandDim(session)
                    {
                        Description = "Default inventory command",
                        Name = "Default inventory command",
                        RowStatus = 1,
                        Code = Enum.GetName(typeof(InventoryCommandDimEnum), InventoryCommandDimEnum.NAAN_DEFAULT),
                        IssueDate = DateTime.MinValue
                    };

                    command.Save();
                }

                if (!Util.isExistXpoObject<InventoryCommandDim>("Code", Enum.GetName(typeof(InventoryCommandDimEnum), InventoryCommandDimEnum.UNKNOWN)))
                {
                    InventoryCommandDim command = new InventoryCommandDim(session)
                    {
                        Description = "Unknown inventory command",
                        Name = "Unknown inventory command",
                        RowStatus = 1,
                        Code = Enum.GetName(typeof(InventoryCommandDimEnum), InventoryCommandDimEnum.UNKNOWN),
                        IssueDate = DateTime.MinValue
                    };

                    command.Save();
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

        #region Properties
        int fInventoryCommandDimId;
        [Key(true)]
        public int InventoryCommandDimId
        {
            get { return fInventoryCommandDimId; }
            set { SetPropertyValue<int>("InventoryCommandDimId", ref fInventoryCommandDimId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        DateTime fIssueDate;
        public DateTime IssueDate
        {
            get { return fIssueDate; }
            set { SetPropertyValue<DateTime>("IssueDate", ref fIssueDate, value); }
        }

        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        Guid fRefId;
        public Guid RefId
        {
            get { return fRefId; }
            set { SetPropertyValue<Guid>("RefId", ref fRefId, value); }
        }

        char fCommandType;
        public char CommandType
        {
            get { return fCommandType; }
            set { SetPropertyValue<char>("CommandType", ref fCommandType, value); }
        }

        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        #endregion

        #region Reference
        [Association(@"GoodsInInventoryDetailReferencesInventoryCommandDim")]
        public XPCollection<GoodsInInventoryDetail> GoodsInInventoryDetails
        {
            get { return GetCollection<GoodsInInventoryDetail>("GoodsInInventoryDetails"); }
        }

        [Association(@"GoodsInTransitForSaleDetailReferencesInventoryCommandDim")]
        public XPCollection<GoodsInTransitForSaleDetail> GoodsInTransitForSaleDetails
        {
            get { return GetCollection<GoodsInTransitForSaleDetail>("GoodsInTransitForSaleDetails"); }
        }

        [Association(@"ItemInventoryByArtifact-InventoryCommandDim", typeof(ItemInventoryByArtifact))]
        public XPCollection<ItemInventoryByArtifact> ItemInventoryByArtifacts { get { return GetCollection<ItemInventoryByArtifact>("ItemInventoryByArtifacts"); } }
        #endregion
    }
}
