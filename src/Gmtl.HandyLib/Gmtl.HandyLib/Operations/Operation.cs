using System;

namespace Gmtl.HandyLib.Operations
{
    /// <summary>
    /// Class for managing multiple operations and their statuses
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Function to be executed
        /// </summary>
        public Func<bool> Func { get; private set; }

        /// <summary>
        /// Message if Func return false
        /// </summary>
        public string ErrorMsg { get; private set; }

        /// <summary>
        /// Message if Func return true
        /// </summary>
        public string SuccessMsg { get; private set; }

        /// <summary>
        /// Executed Func and returns a result
        /// </summary>
        /// <returns></returns>
        public OperationResult<bool> Execute()
        {
            try
            {
                bool result = Func();
                if (result)
                {
                    return OperationResult<bool>.Success(true, SuccessMsg);
                }
                else
                {
                    return OperationResult<bool>.Error(false, ErrorMsg);
                }
            }
            catch
            {
                return OperationResult<bool>.Error(false, ErrorMsg);
            }
        }

        /// <summary>
        /// Executed all operation in sequece
        /// </summary>
        public static OperationResult<bool> ExecuteAll(params Operation[] operations)
        {
            foreach (var op in operations)
            {
                var r = op.Execute();
                if (!r)
                    return r;
            }

            return OperationResult<bool>.Success(value: true, "All operations were successful");
        }

        public static Operation Create(bool result, string errorMsg = "Error", string successMsg = "Ok")
        {
            return Create(() => result, errorMsg, successMsg);
        }

        /// <summary>
        /// Create operation that was already executed. Just pass the result
        /// </summary>
        public static Operation Create(OperationResult<bool> result)
        {
            return Create(() => result.Result, result.Message, result.Message);
        }

        public static Operation Create(Func<bool> func, string errorMsg = "Error", string successMsg = "Ok")
        {
            return new Operation()
            {
                Func = func,
                ErrorMsg = errorMsg,
                SuccessMsg = successMsg
            };
        }
    }
}