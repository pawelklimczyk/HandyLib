using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    class OperationResultTests
    {
        [Test]
        public void SuccessOperationResultShouldBeImplicitlyCastToTrue()
        {
            OperationResult<string> result = OperationResult<string>.Success("test");
            Assert.That(result == true, Is.EqualTo(true));
        }

        [Test]
        public void ErrorOperationResultShouldBeImplicitlyCastToFalse()
        {
            OperationResult<string> result = OperationResult<string>.Error("test");
            Assert.That(result == false, Is.EqualTo(true));
        }
    }
}