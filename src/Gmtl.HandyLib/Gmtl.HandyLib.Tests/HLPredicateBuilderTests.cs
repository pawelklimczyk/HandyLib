using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLPredicateBuilderTests
    {
        private List<Item> items;

        [SetUp]
        public void SetUp()
        {
            items = new List<Item>
            {
                new Item { Id = 1, Name = "Alpha" },
                new Item { Id = 2, Name = "Beta" },
                new Item { Id = 3, Name = "Gamma" },
            };
        }

        [Test]
        public void True_ShouldMatchAllItems()
        {
            Expression<Func<Item, bool>> predicate = HLPredicateBuilder.True<Item>();

            var result = items.AsQueryable().Where(predicate).ToList();

            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void False_ShouldMatchNoItems()
        {
            Expression<Func<Item, bool>> predicate = HLPredicateBuilder.False<Item>();

            var result = items.AsQueryable().Where(predicate).ToList();

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void And_ShouldCombinePredicatesWithLogicalAnd()
        {
            Expression<Func<Item, bool>> idFilter = i => i.Id > 1;
            Expression<Func<Item, bool>> nameFilter = i => i.Name.StartsWith("B");

            var combined = idFilter.And(nameFilter);
            var result = items.AsQueryable().Where(combined).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Beta", result[0].Name);
        }

        [Test]
        public void Or_ShouldCombinePredicatesWithLogicalOr()
        {
            Expression<Func<Item, bool>> idFilter = i => i.Id == 1;
            Expression<Func<Item, bool>> nameFilter = i => i.Name == "Gamma";

            var combined = idFilter.Or(nameFilter);
            var result = items.AsQueryable().Where(combined).OrderBy(i => i.Id).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Alpha", result[0].Name);
            Assert.AreEqual("Gamma", result[1].Name);
        }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
