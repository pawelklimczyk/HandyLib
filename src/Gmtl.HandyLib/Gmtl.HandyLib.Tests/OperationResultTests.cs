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
        
        [Test]
        public void OperationShouldBeReturnedAsJsonString()
        {
            string result = OperationResult<string>.Error("test value","test message").AsJson();

            Assert.That(result, Is.EqualTo("{\"status\":\"false\",\"message\":\"test message\",\"data\":\"test value\"}"));
        }
        
        [Test]
        public void OperationShouldBeReturnedAsJsonStringWithAComplexObject()
        {
            var jsonObj = "{\"field1\":\"value1\", \"field2\":\"value2\"}";
            string result = OperationResult<string>.Error( jsonObj, "test").AsJson();

            Assert.That(result, Contains.Substring(jsonObj));
        }
    }
}