// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLBase64.cs" project="Gmtl.HandyLib" date="2016-09-17 12:46:53">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

namespace Gmtl.HandyLib
{
    public static class HLBase64
    {
        public static string Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Decode(string encodedText)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodedText);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        } 
    }
}