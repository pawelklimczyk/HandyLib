// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLCloner.cs" project="Gmtl.HandyLib" date="2019-03-28 07:21">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Clones an object
    /// </summary>
    public static class HLCloner
    {
        /// <summary>
        /// Clones an object using BinaryFormatter
        /// Type must be marked with [Serializable] attribute
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="obj">Actual object to clone</param>
        /// <returns></returns>
        public static T CloneViaSerialization<T>(T obj)
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException(string.Format("The type {0} must be serializable.", typeof(T)), "obj");

            if (ReferenceEquals(obj, null)) return default(T);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}