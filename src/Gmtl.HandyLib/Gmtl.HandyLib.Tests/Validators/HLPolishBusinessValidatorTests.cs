using Gmtl.HandyLib.Validators;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests.Validators
{
    [TestFixture]
    public class HLPolishBusinessValidatorTests
    {
        [TestCase("1234563218")]
        public void IsValidNIP_shouldReturnTrue_forValidNip(string input)
        {
            Assert.IsTrue(HLPolishBusinessValidator.IsValidNIP(input));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("123456789")]
        [TestCase("12345678901")]
        [TestCase("1234563219")]
        [TestCase("0000000030")]
        [TestCase("12345A3218")]
        public void IsValidNIP_shouldReturnFalse_forInvalidNip(string input)
        {
            Assert.IsFalse(HLPolishBusinessValidator.IsValidNIP(input));
        }

        [TestCase("0000000")]
        [TestCase("0000030")]
        [TestCase("000000000")]
        [TestCase("000000030")]
        [TestCase("00000000000000")]
        public void IsValidREGON_shouldReturnTrue_forValidRegon(string input)
        {
            Assert.IsTrue(HLPolishBusinessValidator.IsValidREGON(input));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("123456")]
        [TestCase("12345678")]
        [TestCase("1234567890")]
        [TestCase("1234567890123")]
        [TestCase("123456789012345")]
        [TestCase("000000001")]
        [TestCase("12A456789")]
        public void IsValidREGON_shouldReturnFalse_forInvalidRegon(string input)
        {
            Assert.IsFalse(HLPolishBusinessValidator.IsValidREGON(input));
        }

        [TestCase("44051401458")]
        [TestCase("00000000000")]
        public void IsValidPESEL_shouldReturnTrue_forValidPesel(string input)
        {
            Assert.IsTrue(HLPolishBusinessValidator.IsValidPESEL(input));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("4405140145")]
        [TestCase("440514014580")]
        [TestCase("44051401459")]
        [TestCase("4405140145A")]
        public void IsValidPESEL_shouldReturnFalse_forInvalidPesel(string input)
        {
            Assert.IsFalse(HLPolishBusinessValidator.IsValidPESEL(input));
        }
    }
}