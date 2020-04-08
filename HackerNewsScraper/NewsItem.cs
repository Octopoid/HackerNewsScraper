namespace HackerNewsScraper
{
    using System.Text;

    public class NewsItem
    {
        private readonly string title;
        private readonly string uri;
        private readonly string author;
        private readonly uint points;
        private readonly uint comments;
        private readonly uint rank;

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

        public string Title { get { return this.title; } }

        public string Uri { get { return this.uri; } }

        public string Author { get { return this.author; } }

        public uint Points { get { return this.points; } }

        public uint Comments { get { return this.comments; } }

        public uint Rank { get { return this.rank; } }

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
