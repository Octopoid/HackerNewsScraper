namespace HackerNewsScraper
{
    using System.Diagnostics;
    using CommandLine;

    /// <summary>
    ///     The configuration used to perform a news item scrape operation.
    /// </summary>
    public class ScraperOptions
    {
        /// <summary>
        ///     The number of posts to scrape. Defaults to 10. Must be within the range of 0 to 100.
        /// </summary>
        [Option('p', "posts", Required = false, HelpText = "How many posts to print. A positive integer <= 100.")]
        public int Posts { get; set; } = 10;

        /// <summary>
        ///     The type of scraper to use. Currently only YCombinator is supported.
        ///     TODO: Other scrapers, e.g. Reddit
        /// </summary>
        public ScraperType ScraperType { get; set; }

        /// <summary>
        ///     The Uri to scrape the <see cref="NewsItemCollection" /> from.
        /// </summary>
        [Option('u', "url", Required = false, HelpText = "The url to scrape news articles from.")]
        public string Uri { get; set; } = "https://news.ycombinator.com/";

        /// <summary>
        ///     Ensures the options object is valid.
        ///     If the posts are outside their valid range, trace output is written and the values are corrected.
        /// </summary>
        public void Validate()
        {
            if (this.Posts < 1)
            {
                Trace.WriteLine($"Requested {this.Posts} posts, which is less than the minimum allowed. Showing 1 post.");
                this.Posts = 1;
            }

            if (this.Posts > 100)
            {
                Trace.WriteLine($"Requested {this.Posts} posts, which is greater than the maximum allowed. Showing 100 posts.");
                this.Posts = 100;
            }
        }
    }
}
