using Gmtl.HandyLib.Operations;
using NUnit.Framework;
using System.Linq;

namespace Gmtl.HandyLib.Tests.Operations
{
    [TestFixture]
    public class OperationExecuteAllTests
    {
        [Test]
        public void ExecuteAll_All_PassOperation_PassLogic()
        {
            Operation<bool> operation1 = Operation<bool>.Create(() => true);
            Operation<bool> operation2 = Operation<bool>.Create(() => true);
            Operation<bool> operation3 = Operation<bool>.Create(() => true);


            var results = Operation<bool>.ExecuteAll(false, operation1, operation2, operation3);

            Assert.IsTrue(results.Count() == 3);
            Assert.IsTrue(results.All(r => r.IsSuccess));
            Assert.IsTrue(results.All(r => r.Value == true));
        }

        [Test]
        public void ExecuteAll_All_PassOperation_FailLogic()
        {
            Operation<bool> operation1 = Operation<bool>.Create(() => true);
            Operation<bool> operation2 = Operation<bool>.Create(() => true);
            Operation<bool> operation3 = Operation<bool>.Create(() => false);

            var results = Operation<bool>.ExecuteAll(false, operation1, operation2, operation3);

            Assert.IsTrue(results.Count() == 3);
            Assert.IsTrue(results.All(r => r.IsSuccess));
            Assert.IsTrue(results.Any(r => r.Value == true));
            Assert.IsTrue(results.Any(r => r.Value == false));
        }

        [Test]
        public void ExecuteAll_All_FailOperation_PassLogic()
        {
            Operation<bool> operation1 = Operation<bool>.Create(() => throw new System.Exception());
            Operation<bool> operation2 = Operation<bool>.Create(() => true);
            Operation<bool> operation3 = Operation<bool>.Create(() => true);

            var results = Operation<bool>.ExecuteAll(false, operation1, operation2, operation3);

            Assert.IsTrue(results.Count() == 3);
            Assert.IsTrue(results.Any(r => r.IsSuccess));
            Assert.IsTrue(results.Any(r => !r.IsSuccess));
            Assert.IsTrue(results.Any(r => r.Value == true));
            
            //we would expect that when operation fails then value is in available.
            //But current .net language does not support nullable T
            //Assert.IsTrue(results.All(r => r.Value != false));
        }

        [Test]
        public void ExecuteAll_All_FailOperation_FailLogic()
        {
            Operation<bool> operation1 = Operation<bool>.Create(() => throw new System.Exception());
            Operation<bool> operation2 = Operation<bool>.Create(() => false);
            Operation<bool> operation3 = Operation<bool>.Create(() => true);

            var results = Operation<bool>.ExecuteAll(false, operation1, operation2, operation3);

            Assert.IsTrue(results.Count() == 3);
            Assert.IsTrue(results.Any(r => r.IsSuccess));
            Assert.IsTrue(results.Any(r => !r.IsSuccess));
            Assert.IsTrue(results.Any(r => r.Value == true));
            Assert.IsTrue(results.Any(r => r.Value == false));
        }
    }
}
