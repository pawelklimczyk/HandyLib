
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
    [DebuggerDisplay("{Status} ({Message})")]
    public class OperationResult
    {
        public const string GeneralError = "GeneralError";

        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        /// <summary>
        /// Operation status
        /// </summary>
        public OperationStatus Status { get; set; }

        /// <summary>
        /// Indicates if operation was successful
        /// </summary>
        public bool IsSuccess => Status == OperationStatus.Success;

        /// <summary>
        /// Extra info send with operation result (optional)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// List of potential errors from the Operation
        /// </summary>
        public ReadOnlyDictionary<string, string> Errors => new ReadOnlyDictionary<string, string>(_errors);

        public OperationResult AddError(string errorKey, string message)
        {
            if (_errors.ContainsKey(errorKey))
            {
                _errors[errorKey] += ". " + message;
            }
            else
            {
                _errors.Add(errorKey, message);
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
            if (_errors.Count == 0)
                return string.Empty;

            return string.Join(Environment.NewLine, _errors.Select(e => $"{e.Key}: {e.Value}"));
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

        public static OperationResult Error<T>(string message, Action<T> builder = null)
        {
            if (typeof(OperationResult).IsAssignableFrom(typeof(T)))
            {
                var obj = CreateAndConfigureResult<T>(OperationStatus.Error, message, builder);
                obj.AddError(GeneralError, message);

                return obj;
            }

            var result = new OperationResult<T>
            {
                Message = message,
                Status = OperationStatus.Error
            };
            result.AddError(GeneralError, message);

            return result;
        }

        public static T ErrorFactory<T>(string message, Action<T> builder = null) where T : OperationResult
        {
            var obj = CreateAndConfigureResult<T>(OperationStatus.Error, message, builder);
            obj.AddError(GeneralError, message);

            return (T)obj;
        }

        public static OperationResult Error<T>(T obj, string message)
        {
            if (obj is OperationResult operationResult)
            {
                operationResult.Status = OperationStatus.Error;
                operationResult.Message = message;
                operationResult.AddError(GeneralError, message);

                return operationResult;
            }

            var result = new OperationResult<T>
            {
                Value = obj,
                Message = message,
                Status = OperationStatus.Error
            };
            result.AddError(GeneralError, message);

            return result;
        }

        public static OperationResult Error(OperationResult parentResult)
        {
            if (parentResult == null)
                throw new ArgumentNullException(nameof(parentResult));

            var result = new OperationResult
            {
                Message = parentResult.Message,
                Status = OperationStatus.Error
            };
            result.AddErrors(parentResult.Errors.ToDictionary(x => x.Key, x => x.Value));

            return result;
        }

        public static OperationResult Error(Dictionary<string, string> errors)
        {
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));
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

        public static OperationResult Success() => new OperationResult { Status = OperationStatus.Success };

        public static OperationResult Success(string message) => new OperationResult
        {
            Message = message,
            Status = OperationStatus.Success
        };

        public static OperationResult Success<T>(string message)
        {
            if (typeof(OperationResult).IsAssignableFrom(typeof(T)))
            {
                return CreateAndConfigureResult<T>(OperationStatus.Success, message);
            }

            return new OperationResult<T>
            {
                Message = message,
                Status = OperationStatus.Success
            };
        }

        public static OperationResult Success<T>(T obj, string message)
        {
            if (obj is OperationResult operationResult)
            {
                operationResult.Status = OperationStatus.Success;
                operationResult.Message = message;

                return operationResult;
            }

            return new OperationResult<T>
            {
                Value = obj,
                Message = message,
                Status = OperationStatus.Success
            };
        }

        public static OperationResult Success<T>(string message, Action<T> builder = null)
        {
            if (typeof(OperationResult).IsAssignableFrom(typeof(T)))
            {
                return CreateAndConfigureResult<T>(OperationStatus.Success, message, builder);
            }

            return new OperationResult<T>
            {
                Message = message,
                Status = OperationStatus.Success
            };
        }

        public static T SuccessFactory<T>(string message, Action<T> builder = null) where T : OperationResult
        {
            return (T)CreateAndConfigureResult<T>(OperationStatus.Success, message, builder);
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

            result.AddErrors(innerOperationResult.Errors.ToDictionary(x => x.Key, x => x.Value));

            return result;
        }

        public static implicit operator bool(OperationResult operationResult)
        {
            return operationResult.Status == OperationStatus.Success;
        }

        private static OperationResult CreateAndConfigureResult<T>(OperationStatus status, string message, Action<T> builder = null)
        {
            var obj = (OperationResult)(object)Activator.CreateInstance<T>();
            obj.Status = status;
            obj.Message = message;

            if (builder != null)
            {
                builder((T)(object)obj);
            }

            return obj;
        }
    }
}
