using System;
using System.Collections.Generic;
using System.Linq;

namespace Gmtl.HandyLib.Cache
{
    /// <summary>
    /// Cache cna by initialized via initializeFunc method or by Insert method
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class Cache<TKey, TData> : ICache<TKey, TData>
    {
        private readonly Func<Dictionary<TKey, TData>> _initializerFunction;
        private readonly bool _autoMaintainList;
        private static Dictionary<TKey, TData> _data = new Dictionary<TKey, TData>();
        private static List<TData> _inCacheList = new List<TData>();

        private static bool _isInitialized = false;
        private static object _initializeLock = new object();
        private static object _deleteLock = new object();
        private static object _insertLock = new object();

        public bool Empty => _data.Count == 0;

        public Cache() { }

        public Cache(Func<Dictionary<TKey, TData>> initializerFunction)
        {
            _initializerFunction = initializerFunction;
        }

        public Cache(Func<Dictionary<TKey, TData>> initializerFunction, bool autoMaintainList)
        {
            _initializerFunction = initializerFunction;
            _autoMaintainList = autoMaintainList;
        }

        public Cache(bool autoMaintainList)
        {
            _autoMaintainList = autoMaintainList;
        }

        /// <summary>
        /// Return a copy of the whole data in cache
        /// </summary>
        /// <returns></returns>
        public Dictionary<TKey, TData> GetAll()
        {
            return new Dictionary<TKey, TData>(_data);
        }

        public List<TData> GetList()
        {
            if (_autoMaintainList)
                return _inCacheList;

            return _data.Values.ToList();
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

            if (!_isInitialized) Initialize();

            var query = _data.Values.Where(filterFunc);
            if (orderFunc != null)
            {
                query = orderDirection == OrderDirection.Ascending ? query.OrderBy(orderFunc) : query.OrderByDescending(orderFunc);
            }

            var list = query.Skip(itemsToSkip).Take(pageSize).ToList();
            var totalCount = _data.Values.Count(filterFunc);
            HLListPage<TData> dataToReturn = new HLListPage<TData>(list, totalCount, page, pageSize);

            return dataToReturn;
        }

        private void Initialize()
        {
            if (_isInitialized) return;

            lock (_initializeLock)
            {
                if (_isInitialized) return;

                if (_initializerFunction != null)
                {
                    _data = _initializerFunction();
                    if (_autoMaintainList)
                    {
                        BuildListFromDict();
                    }
                }

                _isInitialized = true;
            }
        }

        private void BuildListFromDict()
        {
            _inCacheList = _data.Values.ToList();
        }

        public TData this[TKey key] => _data[key];

        public TData Get(TKey key)
        {
            return _data[key];
        }

        public TData GetOrDefault(TKey key)
        {
            if (_data.ContainsKey(key)) return _data[key];

            return default(TData);
        }

        public bool HasKey(TKey key)
        {
            return _data.ContainsKey(key);
        }

        public void Delete(TKey key)
        {
            lock (_deleteLock)
            {
                if (_data.ContainsKey(key))
                    _data.Remove(key);
                if (_autoMaintainList)
                    BuildListFromDict();
            }
        }

        public void DeleteAll()
        {
            lock (_deleteLock)
            {
                _data.Clear();
                if (_autoMaintainList)
                    BuildListFromDict();
            }
        }

        public void InsertOrUpdate(TKey key, TData newItemData)
        {
            lock (_insertLock)
            {
                if (_data.ContainsKey(key))
                    _data[key] = newItemData;
                else
                    _data.Add(key, newItemData);

                if (_autoMaintainList)
                    BuildListFromDict();
            }
        }

        public void UpdateCache(TKey key, TData newItemData, CacheUpdateType updatedType)
        {
            switch (updatedType)
            {
                case CacheUpdateType.Override:
                    {
                        InsertOrUpdate(key, newItemData);
                        break;
                    }
                case CacheUpdateType.Delete:
                    {
                        Delete(key);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(updatedType), updatedType, null);
            }
        }
    }
}
