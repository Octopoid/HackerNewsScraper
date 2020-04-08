namespace HackerNewsScraper.Extensions
{
    using System.Text.RegularExpressions;
    using HtmlAgilityPack;

    internal static class HtmlNodeExtensions
    {
        public static string ParseInnerText(this HtmlNode node, string xPath, Regex regexRemove = null)
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
