namespace HackerNewsScraper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    ///     A read only collection of <see cref="NewsItem" />.
    /// </summary>
    public class NewsItemCollection : IEnumerable<NewsItem>
    {
        private readonly DateTime scrapedOn;
        private readonly List<NewsItem> items;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NewsItemCollection" /> class with elements contained in the provided
        ///     collection.
        /// </summary>
        public NewsItemCollection(IEnumerable<NewsItem> items)
        {
            this.scrapedOn = DateTime.Now;
            this.items = new List<NewsItem>(items);
        }

        /// <summary>
        ///     Gets the number of items contained in the <see cref="NewsItemCollection" />.
        /// </summary>
        public int Count { get { return this.items.Count; } }

        /// <summary>
        ///     Gets the Date and Time the scrape operation was performed on.
        /// </summary>
        public DateTime ScrapedOn { get { return this.scrapedOn; } }

        /// <inheritdoc />
        public IEnumerator<NewsItem> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) this.items).GetEnumerator();
        }
    }
}
