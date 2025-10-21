namespace Gmtl.HandyLib.Operations
{
    /// <summary>
    /// OperationResult Extensions
    /// </summary>
    public static class OperationResultExtensions
    {
        /// <summary>
        /// Creates a successful OperationResult from the item
        /// </summary>
        public static OperationResult<T> ToOperationResult<T>(this T item)
        {
            return OperationResult<T>.Success(item);
        }

        /// <summary>
        /// Converts one OperationResult to another OperationResult type
        /// </summary>
        public static T2 ToOtherOperationResult<T1, T2>(this T1 t1)
            where T1 : OperationResult
            where T2 : OperationResult, new()
        {
            return new T2 { Message = t1.Message, Status = t1.Status };
        }
    }
}