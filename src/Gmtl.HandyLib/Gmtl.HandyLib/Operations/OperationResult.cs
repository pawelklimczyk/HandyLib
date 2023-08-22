// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="OperationResult.cs" project="Gmtl.HandyLib" date="2016-04-02 17:09">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gmtl.HandyLib.Operations
{
    /// <summary>
    /// Generic class working as a wrapper
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    [DebuggerDisplay("{Value} {Status}")]
    public class OperationResult<T>
    {
        /// <summary>
        /// Result value
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Operation status
        /// </summary>
        public OperationStatus Status { get; set; }

        /// <summary>
        /// Indicates if operation was successful
        /// </summary>
        public bool IsSuccess
        {
            get { return Status == OperationStatus.Success; }
        }

        /// <summary>
        /// Extra info send with operation result (optional)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// List of potential errors from the Operation
        /// </summary>
        public List<SubError> Errors { get; set; } = new List<SubError>();

        /// <summary>
        /// return Object as a JSON string
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public string AsJson()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("{");
            builder.AppendLine("\"status\":\"" + (Status == OperationStatus.Success ? "true" : "false") + "\",");
            builder.AppendLine("\"message\":\"" + (Message != null ? ReplaceDoubleQuote(Message) : "") + "\",");

            Type type = typeof(T);

            if (type.IsPrimitive || type.IsEnum || type == typeof(decimal))
            {
                builder.Append("\"data\":" + (Value != null ? Value.ToString() : ""));
            }
            else if (type == typeof(string))
            {
                builder.Append("\"data\":\"" + (Value != null ? ReplaceDoubleQuote(Value.ToString()) : "") + "\"");
            }
            else
            {
                string objectAsJson = Value != null ? Value.ToString() : "";
                if (objectAsJson.StartsWith("{")) //if object has { don't add it
                    builder.Append("\"data\":" + objectAsJson);
                else
                    //assume we have complex object, so add {}
                    builder.Append("\"data\":{" + objectAsJson + "}");
            }

            if (Errors != null && Errors.Any())
            {
                builder.AppendLine(",");
                builder.AppendLine("\"errors\": [");
                builder.AppendLine(string.Join(",", Errors.Select(e => AddErrorToJsonResult(e))));
                builder.AppendLine("]");
            }
            else
            {
                builder.AppendLine("");
            }

            builder.AppendLine("}");

            return builder.ToString();
        }

        public class SubError
        {
            public string ErrorKey { get; set; }

            public string ErrorMessage { get; set; }
        }

        private string AddErrorToJsonResult(SubError error)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("{\"" + error.ErrorKey + "\":\"" + error.ErrorMessage + "\"}");

            return builder.ToString();
        }

        public static OperationResult<T> Error(T value, string message = "ERROR OCCURED")
        {
            var result = new OperationResult<T>
            {
                Value = value,
                Message = message,
                Status = OperationStatus.Error
            };

            return result;
        }

        public static OperationResult<T> Error<T2>(T value, OperationResult<T2> parentResult)
        {
            if (parentResult == null)
                throw new ArgumentNullException("parentResult");

            var result = new OperationResult<T>
            {
                Value = value,
                Message = "ERROR",
                Status = OperationStatus.Error
            };
            
            foreach(var parentError in parentResult.Errors)
            {
                result.Errors.Add(new SubError { ErrorKey = parentError.ErrorKey, ErrorMessage = parentError.ErrorMessage });
            }

            return result;
        }

        public static OperationResult<T> Error(T value, Dictionary<string, string> errors)
        {
            if (errors == null)
                throw new ArgumentNullException("errors");
            if (errors.Count == 0)
                throw new ArgumentException("errors");

            var result = new OperationResult<T>
            {
                Value = value,
                Message = "ERROR",
                Status = OperationStatus.Error
            };

            return result.AddErrors(errors);
        }

        public OperationResult<T> AddError(string errorKey, string message)
        {
            Errors.Add(new SubError { ErrorKey = errorKey, ErrorMessage = message });

            return this;
        }

        public OperationResult<T> AddErrors(Dictionary<string, string> errors)
        {
            foreach (var error in errors)
            {
                Errors.Add(new SubError { ErrorKey = error.Key, ErrorMessage = error.Value });
            }

            return this;
        }

        public static OperationResult<T> Error(string message)
        {
            return Error(default, message);
        }

        public static OperationResult<T> Success(T value = default, string message = "OK")
        {
            return new OperationResult<T>
            {
                Value = value,
                Message = message,
                Status = OperationStatus.Success
            };
        }

        public static OperationResult<T> Success(string message)
        {
            return Success(default, message);
        }

        public static OperationResult<T> FromBool(bool isSuccess, string successMessage = "", string errorMessage = "")
        {
            return isSuccess ? Success(default, successMessage) : Error(default, errorMessage);
        }

        public static OperationResult<T> FromBool(bool isSuccess, T value, string successMessage = "", string errorMessage = "")
        {
            return isSuccess ? Success(value, successMessage) : Error(value, errorMessage);
        }

        public static implicit operator bool(OperationResult<T> operationResult)
        {
            return operationResult.Status == OperationStatus.Success;
        }

        private static string ReplaceDoubleQuote(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            return input.Replace("\"", "'");
        }
    }

    public class OperationResult : OperationResult<bool> { }
}