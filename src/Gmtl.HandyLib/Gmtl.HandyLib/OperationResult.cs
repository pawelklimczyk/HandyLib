﻿// -------------------------------------------------------------------------------------------------------------------
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
        public OperationStatus Status { get; set; }

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

        public static explicit operator bool(OperationResult<T> operationResult)
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