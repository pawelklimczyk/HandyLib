using Gmtl.HandyLib.Operations;
using NUnit.Framework;
using System;
using System.Text.Json;

namespace Gmtl.HandyLib.Tests.Operations
{
    [TestFixture]
    public class OperationResultTests
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

        [Test]
        public void OperationShouldBeReturnedAsJsonString()
        {
            string result = OperationResult<string>.Error("test value", "test message").AsJson();

            var jsonDocument = EnsureValidJson(result);
            Assert.That(result, Contains.Substring("false"));
            Assert.That(result, Contains.Substring("test message"));
            Assert.That(result, Contains.Substring("test value"));
        }

        [Test]
        public void OperationShouldBeReturnedAsJsonInt()
        {
            string result = OperationResult<int>.Error(123, "test message").AsJson();


            var jsonDocument = EnsureValidJson(result);
            Assert.That(result, Contains.Substring("false"));
            Assert.That(result, Contains.Substring("test message"));
            Assert.That(result, Contains.Substring("123"));
        }

        [Test]
        public void OperationShouldBeReturnedAsJsonStringWithAComplexObject()
        {
            var jsonObj = "{\"field1\":\"value1\", \"field2\":\"value2\"}";
            string result = OperationResult<object>.Error(jsonObj, "test").AsJson();

            var jsonDocument = EnsureValidJson(result);
            Assert.That(result, Contains.Substring("false"));
            Assert.That(result, Contains.Substring("value1"));
            Assert.That(result, Contains.Substring("value2"));
        }

        [Test]
        public void OperationShouldBeReturnedAsJsonStringWithAComplexObjectProvidedAsParam()
        {
            var jsonObj = "{\"field1-edited\":\"value1\", \"field2-edited\":\"value2\"}";
            string result = OperationResult<object>.Error(jsonObj, "test").AsJson();

            var jsonDocument = EnsureValidJson(result);
            Assert.That(result, Contains.Substring("false"));
            Assert.That(result, Contains.Substring("test"));
            Assert.That(result, Contains.Substring(jsonObj));
            Assert.That(result, !Contains.Substring("\"{"));
        }

        [Test]
        public void FromBoolTrueShouldCreateSuccessOperationResult()
        {
            OperationResult<string> result = OperationResult<string>.FromBool(true);

            Assert.IsTrue(result.Status == OperationStatus.Success);
        }

        [Test]
        public void FromBoolFalseShouldCreateErrorOperationResult()
        {
            OperationResult<string> result = OperationResult<string>.FromBool(false);

            Assert.IsTrue(result.Status == OperationStatus.Error);
        }

        private JsonDocument EnsureValidJson(string result)
        {
            try
            {
                return JsonDocument.Parse(result);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                throw;
            }
        }
    }
}