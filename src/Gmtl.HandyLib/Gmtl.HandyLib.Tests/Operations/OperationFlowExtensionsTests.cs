using Gmtl.HandyLib.Operations;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests.Operations
{
    [TestFixture]
    public class OperationFlowExtensionsTests
    {
        [Test]
        public void IfSuccessShouldInvokeActionForSuccessfulOperationResult()
        {
            var result = OperationResult.Success("ok");
            var called = false;

            var returned = result.IfSuccess(current =>
            {
                called = true;
                Assert.AreSame(result, current);
                Assert.That(current.IsSuccess, Is.True);
            });

            Assert.That(called, Is.True);
            Assert.AreSame(result, returned);
        }

        [Test]
        public void IfSuccessShouldSkipActionForFailedOperationResult()
        {
            var result = OperationResult.Error("error");
            var called = false;

            var returned = result.IfSuccess(_ => called = true);

            Assert.That(called, Is.False);
            Assert.AreSame(result, returned);
        }

        [Test]
        public void IfErrorShouldInvokeActionForFailedOperationResult()
        {
            var result = OperationResult.Error("error");
            var called = false;

            var returned = result.IfError(current =>
            {
                called = true;
                Assert.AreSame(result, current);
                Assert.That(current.IsSuccess, Is.False);
            });

            Assert.That(called, Is.True);
            Assert.AreSame(result, returned);
        }

        [Test]
        public void IfErrorShouldSkipActionForSuccessfulOperationResult()
        {
            var result = OperationResult.Success("ok");
            var called = false;

            var returned = result.IfError(_ => called = true);

            Assert.That(called, Is.False);
            Assert.AreSame(result, returned);
        }

        [Test]
        public void GenericIfSuccessShouldInvokeActionForSuccessfulOperationResult()
        {
            var result = OperationResult<int>.Success(42, "ok");
            var called = false;

            var returned = result.IfSuccess(current =>
            {
                called = true;
                Assert.AreSame(result, current);
                Assert.That(current.IsSuccess, Is.True);
                Assert.That(current.Value, Is.EqualTo(42));
            });

            Assert.That(called, Is.True);
            Assert.AreSame(result, returned);
        }

        [Test]
        public void GenericIfSuccessShouldSkipActionForFailedOperationResult()
        {
            var result = OperationResult<int>.Error(42, "error");
            var called = false;

            var returned = result.IfSuccess(_ => called = true);

            Assert.That(called, Is.False);
            Assert.AreSame(result, returned);
        }

        [Test]
        public void GenericIfErrorShouldInvokeActionForFailedOperationResult()
        {
            var result = OperationResult<int>.Error(42, "error");
            var called = false;

            var returned = result.IfError(current =>
            {
                called = true;
                Assert.AreSame(result, current);
                Assert.That(current.IsSuccess, Is.False);
                Assert.That(current.Value, Is.EqualTo(42));
            });

            Assert.That(called, Is.True);
            Assert.AreSame(result, returned);
        }

        [Test]
        public void GenericIfErrorShouldSkipActionForSuccessfulOperationResult()
        {
            var result = OperationResult<int>.Success(42, "ok");
            var called = false;

            var returned = result.IfError(_ => called = true);

            Assert.That(called, Is.False);
            Assert.AreSame(result, returned);
        }

        [Test]
        public void IfSuccessShouldThrowWhenActionIsNullAndResultIsSuccessful()
        {
            var result = OperationResult.Success("ok");

            Assert.Throws<System.NullReferenceException>(() => result.IfSuccess((System.Action<OperationResult>)null));
        }

        [Test]
        public void IfSuccessShouldNotThrowWhenActionIsNullAndResultIsError()
        {
            var result = OperationResult.Error("error");

            Assert.DoesNotThrow(() => result.IfSuccess((System.Action<OperationResult>)null));
        }

        [Test]
        public void IfErrorShouldThrowWhenActionIsNullAndResultIsError()
        {
            var result = OperationResult.Error("error");

            Assert.Throws<System.NullReferenceException>(() => result.IfError((System.Action<OperationResult>)null));
        }

        [Test]
        public void IfErrorShouldNotThrowWhenActionIsNullAndResultIsSuccessful()
        {
            var result = OperationResult.Success("ok");

            Assert.DoesNotThrow(() => result.IfError((System.Action<OperationResult>)null));
        }

        [Test]
        public void GenericIfSuccessShouldThrowWhenActionIsNullAndResultIsSuccessful()
        {
            var result = OperationResult<int>.Success(1, "ok");

            Assert.Throws<System.NullReferenceException>(() => result.IfSuccess((System.Action<OperationResult<int>>)null));
        }

        [Test]
        public void GenericIfErrorShouldThrowWhenActionIsNullAndResultIsError()
        {
            var result = OperationResult<int>.Error(1, "error");

            Assert.Throws<System.NullReferenceException>(() => result.IfError((System.Action<OperationResult<int>>)null));
        }

        [Test]
        public void IfSuccessAndIfErrorShouldSupportChaining()
        {
            var result = OperationResult.Success("ok");
            var successCalled = false;
            var errorCalled = false;

            var returned = result
                .IfSuccess(_ => successCalled = true)
                .IfError(_ => errorCalled = true);

            Assert.That(successCalled, Is.True);
            Assert.That(errorCalled, Is.False);
            Assert.AreSame(result, returned);
        }

        [Test]
        public void GenericIfSuccessAndIfErrorShouldSupportChaining()
        {
            var result = OperationResult<int>.Error(42, "error");
            var successCalled = false;
            var errorCalled = false;

            var returned = result
                .IfSuccess(_ => successCalled = true)
                .IfError(_ => errorCalled = true);

            Assert.That(successCalled, Is.False);
            Assert.That(errorCalled, Is.True);
            Assert.AreSame(result, returned);
        }
    }
}