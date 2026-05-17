using Gmtl.HandyLib.Operations;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Gmtl.HandyLib.Tests.Operations
{
    [TestFixture]
    public class OperationResultExtendedTests
    {
        [Test]
        public void AddError_ShouldUpdateStatusToError()
        {
            var result = new OperationResult();

            result.AddError("TestKey", "Test error message");

            Assert.That(result.Status, Is.EqualTo(OperationStatus.Error));
            Assert.That(result.Errors.ContainsKey("TestKey"), Is.True);
            Assert.That(result.Errors["TestKey"], Is.EqualTo("Test error message"));
        }

        [Test]
        public void AddErrors_ShouldAddMultipleErrors()
        {
            var result = new OperationResult();
            var errors = new Dictionary<string, string>
            {
                { "Error1", "First error" },
                { "Error2", "Second error" }
            };

            result.AddErrors(errors);

            Assert.That(result.Errors.Count, Is.EqualTo(2));
            Assert.That(result.Errors["Error1"], Is.EqualTo("First error"));
            Assert.That(result.Errors["Error2"], Is.EqualTo("Second error"));
        }

        [Test]
        public void FromBool_ShouldReturnCorrectOperationResult()
        {
            var successResult = OperationResult.FromBool(true, "Success", "Error");
            var errorResult = OperationResult.FromBool(false, "Success", "Error");

            Assert.That(successResult.IsSuccess, Is.True);
            Assert.That(successResult.Message, Is.EqualTo("Success"));

            Assert.That(errorResult.IsSuccess, Is.False);
            Assert.That(errorResult.Message, Is.EqualTo("Error"));
        }

        [Test]
        public void GenericSuccess_ShouldSetCorrectValue()
        {
            var result = OperationResult<string>.Success("TestValue", "Success message");

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.EqualTo("TestValue"));
            Assert.That(result.Message, Is.EqualTo("Success message"));
        }

        [Test]
        public void GenericError_ShouldSetCorrectValueAndMessage()
        {
            var result = OperationResult<string>.Error("TestValue", "Error message");

            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Value, Is.EqualTo("TestValue"));
            Assert.That(result.Message, Is.EqualTo("Error message"));
        }

        [Test]
        public void CombineOperationResults_ShouldReturnCombinedResult()
        {
            var operations = new[]
            {
                OperationResult<bool>.Success(true),
                OperationResult<bool>.Success(false),
                OperationResult<bool>.Error(false, "Error occurred")
            };

            var combinedResult = operations.CombineOperationResults();

            Assert.That(combinedResult.IsSuccess, Is.False);
            Assert.That(combinedResult.Message, Is.EqualTo("Error occurred"));
        }
    }
}