using System;
using System.Reflection;

namespace Gmtl.HandyLib
{
    public class HLSoftwareMetrics
    {
        public static string Title => GetAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title);
        public static string Copyright => GetAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright);
        public static string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static string Description => GetAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description);

        private static string GetAssemblyAttribute<T>(Func<T, string> value)
            where T : Attribute
        {
            T attribute = (T)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(T));
            return value.Invoke(attribute);
        }
    }
}