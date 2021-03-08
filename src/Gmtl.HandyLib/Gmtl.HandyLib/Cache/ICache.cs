using System;
using System.Collections.Generic;

namespace Gmtl.HandyLib.Cache
{
    public interface ICache<TKey, TData>
    {
        Dictionary<TKey, TData> GetAll();

        List<TData> GetList();

        HLListPage<TData> FindBy<TOrder>(Func<TData, bool> filterFunc, Func<TData, TOrder> orderFunc = null,
            OrderDirection orderDirection = OrderDirection.Ascending, int page = 1, int pageSize = 10);

         TData this[TKey key] { get; }

         TData Get(TKey key);

         TData GetOrDefault(TKey key);

         void Delete(TKey key);

         void DeleteAll();

         void InsertOrUpdate(TKey key, TData newItemData);

         void UpdateCache(TKey key, TData newItemData, CacheUpdateType updatedType);
    }
}