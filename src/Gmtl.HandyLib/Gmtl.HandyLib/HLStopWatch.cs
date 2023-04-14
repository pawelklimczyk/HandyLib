using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Helper for efficient handling Stopwatch class
    /// </summary>
    public static class HLStopWatch
    {
        private static Dictionary<string, Stopwatch> _watchers = new Dictionary<string, Stopwatch>();
        private static object _locker = new object();

        /// <summary>
        /// Starts the watched with provided or default key
        /// </summary>
        public static string Start(string key = null)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                key = HLRandomizer.RandomString.Next(16, 32);
            }

            lock (_locker)
            {
                if (_watchers.ContainsKey(key))
                {
                    throw new Exception($"StopWatch already exists. Clean it with Clean({key}).");
                }

                Stopwatch watch = new Stopwatch();
                watch.Start();
                _watchers.Add(key, watch);
            }

            return key;
        }

        /// <summary>
        /// Stops the watcher and return elapsed milliseconds
        /// </summary>
        public static long Stop(string key)
        {
            lock (_locker)
            {
                if (_watchers.ContainsKey(key))
                {
                    Stopwatch watch = new Stopwatch();
                    long elapsed = _watchers[key].ElapsedMilliseconds;
                    CleanNoLock(key);

                    return elapsed;
                }
                else
                {
                    throw new Exception($"Watcher with key: {key} does not exists.");
                }
            }
        }

        /// <summary>
        /// Returns how many milliseconds elapsed since start
        /// </summary>
        public static long Elapsed(string key)
        {
            lock (_locker)
            {
                if (_watchers.ContainsKey(key))
                {
                    return _watchers[key].ElapsedMilliseconds;
                }
                else
                {
                    throw new Exception($"Watcher with key: {key} does not exists.");
                }
            }
        }

        /// <summary>
        /// Clear StopWatch
        /// </summary>
        /// <param name="key">StopWatch key</param>
        public static void Clean(string key)
        {
            lock (_locker)
            {
                CleanNoLock(key);
            }
        }

        private static void CleanNoLock(string key)
        {
            if (_watchers.ContainsKey(key))
            {
                Stopwatch watch = _watchers[key];
                if (watch != null)
                {
                    watch.Stop();
                }
                _watchers.Remove(key);
            }
        }
    }
}
