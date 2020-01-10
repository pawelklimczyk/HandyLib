using Gmtl.HandyLib.Extensions;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class LogicFowExtensionsTests
    {
        private bool DummyAction(bool returnValue)
        {
            return returnValue;
        }

        [Test]
        public void TestFalseWithNoState()
        {
            bool trueCalled = false, falseCalled = false, alwaysCalled = false;
            var result = DummyAction(false);

            result
                .True(() => { trueCalled = true; })
                .False(() => { falseCalled = true; })
                .Always(() => { alwaysCalled = true; });

            Assert.IsTrue(falseCalled);
            Assert.IsFalse(trueCalled);
            Assert.IsTrue(alwaysCalled);
        }

        [Test]
        public void TestFalseWithState()
        {
            bool trueCalled = false, falseCalled = false, alwaysCalled = false;
            var result = DummyAction(false);
            var state = new object();

            result
                .True(state, s => { trueCalled = true; })
                .False(state, s => { falseCalled = true; })
                .Always(state, s => { alwaysCalled = true; });

            Assert.IsTrue(falseCalled);
            Assert.IsFalse(trueCalled);
            Assert.IsTrue(alwaysCalled);
        }

        [Test]
        public void TestTrueWithNoState()
        {
            bool trueCalled = false, falseCalled = false, alwaysCalled = false;
            var result = DummyAction(true);

            result
                .True(() => { trueCalled = true; })
                .False(() => { falseCalled = true; })
                .Always(() => { alwaysCalled = true; });

            Assert.IsTrue(trueCalled);
            Assert.IsFalse(falseCalled);
            Assert.IsTrue(alwaysCalled);
        }

        [Test]
        public void TestTrueWithState()
        {
            bool trueCalled = false, falseCalled = false, alwaysCalled = false;
            var result = DummyAction(true);
            var state = new object();

            result
                .True(state, s => { trueCalled = true; })
                .False(state, s => { falseCalled = true; })
                .Always(state, s => { alwaysCalled = true; });

            Assert.IsTrue(trueCalled);
            Assert.IsFalse(falseCalled);
            Assert.IsTrue(alwaysCalled);
        }
    }
}