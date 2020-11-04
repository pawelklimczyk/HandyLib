using System;

namespace Gmtl.HandyLib.Validators
{
    public static class HLBankValidator
    {
        public static bool IsValidBankAccountNumber(this string input)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(input))
                {
                    return false;
                }

                input = input.Replace(" ", String.Empty);

                if (input.Length != 26 && input.Length != 32)
                {
                    return false;
                }

                const int countryCode = 2521;
                string checkSum = input.Substring(0, 2);
                string accountNumber = input.Substring(2);
                string reversedDigits = accountNumber + countryCode + checkSum;

                return ModString(reversedDigits, 97) == 1;
            }
            catch
            {
                return false;
            }
        }

        private static int ModString(string x, int y)
        {
            if (string.IsNullOrEmpty(x))
            {
                return 0;
            }

            string firstDigit = x.Substring(0, x.Length - 1); // first digits
            int lastDigit = int.Parse(x.Substring(x.Length - 1)); // last digit

            return ((ModString(firstDigit, y) * 10) + lastDigit) % y;
        }

    }

}
