// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLCloner.cs" project="Gmtl.HandyLib" date="2019-03-28 07:21">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

//using System;
using System.IO;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Clones an object
    /// </summary>
    public static class HLCloner
    {
        /// <summary>
        /// Clones an object using XmlSerializer
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="obj">Actual object to clone</param>
        /// <returns></returns>
        public static T CloneViaSerialization<T>(T obj)
        {
            if (ReferenceEquals(obj, null)) return default(T);
       
            //if (!typeof(T).IsSerializable)
            //    throw new ArgumentException(string.Format("The type {0} must be serializable.", typeof(T)), "obj");

            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new MemoryStream();
            //using (stream)
            //{
            //    formatter.Serialize(stream, obj);
            //    stream.Seek(0, SeekOrigin.Begin);

            //    return (T)formatter.Deserialize(stream);
            //}

            using (var ms = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                return (T)serializer.Deserialize(ms);
            }
        }
    }
}