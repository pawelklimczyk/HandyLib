using Gmtl.HandyLib.Operations;
using NUnit.Framework;
using System;

namespace Gmtl.HandyLib.Tests.Operations
{
    [TestFixture]
    public class OperationTests
    {
        [Test]
        public void TestBasicFunction()
        {
            int a = 10, b = 2;
            var operation = Operation<SampleResultClass>.Create(() => PureCalculation(a, b));

            var result = operation.Execute();

            Assert.True(result.Status == OperationStatus.Success);

            Assert.AreEqual(result.Value.Sum, a + b);
            Assert.AreEqual(result.Value.Deduction, a - b);
            Assert.AreEqual(result.Value.Multiplication, a * b);
            Assert.AreEqual(result.Value.Division, a / b);
        }

        [Test]
        public void TestBasicOperationResult()
        {
            int a = 10, b = 2;
            var operation = Operation<SampleResultClass>.Create(() => Calculate(a, b));

            var result = operation.Execute();

            Assert.True(result.Status == OperationStatus.Success);

            Assert.AreEqual(result.Value.Sum, a + b);
            Assert.AreEqual(result.Value.Deduction, a - b);
            Assert.AreEqual(result.Value.Multiplication, a * b);
            Assert.AreEqual(result.Value.Division, a / b);
        }

        [Test]
        public void TestExceptionBasicFunction()
        {
            int a = 10, b = 0;
            var operation = Operation<SampleResultClass>.Create(() => PureCalculation(a, b));

            var result = operation.Execute();

            Assert.True(result.Status == OperationStatus.Error);
        }

        [Test]
        public void TestExceptionOperationResult()
        {
            int a = 10, b = 0;
            var operation = Operation<SampleResultClass>.Create(() => Calculate(a, b));

            var result = operation.Execute();

            Assert.True(result.Status == OperationStatus.Error);
        }

        public SampleResultClass PureCalculation(int a, int b)
        {
            return SampleResultClass.Create(a, b);
        }

        public OperationResult<SampleResultClass> Calculate(int a, int b)
        {
            try
            {
                var calculation = SampleResultClass.Create(a, b);

                return OperationResult<SampleResultClass>.Success(calculation);
            }
            catch (Exception exc)
            {
                return OperationResult<SampleResultClass>.Error(message: exc.Message);
            }
        }

        public class SampleResultClass
        {
            public int Sum { get; set; }
            public int Deduction { get; set; }
            public int Multiplication { get; set; }
            public int Division { get; set; }

            public static SampleResultClass Create(int a, int b)
            {
                return new SampleResultClass
                {
                    Sum = a + b,
                    Deduction = a - b,
                    Multiplication = a * b,
                    Division = a / b
                };
            }
        }
    }
}
