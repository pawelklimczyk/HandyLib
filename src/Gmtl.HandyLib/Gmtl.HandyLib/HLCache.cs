using System;
using System.Collections.Generic;
using System.Linq;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Cache cna by initialized via initializeFunc method or by Insert method
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class Cache<TKey, TData>
    {
        private readonly Func<Dictionary<TKey, TData>> _initializerFunction;
        private static Dictionary<TKey, TData> data = new Dictionary<TKey, TData>();
        private static bool isInitialized = false;
        private static object _initializeLock = new object();
        private static object _deleteLock = new object();
        private static object _insertLock = new object();

        public Cache() { }

        public Cache(Func<Dictionary<TKey, TData>> initializerFunction)
        {
            _initializerFunction = initializerFunction;
        }

        /// <summary>
        /// Return a copy of the whole data in cache
        /// </summary>
        /// <returns></returns>
        public Dictionary<TKey, TData> GetAll()
        {
            return new Dictionary<TKey, TData>(data);
        }

        /// <summary>
        /// Return a page of data in cache
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="filterFunc"></param>
        /// <param name="orderFunc"></param>
        /// <param name="orderDirection"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public HLListPage<TData> FindBy<TOrder>(Func<TData, bool> filterFunc, Func<TData, TOrder> orderFunc = null, OrderDirection orderDirection = OrderDirection.Ascending, int page = 1, int pageSize = 10)
        {
            int itemsToSkip = (page < 0 ? 1 : (page - 1)) * pageSize;

            if (!isInitialized) Initialize();

            var query = data.Values.Where(filterFunc);
            if (orderFunc != null)
            {
                if (orderDirection == OrderDirection.Ascending)
                    query = query.OrderBy(orderFunc);
                else
                    query = query.OrderByDescending(orderFunc);
            }

            var list = query.Skip(itemsToSkip).Take(pageSize).ToList();
            var totalCount = data.Values.Count(filterFunc);
            HLListPage<TData> dataToReturn = new HLListPage<TData>(list, totalCount, page, pageSize);

            return dataToReturn;
        }

        private void Initialize()
        {
            if (isInitialized) return;

            lock (_initializeLock)
            {
                if (isInitialized) return;

                if (_initializerFunction != null)
                    data = _initializerFunction();

                isInitialized = true;
            }
        }

        public TData this[TKey key] => data[key];

        public TData Get(TKey key)
        {
            return data[key];
        }

        public TData GetOrDefault(TKey key)
        {
            if (data.ContainsKey(key)) return data[key];

            return default(TData);
        }

        public void Delete(TKey key)
        {
            lock (_deleteLock)
            {
                if (data.ContainsKey(key))
                    data.Remove(key);
            }
        }

        public void InsertOrUpdate(TKey key, TData newItemData)
        {
            lock (_insertLock) { 
            if (data.ContainsKey(key))
                data[key] = newItemData;
            else
                data.Add(key, newItemData);
            }
        }
    }
}
