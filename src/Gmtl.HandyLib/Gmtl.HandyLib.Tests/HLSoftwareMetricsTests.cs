using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLSoftwareMetricsTests
    {
        [Test]
        public void HLSoftwareMetrics_shouldReturnMetrics()
        {
            Assert.IsNotEmpty(HLSoftwareMetrics.Title);
            Assert.IsNotNull(HLSoftwareMetrics.Title);
            
            Assert.IsNotEmpty(HLSoftwareMetrics.Version);
            Assert.IsNotNull(HLSoftwareMetrics.Version);
        }
    }
}