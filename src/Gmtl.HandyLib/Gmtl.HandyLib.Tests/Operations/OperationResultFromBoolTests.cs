using Gmtl.HandyLib.Operations;
using NUnit.Framework;
using System.Linq;

namespace Gmtl.HandyLib.Tests.Operations
{
    [TestFixture]
    public class OperationResultFromBoolTests
    {
        [Test]
        public void FromBoolTrueShouldCreateSuccessOperationResult()
        {
            OperationResult result = OperationResult.FromBool(true);

            Assert.IsTrue(result.Status == OperationStatus.Success);
        }

        [Test]
        public void FromBoolFalseShouldCreateErrorOperationResult()
        {
            OperationResult result = OperationResult.FromBool(false);

            Assert.IsTrue(result.Status == OperationStatus.Error);
        }

        [Test]
        public void OperationResultShouldBeCastedToBool()
        {
            OperationResult result1 = OperationResult.FromBool(false);

            Assert.IsTrue(result1 == false);

            OperationResult result2 = OperationResult.FromBool(true);

            Assert.IsTrue(result2 == true);
        }

        [Test]
        public void OperationResultShouldCopyErrorsFromPreviousOperationResult()
        {
            OperationResult result1 = OperationResult.FromBool(false);
            result1.AddError("Error1", "message1");
            result1.AddError("Error2", "message2");

            OperationResult result2 = OperationResult.Error(result1);
            Assert.IsTrue(result2.Errors.Any(e => e.Key == "Error1" && e.Value == "message1"));
            Assert.IsTrue(result2.Errors.Any(e => e.Key == "Error2" && e.Value == "message2"));
            Assert.IsTrue(result2.Errors.Count == 3);
        }

        [Test]
        public void BooleanOperationResultsShouldBeSuccess()
        {
            OperationResult result = OperationResult.Success();

            Assert.IsTrue(result.Status == OperationStatus.Success);
        }
    }
}