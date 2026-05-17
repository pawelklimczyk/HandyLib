using NUnit.Framework;
using Gmtl.HandyLib;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLFileContentHelperTests
    {
        [TestCase("application/pdf", ExpectedResult = "pdf")]
        [TestCase("image/png", ExpectedResult = "png")]
        [TestCase("image/jpg", ExpectedResult = "jpg")]
        [TestCase("image/jpeg", ExpectedResult = "jpeg")]
        [TestCase("application/unknown", ExpectedResult = null)]
        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = null)]
        public string GetFileExtFromContentType_ReturnsExpected(string contentType)
        {
            return HLFileContentHelper.GetFileExtFromContentType(contentType);
        }

        [TestCase("file.pdf", ExpectedResult = "application/pdf")]
        [TestCase("image.png", ExpectedResult = "image/png")]
        [TestCase("photo.jpg", ExpectedResult = "image/jpg")]
        [TestCase("photo.jpeg", ExpectedResult = "image/jpeg")]
        [TestCase("unknownfile.abc", ExpectedResult = "application/octet-stream")]
        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = null)]
        public string GetContentTypeFromFilename_ReturnsExpected(string filename)
        {
            return HLFileContentHelper.GetContentTypeFromFilename(filename);
        }
    }
}
