using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Gmtl.HandyLib
{
    //TODO add XML documentation 

    public class HLSerializer
    {
        public static string SerializeToXml<T>(T xmlObject, bool useNamespaces = true)
        {
            if (xmlObject == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8) { Formatting = Formatting.Indented })
                    {
                        if (useNamespaces)
                        {
                            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                            xmlSerializerNamespaces.Add("", "");
                            xmlSerializer.Serialize(xmlTextWriter, xmlObject, xmlSerializerNamespaces);
                        }
                        else
                            xmlSerializer.Serialize(xmlTextWriter, xmlObject);

                        string output = Encoding.UTF8.GetString(memoryStream.ToArray());
                        string orderMark = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());

                        if (output.StartsWith(orderMark))
                        {
                            output = output.Remove(0, orderMark.Length);
                        }

                        return output;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Serialization error occured", exception);
            }
        }

        public static void SerializeToXmlFile<T>(T xmlObject, string filename, bool useNamespaces = true)
        {
            try
            {
                File.WriteAllText(filename, SerializeToXml<T>(xmlObject, useNamespaces));
            }
            catch
            {
                throw new Exception();
            }
        }

        public static T DeserializeFromXml<T>(string xml) where T : new()
        {
            T xmlObject = new T();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringReader stringReader = new StringReader(xml))
            {
                xmlObject = (T)xmlSerializer.Deserialize(stringReader);
                return xmlObject;
            }
        }
        public static T DeserializeFromXmlFile<T>(string filename) where T : new()
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException();
            }

            return DeserializeFromXml<T>(File.ReadAllText(filename));
        }
    }
}
