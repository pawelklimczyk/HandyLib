// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLPhoneValidatorTests.cs" project="Gmtl.HandyLib.Tests" date="2021-06-02 18:29">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using Gmtl.HandyLib.Validators;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests.Validators
{
    [TestFixture]
    public class HLPhoneValidatorTests
    {
        [TestCase("500 50505053")]
        [TestCase("5 00 505 0 5053")]
        [TestCase("5 00   505 0   50 53")]
        [TestCase("500 45 34 31")]
        public void HLPhoneValidator_shouldBeValidPhoneNumber(string input)
        {
            Assert.IsTrue(HLPhoneValidator.IsValidPhoneNumber(input));
        }

        [TestCase("test")]
        [TestCase("test@test.com")]
        [TestCase("2134TRT")]
        [TestCase("56666999")]
        public void HLPhoneValidator_shouldBeInvalidPhoneNumber(string input)
        {
            Assert.IsFalse(HLPhoneValidator.IsValidPhoneNumber(input));
        }
    }
}