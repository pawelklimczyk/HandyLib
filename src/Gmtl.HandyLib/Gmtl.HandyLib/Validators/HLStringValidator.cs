using System.Text.RegularExpressions;

namespace Gmtl.HandyLib.Validators
{
    public static class HLStringValidator
    {
        // private static string _pattern = @"(?<Protocol>https?://)?(?<Domain>[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,}))|(?<Link>\[([^\]]+)\]\((https?://[^\)]+)\))";
        private static string _pattern = @"/(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z]{2,}(\.[a-zA-Z]{2,})(\.[a-zA-Z]{2,})?\/[a-zA-Z0-9]{2,}|((https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z]{2,}(\.[a-zA-Z]{2,})(\.[a-zA-Z]{2,})?)|(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}(\.[a-zA-Z0-9]{2,})?/g";
        private static Regex _regex = new Regex(_pattern, RegexOptions.Compiled);

        /// <summary>
        /// Check if input contains website addresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ContainsWebsiteUrl(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            MatchCollection matches = _regex.Matches(input);

            return matches.Count > 0;

        }
    }
}
