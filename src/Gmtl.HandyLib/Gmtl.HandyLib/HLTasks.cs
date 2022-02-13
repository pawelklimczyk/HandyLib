using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// System.Threading.Tasks extensions
    /// </summary>
    public class HLTasks
    {
        /// <summary>
        /// Sleep for tasks with cancellation token support
        /// </summary>
        /// <param name="minutes">sleep in minutes</param>
        /// <param name="tokens">external tokens</param>
        /// <returns></returns>
        public static async Task PrimitiveSleep(int minutes, params CancellationToken[] tokens)
        {
            try
            {
                using (CancellationTokenSource linkedCts =
                    CancellationTokenSource.CreateLinkedTokenSource(tokens))
                {
                    await Task.Delay(TimeSpan.FromSeconds(minutes), linkedCts.Token);
                }
            }
            catch { }
        }
    }
}
