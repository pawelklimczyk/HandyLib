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
            var sample = new SampleXmlTestClass
            {
                A = HLRandomizer.RandomDouble.Next(0, 100),
                B = HLRandomizer.RandomString.Next(5, 10),
                C = DateTime.Now
            };

            string xmlText = HLSerializer.SerializeToXml(sample);
            var deserized = HLSerializer.DeserializeFromXml<SampleXmlTestClass>(xmlText);

            Assert.That(deserized.A, Is.EqualTo(sample.A));
            Assert.That(deserized.B, Is.EqualTo(sample.B));
            Assert.That(deserized.C, Is.EqualTo(sample.C));
        }

        [Test]
        public void ShouldSerializeAndDeserializeClassToFile()
        {
            var sample = new SampleXmlTestClass { A = 42, B = "test", C = DateTime.UtcNow };
            string file = System.IO.Path.GetTempFileName();
            try
            {
                sample.SerializeToXmlFile(file);
                var loaded = HLSerializer.DeserializeFromXmlFile<SampleXmlTestClass>(file);
                Assert.That(loaded.A, Is.EqualTo(sample.A));
                Assert.That(loaded.B, Is.EqualTo(sample.B));
                Assert.That(loaded.C, Is.EqualTo(sample.C));
            }
            finally
            {
                if (System.IO.File.Exists(file)) System.IO.File.Delete(file);
            }
        }

        [Test]
        public void ShouldThrowOnDeserializeFromMissingFile()
        {
            string file = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.Guid.NewGuid() + ".xml");
            var ex = Assert.Throws<System.IO.FileNotFoundException>(() => HLSerializer.DeserializeFromXmlFile<SampleXmlTestClass>(file));
            Assert.That(ex.FileName, Is.EqualTo(file));
        }

        [Test]
        public void ShouldReturnEmptyStringOnNullSerializeToXml()
        {
            SampleXmlTestClass sample = null;
            string xml = HLSerializer.SerializeToXml(sample);
            Assert.That(xml, Is.EqualTo(string.Empty));
        }

        [Test]
        public void ShouldThrowOnInvalidXmlDeserialize()
        {
            string invalidXml = "<not-a-valid-xml>";
            Assert.Throws<System.InvalidOperationException>(() => HLSerializer.DeserializeFromXml<SampleXmlTestClass>(invalidXml));
        }

#if NETCOREAPP3_1_OR_GREATER
        [Test]
        public void ShouldSerializeAndDeserializeJson()
        {
            var sample = new SampleXmlTestClass { A = 123, B = "json", C = DateTime.UtcNow };
            string json = HLSerializer.SerializeToJson(sample);
            var loaded = HLSerializer.DeserializeFromJson<SampleXmlTestClass>(json);
            Assert.That(loaded.A, Is.EqualTo(sample.A));
            Assert.That(loaded.B, Is.EqualTo(sample.B));
            Assert.That(loaded.C, Is.EqualTo(sample.C));
        }

        [Test]
        public void ShouldThrowOnInvalidJsonDeserialize()
        {
            string invalidJson = "{ not: valid json }";
            Assert.Throws<System.Text.Json.JsonException>(() => HLSerializer.DeserializeFromJson<SampleXmlTestClass>(invalidJson));
        }

        [Test]
        public void ShouldSerializeExceptionToJson()
        {
            var ex = new InvalidOperationException("fail", new ArgumentNullException("param"));
            string json = HLSerializer.SerializeException(ex);
            Assert.That(json, Does.Contain("fail"));
            Assert.That(json, Does.Contain("param"));
        }
#endif

        [Test]
        public void ShouldHandleEmptyStringDeserializeXml()
        {
            Assert.Throws<System.InvalidOperationException>(() => HLSerializer.DeserializeFromXml<SampleXmlTestClass>(""));
        }
    }

    public class SampleXmlTestClass
    {
        public int A { get; set; }
        public string B { get; set; }
        public DateTime C { get; set; }
    }
}