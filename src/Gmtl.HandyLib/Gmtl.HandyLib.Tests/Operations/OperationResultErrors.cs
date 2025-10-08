using Gmtl.HandyLib.Operations;
using NUnit.Framework;
using System;

namespace Gmtl.HandyLib.Tests.Operations
{
    [TestFixture]
    public class OperationResultErrors
    {
        [Test]
        public void ErrorsAsString_NoErrors_ReturnsEmptyString()
        {
            var result = OperationResult.Success();

            var errors = result.ErrorsAsString();

            Assert.That(errors, Is.Empty);
        }

        [Test]
        public void ErrorsAsString_OneError_ReturnsFormattedError()
        {
            var result = OperationResult.Error("Test error message");

            var errors = result.ErrorsAsString();

            Assert.That(errors, Is.EqualTo($"{OperationResult.GeneralError}: Test error message"));
        }

        [Test]
        public void ErrorsAsString_MultipleErrors_ReturnsAllErrorsFormatted()
        {
            var result = new OperationResult();
            result.AddError("Error1", "First error message");
            result.AddError("Error2", "Second error message");

            var errors = result.ErrorsAsString();

            var expectedErrors =
                "Error1: First error message" + Environment.NewLine +
                "Error2: Second error message";

            Assert.That(errors, Is.EqualTo(expectedErrors));
        }
    }
}
