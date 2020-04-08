using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNewsScraper.Tests
{
    using System.Linq;
    using Xunit;

    public class TestNewsItemCollection
    {
        [Fact]
        public void ConstructCollection()
        {
            NewsItem item1 = new NewsItem("Item 1", "", "", 52, 3, 1);
            NewsItem item2 = new NewsItem("Item 2", "", "", 15, 7, 2);
            NewsItem item3 = new NewsItem("Item 3", "", "", 72, 2, 3);

            NewsItemCollection collection = new NewsItemCollection(new List<NewsItem>() { item1, item2, item3 });
            Assert.Equal(3, collection.Count);

            Assert.True(item1.Points > item2.Points);
            Assert.True(item2.Points < item3.Points);
            Assert.True(item1.Points < item3.Points);

            item1 = collection.Take(1).Single();
            item2 = collection.Skip(1).Take(1).Single();
            item3 = collection.Skip(2).Take(1).Single();

            Assert.True(item1.Points > item2.Points);
            Assert.True(item2.Points < item3.Points);
            Assert.True(item1.Points < item3.Points);
        }
    }
}
