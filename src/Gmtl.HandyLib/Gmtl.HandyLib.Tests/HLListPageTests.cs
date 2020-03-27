using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLListPageTests
    {
        [Test]
        public void HLListPage_ProvidedList_ShouldBePossibleToAccessItViaIEnumerable()
        {
            List<int> items = new List<int>();
            for (int i = 1; i < 10; i++) items.Add(i);

            HLListPage<int> page = new HLListPage<int>(items, 10, 1, 10);

            Assert.That(page.All(i => i > 0));
        }

        [Test]
        public void HLListPage_ProvidedEmptyList_ShouldPresentEmptyHLListPage()
        {
            List<int> items = new List<int>();

            HLListPage<int> page = new HLListPage<int>(items, items.Count, 1, items.Count);

            Assert.That(page.IsEmpty);
            Assert.That(page.PageSize == HLListPage<int>.DefaultPageSize);
            Assert.That(page.PageNumber == 1);
        }
    }
}