using System;
using System.Security.Cryptography;

namespace Gmtl.HandyLib
{
    public static class HLHash
    {
        private const int _saltSize = 32;
        private const int _iterations = 10000;
        private const int _keySize = 32;

        /// <summary>
        /// Computes has for given input
        /// The computed hash is different each time because the salt is generated dynamically
        /// </summary>
        /// <param name="input">input to hash</param>
        public static string ComputeHash(string input)
        {
            byte[] salt = GenerateSalt();

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, _iterations))
            {
                byte[] hash = pbkdf2.GetBytes(_keySize);
                return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            }
        }


        public static bool ValidateHash(string inpuHash, string input)
        {
            string[] parts = inpuHash.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hashToCheck = Convert.FromBase64String(parts[1]);

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, _iterations))
            {
                byte[] computedHash = pbkdf2.GetBytes(_keySize);
                return CompareByteArrays(hashToCheck, computedHash);
            }
        }

        public static string ComputeHashWithSalt(string input, byte[] salt)
        {
            byte[] hash = ComputeHashWithSaltBytes(input, salt);

            return Convert.ToBase64String(hash);
        }

        private static byte[] ComputeHashWithSaltBytes(string input, byte[] salt)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, _iterations))
            {
                return pbkdf2.GetBytes(_keySize);
            }
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[_saltSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
