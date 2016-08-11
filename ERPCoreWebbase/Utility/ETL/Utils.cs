using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.ServiceProcess;
using Microsoft.Win32;
using DevExpress.Xpo;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utility.ETL
{
    public class ETLUtils
    {
        public void logs(string filePath,string log)
        {            
            var process = new System.Diagnostics.Process();
            var startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C echo " + log + " >>"+filePath;
            process.StartInfo = startInfo;
            process.Start();
            
        }
        public string GetProcessRunningMutex(string processName)
        {
            string ret = null;
            try
            {
                ret = Utility.Constant.Process_Running_Mutex_Name_ETLProcess + "_" + processName;                
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
            }
            return ret;
        }
        public string GetProcessStopMutex(string processName)
        {
            string ret = null;
            try
            {
                ret = Utility.Constant.Process_Stop_Mutex_Name_ETLProcess + "_" + processName;
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
            }
            return ret;
        }
        public string GetValFromXML(string xmlPath, string tagName)
        {
            string ret = null;
            try
            {
                var elementReader = XElement.Load(xmlPath).Descendants(tagName).FirstOrDefault();
                if (elementReader != null)
                {
                    ret = elementReader.Value;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
            }
            return ret;
        }
        public void SetValForXML(string xmlPath,string Element, string tagName, string val)
        {
            try
            {
                var xmlFile = XDocument.Load(xmlPath);
                var pathConfig = xmlFile.Descendants(Element).FirstOrDefault();
                pathConfig.SetElementValue(tagName, val);
                xmlFile.Save(xmlPath);
            }
            catch (Exception)
            {                
                throw;
            }
            finally
            {
            }
        }
        public string GetServiceImagePath(string serviceName)
        {
            string ret = null;
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\services\NAS_Service");
                string servicepath = rk.GetValue("ImagePath").ToString();
                string serviceEXE = (servicepath.Split('\\').LastOrDefault()).Split('/').LastOrDefault();
                ret = servicepath.Remove(servicepath.Length - serviceEXE.Length, serviceEXE.Length);
            }
            catch (Exception)
            {
                ret = null;
            }
            finally
            {
            }

            return ret;
        }
        public byte[] ObjectTobyteArray(object obj, bool bCompress)
        {
            using (System.IO.Stream streamWrite = new System.IO.MemoryStream())
            {

                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryWrite
                                = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                //Serialize the object
                binaryWrite.Serialize(streamWrite, obj);
                streamWrite.Seek(0, System.IO.SeekOrigin.Begin);
                //Serialized object into byte array
                byte[] data = new byte[streamWrite.Length];
                streamWrite.Read(data, 0, (int)streamWrite.Length);

                if (!bCompress) return data;

                using (System.IO.Stream zipStream = new System.IO.MemoryStream())
                {

                    using (System.IO.Compression.GZipStream zip
                                        = new System.IO.Compression.GZipStream(
                                                zipStream, System.IO.Compression.CompressionMode.Compress, true))
                    {
                        //Compress the data
                        zip.Write(data, 0, data.Length);
                    }

                    zipStream.Seek(0, System.IO.SeekOrigin.Begin);
                    byte[] compressed = new byte[zipStream.Length + 4];//Extra 4 bytes for size
                    //Read the compressed data to array
                    zipStream.Read(compressed, 4, compressed.Length - 4);
                    //Save the uncompressed data length in first 4 bytes, used for decompression
                    System.Buffer.BlockCopy(BitConverter.GetBytes(data.Length), 0, compressed, 0, 4);

                    return compressed;
                }
            }
        }
        public object byteArrayToObject(byte[] data, bool bIsCompressed)
        {
            object obj = null;

            if (bIsCompressed)
            {

                //Read the compressed data, copy the reference
                byte[] zipData = data;

                //Get the size of the data
                int dataLength = BitConverter.ToInt32(zipData, 0);

                using (System.IO.MemoryStream memStream = new System.IO.MemoryStream())
                {
                    //Remove size from the compressed data
                    memStream.Write(zipData, 4, zipData.Length - 4);
                    memStream.Seek(0, System.IO.SeekOrigin.Begin);
                    using (System.IO.Compression.GZipStream zip
                                        = new System.IO.Compression.GZipStream(
                                                    memStream, System.IO.Compression.CompressionMode.Decompress))
                    {

                        data = new byte[dataLength];
                        //Decompress
                        zip.Read(data, 0, dataLength);
                    }
                }
            }

            //Deserialize
            System.IO.Stream streamWrite = new System.IO.MemoryStream(data);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryWrite
                            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            streamWrite.Seek(0, System.IO.SeekOrigin.Begin);
            obj = binaryWrite.Deserialize(streamWrite);

            return obj;
        }
        public static byte[] TobyteArray(XPCustomObject obj)
        {
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, obj);
            byte[] result = memorystream.ToArray();
            return result;
        }
        public static XPCustomObject ToXPCustomObject(byte[] byteArray)
        {
            MemoryStream memorystreamd = new MemoryStream(byteArray);
            BinaryFormatter bfd = new BinaryFormatter();
            XPCustomObject result = bfd.Deserialize(memorystreamd) as XPCustomObject;
            return result;
        }
        public static string ConvertByteArrayToString(Byte[] ByteOutput)
        {
            string StringOutput = System.Text.Encoding.UTF8.GetString(ByteOutput);
            return StringOutput;
        }
        public static byte[] ConvertStringToByte(string Input)
        {
            return System.Text.Encoding.UTF8.GetBytes(Input);
        }        
    }

    public class TestingExecption : Exception
    {
        public string myMessage;
    }
}
