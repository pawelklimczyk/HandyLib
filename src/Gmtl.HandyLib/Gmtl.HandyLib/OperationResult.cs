// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="OperationResult.cs" project="Gmtl.HandyLib" date="2016-04-02 17:09">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Generic class working as a wrapper
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    public class OperationResult<T>
    {
        /// <summary>
        /// Result value
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Operation status
        /// </summary>
        public OperationalStatus Status { get; set; }

        /// <summary>
        /// Extra info send with operation result (optional)
        /// </summary>
        public string Message { get; set; }

        public static OperationResult<T> Error(T value = default(T), string message = "")
        {
            return new OperationResult<T>
            {
                Result = value,
                Message = message,
                Status = OperationalStatus.Error
            };
        }

        public static OperationResult<T> Success(T value = default(T), string message = "")
        {
            return new OperationResult<T>
            {
                Result = value,
                Message = message,
                Status = OperationalStatus.Success
            };
        }
    }
    /// <summary>
    /// Status of executed operation
    /// </summary>
    public enum OperationalStatus
    {
        Success,
        Warning,
        Error
    }
}