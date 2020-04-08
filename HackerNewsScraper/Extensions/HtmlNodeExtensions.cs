namespace HackerNewsScraper.Extensions
{
    using System.Text.RegularExpressions;
    using HtmlAgilityPack;

    /// <summary>
    ///     Common extension methods for the <see cref="HtmlNode"/> class.
    /// </summary>
    public static class HtmlNodeExtensions
    {
        /// <summary>
        /// Finds the single <see cref="HtmlNode"/> inside represented by the given xPath and reads the InnerText. If the regular expression has been provided, it is then used to remove any matching text.
        /// </summary>
        /// <param name="node">The parent <see cref="HtmlNode"/>.</param>
        /// <param name="xPath">The xPath to search the parent node with.</param>
        /// <param name="regexRemove">A regular expression which is used to find text to remove from the InnerText.</param>
        /// <returns>The selected inner nodes InnerText, after the optional regular expression has been used to remove matches.</returns>
        public static string ParseSingleNodeInnerText(this HtmlNode node, string xPath, string regexRemove)
        {
            return node.ParseSingleNodeInnerText(xPath, new Regex(regexRemove ?? string.Empty));
        }

        /// <summary>
        /// Finds the single <see cref="HtmlNode"/> inside represented by the given xPath and reads the InnerText. If the regular expression has been provided, it is then used to remove any matching text.
        /// </summary>
        /// <param name="node">The parent <see cref="HtmlNode"/>.</param>
        /// <param name="xPath">The xPath to search the parent node with.</param>
        /// <param name="regexRemove">A regular expression which is used to find text to remove from the InnerText.</param>
        /// <returns>The selected inner nodes InnerText, after the optional regular expression has been used to remove matches.</returns>
        public static string ParseSingleNodeInnerText(this HtmlNode node, string xPath, Regex regexRemove = null)
        {
            HtmlNode innerNode = node.SelectSingleNode(xPath);

            if (innerNode == null)
            {
                return null;
            }

            string innerText = innerNode.InnerText ?? string.Empty;

            if (regexRemove != null)
            {
                innerText = regexRemove.Replace(innerText, "");
            }

            return innerText;
        }
    }
}
