using System;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;
using System.Linq;

namespace NAS.DAL.System.ArtifactCode
{

    public class ArtifactType : XPCustomObject
    {
        #region Logic
        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into ArtifactType table
                Organization organization = Util.getXPCollection<Organization>(session, "Code", "QUASAPHARCO").FirstOrDefault();

                if (!Util.isExistXpoObject<ArtifactType>("Code", "VOUCHER_RECEIPT"))
                {
                    ArtifactType artifactType = new ArtifactType(session) {
                        Code = "VOUCHER_RECEIPT",
                        Name = "Phiếu thu", 
                        OrganizationId = organization,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                    };
                    artifactType.Save();
                }

                if (!Util.isExistXpoObject<ArtifactType>("Code", "VOUCHER_PAYMENT"))
                {
                    ArtifactType artifactType = new ArtifactType(session)
                    {
                        Code = "VOUCHER_PAYMENT",
                        Name = "Phiếu chi",
                        OrganizationId = organization,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                    };
                    artifactType.Save();
                }

                if (!Util.isExistXpoObject<ArtifactType>("Code", "INVOICE_PURCHASE"))
                {
                    ArtifactType artifactType = new ArtifactType(session)
                    {
                        Code = "INVOICE_PURCHASE",
                        Name = "Phiếu mua hàng",
                        OrganizationId = organization,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                    };
                    artifactType.Save();
                }

                if (!Util.isExistXpoObject<ArtifactType>("Code", "INVOICE_SALE"))
                {
                    ArtifactType artifactType = new ArtifactType(session)
                    {
                        Code = "INVOICE_SALE",
                        Name = "Phiếu bán hàng",
                        OrganizationId = organization,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                    };
                    artifactType.Save();
                }
                //////////////////////////////////////////////////////////////////////
                if (!Util.isExistXpoObject<ArtifactType>("Code", "INVENTORY_INPUT"))
                {
                    ArtifactType artifactType = new ArtifactType(session)
                    {
                        Code = "INVENTORY_INPUT",
                        Name = "Phiếu nhập kho",
                        OrganizationId = organization,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                    };
                    artifactType.Save();
                }
                else {
                    ArtifactType artifactType = Util.getXPCollection<ArtifactType>(session, "Code", "INVENTORY_INPUT").FirstOrDefault();
                    artifactType.Name = "Phiếu nhập kho";
                    artifactType.Save();
                }
                /////////////////////////////////////////////////////////////////////
                if (!Util.isExistXpoObject<ArtifactType>("Code", "INVENTORY_OUTPUT"))
                {
                    ArtifactType artifactType = new ArtifactType(session)
                    {
                        Code = "INVENTORY_OUTPUT",
                        Name = "Phiếu xuất kho",
                        OrganizationId = organization,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                    };
                    artifactType.Save();
                }
                else
                {
                    ArtifactType artifactType = Util.getXPCollection<ArtifactType>(session, "Code", "INVENTORY_OUTPUT").FirstOrDefault();
                    artifactType.Name = "Phiếu xuất kho";
                    artifactType.Save();
                }
                //////////////////////////////////////////////////////////////
                if (!Util.isExistXpoObject<ArtifactType>("Code", "INVENTORY_MOVE"))
                {
                    ArtifactType artifactType = new ArtifactType(session)
                    {
                        Code = "INVENTORY_MOVE",
                        Name = "Phiếu chuyển kho",
                        OrganizationId = organization,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                    };
                    artifactType.Save();
                }
                else
                {
                    ArtifactType artifactType = Util.getXPCollection<ArtifactType>(session, "Code", "INVENTORY_MOVE").FirstOrDefault();
                    artifactType.Name = "Phiếu chuyển kho";
                    artifactType.Save();
                }
                /////////////////////////////////////////////////////////////////

                if (!Util.isExistXpoObject<ArtifactType>("Code", "TRANSACTION"))
                {
                    ArtifactType artifactType = new ArtifactType(session)
                    {
                        Code = "TRANSACTION",
                        Name = "Bút toán",
                        OrganizationId = organization,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                    };
                    artifactType.Save();
                }

                //////////////////////////////////////////////////////////////
                if (!Util.isExistXpoObject<ArtifactType>("Code", "INVENTORYTRANSACTION_MOVE"))
                {
                    ArtifactType artifactType = new ArtifactType(session)
                    {
                        Code = "INVENTORYTRANSACTION_MOVE",
                        Name = "Phiếu chuyển kho",
                        OrganizationId = organization,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                    };
                    artifactType.Save();
                }

                if (!Util.isExistXpoObject<ArtifactType>("Code", "INVENTORYTRANSACTION_OUTPUT"))
                {
                    ArtifactType artifactType = new ArtifactType(session)
                    {
                        Code = "INVENTORYTRANSACTION_OUTPUT",
                        Name = "Phiếu xuất kho",
                        OrganizationId = organization,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                    };
                    artifactType.Save();
                }

                if (!Util.isExistXpoObject<ArtifactType>("Code", "INVENTORYTRANSACTION_INPUT"))
                {
                    ArtifactType artifactType = new ArtifactType(session)
                    {
                        Code = "INVENTORYTRANSACTION_INPUT",
                        Name = "Phiếu nhập kho",
                        OrganizationId = organization,
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                    };
                    artifactType.Save();
                }
                //////////////////////////////////////////////////////////
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

        public ArtifactType(Session session)
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
        private Organization _OrganizationId;
        private short _RowStatus;
        private string _Code;
        private string _Name;
        private Guid _ArtifactTypeId;
        [Key(true)]
        public Guid ArtifactTypeId
        {
            get
            {
                return _ArtifactTypeId;
            }
            set
            {
                SetPropertyValue("ArtifactTypeId", ref _ArtifactTypeId, value);
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


        [Association(@"ArtifactTypeReferencesOrganization")]
        public Organization OrganizationId
        {
            get
            {
                return _OrganizationId;
            }
            set
            {
                SetPropertyValue("OrganizationId", ref _OrganizationId, value);
            }
        }

        [Association(@"ArtifactCodeRuleReferencesArtifactType")]
        public XPCollection<ArtifactCodeRule> ArtifactCodeRules
        {
            get
            {
                return GetCollection<ArtifactCodeRule>("ArtifactCodeRules");
            }
        }

        [Association(@"ReferenceArtifactsReferencesArtifactType")]
        public XPCollection<ReferenceArtifact> ReferenceArtifacts
        {
            get
            {
                return GetCollection<ReferenceArtifact>("ReferenceArtifacts");
            }
        }


    }

}