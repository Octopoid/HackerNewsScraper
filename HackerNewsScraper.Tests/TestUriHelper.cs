namespace HackerNewsScraper.Tests
{
    using HackerNewsScraper.Helpers;
    using Xunit;

    public class TestUriHelper
    {
        [Fact]
        public void ValidateUri()
        {
            Assert.True(UriHelper.IsValidHttpUri("http://google.com"));
            Assert.True(UriHelper.IsValidHttpUri("https://google.com"));

            Assert.False(UriHelper.IsValidHttpUri("ftp://google.com"));
            Assert.False(UriHelper.IsValidHttpUri("https:/google.com"));
        }

        [Fact]
        public void EnsureAbsoluteUri()
        {
            Assert.Equal("http://google.com", UriHelper.EnsureUriAbsolute("http://google.com", "http://reddit.com"));
            Assert.Equal("http://reddit.com", UriHelper.EnsureUriAbsolute(null, "http://reddit.com"));
            Assert.Equal("http://reddit.com/r/programminghorror/", UriHelper.EnsureUriAbsolute("r/programminghorror/", "http://reddit.com"));
        }
    }
}
