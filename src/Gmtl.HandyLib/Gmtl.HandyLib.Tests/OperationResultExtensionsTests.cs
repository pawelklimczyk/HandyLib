using Gmtl.HandyLib.Operations;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class OperationResultExtensionsTests
    {
        [Test]
        public void TestIfSuccess()
        {
            var assertObject = new AsserObject();
            var operation = Operation<int>.Create(() => 1)
                .IfSuccess(() => ValidateSuccess(assertObject));
            operation.Execute();

            Assert.True(assertObject.Called);
        }

        [Test]
        public void TestIfSuccessAndAlways()
        {
            var assertObject = new AsserObject();
            var assertObject2 = new AsserObject();
            var operation = Operation<int>.Create(() => 1)
                .IfSuccess(() => ValidateSuccess(assertObject))
                .Always(() => ValidateAlways(assertObject2));

            operation.Execute();

            Assert.True(assertObject.Called);
            Assert.True(assertObject2.Called);
        }

        [Test]
        public void TestIfError()
        {
            int a = 0;
            var assertObject = new AsserObject();
            var assertObject2 = new AsserObject();
            var operation = Operation<int>.Create(() => 1 / a)
                .IfError(() => ValidateError(assertObject))
                .IfError(() => ValidateError(assertObject2));

            operation.Execute();

            Assert.True(assertObject.Called);
            Assert.True(assertObject2.Called);
        }

        [Test]
        public void TestChainedOperations()
        {
            var operationObject = new OperationObject();
            var mainOperation = Operation<OperationObject>.Create(() => { operationObject.Operation1 = true; })
                .IfSuccess(() => 
                    Operation<OperationObject>.Create(() => { operationObject.Operation2 = true; })
                    .IfSuccess(() => operationObject.Operation3 = true)
                    .Execute());

            mainOperation.Execute();

            Assert.True(operationObject.Operation1);
            Assert.True(operationObject.Operation2);
            Assert.True(operationObject.Operation3);
        }

        private void ValidateSuccess(AsserObject obj)
        {
            obj.Called = true;
        }

        private void ValidateError(AsserObject obj)
        {
            obj.Called = true;
        }

        private void ValidateAlways(AsserObject obj)
        {
            obj.Called = true;
        }

        private class AsserObject
        {
            public bool Called = false;
        }

        private class OperationObject
        {
            public bool Operation1 = false;
            public bool Operation2 = false;
            public bool Operation3 = false;
        }
    }
}