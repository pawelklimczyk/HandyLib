// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="OperationResult.cs" project="Gmtl.HandyLib" date="2016-04-02 17:09">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;

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
        public OperationStatus Status { get; set; }

        /// <summary>
        /// Extra info send with operation result (optional)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// return Object as a JSON string
        /// </summary>
        /// <returns></returns>
        public string AsJson()
        {
            return String.Format("{{\"status\":\"{0}\",\"message\":\"{1}\",\"data\":\"{2}\"}}", 
                Status == OperationStatus.Success ? "true" : "false",
                Message != null ? Message.Replace("\"", "'") : "", 
                Result != null ? Result.ToString() : ""); 

        }

        public static OperationResult<T> Error(T value = default(T), string message = "")
        {
            return new OperationResult<T>
            {
                Result = value,
                Message = message,
                Status = OperationStatus.Error
            };
        }

        public static OperationResult<T> Success(T value = default(T), string message = "")
        {
            return new OperationResult<T>
            {
                Result = value,
                Message = message,
                Status = OperationStatus.Success
            };
        }

        public static implicit operator bool(OperationResult<T> operationResult)
        {
            return operationResult.Status == OperationStatus.Success;
        }
    }

    /// <summary>
    /// Status of executed operation
    /// </summary>
    public enum OperationStatus
    {
        Success,
        Warning,
        Error
    }
}