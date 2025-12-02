using System.Linq;

namespace Gmtl.HandyLib.Operations
{
    /// <summary>
    /// OperationResult Extensions
    /// </summary>
    public static class OperationResultExtensions
    {
        /// <summary>
        /// Creates a successful OperationResult from the item
        /// </summary>
        public static OperationResult<T> ToOperationResult<T>(this T item)
        {
            return OperationResult<T>.Success(item);
        }

        /// <summary>
        /// Converts one OperationResult to another OperationResult type
        /// </summary>
        public static T2 ToOtherOperationResult<T1, T2>(this T1 t1)
            where T1 : OperationResult
            where T2 : OperationResult, new()
        {
            return new T2 { Message = t1.Message, Status = t1.Status };
        }

        /// <summary>
        /// Get the first error message from an array of OperationResults where IsSuccess is false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operations"></param>
        public static string GetFirstError<T>(this OperationResult<T>[] operations)
        {
            return operations.FirstOrDefault(o => !o.IsSuccess)?.Message;
        }

        /// <summary>
        /// Combines multiple OperationResult-bool- into a single OperationResult-bool-.
        /// If all operations are successful and true, returns true.
        /// If all are successful but at least one is false, returns false.
        /// If any operation failed, returns an error with the first failure message.
        /// </summary>
        /// <param name="operations">Array of OperationResult-bool- to combine.</param>
        /// <returns>A combined OperationResult-bool-.</returns>
        public static OperationResult<bool> CombineOperationResults(this OperationResult<bool>[] operations)
        {
            if (operations == null || operations.Length == 0)
            {
                return OperationResult<bool>.Error(false, "No operations provided.");
            }

            if (operations.All(o => o.IsSuccess))
            {
                if (operations.All(o => o.Value))
                {
                    return OperationResult<bool>.Success(true);
                }

                return OperationResult<bool>.Success(false);
            }

            var firstError = operations.FirstOrDefault(o => !o.IsSuccess)?.Message ?? "Unknown error occurred.";

            return OperationResult<bool>.Error(false, firstError);
        }
    }
}