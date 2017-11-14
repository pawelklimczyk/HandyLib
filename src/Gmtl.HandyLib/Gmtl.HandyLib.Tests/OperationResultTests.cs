using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    class OperationResultTests
    {
        [Test]
        public void SuccessOperationResultShouldBeImplicitlyCastedToTrue()
        {
            OperationResult<string> result = OperationResult<string>.Success("test");
            Assert.That(result == true, Is.EqualTo(true));
        }

        [Test]
        public void ErrorOperationResultShouldBeImplicitlyCastedToFalse()
        {
            OperationResult<string> result = OperationResult<string>.Error("test");
            Assert.That(result == false, Is.EqualTo(true));
        }
    }
}