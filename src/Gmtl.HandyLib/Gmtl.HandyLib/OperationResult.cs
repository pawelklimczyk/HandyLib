// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="OperationResult.cs" project="Gmtl.HandyLib" date="2016-04-02 17:09">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

namespace Gmtl.HandyLib
{
    public class OperationResult<T>
    {
        /// <summary>
        /// Result value
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Operation status
        /// </summary>
        public OperationalStatus Status { get; set; }
        
        /// <summary>
        /// Extra info send with operation result (optional)
        /// </summary>
        public string Message { get; set; }
    }

    public enum OperationalStatus
    {
        Success,
        Warning, 
        Error
    }
}