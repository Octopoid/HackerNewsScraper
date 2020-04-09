# Hacker News Scraper

A console application for scraping news stories from a provided Url and exporting the results as Json.

## Usage Instructions

* The application requires .NET Core 3.1, which can be downloaded [here](https://dotnet.microsoft.com/download/dotnet-core/3.1).
* Compile the solution.
* Open a command line window such as `cmd` or `powershell`, and navigate to the build folder.
* Run `NewsScraper.exe` with the following parameters:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`-p n`&nbsp;&nbsp;&nbsp;&nbsp;`--posts n`&nbsp;&nbsp;&nbsp;&nbsp;Sets the number of posts to scrape. Should be an integer between 1 and 100. Optional, defaults to 10.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`-u url`&nbsp;&nbsp;&nbsp;&nbsp;`--url url`&nbsp;&nbsp;&nbsp;&nbsp;Sets the url to scrape the news items from. Optional, defaults to https://news.ycombinator.com/

## Nuget Packages

### [CommandLineParser](https://github.com/commandlineparser/commandline)

Simplifies the process of parsing command line arguments.

### [HtmlAgilityPack](https://html-agility-pack.net/)

Provides an API for downloading and parsing Html documents into a queryable node model. Used by the scraper to locate and extract the data.

### [Newtonsoft.Json](https://www.newtonsoft.com/json)

Used to serialize the final output as Json.

### [Flurl](https://flurl.dev/)

Contains simple, powerful helper functions for validating and maniuplating Urls.
