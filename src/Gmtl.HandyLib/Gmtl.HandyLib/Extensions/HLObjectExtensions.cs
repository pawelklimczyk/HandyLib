using System.Text;

namespace Gmtl.HandyLib.Extensions
{
    /// <summary>
    /// Useful extensions for System.Object
    /// </summary>
    public static class HLObjectExtensions
    {
        /// <summary>
        /// List all properties and their values 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>List of all properties and their values</returns>
        public static string PropertyList(this object obj)
        {
            var props = obj.GetType().GetProperties();
            var sb = new StringBuilder();

            foreach (var p in props)
            {
                sb.AppendLine(p.Name + ": " + p.GetValue(obj, null));
            }

            return sb.ToString();
        }
    }
}