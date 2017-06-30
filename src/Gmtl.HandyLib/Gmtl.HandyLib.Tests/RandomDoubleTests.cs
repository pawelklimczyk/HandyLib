// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="RandomDoubleTests.cs" project="Gmtl.HandyLib.Tests" date="2016-09-17 12:54">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
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
            var generatedValue = HLRandomizer.RandomDouble.Next(min, max);

            Assert.That(generatedValue, Is.LessThanOrEqualTo(max));
            Assert.That(generatedValue, Is.GreaterThanOrEqualTo(min));
        }

        [Test]
        public void HLRandomizerDoube_generatedValues_shouldBeUnique()
        {
            List<int> values = new List<int>();

            for (int i = 0; i < 1000; i++)
            {
                values.Add(HLRandomizer.RandomDouble.Next(1, 100000000));
            }

            int distinct = values.Distinct().Count();
            Assert.True(distinct == values.Count);
        }

        [TestCase(1.1, 2.1)]
        [TestCase(49.9999, 51.001)]
        [TestCase(-51.001, -49.9999)]
        public void HLRandomizerDouble_providedMinAndMax_shouldGenerateValueInRange(double min, double max)
        {
            var generatedValue = HLRandomizer.RandomDouble.Next(min, max, 5);

            Assert.That(generatedValue, Is.LessThanOrEqualTo(max));
            Assert.That(generatedValue, Is.GreaterThanOrEqualTo(min));
        }
    }
}