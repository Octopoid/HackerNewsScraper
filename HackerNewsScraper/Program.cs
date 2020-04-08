namespace HackerNewsScraper
{
    using System;
    using System.Linq;
    using System.Net;
    using CommandLine;
    using HackerNewsScraper.Helpers;
    using Newtonsoft.Json;

    /// <summary>
    ///     The main application for the NewsItem scraper.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Entry point for application.
        /// </summary>
        /// <param name="args">The commandline arguments.</param>
        public static void Main(string[] args)
        {
            // Run the Main method using the parsed arguments.
            Parser.Default.ParseArguments<ScraperOptions>(args).WithParsed(Program.Scrape);
        }

        /// <summary>
        ///     Scrapes a <see cref="NewsItemCollection" /> using the provided options, and outputs the results via STDOUT.
        ///     Please note, a Json error package may also be provided.
        /// </summary>
        /// <param name="options">The <see cref="ScraperOptions" /> used to perform this operation.</param>
        public static void Scrape(ScraperOptions options)
        {
            // If the base url is not a valid Http/Https Uri, output an error and end.
            if (!UriHelper.IsValidHttpUri(options.Uri))
            {
                string error = JsonConvert.SerializeObject(new {error = $"Provided Uri '{options.Uri}' does not appear to be valid."}, Formatting.Indented);
                Console.Out.WriteLine(error);

                return;
            }

            try
            {
                // Get the scraper implementation from the provided ScraperType, and scrape out the NewsItems.
                IScraper scraper = options.ScraperType.GetScraper();
                NewsItemCollection scraped = scraper.Scrape(options);

                // Return the Json representation of the scraped NewsItems.
                string json = JsonConvert.SerializeObject(scraped.ToArray(), Formatting.Indented);
                Console.Out.WriteLine(json);
            }
            catch (WebException ex)
            {
                // The inner 
                string error = JsonConvert.SerializeObject(new {error = $"Could not read from the Uri '{options.Uri}'.", exception = ex.Message}, Formatting.Indented);
                Console.Out.WriteLine(error);
            }
        }
    }
}
