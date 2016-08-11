using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Invoice
{
    public enum BillActorTypeEnum
    {
        CREATOR,
        BUYER,
        SALES,
        CHIEFACCOUNTANT,
        DIRECTOR
    }

    public class BillActorType : XPCustomObject
    {
        Guid fBillActorTypeId;
        [Key(true)]
        public Guid BillActorTypeId
        {
            get { return fBillActorTypeId; }
            set { SetPropertyValue<Guid>("BillActorTypeId", ref fBillActorTypeId, value); }
        }

        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
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

        [Association(@"BillActorTypeReferencesBillActor", typeof(BillActor))]
        public XPCollection<BillActor> BillActors { get { return GetCollection<BillActor>("BillActors"); } }

        public BillActorType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        public static BillActorType GetDefault(Session session, BillActorTypeEnum code)
        {
            BillActorType ret = null;
            try
            {
                ret = session.FindObject<BillActorType>(
                    new BinaryOperator("Name", Enum.GetName(typeof(BillActorTypeEnum), code)));
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
                string billActorTypeName;
                session = XpoHelper.GetNewSession();
                //Insert CREATOR bill actor type
                billActorTypeName = Enum.GetName(typeof(BillActorTypeEnum), BillActorTypeEnum.CREATOR);
                if (!Util.isExistXpoObject<BillActorType>("Name", billActorTypeName))
                {
                    BillActorType billActorType = new BillActorType(session)
                    {
                        Name = billActorTypeName,
                        Description = "Người lập phiếu",
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                    };

                    billActorType.Save();
                }

                //Insert BUYER bill actor type
                billActorTypeName = Enum.GetName(typeof(BillActorTypeEnum), BillActorTypeEnum.BUYER);
                if (!Util.isExistXpoObject<BillActorType>("Name", billActorTypeName))
                {
                    BillActorType billActorType = new BillActorType(session)
                    {
                        Name = billActorTypeName,
                        Description = "Người mua hàng",
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                    };

                    billActorType.Save();
                }

                //Insert SALES bill actor type
                billActorTypeName = Enum.GetName(typeof(BillActorTypeEnum), BillActorTypeEnum.SALES);
                if (!Util.isExistXpoObject<BillActorType>("Name", billActorTypeName))
                {
                    BillActorType billActorType = new BillActorType(session)
                    {
                        Name = billActorTypeName,
                        Description = "Người bán hàng",
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                    };

                    billActorType.Save();
                }

                //Insert CHIEFACCOUNTANT bill actor type
                billActorTypeName = Enum.GetName(typeof(BillActorTypeEnum), BillActorTypeEnum.CHIEFACCOUNTANT);
                if (!Util.isExistXpoObject<BillActorType>("Name", billActorTypeName))
                {
                    BillActorType billActorType = new BillActorType(session)
                    {
                        Name = billActorTypeName,
                        Description = "Kế toán trưởng",
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                    };

                    billActorType.Save();
                }

                //Insert DIRECTOR bill actor type
                billActorTypeName = Enum.GetName(typeof(BillActorTypeEnum), BillActorTypeEnum.DIRECTOR);
                if (!Util.isExistXpoObject<BillActorType>("Name", billActorTypeName))
                {
                    BillActorType billActorType = new BillActorType(session)
                    {
                        Name = billActorTypeName,
                        Description = "Giám đốc",
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                    };

                    billActorType.Save();
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
