// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLString.cs" project="Gmtl.HandyLib" date="2015-10-22 20:23:01">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

namespace Gmtl.HandyLib
{
    public static class HLString
    {
        /// <summary>
        /// Returns input if it's not null or whitespace
        /// </summary>
        public static string ValueOrDefault(string input, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
        }
    }
}