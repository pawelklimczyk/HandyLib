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
        private static Dictionary<string, Stopwatch> watchers = new Dictionary<string, Stopwatch>();
        private static object locker = new object();
        private static string defaultKey = HLRandomizer.RandomString.Next(10, 15);

        /// <summary>
        /// Starts the watched with provided or default key
        /// </summary>
        public static string Start(string key = null)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                key = defaultKey;
            }

            lock (locker)
            {
                if (watchers.ContainsKey(key))
                {
                    throw new Exception($"StopWatch already exists. Clean it with Clean({key}).");
                }

                Stopwatch watch = new Stopwatch();
                watch.Start();
                watchers.Add(key, watch);
            }

            return key;
        }

        /// <summary>
        /// Stops the watcher and return elapsed milliseconds
        /// </summary>
        public static long Stop(string key = null)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                key = defaultKey;
            }

            lock (locker)
            {
                if (watchers.ContainsKey(key))
                {
                    Stopwatch watch = new Stopwatch();
                    long elapsed = watchers[key].ElapsedMilliseconds;
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
        public static long Elapsed(string key = null)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                key = defaultKey;
            }

            lock (locker)
            {
                if (watchers.ContainsKey(key))
                {
                    return watchers[key].ElapsedMilliseconds;
                }
                else
                {
                    throw new Exception($"Watcher with key: {key} does not exists.");
                }
            }
        }

        public static void Clean(string key)
        {
            lock (locker)
            {
                CleanNoLock(key);
            }
        }

        private static void CleanNoLock(string key)
        {
            if (watchers.ContainsKey(key))
            {
                Stopwatch watch = watchers[key];
                if (watch != null)
                {
                    watch.Stop();
                }
                watchers.Remove(key);
            }
        }
    }
}
