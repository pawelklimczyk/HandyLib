using Gmtl.HandyLib.Cache;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLCacheTests
    {
        [Test]
        public void ShouldPopulateList()
        {
            Cache<int, Item> cache = new Cache<int, Item>(true);

            Item i1 = new Item() { Id = 1, Name = "Test 1" };
            Item i2 = new Item() { Id = 2, Name = "Adam" };
            Item i3 = new Item() { Id = 3, Name = "Bart" };

            cache.InsertOrUpdate(i1.Id, i1);
            cache.InsertOrUpdate(i2.Id, i2);
            cache.InsertOrUpdate(i3.Id, i3);

            var list = cache.GetList();
            Assert.Contains(i1, list);
            Assert.Contains(i2, list);
            Assert.Contains(i3, list);
        }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
