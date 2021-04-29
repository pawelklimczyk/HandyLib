using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Gmtl.HandyLib.Extensions
{
    /// <summary>
    /// Extensions for Enum
    /// </summary>
    public static class HLEnumExtensions
    {
        /// <summary>
        /// Return Description attribute value
        /// </summary>
        public static string GetDescription(this Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()?
                    .GetCustomAttribute<DescriptionAttribute>()?
                    .Description
                    ?? value.ToString();
        }
     
        /// <summary>
        /// Get list of values in enum
        /// </summary>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
