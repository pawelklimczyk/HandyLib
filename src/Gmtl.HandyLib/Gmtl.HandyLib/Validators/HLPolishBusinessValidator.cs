using System;

namespace Gmtl.HandyLib.Validators
{
    public static class HLPolishBusinessValidator
    {
        public static bool IsValidNIP(this string input)
        {
            try
            {
                int[] weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
                bool result = false;
                if (String.IsNullOrWhiteSpace(input) || input.Length != 10)
                {
                    return result;
                }

                int controlSum = CalculateControlSum(input, weights);
                int controlNum = controlSum % 11;
                if (controlNum == 10)
                {
                    return result;
                }

                int lastDigit = int.Parse(input[input.Length - 1].ToString());

                return controlNum == lastDigit;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidREGON(this string input)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(input))
                {
                    return false;
                }

                int controlSum;
                if (input.Length == 7 || input.Length == 9)
                {
                    int[] weights = { 8, 9, 2, 3, 4, 5, 6, 7 };
                    int offset = 9 - input.Length;
                    controlSum = CalculateControlSum(input, weights, offset);
                }
                else if (input.Length == 14)
                {
                    int[] weights = { 2, 4, 8, 5, 0, 9, 7, 3, 6, 1, 2, 4, 8 };
                    controlSum = CalculateControlSum(input, weights);
                }
                else
                {
                    return false;
                }

                int controlNum = controlSum % 11;
                if (controlNum == 10)
                {
                    controlNum = 0;
                }

                int lastDigit = int.Parse(input[input.Length - 1].ToString());

                return controlNum == lastDigit;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPESEL(this string input)
        {
            try
            {
                int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
                bool result = false;

                if (String.IsNullOrWhiteSpace(input))
                {
                    return false;
                }

                if (input.Length == 11)
                {
                    int controlSum = CalculateControlSum(input, weights);
                    int controlNum = controlSum % 10;
                    controlNum = 10 - controlNum;
                    if (controlNum == 10)
                    {
                        controlNum = 0;
                    }

                    int lastDigit = int.Parse(input[input.Length - 1].ToString());
                    result = controlNum == lastDigit;
                }

                return result;
            }
            catch
            {
                return false;
            }
        }

        private static int CalculateControlSum(string input, int[] weights, int offset = 0)
        {
            int controlSum = 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                controlSum += weights[i + offset] * int.Parse(input[i].ToString());
            }

            return controlSum;
        }
    }
}
