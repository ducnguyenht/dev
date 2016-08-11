using System;
using DevExpress.Xpo;

namespace NAS.DAL.System.ArtifactCode
{

    public class RuleRepeaterType : XPCustomObject
    {

        #region Logic

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into CodeRuleDataType table

                if (!Util.isExistXpoObject<RuleRepeaterType>("Code", "INFINITE"))
                {
                    RuleRepeaterType ruleRepeaterType = new RuleRepeaterType(session)
                    {
                        Code = "INFINITE",
                        Name = "Lặp vô hạn",
                        Description = "Lặp vô hạn" 
                    };
                    ruleRepeaterType.Save();
                }

                if (!Util.isExistXpoObject<RuleRepeaterType>("Code", "BY_YEAR"))
                {
                    RuleRepeaterType ruleRepeaterType = new RuleRepeaterType(session)
                    {
                        Code = "BY_YEAR",
                        Name = "Lặp theo năm",
                        Description = "Lặp theo năm"
                    };
                    ruleRepeaterType.Save();
                }

                if (!Util.isExistXpoObject<RuleRepeaterType>("Code", "BY_HALF_YEAR"))
                {
                    RuleRepeaterType ruleRepeaterType = new RuleRepeaterType(session)
                    {
                        Code = "BY_HALF_YEAR",
                        Name = "Lặp theo nửa năm",
                        Description = "Lặp theo nửa năm"
                    };
                    ruleRepeaterType.Save();
                }

                if (!Util.isExistXpoObject<RuleRepeaterType>("Code", "BY_QUARTER"))
                {
                    RuleRepeaterType ruleRepeaterType = new RuleRepeaterType(session)
                    {
                        Code = "BY_QUARTER",
                        Name = "Lặp theo quý",
                        Description = "Lặp theo quý"
                    };
                    ruleRepeaterType.Save();
                }

                if (!Util.isExistXpoObject<RuleRepeaterType>("Code", "BY_MONTH"))
                {
                    RuleRepeaterType ruleRepeaterType = new RuleRepeaterType(session)
                    {
                        Code = "BY_MONTH",
                        Name = "Lặp theo tháng",
                        Description = "Lặp theo tháng"
                    };
                    ruleRepeaterType.Save();
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

        public RuleRepeaterType(Session session)
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
        private string _Description;
        private string _Name;
        private string _Code;
        private Guid _RuleRepeaterTypeId;

        [Key(true)]
        public Guid RuleRepeaterTypeId
        {
            get
            {
                return _RuleRepeaterTypeId;
            }
            set
            {
                SetPropertyValue("RuleRepeaterTypeId", ref _RuleRepeaterTypeId, value);
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

        [Association(@"CodeRuleNumberDataReferencesRuleRepeaterType")]
        public XPCollection<CodeRuleNumberData> CodeRuleNumberData
        {
            get
            {
                return GetCollection<CodeRuleNumberData>("CodeRuleNumberData");
            }
        }

    }

}