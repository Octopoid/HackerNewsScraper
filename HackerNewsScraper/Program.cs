namespace HackerNewsScraper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using CommandLine;
    using Newtonsoft.Json;

    public class Program
    {
        public class Options
        {
            [Option('u', "url", Required = false, HelpText = "The url to scrape news articles from.")]
            public string Url { get; set; } = "https://news.ycombinator.com/";
            
            [Option('p', "posts", Required = false, HelpText = "How many posts to print. A positive integer <= 100.")]
            public int Posts { get; set; } = 10;
        }

        public static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                args = new[] {"-p", "10"};
            }

            // Run the Main method using the parsed arguments.
            Parser.Default.ParseArguments<Options>(args).WithParsed(Program.Main);
        }

        public static void Main(Options options)
        {
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

            NewsItemCollection scraped = NewsItemCollection.Scrape(options);
            string json = JsonConvert.SerializeObject(scraped.Items.ToArray(), Formatting.Indented);

            Console.Out.WriteLine(json);
        }
    }
}
