namespace HackerNewsScraper.Tests
{
    using HackerNewsScraper.Extensions;
    using Xunit;

    public class TestStringExtensions
    {
        [Fact]
        public void LimitLengthLongString()
        {
            Assert.Equal("01234", "012345678901234567890123456789".LimitLength(5));
            Assert.Equal("012345678901234567890...", "012345678901234567890123456789".LimitLength(24));
        }

        [Fact]
        public void LimitLengthShortString()
        {
            Assert.Equal("01234", "0123456789".LimitLength(5));
            Assert.Equal("0123456789", "0123456789".LimitLength(100));
        }
    }
}
