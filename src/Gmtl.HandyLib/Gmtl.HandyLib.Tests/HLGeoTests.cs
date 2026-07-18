using System;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLGeoTests
    {
        [Test]
        public void GetDistanceKm_SameCoordinates_ShouldReturnZero()
        {
            double distance = HLGeo.GetDistanceKm(52.2297, 21.0122, 52.2297, 21.0122);

            Assert.AreEqual(0, distance, 0.0001);
        }

        [Test]
        public void GetDistanceKm_WarsawToKrakow_ShouldReturnKnownApproximateDistance()
        {
            // Warsaw (52.2297, 21.0122) to Krakow (50.0647, 19.9450) is ~252 km
            double distance = HLGeo.GetDistanceKm(52.2297, 21.0122, 50.0647, 19.9450);

            Assert.AreEqual(252, distance, 5);
        }

        [Test]
        public void ToRadians_Zero_ShouldReturnZero()
        {
            Assert.AreEqual(0, HLGeo.ToRadians(0), 0.0001);
        }

        [Test]
        public void ToRadians_180Degrees_ShouldReturnPi()
        {
            Assert.AreEqual(Math.PI, HLGeo.ToRadians(180), 0.0001);
        }

        [Test]
        public void ToRadians_360Degrees_ShouldReturnTwoPi()
        {
            Assert.AreEqual(2 * Math.PI, HLGeo.ToRadians(360), 0.0001);
        }
    }
}
