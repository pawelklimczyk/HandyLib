// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLSerializerTests.cs" project="Gmtl.HandyLib.Tests" date="2017-10-21 19:13">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests.HLSerializerTests
{
    [TestFixture]
    public class HLSerializerTests
    {
        [Test]
        public void ShouldSerializeAndDeserializeClass()
        {
            SampleXmlTestClass sample = new SampleXmlTestClass
            {
                A = HLRandomizer.RandomDouble.Next(0, 100),
                B = HLRandomizer.RandomString.Next(5, 10),
                C = DateTime.Now
            };

            string xmlText=HLSerializer.SerializeToXml(sample);
            SampleXmlTestClass deserized = HLSerializer.DeserializeFromXml<SampleXmlTestClass>(xmlText);

            Assert.That(deserized.A,Is.EqualTo(sample.A));
            Assert.That(deserized.B, Is.EqualTo(sample.B));
            Assert.That(deserized.C, Is.EqualTo(sample.C));
        }
    }

    public class SampleXmlTestClass
    {
        public int A { get; set; }
        public string B { get; set; }
        public DateTime C { get; set; }
    }
}