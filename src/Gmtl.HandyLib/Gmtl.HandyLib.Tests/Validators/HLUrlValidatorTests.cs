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
    public class HLUrlValidatorTests
    {
        [TestCase("http://onet.pl")]
        [TestCase("https://onet.pl")]
        [TestCase("https://aplikuj.hrlink.pl/aplikacja/Doradca-Finansowy-w-punkcie-kredytowym/43237-131481-33-19b-1647.html")]
        public void HLUrlValidator_shouldBeUrl(string input)
        {
            Assert.IsTrue(HLUrlValidator.IsValidUrl(input));
        }

        [TestCase("test")]
        [TestCase("test@test.com")]
        [TestCase("http/onet.pl")]
        [TestCase("aplikuj.hrlink.pl/aplikacja/Doradca-Finansowy-w-punkcie-kredytowym/43237-131481-33-19b-1647.html")]
        public void HLUrlValidator_shouldBeInvalidUrl(string input)
        {
            Assert.IsFalse(HLUrlValidator.IsValidUrl(input));
        }
    }
}