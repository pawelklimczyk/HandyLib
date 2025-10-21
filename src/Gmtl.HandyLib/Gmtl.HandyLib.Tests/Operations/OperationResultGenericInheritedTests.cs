using Gmtl.HandyLib.Operations;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests.Operations
{
    [TestFixture]
    public class OperationResultGenericInheritedTests
    {
        [Test]
        public void CreatingCustomSuccessrOperationResultFromStaticShouldReturnThatCustomObject()
        {
            var result = OperationResult.Success<CustomOperationResult>("test success");

            Assert.True(result is CustomOperationResult);
            Assert.AreEqual("test success", result.Message);
            Assert.AreEqual(result.Status, OperationStatus.Success);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(result.Errors);
        }

        [Test]
        public void CreatingCustomSuccessrOperationResultWithCustomPropertyFromStaticShouldReturnThatCustomObject()
        {
            var result = OperationResult.Success<CustomOperationResult>("test success", e => e.CustomProperty = "custom message");

            Assert.True(result is CustomOperationResult);
            Assert.AreEqual("test success", result.Message);
            Assert.AreEqual(result.Status, OperationStatus.Success);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(result.Errors);

            var resultAsCustom = result as CustomOperationResult;
            Assert.AreEqual("custom message", resultAsCustom.CustomProperty);
        }

        [Test]
        public void CreatingCustomSuccessrOperationFromSuccessFactory()
        {
            var result = OperationResult.SuccessFactory<CustomOperationResult>("test success", e => e.CustomProperty = "custom message");

            Assert.True(result is CustomOperationResult);
            Assert.AreEqual("test success", result.Message);
            Assert.AreEqual(result.Status, OperationStatus.Success);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(result.Errors);

            Assert.AreEqual("custom message", result.CustomProperty);
        }

        [Test]
        public void CreatingCustomErrorOperationResultFromStaticShouldReturnThatCustomObject()
        {
            var result = OperationResult.Error<CustomOperationResult>("test error");

            Assert.True(result is CustomOperationResult);
            Assert.AreEqual("test error", result.Message);
            Assert.AreEqual(result.Status, OperationStatus.Error);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNotEmpty(result.Errors);
        }

        [Test]
        public void CreatingCustomErrorOperationResultWithCustomPropertyFromStaticShouldReturnThatCustomObject()
        {
            var result = OperationResult.Error<CustomOperationResult>("test error", e => e.CustomProperty = "custom message");

            Assert.True(result is CustomOperationResult);
            Assert.AreEqual("test error", result.Message);
            Assert.AreEqual(result.Status, OperationStatus.Error);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNotEmpty(result.Errors);

            var resultAsCustom = result as CustomOperationResult;
            Assert.AreEqual("custom message", resultAsCustom.CustomProperty);
        }

        [Test]
        public void CreatingCustomErrorOperationResultFromErrorFactory()
        {
            var result = OperationResult.ErrorFactory<CustomOperationResult>("test error", e => e.CustomProperty = "custom message");

            Assert.AreEqual("test error", result.Message);
            Assert.AreEqual(result.Status, OperationStatus.Error);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNotEmpty(result.Errors);

            Assert.AreEqual("custom message", result.CustomProperty);
        }

        internal class CustomOperationResult : OperationResult
        {
            public string CustomProperty { get; set; }
        }
    }
}