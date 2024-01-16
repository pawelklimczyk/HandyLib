using System;
using System.Collections.Generic;
using System.Linq;

namespace Gmtl.HandyLib.Operations
{
    /// <summary>
    /// Class for managing multiple operations and their statuses
    /// </summary>
    public class Operation<TResult>
    {
        /// <summary>
        /// Message if Func return false
        /// </summary>
        public string ErrorMsg { get; private set; }

        /// <summary>
        /// Message if Func return true
        /// </summary>
        public string SuccessMsg { get; private set; }

        private OperationResult<TResult> _result;

        private Action _actionPure;
        private Func<TResult> _funcPure;
        private Func<OperationResult<TResult>> _operationResultFunc;

        private List<Action> _successOperations = new List<Action>();
        private List<Action> _errorOperations = new List<Action>();
        private List<Action> _alwaysOperations = new List<Action>();

        public OperationResult<TResult> Execute()
        {
            try
            {
                if (_funcPure != null)
                {
                    _result = OperationResult<TResult>.Success(_funcPure());
                }
                else if (_operationResultFunc != null)
                {
                    _result = _operationResultFunc();
                }
                else if (_actionPure != null)
                {
                    _actionPure();
                    _result = OperationResult<TResult>.Success();
                }
            }
            catch
            {
                _result = OperationResult<TResult>.Error(value: default(TResult), ErrorMsg);
            }

            switch (_result.Status)
            {
                case OperationStatus.Success:
                    _successOperations.ForEach(a => safeExecute(a));
                    break;
                case OperationStatus.Error:
                    _errorOperations.ForEach(a => safeExecute(a));
                    break;
            }

            _alwaysOperations.ForEach(a => safeExecute(a));

            return _result;
        }

        /// <summary>
        /// Executed all operation in sequece
        /// </summary>
        public static OperationResult<TResult> ExecuteAll(params Operation<TResult>[] operations)
        {
            foreach (var op in operations)
            {
                var r = op.Execute();
                if (r.Status == OperationStatus.Error)
                    return r;
            }

            return OperationResult<TResult>.Success(value: default(TResult), "All operations were successful");
        }

        /// <summary>
        /// Create operation that was already executed. Just pass the result
        /// </summary>
        public static Operation<TResult> Create(OperationResult<TResult> result)
        {
            return new Operation<TResult>()
            {
                _result = result,
                ErrorMsg = result.Status == OperationStatus.Error ? string.Join(", ", result.Errors.Select(x => x.Key + ": " + x.Value    )) : null,
                SuccessMsg = result.Status == OperationStatus.Success ? result.Message : null
            };
        }

        /// <summary>
        /// Function returning pure value
        /// </summary>
        /// <param name="func"></param>
        /// <param name="errorMsg"></param>
        /// <param name="successMsg"></param>
        /// <returns></returns>
        public static Operation<TResult> Create(Func<TResult> func, string errorMsg = "Error", string successMsg = "Ok")
        {
            return new Operation<TResult>()
            {
                _funcPure = func,
                ErrorMsg = errorMsg,
                SuccessMsg = successMsg
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="errorMsg"></param>
        /// <param name="successMsg"></param>
        /// <returns></returns>
        public static Operation<TResult> Create(Action action, string errorMsg = "Error", string successMsg = "Ok")
        {
            return new Operation<TResult>()
            {
                _actionPure = action,
                ErrorMsg = errorMsg,
                SuccessMsg = successMsg
            };
        }

        /// <summary>
        /// Function returning OperationResult value
        /// </summary>
        /// <param name="func"></param>
        /// <param name="errorMsg"></param>
        /// <param name="successMsg"></param>
        /// <returns></returns>
        public static Operation<TResult> Create(Func<OperationResult<TResult>> func, string errorMsg = "Error", string successMsg = "Ok")
        {
            return new Operation<TResult>()
            {
                _operationResultFunc = func,
                ErrorMsg = errorMsg,
                SuccessMsg = successMsg
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Operation<TResult> IfSuccess(Action action)
        {
            _successOperations.Add(action);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Operation<TResult> IfError(Action action)
        {
            _errorOperations.Add(action);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Operation<TResult> Always(Action action)
        {
            _alwaysOperations.Add(action);
            return this;
        }

        private void safeExecute(Action action)
        {
            try
            {
                action();
            }
            catch { }
        }


        //approach to pass arguments into sub-operations

        //private Func<object, OperationResult<TResult>> Func1 { get; set; }
        //private Func<object, object, OperationResult<TResult>> Func2 { get; set; }
        //public OperationResult<TResult> Execute<T1>(T1 arg1)
        //{
        //    try
        //    {
        //        result = Func1(arg1);
        //    }
        //    catch
        //    {
        //        result = OperationResult<TResult>.Error(value: default(TResult), ErrorMsg);
        //    }

        //    return result;
        //}

        //public OperationResult<TResult> Execute<T1, T2>(T1 arg1, T2 arg2)
        //{
        //    try
        //    {
        //        result = Func2(arg1, arg2);
        //    }
        //    catch
        //    {
        //        result = OperationResult<TResult>.Error(value: default(TResult), ErrorMsg);
        //    }

        //    return result;
        //}
        //public static Operation<OperationResult<TResult>> Create<T1, TResult>(Func<T1, OperationResult<TResult>> func, string errorMsg = "Error", string successMsg = "Ok")
        //{
        //    return new Operation<TResult>()
        //    {
        //        Func1 = func,
        //        ErrorMsg = errorMsg,
        //        SuccessMsg = successMsg
        //    };
        //}

        //public static Operation<OperationResult<TResult>> Create<T1, T2, TResult>(Func<T1, T2, OperationResult<TResult>> func, string errorMsg = "Error", string successMsg = "Ok")
        //{
        //    return new Operation<TResult>()
        //    {
        //        Func2 = func,
        //        ErrorMsg = errorMsg,
        //        SuccessMsg = successMsg
        //    };
        //}

    }
}