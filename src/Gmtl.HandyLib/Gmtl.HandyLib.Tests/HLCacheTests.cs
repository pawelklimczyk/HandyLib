using System;
using System.Collections.Generic;
using System.Linq;
using Gmtl.HandyLib.Cache;
using Gmtl.HandyLib.Models;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLCacheTests
    {
        private Cache<int, Item> _cache;

        [SetUp]
        public void Setup()
        {
            _cache = new Cache<int, Item>(true);
        }

        [TearDown]
        public void TearDown()
        {
            _cache.DeleteAll();
        }

        #region Constructor Tests

        [Test]
        public void Constructor_DefaultConstructor_CreatesEmptyCache()
        {
            _cache.DeleteAll(); // Clear any static state
            var cache = new Cache<int, Item>();
            Assert.True(cache.Empty);
            Assert.AreEqual(0, cache.Count);
        }

        [Test]
        public void Constructor_WithAutoMaintainListOnly_CreatesEmptyCache()
        {
            _cache.DeleteAll(); // Clear any static state
            var cache = new Cache<int, Item>(true);
            Assert.True(cache.Empty);
        }

        #endregion

        #region Insertion and Updates

        [Test]
        public void InsertOrUpdate_NewItem_AddsToCache()
        {
            var item = new Item { Id = 1, Name = "Test 1" };
            _cache.InsertOrUpdate(item.Id, item);

            var retrieved = _cache.Get(item.Id);
            Assert.AreEqual(item, retrieved);
        }

        [Test]
        public void InsertOrUpdate_ExistingKey_UpdatesValue()
        {
            var item1 = new Item { Id = 1, Name = "Test 1" };
            var item2 = new Item { Id = 1, Name = "Test 2 Updated" };

            _cache.InsertOrUpdate(item1.Id, item1);
            _cache.InsertOrUpdate(item2.Id, item2);

            var retrieved = _cache.Get(item2.Id);
            Assert.AreEqual("Test 2 Updated", retrieved.Name);
        }

        [Test]
        public void InsertOrUpdate_MultipleItems_AllPresent()
        {
            var i1 = new Item { Id = 1, Name = "Test 1" };
            var i2 = new Item { Id = 2, Name = "Adam" };
            var i3 = new Item { Id = 3, Name = "Bart" };

            _cache.InsertOrUpdate(i1.Id, i1);
            _cache.InsertOrUpdate(i2.Id, i2);
            _cache.InsertOrUpdate(i3.Id, i3);

            var list = _cache.GetList();
            Assert.Contains(i1, list);
            Assert.Contains(i2, list);
            Assert.Contains(i3, list);
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void PopulateList_WithAutoMaintainList_UpdatesListOnInsert()
        {
            var i1 = new Item { Id = 1, Name = "Test 1" };
            var i2 = new Item { Id = 2, Name = "Adam" };
            var i3 = new Item { Id = 3, Name = "Bart" };

            _cache.InsertOrUpdate(i1.Id, i1);
            _cache.InsertOrUpdate(i2.Id, i2);
            _cache.InsertOrUpdate(i3.Id, i3);

            var list = _cache.GetList();
            Assert.Contains(i1, list);
            Assert.Contains(i2, list);
            Assert.Contains(i3, list);
            Assert.AreEqual(3, list.Count);
        }

        #endregion

        #region Retrieval Tests

        [Test]
        public void Get_ExistingKey_ReturnsValue()
        {
            var item = new Item { Id = 1, Name = "Test Item" };
            _cache.InsertOrUpdate(item.Id, item);

            var retrieved = _cache.Get(item.Id);
            Assert.AreEqual(item, retrieved);
            Assert.AreEqual("Test Item", retrieved.Name);
        }

        [Test]
        public void Get_NonExistentKey_ThrowsKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => _cache.Get(999));
        }

        [Test]
        public void GetOrDefault_ExistingKey_ReturnsValue()
        {
            var item = new Item { Id = 1, Name = "Test Item" };
            _cache.InsertOrUpdate(item.Id, item);

            var retrieved = _cache.GetOrDefault(item.Id);
            Assert.AreEqual(item, retrieved);
        }

        [Test]
        public void GetOrDefault_NonExistentKey_ReturnsNull()
        {
            var retrieved = _cache.GetOrDefault(999);
            Assert.IsNull(retrieved);
        }

        [Test]
        public void Indexer_ExistingKey_ReturnsValue()
        {
            var item = new Item { Id = 1, Name = "Test Item" };
            _cache.InsertOrUpdate(item.Id, item);

            var retrieved = _cache[item.Id];
            Assert.AreEqual(item, retrieved);
        }

        [Test]
        public void Indexer_NonExistentKey_ThrowsKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => { var x = _cache[999]; });
        }

        [Test]
        public void GetAll_ReturnsDeepCopy()
        {
            var i1 = new Item { Id = 1, Name = "Item 1" };
            var i2 = new Item { Id = 2, Name = "Item 2" };

            _cache.InsertOrUpdate(i1.Id, i1);
            _cache.InsertOrUpdate(i2.Id, i2);

            var all = _cache.GetAll();
            Assert.AreEqual(2, all.Count);
            Assert.IsTrue(all.ContainsKey(1));
            Assert.IsTrue(all.ContainsKey(2));
        }

        [Test]
        public void GetList_WithoutAutoMaintain_ReturnsValuesAsList()
        {
            var cache = new Cache<int, Item>();
            var i1 = new Item { Id = 1, Name = "Item 1" };
            var i2 = new Item { Id = 2, Name = "Item 2" };

            cache.InsertOrUpdate(i1.Id, i1);
            cache.InsertOrUpdate(i2.Id, i2);

            var list = cache.GetList();
            Assert.AreEqual(2, list.Count);
            Assert.Contains(i1, list);
            Assert.Contains(i2, list);
        }

        #endregion

        #region Deletion Tests

        [Test]
        public void Delete_ExistingKey_RemovesItem()
        {
            var item = new Item { Id = 1, Name = "Test Item" };
            _cache.InsertOrUpdate(item.Id, item);
            Assert.AreEqual(1, _cache.Count);

            _cache.Delete(item.Id);
            Assert.AreEqual(0, _cache.Count);
            Assert.False(_cache.HasKey(item.Id));
        }

        [Test]
        public void Delete_NonExistentKey_DoesNotThrowError()
        {
            Assert.DoesNotThrow(() => _cache.Delete(999));
        }

        [Test]
        public void Delete_UpdatesListWhenAutoMaintain()
        {
            var i1 = new Item { Id = 1, Name = "Item 1" };
            var i2 = new Item { Id = 2, Name = "Item 2" };
            var i3 = new Item { Id = 3, Name = "Item 3" };

            _cache.InsertOrUpdate(i1.Id, i1);
            _cache.InsertOrUpdate(i2.Id, i2);
            _cache.InsertOrUpdate(i3.Id, i3);

            _cache.Delete(i2.Id);

            var list = _cache.GetList();
            Assert.AreEqual(2, list.Count);
            Assert.Contains(i1, list);
            Assert.Contains(i3, list);
            Assert.False(list.Contains(i2));
        }

        [Test]
        public void DeleteAll_RemovesAllItems()
        {
            var i1 = new Item { Id = 1, Name = "Item 1" };
            var i2 = new Item { Id = 2, Name = "Item 2" };
            var i3 = new Item { Id = 3, Name = "Item 3" };

            _cache.InsertOrUpdate(i1.Id, i1);
            _cache.InsertOrUpdate(i2.Id, i2);
            _cache.InsertOrUpdate(i3.Id, i3);

            Assert.AreEqual(3, _cache.Count);
            _cache.DeleteAll();

            Assert.AreEqual(0, _cache.Count);
            Assert.True(_cache.Empty);
        }

        [Test]
        public void DeleteAll_ClearsListWhenAutoMaintain()
        {
            var i1 = new Item { Id = 1, Name = "Item 1" };
            var i2 = new Item { Id = 2, Name = "Item 2" };

            _cache.InsertOrUpdate(i1.Id, i1);
            _cache.InsertOrUpdate(i2.Id, i2);

            _cache.DeleteAll();

            var list = _cache.GetList();
            Assert.AreEqual(0, list.Count);
        }

        #endregion

        #region Key Existence Tests

        [Test]
        public void HasKey_ExistingKey_ReturnsTrue()
        {
            var item = new Item { Id = 1, Name = "Test Item" };
            _cache.InsertOrUpdate(item.Id, item);

            Assert.True(_cache.HasKey(item.Id));
        }

        [Test]
        public void HasKey_NonExistentKey_ReturnsFalse()
        {
            Assert.False(_cache.HasKey(999));
        }

        #endregion

        #region Property Tests

        [Test]
        public void Empty_WithNoItems_ReturnsTrue()
        {
            Assert.True(_cache.Empty);
        }

        [Test]
        public void Empty_WithItems_ReturnsFalse()
        {
            var item = new Item { Id = 1, Name = "Test Item" };
            _cache.InsertOrUpdate(item.Id, item);

            Assert.False(_cache.Empty);
        }

        [Test]
        public void Empty_AfterDeleteAll_ReturnsTrue()
        {
            var item = new Item { Id = 1, Name = "Test Item" };
            _cache.InsertOrUpdate(item.Id, item);
            Assert.False(_cache.Empty);

            _cache.DeleteAll();
            Assert.True(_cache.Empty);
        }

        [Test]
        public void Count_EmptyCache_ReturnsZero()
        {
            Assert.AreEqual(0, _cache.Count);
        }

        [Test]
        public void Count_WithItems_ReturnsCorrectCount()
        {
            for (int i = 1; i <= 5; i++)
            {
                _cache.InsertOrUpdate(i, new Item { Id = i, Name = $"Item {i}" });
            }

            Assert.AreEqual(5, _cache.Count);
        }

        [Test]
        public void Count_AfterUpdate_DoesNotIncrease()
        {
            var item = new Item { Id = 1, Name = "Original" };
            _cache.InsertOrUpdate(item.Id, item);
            Assert.AreEqual(1, _cache.Count);

            item.Name = "Updated";
            _cache.InsertOrUpdate(item.Id, item);
            Assert.AreEqual(1, _cache.Count);
        }

        #endregion

        #region FindBy Tests

        [Test]
        public void FindBy_WithFilter_ReturnsFilteredResults()
        {
            var i1 = new Item { Id = 1, Name = "Adam" };
            var i2 = new Item { Id = 2, Name = "Bart" };
            var i3 = new Item { Id = 3, Name = "Charlie" };

            _cache.InsertOrUpdate(i1.Id, i1);
            _cache.InsertOrUpdate(i2.Id, i2);
            _cache.InsertOrUpdate(i3.Id, i3);

            var result = _cache.FindBy<string>(x => x.Name.StartsWith("A") || x.Name.StartsWith("B"));
            var items = result.ToList();
            Assert.AreEqual(2, items.Count);
            Assert.Contains(i1, items);
            Assert.Contains(i2, items);
        }

        [Test]
        public void FindBy_WithOrdering_ReturnsOrderedResults()
        {
            var i1 = new Item { Id = 1, Name = "Zebra" };
            var i2 = new Item { Id = 2, Name = "Apple" };
            var i3 = new Item { Id = 3, Name = "Mango" };

            _cache.InsertOrUpdate(i1.Id, i1);
            _cache.InsertOrUpdate(i2.Id, i2);
            _cache.InsertOrUpdate(i3.Id, i3);

            var result = _cache.FindBy<string>(x => true, x => x.Name, OrderDirection.Ascending);
            var items = result.ToList();

            Assert.AreEqual(3, items.Count);
            Assert.AreEqual("Apple", items[0].Name);
            Assert.AreEqual("Mango", items[1].Name);
            Assert.AreEqual("Zebra", items[2].Name);
        }

        [Test]
        public void FindBy_WithDescendingOrder_ReturnsReverseOrderedResults()
        {
            var i1 = new Item { Id = 1, Name = "Zebra" };
            var i2 = new Item { Id = 2, Name = "Apple" };
            var i3 = new Item { Id = 3, Name = "Mango" };

            _cache.InsertOrUpdate(i1.Id, i1);
            _cache.InsertOrUpdate(i2.Id, i2);
            _cache.InsertOrUpdate(i3.Id, i3);

            var result = _cache.FindBy<string>(x => true, x => x.Name, OrderDirection.Descending);
            var items = result.ToList();

            Assert.AreEqual(3, items.Count);
            Assert.AreEqual("Zebra", items[0].Name);
            Assert.AreEqual("Mango", items[1].Name);
            Assert.AreEqual("Apple", items[2].Name);
        }

        [Test]
        public void FindBy_WithPagination_ReturnsPagedResults()
        {
            for (int i = 1; i <= 25; i++)
            {
                _cache.InsertOrUpdate(i, new Item { Id = i, Name = $"Item {i:D2}" });
            }

            var page1 = _cache.FindBy<int>(x => true, x => x.Id, pageSize: 10, page: 1);
            Assert.AreEqual(10, page1.Count);
            Assert.AreEqual(25, page1.TotalCount);
            Assert.AreEqual(1, page1.PageNumber);

            var page2 = _cache.FindBy<int>(x => true, x => x.Id, pageSize: 10, page: 2);
            Assert.AreEqual(10, page2.Count);
            Assert.AreEqual(25, page2.TotalCount);
            Assert.AreEqual(2, page2.PageNumber);

            var page3 = _cache.FindBy<int>(x => true, x => x.Id, pageSize: 10, page: 3);
            Assert.AreEqual(5, page3.Count);
        }

        [Test]
        public void FindBy_WithFilterAndPagination_ReturnsFilteredPages()
        {
            for (int i = 1; i <= 20; i++)
            {
                _cache.InsertOrUpdate(i, new Item { Id = i, Name = $"Item {i}" });
            }

            var result = _cache.FindBy<int>(x => x.Id % 2 == 0, x => x.Id, pageSize: 5, page: 1);
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(10, result.TotalCount); // 10 even items total
        }

        [Test]
        public void FindBy_WithPaginationAndOrdering_ReturnsCorrectPageAndOrder()
        {
            // Clear and repopulate for this specific test
            _cache.DeleteAll();
            for (int i = 1; i <= 10; i++)
            {
                _cache.InsertOrUpdate(i, new Item { Id = i, Name = $"Item {i}" });
            }

            var result = _cache.FindBy<int>(x => true, x => x.Id, OrderDirection.Descending, page: 1, pageSize: 3);

            var items = result.ToList();
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(10, result.TotalCount);
            Assert.AreEqual(1, result.PageNumber);
            // Should be descending, so highest IDs first
            Assert.AreEqual(10, items[0].Id);
            Assert.AreEqual(9, items[1].Id);
            Assert.AreEqual(8, items[2].Id);
        }

        [Test]
        public void FindBy_NoMatches_ReturnsEmptyList()
        {
            var i1 = new Item { Id = 1, Name = "Adam" };
            var i2 = new Item { Id = 2, Name = "Bart" };

            _cache.InsertOrUpdate(i1.Id, i1);
            _cache.InsertOrUpdate(i2.Id, i2);

            var result = _cache.FindBy<string>(x => x.Name == "Nonexistent");
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(0, result.TotalCount);
        }

        #endregion

        #region UpdateCache Tests

        [Test]
        public void UpdateCache_WithOverrideType_UpdatesExistingItem()
        {
            var item1 = new Item { Id = 1, Name = "Original" };
            _cache.InsertOrUpdate(item1.Id, item1);

            var item2 = new Item { Id = 1, Name = "Updated" };
            _cache.UpdateCache(item2.Id, item2, CacheUpdateType.Override);

            var retrieved = _cache.Get(item2.Id);
            Assert.AreEqual("Updated", retrieved.Name);
        }

        [Test]
        public void UpdateCache_WithOverrideType_InsertsNewItem()
        {
            var item = new Item { Id = 1, Name = "New Item" };
            _cache.UpdateCache(item.Id, item, CacheUpdateType.Override);

            var retrieved = _cache.Get(item.Id);
            Assert.AreEqual("New Item", retrieved.Name);
        }

        [Test]
        public void UpdateCache_WithDeleteType_RemovesItem()
        {
            var item = new Item { Id = 1, Name = "To Delete" };
            _cache.InsertOrUpdate(item.Id, item);
            Assert.AreEqual(1, _cache.Count);

            _cache.UpdateCache(item.Id, item, CacheUpdateType.Delete);
            Assert.AreEqual(0, _cache.Count);
            Assert.False(_cache.HasKey(item.Id));
        }

        [Test]
        public void UpdateCache_WithInvalidType_ThrowsException()
        {
            var item = new Item { Id = 1, Name = "Test" };
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _cache.UpdateCache(item.Id, item, (CacheUpdateType)999));
        }

        #endregion
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Item item)
                return Id == item.Id && Name == item.Name;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }
    }
}
