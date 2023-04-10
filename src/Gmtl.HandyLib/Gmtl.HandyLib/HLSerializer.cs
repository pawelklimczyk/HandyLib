using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#if NETCOREAPP3_1_OR_GREATER
using System.Text.Json;
using System.Text.Json.Serialization;
#endif

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Serialization and deserialization helper class
    /// </summary>
    public static class HLSerializer
    {
        /// <summary>
        /// Serializes object to XML
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="objectToSerialize">Object to serialize</param>
        /// <param name="useNamespaces">If true, adds namespaces to output string</param>
        /// <returns>Serialized object</returns>
        public static string SerializeToXml<T>(this T objectToSerialize, bool useNamespaces = true)
        {
            if (objectToSerialize == null)
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
                            xmlSerializer.Serialize(xmlTextWriter, objectToSerialize, xmlSerializerNamespaces);
                        }
                        else
                            xmlSerializer.Serialize(xmlTextWriter, objectToSerialize);

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

        /// <summary>
        /// Serializes object and saves it into a file
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="objectToSerialize">Object to serialize</param>
        /// <param name="useNamespaces">If true, adds namespaces to output string</param>
        /// <param name="filename">Filename where save the serialized object</param>
        public static void SerializeToXmlFile<T>(this T objectToSerialize, string filename, bool useNamespaces = true)
        {
            try
            {
                File.WriteAllText(filename, SerializeToXml<T>(objectToSerialize, useNamespaces));
            }
            catch (Exception exception)
            {
                throw new Exception("Serialization error occured", exception);
            }
        }

        /// <summary>
        /// Deserializes string to specified object type
        /// </summary>
        /// <typeparam name="T">Expected object type</typeparam>
        /// <param name="xml">serialized object</param>
        /// <returns>Deserialized object</returns>
        public static T DeserializeFromXml<T>(this string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringReader stringReader = new StringReader(xml))
            {
                T xmlObject = (T)xmlSerializer.Deserialize(stringReader);
                return xmlObject;
            }
        }

        /// <summary>
        /// Deserializes object from specified filename
        /// </summary>
        /// <typeparam name="T">Expected object type</typeparam>
        /// <param name="filename">File storing serialized object</param>
        /// <returns>Deserialized object</returns>
        public static T DeserializeFromXmlFile<T>(this string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("File not found", filename);
            }

            return DeserializeFromXml<T>(File.ReadAllText(filename));
        }

#if NETCOREAPP3_1_OR_GREATER 
        public static string SerializeToJson<T>(this T objectToSerialize, JsonSerializerOptions options = null)
        {
            if (options == null)
            {
                options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    IgnoreNullValues = true,
                };
            }

            return JsonSerializer.Serialize(objectToSerialize, options);
        }

        public static T DeserializeFromJson<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
#endif


    }
}
