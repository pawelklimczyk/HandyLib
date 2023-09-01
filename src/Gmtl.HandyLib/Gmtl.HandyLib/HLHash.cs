using System;
using System.Security.Cryptography;

namespace Gmtl.HandyLib
{
    public static class HLHash
    {
        private const int SaltSize = 32;
        private const int Iterations = 10000;
        private const int KeySize = 32;

        public static string ComputeHash(string input)
        {
            byte[] salt = GenerateSalt();

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(KeySize);
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

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, Iterations))
            {
                byte[] computedHash = pbkdf2.GetBytes(KeySize);
                return CompareByteArrays(hashToCheck, computedHash);
            }
        }

        internal static string ComputeHashWithSalt(string input, byte[] salt)
        {
            byte[] hash = ComputeHashWithSaltBytes(input, salt);
            return Convert.ToBase64String(hash);
        }

        private static byte[] ComputeHashWithSaltBytes(string input, byte[] salt)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, Iterations))
            {
                return pbkdf2.GetBytes(KeySize);
            }
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];
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
