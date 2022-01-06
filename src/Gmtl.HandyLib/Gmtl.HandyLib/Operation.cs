using System;

namespace Gmtl.HandyLib
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
                    return OperationResult<bool>.Success(message: SuccessMsg);
                }
                else
                {
                    return OperationResult<bool>.Error(message: ErrorMsg);
                }
            }
            catch
            {
                return OperationResult<bool>.Error(message: ErrorMsg);
            }
        }


        public static OperationResult<bool> All(params Operation[] operations)
        {
            foreach (var op in operations)
            {
                var r = op.Execute();
                if (!r)
                    return r;
            }

            return OperationResult<bool>.Success();
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