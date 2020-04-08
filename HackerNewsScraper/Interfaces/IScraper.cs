namespace HackerNewsScraper
{
    public interface IScraper
    {
        /// <summary>
        ///     Scrapes a <see cref="NewsItemCollection" /> of <see cref="NewsItem" /> using the provided
        ///     <see cref="ScraperOptions" />.
        /// </summary>
        /// <param name="options">
        ///     The <see cref="ScraperOptions" /> to use. This includes the base Uri and the number of posts to
        ///     scrape.
        /// </param>
        /// <returns>Returns a <see cref="NewsItemCollection" /> of <see cref="NewsItem" />.</returns>
        NewsItemCollection Scrape(ScraperOptions options);
    }
}
