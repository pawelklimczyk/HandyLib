using NUnit.Framework;
using System;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLClonerTests
    {
        [Test]
        public void ShouldCloneObject()
        {
            Item originalItem = new Item() { Id = 12, Name = "test" };
            Item clone = HLCloner.CloneViaSerialization(originalItem);

            Assert.AreNotEqual(originalItem, clone);

            Assert.AreEqual(originalItem.Id, clone.Id);
            StringAssert.AreEqualIgnoringCase(originalItem.Name, clone.Name);
        }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
