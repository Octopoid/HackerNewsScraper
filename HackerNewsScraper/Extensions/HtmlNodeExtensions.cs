namespace HackerNewsScraper.Extensions
{
    using System.Text.RegularExpressions;
    using HtmlAgilityPack;

    /// <summary>
    ///     Common extension methods for the <see cref="HtmlNode" /> class.
    /// </summary>
    public static class HtmlNodeExtensions
    {
        /// <summary>
        ///     Finds the single <see cref="HtmlNode" /> inside represented by the given xPath and reads the InnerText. If the
        ///     regular expression has been provided, it is then used to remove any matching text.
        /// </summary>
        /// <param name="node">The parent <see cref="HtmlNode" />.</param>
        /// <param name="xPath">The xPath to search the parent node with.</param>
        /// <param name="regexRemove">A regular expression which is used to find text to remove from the InnerText.</param>
        /// <returns>The selected inner nodes InnerText, after the optional regular expression has been used to remove matches.</returns>
        public static string ParseSingleNodeInnerText(this HtmlNode node, string xPath, string regexRemove)
        {
            return node.ParseSingleNodeInnerText(xPath, new Regex(regexRemove ?? string.Empty));
        }

        /// <summary>
        ///     Finds the single <see cref="HtmlNode" /> inside represented by the given xPath and reads the InnerText. If the
        ///     regular expression has been provided, it is then used to remove any matching text.
        /// </summary>
        /// <param name="node">The parent <see cref="HtmlNode" />.</param>
        /// <param name="xPath">The xPath to search the parent node with.</param>
        /// <param name="regexRemove">A regular expression which is used to find text to remove from the InnerText.</param>
        /// <returns>The selected inner nodes InnerText, after the optional regular expression has been used to remove matches.</returns>
        public static string ParseSingleNodeInnerText(this HtmlNode node, string xPath, Regex regexRemove = null)
        {
            // Find the node using the xPath.
            HtmlNode innerNode = node.SelectSingleNode(xPath);

            // If the xPath returns nothing, preserve the null result.
            if (innerNode == null)
            {
                return null;
            }

            // Get the inner text. Nulls are replaced with blank strings.
            string innerText = innerNode.InnerText ?? string.Empty;

            // If a regex is supplied, find any matches and remove them.
            if (regexRemove != null)
            {
                innerText = regexRemove.Replace(innerText, "");
            }

            return innerText;
        }
    }
}
