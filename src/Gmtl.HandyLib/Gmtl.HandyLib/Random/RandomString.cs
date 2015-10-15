using System;
using System.Text;

namespace Gmtl.HandyLib.Random
{
    internal class RandomString : IRandomString
    {
        private static readonly System.Random random = new System.Random((int)DateTime.Now.Ticks);
        private static string lowerCaseLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly int lettersCount;

        static RandomString()
        {
            lettersCount = lowerCaseLetters.Length;
        }

        public string Next()
        {
            return Next(1, random.Next());
        }

        public string Next(int max)
        {
            return Next(1, max);
        }

        public string NextExact(int exactStringLength)
        {
            return Next(exactStringLength, exactStringLength);
        }

        public string Next(int min, int max)
        {
            CheckContractInput(min, max);

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < random.Next(min, max + 1); i++)
            {
                builder.Append(lowerCaseLetters[random.Next(lettersCount)]);
            }

            return builder.ToString();
        }

        private void CheckContractInput(int min, int max)
        {
            if (min < 1)
            {
                throw new ArgumentException("'min' must be greater then 0", "min");
            }

            if (max < min)
            {
                throw new ArgumentException("'max' must be must be greater or equal to 'min'");
            }
        }
    }
}