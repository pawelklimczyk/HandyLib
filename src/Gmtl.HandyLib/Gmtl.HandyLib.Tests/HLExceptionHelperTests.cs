// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLExceptionHelperTests.cs" project="Gmtl.HandyLib.Tests" date="2015-11-02 07:30">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLExceptionHelperTests
    {
        private string serializedException;

        [Test]
        public void ExceptionHelper_providedException_shouldSerializeIt()
        {
            //Arrange
            string exceptionMessage = "TestMessage";
            Exception exc = new Exception(exceptionMessage);

            //Act
            serializedException = exc.ToXmlString(true);

            //Assert
            var xml = deserializeException(serializedException);

            Assert.That(xml.Name.LocalName, Is.EqualTo("System.Exception"));
            Assert.That(xml.Nodes().Count(), Is.EqualTo(1));

            XElement messageNode = xml.FirstNode as XElement;
            Assert.That(messageNode, Is.Not.Null);
            Assert.That(messageNode.Value, Is.EqualTo(exceptionMessage));
        }

        [Test]
        public void ExceptionHelper_providedExceptionWith1StackTrace_shouldSerializeIt()
        {
            //Arrange
            string exceptionMessage = "TestMessage";
            Exception exc = CreateExceptionWithStackTrace(exceptionMessage);

            //Act
            serializedException = exc.ToXmlString(true);

            //Assert
            var xml = deserializeException(serializedException);

            List<XElement> xElements = xml.Nodes().OfType<XElement>().ToList();
            Assert.That(xml.Name.LocalName, Is.EqualTo("System.Exception"));
            Assert.That(xElements.Count, Is.EqualTo(2));

            Assert.That(xElements.Where(n => n.Name.LocalName == "StackTrace").Count(), Is.EqualTo(1));
            Assert.That(xElements.Where(n => n.Name.LocalName == "StackTrace").FirstOrDefault().Nodes().Count(), Is.EqualTo(1));

            XElement messageNode = xElements[0];
            Assert.That(messageNode, Is.Not.Null);
            Assert.That(messageNode.Value, Is.EqualTo(exceptionMessage));
        }

        [Test]
        [Ignore("On AppVeyor it claims to have just 2 stack levels")]
        public void ExceptionHelper_providedExceptionWith5StackTrace_shouldSerializeIt()
        {
            //Arrange
            string exceptionMessage = "TestMessage";
            Exception exc = CreateExceptionWithStackTrace(exceptionMessage, 4 /* starting with 0 :) */);

            //Act
            serializedException = exc.ToXmlString(true);

            //Assert
            var xml = deserializeException(serializedException);

            List<XElement> xElements = xml.Nodes().OfType<XElement>().ToList();
            Assert.That(xml.Name.LocalName, Is.EqualTo("System.Exception"));
            Assert.That(xElements.Count, Is.EqualTo(2));

            Assert.That(xElements.Where(n => n.Name.LocalName == "StackTrace").Count(), Is.EqualTo(1));
            Assert.That(xElements.Where(n => n.Name.LocalName == "StackTrace").FirstOrDefault().Nodes().Count(), Is.EqualTo(5));

            XElement messageNode = xElements[0];
            Assert.That(messageNode, Is.Not.Null);
            Assert.That(messageNode.Value, Is.EqualTo(exceptionMessage));
        }

        [Test]
        public void ExceptionHelper_providedExceptionAndInnerException_shouldSerializeIt()
        {
            //Arrange
            string exceptionMessage = "TestMessage";
            string innerExceptionMessage = "TestInnerMessage";
            Exception exc = new Exception(exceptionMessage, new Exception(innerExceptionMessage));

            //Act
            serializedException = exc.ToXmlString(true);

            //Assert
            var xml = deserializeException(serializedException);
            
            List<XElement> xElements = xml.Nodes().OfType<XElement>().ToList();
            Assert.That(xml.Name.LocalName, Is.EqualTo("System.Exception"));
            Assert.That(xElements.Count, Is.EqualTo(2));

            Assert.That(xElements.Count(n => n.Name.LocalName == "System.Exception"), Is.EqualTo(1));
            Assert.That(xElements[0].Value, Is.EqualTo(exceptionMessage));
            Assert.That(xElements[1].Value, Is.EqualTo(innerExceptionMessage));
        }
        
        private Exception CreateExceptionWithStackTrace(string exceptionMessage, int depth = 0)
        {
            Action<string, int> drillDownStackTrace2 = (s, i) => { };//dummy implementation

            Action<string, int> drillDownStackTrace = (message, step) =>
            {
                if (step == 0)
                {
                    throw new Exception(exceptionMessage);
                }

                step--;
                drillDownStackTrace2(message, step);
            };

            drillDownStackTrace2 = (message, step) =>
            {
                if (step == 0)
                {
                    throw new Exception(exceptionMessage);
                }

                step--;
                drillDownStackTrace(message, step);
            };
            try
            {
                if (depth == 0)
                {
                    throw new Exception(exceptionMessage);
                }

                drillDownStackTrace(exceptionMessage, depth - 1);

                return new Exception("Never reaches this point");
            }
            catch (Exception exc)
            {
                return exc;
            }
        }

        private static XElement deserializeException(string exception)
        {
            return XElement.Parse(exception);

        }
    }
}