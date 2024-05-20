// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLStringTests.cs" project="Gmtl.HandyLib.Tests" date="2015-10-22 20:24">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLStringTests
    {
        private string defaultValue = "default";

        [TestCase("0987654")]
        [TestCase("!~@#@*%^")]
        [TestCase("test string")]
        [TestCase("TEST STRING")]
        public void HLString_providedNonEmptyString_shouldReturnThatString(string inputString)
        {
            //Act
            string result = HLString.ValueOrDefault(inputString, defaultValue);

            //Assert
            Assert.That(result, Is.EqualTo(inputString));
        }

        [Test]
        public void HLString_providedEmptyString_shouldReturnDefaultValue()
        {
            //Act
            string result = HLString.ValueOrDefault(String.Empty, defaultValue);

            //Assert
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("     ")]
        public void HLString_providedWhitespacesString_shouldReturnDefaultValue(string inputString)
        {
            //Act
            string result = HLString.ValueOrDefault(inputString, defaultValue);

            //Assert
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void HLString_providedNUll_shouldReturnDefaultValue()
        {
            //Act
            string result = HLString.ValueOrDefault(null, defaultValue);

            //Assert
            Assert.That(result, Is.EqualTo(defaultValue));
        }

        [Test]
        public void HLString_providedNullValue_shouldReturnEmpty()
        {
            //Act
            string result = HLString.ValueOrEmpty(null);

            //Assert
            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void HLString_providedEmptyValue_shouldReturnEmpty()
        {
            //Act
            string result = HLString.ValueOrEmpty(String.Empty);

            //Assert
            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [TestCase("0987654")]
        [TestCase("!~@#@*%^")]
        [TestCase("test string")]
        [TestCase("TEST STRING")]
        public void HLString_providedValue_shouldReturnIn(string input)
        {
            //Act
            string result = HLString.ValueOrEmpty(input);

            //Assert
            Assert.That(result, Is.EqualTo(input));
        }

        [TestCase("paweł", "pawel")]
        [TestCase("Paweł", "Pawel")]
        [TestCase("ąćźż", "aczz")]
        public void HLString_providedString_shouldRemoveNonStandardLetters(string inputString, string expectedOutput)
        {
            //Act
            string result = HLString.RemoveAccents(inputString);

            //Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("paweł", "pawel")]
        [TestCase("Paweł", "pawel")]
        [TestCase("Paweł Klimczyk", "pawel-klimczyk")]
        [TestCase(" Paweł Klimczyk", "pawel-klimczyk")]
        [TestCase(" Paweł Klimczyk ", "pawel-klimczyk")]
        [TestCase(" Paweł-Klimczyk-", "pawel-klimczyk")]
        [TestCase("ąćźż", "aczz")]
        public void HLString_providedString_shouldCreateUrlFriendlyText(string inputString, string expectedOutput)
        {
            //Act
            string result = HLString.ToSlug(inputString);

            //Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("<h1>test</h1>", "test")]
        [TestCase("<p>test 123 123</p>", "test 123 123")]
        [TestCase("<br/>test 123 123", "test 123 123")]
        [TestCase("<br />test 123 123", "test 123 123")]
        [TestCase("<br>test 123 123", "test 123 123")]
        [TestCase("<br >test 123 123", "test 123 123")]
        public void HLString_shouldStripHtml(string inputString, string expectedOutput)
        {
            //Act
            string result = HLString.StripHtml(inputString);

            //Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("<h1>test</h1>", "<h1>test</h1>")]
        [TestCase("<h1><span>test</span></h1>", "<h1><span>test</span></h1>")]
        [TestCase("<h1 style=\"teststyle\" attr='test attr'><span style=\"teststyle\" attr='test attr'>test</span></h1>", "<h1><span>test</span></h1>")]
        [TestCase("<h1 style=\"teststyle\" attr='test attr'>test</h1>", "<h1>test</h1>")]
        [TestCase("<br/>test 123 123", "<br/>test 123 123")]
        [TestCase("<br />test 123 123", "<br/>test 123 123")]
        [TestCase("<br style=\"teststyle\" attr='test attr' />test 123 123", "<br/>test 123 123")]
        [TestCase("<br style=\"teststyle\" attr='test attr'/>test 123 123", "<br/>test 123 123")]
        public void HLString_shouldCleanHtml(string inputString, string expectedOutput)
        {
            //Act
            string result = HLString.CleanHtml(inputString);

            //Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("test", "Test")]
        [TestCase("ławka", "Ławka")]
        [TestCase("ławka leśna", "Ławka leśna")]
        [TestCase("TEST", "TEST")]
        [TestCase("<TEST>", "<TEST>")]
        public void HLString_shouldMakeFirstCharacterUppercase(string inputString, string expectedOutput)
        {
            //Act
            string result = HLString.FirstLetterToUpper(inputString);

            //Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("<TEST>")]
        [TestCase("")]
        public void HLString_shouldReturnInput(string inputString)
        {
            //Act
            string result = HLString.FirstLetterToUpper(inputString);

            //Assert
            Assert.That(result, Is.EqualTo(inputString));
        }

        [TestCase(null)]
        public void HLString_shouldReturnEmptyString(string inputString)
        {
            //Act
            string result = HLString.FirstLetterToUpper(inputString);

            //Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [TestCase("teestee", "e", "teste")]
        [TestCase("aa  bb cc    dd", " ", "aa bb cc dd")]
        [TestCase("aa__!__!__!rer__!rere____!111____!__!111", "__!", "aa__!rer__!rere____!111____!111")]
        [TestCase("<TEST>", "", "<TEST>")]
        [TestCase("<TEST>", "  ", "<TEST>")]
        [TestCase("<TE    ST>", "  ", "<TE  ST>")]
        [TestCase("<TEST><br/><br/></p>", "<br/>", "<TEST><br/></p>")]
        [TestCase("<TEST><br /><br /></p>", "<br />", "<TEST><br /></p>")]
        public void HLString_shouldReplacedMultipleCharacters(string inputString, string charactersToReplace, string expectedOutput)
        {
            //Act
            string result = HLString.ReplaceMulti(inputString, charactersToReplace);

            //Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }


        [TestCase("paweł", "paweł")]
        [TestCase(" Adam-Smith-", " Adam Smith ")]
        [TestCase(" Adam-%%6Smith-", " Adam Smith ")]
        public void HLString_providedString_shouldReplaceNonAlphabeticCharactersWithSpace(string inputString, string expectedOutput)
        {
            //Act
            string result = HLString.ReplaceNonAlphabetic(inputString);

            //Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("paweł", "paweł")]
        [TestCase(" Adam-Smith-", "*Adam*Smith*")]
        [TestCase(" Adam-%%6Smith-", "*Adam*Smith*")]
        public void HLString_providedString_shouldReplaceNonAlphabeticCharactersWithAsterics(string inputString, string expectedOutput)
        {
            //Act
            string result = HLString.ReplaceNonAlphabetic(inputString, "*");

            //Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("paweł", "paweł")]
        [TestCase(" Adam-Smith-", "*Adam*Smith*")]
        [TestCase(" Adam-%%6Smith-", "*Adam****Smith*")]
        [TestCase("## Adam-%%6Smith-  ", "***Adam****Smith***")]
        public void HLString_providedString_shouldReplaceNonAlphabeticCharactersWithMultipleAsterics(string inputString, string expectedOutput)
        {
            //Act
            string result = HLString.ReplaceNonAlphabetic(inputString, "*", false);

            //Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }



        [Test]
        public void SplitByLineBreaks_InputIsNull_ReturnsEmptyList()
        {
            // Arrange
            string input = null;

            // Act
            var result = input.SplitByLineBreaks();

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void SplitByLineBreaks_InputIsEmptyString_ReturnsEmptyList()
        {
            // Arrange
            string input = string.Empty;

            // Act
            var result = input.SplitByLineBreaks();

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void SplitByLineBreaks_InputHasSingleLine_ReturnsSingleElementList()
        {
            // Arrange
            string input = "Single line without line breaks";

            // Act
            var result = input.SplitByLineBreaks();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Single line without line breaks", result[0]);
        }

        [Test]
        public void SplitByLineBreaks_InputHasMultipleLineBreaks_ReturnsCorrectSplit()
        {
            // Arrange
            string input = "Line1\r\nLine2\nLine3\rLine4<br/>Line5";

            // Act
            var result = input.SplitByLineBreaks();

            // Assert
            var expected = new List<string> { "Line1", "Line2", "Line3", "Line4", "Line5" };
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SplitByLineBreaks_InputHasConsecutiveLineBreaks_ReturnsCorrectSplit()
        {
            // Arrange
            string input = "Line1\r\n\r\nLine2\n\nLine3\r\rLine4<br/><br/>Line5";

            // Act
            var result = input.SplitByLineBreaks();

            // Assert
            var expected = new List<string> { "Line1", "Line2", "Line3", "Line4", "Line5" };
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SplitByLineBreaks_InputHasMixedContent_ReturnsCorrectSplit()
        {
            // Arrange
            string input = "First line\r\nSecond line\nThird line\rFourth line<br/>Fifth line";

            // Act
            var result = input.SplitByLineBreaks();

            // Assert
            var expected = new List<string> { "First line", "Second line", "Third line", "Fourth line", "Fifth line" };
            Assert.AreEqual(expected, result);
        }
    }
}