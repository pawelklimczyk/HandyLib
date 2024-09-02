namespace Gmtl.HandyLib.Operations
{
    /// <summary>
    /// OperationResult Extensions
    /// </summary>
    public static class OperationResultExtensions
    {
        public static OperationResult<T> ToOperationResult<T>(this T item)
        {
            return OperationResult<T>.Success(item);
        }
    }
}