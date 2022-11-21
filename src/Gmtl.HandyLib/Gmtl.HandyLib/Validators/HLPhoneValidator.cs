using System;

namespace Gmtl.HandyLib.Validators
{
    public static class HLPhoneValidator
    {
        /// <summary>
        /// Test if provided string is valid phone number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValidPhoneNumber(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            try
            {
                input = input.Replace(" ", String.Empty);
                char[] arr = input.ToCharArray();

                foreach (var c in input.ToCharArray())
                    if (!char.IsDigit(c))
                        return false;

                if (input.Length < 9)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
