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

        #region MaxOf Tests

        [Test]
        public void MaxOf_InputShorterThanLimit_ReturnsFullString()
        {
            // Arrange
            string input = "hello";

            // Act
            string result = input.MaxOf(10);

            // Assert
            Assert.That(result, Is.EqualTo("hello"));
        }

        [Test]
        public void MaxOf_InputEqualToLimit_ReturnsFullString()
        {
            // Arrange
            string input = "hello";

            // Act
            string result = input.MaxOf(5);

            // Assert
            Assert.That(result, Is.EqualTo("hello"));
        }

        [Test]
        public void MaxOf_InputLongerThanLimit_ReturnsTruncatedString()
        {
            // Arrange
            string input = "hello world";

            // Act
            string result = input.MaxOf(5);

            // Assert
            Assert.That(result, Is.EqualTo("hello"));
        }

        [Test]
        public void MaxOf_InputLongerThanLimitWithSuffix_ReturnsTruncatedStringWithSuffix()
        {
            // Arrange
            string input = "hello world";

            // Act
            string result = input.MaxOf(5, "...");

            // Assert
            Assert.That(result, Is.EqualTo("hello..."));
        }

        [Test]
        public void MaxOf_NullInput_ReturnsEmptyString()
        {
            // Arrange
            string input = null;

            // Act
            string result = input.MaxOf(10);

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void MaxOf_EmptyInput_ReturnsEmptyString()
        {
            // Arrange
            string input = string.Empty;

            // Act
            string result = input.MaxOf(10);

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void MaxOf_ZeroLimit_ReturnsSuffix()
        {
            // Arrange
            string input = "hello";

            // Act
            string result = input.MaxOf(0, "...");

            // Assert
            Assert.That(result, Is.EqualTo("..."));
        }

        [TestCase("hello world", 1, "h")]
        [TestCase("hello world", 6, "hello ")]
        [TestCase("test", 10, "test")]
        public void MaxOf_VariousInputs_ReturnsCorrectTruncation(string input, int limit, string expected)
        {
            // Act
            string result = input.MaxOf(limit);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        #endregion

        #region RemoveUnicode Tests

        [Test]
        public void RemoveUnicode_InputWithoutUnicode_ReturnsUnchangedString()
        {
            // Arrange
            string input = "Hello World";

            // Act
            string result = input.RemoveUnicode();

            // Assert
            Assert.That(result, Is.EqualTo("Hello World"));
        }

        [Test]
        public void RemoveUnicode_InputWithControlCharacters_RemovesControlCharacters()
        {
            // Arrange
            string input = "Hello\u0001World\u0008Test";

            // Act
            string result = input.RemoveUnicode();

            // Assert
            Assert.That(result, Is.EqualTo("HelloWorldTest"));
        }

        [Test]
        public void RemoveUnicode_InputWithNullCharacter_RemovesNullCharacter()
        {
            // Arrange
            string input = "Hello\u0000World";

            // Act
            string result = input.RemoveUnicode();

            // Assert
            Assert.That(result, Is.EqualTo("HelloWorld"));
        }

        [Test]
        public void RemoveUnicode_InputWithMixedControlCharacters_RemovesAll()
        {
            // Arrange
            string input = "A\u0001B\u000AC\u001FD";

            // Act
            string result = input.RemoveUnicode();

            // Assert
            Assert.That(result, Is.EqualTo("ABCD"));
        }

        [Test]
        public void RemoveUnicode_EmptyString_ReturnsEmptyString()
        {
            // Arrange
            string input = string.Empty;

            // Act
            string result = input.RemoveUnicode();

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void RemoveUnicode_OnlyControlCharacters_ReturnsEmptyString()
        {
            // Arrange
            string input = "\u0001\u0008\u000A";

            // Act
            string result = input.RemoveUnicode();

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        #endregion

        #region FilterHtml Tests

        [Test]
        public void FilterHtml_NullInput_ReturnsEmptyString()
        {
            // Act
            string result = HLString.FilterHtml(null, new List<string>(), new List<string>());

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void FilterHtml_EmptyInput_ReturnsEmptyString()
        {
            // Act
            string result = HLString.FilterHtml(string.Empty, new List<string>(), new List<string>());

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void FilterHtml_NoAllowedTags_KeepsAllTags()
        {
            // Arrange
            string input = "<h1>Title</h1><p>Paragraph</p>";

            // Act
            string result = HLString.FilterHtml(input, new List<string>(), new List<string>());

            // Assert
            Assert.That(result, Is.EqualTo("<h1>Title</h1><p>Paragraph</p>"));
        }

        [Test]
        public void FilterHtml_AllowSpecificTags_KeepsOnlyAllowedTags()
        {
            // Arrange
            string input = "<h1>Title</h1><p>Paragraph</p>";
            var allowedTags = new List<string> { "p" };

            // Act
            string result = HLString.FilterHtml(input, allowedTags, new List<string>());

            // Assert
            // When a tag is not allowed, it's removed but content is kept
            Assert.That(result, Is.EqualTo("Title<p>Paragraph</p>"));
        }

        [Test]
        public void FilterHtml_AllowMultipleTags_KeepsAllowedTags()
        {
            // Arrange
            string input = "<h1>Title</h1><p>Paragraph</p><br/>";
            var allowedTags = new List<string> { "h1", "p" };

            // Act
            string result = HLString.FilterHtml(input, allowedTags, new List<string>());

            // Assert
            Assert.That(result, Is.EqualTo("<h1>Title</h1><p>Paragraph</p>"));
        }

        [Test]
        public void FilterHtml_AllowTagsWithAttributes_KeepsAllowedAttributes()
        {
            // Arrange
            string input = "<a href=\"test.html\" onclick=\"alert()\">Link</a>";
            var allowedTags = new List<string> { "a" };
            var allowedAttributes = new List<string> { "href" };

            // Act
            string result = HLString.FilterHtml(input, allowedTags, allowedAttributes);

            // Assert
            Assert.That(result, Is.EqualTo("<a href=\"test.html\">Link</a>"));
        }

        [Test]
        public void FilterHtml_NoAllowedAttributes_RemovesAllAttributes()
        {
            // Arrange
            string input = "<h1 id=\"title\" class=\"big\">Title</h1>";
            var allowedTags = new List<string> { "h1" };

            // Act
            string result = HLString.FilterHtml(input, allowedTags, new List<string>());

            // Assert
            Assert.That(result, Is.EqualTo("<h1>Title</h1>"));
        }

        [Test]
        public void FilterHtml_ClosingTags_PreservesTags()
        {
            // Arrange
            string input = "<div>Content</div>";
            var allowedTags = new List<string> { "div" };

            // Act
            string result = HLString.FilterHtml(input, allowedTags, new List<string>());

            // Assert
            Assert.That(result, Is.EqualTo("<div>Content</div>"));
        }

        [Test]
        public void FilterHtml_SelfClosingTags_PreservesTags()
        {
            // Arrange
            string input = "<br/><hr/><img/>";
            var allowedTags = new List<string> { "br", "hr", "img" };

            // Act
            string result = HLString.FilterHtml(input, allowedTags, new List<string>());

            // Assert
            // Note: FilterHtml normalizes tags to lowercase without the trailing slash
            Assert.That(result, Is.EqualTo("<br><hr><img>"));
        }

        [Test]
        public void FilterHtml_NullTags_DefaultsToEmptyList()
        {
            // Arrange
            string input = "<h1>Title</h1>";

            // Act
            string result = HLString.FilterHtml(input, null, new List<string>());

            // Assert
            // When allowedTags is null (becomes empty list), all tags are kept
            Assert.That(result, Is.EqualTo("<h1>Title</h1>"));
        }

        [Test]
        public void FilterHtml_NullAttributes_DefaultsToEmptyList()
        {
            // Arrange
            string input = "<h1 class=\"big\">Title</h1>";
            var allowedTags = new List<string> { "h1" };

            // Act
            string result = HLString.FilterHtml(input, allowedTags, null);

            // Assert
            Assert.That(result, Is.EqualTo("<h1>Title</h1>"));
        }

        [Test]
        public void FilterHtml_MixedCase_IsCaseInsensitive()
        {
            // Arrange
            string input = "<H1>Title</h1>";
            var allowedTags = new List<string> { "h1" };

            // Act
            string result = HLString.FilterHtml(input, allowedTags, new List<string>());

            // Assert
            // FilterHtml lowercases the tags, so <H1> becomes <h1>
            Assert.That(result, Is.EqualTo("<h1>Title</h1>"));
        }

        #endregion

        #region HowManyTimeOccurenceCharInString Tests

        [Test]
        public void HowManyTimeOccurenceCharInString_CharacterNotInString_ReturnsZero()
        {
            // Arrange
            string text = "hello world";
            char searchChar = 'z';

            // Act
            int result = text.HowManyTimeOccurenceCharInString(searchChar);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void HowManyTimeOccurenceCharInString_CharacterOccursOnce_ReturnsOne()
        {
            // Arrange
            string text = "hello world";
            char searchChar = 'w';

            // Act
            int result = text.HowManyTimeOccurenceCharInString(searchChar);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void HowManyTimeOccurenceCharInString_CharacterOccursMultipleTimes_ReturnsCorrectCount()
        {
            // Arrange
            string text = "hello world";
            char searchChar = 'l';

            // Act
            int result = text.HowManyTimeOccurenceCharInString(searchChar);

            // Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void HowManyTimeOccurenceCharInString_EmptyString_ReturnsZero()
        {
            // Arrange
            string text = string.Empty;
            char searchChar = 'a';

            // Act
            int result = text.HowManyTimeOccurenceCharInString(searchChar);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void HowManyTimeOccurenceCharInString_SingleCharacterMatch_ReturnsOne()
        {
            // Arrange
            string text = "a";
            char searchChar = 'a';

            // Act
            int result = text.HowManyTimeOccurenceCharInString(searchChar);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void HowManyTimeOccurenceCharInString_AllCharactersSame_ReturnsStringLength()
        {
            // Arrange
            string text = "aaaaaaa";
            char searchChar = 'a';

            // Act
            int result = text.HowManyTimeOccurenceCharInString(searchChar);

            // Assert
            Assert.That(result, Is.EqualTo(7));
        }

        [Test]
        public void HowManyTimeOccurenceCharInString_SpecialCharacters_ReturnsCorrectCount()
        {
            // Arrange
            string text = "a-b-c-d";
            char searchChar = '-';

            // Act
            int result = text.HowManyTimeOccurenceCharInString(searchChar);

            // Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void HowManyTimeOccurenceCharInString_CaseSensitive_DistinguishesCase()
        {
            // Arrange
            string text = "AaAaAa";
            char searchChar = 'a';

            // Act
            int result = text.HowManyTimeOccurenceCharInString(searchChar);

            // Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void HowManyTimeOccurenceCharInString_WhitespaceCharacter_CountsWhitespace()
        {
            // Arrange
            string text = "a b c d e";
            char searchChar = ' ';

            // Act
            int result = text.HowManyTimeOccurenceCharInString(searchChar);

            // Assert
            Assert.That(result, Is.EqualTo(4));
        }

        [TestCase("hello", 'l', 2)]
        [TestCase("banana", 'a', 3)]
        [TestCase("test", 't', 2)]
        [TestCase("mississippi", 's', 4)]
        public void HowManyTimeOccurenceCharInString_VariousInputs_ReturnsCorrectCount(string text, char searchChar, int expected)
        {
            // Act
            int result = text.HowManyTimeOccurenceCharInString(searchChar);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        #endregion
    }
}
