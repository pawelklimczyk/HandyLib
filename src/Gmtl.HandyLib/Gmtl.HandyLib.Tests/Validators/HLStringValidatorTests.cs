using Gmtl.HandyLib.Validators;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests.Validators
{
    [TestFixture]
    public class HLStringValidatorTests
    {
        [TestCase("Just a string without a link", ExpectedResult = false)]
        [TestCase("Just a string without a link. ok ", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("https://", ExpectedResult = false)]
        [TestCase("https://.", ExpectedResult = false)]
        public bool ShouldNotContainWebsiteAddress(string input)
        {
            return input.ContainsWebsiteUrl();
        }

        [TestCase("[MalformedLink]example.com)", ExpectedResult = true)]
        [TestCase("example.com", ExpectedResult = true)]
        [TestCase("https://www.example.com", ExpectedResult = true)]
        [TestCase("http://example.com", ExpectedResult = true)]
        [TestCase("https://subdomain.example.com", ExpectedResult = true)]
        [TestCase("[Link](https://www.example.com)", ExpectedResult = true)]
        [TestCase("This text contains a link: https://www.example.com in the middle.", ExpectedResult = true)]
        [TestCase("This text contains a link with subfolder: https://www.example.com/path/test.php in the middle.", ExpectedResult = true)]
        public bool ShouldContainWebsiteAddress(string input)
        {
            return input.ContainsWebsiteUrl();
        }
    }
}
