using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Core.Common.Utility
{
    public class SerializeHelper
    {
        private string Utf8ByteArrayToString(byte[] characters)
        {
            var encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private Byte[] StringToUtf8ByteArray(string pXmlString)
        {
            var encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }


        public string SerializeObject<T>(T obj)
        {
            string xmlString = null;
            var memoryStream = new MemoryStream();
            var xs = new XmlSerializer(typeof(T));
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(xmlTextWriter, obj);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            xmlString = Utf8ByteArrayToString(memoryStream.ToArray()); return xmlString;

        }


        public T DeserializeObject<T>(string xml)
        {
            var xs = new XmlSerializer(typeof(T));
            var memoryStream = new MemoryStream(StringToUtf8ByteArray(xml));
            return (T)xs.Deserialize(memoryStream);
        }



        public static string SerializeJsonObject<T>(T source)
        {
            if (ReferenceEquals(source, null))
            {
                return string.Empty;
            }

            return JsonConvert.SerializeObject(source);

        }

        public static T CloneJson<T>(T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }

    }
}