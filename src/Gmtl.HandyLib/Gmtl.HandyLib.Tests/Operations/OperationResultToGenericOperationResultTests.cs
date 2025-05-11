using Gmtl.HandyLib.Operations;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests.Operations
{
    [TestFixture]
    public class OperationResultToGenericOperationResultTests
    {
        [Test]
        public void SuccessOperationResultShouldBeImplicitlyCastToTrue()
        {
            OperationResult<string> result = OperationResult.Success("test", "ok");

            Assert.That(result == true, Is.EqualTo(true));
            Assert.That(result.Value, Is.EqualTo("test"));
            Assert.That(result.Message, Is.EqualTo("ok"));
        }

        [Test]
        public void ErrorOperationResultShouldBeImplicitlyCastToFalse()
        {
            OperationResult<string> result = OperationResult.Error("test", "error");

            Assert.That(result == false, Is.EqualTo(true));
            Assert.That(result.Value, Is.EqualTo("test"));
        }

        [Test]
        public void SuccessFromOperationResultShouldBeConvertedToGenericOperationResult()
        {
            OperationResult baseResult = OperationResult.Success("test message");
            OperationResult<string> result = OperationResult<string>.FromOperationResult("test value", baseResult);

            Assert.That(result == true, Is.EqualTo(true));
            Assert.That(result.Value, Is.EqualTo("test value"));
            Assert.That(result.Message, Is.EqualTo("test message"));
        }

        [Test]
        public void ErrorFromOperationResultShouldBeConvertedToGenericOperationResult()
        {
            OperationResult baseResult = OperationResult.Error("test error message");
            OperationResult<string> result = OperationResult<string>.FromOperationResult("test value", baseResult);

            Assert.That(result == false, Is.EqualTo(true));
            Assert.That(result.Value, Is.EqualTo("test value"));
            Assert.That(result.Message, Is.EqualTo("test error message"));
        }

        [Test]
        public void ErrorFromOperationResultWithoutValueShouldBeConvertedToGenericOperationResult()
        {
            OperationResult baseResult = OperationResult.Error("test error message");
            OperationResult<object> result = OperationResult<object>.FromOperationResult(new object(), baseResult);

            Assert.That(result == false, Is.EqualTo(true));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Message, Is.EqualTo("test error message"));
        }

        [Test]
        public void SuccessFromOperationResultWithoutValueShouldBeConvertedToGenericOperationResult()
        {
            OperationResult baseResult = OperationResult.Success("test success message");
            OperationResult<object> result = OperationResult<object>.FromOperationResult(new object(), baseResult);

            Assert.That(result == true, Is.EqualTo(true));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Message, Is.EqualTo("test success message"));
        }
    }
}