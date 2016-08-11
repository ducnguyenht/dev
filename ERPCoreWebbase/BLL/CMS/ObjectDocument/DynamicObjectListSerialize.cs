using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NAS.BO.CMS.ObjectDocument
{
    /// <summary>
    /// Key: Id of ObjectCustomField class
    /// Value: Serialize data
    /// </summary>
    [Serializable]
    public class DynamicObjectListSerialize : Dictionary<string, DynamicObjectListSerializeDataItem>
    {
        public DynamicObjectListSerialize() : base() {

        }

        public DynamicObjectListSerialize(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public override string ToString()
        {
            string ret = String.Empty;
            if (Values != null)
            {
                var list = Values.Where(r => r.CustomFieldData != null && r.CustomFieldData.Trim().Length > 0);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (list.ToList().IndexOf(item) == 0)
                        {
                            ret = item.DisplayText;
                        }
                        else
                        {
                            ret += String.Format("|{0}", item.DisplayText);
                        }
                    }
                }
            }
            return ret;
        }

        public static void Serialize(DynamicObjectListSerialize dictionary, Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, dictionary);
        }

        public static DynamicObjectListSerialize Deserialize(Stream stream)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                return (DynamicObjectListSerialize)formatter.Deserialize(stream);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
