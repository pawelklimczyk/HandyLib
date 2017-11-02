// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLSingleton.cs" project="Gmtl.HandyLib" date="2017-11-01 18:33">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------
namespace Gmtl.HandyLib
{
    public abstract class HLSingleton<T> where T : new()
    {
        private static readonly object _locker = new object();
        private static T _instance;

        /// <summary>
        /// Class instance
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}