// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="RandomStringTests.cs" project="Gmtl.HandyLib.Tests" date="2015-09-20 15:59:22">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class RandomStringTests
    {
        [Test]
        public void RandomString_shouldReturnRandomString()
        {
            //Arrange
            List<string> uniqueStringList = new List<string>();

            //Act
            for (int i = 0; i < 100; i++)
            {
                uniqueStringList.Add(HLRandomizer.HLRandomizer.RandomString.Next(10, 100));
            }

            //Assert
            Assert.That(uniqueStringList, Is.Unique);
        }

        [TestCase(4)]
        [TestCase(16)]
        [TestCase(32)]
        public void RandomString_shouldReturnRandomStringOfSizeX(int stringLength)
        {
            //Arrange
            int itemsCount = 100;
            List<string> uniqueStringList = new List<string>();

            //Act
            for (int i = 0; i < itemsCount; i++)
            {
                uniqueStringList.Add(HLRandomizer.HLRandomizer.RandomString.NextExact(stringLength));
            }

            //Assert
            for (int i = 0; i < itemsCount; i++)
            {
                Assert.That(uniqueStringList[i].Length, Is.EqualTo(stringLength));
            }
        }
    }
}