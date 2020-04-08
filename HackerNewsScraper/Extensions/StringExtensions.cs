using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNewsScraper.Extensions
{
    public static class StringExtensions
    {
        public static string LimitLength(this string input, int length)
        {
            // Preserve nulls - extension methods compile to static methods, so null calls are possible.
            if (input == null)
            {
                return null;
            }

            int inputLength = input.Length;

            // If the string is short enough, return as is.
            if (inputLength <= length)
            {
                return input;
            }

            // Append long strings with an ellipsis
            if (inputLength > 16)
            {
                return $"{input.Substring(0, length - 3)}...";
            }

            return input.Substring(0, length);

        }
    }
}
