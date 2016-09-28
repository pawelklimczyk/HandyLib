using System.IO;
using System.Reflection;

namespace Gmtl.HandyLib
{
    public class HLDllEmbeddedResource
    {
        /// <summary>
        /// Return content of the file
        /// </summary>
        /// <param name="resourceName"> path to the file included as embedded resource. IMPORTANT: replace '\\' with '.' eg. Scripts\\run.bat => Scripts.run.bat</param>
        /// <param name="assembly">Assembly with resource</param>
        /// <returns>content of the file</returns>
        public static string GetTextResource(string resourceName, Assembly assembly)
        {
            using (Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + "." + resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Return content of the file in calling assembly
        /// </summary>
        /// <param name="resourceName"> path to the file included as embedded resource. IMPORTANT: replace '\\' with '.' eg. Scripts\\run.bat => Scripts.run.bat</param>
        /// <returns>content of the file</returns>
        public static string GetTextResource(string resourceName)
        {
            var assembly = Assembly.GetCallingAssembly();

            return GetTextResource(resourceName, assembly);
        }
    }
}