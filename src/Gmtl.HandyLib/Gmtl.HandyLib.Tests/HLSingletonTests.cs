using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLSingletonTests
    {
        [Test]
        public void Instance_CalledMultipleTimes_ShouldReturnSameInstance()
        {
            var first = SingletonItem.Instance;
            var second = SingletonItem.Instance;

            Assert.AreSame(first, second);
        }

        [Test]
        public void Instance_ShouldBeUsableAndPersistStateAcrossAccesses()
        {
            SingletonItem.Instance.Value = "set once";

            Assert.AreEqual("set once", SingletonItem.Instance.Value);
        }

        public class SingletonItem : HLSingleton<SingletonItem>
        {
            public string Value { get; set; }
        }
    }
}
