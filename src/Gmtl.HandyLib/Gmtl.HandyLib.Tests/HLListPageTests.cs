using System;
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

        [TestCase(10, 20, 1)]
        [TestCase(19, 20, 1)]
        [TestCase(20, 20, 1)]
        [TestCase(21, 20, 2)]
        [TestCase(10, 2, 5)]
        [TestCase(10, 3, 4)]
        [TestCase(9, 3, 3)]
        [TestCase(8, 3, 3)]
        [TestCase(10, 10, 1)]
        public void HLListPage_ProvidedList_ShouldCalculateNumberOfPages(int items, int pageSize, int expectedPages)
        {
            List<int> itemList = new List<int>();
            for (int i = 1; i <= items; i++) itemList.Add(i);

            HLListPage<int> page = new HLListPage<int>(itemList, items, 1, pageSize);

            Assert.AreEqual(expectedPages, page.TotalPages);
        }

        [Test]
        public void HLListPage_NullItems_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new HLListPage<int>(null, 0, 1, 10));
        }

        [Test]
        public void HLListPage_InvalidPageInputs_ShouldUseDefaults()
        {
            List<int> items = new List<int> { 1, 2, 3 };

            HLListPage<int> page = new HLListPage<int>(items, items.Count, 0, 0);

            Assert.That(page.PageNumber, Is.EqualTo(1));
            Assert.That(page.PageSize, Is.EqualTo(HLListPage<int>.DefaultPageSize));
            Assert.That(page.IsFirstPage, Is.True);
            Assert.That(page.IsLastPage, Is.True);
            Assert.That(page.HasPreviousPage, Is.False);
            Assert.That(page.HasNextPage, Is.False);
            Assert.That(page.FirstItemOnPage, Is.EqualTo(1));
            Assert.That(page.LastItemOnPage, Is.EqualTo(items.Count));
        }

        [TestCase(1, 10, true, false, false, true, 1, 10)]
        [TestCase(2, 10, false, false, true, true, 11, 20)]
        [TestCase(3, 10, false, true, true, false, 21, 25)]
        public void HLListPage_ProvidedPagedList_ShouldCalculatePageState(
            int pageNumber,
            int pageSize,
            bool isFirstPage,
            bool isLastPage,
            bool hasPreviousPage,
            bool hasNextPage,
            int firstItemOnPage,
            int lastItemOnPage)
        {
            const int totalCount = 25;
            List<int> items = Enumerable.Range(firstItemOnPage, lastItemOnPage - firstItemOnPage + 1).ToList();

            HLListPage<int> page = new HLListPage<int>(items, totalCount, pageNumber, pageSize);

            Assert.That(page.IsFirstPage, Is.EqualTo(isFirstPage));
            Assert.That(page.IsLastPage, Is.EqualTo(isLastPage));
            Assert.That(page.HasPreviousPage, Is.EqualTo(hasPreviousPage));
            Assert.That(page.HasNextPage, Is.EqualTo(hasNextPage));
            Assert.That(page.FirstItemOnPage, Is.EqualTo(firstItemOnPage));
            Assert.That(page.LastItemOnPage, Is.EqualTo(lastItemOnPage));
            Assert.That(page.Count, Is.EqualTo(items.Count));
            Assert.That(page[0], Is.EqualTo(firstItemOnPage));
        }
    }
}