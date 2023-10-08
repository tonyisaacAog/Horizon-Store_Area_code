using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MyInfrastructure.Extensions
{
    public static class StringExtension
    {
        public static string ToIndicDigits(this string input)
        {
            return input.Replace('0', '\u0660')
                    .Replace('1', '\u0661')
                    .Replace('2', '\u0662')
                    .Replace('3', '\u0663')
                    .Replace('4', '\u0664')
                    .Replace('5', '\u0665')
                    .Replace('6', '\u0666')
                    .Replace('7', '\u0667')
                    .Replace('8', '\u0668')
                    .Replace('9', '\u0669');
        }

        public static string GetStringWithSpaces(this string input)
        {
            if (!string.IsNullOrEmpty(input))
                return Regex.Replace(
                   input,
                   "(?<!^)" +
                   "(" +
                   "  [A-Z][a-z] |" +
                   "  (?<=[a-z])[A-Z] |" +
                   "  (?<![A-Z])[A-Z]$" +
                   ")",
                   " $1",
                   RegexOptions.IgnorePatternWhitespace);
            else
                return string.Empty;
        }

        private static String UrlPathEncoder(this String str)
        {
            return HttpUtility.UrlPathEncode(str);
        }

        public static String UrlPathEncode(this String str)
        {
            return "attr:{href:&apos;" + str.UrlPathEncoder() + "&apos;}";
        }

        public static String UrlPathEncodeWithId(this String str,
            string Id)
        {
            return "attr:{href:&apos;" + str.UrlPathEncoder() + "&apos;+" +
                   Id + "}";
        }
    }
}
