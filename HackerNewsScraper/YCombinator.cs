namespace HackerNewsScraper
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using HackerNewsScraper.Extensions;
    using HackerNewsScraper.Helpers;
    using HtmlAgilityPack;

    /// <summary>
    ///     An implementation of <see cref="IScraper" /> which targets YCombinator.
    /// </summary>
    public class YCombinator : IScraper
    {
        /// <inheritdoc />
        public NewsItemCollection Scrape(ScraperOptions options)
        {
            // If the base Uri is not valid return a null object.
            if (!UriHelper.IsValidHttpUri(options.Uri))
            {
                return null;
            }

            // Ensure the options are validated. Posts value is automatically adjusted as per the specified 1->100 range.
            options.Validate();

            // Create the objects used by the loop.
            HtmlWeb htmlWeb = new HtmlWeb();
            List<NewsItem> news = new List<NewsItem>();
            int newsItemsRemaining = options.Posts;
            int currentPage = 1;

            do
            {
                // The current uri has the current page number appended to the p querystring parameter.
                string currentUri = UriHelper.SetQuerystringParameter(options.Uri, "p", currentPage);

                // The Html document is loaded with the Nuget package Html Agility Pack.
                HtmlDocument htmlDocument = htmlWeb.Load(currentUri);

                // The main node for a news item is a tr with the class 'athing'. Get a collection of them.
                HtmlNodeCollection newsItemMainNodes = htmlDocument.DocumentNode.SelectNodes("//tr[@class='athing']");

                // Loop through each.
                foreach (HtmlNode newsItemMainNode in newsItemMainNodes)
                {
                    // Scrape the news item, and decrement the remaining count.
                    NewsItem newsItem = this.Scrape(options, newsItemMainNode);
                    news.Add(newsItem);

                    newsItemsRemaining--;

                    // If there are no items remaining, break out of the foreach.
                    if (newsItemsRemaining <= 0)
                    {
                        break;
                    }
                }

                // Increment the current page value.
                currentPage++;
            } while (newsItemsRemaining > 0); // Once there are no items remaining, exit the loop.

            return new NewsItemCollection(news);
        }

        /// <summary>
        ///     Scrapes a single news item using the provided <see cref="ScraperOptions" /> and <see cref="HtmlNode" />.
        /// </summary>
        /// <param name="options">The <see cref="ScraperOptions" /> to use. Used for the fallback Uri for relative Uris.</param>
        /// <param name="newsItemMainNode">
        ///     The main node containing <see cref="NewsItem" />. This is a tr node with the class
        ///     attribute set to 'athing'.
        /// </param>
        /// <returns>A single scraped <see cref="NewsItem" />.</returns>
        private NewsItem Scrape(ScraperOptions options, HtmlNode newsItemMainNode)
        {
            // Gets the rank. If this cannot be parsed, rank will be 0.
            uint.TryParse(newsItemMainNode.ParseSingleNodeInnerText(".//span[@class='rank']", new Regex(@"\.")), out uint rank);

            // Gets the title and the Uri for the NewsItem.
            HtmlNode storylinkNode = newsItemMainNode.SelectSingleNode(".//a[@class='storylink']");
            string title = storylinkNode?.InnerText ?? "Untitled";
            string uri = storylinkNode?.Attributes["href"]?.Value;

            // The following details are available on the next sibling tr.
            HtmlNode newsItemFooterNode = newsItemMainNode.NextSibling;

            // Gets the author. If this cannot be parsed, the author will be "Anonymous".
            string author = newsItemFooterNode.ParseSingleNodeInnerText(".//a[@class='hnuser']") ?? "Anonymous";

            // Gets the points. If this cannot be parsed, points will be 0.
            uint.TryParse(newsItemFooterNode.ParseSingleNodeInnerText(".//span[@class='score']", new Regex(" points")) ?? "0", out uint points);

            // Gets the comments. If this cannot be parsed, comments will be 0.
            uint.TryParse(newsItemFooterNode.ParseSingleNodeInnerText(".//td[@class='subtext']/a[last()]", new Regex("&nbsp;comments")) ?? "0", out uint comments);

            // Limit the title and author to 256 characters, and fix relative or invalid Uris.
            title = title.LimitLength(256);
            author = author.LimitLength(256);
            uri = UriHelper.EnsureUriAbsolute(uri, options.Uri);

            return new NewsItem(title,
                                uri,
                                author,
                                points,
                                comments,
                                rank);
        }
    }
}
