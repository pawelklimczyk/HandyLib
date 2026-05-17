using Gmtl.HandyLib.Validators;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests.Validators
{
    [TestFixture]
    public class HLBankValidatorTests
    {
        [TestCase("61109010140000071219812874")]
        [TestCase("61 1090 1014 0000 0712 1981 2874")]
        [TestCase("04000000000000000000000000000000")]
        public void HLBankValidator_shouldBeValidBankAccountNumber(string input)
        {
            Assert.IsTrue(HLBankValidator.IsValidBankAccountNumber(input));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("6110901014000007121981287")]
        [TestCase("611090101400000712198128749")]
        [TestCase("00109010140000071219812874")]
        [TestCase("6110901014000007121981287A")]
        [TestCase("PL61109010140000071219812874")]
        [TestCase("02000000000000000000000000000000")]
        public void HLBankValidator_shouldBeInvalidBankAccountNumber(string input)
        {
            Assert.IsFalse(HLBankValidator.IsValidBankAccountNumber(input));
        }
    }
}