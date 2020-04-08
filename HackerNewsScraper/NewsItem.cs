namespace HackerNewsScraper
{
    using System.Text;

    /// <summary>
    ///     Represents a single YCombinator news item.
    /// </summary>
    public class NewsItem
    {
        private readonly string title;
        private readonly string uri;
        private readonly string author;
        private readonly uint points;
        private readonly uint comments;
        private readonly uint rank;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NewsItem" /> class.
        /// </summary>
        /// <param name="title">The title of the news item, limited to 256 characters.</param>
        /// <param name="uri">The Uri the news item links to. This is always an absolute Uri.</param>
        /// <param name="author">The author of the news item, limited to 256 characters.</param>
        /// <param name="points">The number of points that have been given to the news item.</param>
        /// <param name="comments">The number of comments that have been made on the news item.</param>
        /// <param name="rank">The index of the news item at the moment of scraping.</param>
        public NewsItem(string title,
                        string uri,
                        string author,
                        uint points,
                        uint comments,
                        uint rank)
        {
            this.title = title;
            this.uri = uri;
            this.author = author;
            this.points = points;
            this.comments = comments;
            this.rank = rank;
        }

        /// <summary>
        ///     The author of the news item, limited to 256 characters.
        /// </summary>
        public string Author { get { return this.author; } }

        /// <summary>
        ///     The number of comments that have been made on the news item.
        /// </summary>
        public uint Comments { get { return this.comments; } }

        /// <summary>
        ///     The number of points that have been given to the news item.
        /// </summary>
        public uint Points { get { return this.points; } }

        /// <summary>
        ///     The index of the news item at the moment of scraping.
        /// </summary>
        public uint Rank { get { return this.rank; } }

        /// <summary>
        ///     The title of the news item, limited to 256 characters.
        /// </summary>
        public string Title { get { return this.title; } }

        /// <summary>
        ///     The Uri the news item links to. This is always an absolute Uri.
        /// </summary>
        public string Uri { get { return this.uri; } }

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Title:    {this.title}");
            stringBuilder.AppendLine($"Uri:      {this.uri}");
            stringBuilder.AppendLine($"Author:   {this.author}");
            stringBuilder.AppendLine($"Rank:     {this.rank}");
            stringBuilder.AppendLine($"Score:    {this.points}");
            stringBuilder.AppendLine($"Comments: {this.comments}");

            return stringBuilder.ToString();
        }
    }
}
