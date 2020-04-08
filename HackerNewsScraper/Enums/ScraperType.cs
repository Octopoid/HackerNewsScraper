namespace HackerNewsScraper
{
    using System;

    /// <summary>
    ///     A enum containing the currently available types of scrapers.
    /// </summary>
    public enum ScraperType
    {
        /// <summary>
        ///     A scraper which targets YCombinator.
        /// </summary>
        YCombinator
    }

    /// <summary>
    ///     Extension methods for the <see cref="ScraperType" /> enum.
    /// </summary>
    public static class ScraperTypeExtensions
    {
        /// <summary>
        ///     Gets the concrete implementation of the <see cref="IScraper" /> which represents the provided
        ///     <see cref="ScraperType" />.
        /// </summary>
        /// <param name="type">The <see cref="ScraperType" /> to provide the concrete implementation for.</param>
        /// <returns>
        ///     The concrete implementation of the <see cref="IScraper" /> which represents the provided
        ///     <see cref="ScraperType" />.
        /// </returns>
        public static IScraper GetScraper(this ScraperType type)
        {
            switch (type)
            {
                case ScraperType.YCombinator:
                    return new YCombinator();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
