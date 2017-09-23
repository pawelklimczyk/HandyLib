// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLCrypter.cs" project="Gmtl.HandyLib" date="2017-09-23 08:21">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// HLCrypter encrypt and decrypt strings
    /// </summary>
    public class HLCrypter
    {
        static byte[] entropy = Encoding.Unicode.GetBytes("GMTL 2017 default entropy key");

        /// <summary>
        /// Set seed for encryption and decryption
        /// </summary>
        /// <param name="seed">new seed</param>
        public static void SetEntropy(string seed)
        {
            entropy = Encoding.Unicode.GetBytes(seed);
        }

        public static string EncryptString(string plainText)
        {
            SecureString input = ToSecureString(plainText);
            byte[] encryptedData = ProtectedData.Protect(
                Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static string DecryptString(string encryptedText)
        {
            try
            {
                byte[] decryptedData = ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedText),
                    entropy,
                    DataProtectionScope.CurrentUser);
                return Encoding.Unicode.GetString(decryptedData);
            }
            catch
            {
                return String.Empty;
            }
        }

        private static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();

            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();

            return secure;
        }

        private static string ToInsecureString(SecureString input)
        {
            string decryptedString = string.Empty;

            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                decryptedString = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }

            return decryptedString;
        }
    }
}