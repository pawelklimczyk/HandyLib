// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLEmailValidatorTests.cs" project="Gmtl.HandyLib.Tests" date="2020-01-24 18:29">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using Gmtl.HandyLib.Validators;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests.Validators
{
    [TestFixture]
    public class HLEmailValidatorTests
    {
        [TestCase("test@test.com")]
        [TestCase("TEST.TEST+tag@example.co.uk")]
        [TestCase("user_name-123@example-domain.org")]
        [TestCase("  test@test.com  ")]
        [TestCase("zażółć@example.pl")]
        public void HLEmailValidator_shouldBeValidEmail(string input)
        {
            Assert.IsTrue(HLEmailValidator.IsValidEmail(input));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("test")]
        [TestCase("test@test")]
        [TestCase("test@.com")]
        [TestCase("test@@test.com")]
        [TestCase("test test@test.com")]
        public void HLEmailValidator_shouldBeInvalidEmail(string input)
        {
            Assert.IsFalse(HLEmailValidator.IsValidEmail(input));
        }
    }
}