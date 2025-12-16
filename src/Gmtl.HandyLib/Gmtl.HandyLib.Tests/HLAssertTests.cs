// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLPhoneValidatorTests.cs" project="Gmtl.HandyLib.Tests" date="2025-12-15 18:29">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------


using NUnit.Framework;
using System;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLAssertTests
    {
        #region String overload tests

        [Test]
        public void AssertArgument_String_ParamNameNull_ThrowsArgumentNullExceptionForParamName()
        {
            // paramName is null -> method should throw ArgumentNullException for the paramName parameter itself
            var ex = Assert.Throws<ArgumentNullException>(() => HLAssert.AssertArgument("value", null));
            Assert.AreEqual("paramName", ex.ParamName);
        }

        [Test]
        public void AssertArgument_String_ArgumentNull_ThrowsArgumentNullException_WithProvidedParamName()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => HLAssert.AssertArgument((string)null, "myParam"));
            Assert.AreEqual("myParam", ex.ParamName);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        [TestCase("\n")]
        public void AssertArgument_String_EmptyOrWhitespace_ThrowsArgumentException_WithProvidedParamName(string input)
        {
            var ex = Assert.Throws<ArgumentException>(() => HLAssert.AssertArgument(input, "p"));
            Assert.AreEqual("p", ex.ParamName);
            StringAssert.Contains("empty or whitespace", ex.Message.ToLowerInvariant());
        }

        [Test]
        public void AssertArgument_String_Valid_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => HLAssert.AssertArgument("valid", "p"));
        }

        [Test]
        public void AssertArgument_String_EmptyParamName_AllowsEmptyParamNameAndUsesItInException()
        {
            // paramName is empty string - method only checks for null, so empty is allowed.
            var ex = Assert.Throws<ArgumentNullException>(() => HLAssert.AssertArgument((string)null, string.Empty));
            Assert.AreEqual(string.Empty, ex.ParamName);
        }

        #endregion

        #region Object overload tests

        [Test]
        public void AssertArgument_Object_ParamNameNull_ThrowsArgumentNullExceptionForParamName()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => HLAssert.AssertArgument((object)null, null));
            Assert.AreEqual("paramName", ex.ParamName);
        }

        [Test]
        public void AssertArgument_Object_ArgumentNull_ThrowsArgumentNullException_WithProvidedParamName()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => HLAssert.AssertArgument((object)null, "objParam"));
            Assert.AreEqual("objParam", ex.ParamName);
        }

        [Test]
        public void AssertArgument_Object_NotNull_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => HLAssert.AssertArgument(new object(), "objParam"));
        }

        [Test]
        public void AssertArgument_Object_EmptyParamName_AllowsEmptyParamNameAndUsesItInException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => HLAssert.AssertArgument((object)null, string.Empty));
            Assert.AreEqual(string.Empty, ex.ParamName);
        }

        #endregion

        #region Overload resolution and ambiguity checks

        [Test]
        public void AssertArgument_NullCastToString_InvokesStringOverload()
        {
            // Explicit cast to string should call the string overload and therefore throw with provided param name
            var ex = Assert.Throws<ArgumentNullException>(() => HLAssert.AssertArgument((string)null, "sParam"));
            Assert.AreEqual("sParam", ex.ParamName);
        }

        [Test]
        public void AssertArgument_NullCastToObject_InvokesObjectOverload()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => HLAssert.AssertArgument((object)null, "oParam"));
            Assert.AreEqual("oParam", ex.ParamName);
        }

        #endregion

    }
}
