using System.Collections.Generic;
using NUnit.Framework;
using Randomizer = Gmtl.HandyLib.Random.Randomizer;

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
                uniqueStringList.Add(Randomizer.Instance.RandomString.Next(10, 100));
            }

            //Assert
            Assert.That(uniqueStringList, Is.Unique);
        }

        [Test]
        public void RandomString_shouldReturnRandomStringOfSize16()
        {
            //Arrange
            int itemsCount = 100;
            int stringLength = 16;
            List<string> uniqueStringList = new List<string>();

            //Act
            for (int i = 0; i < itemsCount; i++)
            {
                uniqueStringList.Add(Randomizer.Instance.RandomString.Next(stringLength, stringLength));
            }

            //Assert
            for (int i = 0; i < itemsCount; i++)
            {
                Assert.That(uniqueStringList[i].Length, Is.EqualTo(stringLength));
            }
        }
    }
}