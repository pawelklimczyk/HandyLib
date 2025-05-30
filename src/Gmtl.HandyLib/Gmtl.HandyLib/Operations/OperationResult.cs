﻿// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="OperationResult.cs" project="Gmtl.HandyLib" date="2016-04-02 17:09">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class OperationResult<T> : OperationResult
    {
        /// <summary>
        /// Result value
        /// </summary>
        public T Value { get; set; }

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
                builder.AppendLine(string.Join(",", Errors.Select(e =>
                {
                    StringBuilder b = new StringBuilder();
                    b.AppendLine("{\"" + e.Key + "\":\"" + e.Value + "\"}");
                    return b.ToString();
                })));
                builder.AppendLine("]");
            }
            else
            {
                builder.AppendLine("");
            }

            builder.AppendLine("}");

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

            result.AddError(GeneralError, message);

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

            foreach (var parentError in parentResult.Errors)
            {
                result.AddError(parentError.Key, parentError.Value);
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

            result.AddErrors(errors);

            return result;
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

    /// <summary>
    /// Boolean OperationResult
    /// </summary>
    public class OperationResult
    {
        public const string GeneralError = "GeneralError";

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
        public ReadOnlyDictionary<string, string> Errors => new ReadOnlyDictionary<string, string>(_errors);

        private Dictionary<string, string> _errors = new Dictionary<string, string>();
        private static object _locker = new object();

        public OperationResult AddError(string errorKey, string message)
        {
            lock (_locker)
            {
                if (_errors.ContainsKey(errorKey))
                {
                    _errors[errorKey] += ". " + message;
                }
                else
                {
                    _errors.Add(errorKey, message);
                }
            }

            Status = OperationStatus.Error;

            return this;
        }

        public OperationResult AddErrors(Dictionary<string, string> errors)
        {
            foreach (var error in errors)
            {
                AddError(error.Key, error.Value);
            }

            return this;
        }

        public static OperationResult FromBool(bool isSuccess, string successMessage = "bool-success", string errorMessage = "bool-error")
        {
            var result = new OperationResult
            {
                Status = isSuccess ? OperationStatus.Success : OperationStatus.Error,
                Message = isSuccess ? successMessage : errorMessage
            };

            if (!isSuccess)
                result.AddError(GeneralError, errorMessage);

            return result;
        }

        public static OperationResult Error(string message)
        {
            var result = new OperationResult
            {
                Message = message,
                Status = OperationStatus.Error
            };

            result.AddError(GeneralError, message);

            return result;
        }

        public static OperationResult<T> Error<T>(string message)
        {
            var result = new OperationResult<T>
            {
                Message = message,
                Status = OperationStatus.Error
            };

            result.AddError(GeneralError, message);

            return result;
        }

        public static OperationResult<T> Error<T>(T obj, string message)
        {
            var result = new OperationResult<T>
            {
                Value = obj,
                Message = message,
                Status = OperationStatus.Error
            };

            result.AddError(GeneralError, message);

            return result;
        }

        public static OperationResult<T> FromOperationResult<T>(T obj, OperationResult innerOperationResult)
        {
            if (innerOperationResult == null)
                throw new ArgumentNullException(nameof(innerOperationResult));

            var result = new OperationResult<T>
            {
                Value = obj,
                Message = innerOperationResult.Message,
                Status = innerOperationResult.Status
            };

            foreach (var error in innerOperationResult.Errors)
            {
                result.AddError(error.Key, error.Value);
            }

            return result;
        }


        public static OperationResult Error(OperationResult parentResult)
        {
            if (parentResult == null)
                throw new ArgumentNullException("parentResult");

            var result = new OperationResult
            {
                Message = parentResult.Message,
                Status = OperationStatus.Error
            };

            foreach (var parentError in parentResult.Errors)
            {
                result.AddError(parentError.Key, parentError.Value);
            }

            return result;
        }

        //public  OperationResult Error(string key, string message)
        //{
        //    var result = new OperationResult
        //    {
        //        Message = message,
        //        Status = OperationStatus.Error
        //    };

        //    result.AddError(key, message);

        //    return result;
        //}

        public static OperationResult Error(Dictionary<string, string> errors)
        {
            if (errors == null)
                throw new ArgumentNullException("errors");
            if (errors.Count == 0)
                throw new ArgumentException("errors");

            var result = new OperationResult
            {
                Message = "ERROR",
                Status = OperationStatus.Error
            };

            result.AddErrors(errors);

            return result;
        }
        public static OperationResult Success()
        {
            return new OperationResult
            {
                Status = OperationStatus.Success
            };
        }

        public static OperationResult Success(string message)
        {
            return new OperationResult
            {
                Message = message,
                Status = OperationStatus.Success
            };
        }

        public static OperationResult<T> Success<T>(string message)
        {
            return new OperationResult<T>
            {
                Message = message,
                Status = OperationStatus.Success
            };
        }

        public static OperationResult<T> Success<T>(T obj, string message)
        {
            return new OperationResult<T>
            {
                Value = obj,
                Message = message,
                Status = OperationStatus.Success
            };
        }

        public static implicit operator bool(OperationResult operationResult)
        {
            return operationResult.Status == OperationStatus.Success;
        }
    }
}