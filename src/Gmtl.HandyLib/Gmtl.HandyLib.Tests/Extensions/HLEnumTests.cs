using NUnit.Framework;
using Gmtl.HandyLib.Extensions;

namespace Gmtl.HandyLib.Tests.Extensions
{
    [TestFixture]
    public class HLEnumTests
    {

        [TestCase(TestEnum.Value1, "Value 1")]
        [TestCase(TestEnum.Value100, "Value 100")]
        [TestCase(TestEnum.ValueWithoutDescription, "ValueWithoutDescription")]
        public void HLEnumTests_providedEnumDescription_shouldBeValid(TestEnum val, string expectedOutput)
        {
            string actualOutput = val.GetDescription();
            Assert.AreEqual(actualOutput, expectedOutput);
        }
    }

    public enum TestEnum
    {
        [System.ComponentModel.Description("Value 1")]
        Value1 = 1,
        [System.ComponentModel.Description("Value 100")]
        Value100 = 100,
        ValueWithoutDescription = -1
    }
}
