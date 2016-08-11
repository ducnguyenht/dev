using System;
using DevExpress.Xpo;

namespace NAS.DAL.System.ArtifactCode
{

    public class CodeRuleDataType : XPCustomObject
    {

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into CodeRuleDataType table

                if (!Util.isExistXpoObject<CodeRuleDataType>("Code", "STRING"))
                {
                    CodeRuleDataType codeRuleDataType = new CodeRuleDataType(session)
                    {
                        Code = "STRING",
                        Name = "Chuỗi",
                        Description = "Chuỗi kí tự"
                    };
                    codeRuleDataType.Save();
                }

                if (!Util.isExistXpoObject<CodeRuleDataType>("Code", "DATETIME"))
                {
                    CodeRuleDataType codeRuleDataType = new CodeRuleDataType(session)
                    {
                        Code = "DATETIME",
                        Name = "Ngày tháng",
                        Description = "Ngày tháng"
                    };
                    codeRuleDataType.Save();
                }

                if (!Util.isExistXpoObject<CodeRuleDataType>("Code", "NUMBER"))
                {
                    CodeRuleDataType codeRuleDataType = new CodeRuleDataType(session)
                    {
                        Code = "NUMBER",
                        Name = "Số",
                        Description = "Số"
                    };
                    codeRuleDataType.Save();
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


        public CodeRuleDataType(Session session)
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
        private string _Code;
        private string _Name;
        private string _Description;
        private Guid _CodeRuleDataTypeId;

        [Key(true)]
        public Guid CodeRuleDataTypeId
        {
            get
            {
                return _CodeRuleDataTypeId;
            }
            set
            {
                SetPropertyValue("CodeRuleDataTypeId", ref _CodeRuleDataTypeId, value);
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

        [Association(@"CodeRuleDefinitionsReferencesCodeRuleDataType")]
        public XPCollection<CodeRuleDefinition> CodeRuleDefinitions
        {
            get
            {
                return GetCollection<CodeRuleDefinition>("CodeRuleDefinitions");
            }
        }

        [Association(@"ReferenceArtifactsReferencesCodeRuleDataType")]
        public XPCollection<ReferenceArtifact> ReferenceArtifacts
        {
            get
            {
                return GetCollection<ReferenceArtifact>("ReferenceArtifacts");
            }
        }

        [Association(@"CodeRuleDataFormatsReferencesCodeRuleDataType")]
        public XPCollection<CodeRuleDataFormat> CodeRuleDataFormats
        {
            get
            {
                return GetCollection<CodeRuleDataFormat>("CodeRuleDataFormats");
            }
        }

    }

}