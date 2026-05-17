using System;

namespace Gmtl.HandyLib.Operations
{
    public static class OperationFlowExtensions
    {
        public static OperationResult IfSuccess(this OperationResult operationResult, Action<OperationResult> action)
        {
            if (operationResult.IsSuccess)
            {
                action(operationResult);
            }

            return operationResult;
        }

        public static OperationResult IfError(this OperationResult operationResult, Action<OperationResult> action)
        {
            if (!operationResult.IsSuccess)
            {
                action(operationResult);
            }

            return operationResult;
        }

        public static OperationResult<T> IfSuccess<T>(this OperationResult<T> operationResult, Action<OperationResult<T>> action)
        {
            if (operationResult.IsSuccess)
            {
                action(operationResult);
            }

            return operationResult;
        }

        public static OperationResult<T> IfError<T>(this OperationResult<T> operationResult, Action<OperationResult<T>> action)
        {
            if (!operationResult.IsSuccess)
            {
                action(operationResult);
            }

            return operationResult;
        }
    }
}
