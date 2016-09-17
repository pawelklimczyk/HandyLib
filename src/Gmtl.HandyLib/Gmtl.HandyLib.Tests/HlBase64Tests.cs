// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLBase64Tests.cs" project="Gmtl.HandyLib.Tests" date="2016-09-17 12:47">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLBase64Tests
    {
        [TestCase("sample test", "c2FtcGxlIHRlc3Q=")]
        [TestCase("1234567890", "MTIzNDU2Nzg5MA==")]
        public void HLBase64_providedSampleText_shouldEncodeIt(string input, string expectedOutput)
        {
            Assert.That(HLBase64.Encode(input), Is.EqualTo(expectedOutput));
        }

        public void HLBase64_shouldEncodeAndDecodeText()
        {
            string text = "Sample text for testing.";
            Assert.That(HLBase64.Decode(HLBase64.Encode(text)), Is.EqualTo(text));
        }
    }
}