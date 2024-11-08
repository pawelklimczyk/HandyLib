namespace Gmtl.HandyLib.Validators
{
    /// <summary>
    /// Phone numbers validator
    /// </summary>
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
                input = input.Replace(" ", string.Empty).Replace("-", string.Empty).Replace("+", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);
                char[] arr = input.ToCharArray();

                foreach (var c in input.ToCharArray())
                    if (!char.IsDigit(c))
                        return false;

                return input.Length >= 9;
            }
            catch
            {
                return false;
            }
        }
    }
}
