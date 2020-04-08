namespace HackerNewsScraper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using HackerNewsScraper.Extensions;
    using HackerNewsScraper.Helpers;
    using HtmlAgilityPack;

    public class NewsItemCollection
    {
        private List<NewsItem> items;

        public NewsItemCollection()
        {
            this.items = new List<NewsItem>();
        }

        public IEnumerable<NewsItem> Items { get { return this.items; } }

        public static NewsItemCollection Scrape(Program.Options options)
        {
            HtmlWeb htmlWeb = new HtmlWeb();

            NewsItemCollection news = new NewsItemCollection();
            int newsItemsRemaining = options.Posts;
            int currentPage = 1;

            do
            {
                string currentUri = UriHelper.SetQuerystringParameter(options.Uri, "p", currentPage);

                HtmlDocument htmlDocument = htmlWeb.Load(currentUri);
                HtmlNodeCollection newsItemMainNodes = htmlDocument.DocumentNode.SelectNodes("//tr[@class='athing']");

                foreach (HtmlNode newsItemMainNode in newsItemMainNodes)
                {
                    NewsItem newsItem = NewsItemCollection.Scrape(options, newsItemMainNode);
                    news.items.Add(newsItem);

                    newsItemsRemaining--;

                    if (newsItemsRemaining <= 0)
                    {
                        break;
                    }
                }

                currentPage++;
            } while (newsItemsRemaining > 0);

            return news;
        }

        private static NewsItem Scrape(Program.Options options, HtmlNode newsItemMainNode)
        {
            uint.TryParse(newsItemMainNode.ParseInnerText(".//span[@class='rank']", new Regex(@"\.")), out uint rank);

            HtmlNode storylinkNode = newsItemMainNode.SelectSingleNode(".//a[@class='storylink']");
            string title = storylinkNode?.InnerText ?? "Untitled";
            string uri = storylinkNode?.Attributes["href"]?.Value;

            HtmlNode newsItemFooterNode = newsItemMainNode.NextSibling;
            string author = newsItemFooterNode.ParseInnerText(".//a[@class='hnuser']") ?? "Anonymous";
            uint.TryParse(newsItemFooterNode.ParseInnerText(".//span[@class='score']", new Regex(" points")) ?? "0", out uint points);
            uint.TryParse(newsItemFooterNode.ParseInnerText(".//td[@class='subtext']/a[last()]", new Regex("&nbsp;comments")) ?? "0", out uint comments);

            title = title.LimitLength(256);
            author = author.LimitLength(256);
            uri = UriHelper.ValidateUri(uri, options.Uri);

            return new NewsItem(title, uri, author, points, comments, rank);
        }
    }
}
