namespace HackerNewsScraper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
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
            HtmlDocument htmlDocument = htmlWeb.Load(options.Url);

            NewsItemCollection news = new NewsItemCollection();
            HtmlNodeCollection newsItemMainNodes = htmlDocument.DocumentNode.SelectNodes("//tr[@class='athing']");

            foreach (HtmlNode newsItemMainNode in newsItemMainNodes.Take(options.Posts))
            {
                uint.TryParse(newsItemMainNode.SelectSingleNode(".//span[@class='rank']").InnerText.Replace(".", ""), out uint rank);

                HtmlNode storylinkNode = newsItemMainNode.SelectSingleNode(".//a[@class='storylink']");
                string title = storylinkNode.InnerText;
                string uri = storylinkNode.Attributes["href"].Value;

                HtmlNode newsItemFooterNode = newsItemMainNode.NextSibling;
                string author = newsItemFooterNode.SelectSingleNode(".//a[@class='hnuser']").InnerText;
                uint.TryParse(newsItemFooterNode.SelectSingleNode(".//span[@class='score']").InnerText.Replace(" points", ""), out uint points);
                uint.TryParse(newsItemFooterNode.SelectSingleNode(".//td[@class='subtext']/a[last()]").InnerText.Replace("&nbsp;comments", ""), out uint comments);

                news.items.Add(new NewsItem(title, uri, author, points, comments, rank));
            }

            return news;
        }
    }
}
