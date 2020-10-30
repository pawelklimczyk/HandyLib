// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLListPage.cs" project="Gmtl.HandyLib" date="2016-06-05 15:27">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Provides base class for 'pagination'
    /// </summary>
    /// <typeparam name="T">List item type</typeparam>
    public class HLListPage<T> : IEnumerable<T>
    {
        private readonly List<T> itemsOnPage;

        public static int DefaultPageSize = 20;

        public HLListPage(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = DefaultPageSize;

            if (items == null)
                throw new ArgumentNullException("items", "superset cannot be null.");

            TotalCount = totalCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (TotalCount / PageSize) + (TotalCount % PageSize);
            itemsOnPage = new List<T>(items);
        }

        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        /// <summary>
        /// Actual number of items on page
        /// </summary>
        public int PageCount
        {
            get { return this.itemsOnPage.Count; }
        }

        public T this[int index]
        {
            get { return this.itemsOnPage[index]; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.itemsOnPage.GetEnumerator();
        }

        public bool IsEmpty
        {
            get { return itemsOnPage.Count == 0; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}