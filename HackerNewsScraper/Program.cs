namespace HackerNewsScraper
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using CommandLine;
    using HackerNewsScraper.Helpers;
    using Newtonsoft.Json;

    public class Program
    {
        public static void Main(string[] args)
        {
            // Run the Main method using the parsed arguments.
            Parser.Default.ParseArguments<Options>(args).WithParsed(Program.Main);
        }

        public static void Main(Options options)
        {
            if (!UriHelper.IsValidHttpUri(options.Uri))
            {
                string error = JsonConvert.SerializeObject(new { error = $"Provided Uri '{options.Uri}' does not appear to be valid." }, Formatting.Indented);
                Console.Out.WriteLine(error);
                return;
            }

            if (options.Posts < 1)
            {
                Trace.WriteLine($"Requested {options.Posts} posts, which is less than the minimum allowed. Showing 1 post.");
                options.Posts = 1;
            }

            if (options.Posts > 100)
            {
                Trace.WriteLine($"Requested {options.Posts} posts, which is greater than the maximum allowed. Showing 100 posts.");
                options.Posts = 100;
            }

            try
            {
                NewsItemCollection scraped = NewsItemCollection.Scrape(options);

                string json = JsonConvert.SerializeObject(scraped.Items.ToArray(), Formatting.Indented);
                Console.Out.WriteLine(json);
            }
            catch (WebException ex)
            {
                string error = JsonConvert.SerializeObject(new { error = $"Could not read from the Uri '{options.Uri}'.", exception = ex.Message }, Formatting.Indented);
                Console.Out.WriteLine(error);
            }
        }

        public class Options
        {
            [Option('p', "posts", Required = false, HelpText = "How many posts to print. A positive integer <= 100.")]
            public int Posts { get; set; } = 100;

            [Option('u', "url", Required = false, HelpText = "The url to scrape news articles from.")]
            public string Uri { get; set; } = "https://news.ycombinator.com/";
        }
    }
}
