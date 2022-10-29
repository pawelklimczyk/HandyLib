// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="OperationResult.cs" project="Gmtl.HandyLib" date="2016-04-02 17:09">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Text;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Generic class working as a wrapper
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    [DebuggerDisplay("{Result} {Status}")]
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
        public string AsJson(string dataInParantheses = null)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("{");
            builder.Append("\"status\":\"" + (Status == OperationStatus.Success ? "true" : "false") + "\",");
            builder.Append("\"message\":\"" + (Message != null ? Message.Replace("\"", "'") : "") + "\",");

            if (dataInParantheses != null)
            {
                builder.Append("\"data\":" + dataInParantheses);
            }
            else
            {

                Type type = typeof(T);

                if (type.IsPrimitive || type.IsEnum || type == typeof(decimal))
                {
                    builder.Append("\"data\":" + (Result != null ? Result.ToString() : ""));
                }
                else if (type == typeof(string))
                {
                    builder.Append("\"data\":\"" + (Result != null ? Result.ToString() : "") + "\"");
                }
                else
                {
                    string objectAsJson = Result != null ? Result.ToString() : "";
                    if (objectAsJson.StartsWith("{")) //if object has { don't add it
                        builder.Append("\"data\":" + objectAsJson);
                    else
                        //assume we have complex object, so add {}
                        builder.Append("\"data\":{" + objectAsJson + "}");
                }
            }

            builder.Append("}");

            return builder.ToString();
        }

        public static OperationResult<T> Error(T value = default(T), string message = "Default error status message")
        {
            return new OperationResult<T>
            {
                Result = value,
                Message = message,
                Status = OperationStatus.Error
            };
        }
        public static OperationResult<T> Error(string message)
        {
            return Error(default(T), message);
        }

        public static OperationResult<T> Success(T value = default(T), string message = "Default Ok status message")
        {
            return new OperationResult<T>
            {
                Result = value,
                Message = message,
                Status = OperationStatus.Success
            };
        }

        public static OperationResult<T> Success(string message)
        {
            return Success(default(T), message);
        }

        public static OperationResult<T> FromBool(bool isSuccess, string successMessage = "", string errorMessage = "")
        {
            return (isSuccess) ? Success(default(T), successMessage) : Error(default(T), errorMessage);
        }

        public static OperationResult<T> FromBool(bool isSuccess, T value, string successMessage = "", string errorMessage = "")
        {
            return (isSuccess) ? Success(value, successMessage) : Error(value, errorMessage);
        }

        public static implicit operator bool(OperationResult<T> operationResult)
        {
            return operationResult.Status == OperationStatus.Success;
        }
    }
}