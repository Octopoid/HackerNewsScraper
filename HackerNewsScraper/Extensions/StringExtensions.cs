namespace HackerNewsScraper.Extensions
{
    /// <summary>
    ///     Common extension methods for the <see cref="string"/> class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Limits the length of a given string. If the string is longer than 16 characters, an ellipsis is added.
        /// Null input strings are preserved.
        /// </summary>
        /// <param name="input">The input string to limit the length of.</param>
        /// <param name="length">The maximum length of the string. If the input string is longer than 16 characters, this maximum length includes 3 periods to form an ellipsis.</param>
        /// <returns>The length limited string.</returns>
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
