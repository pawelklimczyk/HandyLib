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

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Handy methods related to System.String
    /// </summary>
    public static class HLString
    {
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
        public static string ValueOrDefault(string input, string defaultValue)
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
        public static string ValueOrEmpty(string input)
        {
            return ValueOrDefault(input, String.Empty);
        }

        /// <summary>
        /// Returns truncated string
        /// </summary>
        public static string MaxOf(string input, int lettersCount, string suffix = "")
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

        public static string StripHtml(string input)
        {
            char[] array = new char[input.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < input.Length; i++)
            {
                char let = input[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        /// <summary>
        /// Removes non-standard letters like ę=>e
        /// </summary>
        public static string RemoveAccents(string input)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in input)
            {
                int len = sb.Length;

                foreach (var entry in foreign_characters.Where(entry => entry.Key.IndexOf(c) != -1))
                {
                    sb.Append(entry.Value);
                    break;
                }

                if (len == sb.Length)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts first letter in sentence to uppercase letter
        /// </summary>
        public static string FirstLetterToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("There is no first letter");

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        static Dictionary<string, string> foreign_characters = new Dictionary<string, string>
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
    }
}