using Gmtl.HandyLib.Extensions;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLObjectExtensionsTests
    {
        [Test]
        public void ShouldListAllPropertiesAndTheirValues()
        {
            //Arrange
            TestClass obj = new TestClass {IntValue = 123, StringValue = "strVal"};

            //Act
            string result = obj.PropertyList();

            //Assert
            StringAssert.Contains("123", result);
            StringAssert.Contains("IntValue", result);
            StringAssert.Contains("strVal", result);
            StringAssert.Contains("StringValue", result);
        }

        class TestClass
        {
            public int IntValue { get; set; }
            public string StringValue { get; set; }
        }
    }
}