using NUnit.Framework;
using Gmtl.HandyLib.Extensions;
using System.Linq;

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

        public void HLEnumTests_shouldGetListOfValues()
        {
            var output = HLEnumExtensions.GetValues<TestEnum>().ToList();
            Assert.Contains(TestEnum.Value1, output);
            Assert.Contains(TestEnum.Value100, output);
            Assert.Contains(TestEnum.ValueWithoutDescription, output);
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
