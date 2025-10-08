// -------------------------------------------------------------------------------------------------------------------
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

        /// <summary>
        /// Return all errors as a single string
        /// </summary>
        public string ErrorsAsString()
        {
            if (_errors == null || _errors.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();

            for (var i = 0; i < _errors.Count; i++)
            {
                var error = _errors.ElementAt(i);
                if (i < _errors.Count - 1)
                    sb.AppendLine($"{error.Key}: {error.Value}");
                else
                    sb.Append($"{error.Key}: {error.Value}");
            }

            return sb.ToString();
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