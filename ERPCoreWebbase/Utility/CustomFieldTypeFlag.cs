using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{

    public class CustomFieldTypeConstant
    {
        public const string CUSTOM_FIELD_TYPE_DEFAULT = "D";
        public const string CUSTOM_FIELD_TYPE_MASTER = "M";
        public const string CUSTOM_FIELD_TYPE_READONLY = "R";
        public const string CUSTOM_FIELD_TYPE_MASTER_READONLY = "MR";
    }

    public sealed class CustomFieldTypeFlag
    {
        private string value;

        public static readonly CustomFieldTypeFlag CUSTOM_FIELD_TYPE_DEFAULT =
            new CustomFieldTypeFlag(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_DEFAULT);
        public static readonly CustomFieldTypeFlag CUSTOM_FIELD_TYPE_MASTER =
            new CustomFieldTypeFlag(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER);
        public static readonly CustomFieldTypeFlag CUSTOM_FIELD_TYPE_READONLY =
            new CustomFieldTypeFlag(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY);
        public static readonly CustomFieldTypeFlag CUSTOM_FIELD_TYPE_MASTER_READONLY =
            new CustomFieldTypeFlag(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY);

        private CustomFieldTypeFlag(string v)
        {
            value = v;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public string Value { get { return value; } }

        public static CustomFieldTypeFlag Parse(string flagString)
        {
            switch (flagString)
            {
                case CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_DEFAULT:
                    return CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT;
                case CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER:
                    return CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER;
                case CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY:
                    return CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY;
                case CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY:
                    return CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER_READONLY;
                default:
                    throw new Exception("The specific flag is not supported");
            }
        }
    }

}
