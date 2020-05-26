// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="OperationResult.cs" project="Gmtl.HandyLib" date="2016-04-02 17:09">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System.Text;

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
            StringBuilder builder = new StringBuilder();

            builder.Append("{");

            builder.Append("\"status\":\"" + (Status == OperationStatus.Success ? "true" : "false") + "\",");
            builder.Append("\"message\":\"" + (Message != null ? Message.Replace("\"", "'") : "") + "\",");

            if (typeof(T) == typeof(string))
            {
                builder.Append("\"data\":\"" + (Result != null ? Result.ToString() : "") + "\"");
            }
            else
            {
                //assume we have complex object, so add {}
                builder.Append("\"data\":{" + (Result != null ? Result.ToString() : "") + "}");
            }

            builder.Append("}");

            return builder.ToString();
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
        public static OperationResult<T> Error(string message)
        {
            return Error(default(T), message);
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

        public static OperationResult<T> Success(string message)
        {
            return Success(default(T), message);
        }

        public static implicit operator bool(OperationResult<T> operationResult)
        {
            return operationResult.Status == OperationStatus.Success;
        }
    }
}