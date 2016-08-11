using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Utility.ETL
{
    #region Serialize
    /// <summary>
    /// Data structure
    /// A
    /// {
    ///     B
    ///     {
    ///        D
    ///        E[]         
    ///     }
    ///     C
    ///     {
    ///        F[]
    ///     }
    /// }
    /// 
    /// How to serialize and deserialize Class_a????
    /// exam:
    /// //Serialize
    /// Class_a template_A;
    /// string serializeResult = null;
    /// serializeResult = Class_a.SerializeToBase64String(template_A); //Save this string to Database or File.
    /// 
    /// //Deserialize
    /// Class_a template_A_deser;
    /// template_A_deser = Class_a.DeserializeFromBase64String(serializeResult);
    /// </summary>

    [Serializable]
    public class Class_A
    {
        #region Serialize
        public static string SerializeToBase64String(Class_A edgeType)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, edgeType);
                ms.Seek(0, SeekOrigin.Begin);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static Class_A DeserializeFromBase64String(String str)
        {
            using (var ms = new MemoryStream(Convert.FromBase64String(str)))
            {
                ms.Seek(0, SeekOrigin.Begin);
                return (Class_A)new BinaryFormatter().Deserialize(ms);
            }
        }
        #endregion

        #region Field

        public Class_B elementB;
        public Class_C elementC;

        public string Code;
        public string Name;
        #endregion
    }

    [Serializable]
    public class Class_B
    {
        #region Serialize
        public static string SerializeToBase64String(Class_B triangle)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, triangle);
                ms.Seek(0, SeekOrigin.Begin);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static Class_B DeserializeFromBase64String(String str)
        {
            using (var ms = new MemoryStream(Convert.FromBase64String(str)))
            {
                ms.Seek(0, SeekOrigin.Begin);
                return (Class_B)new BinaryFormatter().Deserialize(ms);
            }
        }
        #endregion

        #region Field

        public Class_D elementD;
        public List<Class_E> elementListE;

        public string Code;
        public string Name;
        #endregion
    }

    [Serializable]
    public class Class_C
    {
        #region Serialize
        public static string SerializeToBase64String(Class_C triangleEdge)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, triangleEdge);
                ms.Seek(0, SeekOrigin.Begin);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static Class_C DeserializeFromBase64String(String str)
        {
            using (var ms = new MemoryStream(Convert.FromBase64String(str)))
            {
                ms.Seek(0, SeekOrigin.Begin);
                return (Class_C)new BinaryFormatter().Deserialize(ms);
            }
        }
        #endregion

        #region Field

        public List<Class_F> elementListF;

        #endregion
    }
    public class Class_D
    {
        #region Serialize
        public static string SerializeToBase64String(Class_D edgeType)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, edgeType);
                ms.Seek(0, SeekOrigin.Begin);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static Class_D DeserializeFromBase64String(String str)
        {
            using (var ms = new MemoryStream(Convert.FromBase64String(str)))
            {
                ms.Seek(0, SeekOrigin.Begin);
                return (Class_D)new BinaryFormatter().Deserialize(ms);
            }
        }
        #endregion

        #region Field

        public string Code;
        public string Name;
        #endregion
    }

    [Serializable]
    public class Class_E
    {
        #region Serialize
        public static string SerializeToBase64String(Class_E triangle)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, triangle);
                ms.Seek(0, SeekOrigin.Begin);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static Class_E DeserializeFromBase64String(String str)
        {
            using (var ms = new MemoryStream(Convert.FromBase64String(str)))
            {
                ms.Seek(0, SeekOrigin.Begin);
                return (Class_E)new BinaryFormatter().Deserialize(ms);
            }
        }
        #endregion

        #region Field

        public string Code;
        public string Name;
        #endregion
    }

    [Serializable]
    public class Class_F
    {
        #region Serialize
        public static string SerializeToBase64String(Class_F triangleEdge)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, triangleEdge);
                ms.Seek(0, SeekOrigin.Begin);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static Class_F DeserializeFromBase64String(String str)
        {
            using (var ms = new MemoryStream(Convert.FromBase64String(str)))
            {
                ms.Seek(0, SeekOrigin.Begin);
                return (Class_F)new BinaryFormatter().Deserialize(ms);
            }
        }
        #endregion

        #region Field
        #endregion
    }
    #endregion

    class SerializeToBase64
    {
    }
}
