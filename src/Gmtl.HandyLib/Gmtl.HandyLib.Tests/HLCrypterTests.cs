using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLCrypterTests
    {
        [Test]
        public void EncryptString_ThenDecryptString_ShouldReturnOriginalText()
        {
            const string plainText = "Hello, HandyLib!";

            string encrypted = HLCrypter.EncryptString(plainText);
            string decrypted = HLCrypter.DecryptString(encrypted);

            Assert.AreNotEqual(plainText, encrypted);
            Assert.AreEqual(plainText, decrypted);
        }

        [Test]
        public void EncryptString_CalledTwiceWithSameInput_ShouldProduceDifferentCipherText()
        {
            const string plainText = "same input";

            string encryptedFirst = HLCrypter.EncryptString(plainText);
            string encryptedSecond = HLCrypter.EncryptString(plainText);

            Assert.AreNotEqual(encryptedFirst, encryptedSecond);
        }

        [Test]
        public void DecryptString_WithInvalidCipherText_ShouldReturnEmptyString()
        {
            string result = HLCrypter.DecryptString("not-a-valid-base64-cipher-text");

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void SetEntropy_WithDifferentSeed_ShouldPreventDecryptionOfPreviouslyEncryptedText()
        {
            const string plainText = "seed sensitive text";

            HLCrypter.SetEntropy("seed-one");
            string encrypted = HLCrypter.EncryptString(plainText);

            HLCrypter.SetEntropy("seed-two");
            string decrypted = HLCrypter.DecryptString(encrypted);

            Assert.AreEqual(string.Empty, decrypted);

            HLCrypter.SetEntropy("seed-one");
            Assert.AreEqual(plainText, HLCrypter.DecryptString(encrypted));
        }
    }
}
