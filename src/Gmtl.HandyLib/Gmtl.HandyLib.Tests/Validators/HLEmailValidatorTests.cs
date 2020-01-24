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
        public void HLEmailValidator_shouldBeValidEmail(string input)
        {
            Assert.IsTrue(HLEmailValidator.IsValidEmail(input));
        }

        [TestCase("test")]
        [TestCase("test@test")]
        public void HLEmailValidator_shouldBeInvalidEmail(string input)
        {
            Assert.IsFalse(HLEmailValidator.IsValidEmail(input));
        }
    }
}