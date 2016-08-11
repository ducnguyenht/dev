using System;
using DevExpress.Xpo;
using System.Linq;

namespace NAS.DAL.System.ArtifactCode
{

    public class CodeRuleDataFormat : XPCustomObject
    {

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                CodeRuleDataType.Populate();
                //insert default data into CodeRuleDataFormat table

                #region STRING
                
                //Get STRING data type
                CodeRuleDataType stringDataType =
                    Util.getXPCollection<CodeRuleDataType>(session, "Code", "STRING").FirstOrDefault();

                if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "NORMAL"))
                {
                    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                    {
                        Code = "NORMAL",
                        Name = "Không định dạng",
                        Description = "Không định dạng",
                        FormatString = "{0}",
                        CodeRuleDataTypeId = stringDataType
                    };
                    codeRuleDataFormat.Save();
                }

                if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "LOWERCASE"))
                {
                    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                    {
                        Code = "LOWERCASE",
                        Name = "Chữ thường",
                        Description = "Chữ thường",
                        FormatString = "{0}",
                        CodeRuleDataTypeId = stringDataType
                    };
                    codeRuleDataFormat.Save();
                }

                if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "UPPERCASE"))
                {
                    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                    {
                        Code = "UPPERCASE",
                        Name = "Chữ hoa",
                        Description = "Chữ hoa",
                        FormatString = "{0}",
                        CodeRuleDataTypeId = stringDataType
                    };
                    codeRuleDataFormat.Save();
                }

                //if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "ACSII"))
                //{
                //    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                //    {
                //        Code = "ACSII",
                //        Name = "Chữ không dấu",
                //        Description = "Chữ không dấu",
                //        FormatString = "{0}",
                //        CodeRuleDataTypeId = stringDataType
                //    };
                //    codeRuleDataFormat.Save();
                //}

                //if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "UPPERCASE_ACSII"))
                //{
                //    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                //    {
                //        Code = "UPPERCASE_ACSII",
                //        Name = "Chữ hoa không dấu",
                //        Description = "Chữ hoa không dấu",
                //        FormatString = "{0}",
                //        CodeRuleDataTypeId = stringDataType
                //    };
                //    codeRuleDataFormat.Save();
                //}

                //if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "LOWERCASE_ACSII"))
                //{
                //    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                //    {
                //        Code = "LOWERCASE_ACSII",
                //        Name = "Chữ thường không dấu",
                //        Description = "Chữ thường không dấu",
                //        FormatString = "{0}",
                //        CodeRuleDataTypeId = stringDataType
                //    };
                //    codeRuleDataFormat.Save();
                //}

                #endregion

                #region DATETIME
                //Get DATETIME data type
                CodeRuleDataType datetimeDataType =
                    Util.getXPCollection<CodeRuleDataType>(session, "Code", "DATETIME").FirstOrDefault();

                if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "YEAR"))
                {
                    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                    {
                        Code = "YEAR",
                        Name = "yyyy(năm)",
                        Description = "Năm",
                        FormatString = "{0:yyyy}",
                        CodeRuleDataTypeId = datetimeDataType
                    };
                    codeRuleDataFormat.Save();
                }

                if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "MONTH"))
                {
                    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                    {
                        Code = "MONTH",
                        Name = "MM(tháng)",
                        Description = "Tháng",
                        FormatString = "{0:MM}",
                        CodeRuleDataTypeId = datetimeDataType
                    };
                    codeRuleDataFormat.Save();
                }

                if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "DAY"))
                {
                    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                    {
                        Code = "DAY",
                        Name = "dd(ngày)",
                        Description = "Ngày",
                        FormatString = "{0:dd}",
                        CodeRuleDataTypeId = datetimeDataType
                    };
                    codeRuleDataFormat.Save();
                }

                if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "HOUR"))
                {
                    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                    {
                        Code = "HOUR",
                        Name = "HH(giờ)",
                        Description = "Giờ",
                        FormatString = "{0:HH}",
                        CodeRuleDataTypeId = datetimeDataType
                    };
                    codeRuleDataFormat.Save();
                }

                if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "MINUTE"))
                {
                    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                    {
                        Code = "MINUTE",
                        Name = "mm(phút)",
                        Description = "Phút",
                        FormatString = "{0:mm}",
                        CodeRuleDataTypeId = datetimeDataType
                    };
                    codeRuleDataFormat.Save();
                }

                if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "SECOND"))
                {
                    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                    {
                        Code = "SECOND",
                        Name = "ss(giây)",
                        Description = "Giây",
                        FormatString = "{0:ss}",
                        CodeRuleDataTypeId = datetimeDataType
                    };
                    codeRuleDataFormat.Save();
                }
                #endregion

                #region NUMBER
                //Get NUMBER data type
                CodeRuleDataType numberDataType =
                    Util.getXPCollection<CodeRuleDataType>(session, "Code", "NUMBER").FirstOrDefault();

                if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "DECIMAL"))
                {
                    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                    {
                        Code = "DECIMAL",
                        Name = "Số thập phân",
                        Description = "Số thập phân",
                        FormatString = "{0:D}",
                        CodeRuleDataTypeId = numberDataType
                    };
                    codeRuleDataFormat.Save();
                }

                if (!Util.isExistXpoObject<CodeRuleDataFormat>("Code", "DECIMAL_WITH_GROUP"))
                {
                    CodeRuleDataFormat codeRuleDataFormat = new CodeRuleDataFormat(session)
                    {
                        Code = "DECIMAL_WITH_GROUP",
                        Name = "Số thập phân có dấu phân cách hàng nghìn",
                        Description = "Số thập phân có dấu phân cách hàng nghìn",
                        FormatString = "{0:#,###}",
                        CodeRuleDataTypeId = numberDataType
                    };
                    codeRuleDataFormat.Save();
                }
                #endregion
                

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

        public CodeRuleDataFormat(Session session)
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
        private CodeRuleDataType _CodeRuleDataTypeId;
        private string _Description;
        private string _Code;
        private string _Name;
        private string _FormatString;
        private Guid _CodeRuleDataFormatId;

        [Key(true)]
        public Guid CodeRuleDataFormatId
        {
            get
            {
                return _CodeRuleDataFormatId;
            }
            set
            {
                SetPropertyValue("CodeRuleDataFormatId", ref _CodeRuleDataFormatId, value);
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

        public string FormatString
        {
            get
            {
                return _FormatString;
            }
            set
            {
                SetPropertyValue("FormatString", ref _FormatString, value);
            }
        }


        [Association(@"CodeRuleDataFormatsReferencesCodeRuleDataType")]
        public CodeRuleDataType CodeRuleDataTypeId
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

        [Association(@"CodeRuleDataReferencesCodeRuleDataFormat")]
        public XPCollection<CodeRuleData> CodeRuleData
        {
            get
            {
                return GetCollection<CodeRuleData>("CodeRuleData");
            }
        }

    }

}