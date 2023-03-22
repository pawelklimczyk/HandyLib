// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLString.cs" project="Gmtl.HandyLib" date="2015-10-22 20:23:01">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Handy methods related to System.String
    /// </summary>
    public static class HLString
    {
        private static Dictionary<string, Regex> _replaceMultipleRegexCache = new Dictionary<string, Regex>();
        private static Regex _unicodeRegex = new Regex(@"[\u0000-\u0008\u000A-\u001F]", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
        private static object _replaceMultipleRegexCacheLock = new object();

        static HLString()
        {
            _replaceMultipleRegexCache.Add(" ", CreateNewReplacedMultipleRegex(" "));
        }

        /// <summary>
        /// Returns input if it's not null or whitespace, defaultValue otherwise
        /// </summary>
        /// <remarks>
        /// <code>
        /// HLString.ValueOrDefault("test", "replaced"); //'test' returned
        /// HLString.ValueOrDefault("", "replaced"); //'replaced' returned
        /// HLString.ValueOrDefault(null, "replaced"); //'replaced' returned
        /// </code>
        /// </remarks>
        public static string ValueOrDefault(this string input, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
        }

        /// <summary>
        /// Returns input if it's not null or whitespace, String.Empty otherwise
        /// </summary>
        /// <remarks>
        /// <code>
        /// HLString.ValueOrEmpty("test"); //'test' returned
        /// HLString.ValueOrEmpty(null); //'String.Empty' returned
        /// </code>
        /// </remarks>
        public static string ValueOrEmpty(this string input)
        {
            return ValueOrDefault(input, String.Empty);
        }

        /// <summary>
        /// Returns truncated string
        /// </summary>
        public static string MaxOf(this string input, int lettersCount, string suffix = "")
        {
            string sanitized = ValueOrDefault(input, String.Empty);

            if (sanitized.Length <= lettersCount)
                return sanitized;

            return sanitized.Substring(0, lettersCount) + suffix;
        }

        //public static string DecodeHtml(string input)
        //{
        //    //TODO
        //    return input;
        //}

        //public static string EncodeHtml(string input)
        //{
        //    //TODO
        //    return input;
        //}

        /// <summary>
        /// Removes HTML tags from input
        /// </summary>
        public static string StripHtml(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            char[] array = new char[input.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                if (currentChar == '<')
                {
                    inside = true;
                    continue;
                }
                if (currentChar == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = currentChar;
                    arrayIndex++;
                }
            }

            return new string(array, 0, arrayIndex);
        }

        /// <summary>
        /// Removes all attributes from HTML string
        /// </summary>
        public static string CleanHtml(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            char[] array = new char[input.Length];
            int arrayIndex = 0;
            bool inside = false;
            bool tagNameFinished = false;

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                if (currentChar == '<')
                {
                    inside = true;
                    tagNameFinished = false;
                }
                if (currentChar == '>')
                {
                    inside = false;
                    tagNameFinished = false;
                }
                if (inside && currentChar == ' ')
                {
                    tagNameFinished = true;
                }

                if (!inside
                    || (inside && (currentChar == '/' || currentChar == '<' || currentChar == '>'))
                    || (inside && !tagNameFinished)
                    )
                {
                    array[arrayIndex] = currentChar;
                    arrayIndex++;
                }

            }

            return new string(array, 0, arrayIndex);
        }

        /// <summary>
        /// Removes non-standard letters like ę=>e
        /// </summary>
        public static string RemoveAccents(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (char c in input)
            {
                int len = sb.Length;

                sb.Append(FindReplacement(c));

                if (len == sb.Length)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Removes all unicode characters from string
        /// Useful in XML deserialization
        /// </summary>
        public static string RemoveUnicode(this string input)
        {
            return _unicodeRegex.Replace(input, string.Empty);
        }

        /// <summary>
        /// Converts first letter in sentence to uppercase letter
        /// </summary>
        public static string FirstLetterToUpper(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        /// <summary>
        /// Replaces multiple characters with single
        /// </summary>
        public static string ReplaceMulti(this string name, string patternToReplace = " ")
        {
            if (string.IsNullOrEmpty(patternToReplace))
            {
                return name;
            }

            name = name.Trim();

            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            lock (_replaceMultipleRegexCacheLock)
            {
                if (!_replaceMultipleRegexCache.ContainsKey(patternToReplace))
                {
                    _replaceMultipleRegexCache.Add(patternToReplace, CreateNewReplacedMultipleRegex(patternToReplace));
                }
            }

            name = _replaceMultipleRegexCache[patternToReplace].Replace(name, patternToReplace);

            return name;
        }


        /// <summary>
        /// Creates http url friendly text from input
        /// </summary>
        /// <param name="input"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string ToSlug(this string input, int maxLength = 0)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var normalizedString = input
                .ToLowerInvariant()
                .Normalize(NormalizationForm.FormD);

            var stringBuilder = new StringBuilder();
            var stringLength = normalizedString.Length;
            var prevdash = false;
            var trueLength = 0;

            char c;

            for (int i = 0; i < stringLength; i++)
            {
                c = normalizedString[i];

                switch (CharUnicodeInfo.GetUnicodeCategory(c))
                {
                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        if (c < 128)
                            stringBuilder.Append(c);
                        else
                            stringBuilder.Append(FindReplacement(c));

                        prevdash = false;
                        trueLength = stringBuilder.Length;
                        break;

                    // Check if the character is to be replaced by a hyphen but only if the last character wasn't
                    case UnicodeCategory.SpaceSeparator:
                    case UnicodeCategory.ConnectorPunctuation:
                    case UnicodeCategory.DashPunctuation:
                    case UnicodeCategory.OtherPunctuation:
                    case UnicodeCategory.MathSymbol:
                        if (!prevdash)
                        {
                            stringBuilder.Append('-');
                            prevdash = true;
                            trueLength = stringBuilder.Length;
                        }
                        break;
                }

                // If we are at max length, stop parsing
                if (maxLength > 0 && trueLength >= maxLength)
                    break;
            }

            var result = stringBuilder.ToString().Trim('-');

            // Remove any excess character to meet maxlength criteria
            return maxLength <= 0 || result.Length <= maxLength ? result : result.Substring(0, maxLength);
        }

        private static string FindReplacement(char input)
        {
            foreach (var entry in _foreignCharacters.Where(entry => entry.Key.IndexOf(input) != -1))
            {
                return entry.Value;
            }

            return String.Empty;
        }

        static Dictionary<string, string> _foreignCharacters = new Dictionary<string, string>
        {
            { "äæǽ", "ae" },
            { "öœ", "oe" },
            { "ü", "ue" },
            { "Ä", "Ae" },
            { "Ü", "Ue" },
            { "Ö", "Oe" },
            { "ÀÁÂÃÄÅǺĀĂĄǍΑΆẢẠẦẪẨẬẰẮẴẲẶА", "A" },
            { "àáâãåǻāăąǎªαάảạầấẫẩậằắẵẳặа", "a" },
            { "Б", "B" },
            { "б", "b" },
            { "ÇĆĈĊČ", "C" },
            { "çćĉċč", "c" },
            { "Д", "D" },
            { "д", "d" },
            { "ÐĎĐΔ", "Dj" },
            { "ðďđδ", "dj" },
            { "ÈÉÊËĒĔĖĘĚΕΈẼẺẸỀẾỄỂỆЕЭ", "E" },
            { "èéêëēĕėęěέεẽẻẹềếễểệеэ", "e" },
            { "Ф", "F" },
            { "ф", "f" },
            { "ĜĞĠĢΓГҐ", "G" },
            { "ĝğġģγгґ", "g" },
            { "ĤĦ", "H" },
            { "ĥħ", "h" },
            { "ÌÍÎÏĨĪĬǏĮİΗΉΊΙΪỈỊИЫ", "I" },
            { "ìíîïĩīĭǐįıηήίιϊỉịиыї", "i" },
            { "Ĵ", "J" },
            { "ĵ", "j" },
            { "ĶΚК", "K" },
            { "ķκк", "k" },
            { "ĹĻĽĿŁΛЛ", "L" },
            { "ĺļľŀłλл", "l" },
            { "М", "M" },
            { "м", "m" },
            { "ÑŃŅŇΝН", "N" },
            { "ñńņňŉνн", "n" },
            { "ÒÓÔÕŌŎǑŐƠØǾΟΌΩΏỎỌỒỐỖỔỘỜỚỠỞỢО", "O" },
            { "òóôõōŏǒőơøǿºοόωώỏọồốỗổộờớỡởợо", "o" },
            { "П", "P" },
            { "п", "p" },
            { "ŔŖŘΡР", "R" },
            { "ŕŗřρр", "r" },
            { "ŚŜŞȘŠΣС", "S" },
            { "śŝşșšſσςс", "s" },
            { "ȚŢŤŦτТ", "T" },
            { "țţťŧт", "t" },
            { "ÙÚÛŨŪŬŮŰŲƯǓǕǗǙǛŨỦỤỪỨỮỬỰУ", "U" },
            { "ùúûũūŭůűųưǔǖǘǚǜυύϋủụừứữửựу", "u" },
            { "ÝŸŶΥΎΫỲỸỶỴЙ", "Y" },
            { "ýÿŷỳỹỷỵй", "y" },
            { "В", "V" },
            { "в", "v" },
            { "Ŵ", "W" },
            { "ŵ", "w" },
            { "ŹŻŽΖЗ", "Z" },
            { "źżžζз", "z" },
            { "ÆǼ", "AE" },
            { "ß", "ss" },
            { "Ĳ", "IJ" },
            { "ĳ", "ij" },
            { "Œ", "OE" },
            { "ƒ", "f" },
            { "ξ", "ks" },
            { "π", "p" },
            { "β", "v" },
            { "μ", "m" },
            { "ψ", "ps" },
            { "Ё", "Yo" },
            { "ё", "yo" },
            { "Є", "Ye" },
            { "є", "ye" },
            { "Ї", "Yi" },
            { "Ж", "Zh" },
            { "ж", "zh" },
            { "Х", "Kh" },
            { "х", "kh" },
            { "Ц", "Ts" },
            { "ц", "ts" },
            { "Ч", "Ch" },
            { "ч", "ch" },
            { "Ш", "Sh" },
            { "ш", "sh" },
            { "Щ", "Shch" },
            { "щ", "shch" },
            { "ЪъЬь", "" },
            { "Ю", "Yu" },
            { "ю", "yu" },
            { "Я", "Ya" },
            { "я", "ya" },
        };

        private static Regex CreateNewReplacedMultipleRegex(string pattern)
        {
            return new Regex(@"(" + Regex.Escape(pattern) + "){2,}", RegexOptions.None);
        }
    }
}