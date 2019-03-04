using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Clones an object
    /// </summary>
    public static class HLCloner
    {
        public static T CloneViaSerialization<T>(T obj)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException($"The type{typeof(T)} must be serializable.", "obj");
            }

            if (Object.ReferenceEquals(obj, null))
            {
                return default(T);
            }

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
