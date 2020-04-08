using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNewsScraper.Tests
{
    using System.Linq;
    using Xunit;

    public class TestScraper
    {
        [Fact]
        public void GetConcreteScraper()
        {
            // Ensure all ScraperTypes provide a concrete implementation.
            IEnumerable<ScraperType> scraperTypes = Enum.GetValues(typeof(ScraperType)).Cast<ScraperType>().ToList();

            foreach (ScraperType scraperType in scraperTypes)
            {
                Assert.NotNull(scraperType.GetScraper());
            }
        }
    }
}
