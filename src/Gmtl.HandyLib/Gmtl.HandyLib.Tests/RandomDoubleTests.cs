// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="RandomDoubleTests.cs" project="Gmtl.HandyLib.Tests" date="2016-09-17 12:54">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class RandomDoubleTests
    {
        [TestCase(1, 2)]
        [TestCase(49, 51)]
        public void HLRandomizerDouble_providedMinAndMaxAsIn_shouldGenerateValueInRange(int min, int max)
        {
            var generatedValue = (int)HLRandomizer.RandomDouble.Next(min, max);

            Assert.That(generatedValue, Is.LessThanOrEqualTo(max));
            Assert.That(generatedValue, Is.GreaterThanOrEqualTo(min));
        }

        [TestCase(1.1, 2.1)]
        [TestCase(49.9999, 51.001)]
        [TestCase(-51.001, - 49.9999)]
        public void HLRandomizerDouble_providedMinAndMax_shouldGenerateValueInRange(double min, double max)
        {
            var generatedValue = HLRandomizer.RandomDouble.Next(min, max, 5);

            Assert.That(generatedValue, Is.LessThanOrEqualTo(max));
            Assert.That(generatedValue, Is.GreaterThanOrEqualTo(min));
        }
    }
}