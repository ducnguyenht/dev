using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.LegalInvoice
{
    public partial class LegalInvoiceArtifactIdentifierType : XPCustomObject
    {
        public LegalInvoiceArtifactIdentifierType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fLegalInvoiceArtifactIdentifierTypeId;
        [Key(true)]
        public Guid LegalInvoiceArtifactIdentifierTypeId
        {
            get { return fLegalInvoiceArtifactIdentifierTypeId; }
            set { SetPropertyValue<Guid>("LegalInvoiceArtifactIdentifierTypeId", ref fLegalInvoiceArtifactIdentifierTypeId, value); }
        }

        private string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        [Association(@"LegalInvoiceArtifactIdentifierReferencesLegalInvoiceArtifactIdentifierType", typeof(LegalInvoiceArtifactIdentifier))]
        public XPCollection<LegalInvoiceArtifactIdentifier> LegalInvoiceArtifactIdentifiers { get { return GetCollection<LegalInvoiceArtifactIdentifier>("LegalInvoiceArtifactIdentifiers"); } }
        #endregion

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Unit table
                if (!Util.isExistXpoObject<LegalInvoiceArtifactIdentifierType>("Name", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    LegalInvoiceArtifactIdentifierType IdentifierType = new LegalInvoiceArtifactIdentifierType(session)
                    {
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "Chưa Xác Định",
                        RowStatus = -1,
                    };
                    IdentifierType.Save();
                }
                string[] Identifier_name = { "SERIES","NUMBER","TEMPLATE" };
                string[] Identifier_description = { "Số Sêri","Số hóa đơn GTGT","Mẩu số" };

                LegalInvoiceArtifactIdentifierType IdentifierTypes = new LegalInvoiceArtifactIdentifierType(session);
                if (IdentifierTypes != null)
                {
                    for (int i = 0; i < Identifier_name.Length; i++)
                    {
                        if (!Util.isExistXpoObject<LegalInvoiceArtifactIdentifierType>("Name", Identifier_name[i]))
                        {
                            LegalInvoiceArtifactIdentifierType identifier = new LegalInvoiceArtifactIdentifierType(session);
                            identifier.Name = Identifier_name[i];
                            identifier.Description = Identifier_description[i];
                            identifier.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                            identifier.Save();
                        }
                    }
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
    }
}
